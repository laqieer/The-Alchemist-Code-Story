def SectionParam(json):
    this={}#SectionParamjson)
    #if(json==null)
    #thrownewInvalidJSONException()
    if 'iname' in json:
        this['iname'] = json['iname']
    if 'name' in json:
        this['name'] = json['name']
    if 'expr' in json:
        this['expr'] = json['expr']
    if 'start' in json:
        this['start'] = json['start']
    if 'end' in json:
        this['end'] = json['end']
    if 'hide' in json:
        this['hidden'] = json['hide']!=0
    if 'home' in json:
        this['home'] = json['home']
    if 'unit' in json:
        this['unit'] = json['unit']
    if 'item' in json:
        this['prefabPath'] = json['item']
    if 'shop' in json:
        this['shop'] = json['shop']
    if 'inn' in json:
        this['inn'] = json['inn']
    if 'bar' in json:
        this['bar'] = json['bar']
    if 'bgm' in json:
        this['bgm'] = json['bgm']
    if 'story_part' in json:
        this['storyPart'] = json['story_part']
    if 'release_key_quest' in json:
        this['releaseKeyQuest'] = json['release_key_quest']
    if 'message_sys_id' in json:
        this['message_sys_id'] = json['message_sys_id']
return this
