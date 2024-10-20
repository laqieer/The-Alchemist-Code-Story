import os

Loc = {
    'japan': {
        'path': 'assets/japan/Loc/japanese/',
        'name': '日本語'
    },
    'taiwan': {
        'path': 'assets/taiwan/Loc/japanese/',
        'name': '繁體中文'
    },
    'global': {
        'path': 'assets/global/Loc/english/',
        'name': 'English'
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

with open('docs/all.html', 'w', encoding='utf-8') as f_all:
    f_all.write('<html>\n')
    f_all.write('<head>\n')
    f_all.write('<meta charset="utf-8">\n')
    f_all.write('<title>All</title>\n')
    f_all.write('<link rel="icon" href="img/favicons/favicon.ico">\n')
    f_all.write('</head>\n')
    f_all.write('<body>\n')
    f_all.write('<h1>All</h1>\n')
    f_all.write('<ul>\n')

    for file in sorted(os.listdir(Loc['japan']['path'])):
        if file.endswith('.txt') and file != 'dummy.txt':
            name = file.split('.')[0]
            print('Building page for {}'.format(name))
            with open('docs/{}.html'.format(name), 'w', encoding='utf-8') as f:
                f.write('<html>\n')
                f.write('<head>\n')
                f.write('<meta charset="utf-8">\n')
                f.write('<title>{}</title>\n'.format(name))
                f.write('<link rel="icon" href="img/favicons/favicon.ico">\n')
                f.write('</head>\n')
                f.write('<body>\n')
                f.write('<h1>{}</h1>\n'.format(name))
                texts_japan = parse_text(os.path.join(Loc['japan']['path'], file))
                if not texts_japan:
                    continue
                texts_taiwan = parse_text(os.path.join(Loc['taiwan']['path'], file))
                texts_global = parse_text(os.path.join(Loc['global']['path'], file))
                f.write('<table>\n')
                f.write('<tr>\n')
                # print text keys
                f.write('<th>ID</th>\n')
                f.write('<th><a href="{}">{}</a></th>'.format(Repo + Loc['japan']['path'] + file, Loc['japan']['name']))
                if texts_taiwan:
                    f.write('<th><a href="{}">{}</a></th>'.format(Repo + Loc['taiwan']['path'] + file, Loc['taiwan']['name']))
                if texts_global:
                    f.write('<th><a href="{}">{}</a></th>'.format(Repo + Loc['global']['path'] + file, Loc['global']['name']))
                f.write('</tr>\n')
                for key in texts_japan:
                    f.write('<tr>\n')
                    f.write('<td>{}</td>\n'.format(key))
                    f.write('<td>{}</td>\n'.format(texts_japan[key]))
                    if texts_taiwan:
                        f.write('<td>{}</td>\n'.format(texts_taiwan.get(key, '')))
                    if texts_global:
                        f.write('<td>{}</td>\n'.format(texts_global.get(key, '')))
                    f.write('</tr>\n')
                f.write('</body>\n')
                f.write('</html>\n')
                f_all.write('<li><a href="{}.html">{}</a></li>\n'.format(name, name))
    
    f_all.write('</ul>\n')
    f_all.write('</body>\n')
    f_all.write('</html>\n')
