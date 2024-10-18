def RaidBossInfo(json):
    this={}#RaidBossInfojson)
    if 'no' in json:
        this['mNo'] = json['no']
    if 'boss_id' in json:
        this['mBossId'] = json['boss_id']
    if 'round' in json:
        this['mRound'] = json['round']
    if 'current_hp' in json:
        this['mHP'] = json['current_hp']
    if 'start_time' in json:
        this['mStartTime'] = json['start_time']
    if 'is_reward' in json:
        this['mIsReward'] = json['is_reward']==1
    if 'is_timeover' in json:
        this['mIsTimeOver'] = json['is_timeover']==1
    #this.mRaidBossParam=MonoSingleton<GameManager>.Instance.MasterParam.GetRaidBoss(this.mBossId)
    #if(this.mRaidBossParam==null)
    #returnfalse
    #this.mMaxHP=RaidBossParam.CalcMaxHP(this.mRaidBossParam,this.mRound)
    #returntrue
return this
