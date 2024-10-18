def SkillAbilityDeriveParam(json):
    this={}#SkillAbilityDeriveParamjson)
    if 'iname' in json:
        this['iname'] = json['iname']
    #ESkillAbilityDeriveCondsarray1=((IEnumerable<ESkillAbilityDeriveConds>)newESkillAbilityDeriveConds[3]{(ESkillAbilityDeriveConds)json.trig_type_1,(ESkillAbilityDeriveConds)json.trig_type_2,(ESkillAbilityDeriveConds)json.trig_type_3}).Where<ESkillAbilityDeriveConds>((Func<ESkillAbilityDeriveConds,bool>)(trig_type=>trig_type!=ESkillAbilityDeriveConds.Unknown)).ToArray<ESkillAbilityDeriveConds>()
    #stringarray2=((IEnumerable<string>)newstring[3]{json.trig_iname_1,json.trig_iname_2,json.trig_iname_3}).Where<string>((Func<string,bool>)(trig_iname=>!string.IsNullOrEmpty(trig_iname))).ToArray<string>()
    #this.deriveTriggers=newSkillAbilityDeriveTriggerParam[array2.Length]
    #for(intindex=0index<array2.Length++index)
    #this.deriveTriggers=newSkillAbilityDeriveTriggerParam(array1,array2)
    #if(json.base_abils!=null)
        #for(intindex=0index<this.base_abils.Length++index)
        if 'base_abils' in json:
            this['base_abils'] = json['base_abils']
    #if(json.derive_abils!=null)
        #for(intindex=0index<this.derive_abils.Length++index)
        if 'derive_abils' in json:
            this['derive_abils'] = json['derive_abils']
    #if(json.base_skills!=null)
        #for(intindex=0index<this.base_skills.Length++index)
        if 'base_skills' in json:
            this['base_skills'] = json['base_skills']
    #if(json.base_skills==null)
    #return
    #for(intindex=0index<this.derive_skills.Length++index)
    if 'derive_skills' in json:
        this['derive_skills'] = json['derive_skills']
return this
