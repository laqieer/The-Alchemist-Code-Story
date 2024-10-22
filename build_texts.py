import os

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

for version in Loc.keys():
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

        for file in sorted(os.listdir(Loc[version]['path'])):
            if file.endswith('.txt') and file != 'dummy.txt':
                texts = parse_text(os.path.join(Loc[version]['path'], file))
                if not texts:
                    continue
                name = file.split('.')[0]
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
                    f.write('<p><a href="{}{}">View Source</a></p>\n'.format(Repo, Loc[version]['path'] + file))
                    f.write('<table>\n')
                    for key in texts:
                        f.write('<tr>\n')
                        f.write('<td>{}</td>\n'.format(key))
                        f.write('<td>{}</td>\n'.format(texts[key]))
                        f.write('</tr>\n')
                    f.write('</body>\n')
                    f.write('</html>\n')
                    f_texts.write('<li><a href="{}.html">{}</a></li>\n'.format(name, name))
        
        f_texts.write('</ul>\n')
        f_texts.write('</body>\n')
        f_texts.write('</html>\n')
