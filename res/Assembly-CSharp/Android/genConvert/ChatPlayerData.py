def ChatPlayerData(json):
    this={}#ChatPlayerDatajson)
    #if(json==null)
    #return
    if 'name' in json:
        this['name'] = json['name']
    if 'exp' in json:
        this['exp'] = json['exp']
    if 'lastlogin' in json:
        this['lastlogin'] = json['lastlogin']
    if 'fuid' in json:
        this['fuid'] = json['fuid']
    if 'is_friend' in json:
        this['is_friend'] = json['is_friend']
    if 'is_favorite' in json:
        this['is_favorite'] = json['is_favorite']
    #this.lv=PlayerData.CalcLevelFromExp(this.exp)
    if 'award' in json:
        this['award'] = json['award']
    #if(json.unit==null)
    #return
    #UnitDataunitData=newUnitData()
    #unitData.Deserialize(json.unit)
    #this.unit=unitData
return this
