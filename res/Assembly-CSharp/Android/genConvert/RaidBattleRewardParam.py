def RaidBattleRewardParam(json):
    this={}#RaidBattleRewardParamjson)
    #if(json==null)
    #returnfalse
    if 'id' in json:
        this['mId'] = json['id']
    #this.mRewards=newList<RaidBattleRewardWeightParam>()
    #if(json.rewards!=null)
        #for(intindex=0index<json.rewards.Length++index)
            #RaidBattleRewardWeightParamrewardWeightParam=newRaidBattleRewardWeightParam()
            #if(rewardWeightParam.Deserialize(json.rewards))
            #this.mRewards.Add(rewardWeightParam)
    #returntrue
return this
