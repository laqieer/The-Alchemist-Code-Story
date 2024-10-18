def GuildFacilityLvParam(json):
    this={}#GuildFacilityLvParamjson)
    if 'lv' in json:
        this['lv'] = json['lv']
    if 'base_camp' in json:
        this['base_camp'] = json['base_camp']
    #returntrue
return this
