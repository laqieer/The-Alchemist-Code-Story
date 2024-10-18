def RaidRescueMember(json):
    this={}#RaidRescueMemberjson)
    if 'uid' in json:
        this['mUID'] = json['uid']
    if 'name' in json:
        this['mName'] = json['name']
    if 'lv' in json:
        this['mLv'] = json['lv']
    if 'member_type' in json:
        this['mMemberType'] = ENUM['RaidRescueMemberType'][json['member_type']]
    if 'selected_award' in json:
        this['mSelectedAward'] = json['selected_award']
    if 'lastlogin' in json:
        this['mLastLogin'] = TimeManager.FromUnixTimejson['lastlogin'])
    if 'area_id' in json:
        this['mAreaId'] = json['area_id']
    if 'boss_id' in json:
        this['mBossId'] = json['boss_id']
    if 'round' in json:
        this['mRound'] = json['round']
    if 'current_hp' in json:
        this['mCurrentHp'] = json['current_hp']
    if 'start_time' in json:
        this['mStartTime'] = json['start_time']
    #if(json.unit!=null)
        #this.mUnit=newUnitData()
        #this.mUnit.Deserialize(json.unit)
    #returntrue
return this
