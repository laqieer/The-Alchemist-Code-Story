def RaidBossParam(json):
    this={}#RaidBossParamjson)
    #if(json==null)
    #returnfalse
    if 'id' in json:
        this['mId'] = json['id']
    if 'stamp_index' in json:
        this['mStampIndex'] = json['stamp_index']
    if 'period_id' in json:
        this['mPeriodId'] = json['period_id']
    if 'weight' in json:
        this['mWeight'] = json['weight']
    if 'name' in json:
        this['mName'] = json['name']
    if 'hp' in json:
        this['mHP'] = json['hp']
    if 'unit_iname' in json:
        this['mUnitIName'] = json['unit_iname']
    if 'quest_iname' in json:
        this['mQuestIName'] = json['quest_iname']
    if 'time_limit' in json:
        this['mTimeLimit'] = json['time_limit']
    if 'battle_reward_id' in json:
        this['mBattleRewardId'] = json['battle_reward_id']
    if 'beat_reward_id' in json:
        this['mBeatRewardId'] = json['beat_reward_id']
    if 'damage_ratio_reward_id' in json:
        this['mDamageRatioRewardId'] = json['damage_ratio_reward_id']
    if 'buff_id' in json:
        this['mBuffId'] = json['buff_id']
    if 'is_boss' in json:
        this['mIsBoss'] = json['is_boss']==1
    #stringstrArray=this.mTimeLimit.Split(':')
    #intnumArray=newint[3]
    #if(strArray.Length==2)
    #numArray[2]=0
    #for(intindex=0index<strArray.Length++index)
    #numArray=int.Parse(strArray)
    #this.mTimeLimitSpan=newTimeSpan(numArray[0],numArray[1],numArray[2])
    #returntrue
return this
