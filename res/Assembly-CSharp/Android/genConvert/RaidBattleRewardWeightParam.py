def RaidBattleRewardWeightParam(json):
    this={}#RaidBattleRewardWeightParamjson)
    #if(json==null)
    #returnfalse
    if 'weight' in json:
        this['mWeight'] = json['weight']
    if 'reward_id' in json:
        this['mRewardId'] = json['reward_id']
    #returntrue
return this
