def RaidSOSMember(json):
    this={}#RaidSOSMemberjson)
    if 'fuid' in json:
        this['mFUID'] = json['fuid']
    if 'name' in json:
        this['mName'] = json['name']
    if 'lv' in json:
        this['mLv'] = json['lv']
    if 'member_type' in json:
        this['mMemberType'] = ENUM['RaidRescueMemberType'][json['member_type']]
    if 'last_battle_time' in json:
        this['mLastBattleTime'] = json['last_battle_time']
    #if(json.unit!=null)
        #this.mUnit=newUnitData()
        #this.mUnit.Deserialize(json.unit)
    #returntrue
return this
