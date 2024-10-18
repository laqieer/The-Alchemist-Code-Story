def RaidBossData(json):
    this={}#RaidBossDatajson)
    if 'uid' in json:
        this['mOwnerUID'] = json['uid']
    if 'name' in json:
        this['mOwnerName'] = json['name']
    if 'area_id' in json:
        this['mAreaId'] = json['area_id']
    #if(json.boss_info==null)
    #returnfalse
    #this.mRaidBossInfo=newRaidBossInfo()
    #if(!this.mRaidBossInfo.Deserialize(json.boss_info))
    #returnfalse
    if 'sos_status' in json:
        this['mSOSStatus'] = ENUM['RaidSOSStatus'][json['sos_status']]
    #this.mSOSMember=newList<RaidSOSMember>()
    #if(json.sos_member!=null)
        #for(intindex=0index<json.sos_member.Length++index)
            #RaidSOSMemberraidSosMember=newRaidSOSMember()
            #if(!raidSosMember.Deserialize(json.sos_member))
            #returnfalse
            #this.mSOSMember.Add(raidSosMember)
        #this.mSOSMember.Sort((Comparison<RaidSOSMember>)((a,b)=>(int)(b.LastBattleTime-a.LastBattleTime)))
    #returntrue
return this
