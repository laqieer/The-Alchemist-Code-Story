def AbilityParam(json):
    this={}#AbilityParamjson)
    #if(json==null)
    #returnfalse
    if 'iname' in json:
        this['iname'] = json['iname']
    if 'name' in json:
        this['name'] = json['name']
    if 'expr' in json:
        this['expr'] = json['expr']
    if 'icon' in json:
        this['icon'] = json['icon']
    if 'type' in json:
        this['type'] = ENUM['EAbilityType'][json['type']]
    if 'slot' in json:
        this['slot'] = ENUM['EAbilitySlot'][json['slot']]
    if 'cap' in json:
        this['lvcap'] = Math.Max(json['cap'],1)
    if 'fix' in json:
        this['is_fixed'] = json['fix']!=0
    #intlength=0
    #stringstrArray=newstring[10]{json.skl1,json.skl2,json.skl3,json.skl4,json.skl5,json.skl6,json.skl7,json.skl8,json.skl9,json.skl10}
    #for(intindex=0index<strArray.Length&&!string.IsNullOrEmpty(strArray)++index)
    #++length
    #if(length>0)
        #intnumArray=newint[10]{json.lv1,json.lv2,json.lv3,json.lv4,json.lv5,json.lv6,json.lv7,json.lv8,json.lv9,json.lv10}
        #this.skills=newLearningSkill[length]
        #for(intindex=0index<length++index)
            #this.skills=newLearningSkill()
            #this.skills.iname=strArray
            #this.skills.locklv=numArray
    #this.condition_units=(string)null
    #if(json.units!=null&&json.units.Length>0)
        #for(intindex=0index<json.units.Length++index)
        if 'units' in json:
            this['condition_units'] = json['units']
    if 'units_cnds_type' in json:
        this['units_conditions_type'] = ENUM['EUseConditionsType'][json['units_cnds_type']]
    #this.condition_jobs=(string)null
    #if(json.jobs!=null&&json.jobs.Length>0)
        #for(intindex=0index<json.jobs.Length++index)
        if 'jobs' in json:
            this['condition_jobs'] = json['jobs']
    if 'jobs_cnds_type' in json:
        this['jobs_conditions_type'] = ENUM['EUseConditionsType'][json['jobs_cnds_type']]
    if 'birth' in json:
        this['condition_birth'] = json['birth']
    if 'sex' in json:
        this['condition_sex'] = ENUM['ESex'][json['sex']]
    if 'elem' in json:
        this['condition_element'] = ENUM['EElement'][json['elem']]
    if 'rmin' in json:
        this['condition_raremin'] = json['rmin']
    if 'rmax' in json:
        this['condition_raremax'] = json['rmax']
    if 'type_detail' in json:
        this['type_detail'] = ENUM['EAbilityTypeDetail'][json['type_detail']]
    #returntrue
return this
