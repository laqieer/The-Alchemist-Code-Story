def GuildFacilityData(json):
    this={}#GuildFacilityDatajson)
    if 'gid' in json:
        this['mGid'] = json['gid']
    if 'facility_iname' in json:
        this['mIname'] = json['facility_iname']
    if 'exp' in json:
        this['mExp'] = json['exp']
    if 'level' in json:
        this['mLevel'] = json['level']
    #this.mParam=MonoSingleton<GameManager>.Instance.MasterParam.GetGuildFacility(this.mIname)
    #returntrue
return this
