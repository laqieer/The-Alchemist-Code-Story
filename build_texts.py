import json
import os
import shutil
import warnings

Loc = {
    'japan': {
        'path': 'assets/japan/Loc/japanese/',
        'title': 'テキスト'
    },
    'taiwan': {
        'path': 'assets/taiwan/Loc/japanese/',
        'title': '文本'
    },
    'global': {
        'path': 'assets/global/Loc/english/',
        'title': 'Texts'
    },
}

Repo = "https://github.com/laqieer/The-Alchemist-Code-Story/tree/master/"

def parse_text(filepath):
    texts = {}
    if os.path.exists(filepath):
        try:
            f = open(filepath, 'r', encoding='utf-8')
            # remove utf-8 BOM if present
            if f.read(1) != '\ufeff':
                f.seek(0)
            lines = f.readlines()
        except UnicodeDecodeError:
            try:
                f = open(filepath, 'r', encoding='utf-16')
                lines = f.readlines()
            except UnicodeDecodeError:
                f = open(filepath, 'r', encoding='gbk')
                lines = f.readlines()
        for line in lines:
            line = line.strip()
            if line and not line.startswith(';'):
                line = line.replace('     ', '\t', 1)
                kv = line.split('\t')[:2]
                key = kv[0]
                text = ''
                if len(kv) > 1:
                    text = kv[1]
                texts[key] = text
        f.close()
    return texts

LocalizedMasterParam = parse_text('assets/global/Loc/english/LocalizedMasterParam.txt')

for version in Loc.keys():
    Units = {}
    with open(f'assets/{version}/Data/MasterParam.json', 'r', encoding='utf-8') as f:
        MasterParam = json.load(f)
        for unit in MasterParam['Unit']:
            Units[unit['iname']] = {}
            if 'img' in unit:
                for folder in ('Portraits', 'PortraitsM'):
                    img_path = f'assets/japan/{folder}/{unit["img"]}.png'
                    if os.path.exists(img_path):
                        break
                if not os.path.exists(img_path):
                    img_path = f'assets/japan/PortraitsS/{unit["img"]}/{unit["img"]}_normal.png'
                if os.path.exists(img_path):
                    shutil.copy(img_path, f'docs/img/portraits/{unit["img"]}.png')
                    Units[unit['iname']]['img'] = unit['img']
            if version == 'global':
                Units[unit['iname']]['name'] = LocalizedMasterParam.get(f'SRPG_UnitParam_{unit["iname"]}_NAME', unit['name'])
            else:
                Units[unit['iname']]['name'] = unit['name']
    with open(f'docs/{version}/texts.html', 'w', encoding='utf-8') as f_texts:
        f_texts.write('<html>\n')
        f_texts.write('<head>\n')
        f_texts.write('<meta charset="utf-8">\n')
        f_texts.write(f'<title>{Loc[version]["title"]}</title>\n')
        f_texts.write('<link rel="icon" href="../img/favicons/favicon.ico">\n')
        f_texts.write('</head>\n')
        f_texts.write('<body>\n')
        f_texts.write(f'<h1>{Loc[version]["title"]}</h1>\n')
        f_texts.write('<ul>\n')

        all_texts = {}
        for file in sorted(os.listdir(Loc[version]['path'])):
            if file.endswith('.txt') and file != 'dummy.txt':
                texts = parse_text(os.path.join(Loc[version]['path'], file))
                name = file.split('.')[0]
                for text_key, text in texts.items():
                    all_texts[name + '.' + text_key] = text
        for file in sorted(os.listdir('res/Tagatame-Datamine/Events/')):
            name = file.split('.')[0]
            if os.path.exists(os.path.join(Loc[version]['path'], name + '.txt')):
                event_actions = []
                with open(os.path.join('res/Tagatame-Datamine/Events/', file), 'r', encoding='utf-8') as f:
                    event_actions = json.load(f)['Actions']
                if len(event_actions) == 0:
                    continue
                print('Building page for {}/{}'.format(version, name))
                with open('docs/{}/{}.html'.format(version, name), 'w', encoding='utf-8') as f:
                    f.write('<html>\n')
                    f.write('<head>\n')
                    f.write('<meta charset="utf-8">\n')
                    f.write('<title>{}</title>\n'.format(name))
                    f.write('<link rel="icon" href="../img/favicons/favicon.ico">\n')
                    f.write('</head>\n')
                    f.write('<body>\n')
                    f.write('<h1>{}</h1>\n'.format(name))
                    f.write('<p><a href="{}{}.txt">View Source</a></p>\n'.format(Repo, Loc[version]['path'] + name))
                    f.write('<table>\n')
                    for action in event_actions:
                        if 'TextID' in action:
                            speaker = ''
                            unitID = action.get('UnitID', action.get('CharaID', action.get('ActorID', '')))
                            if unitID in Units:
                                unit = Units[unitID]
                                if 'img' in unit:
                                    speaker = f'<img src="../img/portraits/{unit["img"]}.png" alt="{unit["name"]}" height="128"><br>'
                                speaker += unit['name']
                            elif unitID == '2DPlus':
                                speaker = 'VO'
                            else:
                                speaker = unitID
                            f.write('<tr>\n')
                            f.write('<td>{}</td>\n'.format(speaker))
                            textID = action['TextID'].replace(' ', '')
                            if textID not in all_texts:
                                warnings.warn(f'TextID {textID} not found')
                            f.write('<td>{}</td>\n'.format(all_texts.get(textID, textID)))
                            f.write('</tr>\n')
                        elif 'Background' in action or 'BackgroundImage' in action:
                            bg = action.get('Background', action.get('BackgroundImage'))
                            if type(bg) is str and len(bg) > 0:
                                f.write('<tr>\n')
                                f.write(f'<td colspan="2"><img src="../img/backgrounds/{bg}.png" alt="{bg}"></td>')
                                f.write('</tr>\n')
                        elif 'Filename' in action:
                            f.write('<tr>\n')
                            f.write(f'<td colspan="2"><video controls><source src="https://alchemist-dlc2.gu3.jp/assets_ex/39c5254f/movies/{action["Filename"]}.mp4" type="video/mp4"></video></td>')
                            f.write('</tr>\n')
                    f.write('</body>\n')
                    f.write('</html>\n')
                    f_texts.write('<li><a href="{}.html">{}</a></li>\n'.format(name, name))
        
        f_texts.write('</ul>\n')
        f_texts.write('</body>\n')
        f_texts.write('</html>\n')
