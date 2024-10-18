def GuildEntryCondition(json):
    this={}#GuildEntryConditionjson)
    if 'lower_level' in json:
        this['mLowerLevel'] = json['lower_level']
    if 'is_auto_approval' in json:
        this['mIsAutoApproval'] = json['is_auto_approval']!=0
    if 'recruit_comment' in json:
        this['mComment'] = !string.IsNullOrEmpty?json['recruit_comment']:string.Empty
    #returntrue
return this
