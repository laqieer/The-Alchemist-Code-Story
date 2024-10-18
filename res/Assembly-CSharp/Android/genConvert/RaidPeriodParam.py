def RaidPeriodParam(json):
    this={}#RaidPeriodParamjson)
    #if(json==null)
    #returnfalse
    if 'id' in json:
        this['mId'] = json['id']
    if 'max_bp' in json:
        this['mMaxBp'] = json['max_bp']
    if 'add_bp_time' in json:
        this['mAddBpTime'] = json['add_bp_time']
    if 'bp_by_coin' in json:
        this['mBpByCoin'] = json['bp_by_coin']
    if 'rescue_member_max' in json:
        this['mRescueMemberMax'] = json['rescue_member_max']
    if 'rescure_send_interval' in json:
        this['mRescureSendInterval'] = json['rescure_send_interval']
    if 'complete_reward_id' in json:
        this['mCompleteRewardId'] = json['complete_reward_id']
    if 'round_buff_max' in json:
        this['mRoundBuffMax'] = json['round_buff_max']
    #this.mBeginAt=DateTime.MinValue
    #if(!string.IsNullOrEmpty(json.begin_at))
    #DateTime.TryParse(json.begin_at,outthis.mBeginAt)
    #this.mEndAt=DateTime.MaxValue
    #if(!string.IsNullOrEmpty(json.end_at))
    #DateTime.TryParse(json.end_at,outthis.mEndAt)
    #returntrue
return this
