def SkillMotionDataParam(json):
    this={}#SkillMotionDataParamjson)
    #if(json==null)
    #return
    #if(json.unit_ids!=null)
        #for(intindex=0index<json.unit_ids.Length++index)
        #this.mUnitList.Add(json.unit_ids)
    #if(json.job_ids!=null)
        #for(intindex=0index<json.job_ids.Length++index)
        #this.mJobList.Add(json.job_ids)
    if 'motnm' in json:
        this['mMotionId'] = json['motnm']
    #this.mFlags=(SkillMotionDataParam.Flags)0
    #if(json.isbtl==0)
    #return
    #this.mFlags|=SkillMotionDataParam.Flags.IsBattleScene
return this
