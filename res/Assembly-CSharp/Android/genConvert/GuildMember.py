def GuildMember(json):
    this={}#GuildMemberjson)
    if 'gid' in json:
        this['mGid'] = json['gid']
    if 'uid' in json:
        this['mUid'] = json['uid']
    if 'role_id' in json:
        this['mRoleId'] = json['role_id']
    if 'name' in json:
        this['mName'] = json['name']
    if 'lv' in json:
        this['mLevel'] = json['lv']
    if 'award_id' in json:
        this['mAward'] = json['award_id']
    if 'applied_at' in json:
        this['mAppliedAt'] = json['applied_at']
    if 'joined_at' in json:
        this['mJoinedAt'] = json['joined_at']
    if 'leave_at' in json:
        this['mLeaveAt'] = json['leave_at']
    if 'lastlogin' in json:
        this['mLastLogin'] = json['lastlogin']
    #this.mUnit=newUnitData()
    #this.mUnit.Deserialize(json.units)
    #returntrue
return this
