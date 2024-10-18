def SkillMotionParam(json):
    this={}#SkillMotionParamjson)
    #if(json==null)
    #return
    if 'iname' in json:
        this['mIname'] = json['iname']
    #if(json.datas==null)
    #return
    if 'datas' in json:
        this['mDataList'] = newList<SkillMotionDataParam>
    #for(intindex=0index<json.datas.Length++index)
        #SkillMotionDataParamskillMotionDataParam=newSkillMotionDataParam()
        #skillMotionDataParam.Deserialize(json.datas)
        #this.mDataList.Add(skillMotionDataParam)
return this
