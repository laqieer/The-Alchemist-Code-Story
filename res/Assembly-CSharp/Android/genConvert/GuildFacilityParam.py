def GuildFacilityParam(json):
    this={}#GuildFacilityParamjson)
    if 'iname' in json:
        this['iname'] = json['iname']
    if 'name' in json:
        this['name'] = json['name']
    if 'image' in json:
        this['image'] = json['image']
    if 'type' in json:
        this['type'] = json['type']
    if 'rel_cnds_type' in json:
        this['release_cnds_type'] = json['rel_cnds_type']
    if 'rel_cnds_val1' in json:
        this['release_cnds_val1'] = json['rel_cnds_val1']
    if 'rel_cnds_val2' in json:
        this['release_cnds_val2'] = json['rel_cnds_val2']
    if 'effects' in json:
        this['effects'] = newGuildFacilityEffectParam[json['effects'].Length]
    #for(intindex=0index<json.effects.Length++index)
        #this.effects=newGuildFacilityEffectParam()
        #this.effects.Deserialize(json.effects)
    #returntrue
return this
