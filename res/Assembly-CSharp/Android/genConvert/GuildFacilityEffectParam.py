def GuildFacilityEffectParam(json):
    this={}#GuildFacilityEffectParamjson)
    if 'lv' in json:
        this['lv'] = json['lv']
    if 'mem_cnt' in json:
        this['member_count'] = json['mem_cnt']
    if 'sub_mas' in json:
        this['sub_master'] = json['sub_mas']
    #this.buff_effect=(string)null
    #if(json.buff!=null&&json.buff.Length>0)
        #for(intindex=0index<json.buff.Length++index)
        if 'buff' in json:
            this['buff_effect'] = json['buff']
    #returntrue
return this
