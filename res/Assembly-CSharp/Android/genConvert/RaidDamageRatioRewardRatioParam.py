def RaidDamageRatioRewardRatioParam(json):
    this={}#RaidDamageRatioRewardRatioParamjson)
    #if(json==null)
    #returnfalse
    if 'damage_ratio' in json:
        this['mDamageRatio'] = json['damage_ratio']
    if 'reward_id' in json:
        this['mRewardId'] = json['reward_id']
    #returntrue
return this
