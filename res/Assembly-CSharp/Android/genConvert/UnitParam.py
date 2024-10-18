def UnitParam(json):
    this={}#UnitParamjson)
    #if(json==null)
    #returnfalse
    if 'iname' in json:
        this['iname'] = json['iname']
    if 'name' in json:
        this['name'] = json['name']
    if 'mdl' in json:
        this['model'] = json['mdl']
    if 'grow' in json:
        this['grow'] = json['grow']
    if 'piece' in json:
        this['piece'] = json['piece']
    if 'sub_piece' in json:
        this['subPiece'] = json['sub_piece']
    if 'birth' in json:
        this['birth'] = json['birth']
    if 'birth_id' in json:
        this['birthID'] = json['birth_id']
    if 'ability' in json:
        this['ability'] = json['ability']
    if 'ma_quest' in json:
        this['ma_quest'] = json['ma_quest']
    if 'sex' in json:
        this['sex'] = ENUM['ESex'][json['sex']]
    if 'rare' in json:
        this['rare'] = json['rare']
    if 'raremax' in json:
        this['raremax'] = json['raremax']
    if 'type' in json:
        this['type'] = ENUM['EUnitType'][json['type']]
    if 'elem' in json:
        this['element'] = ENUM['EElement'][json['elem']]
    #this.flag.Set(0,json.hero!=0)
    if 'sw' in json:
        this['sw'] = json['sw']
    if 'sh' in json:
        this['sh'] = json['sh']
    if 'sd' in json:
        this['sd'] = json['sd']
    if 'dbuki' in json:
        this['dbuki'] = json['dbuki']
    if 'search' in json:
        this['search'] = json['search']
    #this.flag.Set(1,json.notsmn==0)
    #if(!string.IsNullOrEmpty(json.available_at))
        #try
            if 'available_at' in json:
                this['available_at'] = DateTime.Parse
        #catch
            #this.available_at=DateTime.MaxValue
    if 'height' in json:
        this['height'] = json['height']
    if 'weight' in json:
        this['weight'] = json['weight']
    #this.jobsets=(string)null
    #this.mJobSetCache=(JobSetParam)null
    #this.tags=(string)null
    #if(json.skins!=null&&json.skins.Length>=1)
        #for(intindex=0index<json.skins.Length++index)
        if 'skins' in json:
            this['skins'] = json['skins']
    #if(UnitParam.NoJobStatus.IsExistParam(json))
        #this.no_job_status=newUnitParam.NoJobStatus()
        #this.no_job_status.SetParam(json)
    #if(this.type==EUnitType.EventUnit)
    #returntrue
    #if(json.jobsets!=null)
        #for(intindex=0index<this.jobsets.Length++index)
        if 'jobsets' in json:
            this['jobsets'] = json['jobsets']
    #if(json.tag!=null)
    if 'tag' in json:
        this['tags'] = json['tag'].Split
    #if(this.ini_status==null)
    #this.ini_status=newUnitParam.Status()
    #this.ini_status.SetParamIni(json)
    #this.ini_status.SetEnchentParamIni(json)
    #if(this.max_status==null)
    #this.max_status=newUnitParam.Status()
    #this.max_status.SetParamMax(json)
    #this.max_status.SetEnchentParamMax(json)
    if 'ls1' in json:
        this['leader_skills'][0] = json['ls1']
    if 'ls2' in json:
        this['leader_skills'][1] = json['ls2']
    if 'ls3' in json:
        this['leader_skills'][2] = json['ls3']
    if 'ls4' in json:
        this['leader_skills'][3] = json['ls4']
    if 'ls5' in json:
        this['leader_skills'][4] = json['ls5']
    if 'ls6' in json:
        this['leader_skills'][5] = json['ls6']
    if 'recipe1' in json:
        this['recipes'][0] = json['recipe1']
    if 'recipe2' in json:
        this['recipes'][1] = json['recipe2']
    if 'recipe3' in json:
        this['recipes'][2] = json['recipe3']
    if 'recipe4' in json:
        this['recipes'][3] = json['recipe4']
    if 'recipe5' in json:
        this['recipes'][4] = json['recipe5']
    if 'recipe6' in json:
        this['recipes'][5] = json['recipe6']
    if 'img' in json:
        this['image'] = json['img']
    if 'vce' in json:
        this['voice'] = json['vce']
    #this.flag.Set(2,json.no_trw==0)
    #this.flag.Set(3,json.no_kb==0)
    #this.flag.Set(4,json.no_chg==0)
    if 'unlck_t' in json:
        this['unlock_time'] = json['unlck_t']
    #returntrue
return this
