def RaidAreaClearRewardDataParam(json):
    this={}#RaidAreaClearRewardDataParamjson)
    #if(json==null)
    #returnfalse
    if 'round' in json:
        this['mRound'] = json['round']
    if 'reward_id' in json:
        this['mRewardId'] = json['reward_id']
    #returntrue
return this
