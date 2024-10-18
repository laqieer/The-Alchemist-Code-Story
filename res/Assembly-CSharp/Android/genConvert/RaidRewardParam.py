def RaidRewardParam(json):
    this={}#RaidRewardParamjson)
    #if(json==null)
    #returnfalse
    if 'id' in json:
        this['mId'] = json['id']
    #this.mRewards=newList<RaidReward>()
    #if(json.rewards!=null)
        #for(intindex=0index<json.rewards.Length++index)
            #RaidRewardraidReward=newRaidReward()
            #if(raidReward.Deserialize(json.rewards))
            #this.mRewards.Add(raidReward)
    #returntrue
return this
