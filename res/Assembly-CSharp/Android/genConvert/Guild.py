def Guild(json):
    this={}#Guildjson)
    if 'id' in json:
        this['mUniqueID'] = json['id']
    if 'created_uid' in json:
        this['mCreatedUid'] = json['created_uid']
    if 'name' in json:
        this['mName'] = json['name']
    if 'award_id' in json:
        this['mEmblem'] = json['award_id']
    if 'board' in json:
        this['mBoard'] = !string.IsNullOrEmpty?json['board']:string.Empty
    if 'count' in json:
        this['mMemberCount'] = json['count']
    if 'max_count' in json:
        this['mMemberCountMax'] = json['max_count']
    if 'submaster_count' in json:
        this['mSubMasterCountMax'] = json['submaster_count']
    if 'created_at' in json:
        this['mCreatedAt'] = json['created_at']
    #this.mEntryConditions=newGuildEntryConditions()
    #this.mEntryConditions.Deserialize(json.guild_subscription_condition)
    #this.mMembers=(GuildMemberData)null
    #if(json.guild_member!=null)
        if 'guild_member' in json:
            this['mMembers'] = newGuildMemberData[json['guild_member'].Length]
        #for(intindex=0index<json.guild_member.Length++index)
            #this.mMembers=newGuildMemberData()
            #this.mMembers.Deserialize(json.guild_member)
    #this.mHaveAwards=newstring[0]
    #if(json.have_awards!=null)
        if 'have_awards' in json:
            this['mHaveAwards'] = newstring[json['have_awards'].Length]
        #json.have_awards.CopyTo((Array)this.mHaveAwards,0)
    #this.mFacilities=(GuildFacilityData)null
    #if(json.facilities!=null)
        if 'facilities' in json:
            this['mFacilities'] = newGuildFacilityData[json['facilities'].Length]
        #for(intindex=0index<json.facilities.Length++index)
            #this.mFacilities=newGuildFacilityData()
            #this.mFacilities.Deserialize(json.facilities)
    #returntrue
return this
