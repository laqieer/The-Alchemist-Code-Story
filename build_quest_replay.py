import os
import json
from enum import Enum

# Refre to: https://github.com/laqieer/Tagatame-Datamine/blob/main/DMM/Assembly-CSharp/SRPG/QuestTypes.cs
class QuestTypes(Enum):
    Story = 0
    Multi = 1
    Arena = 2
    Tutorial = 3
    Free = 4
    Event = 5
    Character = 6
    Tower = 7
    VersusFree = 8
    VersusRank = 9
    Gps = 10
    StoryExtra = 11
    MultiTower = 12
    Beginner = 13
    MultiGps = 14
    Ordeal = 15
    RankMatch = 16
    Raid = 17
    GenesisStory = 18
    GenesisBoss = 19
    AdvanceStory = 20
    AdvanceBoss = 21
    UnitRental = 22
    GuildRaid = 23
    GvG = 24
    WorldRaid = 25
    CombatPower = 26
    DragonGod = 27
    PointEvent = 28
    LeagueMatch = 29
    GuildRaidTrial = 30
    NONE = 127

# Refre to: res\Assembly-CSharp\DMM\SRPG\ReplayCategoryList.cs
class ReplayCategoryType(Enum):
    Invalid = -1
    Story = 0
    Event = 1
    Character = 2
    Movie = 3

categories = {
    ReplayCategoryType.Story: {
        'worlds': [
            "WD_01",
            "WD_06",
            "WD_10",
            "WD_13",
            "WD_14",
            "WD_15",
            "WD_16",
            "WD_17",
            "WD_18",
            "WD_19",
            "WD_SEISEKI",
            "WD_BABEL",
        ],
        'name': {
            'japan': 'ストーリークエスト',
            'taiwan': '劇情任務',
            'global': 'Story Quest',
        }
    },
    ReplayCategoryType.Event: {
        'worlds': [
            "WD_GENESIS",
            "WD_ADVANCE",
            "WD_DRAGONGOD",
            "WD_DAILY",
        ],
        'name': {
            'japan': 'イベントクエスト',
            'taiwan': '活動任務',
            'global': 'Event Quest',
        }
    },
    ReplayCategoryType.Character: {
        'worlds': [
            "WD_CHARA",
        ],
        'name': {
            'japan': 'キャラクタークエスト',
            'taiwan': '角色任務',
            'global': 'Character Quest',
        }
    },
}

titles = {
    'quests': {
        'japan': 'クエスト',
        'taiwan': '任務',
        'global': 'Quests',
    },
    'replay': {
        'japan': 'ストーリー回想',
        'taiwan': '劇情回顧',
        'global': 'Story Replay',
    },
    'start': {
        'japan': '開始前',
        'taiwan': '開始前',
        'global': 'Start',
    },
    'clear': {
        'japan': 'クリア後',
        'taiwan': '完成後',
        'global': 'Clear',
    },
    'battle': {
        'japan': 'バトル',
        'taiwan': '戰鬥',
        'global': 'Battle',
    },
}

for version in ('japan', 'taiwan', 'global'):
    print(f'Building quests & replay pages for {version}...')

    def makeLink(textfilename):
        if os.path.exists(f'docs/{version}/{textfilename}.html'):
            return f'<a href="{textfilename}.html">{textfilename}</a>'
        return textfilename

    worlds = {
        "WD_CHARA": {
            "iname": "WD_CHARA",
            "name": "キャラクタークエスト",
            "areas": []
        },
    }

    QuestParam = {}

    with open(f'assets/{version}/Data/QuestParam.json', 'r', encoding='utf-8') as f:
        QuestParam = json.load(f)

    for world in QuestParam['worlds']:
        world['areas'] = []
        worlds[world['iname']] = world

    areas = {}

    for area in QuestParam['areas']:
        area['quests'] = []
        areas[area['iname']] = area
        if 'chap' in area:
            worlds[area['chap']]['areas'].append(area['iname'])

    quests = {}
    questTypes = {}

    for questType in QuestTypes:
        questTypes[questType] = []

    for quest in QuestParam['quests']:
        quests[quest['iname']] = quest
        questTypes[QuestTypes(quest['type'])].append(quest['iname'])
        if 'area' in quest and quest['area'] in areas:
            areas[quest['area']]['quests'].append(quest['iname'])

    with open(f'docs/{version}/quests.html', 'w', encoding='utf-8') as f:
        f.write('<!DOCTYPE html>\n')
        f.write('<html>\n')
        f.write('<head>\n')
        f.write('<meta charset="utf-8">\n')
        f.write(f'<title>{titles["quests"][version]}</title>\n')
        f.write('<link rel="icon" href="../img/favicons/favicon.ico">\n')
        f.write('</head>\n')
        f.write('<body>\n')
        f.write(f'<h1>{titles["quests"][version]}</h1>\n')
        for questType in QuestTypes:
            if len(questTypes[questType]) > 0:
                f.write('<h2>{}</h2>\n'.format(questType.name))
                for quest in questTypes[questType]:
                    f.write('<h3>{} {} {}</h3>\n'.format(quests[quest]['iname'], quests[quest].get('title', ''), quests[quest]['name']))
                    f.write('<ul>\n')
                    if 'evst' in quests[quest]:
                        f.write('<li>{}: {}</li>\n'.format(titles['start'][version], makeLink(quests[quest]['evst'])))
                    for m in quests[quest].get('map', []):
                        if 'ev' in m:
                            f.write('<li>{}: {}</li>\n'.format(titles['battle'][version], makeLink(m['ev'])))
                    if 'evw' in quests[quest]:
                        f.write('<li>{}: {}</li>\n'.format(titles['clear'][version], makeLink(quests[quest]['evw'])))
                    f.write('</ul>\n')
        f.write('</body>\n')

    with open(f'docs/{version}/replay.html', 'w', encoding='utf-8') as f:
        f.write('<!DOCTYPE html>\n')
        f.write('<html>\n')
        f.write('<head>\n')
        f.write('<meta charset="utf-8">\n')
        f.write(f'<title>{titles["replay"][version]}</title>\n')
        f.write('<link rel="icon" href="../img/favicons/favicon.ico">\n')
        f.write('</head>\n')
        f.write('<body>\n')
        f.write(f'<h1>{titles["replay"][version]}</h1>\n')
        for category in categories:
            f.write('<h2>{}</h2>\n'.format(categories[category]['name'][version]))
            for world in categories[category]['worlds']:
                if world in worlds:
                    f.write('<h3>{}</h3>\n'.format(worlds[world]['name']))
                    for area in worlds[world]['areas']:
                        if len(areas[area]['quests']) > 0:
                            f.write('<h4>{}</h4>\n'.format(areas[area]['name']))
                            for quest in areas[area]['quests']:
                                if 'evst' in quests[quest] or 'evw' in quests[quest]:
                                    f.write('<h5>{} {}</h5>\n'.format(quests[quest].get('expr', ''), quests[quest]['name']))
                                    f.write('<ul>\n')
                                    if 'evst' in quests[quest]:
                                        f.write('<li>{}: {}</li>\n'.format(titles['start'][version], makeLink(quests[quest]['evst'])))
                                    if 'evw' in quests[quest]:
                                        f.write('<li>{}: {}</li>\n'.format(titles['clear'][version], makeLink(quests[quest]['evw'])))
                                    f.write('</ul>\n')
        f.write('</body>\n')
