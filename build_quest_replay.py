import os
import json
from enum import Enum

QuestParam = {}

with open('assets/japan/Data/QuestParam.json', 'r', encoding='utf-8') as f:
    QuestParam = json.load(f)

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
categories = {
    "ストーリークエスト(Story Quest)": {
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
    },
    "イベントクエスト(Event Quest)": {
        'worlds': [
            "WD_GENESIS",
            "WD_ADVANCE",
            "WD_DRAGONGOD",
            "WD_DAILY",
        ],
    },
    "キャラクタークエスト(Character Quest)": {
        'worlds': [
            "WD_CHARA",
        ],
    },
}

worlds = {
    "WD_CHARA": {
        "iname": "WD_CHARA",
        "name": "キャラクタークエスト",
        "areas": []
    },
}

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
    if 'area' in quest:
        areas[quest['area']]['quests'].append(quest['iname'])

with open('docs/quests.html', 'w', encoding='utf-8') as f:
    f.write('<!DOCTYPE html>\n')
    f.write('<html>\n')
    f.write('<head>\n')
    f.write('<meta charset="utf-8">\n')
    f.write('<title>Quest</title>\n')
    f.write('<link rel="icon" href="img/favicons/favicon.ico">\n')
    f.write('</head>\n')
    f.write('<body>\n')
    f.write('<h1>Quest</h1>\n')
    for questType in QuestTypes:
        f.write('<h2>{}</h2>\n'.format(questType.name))
        for quest in questTypes[questType]:
            f.write('<h3>{} {} {}</h3>\n'.format(quests[quest]['iname'], quests[quest].get('title', ''), quests[quest]['name']))
            f.write('<ul>\n')
            if 'evst' in quests[quest]:
                f.write('<li><a href="{}.html">Start: {}</a></li>\n'.format(quests[quest]['evst'], quests[quest]['evst']))
            for m in quests[quest].get('map', []):
                if 'ev' in m:
                    f.write('<li><a href="{}.html">Map: {}</a></li>\n'.format(m['ev'], m['ev']))
            if 'evw' in quests[quest]:
                f.write('<li><a href="{}.html">Clear: {}</a></li>\n'.format(quests[quest]['evw'], quests[quest]['evw']))
            f.write('</ul>\n')
    f.write('</body>\n')

with open('docs/replay.html', 'w', encoding='utf-8') as f:
    f.write('<!DOCTYPE html>\n')
    f.write('<html>\n')
    f.write('<head>\n')
    f.write('<meta charset="utf-8">\n')
    f.write('<title>Story Replay</title>\n')
    f.write('<link rel="icon" href="img/favicons/favicon.ico">\n')
    f.write('</head>\n')
    f.write('<body>\n')
    f.write('<h1>ストーリー回想</h1>\n')
    for category in categories:
        f.write('<h2>{}</h2>\n'.format(category))
        for world in categories[category]['worlds']:
            f.write('<h3>{}</h3>\n'.format(worlds[world]['name']))
            for area in worlds[world]['areas']:
                f.write('<h4>{}</h4>\n'.format(areas[area]['name']))
                for quest in areas[area]['quests']:
                    if 'evst' in quests[quest] or 'evw' in quests[quest]:
                        f.write('<h5>{} {}</h5>\n'.format(quests[quest].get('expr', ''), quests[quest]['name']))
                        f.write('<ul>\n')
                        if 'evst' in quests[quest]:
                            f.write('<li><a href="{}.html">開始前: {}</a></li>\n'.format(quests[quest]['evst'], quests[quest]['evst']))
                        if 'evw' in quests[quest]:
                            f.write('<li><a href="{}.html">クリア後: {}</a></li>\n'.format(quests[quest]['evw'], quests[quest]['evw']))
                        f.write('</ul>\n')
    f.write('</body>\n')
