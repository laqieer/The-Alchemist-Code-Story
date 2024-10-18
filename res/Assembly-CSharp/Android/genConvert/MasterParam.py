def MasterParam(json):
    this={}#MasterParamjson)
    #if(this.Loaded)
    #returntrue
    #DebugUtility.Verify((object)json,typeof(JSON_MasterParam))
    #this.mLocalNotificationParam=(LocalNotificationParam)null
    #this.mFixParam.Deserialize(json.Fix[0])
    #if(json.Unit!=null)
        #if(this.mUnitParam==null)
        if 'Unit' in json:
            this['mUnitParam'] = newList<UnitParam>
        #if(this.mUnitDictionary==null)
        if 'Unit' in json:
            this['mUnitDictionary'] = newDictionary<string,UnitParam>
        #for(intindex=0index<json.Unit.Length++index)
            #JSON_UnitParamdata=json.Unit
            #UnitParamunitParam=this.mUnitParam.Find((Predicate<UnitParam>)(p=>p.iname==data.iname))
            #if(unitParam==null)
                #unitParam=newUnitParam()
                #this.mUnitParam.Add(unitParam)
            #unitParam.Deserialize(data)
            #if(this.mUnitDictionary.ContainsKey(data.iname))
            #thrownewException("重複エラー：Unit["+data.iname+"]")
            #this.mUnitDictionary.Add(data.iname,unitParam)
    #if(json.UnitJobOverwrite!=null)
        #if(this.mUnitJobOverwriteParam==null)
        #this.mUnitJobOverwriteParam=newList<UnitJobOverwriteParam>()
        #if(this.mUnitJobOverwriteDictionary==null)
        #this.mUnitJobOverwriteDictionary=newDictionary<string,Dictionary<string,UnitJobOverwriteParam>>()
        #foreach(JSON_UnitJobOverwriteParamjson1injson.UnitJobOverwrite)
            #UnitJobOverwriteParamjobOverwriteParam=newUnitJobOverwriteParam()
            #this.mUnitJobOverwriteParam.Add(jobOverwriteParam)
            #jobOverwriteParam.Deserialize(json1)
            #Dictionary<string,UnitJobOverwriteParam>dictionary
            #this.mUnitJobOverwriteDictionary.TryGetValue(json1.unit_iname,outdictionary)
            #if(dictionary==null)
                #dictionary=newDictionary<string,UnitJobOverwriteParam>()
                #this.mUnitJobOverwriteDictionary.Add(json1.unit_iname,dictionary)
            #if(!dictionary.ContainsKey(json1.job_iname))
            #dictionary.Add(json1.job_iname,jobOverwriteParam)
    #if(json.Skill!=null)
        #if(this.mSkillParam==null)
        if 'Skill' in json:
            this['mSkillParam'] = newList<SkillParam>
        #if(this.mSkillDictionary==null)
        if 'Skill' in json:
            this['mSkillDictionary'] = newDictionary<string,SkillParam>
        #for(intindex=0index<json.Skill.Length++index)
            #JSON_SkillParamdata=json.Skill
            #SkillParamskillParam=this.mSkillParam.Find((Predicate<SkillParam>)(p=>p.iname==data.iname))
            #if(skillParam==null)
                #skillParam=newSkillParam()
                #this.mSkillParam.Add(skillParam)
            #skillParam.Deserialize(data)
            #if(this.mSkillDictionary.ContainsKey(data.iname))
            #thrownewException("重複エラー：Skill["+data.iname+"]")
            #this.mSkillDictionary.Add(data.iname,skillParam)
        #SkillParam.UpdateReplaceSkill(this.mSkillParam)
    #if(json.Buff!=null)
        #if(this.mBuffEffectParam==null)
        if 'Buff' in json:
            this['mBuffEffectParam'] = newList<BuffEffectParam>
        #for(intindex=0index<json.Buff.Length++index)
            #JSON_BuffEffectParamdata=json.Buff
            #BuffEffectParambuffEffectParam=this.mBuffEffectParam.Find((Predicate<BuffEffectParam>)(p=>p.iname==data.iname))
            #if(buffEffectParam==null)
                #buffEffectParam=newBuffEffectParam()
                #this.mBuffEffectParam.Add(buffEffectParam)
            #buffEffectParam.Deserialize(data)
    #if(json.Cond!=null)
        #if(this.mCondEffectParam==null)
        if 'Cond' in json:
            this['mCondEffectParam'] = newList<CondEffectParam>
        #for(intindex=0index<json.Cond.Length++index)
            #JSON_CondEffectParamdata=json.Cond
            #CondEffectParamcondEffectParam=this.mCondEffectParam.Find((Predicate<CondEffectParam>)(p=>p.iname==data.iname))
            #if(condEffectParam==null)
                #condEffectParam=newCondEffectParam()
                #this.mCondEffectParam.Add(condEffectParam)
            #condEffectParam.Deserialize(data)
    #if(json.Ability!=null)
        #if(this.mAbilityParam==null)
        if 'Ability' in json:
            this['mAbilityParam'] = newList<AbilityParam>
        #if(this.mAbilityDictionary==null)
        if 'Ability' in json:
            this['mAbilityDictionary'] = newDictionary<string,AbilityParam>
        #for(intindex=0index<json.Ability.Length++index)
            #JSON_AbilityParamdata=json.Ability
            #AbilityParamabilityParam=this.mAbilityParam.Find((Predicate<AbilityParam>)(p=>p.iname==data.iname))
            #if(abilityParam==null)
                #abilityParam=newAbilityParam()
                #this.mAbilityParam.Add(abilityParam)
            #abilityParam.Deserialize(data)
            #if(this.mAbilityDictionary.ContainsKey(data.iname))
            #thrownewException("重複エラー：Ability["+data.iname+"]")
            #this.mAbilityDictionary.Add(data.iname,abilityParam)
    #if(json.Item!=null)
        #if(this.mItemParam==null)
        if 'Item' in json:
            this['mItemParam'] = newList<ItemParam>
        #if(this.mItemDictionary==null)
        if 'Item' in json:
            this['mItemDictionary'] = newDictionary<string,ItemParam>
        #for(intindex=0index<json.Item.Length++index)
            #JSON_ItemParamdata=json.Item
            #ItemParamitemParam=this.mItemParam.Find((Predicate<ItemParam>)(p=>p.iname==data.iname))
            #if(itemParam==null)
                #itemParam=newItemParam()
                #this.mItemParam.Add(itemParam)
            #itemParam.Deserialize(data)
            #itemParam.no=index+1
            #if(this.mItemDictionary.ContainsKey(data.iname))
            #thrownewException("重複エラー：Item["+data.iname+"]")
            #this.mItemDictionary.Add(data.iname,itemParam)
        #this.AddUnitToItem()
    #if(json.Artifact!=null)
        #if(this.mArtifactParam==null)
        if 'Artifact' in json:
            this['mArtifactParam'] = newList<ArtifactParam>
        #if(this.mArtifactDictionary==null)
        if 'Artifact' in json:
            this['mArtifactDictionary'] = newDictionary<string,ArtifactParam>
        #for(intindex=0index<json.Artifact.Length++index)
            #JSON_ArtifactParamdata=json.Artifact
            #if(data.iname!=null)
                #ArtifactParamartifactParam=this.mArtifactParam.Find((Predicate<ArtifactParam>)(p=>p.iname==data.iname))
                #if(artifactParam==null)
                    #artifactParam=newArtifactParam()
                    #this.mArtifactParam.Add(artifactParam)
                #artifactParam.Deserialize(data)
                #if(this.mArtifactDictionary.ContainsKey(data.iname))
                #thrownewException("重複エラー：Artifact["+data.iname+"]")
                #this.mArtifactDictionary.Add(data.iname,artifactParam)
    #if(json.Weapon!=null)
        #if(this.mWeaponParam==null)
        if 'Weapon' in json:
            this['mWeaponParam'] = newList<WeaponParam>
        #for(intindex=0index<json.Weapon.Length++index)
            #JSON_WeaponParamdata=json.Weapon
            #WeaponParamweaponParam=this.mWeaponParam.Find((Predicate<WeaponParam>)(p=>p.iname==data.iname))
            #if(weaponParam==null)
                #weaponParam=newWeaponParam()
                #this.mWeaponParam.Add(weaponParam)
            #weaponParam.Deserialize(data)
    #if(json.Recipe!=null)
        #if(this.mRecipeParam==null)
        if 'Recipe' in json:
            this['mRecipeParam'] = newList<RecipeParam>
        #for(intindex=0index<json.Recipe.Length++index)
            #JSON_RecipeParamjson1=json.Recipe
            #RecipeParamrecipeParam=newRecipeParam()
            #this.mRecipeParam.Add(recipeParam)
            #recipeParam.Deserialize(json1)
    #if(json.Job!=null)
        #if(this.mJobParam==null)
        if 'Job' in json:
            this['mJobParam'] = newList<JobParam>
        #for(intindex=0index<json.Job.Length++index)
            #JSON_JobParamdata=json.Job
            #JobParamjobParam=this.mJobParam.Find((Predicate<JobParam>)(p=>p.iname==data.iname))
            #if(jobParam==null)
                #jobParam=newJobParam()
                #this.mJobParam.Add(jobParam)
                #this.mJobParamDict[data.iname]=jobParam
            #jobParam.Deserialize(data,this)
    #if(json.JobSet!=null)
        #if(this.mJobSetParam==null)
        if 'JobSet' in json:
            this['mJobSetParam'] = newList<JobSetParam>
        #if(this.mJobsetDictionary==null)
        if 'Unit' in json:
            this['mJobsetDictionary'] = newDictionary<string,List<JobSetParam>>
        #for(intindex=0index<json.JobSet.Length++index)
            #JSON_JobSetParamdata=json.JobSet
            #JobSetParamjobSetParam=this.mJobSetParam.Find((Predicate<JobSetParam>)(p=>p.iname==data.iname))
            #if(jobSetParam==null)
                #jobSetParam=newJobSetParam()
                #this.mJobSetParam.Add(jobSetParam)
            #jobSetParam.Deserialize(data)
            #if(!string.IsNullOrEmpty(jobSetParam.target_unit))
                #List<JobSetParam>jobSetParamList
                #if(this.mJobsetDictionary.ContainsKey(jobSetParam.target_unit))
                    #jobSetParamList=this.mJobsetDictionary[jobSetParam.target_unit]
                #else
                    #jobSetParamList=newList<JobSetParam>(3)
                    #this.mJobsetDictionary.Add(jobSetParam.target_unit,jobSetParamList)
                #jobSetParamList.Add(jobSetParam)
    #if(json.Grow!=null)
        #if(this.mGrowParam==null)
        if 'Grow' in json:
            this['mGrowParam'] = newList<GrowParam>
        #for(intindex=0index<json.Grow.Length++index)
            #JSON_GrowParamdata=json.Grow
            #GrowParamgrowParam=this.mGrowParam.Find((Predicate<GrowParam>)(p=>p.type==data.type))
            #if(growParam==null)
                #growParam=newGrowParam()
                #this.mGrowParam.Add(growParam)
            #growParam.Deserialize(data)
    #if(json.AI!=null)
        #if(this.mAIParam==null)
        if 'AI' in json:
            this['mAIParam'] = newList<AIParam>
        #for(intindex=0index<json.AI.Length++index)
            #JSON_AIParamdata=json.AI
            #AIParamaiParam=this.mAIParam.Find((Predicate<AIParam>)(p=>p.iname==data.iname))
            #if(aiParam==null)
                #aiParam=newAIParam()
                #this.mAIParam.Add(aiParam)
            #aiParam.Deserialize(data)
    #if(json.Geo!=null)
        #if(this.mGeoParam==null)
        if 'Geo' in json:
            this['mGeoParam'] = newList<GeoParam>
        #for(intindex=0index<json.Geo.Length++index)
            #JSON_GeoParamdata=json.Geo
            #GeoParamgeoParam=this.mGeoParam.Find((Predicate<GeoParam>)(p=>p.iname==data.iname))
            #if(geoParam==null)
                #geoParam=newGeoParam()
                #this.mGeoParam.Add(geoParam)
            #geoParam.Deserialize(data)
    #if(json.Rarity!=null)
        #if(this.mRarityParam==null)
        if 'Rarity' in json:
            this['mRarityParam'] = newList<RarityParam>
        #for(intindex=0index<json.Rarity.Length++index)
            #RarityParamrarityParam
            #if(this.mRarityParam.Count>index)
                #rarityParam=this.mRarityParam
            #else
                #rarityParam=newRarityParam()
                #this.mRarityParam.Add(rarityParam)
            #rarityParam.Deserialize(json.Rarity)
    #if(json.Shop!=null)
        #if(this.mShopParam==null)
        if 'Shop' in json:
            this['mShopParam'] = newList<ShopParam>
        #for(intindex=0index<json.Shop.Length++index)
            #ShopParamshopParam
            #if(this.mShopParam.Count>index)
                #shopParam=this.mShopParam
            #else
                #shopParam=newShopParam()
                #this.mShopParam.Add(shopParam)
            #shopParam.Deserialize(json.Shop)
    #if(json.Player!=null)
        if 'Player' in json:
            this['mPlayerParamTbl'] = newPlayerParam[json['Player'].Length]
        #for(intindex=0index<json.Player.Length++index)
            #JSON_PlayerParamjson1=json.Player
            #this.mPlayerParamTbl=newPlayerParam()
            #this.mPlayerParamTbl.Deserialize(json1)
    #if(json.PlayerLvTbl!=null)
        #for(intindex=0index<json.PlayerLvTbl.Length++index)
        if 'PlayerLvTbl' in json:
            this['mPlayerExpTbl'] = json['PlayerLvTbl']
    #if(json.UnitLvTbl!=null)
        #for(intindex=0index<json.UnitLvTbl.Length++index)
        if 'UnitLvTbl' in json:
            this['mUnitExpTbl'] = json['UnitLvTbl']
    #if(json.ArtifactLvTbl!=null)
        #for(intindex=0index<json.ArtifactLvTbl.Length++index)
        if 'ArtifactLvTbl' in json:
            this['mArtifactExpTbl'] = json['ArtifactLvTbl']
    #if(json.AbilityRank!=null)
        #for(intindex=0index<json.AbilityRank.Length++index)
        if 'AbilityRank' in json:
            this['mAbilityExpTbl'] = json['AbilityRank']
    #if(json.AwakePieceTbl!=null)
        #for(intindex=0index<json.AwakePieceTbl.Length++index)
        if 'AwakePieceTbl' in json:
            this['mAwakePieceTbl'] = json['AwakePieceTbl']
    #this.mLocalNotificationParam=newLocalNotificationParam()
    #if(json.LocalNotification!=null)
        this['']
        this['mLocalNotificationParam']
        if 'LocalNotification' in json:
            this['mLocalNotificationParam']['msg_stamina'] = json['LocalNotification'][0].msg_stamina
        this['mLocalNotificationParam']
        if 'LocalNotification' in json:
            this['mLocalNotificationParam']['iOSAct_stamina'] = json['LocalNotification'][0].iOSAct_stamina
        this['mLocalNotificationParam']
        if 'LocalNotification' in json:
            this['mLocalNotificationParam']['limitSec_stamina'] = json['LocalNotification'][0].limitSec_stamina
    #Dictionary<int,TrophyCategoryParam>dictionary1=newDictionary<int,TrophyCategoryParam>()
    #if(json.TrophyCategory!=null)
        #List<TrophyCategoryParam>trophyCategoryParamList=newList<TrophyCategoryParam>(json.TrophyCategory.Length)
        #for(intindex=0index<json.TrophyCategory.Length++index)
            #TrophyCategoryParamtrophyCategoryParam=newTrophyCategoryParam()
            #if(trophyCategoryParam.Deserialize(json.TrophyCategory))
                #trophyCategoryParamList.Add(trophyCategoryParam)
                #if(!dictionary1.ContainsKey(trophyCategoryParam.hash_code))
                #dictionary1.Add(trophyCategoryParam.hash_code,trophyCategoryParam)
        #this.mTrophyCategory=trophyCategoryParamList.ToArray()
    #if(json.Trophy!=null)
        #List<TrophyParam>trophyParamList=newList<TrophyParam>(json.Trophy.Length)
        #for(intindex=0index<json.Trophy.Length++index)
            #TrophyParamtrophyParam=newTrophyParam()
            #if(trophyParam.Deserialize(json.Trophy))
                #if(dictionary1.ContainsKey(trophyParam.category_hash_code))
                #trophyParam.CategoryParam=dictionary1[trophyParam.category_hash_code]
                #if(trophyParam.IsPlanningToUse())
                #trophyParamList.Add(trophyParam)
        #this.mTrophy=trophyParamList.ToArray()
        #this.mTrophyInameDict=newDictionary<string,TrophyParam>()
        #foreach(TrophyParamtrophyParaminthis.mTrophy)
        #this.mTrophyInameDict.Add(trophyParam.iname,trophyParam)
    #Dictionary<string,ChallengeCategoryParam>dictionary2=newDictionary<string,ChallengeCategoryParam>()
    #if(json.ChallengeCategory!=null)
        #List<ChallengeCategoryParam>challengeCategoryParamList=newList<ChallengeCategoryParam>(json.ChallengeCategory.Length)
        #for(intindex=0index<json.ChallengeCategory.Length++index)
            #ChallengeCategoryParamchallengeCategoryParam=newChallengeCategoryParam()
            #if(challengeCategoryParam.Deserialize(json.ChallengeCategory))
                #dictionary2[challengeCategoryParam.iname]=challengeCategoryParam
                #challengeCategoryParamList.Add(challengeCategoryParam)
        #this.mChallengeCategory=challengeCategoryParamList.ToArray()
    #if(json.Challenge!=null)
        #List<TrophyParam>trophyParamList=newList<TrophyParam>(json.Challenge.Length)
        #for(intindex=0index<json.Challenge.Length++index)
            #TrophyParamtrophyParam=newTrophyParam()
            #if(trophyParam.Deserialize(json.Challenge))
                #if(dictionary2.ContainsKey(trophyParam.Category))
                #trophyParam.ChallengeCategoryParam=dictionary2[trophyParam.Category]
                #trophyParam.Challenge=1
                #trophyParamList.Add(trophyParam)
        #this.mChallenge=trophyParamList.ToArray()
        #intlength=this.mTrophy.Length
        #Array.Resize<TrophyParam>(refthis.mTrophy,length+this.mChallenge.Length)
        #Array.Copy((Array)this.mChallenge,0,(Array)this.mTrophy,length,this.mChallenge.Length)
        #foreach(TrophyParamtrophyParaminthis.mChallenge)
        #this.mTrophyInameDict.Add(trophyParam.iname,trophyParam)
    #this.CreateTrophyDict()
    #if(json.Unlock!=null)
        #List<UnlockParam>unlockParamList=newList<UnlockParam>(json.Unlock.Length)
        #for(intindex=0index<json.Unlock.Length++index)
            #UnlockParamunlockParam=newUnlockParam()
            #if(unlockParam.Deserialize(json.Unlock))
            #unlockParamList.Add(unlockParam)
        #this.mUnlock=unlockParamList.ToArray()
    #if(json.Vip!=null)
        #List<VipParam>vipParamList=newList<VipParam>(json.Vip.Length)
        #for(intindex=0index<json.Vip.Length++index)
            #VipParamvipParam=newVipParam()
            #if(vipParam.Deserialize(json.Vip))
            #vipParamList.Add(vipParam)
        #this.mVip=vipParamList.ToArray()
    #if(json.Premium!=null)
        #List<PremiumParam>premiumParamList=newList<PremiumParam>(json.Premium.Length)
        #for(intindex=0index<json.Premium.Length++index)
            #PremiumParampremiumParam=newPremiumParam()
            #if(premiumParam.Deserialize(json.Premium))
            #premiumParamList.Add(premiumParam)
        #this.mPremium=premiumParamList.ToArray()
    #if(json.Mov!=null)
        if 'Mov' in json:
            this['mStreamingMovies'] = newJSON_StreamingMovie[json['Mov'].Length]
        #for(intindex=0index<json.Mov.Length++index)
            #this.mStreamingMovies=newJSON_StreamingMovie()
            this['']
            this['mStreamingMovies']
            if 'Mov' in json:
                this['mStreamingMovies']['alias'] = json['Mov'].alias
            this['mStreamingMovies']
            if 'Mov' in json:
                this['mStreamingMovies']['path'] = json['Mov'].path
    #if(json.Banner!=null)
        #List<BannerParam>bannerParamList=newList<BannerParam>(json.Banner.Length)
        #for(intindex=0index<json.Banner.Length++index)
            #BannerParambannerParam=newBannerParam()
            #if(bannerParam.Deserialize(json.Banner))
            #bannerParamList.Add(bannerParam)
        #this.mBanner=bannerParamList.ToArray()
    #if(json.QuestClearUnlockUnitData!=null)
        #List<QuestClearUnlockUnitDataParam>unlockUnitDataParamList=newList<QuestClearUnlockUnitDataParam>(json.QuestClearUnlockUnitData.Length)
        #for(intindex=0index<json.QuestClearUnlockUnitData.Length++index)
            #QuestClearUnlockUnitDataParamunlockUnitDataParam=newQuestClearUnlockUnitDataParam()
            #unlockUnitDataParam.Deserialize(json.QuestClearUnlockUnitData)
            #unlockUnitDataParamList.Add(unlockUnitDataParam)
        #this.mUnlockUnitDataParam=unlockUnitDataParamList
    #if(json.Award!=null)
        #if(this.mAwardParam==null)
        if 'Award' in json:
            this['mAwardParam'] = newList<AwardParam>
        #if(this.mAwardDictionary==null)
        if 'Award' in json:
            this['mAwardDictionary'] = newDictionary<string,AwardParam>
        #for(intindex=0index<json.Award.Length++index)
            #JSON_AwardParamdata=json.Award
            #if(data.iname!=null)
                #AwardParamawardParam=this.mAwardParam.Find((Predicate<AwardParam>)(p=>p.iname==data.iname))
                #if(awardParam==null)
                    #awardParam=newAwardParam()
                    #this.mAwardParam.Add(awardParam)
                #awardParam.Deserialize(data)
                #if(this.mAwardDictionary.ContainsKey(awardParam.iname))
                #thrownewException("Overlap:Award["+awardParam.iname+"]")
                #this.mAwardDictionary.Add(awardParam.iname,awardParam)
    #if(json.LoginInfo!=null)
        #List<LoginInfoParam>loginInfoParamList=newList<LoginInfoParam>(json.LoginInfo.Length)
        #for(intindex=0index<json.LoginInfo.Length++index)
            #LoginInfoParamloginInfoParam=newLoginInfoParam()
            #if(loginInfoParam.Deserialize(json.LoginInfo))
            #loginInfoParamList.Add(loginInfoParam)
        #this.mLoginInfoParam=loginInfoParamList.ToArray()
    #if(json.CollaboSkill!=null)
        #List<CollaboSkillParam>collaboSkillParamList=newList<CollaboSkillParam>(json.CollaboSkill.Length)
        #for(intindex=0index<json.CollaboSkill.Length++index)
            #CollaboSkillParamcollaboSkillParam=newCollaboSkillParam()
            #collaboSkillParam.Deserialize(json.CollaboSkill)
            #collaboSkillParamList.Add(collaboSkillParam)
        #this.mCollaboSkillParam=collaboSkillParamList
        #CollaboSkillParam.UpdateCollaboSkill(this.mCollaboSkillParam)
    #if(json.Trick!=null)
        #List<TrickParam>trickParamList=newList<TrickParam>(json.Trick.Length)
        #for(intindex=0index<json.Trick.Length++index)
            #TrickParamtrickParam=newTrickParam()
            #trickParam.Deserialize(json.Trick)
            #trickParamList.Add(trickParam)
        #this.mTrickParam=trickParamList
    #if(json.BreakObj!=null)
        #List<BreakObjParam>breakObjParamList=newList<BreakObjParam>(json.BreakObj.Length)
        #for(intindex=0index<json.BreakObj.Length++index)
            #BreakObjParambreakObjParam=newBreakObjParam()
            #breakObjParam.Deserialize(json.BreakObj)
            #breakObjParamList.Add(breakObjParam)
        #this.mBreakObjParam=breakObjParamList
    #if(json.VersusMatchKey!=null)
        if 'VersusMatchKey' in json:
            this['mVersusMatching'] = newList<VersusMatchingParam>
        #for(intindex=0index<json.VersusMatchKey.Length++index)
            #VersusMatchingParamversusMatchingParam=newVersusMatchingParam()
            #versusMatchingParam.Deserialize(json.VersusMatchKey)
            #this.mVersusMatching.Add(versusMatchingParam)
    #if(json.VersusMatchCond!=null)
        if 'VersusMatchCond' in json:
            this['mVersusMatchCond'] = newList<VersusMatchCondParam>
        #for(intindex=0index<json.VersusMatchCond.Length++index)
            #VersusMatchCondParamversusMatchCondParam=newVersusMatchCondParam()
            #versusMatchCondParam.Deserialize(json.VersusMatchCond)
            #this.mVersusMatchCond.Add(versusMatchCondParam)
    #if(json.TowerScore!=null)
        if 'TowerScore' in json:
            this['mTowerScores'] = newDictionary<string,TowerScoreParam>
        #for(intindex1=0index1<json.TowerScore.Length++index1)
            #JSON_TowerScorejsonTowerScore=json.TowerScore[index1]
            #intlength=jsonTowerScore.threshold_vals.Length
            #TowerScoreParamtowerScoreParamArray=newTowerScoreParam[length]
            #for(intindex2=0index2<length++index2)
                #JSON_TowerScoreThresholdthresholdVal=jsonTowerScore.threshold_vals[index2]
                #towerScoreParamArray[index2]=newTowerScoreParam()
                #towerScoreParamArray[index2].Deserialize(thresholdVal)
            #this.mTowerScores.Add(jsonTowerScore.iname,towerScoreParamArray)
    #if(json.TowerRank!=null)
        #for(intindex=0index<json.TowerRank.Length++index)
        if 'TowerRank' in json:
            this['mTowerRankTbl'] = json['TowerRank']
    #if(json.MultilimitUnitLv!=null)
        #for(intindex=0index<json.MultilimitUnitLv.Length++index)
        if 'MultilimitUnitLv' in json:
            this['mMultiLimitUnitLv'] = json['MultilimitUnitLv']
    #if(json.FriendPresentItem!=null)
        #this.mFriendPresentItemParam=newDictionary<string,FriendPresentItemParam>()
        #for(intindex=0index<json.FriendPresentItem.Length++index)
            #FriendPresentItemParampresentItemParam=newFriendPresentItemParam()
            #presentItemParam.Deserialize(json.FriendPresentItem,this)
            #this.mFriendPresentItemParam.Add(presentItemParam.iname,presentItemParam)
    #if(json.Weather!=null)
        #List<WeatherParam>weatherParamList=newList<WeatherParam>(json.Weather.Length)
        #for(intindex=0index<json.Weather.Length++index)
            #WeatherParamweatherParam=newWeatherParam()
            #weatherParam.Deserialize(json.Weather)
            #weatherParamList.Add(weatherParam)
        #this.mWeatherParam=weatherParamList
    #if(json.UnitUnlockTime!=null)
        #this.mUnitUnlockTimeParam=newDictionary<string,UnitUnlockTimeParam>()
        #for(intindex=0index<json.UnitUnlockTime.Length++index)
            #UnitUnlockTimeParamunitUnlockTimeParam=newUnitUnlockTimeParam()
            #unitUnlockTimeParam.Deserialize(json.UnitUnlockTime)
            #this.mUnitUnlockTimeParam.Add(unitUnlockTimeParam.iname,unitUnlockTimeParam)
    #if(json.Tobira!=null)
        #for(intindex=0index<json.Tobira.Length++index)
            #TobiraParamtobiraParam=newTobiraParam()
            #tobiraParam.Deserialize(json.Tobira)
            #this.mTobiraParam.Add(tobiraParam)
    #if(json.TobiraCategories!=null)
        #for(intindex=0index<json.TobiraCategories.Length++index)
            #TobiraCategoriesParamtobiraCategoriesParam=newTobiraCategoriesParam()
            #tobiraCategoriesParam.Deserialize(json.TobiraCategories)
            #this.mTobiraCategoriesParam.Add(tobiraCategoriesParam.TobiraCategory,tobiraCategoriesParam)
    #if(json.TobiraConds!=null)
        #for(intindex=0index<json.TobiraConds.Length++index)
            #TobiraCondsParamtobiraCondsParam=newTobiraCondsParam()
            #tobiraCondsParam.Deserialize(json.TobiraConds)
            #this.mTobiraCondParam.Add(tobiraCondsParam)
    #if(json.TobiraCondsUnit!=null)
        #for(intindex=0index<json.TobiraCondsUnit.Length++index)
            #TobiraCondsUnitParamtobiraCondsUnitParam=newTobiraCondsUnitParam()
            #tobiraCondsUnitParam.Deserialize(json.TobiraCondsUnit)
            #this.mTobiraCondUnitParam.Add(tobiraCondsUnitParam.Id,tobiraCondsUnitParam)
    #if(json.TobiraRecipe!=null)
        #for(intindex=0index<json.TobiraRecipe.Length++index)
            #TobiraRecipeParamtobiraRecipeParam=newTobiraRecipeParam()
            #tobiraRecipeParam.Deserialize(json.TobiraRecipe)
            #this.mTobiraRecipeParam.Add(tobiraRecipeParam)
    #if(json.ConceptCard!=null)
        #this.mConceptCard=newDictionary<string,ConceptCardParam>()
        #for(intindex=0index<json.ConceptCard.Length++index)
            #ConceptCardParamconceptCardParam=newConceptCardParam()
            #conceptCardParam.Deserialize(json.ConceptCard,this)
            #this.mConceptCard.Add(conceptCardParam.iname,conceptCardParam)
    #intnumArray=newint[6]{json.ConceptCardLvTbl1,json.ConceptCardLvTbl2,json.ConceptCardLvTbl3,json.ConceptCardLvTbl4,json.ConceptCardLvTbl5,json.ConceptCardLvTbl6}
    #if(0<numArray.Length&&0<numArray[0].Length)
        #this.mConceptCardLvTbl=newOInt[numArray.Length,numArray[0].Length]
        #for(intindex1=0index1<numArray.Length++index1)
            #for(intindex2=0index2<numArray[index1].Length++index2)
            #this.mConceptCardLvTbl[index1,index2]=(OInt)numArray[index1][index2]
    #if(json.ConceptCardConditions!=null)
        #this.mConceptCardConditions=newDictionary<string,ConceptCardConditionsParam>()
        #for(intindex=0index<json.ConceptCardConditions.Length++index)
            #ConceptCardConditionsParamcardConditionsParam=newConceptCardConditionsParam()
            #cardConditionsParam.Deserialize(json.ConceptCardConditions)
            #this.mConceptCardConditions.Add(cardConditionsParam.iname,cardConditionsParam)
    #if(json.ConceptCardTrustReward!=null)
        #this.mConceptCardTrustReward=newDictionary<string,ConceptCardTrustRewardParam>()
        #for(intindex=0index<json.ConceptCardTrustReward.Length++index)
            #ConceptCardTrustRewardParamtrustRewardParam=newConceptCardTrustRewardParam()
            #trustRewardParam.Deserialize(json.ConceptCardTrustReward)
            #this.mConceptCardTrustReward.Add(trustRewardParam.iname,trustRewardParam)
    #if(json.UnitGroup!=null)
        #this.mUnitGroup=newDictionary<string,UnitGroupParam>()
        #for(intindex=0index<json.UnitGroup.Length++index)
            #UnitGroupParamunitGroupParam=newUnitGroupParam()
            #unitGroupParam.Deserialize(json.UnitGroup)
            #this.mUnitGroup.Add(unitGroupParam.iname,unitGroupParam)
    #if(json.JobGroup!=null)
        #this.mJobGroup=newDictionary<string,JobGroupParam>()
        #for(intindex=0index<json.JobGroup.Length++index)
            #JobGroupParamjobGroupParam=newJobGroupParam()
            #jobGroupParam.Deserialize(json.JobGroup)
            #this.mJobGroup.Add(jobGroupParam.iname,jobGroupParam)
    #if(json.StatusCoefficient!=null&&json.StatusCoefficient.Length>0)
        #this.mStatusCoefficient=newStatusCoefficientParam()
        #this.mStatusCoefficient.Deserialize(json.StatusCoefficient[0])
    #if(json.CustomTarget!=null)
        #this.mCustomTarget=newDictionary<string,CustomTargetParam>()
        #for(intindex=0index<json.CustomTarget.Length++index)
            #CustomTargetParamcustomTargetParam=newCustomTargetParam()
            #customTargetParam.Deserialize(json.CustomTarget)
            #this.mCustomTarget.Add(customTargetParam.iname,customTargetParam)
    #if(json.SkillAbilityDerive!=null&&json.SkillAbilityDerive.Length>0)
        if 'SkillAbilityDerive' in json:
            this['mSkillAbilityDeriveParam'] = newSkillAbilityDeriveParam[json['SkillAbilityDerive'].Length]
        #for(intindex=0index<json.SkillAbilityDerive.Length++index)
            #this.mSkillAbilityDeriveParam=newSkillAbilityDeriveParam(index)
            #this.mSkillAbilityDeriveParam.Deserialize(json.SkillAbilityDerive,this)
        #for(intindex=0index<this.mSkillAbilityDeriveParam.Length++index)
            #SkillAbilityDeriveDataabilityDeriveData=newSkillAbilityDeriveData()
            #List<SkillAbilityDeriveParam>abilityDeriveParam=this.FindAditionalSkillAbilityDeriveParam(this.mSkillAbilityDeriveParam)
            #abilityDeriveData.Setup(this.mSkillAbilityDeriveParam,abilityDeriveParam)
            #this.mSkillAbilityDerives.Add(abilityDeriveData)
    #if(json.Tips!=null&&json.Tips.Length>0)
        if 'Tips' in json:
            this['mTipsParam'] = newTipsParam[json['Tips'].Length]
        #for(intindex=0index<json.Tips.Length++index)
            #this.mTipsParam=newTipsParam()
            #this.mTipsParam.Deserialize(json.Tips)
    #if(json.GuildEmblem!=null)
        #if(this.mGuildEmblemParam==null)
        if 'GuildEmblem' in json:
            this['mGuildEmblemParam'] = newList<GuildEmblemParam>
        #if(this.mGuildEmblemDictionary==null)
        if 'GuildEmblem' in json:
            this['mGuildEmblemDictionary'] = newDictionary<string,GuildEmblemParam>
        #for(intindex=0index<json.GuildEmblem.Length++index)
            #JSON_GuildEmblemParamdata=json.GuildEmblem
            #if(!string.IsNullOrEmpty(data.iname))
                #GuildEmblemParamguildEmblemParam=this.mGuildEmblemParam.Find((Predicate<GuildEmblemParam>)(p=>p.Iname==data.iname))
                #if(guildEmblemParam==null)
                    #guildEmblemParam=newGuildEmblemParam()
                    #this.mGuildEmblemParam.Add(guildEmblemParam)
                #guildEmblemParam.Deserialize(data)
                #if(this.mGuildEmblemDictionary.ContainsKey(guildEmblemParam.Iname))
                #thrownewException("Overlap:GuildEmblem["+guildEmblemParam.Iname+"]")
                #this.mGuildEmblemDictionary.Add(guildEmblemParam.Iname,guildEmblemParam)
    #if(json.GuildFacility!=null)
        #if(this.mGuildFacilityParam==null)
        if 'GuildFacility' in json:
            this['mGuildFacilityParam'] = newList<GuildFacilityParam>
        #if(this.mGuildFacilityDictionary==null)
        if 'GuildFacility' in json:
            this['mGuildFacilityDictionary'] = newDictionary<string,GuildFacilityParam>
        #for(intindex=0index<json.GuildFacility.Length++index)
            #JSON_GuildFacilityParamdata=json.GuildFacility
            #if(!string.IsNullOrEmpty(data.iname))
                #GuildFacilityParamguildFacilityParam=this.mGuildFacilityParam.Find((Predicate<GuildFacilityParam>)(p=>p.Iname==data.iname))
                #if(guildFacilityParam==null)
                    #guildFacilityParam=newGuildFacilityParam()
                    #this.mGuildFacilityParam.Add(guildFacilityParam)
                #guildFacilityParam.Deserialize(data)
                #if(this.mGuildFacilityDictionary.ContainsKey(guildFacilityParam.Iname))
                #thrownewException("Overlap:GuildFacilityParam["+guildFacilityParam.Iname+"]")
                #this.mGuildFacilityDictionary.Add(guildFacilityParam.Iname,guildFacilityParam)
    #if(json.GuildFacilityLvTbl!=null)
        #if(this.mGuildFacilityLvParam==null)
        if 'GuildFacilityLvTbl' in json:
            this['mGuildFacilityLvParam'] = newGuildFacilityLvParam[json['GuildFacilityLvTbl'].Length]
        #for(intindex=0index<json.GuildFacilityLvTbl.Length++index)
            #GuildFacilityLvParamguildFacilityLvParam=newGuildFacilityLvParam()
            #guildFacilityLvParam.Deserialize(json.GuildFacilityLvTbl)
            #this.mGuildFacilityLvParam=guildFacilityLvParam
    #if(json.DynamicTransformUnit!=null)
        #List<DynamicTransformUnitParam>transformUnitParamList=newList<DynamicTransformUnitParam>(json.DynamicTransformUnit.Length)
        #for(intindex=0index<json.DynamicTransformUnit.Length++index)
            #DynamicTransformUnitParamtransformUnitParam=newDynamicTransformUnitParam()
            #transformUnitParam.Deserialize(json.DynamicTransformUnit)
            #transformUnitParamList.Add(transformUnitParam)
        #this.mDynamicTransformUnitParam=transformUnitParamList
    #if(json.ConvertUnitPieceExclude!=null&&json.ConvertUnitPieceExclude.Length>0)
        if 'ConvertUnitPieceExclude' in json:
            this['mConvertUnitPieceExcludeParam'] = newConvertUnitPieceExcludeParam[json['ConvertUnitPieceExclude'].Length]
        #for(intindex=0index<json.ConvertUnitPieceExclude.Length++index)
            #this.mConvertUnitPieceExcludeParam=newConvertUnitPieceExcludeParam()
            #this.mConvertUnitPieceExcludeParam.Deserialize(json.ConvertUnitPieceExclude)
    #if(json.RecommendedArtifact!=null&&json.RecommendedArtifact.Length>0)
        #this.mRecommendedArtifactList=newRecommendedArtifactList()
        #this.mRecommendedArtifactList.Deserialize(json.RecommendedArtifact)
    #RaidMaster.Deserialize<RaidPeriodParam,JSON_RaidPeriodParam>(refthis.mRaidPeriodParam,json.RaidPeriod)
    #RaidMaster.Deserialize<RaidAreaParam,JSON_RaidAreaParam>(refthis.mRaidAreaParam,json.RaidArea)
    #RaidMaster.Deserialize<RaidBossParam,JSON_RaidBossParam>(refthis.mRaidBossParam,json.RaidBoss)
    #RaidMaster.Deserialize<RaidBattleRewardParam,JSON_RaidBattleRewardParam>(refthis.mRaidBattleRewardParam,json.RaidBattleReward)
    #RaidMaster.Deserialize<RaidBeatRewardParam,JSON_RaidBeatRewardParam>(refthis.mRaidBeatRewardParam,json.RaidBeatReward)
    #RaidMaster.Deserialize<RaidDamageRatioRewardParam,JSON_RaidDamageRatioRewardParam>(refthis.mRaidDamageRatioRewardParam,json.RaidDamageRatioReward)
    #RaidMaster.Deserialize<RaidAreaClearRewardParam,JSON_RaidAreaClearRewardParam>(refthis.mRaidAreaClearRewardParam,json.RaidAreaClearReward)
    #RaidMaster.Deserialize<RaidCompleteRewardParam,JSON_RaidCompleteRewardParam>(refthis.mRaidCompleteRewardParam,json.RaidCompleteReward)
    #RaidMaster.Deserialize<RaidRewardParam,JSON_RaidRewardParam>(refthis.mRaidRewardParam,json.RaidReward)
    #if(json.SkillMotion!=null)
        #List<SkillMotionParam>skillMotionParamList=newList<SkillMotionParam>(json.SkillMotion.Length)
        #for(intindex=0index<json.SkillMotion.Length++index)
            #SkillMotionParamskillMotionParam=newSkillMotionParam()
            #skillMotionParam.Deserialize(json.SkillMotion)
            #skillMotionParamList.Add(skillMotionParam)
        #this.mSkillMotionParam=skillMotionParamList
    #this.Loaded=true
    #returntrue
return this
