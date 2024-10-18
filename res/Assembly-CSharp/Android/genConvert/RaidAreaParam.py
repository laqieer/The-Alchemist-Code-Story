def RaidAreaParam(json):
    this={}#RaidAreaParamjson)
    #if(json==null)
    #returnfalse
    if 'id' in json:
        this['mId'] = json['id']
    if 'order' in json:
        this['mOrder'] = json['order']
    if 'period_id' in json:
        this['mPeriodId'] = json['period_id']
    if 'boss_count' in json:
        this['mBossCount'] = json['boss_count']
    if 'area_boss_id' in json:
        this['mAreaBossId'] = json['area_boss_id']
    if 'clear_reward_id' in json:
        this['mClearRewardId'] = json['clear_reward_id']
    #returntrue
return this
