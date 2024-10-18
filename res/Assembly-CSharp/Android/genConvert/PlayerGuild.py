def PlayerGuild(json):
    this={}#PlayerGuildjson)
    if 'gid' in json:
        this['mGid'] = json['gid']
    if 'guild_name' in json:
        this['mName'] = json['guild_name']
    if 'role_id' in json:
        this['mRoleId'] = json['role_id']
    if 'invest_point' in json:
        this['mInvestPoint'] = json['invest_point']
    if 'applied_at' in json:
        this['mAppliedAt'] = json['applied_at']
    if 'joined_at' in json:
        this['mJoinedAt'] = json['joined_at']
    if 'leaved_at' in json:
        this['mLeavedAt'] = json['leaved_at']
    #returntrue
return this
