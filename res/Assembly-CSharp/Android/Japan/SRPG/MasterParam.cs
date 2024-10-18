﻿// Decompiled with JetBrains decompiler
// Type: SRPG.MasterParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SRPG
{
  public class MasterParam
  {
    private FixParam mFixParam = new FixParam();
    private Dictionary<string, JobParam> mJobParamDict = new Dictionary<string, JobParam>();
    private List<TobiraParam> mTobiraParam = new List<TobiraParam>();
    private Dictionary<TobiraParam.Category, TobiraCategoriesParam> mTobiraCategoriesParam = new Dictionary<TobiraParam.Category, TobiraCategoriesParam>();
    private List<TobiraCondsParam> mTobiraCondParam = new List<TobiraCondsParam>();
    private Dictionary<string, TobiraCondsUnitParam> mTobiraCondUnitParam = new Dictionary<string, TobiraCondsUnitParam>();
    private List<TobiraRecipeParam> mTobiraRecipeParam = new List<TobiraRecipeParam>();
    private List<SkillAbilityDeriveData> mSkillAbilityDerives = new List<SkillAbilityDeriveData>();
    private List<UnitParam> mUnitParam;
    private List<UnitJobOverwriteParam> mUnitJobOverwriteParam;
    private List<SkillParam> mSkillParam;
    private List<AbilityParam> mAbilityParam;
    private List<ItemParam> mItemParam;
    private List<ArtifactParam> mArtifactParam;
    private List<WeaponParam> mWeaponParam;
    private List<RecipeParam> mRecipeParam;
    private List<JobParam> mJobParam;
    private List<QuestClearUnlockUnitDataParam> mUnlockUnitDataParam;
    private List<CollaboSkillParam> mCollaboSkillParam;
    private List<TrickParam> mTrickParam;
    private List<BreakObjParam> mBreakObjParam;
    private List<WeatherParam> mWeatherParam;
    private Dictionary<string, UnitUnlockTimeParam> mUnitUnlockTimeParam;
    private Dictionary<string, ConceptCardParam> mConceptCard;
    private OInt[,] mConceptCardLvTbl;
    private Dictionary<string, ConceptCardConditionsParam> mConceptCardConditions;
    private Dictionary<string, ConceptCardTrustRewardParam> mConceptCardTrustReward;
    private Dictionary<string, UnitGroupParam> mUnitGroup;
    private Dictionary<string, JobGroupParam> mJobGroup;
    private List<JobSetParam> mJobSetParam;
    private List<GrowParam> mGrowParam;
    private List<AIParam> mAIParam;
    private List<GeoParam> mGeoParam;
    private List<RarityParam> mRarityParam;
    private List<ShopParam> mShopParam;
    private PlayerParam[] mPlayerParamTbl;
    private OInt[] mPlayerExpTbl;
    private OInt[] mUnitExpTbl;
    private OInt[] mArtifactExpTbl;
    private OInt[] mAbilityExpTbl;
    private OInt[] mAwakePieceTbl;
    private LocalNotificationParam mLocalNotificationParam;
    private TrophyCategoryParam[] mTrophyCategory;
    private ChallengeCategoryParam[] mChallengeCategory;
    private TrophyParam[] mTrophy;
    public Dictionary<string, TrophyParam> mTrophyInameDict;
    private TrophyObjective[][] mTrophyDict;
    private TrophyParam[] mChallenge;
    private UnlockParam[] mUnlock;
    private VipParam[] mVip;
    private PremiumParam[] mPremium;
    private JSON_StreamingMovie[] mStreamingMovies;
    private BannerParam[] mBanner;
    private List<VersusMatchingParam> mVersusMatching;
    private List<VersusMatchCondParam> mVersusMatchCond;
    private Dictionary<string, TowerScoreParam[]> mTowerScores;
    private OInt[] mTowerRankTbl;
    public OInt[] mMultiLimitUnitLv;
    private Dictionary<string, UnitParam> mUnitDictionary;
    private Dictionary<string, Dictionary<string, UnitJobOverwriteParam>> mUnitJobOverwriteDictionary;
    private Dictionary<string, SkillParam> mSkillDictionary;
    private Dictionary<string, AbilityParam> mAbilityDictionary;
    private Dictionary<string, ItemParam> mItemDictionary;
    private Dictionary<string, ArtifactParam> mArtifactDictionary;
    private Dictionary<string, BuffEffectParam> mBuffEffectDictionary;
    private Dictionary<string, CondEffectParam> mCondEffectDictionary;
    private Dictionary<string, RecipeParam> mRecipeDictionary;
    private Dictionary<string, JobSetParam> mJobsetDictionary;
    private Dictionary<string, List<JobSetParam>> mJobsetTargetDictionary;
    private List<AwardParam> mAwardParam;
    private Dictionary<string, AwardParam> mAwardDictionary;
    private LoginInfoParam[] mLoginInfoParam;
    private Dictionary<string, FriendPresentItemParam> mFriendPresentItemParam;
    public StatusCoefficientParam mStatusCoefficient;
    private Dictionary<string, CustomTargetParam> mCustomTarget;
    public SkillAbilityDeriveParam[] mSkillAbilityDeriveParam;
    private TipsParam[] mTipsParam;
    private List<GuildEmblemParam> mGuildEmblemParam;
    private Dictionary<string, GuildEmblemParam> mGuildEmblemDictionary;
    private List<GuildFacilityParam> mGuildFacilityParam;
    private Dictionary<string, GuildFacilityParam> mGuildFacilityDictionary;
    private GuildFacilityLvParam[] mGuildFacilityLvParam;
    private List<DynamicTransformUnitParam> mDynamicTransformUnitParam;
    private ConvertUnitPieceExcludeParam[] mConvertUnitPieceExcludeParam;
    private RecommendedArtifactList mRecommendedArtifactList;
    private List<RaidPeriodParam> mRaidPeriodParam;
    private List<RaidAreaParam> mRaidAreaParam;
    private List<RaidBossParam> mRaidBossParam;
    private List<RaidBattleRewardParam> mRaidBattleRewardParam;
    private List<RaidBeatRewardParam> mRaidBeatRewardParam;
    private List<RaidDamageRatioRewardParam> mRaidDamageRatioRewardParam;
    private List<RaidAreaClearRewardParam> mRaidAreaClearRewardParam;
    private List<RaidCompleteRewardParam> mRaidCompleteRewardParam;
    private List<RaidRewardParam> mRaidRewardParam;
    private List<ArenaRewardParam> mArenaRewardParams;
    private List<SkillMotionParam> mSkillMotionParam;
    private List<DependStateSpcEffParam> mDependStateSpcEffParam;
    private List<InspSkillLvUpCostParam> mInspSkillLvUpCostParam;
    private Dictionary<string, InspSkillParam> mInspSkillDictionary;
    private Dictionary<int, InspSkillCostParam> mInspSkillResetCostDictionary;
    private Dictionary<int, InspSkillCostParam> mInspSkillOpenCostDictionary;
    private SRPG.HighlightParam[] mHighlightParam;
    private List<GenesisParam> mGenesisParam;

    public PremiumParam[] Premium
    {
      get
      {
        return this.mPremium;
      }
    }

    public List<SkillAbilityDeriveData> SkillAbilityDerives
    {
      get
      {
        return this.mSkillAbilityDerives;
      }
    }

    public TipsParam[] Tips
    {
      get
      {
        return this.mTipsParam;
      }
    }

    public ConvertUnitPieceExcludeParam[] ConvertUnitPieceExclude
    {
      get
      {
        return this.mConvertUnitPieceExcludeParam;
      }
    }

    public SRPG.HighlightParam[] HighlightParam
    {
      get
      {
        return this.mHighlightParam;
      }
    }

    public bool Loaded { get; set; }

    public FixParam FixParam
    {
      get
      {
        return this.mFixParam;
      }
    }

    public LocalNotificationParam LocalNotificationParam
    {
      get
      {
        return this.mLocalNotificationParam;
      }
    }

    public List<ItemParam> Items
    {
      get
      {
        return this.mItemParam;
      }
    }

    public JobSetParam[] JobSets
    {
      get
      {
        return this.mJobSetParam.ToArray();
      }
    }

    public List<ArtifactParam> Artifacts
    {
      get
      {
        return this.mArtifactParam;
      }
    }

    public List<CollaboSkillParam> CollaboSkills
    {
      get
      {
        return this.mCollaboSkillParam;
      }
    }

    public Dictionary<string, InspSkillParam> InspirationSkillParams
    {
      get
      {
        return this.mInspSkillDictionary;
      }
    }

    private void CreateTrophyDict()
    {
      if (this.mTrophy == null)
        return;
      List<TrophyObjective> trophyObjectiveList = new List<TrophyObjective>(this.mTrophy.Length);
      Array values = Enum.GetValues(typeof (TrophyConditionTypes));
      this.mTrophyDict = new TrophyObjective[values.Length][];
      for (int index1 = 0; index1 < values.Length; ++index1)
      {
        TrophyConditionTypes trophyConditionTypes = (TrophyConditionTypes) values.GetValue(index1);
        trophyObjectiveList.Clear();
        for (int index2 = 0; index2 < this.mTrophy.Length; ++index2)
        {
          TrophyParam trophyParam = this.mTrophy[index2];
          for (int index3 = 0; index3 < trophyParam.Objectives.Length; ++index3)
          {
            if (trophyParam.Objectives[index3].type == trophyConditionTypes)
              trophyObjectiveList.Add(trophyParam.Objectives[index3]);
          }
        }
        this.mTrophyDict[index1] = trophyObjectiveList.ToArray();
      }
    }

    private void AddUnitToItem()
    {
      if (this.mUnitDictionary.Count <= 0)
        return;
      foreach (KeyValuePair<string, UnitParam> mUnit in this.mUnitDictionary)
      {
        if (mUnit.Value != null && !string.IsNullOrEmpty(mUnit.Value.piece))
        {
          ItemParam itemParam = new ItemParam();
          itemParam.iname = mUnit.Value.iname;
          if (this.mItemDictionary.ContainsKey(mUnit.Value.piece))
          {
            ItemParam mItem = this.mItemDictionary[mUnit.Value.piece];
            if (mItem != null)
            {
              itemParam.name = mUnit.Value.name;
              itemParam.icon = mItem.icon;
              itemParam.type = EItemType.Unit;
              itemParam.cap = 999;
              this.mItemDictionary.Add(itemParam.iname, itemParam);
            }
          }
        }
      }
    }

    public bool Deserialize(JSON_MasterParam json)
    {
      if (this.Loaded)
        return true;
      DebugUtility.Verify((object) json, typeof (JSON_MasterParam));
      this.mLocalNotificationParam = (LocalNotificationParam) null;
      this.mFixParam.Deserialize(json.Fix[0]);
      if (json.Unit != null)
      {
        if (this.mUnitParam == null)
          this.mUnitParam = new List<UnitParam>(json.Unit.Length);
        if (this.mUnitDictionary == null)
          this.mUnitDictionary = new Dictionary<string, UnitParam>(json.Unit.Length);
        for (int index = 0; index < json.Unit.Length; ++index)
        {
          JSON_UnitParam json1 = json.Unit[index];
          UnitParam unitParam = new UnitParam();
          this.mUnitParam.Add(unitParam);
          unitParam.Deserialize(json1);
          if (this.mUnitDictionary.ContainsKey(json1.iname))
            throw new Exception("重複エラー：Unit[" + json1.iname + "]");
          this.mUnitDictionary.Add(json1.iname, unitParam);
        }
      }
      if (json.UnitJobOverwrite != null)
      {
        if (this.mUnitJobOverwriteParam == null)
          this.mUnitJobOverwriteParam = new List<UnitJobOverwriteParam>();
        if (this.mUnitJobOverwriteDictionary == null)
          this.mUnitJobOverwriteDictionary = new Dictionary<string, Dictionary<string, UnitJobOverwriteParam>>();
        foreach (JSON_UnitJobOverwriteParam json1 in json.UnitJobOverwrite)
        {
          UnitJobOverwriteParam jobOverwriteParam = new UnitJobOverwriteParam();
          this.mUnitJobOverwriteParam.Add(jobOverwriteParam);
          jobOverwriteParam.Deserialize(json1);
          Dictionary<string, UnitJobOverwriteParam> dictionary;
          this.mUnitJobOverwriteDictionary.TryGetValue(json1.unit_iname, out dictionary);
          if (dictionary == null)
          {
            dictionary = new Dictionary<string, UnitJobOverwriteParam>();
            this.mUnitJobOverwriteDictionary.Add(json1.unit_iname, dictionary);
          }
          if (!dictionary.ContainsKey(json1.job_iname))
            dictionary.Add(json1.job_iname, jobOverwriteParam);
        }
      }
      if (json.Skill != null)
      {
        if (this.mSkillParam == null)
          this.mSkillParam = new List<SkillParam>(json.Skill.Length);
        if (this.mSkillDictionary == null)
          this.mSkillDictionary = new Dictionary<string, SkillParam>(json.Skill.Length);
        for (int index = 0; index < json.Skill.Length; ++index)
        {
          JSON_SkillParam json1 = json.Skill[index];
          SkillParam skillParam = new SkillParam();
          this.mSkillParam.Add(skillParam);
          skillParam.Deserialize(json1);
          if (this.mSkillDictionary.ContainsKey(json1.iname))
            throw new Exception("重複エラー：Skill[" + json1.iname + "]");
          this.mSkillDictionary.Add(json1.iname, skillParam);
        }
        SkillParam.UpdateReplaceSkill(this.mSkillParam);
      }
      if (json.Buff != null)
      {
        this.mBuffEffectDictionary = new Dictionary<string, BuffEffectParam>(json.Buff.Length);
        for (int index = 0; index < json.Buff.Length; ++index)
        {
          JSON_BuffEffectParam json1 = json.Buff[index];
          BuffEffectParam buffEffectParam = new BuffEffectParam();
          buffEffectParam.Deserialize(json1);
          if (!this.mBuffEffectDictionary.ContainsKey(json1.iname))
          {
            this.mBuffEffectDictionary.Add(json1.iname, buffEffectParam);
          }
          else
          {
            this.mBuffEffectDictionary.Remove(json1.iname);
            this.mBuffEffectDictionary.Add(json1.iname, buffEffectParam);
            this.DebugLogError("重複エラー：Buff[" + json1.iname + "]");
          }
        }
      }
      if (json.Cond != null)
      {
        this.mCondEffectDictionary = new Dictionary<string, CondEffectParam>(json.Cond.Length);
        for (int index = 0; index < json.Cond.Length; ++index)
        {
          JSON_CondEffectParam json1 = json.Cond[index];
          CondEffectParam condEffectParam = new CondEffectParam();
          condEffectParam.Deserialize(json1);
          if (!this.mCondEffectDictionary.ContainsKey(json1.iname))
          {
            this.mCondEffectDictionary.Add(json1.iname, condEffectParam);
          }
          else
          {
            this.mCondEffectDictionary.Remove(json1.iname);
            this.mCondEffectDictionary.Add(json1.iname, condEffectParam);
            this.DebugLogError("重複エラー：Cond[" + json1.iname + "]");
          }
        }
      }
      if (json.Ability != null)
      {
        if (this.mAbilityParam == null)
          this.mAbilityParam = new List<AbilityParam>(json.Ability.Length);
        if (this.mAbilityDictionary == null)
          this.mAbilityDictionary = new Dictionary<string, AbilityParam>(json.Ability.Length);
        for (int index = 0; index < json.Ability.Length; ++index)
        {
          JSON_AbilityParam json1 = json.Ability[index];
          AbilityParam abilityParam = new AbilityParam();
          this.mAbilityParam.Add(abilityParam);
          abilityParam.Deserialize(json1);
          if (this.mAbilityDictionary.ContainsKey(json1.iname))
            throw new Exception("重複エラー：Ability[" + json1.iname + "]");
          this.mAbilityDictionary.Add(json1.iname, abilityParam);
        }
      }
      if (json.Item != null)
      {
        if (this.mItemParam == null)
          this.mItemParam = new List<ItemParam>(json.Item.Length);
        if (this.mItemDictionary == null)
          this.mItemDictionary = new Dictionary<string, ItemParam>(json.Item.Length);
        for (int index = 0; index < json.Item.Length; ++index)
        {
          JSON_ItemParam json1 = json.Item[index];
          ItemParam itemParam = new ItemParam();
          this.mItemParam.Add(itemParam);
          itemParam.Deserialize(json1);
          itemParam.no = index + 1;
          if (this.mItemDictionary.ContainsKey(json1.iname))
            throw new Exception("重複エラー：Item[" + json1.iname + "]");
          this.mItemDictionary.Add(json1.iname, itemParam);
        }
        this.AddUnitToItem();
      }
      if (json.Artifact != null)
      {
        if (this.mArtifactParam == null)
          this.mArtifactParam = new List<ArtifactParam>(json.Artifact.Length);
        if (this.mArtifactDictionary == null)
          this.mArtifactDictionary = new Dictionary<string, ArtifactParam>(json.Artifact.Length);
        for (int index = 0; index < json.Artifact.Length; ++index)
        {
          JSON_ArtifactParam json1 = json.Artifact[index];
          if (json1.iname != null)
          {
            ArtifactParam artifactParam = new ArtifactParam();
            this.mArtifactParam.Add(artifactParam);
            artifactParam.Deserialize(json1);
            if (this.mArtifactDictionary.ContainsKey(json1.iname))
              throw new Exception("重複エラー：Artifact[" + json1.iname + "]");
            this.mArtifactDictionary.Add(json1.iname, artifactParam);
          }
        }
      }
      if (json.Weapon != null)
      {
        if (this.mWeaponParam == null)
          this.mWeaponParam = new List<WeaponParam>(json.Weapon.Length);
        for (int index = 0; index < json.Weapon.Length; ++index)
        {
          JSON_WeaponParam json1 = json.Weapon[index];
          WeaponParam weaponParam = new WeaponParam();
          this.mWeaponParam.Add(weaponParam);
          weaponParam.Deserialize(json1);
        }
      }
      if (json.Recipe != null)
      {
        if (this.mRecipeParam == null)
          this.mRecipeParam = new List<RecipeParam>(json.Recipe.Length);
        if (this.mRecipeDictionary == null)
          this.mRecipeDictionary = new Dictionary<string, RecipeParam>(json.Recipe.Length);
        for (int index = 0; index < json.Recipe.Length; ++index)
        {
          JSON_RecipeParam json1 = json.Recipe[index];
          RecipeParam recipeParam = new RecipeParam();
          this.mRecipeParam.Add(recipeParam);
          recipeParam.Deserialize(json1);
          if (!this.mRecipeDictionary.ContainsKey(json1.iname))
          {
            this.mRecipeDictionary.Add(json1.iname, recipeParam);
          }
          else
          {
            this.mRecipeDictionary.Remove(json1.iname);
            this.mRecipeDictionary.Add(json1.iname, recipeParam);
            this.DebugLogError("重複エラー：mRecipe[" + json1.iname + "]");
          }
        }
      }
      if (json.Job != null)
      {
        if (this.mJobParam == null)
          this.mJobParam = new List<JobParam>(json.Job.Length);
        for (int index = 0; index < json.Job.Length; ++index)
        {
          JSON_JobParam json1 = json.Job[index];
          JobParam jobParam = new JobParam();
          this.mJobParam.Add(jobParam);
          this.mJobParamDict[json1.iname] = jobParam;
          jobParam.Deserialize(json1, this);
        }
      }
      if (json.JobSet != null)
      {
        if (this.mJobSetParam == null)
          this.mJobSetParam = new List<JobSetParam>(json.JobSet.Length);
        if (this.mJobsetDictionary == null)
          this.mJobsetDictionary = new Dictionary<string, JobSetParam>(json.JobSet.Length);
        if (this.mJobsetTargetDictionary == null)
          this.mJobsetTargetDictionary = new Dictionary<string, List<JobSetParam>>(json.Unit.Length);
        for (int index = 0; index < json.JobSet.Length; ++index)
        {
          JSON_JobSetParam job = json.JobSet[index];
          JobSetParam jobSetParam = new JobSetParam();
          this.mJobSetParam.Add(jobSetParam);
          jobSetParam.Deserialize(job);
          if (!this.mJobsetDictionary.ContainsKey(job.iname))
          {
            this.mJobsetDictionary.Add(job.iname, jobSetParam);
          }
          else
          {
            this.mJobsetDictionary.Remove(job.iname);
            this.mJobsetDictionary.Add(job.iname, jobSetParam);
            this.DebugLogError("重複エラー：mJobset[" + job.iname + "]");
          }
          if (!string.IsNullOrEmpty(jobSetParam.target_unit))
          {
            List<JobSetParam> jobSetParamList;
            if (this.mJobsetTargetDictionary.ContainsKey(jobSetParam.target_unit))
            {
              jobSetParamList = this.mJobsetTargetDictionary[jobSetParam.target_unit];
            }
            else
            {
              jobSetParamList = new List<JobSetParam>(3);
              this.mJobsetTargetDictionary.Add(jobSetParam.target_unit, jobSetParamList);
            }
            jobSetParamList.Add(jobSetParam);
          }
        }
      }
      if (json.Grow != null)
      {
        if (this.mGrowParam == null)
          this.mGrowParam = new List<GrowParam>(json.Grow.Length);
        for (int index = 0; index < json.Grow.Length; ++index)
        {
          JSON_GrowParam json1 = json.Grow[index];
          GrowParam growParam = new GrowParam();
          this.mGrowParam.Add(growParam);
          growParam.Deserialize(json1);
        }
      }
      if (json.AI != null)
      {
        if (this.mAIParam == null)
          this.mAIParam = new List<AIParam>(json.AI.Length);
        for (int index = 0; index < json.AI.Length; ++index)
        {
          JSON_AIParam json1 = json.AI[index];
          AIParam aiParam = new AIParam();
          this.mAIParam.Add(aiParam);
          aiParam.Deserialize(json1);
        }
      }
      if (json.Geo != null)
      {
        if (this.mGeoParam == null)
          this.mGeoParam = new List<GeoParam>(json.Geo.Length);
        for (int index = 0; index < json.Geo.Length; ++index)
        {
          JSON_GeoParam json1 = json.Geo[index];
          GeoParam geoParam = new GeoParam();
          this.mGeoParam.Add(geoParam);
          geoParam.Deserialize(json1);
        }
      }
      if (json.Rarity != null)
      {
        if (this.mRarityParam == null)
          this.mRarityParam = new List<RarityParam>(json.Rarity.Length);
        for (int index = 0; index < json.Rarity.Length; ++index)
        {
          RarityParam rarityParam;
          if (this.mRarityParam.Count > index)
          {
            rarityParam = this.mRarityParam[index];
          }
          else
          {
            rarityParam = new RarityParam();
            this.mRarityParam.Add(rarityParam);
          }
          rarityParam.Deserialize(json.Rarity[index]);
        }
      }
      if (json.Shop != null)
      {
        if (this.mShopParam == null)
          this.mShopParam = new List<ShopParam>(json.Shop.Length);
        for (int index = 0; index < json.Shop.Length; ++index)
        {
          ShopParam shopParam;
          if (this.mShopParam.Count > index)
          {
            shopParam = this.mShopParam[index];
          }
          else
          {
            shopParam = new ShopParam();
            this.mShopParam.Add(shopParam);
          }
          shopParam.Deserialize(json.Shop[index]);
        }
      }
      if (json.Player != null)
      {
        this.mPlayerParamTbl = new PlayerParam[json.Player.Length];
        for (int index = 0; index < json.Player.Length; ++index)
        {
          JSON_PlayerParam json1 = json.Player[index];
          this.mPlayerParamTbl[index] = new PlayerParam();
          this.mPlayerParamTbl[index].Deserialize(json1);
        }
      }
      if (json.PlayerLvTbl != null)
      {
        this.mPlayerExpTbl = new OInt[json.PlayerLvTbl.Length];
        for (int index = 0; index < json.PlayerLvTbl.Length; ++index)
          this.mPlayerExpTbl[index] = (OInt) json.PlayerLvTbl[index];
      }
      if (json.UnitLvTbl != null)
      {
        this.mUnitExpTbl = new OInt[json.UnitLvTbl.Length];
        for (int index = 0; index < json.UnitLvTbl.Length; ++index)
          this.mUnitExpTbl[index] = (OInt) json.UnitLvTbl[index];
      }
      if (json.ArtifactLvTbl != null)
      {
        this.mArtifactExpTbl = new OInt[json.ArtifactLvTbl.Length];
        for (int index = 0; index < json.ArtifactLvTbl.Length; ++index)
          this.mArtifactExpTbl[index] = (OInt) json.ArtifactLvTbl[index];
      }
      if (json.AbilityRank != null)
      {
        this.mAbilityExpTbl = new OInt[json.AbilityRank.Length];
        for (int index = 0; index < json.AbilityRank.Length; ++index)
          this.mAbilityExpTbl[index] = (OInt) json.AbilityRank[index];
      }
      if (json.AwakePieceTbl != null)
      {
        this.mAwakePieceTbl = new OInt[json.AwakePieceTbl.Length];
        for (int index = 0; index < json.AwakePieceTbl.Length; ++index)
          this.mAwakePieceTbl[index] = (OInt) json.AwakePieceTbl[index];
      }
      this.mLocalNotificationParam = new LocalNotificationParam();
      if (json.LocalNotification != null)
      {
        this.mLocalNotificationParam.msg_stamina = json.LocalNotification[0].msg_stamina;
        this.mLocalNotificationParam.iOSAct_stamina = json.LocalNotification[0].iOSAct_stamina;
        this.mLocalNotificationParam.limitSec_stamina = json.LocalNotification[0].limitSec_stamina;
      }
      Dictionary<int, TrophyCategoryParam> dictionary1 = new Dictionary<int, TrophyCategoryParam>();
      if (json.TrophyCategory != null)
      {
        List<TrophyCategoryParam> trophyCategoryParamList = new List<TrophyCategoryParam>(json.TrophyCategory.Length);
        for (int index = 0; index < json.TrophyCategory.Length; ++index)
        {
          TrophyCategoryParam trophyCategoryParam = new TrophyCategoryParam();
          if (trophyCategoryParam.Deserialize(json.TrophyCategory[index]))
          {
            trophyCategoryParamList.Add(trophyCategoryParam);
            if (!dictionary1.ContainsKey(trophyCategoryParam.hash_code))
              dictionary1.Add(trophyCategoryParam.hash_code, trophyCategoryParam);
          }
        }
        this.mTrophyCategory = trophyCategoryParamList.ToArray();
      }
      if (json.Trophy != null)
      {
        List<TrophyParam> trophyParamList = new List<TrophyParam>(json.Trophy.Length);
        for (int index = 0; index < json.Trophy.Length; ++index)
        {
          TrophyParam trophyParam = new TrophyParam();
          if (trophyParam.Deserialize(json.Trophy[index]))
          {
            if (dictionary1.ContainsKey(trophyParam.category_hash_code))
              trophyParam.CategoryParam = dictionary1[trophyParam.category_hash_code];
            if (trophyParam.IsPlanningToUse())
              trophyParamList.Add(trophyParam);
          }
        }
        this.mTrophy = trophyParamList.ToArray();
        this.mTrophyInameDict = new Dictionary<string, TrophyParam>();
        foreach (TrophyParam trophyParam in this.mTrophy)
          this.mTrophyInameDict.Add(trophyParam.iname, trophyParam);
      }
      Dictionary<string, ChallengeCategoryParam> dictionary2 = new Dictionary<string, ChallengeCategoryParam>();
      if (json.ChallengeCategory != null)
      {
        List<ChallengeCategoryParam> challengeCategoryParamList = new List<ChallengeCategoryParam>(json.ChallengeCategory.Length);
        for (int index = 0; index < json.ChallengeCategory.Length; ++index)
        {
          ChallengeCategoryParam challengeCategoryParam = new ChallengeCategoryParam();
          if (challengeCategoryParam.Deserialize(json.ChallengeCategory[index]))
          {
            dictionary2[challengeCategoryParam.iname] = challengeCategoryParam;
            challengeCategoryParamList.Add(challengeCategoryParam);
          }
        }
        this.mChallengeCategory = challengeCategoryParamList.ToArray();
      }
      if (json.Challenge != null)
      {
        List<TrophyParam> trophyParamList = new List<TrophyParam>(json.Challenge.Length);
        for (int index = 0; index < json.Challenge.Length; ++index)
        {
          TrophyParam trophyParam = new TrophyParam();
          if (trophyParam.Deserialize(json.Challenge[index]))
          {
            if (dictionary2.ContainsKey(trophyParam.Category))
              trophyParam.ChallengeCategoryParam = dictionary2[trophyParam.Category];
            trophyParam.Challenge = 1;
            trophyParamList.Add(trophyParam);
          }
        }
        this.mChallenge = trophyParamList.ToArray();
        int length = this.mTrophy.Length;
        Array.Resize<TrophyParam>(ref this.mTrophy, length + this.mChallenge.Length);
        Array.Copy((Array) this.mChallenge, 0, (Array) this.mTrophy, length, this.mChallenge.Length);
        foreach (TrophyParam trophyParam in this.mChallenge)
          this.mTrophyInameDict.Add(trophyParam.iname, trophyParam);
      }
      this.CreateTrophyDict();
      if (json.Unlock != null)
      {
        List<UnlockParam> unlockParamList = new List<UnlockParam>(json.Unlock.Length);
        for (int index = 0; index < json.Unlock.Length; ++index)
        {
          UnlockParam unlockParam = new UnlockParam();
          if (unlockParam.Deserialize(json.Unlock[index]))
            unlockParamList.Add(unlockParam);
        }
        this.mUnlock = unlockParamList.ToArray();
      }
      if (json.Vip != null)
      {
        List<VipParam> vipParamList = new List<VipParam>(json.Vip.Length);
        for (int index = 0; index < json.Vip.Length; ++index)
        {
          VipParam vipParam = new VipParam();
          if (vipParam.Deserialize(json.Vip[index]))
            vipParamList.Add(vipParam);
        }
        this.mVip = vipParamList.ToArray();
      }
      if (json.Premium != null)
      {
        List<PremiumParam> premiumParamList = new List<PremiumParam>(json.Premium.Length);
        for (int index = 0; index < json.Premium.Length; ++index)
        {
          PremiumParam premiumParam = new PremiumParam();
          if (premiumParam.Deserialize(json.Premium[index]))
            premiumParamList.Add(premiumParam);
        }
        this.mPremium = premiumParamList.ToArray();
      }
      if (json.Mov != null)
      {
        this.mStreamingMovies = new JSON_StreamingMovie[json.Mov.Length];
        for (int index = 0; index < json.Mov.Length; ++index)
        {
          this.mStreamingMovies[index] = new JSON_StreamingMovie();
          this.mStreamingMovies[index].alias = json.Mov[index].alias;
          this.mStreamingMovies[index].path = json.Mov[index].path;
        }
      }
      if (json.Banner != null)
      {
        List<BannerParam> bannerParamList = new List<BannerParam>(json.Banner.Length);
        for (int index = 0; index < json.Banner.Length; ++index)
        {
          BannerParam bannerParam = new BannerParam();
          if (bannerParam.Deserialize(json.Banner[index]))
            bannerParamList.Add(bannerParam);
        }
        this.mBanner = bannerParamList.ToArray();
      }
      if (json.QuestClearUnlockUnitData != null)
      {
        List<QuestClearUnlockUnitDataParam> unlockUnitDataParamList = new List<QuestClearUnlockUnitDataParam>(json.QuestClearUnlockUnitData.Length);
        for (int index = 0; index < json.QuestClearUnlockUnitData.Length; ++index)
        {
          QuestClearUnlockUnitDataParam unlockUnitDataParam = new QuestClearUnlockUnitDataParam();
          unlockUnitDataParam.Deserialize(json.QuestClearUnlockUnitData[index]);
          unlockUnitDataParamList.Add(unlockUnitDataParam);
        }
        this.mUnlockUnitDataParam = unlockUnitDataParamList;
      }
      if (json.Award != null)
      {
        if (this.mAwardParam == null)
          this.mAwardParam = new List<AwardParam>(json.Award.Length);
        if (this.mAwardDictionary == null)
          this.mAwardDictionary = new Dictionary<string, AwardParam>(json.Award.Length);
        for (int index = 0; index < json.Award.Length; ++index)
        {
          JSON_AwardParam json1 = json.Award[index];
          if (json1.iname != null)
          {
            AwardParam awardParam = new AwardParam();
            this.mAwardParam.Add(awardParam);
            awardParam.Deserialize(json1);
            if (this.mAwardDictionary.ContainsKey(awardParam.iname))
              throw new Exception("Overlap : Award[" + awardParam.iname + "]");
            this.mAwardDictionary.Add(awardParam.iname, awardParam);
          }
        }
      }
      if (json.LoginInfo != null)
      {
        List<LoginInfoParam> loginInfoParamList = new List<LoginInfoParam>(json.LoginInfo.Length);
        for (int index = 0; index < json.LoginInfo.Length; ++index)
        {
          LoginInfoParam loginInfoParam = new LoginInfoParam();
          if (loginInfoParam.Deserialize(json.LoginInfo[index]))
            loginInfoParamList.Add(loginInfoParam);
        }
        this.mLoginInfoParam = loginInfoParamList.ToArray();
      }
      if (json.CollaboSkill != null)
      {
        List<CollaboSkillParam> collaboSkillParamList = new List<CollaboSkillParam>(json.CollaboSkill.Length);
        for (int index = 0; index < json.CollaboSkill.Length; ++index)
        {
          CollaboSkillParam collaboSkillParam = new CollaboSkillParam();
          collaboSkillParam.Deserialize(json.CollaboSkill[index]);
          collaboSkillParamList.Add(collaboSkillParam);
        }
        this.mCollaboSkillParam = collaboSkillParamList;
        CollaboSkillParam.UpdateCollaboSkill(this.mCollaboSkillParam);
      }
      if (json.Trick != null)
      {
        List<TrickParam> trickParamList = new List<TrickParam>(json.Trick.Length);
        for (int index = 0; index < json.Trick.Length; ++index)
        {
          TrickParam trickParam = new TrickParam();
          trickParam.Deserialize(json.Trick[index]);
          trickParamList.Add(trickParam);
        }
        this.mTrickParam = trickParamList;
      }
      if (json.BreakObj != null)
      {
        List<BreakObjParam> breakObjParamList = new List<BreakObjParam>(json.BreakObj.Length);
        for (int index = 0; index < json.BreakObj.Length; ++index)
        {
          BreakObjParam breakObjParam = new BreakObjParam();
          breakObjParam.Deserialize(json.BreakObj[index]);
          breakObjParamList.Add(breakObjParam);
        }
        this.mBreakObjParam = breakObjParamList;
      }
      if (json.VersusMatchKey != null)
      {
        this.mVersusMatching = new List<VersusMatchingParam>(json.VersusMatchKey.Length);
        for (int index = 0; index < json.VersusMatchKey.Length; ++index)
        {
          VersusMatchingParam versusMatchingParam = new VersusMatchingParam();
          versusMatchingParam.Deserialize(json.VersusMatchKey[index]);
          this.mVersusMatching.Add(versusMatchingParam);
        }
      }
      if (json.VersusMatchCond != null)
      {
        this.mVersusMatchCond = new List<VersusMatchCondParam>(json.VersusMatchCond.Length);
        for (int index = 0; index < json.VersusMatchCond.Length; ++index)
        {
          VersusMatchCondParam versusMatchCondParam = new VersusMatchCondParam();
          versusMatchCondParam.Deserialize(json.VersusMatchCond[index]);
          this.mVersusMatchCond.Add(versusMatchCondParam);
        }
      }
      if (json.TowerScore != null)
      {
        this.mTowerScores = new Dictionary<string, TowerScoreParam[]>(json.TowerScore.Length);
        for (int index1 = 0; index1 < json.TowerScore.Length; ++index1)
        {
          JSON_TowerScore jsonTowerScore = json.TowerScore[index1];
          int length = jsonTowerScore.threshold_vals.Length;
          TowerScoreParam[] towerScoreParamArray = new TowerScoreParam[length];
          for (int index2 = 0; index2 < length; ++index2)
          {
            JSON_TowerScoreThreshold thresholdVal = jsonTowerScore.threshold_vals[index2];
            towerScoreParamArray[index2] = new TowerScoreParam();
            towerScoreParamArray[index2].Deserialize(thresholdVal);
          }
          this.mTowerScores.Add(jsonTowerScore.iname, towerScoreParamArray);
        }
      }
      if (json.TowerRank != null)
      {
        this.mTowerRankTbl = new OInt[json.TowerRank.Length];
        for (int index = 0; index < json.TowerRank.Length; ++index)
          this.mTowerRankTbl[index] = (OInt) json.TowerRank[index];
      }
      if (json.MultilimitUnitLv != null)
      {
        this.mMultiLimitUnitLv = new OInt[json.MultilimitUnitLv.Length];
        for (int index = 0; index < json.MultilimitUnitLv.Length; ++index)
          this.mMultiLimitUnitLv[index] = (OInt) json.MultilimitUnitLv[index];
      }
      if (json.FriendPresentItem != null)
      {
        this.mFriendPresentItemParam = new Dictionary<string, FriendPresentItemParam>();
        for (int index = 0; index < json.FriendPresentItem.Length; ++index)
        {
          FriendPresentItemParam presentItemParam = new FriendPresentItemParam();
          presentItemParam.Deserialize(json.FriendPresentItem[index], this);
          this.mFriendPresentItemParam.Add(presentItemParam.iname, presentItemParam);
        }
      }
      if (json.Weather != null)
      {
        List<WeatherParam> weatherParamList = new List<WeatherParam>(json.Weather.Length);
        for (int index = 0; index < json.Weather.Length; ++index)
        {
          WeatherParam weatherParam = new WeatherParam();
          weatherParam.Deserialize(json.Weather[index]);
          weatherParamList.Add(weatherParam);
        }
        this.mWeatherParam = weatherParamList;
      }
      if (json.UnitUnlockTime != null)
      {
        this.mUnitUnlockTimeParam = new Dictionary<string, UnitUnlockTimeParam>();
        for (int index = 0; index < json.UnitUnlockTime.Length; ++index)
        {
          UnitUnlockTimeParam unitUnlockTimeParam = new UnitUnlockTimeParam();
          unitUnlockTimeParam.Deserialize(json.UnitUnlockTime[index]);
          this.mUnitUnlockTimeParam.Add(unitUnlockTimeParam.iname, unitUnlockTimeParam);
        }
      }
      if (json.Tobira != null)
      {
        for (int index = 0; index < json.Tobira.Length; ++index)
        {
          TobiraParam tobiraParam = new TobiraParam();
          tobiraParam.Deserialize(json.Tobira[index]);
          this.mTobiraParam.Add(tobiraParam);
        }
      }
      if (json.TobiraCategories != null)
      {
        for (int index = 0; index < json.TobiraCategories.Length; ++index)
        {
          TobiraCategoriesParam tobiraCategoriesParam = new TobiraCategoriesParam();
          tobiraCategoriesParam.Deserialize(json.TobiraCategories[index]);
          this.mTobiraCategoriesParam.Add(tobiraCategoriesParam.TobiraCategory, tobiraCategoriesParam);
        }
      }
      if (json.TobiraConds != null)
      {
        for (int index = 0; index < json.TobiraConds.Length; ++index)
        {
          TobiraCondsParam tobiraCondsParam = new TobiraCondsParam();
          tobiraCondsParam.Deserialize(json.TobiraConds[index]);
          this.mTobiraCondParam.Add(tobiraCondsParam);
        }
      }
      if (json.TobiraCondsUnit != null)
      {
        for (int index = 0; index < json.TobiraCondsUnit.Length; ++index)
        {
          TobiraCondsUnitParam tobiraCondsUnitParam = new TobiraCondsUnitParam();
          tobiraCondsUnitParam.Deserialize(json.TobiraCondsUnit[index]);
          this.mTobiraCondUnitParam.Add(tobiraCondsUnitParam.Id, tobiraCondsUnitParam);
        }
      }
      if (json.TobiraRecipe != null)
      {
        for (int index = 0; index < json.TobiraRecipe.Length; ++index)
        {
          TobiraRecipeParam tobiraRecipeParam = new TobiraRecipeParam();
          tobiraRecipeParam.Deserialize(json.TobiraRecipe[index]);
          this.mTobiraRecipeParam.Add(tobiraRecipeParam);
        }
      }
      if (json.ConceptCard != null)
      {
        this.mConceptCard = new Dictionary<string, ConceptCardParam>();
        for (int index = 0; index < json.ConceptCard.Length; ++index)
        {
          ConceptCardParam conceptCardParam = new ConceptCardParam();
          conceptCardParam.Deserialize(json.ConceptCard[index], this);
          this.mConceptCard.Add(conceptCardParam.iname, conceptCardParam);
        }
      }
      int[][] numArray = new int[6][]{ json.ConceptCardLvTbl1, json.ConceptCardLvTbl2, json.ConceptCardLvTbl3, json.ConceptCardLvTbl4, json.ConceptCardLvTbl5, json.ConceptCardLvTbl6 };
      if (0 < numArray.Length && 0 < numArray[0].Length)
      {
        this.mConceptCardLvTbl = new OInt[numArray.Length, numArray[0].Length];
        for (int index1 = 0; index1 < numArray.Length; ++index1)
        {
          for (int index2 = 0; index2 < numArray[index1].Length; ++index2)
            this.mConceptCardLvTbl[index1, index2] = (OInt) numArray[index1][index2];
        }
      }
      if (json.ConceptCardConditions != null)
      {
        this.mConceptCardConditions = new Dictionary<string, ConceptCardConditionsParam>();
        for (int index = 0; index < json.ConceptCardConditions.Length; ++index)
        {
          ConceptCardConditionsParam cardConditionsParam = new ConceptCardConditionsParam();
          cardConditionsParam.Deserialize(json.ConceptCardConditions[index]);
          this.mConceptCardConditions.Add(cardConditionsParam.iname, cardConditionsParam);
        }
      }
      if (json.ConceptCardTrustReward != null)
      {
        this.mConceptCardTrustReward = new Dictionary<string, ConceptCardTrustRewardParam>();
        for (int index = 0; index < json.ConceptCardTrustReward.Length; ++index)
        {
          ConceptCardTrustRewardParam trustRewardParam = new ConceptCardTrustRewardParam();
          trustRewardParam.Deserialize(json.ConceptCardTrustReward[index]);
          this.mConceptCardTrustReward.Add(trustRewardParam.iname, trustRewardParam);
        }
      }
      if (json.UnitGroup != null)
      {
        this.mUnitGroup = new Dictionary<string, UnitGroupParam>();
        for (int index = 0; index < json.UnitGroup.Length; ++index)
        {
          UnitGroupParam unitGroupParam = new UnitGroupParam();
          unitGroupParam.Deserialize(json.UnitGroup[index]);
          this.mUnitGroup.Add(unitGroupParam.iname, unitGroupParam);
        }
      }
      if (json.JobGroup != null)
      {
        this.mJobGroup = new Dictionary<string, JobGroupParam>();
        for (int index = 0; index < json.JobGroup.Length; ++index)
        {
          JobGroupParam jobGroupParam = new JobGroupParam();
          jobGroupParam.Deserialize(json.JobGroup[index]);
          this.mJobGroup.Add(jobGroupParam.iname, jobGroupParam);
        }
      }
      if (json.StatusCoefficient != null && json.StatusCoefficient.Length > 0)
      {
        this.mStatusCoefficient = new StatusCoefficientParam();
        this.mStatusCoefficient.Deserialize(json.StatusCoefficient[0]);
      }
      if (json.CustomTarget != null)
      {
        this.mCustomTarget = new Dictionary<string, CustomTargetParam>();
        for (int index = 0; index < json.CustomTarget.Length; ++index)
        {
          CustomTargetParam customTargetParam = new CustomTargetParam();
          customTargetParam.Deserialize(json.CustomTarget[index]);
          this.mCustomTarget.Add(customTargetParam.iname, customTargetParam);
        }
      }
      if (json.SkillAbilityDerive != null && json.SkillAbilityDerive.Length > 0)
      {
        this.mSkillAbilityDeriveParam = new SkillAbilityDeriveParam[json.SkillAbilityDerive.Length];
        for (int index = 0; index < json.SkillAbilityDerive.Length; ++index)
        {
          this.mSkillAbilityDeriveParam[index] = new SkillAbilityDeriveParam(index);
          this.mSkillAbilityDeriveParam[index].Deserialize(json.SkillAbilityDerive[index], this);
        }
        for (int index = 0; index < this.mSkillAbilityDeriveParam.Length; ++index)
        {
          SkillAbilityDeriveData abilityDeriveData = new SkillAbilityDeriveData();
          List<SkillAbilityDeriveParam> abilityDeriveParam = this.FindAditionalSkillAbilityDeriveParam(this.mSkillAbilityDeriveParam[index]);
          abilityDeriveData.Setup(this.mSkillAbilityDeriveParam[index], abilityDeriveParam);
          this.mSkillAbilityDerives.Add(abilityDeriveData);
        }
      }
      if (json.Tips != null && json.Tips.Length > 0)
      {
        this.mTipsParam = new TipsParam[json.Tips.Length];
        for (int index = 0; index < json.Tips.Length; ++index)
        {
          this.mTipsParam[index] = new TipsParam();
          this.mTipsParam[index].Deserialize(json.Tips[index]);
        }
      }
      if (json.GuildEmblem != null)
      {
        if (this.mGuildEmblemParam == null)
          this.mGuildEmblemParam = new List<GuildEmblemParam>(json.GuildEmblem.Length);
        if (this.mGuildEmblemDictionary == null)
          this.mGuildEmblemDictionary = new Dictionary<string, GuildEmblemParam>(json.GuildEmblem.Length);
        for (int index = 0; index < json.GuildEmblem.Length; ++index)
        {
          JSON_GuildEmblemParam json1 = json.GuildEmblem[index];
          if (!string.IsNullOrEmpty(json1.iname))
          {
            GuildEmblemParam guildEmblemParam = new GuildEmblemParam();
            this.mGuildEmblemParam.Add(guildEmblemParam);
            guildEmblemParam.Deserialize(json1);
            if (this.mGuildEmblemDictionary.ContainsKey(guildEmblemParam.Iname))
              throw new Exception("Overlap : GuildEmblem[" + guildEmblemParam.Iname + "]");
            this.mGuildEmblemDictionary.Add(guildEmblemParam.Iname, guildEmblemParam);
          }
        }
      }
      if (json.GuildFacility != null)
      {
        if (this.mGuildFacilityParam == null)
          this.mGuildFacilityParam = new List<GuildFacilityParam>(json.GuildFacility.Length);
        if (this.mGuildFacilityDictionary == null)
          this.mGuildFacilityDictionary = new Dictionary<string, GuildFacilityParam>(json.GuildFacility.Length);
        for (int index = 0; index < json.GuildFacility.Length; ++index)
        {
          JSON_GuildFacilityParam json1 = json.GuildFacility[index];
          if (!string.IsNullOrEmpty(json1.iname))
          {
            GuildFacilityParam guildFacilityParam = new GuildFacilityParam();
            this.mGuildFacilityParam.Add(guildFacilityParam);
            guildFacilityParam.Deserialize(json1);
            if (this.mGuildFacilityDictionary.ContainsKey(guildFacilityParam.Iname))
              throw new Exception("Overlap : GuildFacilityParam[" + guildFacilityParam.Iname + "]");
            this.mGuildFacilityDictionary.Add(guildFacilityParam.Iname, guildFacilityParam);
          }
        }
      }
      if (json.GuildFacilityLvTbl != null)
      {
        if (this.mGuildFacilityLvParam == null)
          this.mGuildFacilityLvParam = new GuildFacilityLvParam[json.GuildFacilityLvTbl.Length];
        for (int index = 0; index < json.GuildFacilityLvTbl.Length; ++index)
        {
          GuildFacilityLvParam guildFacilityLvParam = new GuildFacilityLvParam();
          guildFacilityLvParam.Deserialize(json.GuildFacilityLvTbl[index]);
          this.mGuildFacilityLvParam[index] = guildFacilityLvParam;
        }
      }
      if (json.DynamicTransformUnit != null)
      {
        List<DynamicTransformUnitParam> transformUnitParamList = new List<DynamicTransformUnitParam>(json.DynamicTransformUnit.Length);
        for (int index = 0; index < json.DynamicTransformUnit.Length; ++index)
        {
          DynamicTransformUnitParam transformUnitParam = new DynamicTransformUnitParam();
          transformUnitParam.Deserialize(json.DynamicTransformUnit[index]);
          transformUnitParamList.Add(transformUnitParam);
        }
        this.mDynamicTransformUnitParam = transformUnitParamList;
      }
      if (json.ConvertUnitPieceExclude != null && json.ConvertUnitPieceExclude.Length > 0)
      {
        this.mConvertUnitPieceExcludeParam = new ConvertUnitPieceExcludeParam[json.ConvertUnitPieceExclude.Length];
        for (int index = 0; index < json.ConvertUnitPieceExclude.Length; ++index)
        {
          this.mConvertUnitPieceExcludeParam[index] = new ConvertUnitPieceExcludeParam();
          this.mConvertUnitPieceExcludeParam[index].Deserialize(json.ConvertUnitPieceExclude[index]);
        }
      }
      if (json.RecommendedArtifact != null && json.RecommendedArtifact.Length > 0)
      {
        this.mRecommendedArtifactList = new RecommendedArtifactList();
        this.mRecommendedArtifactList.Deserialize(json.RecommendedArtifact);
      }
      RaidMaster.Deserialize<RaidPeriodParam, JSON_RaidPeriodParam>(ref this.mRaidPeriodParam, json.RaidPeriod);
      RaidMaster.Deserialize<RaidAreaParam, JSON_RaidAreaParam>(ref this.mRaidAreaParam, json.RaidArea);
      RaidMaster.Deserialize<RaidBossParam, JSON_RaidBossParam>(ref this.mRaidBossParam, json.RaidBoss);
      RaidMaster.Deserialize<RaidBattleRewardParam, JSON_RaidBattleRewardParam>(ref this.mRaidBattleRewardParam, json.RaidBattleReward);
      RaidMaster.Deserialize<RaidBeatRewardParam, JSON_RaidBeatRewardParam>(ref this.mRaidBeatRewardParam, json.RaidBeatReward);
      RaidMaster.Deserialize<RaidDamageRatioRewardParam, JSON_RaidDamageRatioRewardParam>(ref this.mRaidDamageRatioRewardParam, json.RaidDamageRatioReward);
      RaidMaster.Deserialize<RaidAreaClearRewardParam, JSON_RaidAreaClearRewardParam>(ref this.mRaidAreaClearRewardParam, json.RaidAreaClearReward);
      RaidMaster.Deserialize<RaidCompleteRewardParam, JSON_RaidCompleteRewardParam>(ref this.mRaidCompleteRewardParam, json.RaidCompleteReward);
      RaidMaster.Deserialize<RaidRewardParam, JSON_RaidRewardParam>(ref this.mRaidRewardParam, json.RaidReward);
      if (json.SkillMotion != null)
      {
        List<SkillMotionParam> skillMotionParamList = new List<SkillMotionParam>(json.SkillMotion.Length);
        for (int index = 0; index < json.SkillMotion.Length; ++index)
        {
          SkillMotionParam skillMotionParam = new SkillMotionParam();
          skillMotionParam.Deserialize(json.SkillMotion[index]);
          skillMotionParamList.Add(skillMotionParam);
        }
        this.mSkillMotionParam = skillMotionParamList;
      }
      Dictionary<string, HighlightGift> dictionary3 = new Dictionary<string, HighlightGift>();
      if (json.HighlightGift != null)
      {
        foreach (JSON_HighlightGift json1 in json.HighlightGift)
        {
          HighlightGift highlightGift = new HighlightGift();
          highlightGift.Deserialize(json1);
          dictionary3[highlightGift.iname] = highlightGift;
        }
      }
      if (json.Highlight != null)
      {
        SRPG.HighlightParam[] highlightParamArray = new SRPG.HighlightParam[json.Highlight.Length];
        for (int index = 0; index < json.Highlight.Length; ++index)
        {
          SRPG.HighlightParam highlightParam = new SRPG.HighlightParam();
          highlightParam.Deserialze(json.Highlight[index]);
          HighlightGift highlightGift;
          if (dictionary3.TryGetValue(json.Highlight[index].gift, out highlightGift))
            highlightParam.gift = highlightGift;
          highlightParamArray[index] = highlightParam;
        }
        this.mHighlightParam = highlightParamArray;
      }
      this.mArenaRewardParams = new List<ArenaRewardParam>();
      ArenaRewardParam.Deserialize(ref this.mArenaRewardParams, json.ArenaRankResult);
      DependStateSpcEffParam.Deserialize(ref this.mDependStateSpcEffParam, json.DependStateSpcEff);
      if (json.InspirationSkill != null)
        InspSkillParam.Deserialize(json.InspirationSkill, json.InspSkillTrigger, ref this.mInspSkillDictionary);
      if (json.InspSkillLvUpCost != null)
        InspSkillLvUpCostParam.Desirialize(json.InspSkillLvUpCost, ref this.mInspSkillLvUpCostParam);
      if (json.InspSkillOpenCost != null)
        InspSkillCostParam.Deserialize(json.InspSkillOpenCost, ref this.mInspSkillOpenCostDictionary);
      if (json.InspSkillResetCost != null)
        InspSkillCostParam.Deserialize(json.InspSkillResetCost, ref this.mInspSkillResetCostDictionary);
      GenesisParam.Deserialize(ref this.mGenesisParam, json.Genesis);
      this.Loaded = true;
      return true;
    }

    public void CacheReferences()
    {
      for (int index = 0; index < this.mUnitParam.Count; ++index)
      {
        if (this.mUnitParam[index] != null)
          this.mUnitParam[index].CacheReferences(this);
      }
    }

    private void DebugLogError(string bufs)
    {
    }

    public void DumpLoadedLog()
    {
    }

    public UnitParam[] GetAllUnits()
    {
      if (this.mUnitParam != null)
        return this.mUnitParam.ToArray();
      return new UnitParam[0];
    }

    public bool ContainsUnitID(string key)
    {
      return this.mUnitDictionary.ContainsKey(key);
    }

    public UnitParam GetUnitParam(string key)
    {
      try
      {
        return this.mUnitDictionary[key];
      }
      catch (Exception ex)
      {
        throw new KeyNotFoundException<UnitParam>(key);
      }
    }

    public Dictionary<string, UnitJobOverwriteParam> GetUnitJobOverwriteParamsForUnit(string unit_iname)
    {
      if (string.IsNullOrEmpty(unit_iname))
      {
        DebugUtility.LogError("Unknown UnitJobOverwriteParam \"" + unit_iname + "\"");
        return (Dictionary<string, UnitJobOverwriteParam>) null;
      }
      Dictionary<string, UnitJobOverwriteParam> dictionary;
      this.mUnitJobOverwriteDictionary.TryGetValue(unit_iname, out dictionary);
      return dictionary;
    }

    public SkillParam GetSkillParam(string key)
    {
      try
      {
        return this.mSkillDictionary[key];
      }
      catch (Exception ex)
      {
        throw new KeyNotFoundException<SkillParam>(key);
      }
    }

    public BuffEffectParam GetBuffEffectParam(string key)
    {
      if (string.IsNullOrEmpty(key))
        return (BuffEffectParam) null;
      try
      {
        return this.mBuffEffectDictionary[key];
      }
      catch (Exception ex)
      {
        DebugUtility.LogError("Unknown BuffEffectParam \"" + key + "\"");
        return (BuffEffectParam) null;
      }
    }

    public CondEffectParam GetCondEffectParam(string key)
    {
      if (string.IsNullOrEmpty(key))
        return (CondEffectParam) null;
      try
      {
        return this.mCondEffectDictionary[key];
      }
      catch (Exception ex)
      {
        DebugUtility.LogError("Unknown CondEffectParam \"" + key + "\"");
        return (CondEffectParam) null;
      }
    }

    public AbilityParam GetAbilityParam(string key)
    {
      try
      {
        return this.mAbilityDictionary[key];
      }
      catch (Exception ex)
      {
        throw new KeyNotFoundException<AbilityParam>(key);
      }
    }

    public ItemParam GetItemParam(string key)
    {
      try
      {
        return this.mItemDictionary[key];
      }
      catch (Exception ex)
      {
        DebugUtility.LogError("Unknown ItemParam \"" + key + "\"");
        return (ItemParam) null;
      }
    }

    public ItemParam GetItemParam(string key, bool showLogError)
    {
      if (showLogError)
        return this.GetItemParam(key);
      if (!this.mItemDictionary.ContainsKey(key))
        return (ItemParam) null;
      return this.mItemDictionary[key];
    }

    public ArtifactParam GetArtifactParam(string key)
    {
      try
      {
        return this.mArtifactDictionary[key];
      }
      catch (Exception ex)
      {
        DebugUtility.LogError("Unknown ArtifactParam \"" + key + "\"");
        return (ArtifactParam) null;
      }
    }

    public ArtifactParam GetArtifactParam(string key, bool showLogError)
    {
      if (showLogError)
        return this.GetArtifactParam(key);
      if (!this.mArtifactDictionary.ContainsKey(key))
        return (ArtifactParam) null;
      return this.mArtifactDictionary[key];
    }

    public WeaponParam GetWeaponParam(string key)
    {
      if (string.IsNullOrEmpty(key))
        return (WeaponParam) null;
      WeaponParam weaponParam = this.mWeaponParam.Find((Predicate<WeaponParam>) (p => p.iname == key));
      if (weaponParam == null)
        DebugUtility.LogError("Unknown WeaponParam \"" + key + "\"");
      return weaponParam;
    }

    public RecipeParam GetRecipeParam(string key)
    {
      if (string.IsNullOrEmpty(key))
        return (RecipeParam) null;
      try
      {
        return this.mRecipeDictionary[key];
      }
      catch (Exception ex)
      {
        DebugUtility.LogError("Unknown mRecipeParam \"" + key + "\"");
        return (RecipeParam) null;
      }
    }

    public JobParam GetJobParam(string key)
    {
      try
      {
        return this.mJobParamDict[key];
      }
      catch (Exception ex)
      {
        throw new KeyNotFoundException<JobParam>(key);
      }
    }

    public JobParam[] GetAllJobs()
    {
      if (this.mJobParam != null)
        return this.mJobParam.ToArray();
      return new JobParam[0];
    }

    public AbilityParam[] GetAllAbilities()
    {
      if (this.mAbilityParam != null)
        return this.mAbilityParam.ToArray();
      return new AbilityParam[0];
    }

    public SkillParam[] GetAllSkills()
    {
      if (this.mSkillParam != null)
        return this.mSkillParam.ToArray();
      return new SkillParam[0];
    }

    public QuestClearUnlockUnitDataParam[] GetAllUnlockUnitDatas()
    {
      if (this.mUnlockUnitDataParam != null)
        return this.mUnlockUnitDataParam.ToArray();
      return new QuestClearUnlockUnitDataParam[0];
    }

    public QuestClearUnlockUnitDataParam GetUnlockUnitData(string key)
    {
      if (string.IsNullOrEmpty(key))
        return (QuestClearUnlockUnitDataParam) null;
      QuestClearUnlockUnitDataParam unlockUnitDataParam = this.mUnlockUnitDataParam.Find((Predicate<QuestClearUnlockUnitDataParam>) (p => p.iname == key));
      if (unlockUnitDataParam == null)
        throw new KeyNotFoundException<QuestClearUnlockUnitDataParam>(key);
      return unlockUnitDataParam;
    }

    public CollaboSkillParam GetCollaboSkillData(string unit_iname)
    {
      if (string.IsNullOrEmpty(unit_iname))
        return (CollaboSkillParam) null;
      if (this.mCollaboSkillParam == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>MasterParam/GetCollaboSkillData no data!</color>"));
        return (CollaboSkillParam) null;
      }
      CollaboSkillParam collaboSkillParam = this.mCollaboSkillParam.Find((Predicate<CollaboSkillParam>) (d => d.UnitIname == unit_iname));
      if (collaboSkillParam == null)
        DebugUtility.Log(string.Format("<color=yellow>MasterParam/GetCollaboSkillData data not found! unit_iname={0}</color>", (object) unit_iname));
      return collaboSkillParam;
    }

    public TrickParam GetTrickParam(string iname)
    {
      if (string.IsNullOrEmpty(iname))
        return (TrickParam) null;
      if (this.mTrickParam == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>MasterParam/GetTrickParam no data!</color>"));
        return (TrickParam) null;
      }
      TrickParam trickParam = this.mTrickParam.Find((Predicate<TrickParam>) (d => d.Iname == iname));
      if (trickParam == null)
        DebugUtility.Log(string.Format("<color=yellow>MasterParam/GetTrickParam data not found! iname={0}</color>", (object) iname));
      return trickParam;
    }

    public BreakObjParam GetBreakObjParam(string iname)
    {
      if (string.IsNullOrEmpty(iname))
        return (BreakObjParam) null;
      if (this.mBreakObjParam == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>MasterParam/GetBreakObjParam no data!</color>"));
        return (BreakObjParam) null;
      }
      BreakObjParam breakObjParam = this.mBreakObjParam.Find((Predicate<BreakObjParam>) (d => d.Iname == iname));
      if (breakObjParam == null)
        DebugUtility.Log(string.Format("<color=yellow>MasterParam/GetBreakObjParam data not found! iname={0}</color>", (object) iname));
      return breakObjParam;
    }

    public WeatherParam GetWeatherParam(string iname)
    {
      if (string.IsNullOrEmpty(iname))
        return (WeatherParam) null;
      if (this.mWeatherParam == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>MasterParam/GetWeatherParam no data!</color>"));
        return (WeatherParam) null;
      }
      WeatherParam weatherParam = this.mWeatherParam.Find((Predicate<WeatherParam>) (d => d.Iname == iname));
      if (weatherParam == null)
        DebugUtility.Log(string.Format("<color=yellow>MasterParam/GetWeatherParam data not found! iname={0}</color>", (object) iname));
      return weatherParam;
    }

    public DynamicTransformUnitParam GetDynamicTransformUnitParam(string iname)
    {
      if (string.IsNullOrEmpty(iname))
        return (DynamicTransformUnitParam) null;
      if (this.mDynamicTransformUnitParam == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>MasterParam/GetDynamicTransformUnitParam no data!</color>"));
        return (DynamicTransformUnitParam) null;
      }
      DynamicTransformUnitParam transformUnitParam = this.mDynamicTransformUnitParam.Find((Predicate<DynamicTransformUnitParam>) (d => d.Iname == iname));
      if (transformUnitParam == null)
        DebugUtility.Log(string.Format("<color=yellow>MasterParam/GetDynamicTransformUnitParam data not found! iname={0}</color>", (object) iname));
      return transformUnitParam;
    }

    public SkillMotionParam GetSkillMotionParam(string iname)
    {
      if (string.IsNullOrEmpty(iname))
        return (SkillMotionParam) null;
      if (this.mSkillMotionParam == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>MasterParam/GetSkillMotionParam no data!</color>"));
        return (SkillMotionParam) null;
      }
      SkillMotionParam skillMotionParam = this.mSkillMotionParam.Find((Predicate<SkillMotionParam>) (d => d.Iname == iname));
      if (skillMotionParam == null)
        DebugUtility.Log(string.Format("<color=yellow>MasterParam/GetSkillMotionParam data not found! iname={0}</color>", (object) iname));
      return skillMotionParam;
    }

    public DependStateSpcEffParam GetDependStateSpcEffParam(string iname)
    {
      if (string.IsNullOrEmpty(iname))
        return (DependStateSpcEffParam) null;
      if (this.mDependStateSpcEffParam == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>MasterParam/GetDependStateSpcEffParam no data!</color>"));
        return (DependStateSpcEffParam) null;
      }
      DependStateSpcEffParam stateSpcEffParam = this.mDependStateSpcEffParam.Find((Predicate<DependStateSpcEffParam>) (d => d.Iname == iname));
      if (stateSpcEffParam == null)
        DebugUtility.Log(string.Format("<color=yellow>MasterParam/GetDependStateSpcEffParam data not found! iname={0}</color>", (object) iname));
      return stateSpcEffParam;
    }

    public GenesisParam GetGenesisParam()
    {
      if (this.mGenesisParam == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>MasterParam/GetGenesisParam no data!</color>"));
        return (GenesisParam) null;
      }
      GenesisParam genesisParam = this.mGenesisParam.Find((Predicate<GenesisParam>) (d => d.IsValid));
      if (genesisParam == null)
        DebugUtility.Log(string.Format("<color=yellow>MasterParam/GetGenesisParam valid data not found!</color>"));
      return genesisParam;
    }

    public TobiraParam GetTobiraParam(string unit_iname, TobiraParam.Category category)
    {
      if (string.IsNullOrEmpty(unit_iname))
        return (TobiraParam) null;
      if (this.mTobiraParam != null)
        return this.mTobiraParam.Find((Predicate<TobiraParam>) (param =>
        {
          if (param.UnitIname == unit_iname)
            return param.TobiraCategory == category;
          return false;
        }));
      DebugUtility.Log(string.Format("<color=yellow>MasterParam/GetTobiraListForUnit no data!</color>"));
      return (TobiraParam) null;
    }

    public TobiraParam[] GetTobiraListForUnit(string unit_iname)
    {
      if (string.IsNullOrEmpty(unit_iname))
        return (TobiraParam[]) null;
      if (this.mTobiraParam == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>MasterParam/GetTobiraListForUnit no data!</color>"));
        return (TobiraParam[]) null;
      }
      TobiraParam[] tobira_list = new TobiraParam[TobiraParam.MAX_TOBIRA_COUNT];
      this.mTobiraParam.ForEach((Action<TobiraParam>) (param =>
      {
        if (!(param.UnitIname == unit_iname))
          return;
        tobira_list[(int) param.TobiraCategory] = param;
      }));
      return tobira_list;
    }

    public bool CanUnlockTobira(string unit_iname)
    {
      if (string.IsNullOrEmpty(unit_iname))
        return false;
      if (this.mTobiraCondParam == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>MasterParam/GetTobiraConditionsForUnit no data!</color>"));
        return false;
      }
      return this.mTobiraCondParam.Find((Predicate<TobiraCondsParam>) (param => !(param.UnitIname != unit_iname) && param.TobiraCategory == TobiraParam.Category.START)) != null;
    }

    public TobiraConditionParam[] GetTobiraConditionsForUnit(string unit_iname, TobiraParam.Category category)
    {
      Dictionary<TobiraParam.Category, TobiraConditionParam[]> conditionsForUnit = this.GetTobiraConditionsForUnit(unit_iname);
      if (conditionsForUnit == null)
        return (TobiraConditionParam[]) null;
      TobiraConditionParam[] tobiraConditionParamArray;
      conditionsForUnit.TryGetValue(category, out tobiraConditionParamArray);
      return tobiraConditionParamArray ?? (TobiraConditionParam[]) null;
    }

    public Dictionary<TobiraParam.Category, TobiraConditionParam[]> GetTobiraConditionsForUnit(string unit_iname)
    {
      if (string.IsNullOrEmpty(unit_iname))
        return (Dictionary<TobiraParam.Category, TobiraConditionParam[]>) null;
      if (this.mTobiraCondParam == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>MasterParam/GetTobiraConditionsForUnit no data!</color>"));
        return (Dictionary<TobiraParam.Category, TobiraConditionParam[]>) null;
      }
      Dictionary<TobiraParam.Category, TobiraConditionParam[]> condition_list = new Dictionary<TobiraParam.Category, TobiraConditionParam[]>();
      this.mTobiraCondParam.ForEach((Action<TobiraCondsParam>) (param =>
      {
        if (!(param.UnitIname == unit_iname))
          return;
        condition_list.Add(param.TobiraCategory, param.Conditions);
        Array.ForEach<TobiraConditionParam>(param.Conditions, (Action<TobiraConditionParam>) (tcp =>
        {
          if (tcp.CondType != TobiraConditionParam.ConditionType.Unit)
            return;
          TobiraCondsUnitParam cond_unit;
          this.mTobiraCondUnitParam.TryGetValue(tcp.CondIname, out cond_unit);
          if (cond_unit == null)
            return;
          tcp.SetCondUnit(cond_unit);
        }));
      }));
      return condition_list;
    }

    public TobiraRecipeParam GetTobiraRecipe(string unit_iname, TobiraParam.Category category, int level)
    {
      TobiraParam tobiraParam = this.GetTobiraParam(unit_iname, category);
      if (tobiraParam == null)
        return (TobiraRecipeParam) null;
      return this.GetTobiraRecipe(tobiraParam.RecipeId, level);
    }

    public TobiraRecipeParam GetTobiraRecipe(string recipe_iname, int level)
    {
      return this.mTobiraRecipeParam.Find((Predicate<TobiraRecipeParam>) (param =>
      {
        if (param.RecipeIname == recipe_iname)
          return param.Level == level;
        return false;
      }));
    }

    public RecipeParam[] GetAllRecipes()
    {
      if (this.mRecipeParam != null)
        return this.mRecipeParam.ToArray();
      return new RecipeParam[0];
    }

    public JobSetParam GetJobSetParam(string key)
    {
      if (string.IsNullOrEmpty(key))
        return (JobSetParam) null;
      try
      {
        return this.mJobsetDictionary[key];
      }
      catch (Exception ex)
      {
        DebugUtility.LogError("Unknown mJobSetParam \"" + key + "\"");
        return (JobSetParam) null;
      }
    }

    public JobSetParam[] GetClassChangeJobSetParam(string key)
    {
      if (string.IsNullOrEmpty(key))
        return (JobSetParam[]) null;
      try
      {
        return this.mJobsetTargetDictionary[key].ToArray();
      }
      catch
      {
        return (JobSetParam[]) null;
      }
    }

    public GrowParam GetGrowParam(string key)
    {
      if (string.IsNullOrEmpty(key))
        return (GrowParam) null;
      GrowParam growParam = this.mGrowParam.Find((Predicate<GrowParam>) (p => p.type == key));
      if (growParam == null)
        DebugUtility.LogError("Unknown GrowParam \"" + key + "\"");
      return growParam;
    }

    public AIParam GetAIParam(string key)
    {
      if (string.IsNullOrEmpty(key))
        return (AIParam) null;
      AIParam aiParam = this.mAIParam.Find((Predicate<AIParam>) (p => p.iname == key));
      if (aiParam == null)
        DebugUtility.LogError("Failed AIParam iname \"" + key + "\" not found.");
      return aiParam;
    }

    public GeoParam GetGeoParam(string key)
    {
      if (this.mGeoParam == null)
        return (GeoParam) null;
      if (string.IsNullOrEmpty(key))
        return (GeoParam) null;
      GeoParam geoParam = this.mGeoParam.Find((Predicate<GeoParam>) (p => p.iname == key));
      if (geoParam == null)
        DebugUtility.LogError("Failed GeoParam iname \"" + key + "\" not found.");
      return geoParam;
    }

    public RarityParam GetRarityParam(int rarity)
    {
      if (this.mRarityParam != null && (rarity >= 0 || rarity < this.mRarityParam.Count))
        return this.mRarityParam[rarity];
      DebugUtility.LogError("mRarityParam Stack Overflow.");
      return (RarityParam) null;
    }

    public ShopParam GetShopParam(EShopType type)
    {
      int index1 = (int) type;
      switch (type)
      {
        case EShopType.Event:
          index1 = -1;
          for (int index2 = 0; index2 < this.mShopParam.Count; ++index2)
          {
            string[] strArray = GlobalVars.EventShopItem.shops.gname.Split('-');
            if (this.mShopParam[index2].iname.Equals(strArray[0]))
            {
              index1 = index2;
              break;
            }
          }
          if (index1 < 0)
          {
            DebugUtility.LogError("mShopParam Data Error. Not found: " + (object) GlobalVars.EventShopItem.shops.gname.Split('-'));
            return (ShopParam) null;
          }
          break;
        case EShopType.Limited:
          index1 = -1;
          for (int index2 = 0; index2 < this.mShopParam.Count; ++index2)
          {
            string[] strArray = GlobalVars.LimitedShopItem.shops.gname.Split('-');
            if (this.mShopParam[index2].iname.Equals(strArray[0]))
            {
              index1 = index2;
              break;
            }
          }
          if (index1 < 0)
          {
            DebugUtility.LogError("mShopParam Data Error. Not found: " + (object) GlobalVars.LimitedShopItem.shops.gname.Split('-'));
            return (ShopParam) null;
          }
          break;
        case EShopType.Guerrilla:
          index1 = -1;
          for (int index2 = 0; index2 < this.mShopParam.Count; ++index2)
          {
            if (this.mShopParam[index2].iname.Equals("Guerrilla"))
            {
              index1 = index2;
              break;
            }
          }
          break;
      }
      if (this.mShopParam != null && index1 >= 0 && index1 < this.mShopParam.Count)
        return this.mShopParam[index1];
      DebugUtility.LogError("mShopParam Stack Overflow.");
      return (ShopParam) null;
    }

    public int GetShopType(string iname)
    {
      int index = this.mShopParam.FindIndex((Predicate<ShopParam>) (p => p.iname == iname));
      if (index < 0)
        DebugUtility.LogError("Failed GetShopParam iname \"" + iname + "\" not found.");
      return index;
    }

    public PlayerParam GetPlayerParam(int lv)
    {
      if (lv > 0 && lv <= this.GetPlayerLevelCap())
        return this.mPlayerParamTbl[lv - 1];
      return (PlayerParam) null;
    }

    public int GetAbilityNextGold(int rank)
    {
      DebugUtility.Assert(rank > 0 && rank <= this.mAbilityExpTbl.Length, "指定ランク" + (object) rank + "がアビリティのランク範囲に存在しない。");
      return (int) this.mAbilityExpTbl[rank];
    }

    public int GetAwakeNeedPieces(int awakeLv)
    {
      DebugUtility.Assert(awakeLv >= 0 && awakeLv < this.mAwakePieceTbl.Length, "覚醒回数" + (object) awakeLv + "が覚醒可能な範囲に存在しない。");
      return (int) this.mAwakePieceTbl[awakeLv];
    }

    public int GetUnitNextExp(int lv)
    {
      DebugUtility.Assert(lv > 0 && lv <= this.mUnitExpTbl.Length, "指定レベル" + (object) lv + "がユニットのレベル範囲に存在しない。");
      return (int) this.mUnitExpTbl[lv - 1];
    }

    public int GetUnitLevelExp(int lv)
    {
      DebugUtility.Assert(lv > 0 && lv <= this.mUnitExpTbl.Length, "指定レベル" + (object) lv + "がユニットのレベル範囲に存在しない。");
      int num = 0;
      for (int index = 0; index < lv; ++index)
        num += (int) this.mUnitExpTbl[index];
      return num;
    }

    public int CalcUnitLevel(int totalExp, int levelCap)
    {
      int val2 = levelCap;
      int num = 0;
      int val1 = 0;
      for (int index = 0; index < val2; ++index)
      {
        num += this.GetUnitNextExp(index + 1);
        if (num <= totalExp)
          ++val1;
        else
          break;
      }
      return Math.Min(Math.Max(val1, 1), val2);
    }

    public int GetUnitMaxLevel()
    {
      return this.mUnitExpTbl.Length;
    }

    public int GetPlayerNextExp(int lv)
    {
      DebugUtility.Assert(lv > 0 && lv <= this.mPlayerExpTbl.Length, "指定レベル" + (object) lv + "がプレイヤーのレベル範囲に存在しない。");
      return (int) this.mPlayerExpTbl[lv - 1];
    }

    public int GetPlayerLevelExp(int lv)
    {
      DebugUtility.Assert(lv > 0 && lv <= this.mPlayerExpTbl.Length, "指定レベル" + (object) lv + "がプレイヤーのレベル範囲に存在しない。");
      int num = 0;
      for (int index = 0; index < lv; ++index)
        num += (int) this.mPlayerExpTbl[index];
      return num;
    }

    public int GetPlayerLevelCap()
    {
      return this.mPlayerExpTbl.Length;
    }

    public int GetVipArenaResetCount(int rank)
    {
      DebugUtility.Assert(rank >= 0 && rank < this.mVip.Length, "指定VIPランク" + (object) rank + "がVIPランクの範囲に存在しない。");
      return this.mVip[rank].ResetArenaNum;
    }

    public int GetVipRankNextPoint(int rank)
    {
      DebugUtility.Assert(rank >= 0 && rank < this.mVip.Length, "指定VIPランク" + (object) rank + "がVIPランクの範囲に存在しない。");
      return this.mVip[rank].NextRankNeedPoint;
    }

    public int GetVipRankTotalNeedPoint(int rank)
    {
      DebugUtility.Assert(rank >= 0 && rank < this.mVip.Length, "指定VIPランク" + (object) rank + "がVIPランクの範囲に存在しない。");
      int num = 0;
      for (int index = 0; index < rank; ++index)
        num += this.mVip[index].NextRankNeedPoint;
      return num;
    }

    public int GetVipBuyStaminaLimit(int rank)
    {
      DebugUtility.Assert(rank >= 0 && rank < this.mVip.Length, "指定VIPランク" + (object) rank + "がVIPランクの範囲に存在しない。");
      return this.mVip[rank].BuyStaminaNum;
    }

    public int GetVipBuyGoldLimit(int rank)
    {
      DebugUtility.Assert(rank >= 0 && rank < this.mVip.Length, "指定VIPランク" + (object) rank + "がVIPランクの範囲に存在しない。");
      return this.mVip[rank].BuyCoinNum;
    }

    public int GetVipRankCap()
    {
      if (this.mVip == null)
        return 0;
      return Math.Max(this.mVip.Length - 1, 0);
    }

    public TrophyCategoryParam[] TrophyCategories
    {
      get
      {
        return this.mTrophyCategory;
      }
    }

    public ChallengeCategoryParam[] ChallengeCategories
    {
      get
      {
        return this.mChallengeCategory;
      }
    }

    public TrophyParam[] Trophies
    {
      get
      {
        return this.mTrophy;
      }
    }

    public TrophyObjective[] GetTrophiesOfType(TrophyConditionTypes type)
    {
      if (this.mTrophyDict == null)
        return new TrophyObjective[0];
      return this.mTrophyDict[(int) type];
    }

    public TrophyParam GetTrophy(string iname)
    {
      if (this.mTrophy == null)
        return (TrophyParam) null;
      TrophyParam trophyParam;
      if (this.mTrophyInameDict.TryGetValue(iname, out trophyParam))
        return trophyParam;
      return (TrophyParam) null;
    }

    public UnlockParam[] Unlocks
    {
      get
      {
        return this.mUnlock;
      }
    }

    public UnlockParam GetUnlockParam(string iname)
    {
      for (int index = this.mUnlock.Length - 1; index >= 0; --index)
      {
        if (this.mUnlock[index].iname == iname)
          return this.mUnlock[index];
      }
      return (UnlockParam) null;
    }

    public UnlockParam FindUnlockParam(UnlockTargets value)
    {
      for (int index = this.mUnlock.Length - 1; index >= 0; --index)
      {
        if (this.mUnlock[index].UnlockTarget == value)
          return this.mUnlock[index];
      }
      return (UnlockParam) null;
    }

    public UnitParam GetUnitParamForPiece(string key, bool doCheck = true)
    {
      if (string.IsNullOrEmpty(key))
        return (UnitParam) null;
      if (key == (string) this.FixParam.CommonPieceAll || key == (string) this.FixParam.CommonPieceDark || (key == (string) this.FixParam.CommonPieceFire || key == (string) this.FixParam.CommonPieceShine) || (key == (string) this.FixParam.CommonPieceThunder || key == (string) this.FixParam.CommonPieceWater || key == (string) this.FixParam.CommonPieceWind))
        return (UnitParam) null;
      UnitParam unitParam = this.mUnitParam.Find((Predicate<UnitParam>) (p => p.piece == key));
      if (doCheck && unitParam == null)
        DebugUtility.LogError("Failed UnitParam iname \"" + key + "\" not found.");
      return unitParam;
    }

    public OInt[] GetArtifactExpTable()
    {
      return this.mArtifactExpTbl;
    }

    public string TranslateMoviePath(string path)
    {
      if (this.mStreamingMovies == null)
        return path;
      for (int index = 0; index < this.mStreamingMovies.Length; ++index)
      {
        if (this.mStreamingMovies[index].alias == path)
          return this.mStreamingMovies[index].path;
      }
      return path;
    }

    public ArtifactParam GetSkinParamFromItemId(string itemId)
    {
      return Array.Find<ArtifactParam>(this.mArtifactParam.ToArray(), (Predicate<ArtifactParam>) (s => s.kakera == itemId));
    }

    public bool IsSkinItem(string itemId)
    {
      return this.GetSkinParamFromItemId(itemId) != null;
    }

    public BannerParam[] Banners
    {
      get
      {
        return this.mBanner;
      }
    }

    public bool ContainsAwardID(string key)
    {
      return this.mAwardDictionary.ContainsKey(key);
    }

    public AwardParam GetAwardParam(string key)
    {
      try
      {
        return this.mAwardDictionary[key];
      }
      catch (Exception ex)
      {
        DebugUtility.LogError("Unknown AwardParam \"" + key + "\"");
        return (AwardParam) null;
      }
    }

    public AwardParam[] GetAllAwards()
    {
      if (this.mAwardParam != null)
        return this.mAwardParam.ToArray();
      return new AwardParam[0];
    }

    public LoginInfoParam[] GetAllLoginInfos()
    {
      if (this.mLoginInfoParam != null)
        return this.mLoginInfoParam;
      return new LoginInfoParam[0];
    }

    public LoginInfoParam[] GetActiveLoginInfos()
    {
      if (this.mLoginInfoParam == null)
        return (LoginInfoParam[]) null;
      List<LoginInfoParam> loginInfoParamList = new List<LoginInfoParam>();
      int player_level = MonoSingleton<GameManager>.Instance.Player.CalcLevel();
      bool is_beginner = MonoSingleton<GameManager>.Instance.Player.IsBeginner();
      for (int index = 0; index < this.mLoginInfoParam.Length; ++index)
      {
        if (this.mLoginInfoParam[index].IsDisplayable(TimeManager.ServerTime, player_level, is_beginner))
          loginInfoParamList.Add(this.mLoginInfoParam[index]);
      }
      return loginInfoParamList.ToArray();
    }

    public VersusMatchingParam[] GetVersusMatchingParam()
    {
      return this.mVersusMatching.ToArray();
    }

    public VersusMatchCondParam[] GetVersusMatchingCondition()
    {
      return this.mVersusMatchCond.ToArray();
    }

    public OInt[] TowerRank
    {
      get
      {
        return this.mTowerRankTbl;
      }
    }

    public OInt[] GetMultiPlayLimitUnitLv()
    {
      return this.mMultiLimitUnitLv;
    }

    public bool Deserialize2(JSON_MasterParam json)
    {
      if (this.Loaded)
        return true;
      DebugUtility.Verify((object) json, typeof (JSON_MasterParam));
      this.mLocalNotificationParam = (LocalNotificationParam) null;
      this.mFixParam.Deserialize(json.Fix[0]);
      if (json.Unit != null)
      {
        if (this.mUnitParam == null)
          this.mUnitParam = new List<UnitParam>(json.Unit.Length);
        if (this.mUnitDictionary == null)
          this.mUnitDictionary = new Dictionary<string, UnitParam>(json.Unit.Length);
        for (int index = 0; index < json.Unit.Length; ++index)
        {
          JSON_UnitParam json1 = json.Unit[index];
          UnitParam unitParam = new UnitParam();
          this.mUnitParam.Add(unitParam);
          unitParam.Deserialize(json1);
          this.mUnitDictionary.Add(json1.iname, unitParam);
        }
      }
      if (json.UnitJobOverwrite != null)
      {
        if (this.mUnitJobOverwriteParam == null)
          this.mUnitJobOverwriteParam = new List<UnitJobOverwriteParam>();
        if (this.mUnitJobOverwriteDictionary == null)
          this.mUnitJobOverwriteDictionary = new Dictionary<string, Dictionary<string, UnitJobOverwriteParam>>();
        foreach (JSON_UnitJobOverwriteParam json1 in json.UnitJobOverwrite)
        {
          UnitJobOverwriteParam jobOverwriteParam = new UnitJobOverwriteParam();
          this.mUnitJobOverwriteParam.Add(jobOverwriteParam);
          jobOverwriteParam.Deserialize(json1);
          Dictionary<string, UnitJobOverwriteParam> dictionary;
          this.mUnitJobOverwriteDictionary.TryGetValue(json1.unit_iname, out dictionary);
          if (dictionary == null)
          {
            dictionary = new Dictionary<string, UnitJobOverwriteParam>();
            this.mUnitJobOverwriteDictionary.Add(json1.unit_iname, dictionary);
          }
          if (!dictionary.ContainsKey(json1.job_iname))
            dictionary.Add(json1.job_iname, jobOverwriteParam);
        }
      }
      if (json.Skill != null)
      {
        if (this.mSkillParam == null)
          this.mSkillParam = new List<SkillParam>(json.Skill.Length);
        if (this.mSkillDictionary == null)
          this.mSkillDictionary = new Dictionary<string, SkillParam>(json.Skill.Length);
        for (int index = 0; index < json.Skill.Length; ++index)
        {
          JSON_SkillParam json1 = json.Skill[index];
          SkillParam skillParam = new SkillParam();
          this.mSkillParam.Add(skillParam);
          skillParam.Deserialize(json1);
          this.mSkillDictionary.Add(json1.iname, skillParam);
        }
        SkillParam.UpdateReplaceSkill(this.mSkillParam);
      }
      if (json.Buff != null)
      {
        this.mBuffEffectDictionary = new Dictionary<string, BuffEffectParam>(json.Buff.Length);
        for (int index = 0; index < json.Buff.Length; ++index)
        {
          JSON_BuffEffectParam json1 = json.Buff[index];
          BuffEffectParam buffEffectParam = new BuffEffectParam();
          buffEffectParam.Deserialize(json1);
          if (!this.mBuffEffectDictionary.ContainsKey(json1.iname))
            this.mBuffEffectDictionary.Add(json1.iname, buffEffectParam);
        }
      }
      if (json.Cond != null)
      {
        this.mCondEffectDictionary = new Dictionary<string, CondEffectParam>(json.Cond.Length);
        for (int index = 0; index < json.Cond.Length; ++index)
        {
          JSON_CondEffectParam json1 = json.Cond[index];
          CondEffectParam condEffectParam = new CondEffectParam();
          condEffectParam.Deserialize(json1);
          if (!this.mCondEffectDictionary.ContainsKey(json1.iname))
            this.mCondEffectDictionary.Add(json1.iname, condEffectParam);
        }
      }
      if (json.Ability != null)
      {
        if (this.mAbilityParam == null)
          this.mAbilityParam = new List<AbilityParam>(json.Ability.Length);
        if (this.mAbilityDictionary == null)
          this.mAbilityDictionary = new Dictionary<string, AbilityParam>(json.Ability.Length);
        for (int index = 0; index < json.Ability.Length; ++index)
        {
          JSON_AbilityParam json1 = json.Ability[index];
          AbilityParam abilityParam = new AbilityParam();
          this.mAbilityParam.Add(abilityParam);
          abilityParam.Deserialize(json1);
          this.mAbilityDictionary.Add(json1.iname, abilityParam);
        }
      }
      if (json.Item != null)
      {
        if (this.mItemParam == null)
          this.mItemParam = new List<ItemParam>(json.Item.Length);
        if (this.mItemDictionary == null)
          this.mItemDictionary = new Dictionary<string, ItemParam>(json.Item.Length);
        for (int index = 0; index < json.Item.Length; ++index)
        {
          JSON_ItemParam json1 = json.Item[index];
          ItemParam itemParam = new ItemParam();
          this.mItemParam.Add(itemParam);
          itemParam.Deserialize(json1);
          itemParam.no = index + 1;
          this.mItemDictionary.Add(json1.iname, itemParam);
        }
        this.AddUnitToItem();
      }
      if (json.Artifact != null)
      {
        if (this.mArtifactParam == null)
          this.mArtifactParam = new List<ArtifactParam>(json.Artifact.Length);
        if (this.mArtifactDictionary == null)
          this.mArtifactDictionary = new Dictionary<string, ArtifactParam>(json.Artifact.Length);
        for (int index = 0; index < json.Artifact.Length; ++index)
        {
          JSON_ArtifactParam json1 = json.Artifact[index];
          if (json1.iname != null)
          {
            ArtifactParam artifactParam = new ArtifactParam();
            this.mArtifactParam.Add(artifactParam);
            artifactParam.Deserialize(json1);
            this.mArtifactDictionary.Add(json1.iname, artifactParam);
          }
        }
      }
      if (json.Weapon != null)
      {
        if (this.mWeaponParam == null)
          this.mWeaponParam = new List<WeaponParam>(json.Weapon.Length);
        for (int index = 0; index < json.Weapon.Length; ++index)
        {
          JSON_WeaponParam json1 = json.Weapon[index];
          WeaponParam weaponParam = new WeaponParam();
          this.mWeaponParam.Add(weaponParam);
          weaponParam.Deserialize(json1);
        }
      }
      if (json.Recipe != null)
      {
        if (this.mRecipeParam == null)
          this.mRecipeParam = new List<RecipeParam>(json.Recipe.Length);
        this.mRecipeDictionary = new Dictionary<string, RecipeParam>(json.Recipe.Length);
        for (int index = 0; index < json.Recipe.Length; ++index)
        {
          JSON_RecipeParam json1 = json.Recipe[index];
          RecipeParam recipeParam = new RecipeParam();
          this.mRecipeParam.Add(recipeParam);
          recipeParam.Deserialize(json1);
          if (!this.mRecipeDictionary.ContainsKey(json1.iname))
            this.mRecipeDictionary.Add(json1.iname, recipeParam);
        }
      }
      if (json.Job != null)
      {
        if (this.mJobParam == null)
          this.mJobParam = new List<JobParam>(json.Job.Length);
        for (int index = 0; index < json.Job.Length; ++index)
        {
          JSON_JobParam json1 = json.Job[index];
          JobParam jobParam = new JobParam();
          this.mJobParam.Add(jobParam);
          this.mJobParamDict[json1.iname] = jobParam;
          jobParam.Deserialize(json1, this);
        }
      }
      if (json.JobSet != null)
      {
        if (this.mJobSetParam == null)
          this.mJobSetParam = new List<JobSetParam>(json.JobSet.Length);
        if (this.mJobsetDictionary == null)
          this.mJobsetDictionary = new Dictionary<string, JobSetParam>(json.JobSet.Length);
        if (this.mJobsetTargetDictionary == null)
          this.mJobsetTargetDictionary = new Dictionary<string, List<JobSetParam>>(json.Unit.Length);
        for (int index = 0; index < json.JobSet.Length; ++index)
        {
          JSON_JobSetParam job = json.JobSet[index];
          JobSetParam jobSetParam = new JobSetParam();
          this.mJobSetParam.Add(jobSetParam);
          jobSetParam.Deserialize(job);
          if (!this.mJobsetDictionary.ContainsKey(job.iname))
            this.mJobsetDictionary.Add(job.iname, jobSetParam);
          if (!string.IsNullOrEmpty(jobSetParam.target_unit))
          {
            List<JobSetParam> jobSetParamList;
            if (this.mJobsetTargetDictionary.ContainsKey(jobSetParam.target_unit))
            {
              jobSetParamList = this.mJobsetTargetDictionary[jobSetParam.target_unit];
            }
            else
            {
              jobSetParamList = new List<JobSetParam>(3);
              this.mJobsetTargetDictionary.Add(jobSetParam.target_unit, jobSetParamList);
            }
            jobSetParamList.Add(jobSetParam);
          }
        }
      }
      if (json.Grow != null)
      {
        if (this.mGrowParam == null)
          this.mGrowParam = new List<GrowParam>(json.Grow.Length);
        for (int index = 0; index < json.Grow.Length; ++index)
        {
          JSON_GrowParam json1 = json.Grow[index];
          GrowParam growParam = new GrowParam();
          this.mGrowParam.Add(growParam);
          growParam.Deserialize(json1);
        }
      }
      if (json.AI != null)
      {
        if (this.mAIParam == null)
          this.mAIParam = new List<AIParam>(json.AI.Length);
        for (int index = 0; index < json.AI.Length; ++index)
        {
          JSON_AIParam json1 = json.AI[index];
          AIParam aiParam = new AIParam();
          this.mAIParam.Add(aiParam);
          aiParam.Deserialize(json1);
        }
      }
      if (json.Geo != null)
      {
        if (this.mGeoParam == null)
          this.mGeoParam = new List<GeoParam>(json.Geo.Length);
        for (int index = 0; index < json.Geo.Length; ++index)
        {
          JSON_GeoParam json1 = json.Geo[index];
          GeoParam geoParam = new GeoParam();
          this.mGeoParam.Add(geoParam);
          geoParam.Deserialize(json1);
        }
      }
      if (json.Rarity != null)
      {
        if (this.mRarityParam == null)
          this.mRarityParam = new List<RarityParam>(json.Rarity.Length);
        for (int index = 0; index < json.Rarity.Length; ++index)
        {
          RarityParam rarityParam = new RarityParam();
          this.mRarityParam.Add(rarityParam);
          rarityParam.Deserialize(json.Rarity[index]);
        }
      }
      if (json.Shop != null)
      {
        if (this.mShopParam == null)
          this.mShopParam = new List<ShopParam>(json.Shop.Length);
        for (int index = 0; index < json.Shop.Length; ++index)
        {
          ShopParam shopParam = new ShopParam();
          this.mShopParam.Add(shopParam);
          shopParam.Deserialize(json.Shop[index]);
        }
      }
      if (json.Player != null)
      {
        this.mPlayerParamTbl = new PlayerParam[json.Player.Length];
        for (int index = 0; index < json.Player.Length; ++index)
        {
          JSON_PlayerParam json1 = json.Player[index];
          this.mPlayerParamTbl[index] = new PlayerParam();
          this.mPlayerParamTbl[index].Deserialize(json1);
        }
      }
      if (json.PlayerLvTbl != null)
      {
        this.mPlayerExpTbl = new OInt[json.PlayerLvTbl.Length];
        for (int index = 0; index < json.PlayerLvTbl.Length; ++index)
          this.mPlayerExpTbl[index] = (OInt) json.PlayerLvTbl[index];
      }
      if (json.UnitLvTbl != null)
      {
        this.mUnitExpTbl = new OInt[json.UnitLvTbl.Length];
        for (int index = 0; index < json.UnitLvTbl.Length; ++index)
          this.mUnitExpTbl[index] = (OInt) json.UnitLvTbl[index];
      }
      if (json.ArtifactLvTbl != null)
      {
        this.mArtifactExpTbl = new OInt[json.ArtifactLvTbl.Length];
        for (int index = 0; index < json.ArtifactLvTbl.Length; ++index)
          this.mArtifactExpTbl[index] = (OInt) json.ArtifactLvTbl[index];
      }
      if (json.AbilityRank != null)
      {
        this.mAbilityExpTbl = new OInt[json.AbilityRank.Length];
        for (int index = 0; index < json.AbilityRank.Length; ++index)
          this.mAbilityExpTbl[index] = (OInt) json.AbilityRank[index];
      }
      if (json.AwakePieceTbl != null)
      {
        this.mAwakePieceTbl = new OInt[json.AwakePieceTbl.Length];
        for (int index = 0; index < json.AwakePieceTbl.Length; ++index)
          this.mAwakePieceTbl[index] = (OInt) json.AwakePieceTbl[index];
      }
      this.mLocalNotificationParam = new LocalNotificationParam();
      if (json.LocalNotification != null)
      {
        this.mLocalNotificationParam.msg_stamina = json.LocalNotification[0].msg_stamina;
        this.mLocalNotificationParam.iOSAct_stamina = json.LocalNotification[0].iOSAct_stamina;
        this.mLocalNotificationParam.limitSec_stamina = json.LocalNotification[0].limitSec_stamina;
      }
      Dictionary<int, TrophyCategoryParam> dictionary1 = new Dictionary<int, TrophyCategoryParam>();
      if (json.TrophyCategory != null)
      {
        List<TrophyCategoryParam> trophyCategoryParamList = new List<TrophyCategoryParam>(json.TrophyCategory.Length);
        for (int index = 0; index < json.TrophyCategory.Length; ++index)
        {
          TrophyCategoryParam trophyCategoryParam = new TrophyCategoryParam();
          if (trophyCategoryParam.Deserialize(json.TrophyCategory[index]))
          {
            trophyCategoryParamList.Add(trophyCategoryParam);
            if (!dictionary1.ContainsKey(trophyCategoryParam.hash_code))
              dictionary1.Add(trophyCategoryParam.hash_code, trophyCategoryParam);
          }
        }
        this.mTrophyCategory = trophyCategoryParamList.ToArray();
      }
      if (json.Trophy != null && dictionary1.Count > 0)
      {
        List<TrophyParam> trophyParamList = new List<TrophyParam>(json.Trophy.Length);
        for (int index = 0; index < json.Trophy.Length; ++index)
        {
          TrophyParam trophyParam = new TrophyParam();
          if (trophyParam.Deserialize(json.Trophy[index]))
          {
            if (dictionary1.ContainsKey(trophyParam.category_hash_code))
              trophyParam.CategoryParam = dictionary1[trophyParam.category_hash_code];
            trophyParamList.Add(trophyParam);
          }
        }
        this.mTrophy = trophyParamList.ToArray();
        this.mTrophyInameDict = new Dictionary<string, TrophyParam>();
        foreach (TrophyParam trophyParam in this.mTrophy)
          this.mTrophyInameDict.Add(trophyParam.iname, trophyParam);
      }
      Dictionary<string, ChallengeCategoryParam> dictionary2 = new Dictionary<string, ChallengeCategoryParam>();
      if (json.ChallengeCategory != null)
      {
        List<ChallengeCategoryParam> challengeCategoryParamList = new List<ChallengeCategoryParam>(json.ChallengeCategory.Length);
        for (int index = 0; index < json.ChallengeCategory.Length; ++index)
        {
          ChallengeCategoryParam challengeCategoryParam = new ChallengeCategoryParam();
          if (challengeCategoryParam.Deserialize(json.ChallengeCategory[index]))
          {
            dictionary2[challengeCategoryParam.iname] = challengeCategoryParam;
            challengeCategoryParamList.Add(challengeCategoryParam);
          }
        }
        this.mChallengeCategory = challengeCategoryParamList.ToArray();
      }
      if (json.Challenge != null)
      {
        List<TrophyParam> trophyParamList = new List<TrophyParam>(json.Challenge.Length);
        for (int index = 0; index < json.Challenge.Length; ++index)
        {
          TrophyParam trophyParam = new TrophyParam();
          if (trophyParam.Deserialize(json.Challenge[index]))
          {
            if (dictionary2.ContainsKey(trophyParam.Category))
              trophyParam.ChallengeCategoryParam = dictionary2[trophyParam.Category];
            trophyParam.Challenge = 1;
            trophyParamList.Add(trophyParam);
          }
        }
        this.mChallenge = trophyParamList.ToArray();
        int length = this.mTrophy.Length;
        Array.Resize<TrophyParam>(ref this.mTrophy, length + this.mChallenge.Length);
        Array.Copy((Array) this.mChallenge, 0, (Array) this.mTrophy, length, this.mChallenge.Length);
        foreach (TrophyParam trophyParam in this.mChallenge)
          this.mTrophyInameDict.Add(trophyParam.iname, trophyParam);
      }
      this.CreateTrophyDict();
      if (json.Unlock != null)
      {
        List<UnlockParam> unlockParamList = new List<UnlockParam>(json.Unlock.Length);
        for (int index = 0; index < json.Unlock.Length; ++index)
        {
          UnlockParam unlockParam = new UnlockParam();
          if (unlockParam.Deserialize(json.Unlock[index]))
            unlockParamList.Add(unlockParam);
        }
        this.mUnlock = unlockParamList.ToArray();
      }
      if (json.Vip != null)
      {
        List<VipParam> vipParamList = new List<VipParam>(json.Vip.Length);
        for (int index = 0; index < json.Vip.Length; ++index)
        {
          VipParam vipParam = new VipParam();
          if (vipParam.Deserialize(json.Vip[index]))
            vipParamList.Add(vipParam);
        }
        this.mVip = vipParamList.ToArray();
      }
      if (json.Premium != null)
      {
        List<PremiumParam> premiumParamList = new List<PremiumParam>(json.Premium.Length);
        for (int index = 0; index < json.Premium.Length; ++index)
        {
          PremiumParam premiumParam = new PremiumParam();
          if (premiumParam.Deserialize(json.Premium[index]))
            premiumParamList.Add(premiumParam);
        }
        this.mPremium = premiumParamList.ToArray();
      }
      if (json.Mov != null)
      {
        this.mStreamingMovies = new JSON_StreamingMovie[json.Mov.Length];
        for (int index = 0; index < json.Mov.Length; ++index)
        {
          this.mStreamingMovies[index] = new JSON_StreamingMovie();
          this.mStreamingMovies[index].alias = json.Mov[index].alias;
          this.mStreamingMovies[index].path = json.Mov[index].path;
        }
      }
      if (json.Banner != null)
      {
        List<BannerParam> bannerParamList = new List<BannerParam>(json.Banner.Length);
        for (int index = 0; index < json.Banner.Length; ++index)
        {
          BannerParam bannerParam = new BannerParam();
          if (bannerParam.Deserialize(json.Banner[index]))
            bannerParamList.Add(bannerParam);
        }
        this.mBanner = bannerParamList.ToArray();
      }
      if (json.QuestClearUnlockUnitData != null)
      {
        List<QuestClearUnlockUnitDataParam> unlockUnitDataParamList = new List<QuestClearUnlockUnitDataParam>(json.QuestClearUnlockUnitData.Length);
        for (int index = 0; index < json.QuestClearUnlockUnitData.Length; ++index)
        {
          QuestClearUnlockUnitDataParam unlockUnitDataParam = new QuestClearUnlockUnitDataParam();
          unlockUnitDataParam.Deserialize(json.QuestClearUnlockUnitData[index]);
          unlockUnitDataParamList.Add(unlockUnitDataParam);
        }
        this.mUnlockUnitDataParam = unlockUnitDataParamList;
      }
      if (json.Award != null)
      {
        if (this.mAwardParam == null)
          this.mAwardParam = new List<AwardParam>(json.Award.Length);
        if (this.mAwardDictionary == null)
          this.mAwardDictionary = new Dictionary<string, AwardParam>(json.Award.Length);
        for (int index = 0; index < json.Award.Length; ++index)
        {
          JSON_AwardParam json1 = json.Award[index];
          AwardParam awardParam = new AwardParam();
          this.mAwardParam.Add(awardParam);
          awardParam.Deserialize(json1);
          this.mAwardDictionary.Add(awardParam.iname, awardParam);
        }
      }
      if (json.LoginInfo != null)
      {
        List<LoginInfoParam> loginInfoParamList = new List<LoginInfoParam>(json.LoginInfo.Length);
        for (int index = 0; index < json.LoginInfo.Length; ++index)
        {
          LoginInfoParam loginInfoParam = new LoginInfoParam();
          if (loginInfoParam.Deserialize(json.LoginInfo[index]))
            loginInfoParamList.Add(loginInfoParam);
        }
        this.mLoginInfoParam = loginInfoParamList.ToArray();
      }
      if (json.CollaboSkill != null)
      {
        List<CollaboSkillParam> collaboSkillParamList = new List<CollaboSkillParam>(json.CollaboSkill.Length);
        for (int index = 0; index < json.CollaboSkill.Length; ++index)
        {
          CollaboSkillParam collaboSkillParam = new CollaboSkillParam();
          collaboSkillParam.Deserialize(json.CollaboSkill[index]);
          collaboSkillParamList.Add(collaboSkillParam);
        }
        this.mCollaboSkillParam = collaboSkillParamList;
        CollaboSkillParam.UpdateCollaboSkill(this.mCollaboSkillParam);
      }
      if (json.Trick != null)
      {
        List<TrickParam> trickParamList = new List<TrickParam>(json.Trick.Length);
        for (int index = 0; index < json.Trick.Length; ++index)
        {
          TrickParam trickParam = new TrickParam();
          trickParam.Deserialize(json.Trick[index]);
          trickParamList.Add(trickParam);
        }
        this.mTrickParam = trickParamList;
      }
      if (json.BreakObj != null)
      {
        List<BreakObjParam> breakObjParamList = new List<BreakObjParam>(json.BreakObj.Length);
        for (int index = 0; index < json.BreakObj.Length; ++index)
        {
          BreakObjParam breakObjParam = new BreakObjParam();
          breakObjParam.Deserialize(json.BreakObj[index]);
          breakObjParamList.Add(breakObjParam);
        }
        this.mBreakObjParam = breakObjParamList;
      }
      if (json.VersusMatchKey != null)
      {
        this.mVersusMatching = new List<VersusMatchingParam>(json.VersusMatchKey.Length);
        for (int index = 0; index < json.VersusMatchKey.Length; ++index)
        {
          VersusMatchingParam versusMatchingParam = new VersusMatchingParam();
          versusMatchingParam.Deserialize(json.VersusMatchKey[index]);
          this.mVersusMatching.Add(versusMatchingParam);
        }
      }
      if (json.VersusMatchCond != null)
      {
        this.mVersusMatchCond = new List<VersusMatchCondParam>(json.VersusMatchCond.Length);
        for (int index = 0; index < json.VersusMatchCond.Length; ++index)
        {
          VersusMatchCondParam versusMatchCondParam = new VersusMatchCondParam();
          versusMatchCondParam.Deserialize(json.VersusMatchCond[index]);
          this.mVersusMatchCond.Add(versusMatchCondParam);
        }
      }
      this.mArenaRewardParams = new List<ArenaRewardParam>();
      ArenaRewardParam.Deserialize(ref this.mArenaRewardParams, json.ArenaRankResult);
      if (json.TowerScore != null)
      {
        this.mTowerScores = new Dictionary<string, TowerScoreParam[]>(json.TowerScore.Length);
        for (int index1 = 0; index1 < json.TowerScore.Length; ++index1)
        {
          JSON_TowerScore jsonTowerScore = json.TowerScore[index1];
          int length = jsonTowerScore.threshold_vals.Length;
          TowerScoreParam[] towerScoreParamArray = new TowerScoreParam[length];
          for (int index2 = 0; index2 < length; ++index2)
          {
            JSON_TowerScoreThreshold thresholdVal = jsonTowerScore.threshold_vals[index2];
            towerScoreParamArray[index2] = new TowerScoreParam();
            towerScoreParamArray[index2].Deserialize(thresholdVal);
          }
          this.mTowerScores.Add(jsonTowerScore.iname, towerScoreParamArray);
        }
      }
      if (json.TowerRank != null)
      {
        this.mTowerRankTbl = new OInt[json.TowerRank.Length];
        for (int index = 0; index < json.TowerRank.Length; ++index)
          this.mTowerRankTbl[index] = (OInt) json.TowerRank[index];
      }
      if (json.MultilimitUnitLv != null)
      {
        this.mMultiLimitUnitLv = new OInt[json.MultilimitUnitLv.Length];
        for (int index = 0; index < json.MultilimitUnitLv.Length; ++index)
          this.mMultiLimitUnitLv[index] = (OInt) json.MultilimitUnitLv[index];
      }
      if (json.FriendPresentItem != null)
      {
        this.mFriendPresentItemParam = new Dictionary<string, FriendPresentItemParam>();
        for (int index = 0; index < json.FriendPresentItem.Length; ++index)
        {
          FriendPresentItemParam presentItemParam = new FriendPresentItemParam();
          presentItemParam.Deserialize(json.FriendPresentItem[index], (MasterParam) null);
          this.mFriendPresentItemParam.Add(presentItemParam.iname, presentItemParam);
        }
      }
      if (json.Weather != null)
      {
        List<WeatherParam> weatherParamList = new List<WeatherParam>(json.Weather.Length);
        for (int index = 0; index < json.Weather.Length; ++index)
        {
          WeatherParam weatherParam = new WeatherParam();
          weatherParam.Deserialize(json.Weather[index]);
          weatherParamList.Add(weatherParam);
        }
        this.mWeatherParam = weatherParamList;
      }
      if (json.UnitUnlockTime != null)
      {
        this.mUnitUnlockTimeParam = new Dictionary<string, UnitUnlockTimeParam>();
        for (int index = 0; index < json.UnitUnlockTime.Length; ++index)
        {
          UnitUnlockTimeParam unitUnlockTimeParam = new UnitUnlockTimeParam();
          unitUnlockTimeParam.Deserialize(json.UnitUnlockTime[index]);
          this.mUnitUnlockTimeParam.Add(unitUnlockTimeParam.iname, unitUnlockTimeParam);
        }
      }
      if (json.Tobira != null)
      {
        for (int index = 0; index < json.Tobira.Length; ++index)
        {
          TobiraParam tobiraParam = new TobiraParam();
          tobiraParam.Deserialize(json.Tobira[index]);
          this.mTobiraParam.Add(tobiraParam);
        }
      }
      if (json.TobiraCategories != null)
      {
        for (int index = 0; index < json.TobiraCategories.Length; ++index)
        {
          TobiraCategoriesParam tobiraCategoriesParam = new TobiraCategoriesParam();
          tobiraCategoriesParam.Deserialize(json.TobiraCategories[index]);
          this.mTobiraCategoriesParam.Add(tobiraCategoriesParam.TobiraCategory, tobiraCategoriesParam);
        }
      }
      if (json.TobiraConds != null)
      {
        for (int index = 0; index < json.TobiraConds.Length; ++index)
        {
          TobiraCondsParam tobiraCondsParam = new TobiraCondsParam();
          tobiraCondsParam.Deserialize(json.TobiraConds[index]);
          this.mTobiraCondParam.Add(tobiraCondsParam);
        }
      }
      if (json.TobiraCondsUnit != null)
      {
        for (int index = 0; index < json.TobiraCondsUnit.Length; ++index)
        {
          TobiraCondsUnitParam tobiraCondsUnitParam = new TobiraCondsUnitParam();
          tobiraCondsUnitParam.Deserialize(json.TobiraCondsUnit[index]);
          this.mTobiraCondUnitParam.Add(tobiraCondsUnitParam.Id, tobiraCondsUnitParam);
        }
      }
      if (json.TobiraRecipe != null)
      {
        for (int index = 0; index < json.TobiraRecipe.Length; ++index)
        {
          TobiraRecipeParam tobiraRecipeParam = new TobiraRecipeParam();
          tobiraRecipeParam.Deserialize(json.TobiraRecipe[index]);
          this.mTobiraRecipeParam.Add(tobiraRecipeParam);
        }
      }
      if (json.ConceptCard != null)
      {
        this.mConceptCard = new Dictionary<string, ConceptCardParam>();
        for (int index = 0; index < json.ConceptCard.Length; ++index)
        {
          ConceptCardParam conceptCardParam = new ConceptCardParam();
          conceptCardParam.Deserialize(json.ConceptCard[index], (MasterParam) null);
          this.mConceptCard.Add(conceptCardParam.iname, conceptCardParam);
        }
      }
      int[][] numArray = new int[6][]{ json.ConceptCardLvTbl1, json.ConceptCardLvTbl2, json.ConceptCardLvTbl3, json.ConceptCardLvTbl4, json.ConceptCardLvTbl5, json.ConceptCardLvTbl6 };
      if (0 < numArray.Length && numArray[0] != null && 0 < numArray[0].Length)
      {
        this.mConceptCardLvTbl = new OInt[numArray.Length, numArray[0].Length];
        for (int index1 = 0; index1 < numArray.Length; ++index1)
        {
          for (int index2 = 0; index2 < numArray[index1].Length; ++index2)
            this.mConceptCardLvTbl[index1, index2] = (OInt) numArray[index1][index2];
        }
      }
      if (json.ConceptCardConditions != null)
      {
        this.mConceptCardConditions = new Dictionary<string, ConceptCardConditionsParam>();
        for (int index = 0; index < json.ConceptCardConditions.Length; ++index)
        {
          ConceptCardConditionsParam cardConditionsParam = new ConceptCardConditionsParam();
          cardConditionsParam.Deserialize(json.ConceptCardConditions[index]);
          this.mConceptCardConditions.Add(cardConditionsParam.iname, cardConditionsParam);
        }
      }
      if (json.ConceptCardTrustReward != null)
      {
        this.mConceptCardTrustReward = new Dictionary<string, ConceptCardTrustRewardParam>();
        for (int index = 0; index < json.ConceptCardTrustReward.Length; ++index)
        {
          ConceptCardTrustRewardParam trustRewardParam = new ConceptCardTrustRewardParam();
          trustRewardParam.Deserialize(json.ConceptCardTrustReward[index]);
          this.mConceptCardTrustReward.Add(trustRewardParam.iname, trustRewardParam);
        }
      }
      if (json.UnitGroup != null)
      {
        this.mUnitGroup = new Dictionary<string, UnitGroupParam>();
        for (int index = 0; index < json.UnitGroup.Length; ++index)
        {
          UnitGroupParam unitGroupParam = new UnitGroupParam();
          unitGroupParam.Deserialize(json.UnitGroup[index]);
          this.mUnitGroup.Add(unitGroupParam.iname, unitGroupParam);
        }
      }
      if (json.JobGroup != null)
      {
        this.mJobGroup = new Dictionary<string, JobGroupParam>();
        for (int index = 0; index < json.JobGroup.Length; ++index)
        {
          JobGroupParam jobGroupParam = new JobGroupParam();
          jobGroupParam.Deserialize(json.JobGroup[index]);
          this.mJobGroup.Add(jobGroupParam.iname, jobGroupParam);
        }
      }
      if (json.StatusCoefficient != null && json.StatusCoefficient.Length > 0)
      {
        this.mStatusCoefficient = new StatusCoefficientParam();
        this.mStatusCoefficient.Deserialize(json.StatusCoefficient[0]);
      }
      if (json.CustomTarget != null)
      {
        this.mCustomTarget = new Dictionary<string, CustomTargetParam>();
        for (int index = 0; index < json.CustomTarget.Length; ++index)
        {
          CustomTargetParam customTargetParam = new CustomTargetParam();
          customTargetParam.Deserialize(json.CustomTarget[index]);
          this.mCustomTarget.Add(customTargetParam.iname, customTargetParam);
        }
      }
      if (json.SkillAbilityDerive != null && json.SkillAbilityDerive.Length > 0)
      {
        this.mSkillAbilityDeriveParam = new SkillAbilityDeriveParam[json.SkillAbilityDerive.Length];
        for (int index = 0; index < json.SkillAbilityDerive.Length; ++index)
        {
          this.mSkillAbilityDeriveParam[index] = new SkillAbilityDeriveParam(index);
          this.mSkillAbilityDeriveParam[index].Deserialize(json.SkillAbilityDerive[index], this);
        }
        for (int index = 0; index < this.mSkillAbilityDeriveParam.Length; ++index)
        {
          SkillAbilityDeriveData abilityDeriveData = new SkillAbilityDeriveData();
          List<SkillAbilityDeriveParam> abilityDeriveParam = this.FindAditionalSkillAbilityDeriveParam(this.mSkillAbilityDeriveParam[index]);
          abilityDeriveData.Setup(this.mSkillAbilityDeriveParam[index], abilityDeriveParam);
          this.mSkillAbilityDerives.Add(abilityDeriveData);
        }
      }
      if (json.Tips != null && json.Tips.Length > 0)
      {
        this.mTipsParam = new TipsParam[json.Tips.Length];
        for (int index = 0; index < json.Tips.Length; ++index)
        {
          this.mTipsParam[index] = new TipsParam();
          this.mTipsParam[index].Deserialize(json.Tips[index]);
        }
      }
      if (json.GuildEmblem != null)
      {
        if (this.mGuildEmblemParam == null)
          this.mGuildEmblemParam = new List<GuildEmblemParam>(json.GuildEmblem.Length);
        if (this.mGuildEmblemDictionary == null)
          this.mGuildEmblemDictionary = new Dictionary<string, GuildEmblemParam>(json.GuildEmblem.Length);
        for (int index = 0; index < json.GuildEmblem.Length; ++index)
        {
          JSON_GuildEmblemParam data = json.GuildEmblem[index];
          if (!string.IsNullOrEmpty(data.iname))
          {
            GuildEmblemParam guildEmblemParam = this.mGuildEmblemParam.Find((Predicate<GuildEmblemParam>) (p => p.Iname == data.iname));
            if (guildEmblemParam == null)
            {
              guildEmblemParam = new GuildEmblemParam();
              this.mGuildEmblemParam.Add(guildEmblemParam);
            }
            guildEmblemParam.Deserialize(data);
            if (this.mGuildEmblemDictionary.ContainsKey(guildEmblemParam.Iname))
              throw new Exception("Overlap : GuildEmblem[" + guildEmblemParam.Iname + "]");
            this.mGuildEmblemDictionary.Add(guildEmblemParam.Iname, guildEmblemParam);
          }
        }
      }
      if (json.GuildFacility != null)
      {
        if (this.mGuildFacilityParam == null)
          this.mGuildFacilityParam = new List<GuildFacilityParam>(json.GuildFacility.Length);
        if (this.mGuildFacilityDictionary == null)
          this.mGuildFacilityDictionary = new Dictionary<string, GuildFacilityParam>(json.GuildFacility.Length);
        for (int index = 0; index < json.GuildFacility.Length; ++index)
        {
          JSON_GuildFacilityParam data = json.GuildFacility[index];
          if (!string.IsNullOrEmpty(data.iname))
          {
            GuildFacilityParam guildFacilityParam = this.mGuildFacilityParam.Find((Predicate<GuildFacilityParam>) (p => p.Iname == data.iname));
            if (guildFacilityParam == null)
            {
              guildFacilityParam = new GuildFacilityParam();
              this.mGuildFacilityParam.Add(guildFacilityParam);
            }
            guildFacilityParam.Deserialize(data);
            if (this.mGuildFacilityDictionary.ContainsKey(guildFacilityParam.Iname))
              throw new Exception("Overlap : GuildFacilityParam[" + guildFacilityParam.Iname + "]");
            this.mGuildFacilityDictionary.Add(guildFacilityParam.Iname, guildFacilityParam);
          }
        }
      }
      if (json.GuildFacilityLvTbl != null)
      {
        if (this.mGuildFacilityLvParam == null)
          this.mGuildFacilityLvParam = new GuildFacilityLvParam[json.GuildFacilityLvTbl.Length];
        for (int index = 0; index < json.GuildFacilityLvTbl.Length; ++index)
        {
          GuildFacilityLvParam guildFacilityLvParam = new GuildFacilityLvParam();
          guildFacilityLvParam.Deserialize(json.GuildFacilityLvTbl[index]);
          this.mGuildFacilityLvParam[index] = guildFacilityLvParam;
        }
      }
      if (json.DynamicTransformUnit != null)
      {
        List<DynamicTransformUnitParam> transformUnitParamList = new List<DynamicTransformUnitParam>(json.DynamicTransformUnit.Length);
        for (int index = 0; index < json.DynamicTransformUnit.Length; ++index)
        {
          DynamicTransformUnitParam transformUnitParam = new DynamicTransformUnitParam();
          transformUnitParam.Deserialize(json.DynamicTransformUnit[index]);
          transformUnitParamList.Add(transformUnitParam);
        }
        this.mDynamicTransformUnitParam = transformUnitParamList;
      }
      if (json.ConvertUnitPieceExclude != null && json.ConvertUnitPieceExclude.Length > 0)
      {
        this.mConvertUnitPieceExcludeParam = new ConvertUnitPieceExcludeParam[json.ConvertUnitPieceExclude.Length];
        for (int index = 0; index < json.ConvertUnitPieceExclude.Length; ++index)
        {
          this.mConvertUnitPieceExcludeParam[index] = new ConvertUnitPieceExcludeParam();
          this.mConvertUnitPieceExcludeParam[index].Deserialize(json.ConvertUnitPieceExclude[index]);
        }
      }
      if (json.RecommendedArtifact != null && json.RecommendedArtifact.Length > 0)
      {
        this.mRecommendedArtifactList = new RecommendedArtifactList();
        this.mRecommendedArtifactList.Deserialize(json.RecommendedArtifact);
      }
      RaidMaster.Deserialize<RaidPeriodParam, JSON_RaidPeriodParam>(ref this.mRaidPeriodParam, json.RaidPeriod);
      RaidMaster.Deserialize<RaidAreaParam, JSON_RaidAreaParam>(ref this.mRaidAreaParam, json.RaidArea);
      RaidMaster.Deserialize<RaidBossParam, JSON_RaidBossParam>(ref this.mRaidBossParam, json.RaidBoss);
      RaidMaster.Deserialize<RaidBattleRewardParam, JSON_RaidBattleRewardParam>(ref this.mRaidBattleRewardParam, json.RaidBattleReward);
      RaidMaster.Deserialize<RaidBeatRewardParam, JSON_RaidBeatRewardParam>(ref this.mRaidBeatRewardParam, json.RaidBeatReward);
      RaidMaster.Deserialize<RaidDamageRatioRewardParam, JSON_RaidDamageRatioRewardParam>(ref this.mRaidDamageRatioRewardParam, json.RaidDamageRatioReward);
      RaidMaster.Deserialize<RaidAreaClearRewardParam, JSON_RaidAreaClearRewardParam>(ref this.mRaidAreaClearRewardParam, json.RaidAreaClearReward);
      RaidMaster.Deserialize<RaidCompleteRewardParam, JSON_RaidCompleteRewardParam>(ref this.mRaidCompleteRewardParam, json.RaidCompleteReward);
      RaidMaster.Deserialize<RaidRewardParam, JSON_RaidRewardParam>(ref this.mRaidRewardParam, json.RaidReward);
      if (json.SkillMotion != null)
      {
        List<SkillMotionParam> skillMotionParamList = new List<SkillMotionParam>(json.SkillMotion.Length);
        for (int index = 0; index < json.SkillMotion.Length; ++index)
        {
          SkillMotionParam skillMotionParam = new SkillMotionParam();
          skillMotionParam.Deserialize(json.SkillMotion[index]);
          skillMotionParamList.Add(skillMotionParam);
        }
        this.mSkillMotionParam = skillMotionParamList;
      }
      DependStateSpcEffParam.Deserialize(ref this.mDependStateSpcEffParam, json.DependStateSpcEff);
      Dictionary<string, HighlightGift> dictionary3 = new Dictionary<string, HighlightGift>();
      if (json.HighlightGift != null)
      {
        foreach (JSON_HighlightGift json1 in json.HighlightGift)
        {
          HighlightGift highlightGift = new HighlightGift();
          highlightGift.Deserialize(json1);
          dictionary3[highlightGift.iname] = highlightGift;
        }
      }
      if (json.Highlight != null)
      {
        SRPG.HighlightParam[] highlightParamArray = new SRPG.HighlightParam[json.Highlight.Length];
        for (int index = 0; index < json.Highlight.Length; ++index)
        {
          SRPG.HighlightParam highlightParam = new SRPG.HighlightParam();
          highlightParam.Deserialze(json.Highlight[index]);
          HighlightGift highlightGift;
          if (dictionary3.TryGetValue(json.Highlight[index].gift, out highlightGift))
            highlightParam.gift = highlightGift;
          highlightParamArray[index] = highlightParam;
        }
        this.mHighlightParam = highlightParamArray;
      }
      if (json.InspirationSkill != null)
        InspSkillParam.Deserialize(json.InspirationSkill, json.InspSkillTrigger, ref this.mInspSkillDictionary);
      if (json.InspSkillLvUpCost != null)
        InspSkillLvUpCostParam.Desirialize(json.InspSkillLvUpCost, ref this.mInspSkillLvUpCostParam);
      if (json.InspSkillOpenCost != null)
        InspSkillCostParam.Deserialize(json.InspSkillOpenCost, ref this.mInspSkillOpenCostDictionary);
      if (json.InspSkillResetCost != null)
        InspSkillCostParam.Deserialize(json.InspSkillResetCost, ref this.mInspSkillResetCostDictionary);
      GenesisParam.Deserialize(ref this.mGenesisParam, json.Genesis);
      this.Loaded = true;
      return true;
    }

    public ItemParam GetCommonEquip(ItemParam item_param, bool is_soul)
    {
      if (!is_soul)
      {
        if (!item_param.IsCommon)
          return (ItemParam) null;
        return MonoSingleton<GameManager>.Instance.GetItemParam((string) this.FixParam.EquipCmn[(int) item_param.cmn_type - 1]);
      }
      int rare = item_param.rare;
      if (this.FixParam.SoulCommonPiece == null || this.FixParam.SoulCommonPiece.Length <= rare)
        return (ItemParam) null;
      return MonoSingleton<GameManager>.Instance.GetItemParam((string) this.FixParam.SoulCommonPiece[rare]);
    }

    public bool IsFriendPresentItemParamValid()
    {
      if (this.mFriendPresentItemParam != null)
        return this.mFriendPresentItemParam.Count > 1;
      return false;
    }

    public FriendPresentItemParam[] GetFriendPresentItemParams()
    {
      if (this.mFriendPresentItemParam == null)
        return new FriendPresentItemParam[0];
      FriendPresentItemParam[] array = new FriendPresentItemParam[this.mFriendPresentItemParam.Values.Count];
      this.mFriendPresentItemParam.Values.CopyTo(array, 0);
      return array;
    }

    public FriendPresentItemParam GetFriendPresentItemParam(string key)
    {
      if (this.mFriendPresentItemParam == null)
        return (FriendPresentItemParam) null;
      if (string.IsNullOrEmpty(key))
        return (FriendPresentItemParam) null;
      FriendPresentItemParam presentItemParam = (FriendPresentItemParam) null;
      if (!this.mFriendPresentItemParam.TryGetValue(key, out presentItemParam))
        DebugUtility.LogError("存在しないフレンドプレゼントアイテムパラメータを参照しています > " + key);
      return presentItemParam;
    }

    public void MakeMapEffectHaveJobLists()
    {
      if (this.mJobParam == null || MapEffectParam.IsMakeHaveJobLists())
        return;
      MapEffectParam.MakeHaveJobLists();
      foreach (JobParam job_param in this.mJobParam)
      {
        if (!string.IsNullOrEmpty(job_param.MapEffectAbility) && job_param.IsMapEffectRevReso)
        {
          AbilityParam abilityParam = this.GetAbilityParam(job_param.MapEffectAbility);
          if (abilityParam != null)
          {
            foreach (LearningSkill skill in abilityParam.skills)
              MapEffectParam.AddHaveJob(skill.iname, job_param);
          }
        }
      }
    }

    public UnitUnlockTimeParam[] GetUnitUnlockTimeParams()
    {
      if (this.mUnitUnlockTimeParam == null)
        return (UnitUnlockTimeParam[]) null;
      UnitUnlockTimeParam[] array = new UnitUnlockTimeParam[this.mUnitUnlockTimeParam.Values.Count];
      this.mUnitUnlockTimeParam.Values.CopyTo(array, 0);
      return array;
    }

    public UnitUnlockTimeParam GetUnitUnlockTimeParam(string _key)
    {
      if (string.IsNullOrEmpty(_key))
        return (UnitUnlockTimeParam) null;
      if (this.mUnitUnlockTimeParam == null)
        return (UnitUnlockTimeParam) null;
      UnitUnlockTimeParam unitUnlockTimeParam = (UnitUnlockTimeParam) null;
      if (!this.mUnitUnlockTimeParam.TryGetValue(_key, out unitUnlockTimeParam))
        return (UnitUnlockTimeParam) null;
      return unitUnlockTimeParam;
    }

    public bool IsUnlockableUnit(string _key, DateTime _time)
    {
      if (string.IsNullOrEmpty(_key))
        return true;
      UnitUnlockTimeParam unitUnlockTimeParam = (UnitUnlockTimeParam) null;
      return !this.mUnitUnlockTimeParam.TryGetValue(_key, out unitUnlockTimeParam) || unitUnlockTimeParam.begin_at.CompareTo(_time) <= 0 && unitUnlockTimeParam.end_at.CompareTo(_time) >= 0;
    }

    public ConceptCardParam GetConceptCardParam(string iname)
    {
      if (!this.mConceptCard.ContainsKey(iname))
        return (ConceptCardParam) null;
      return this.mConceptCard[iname];
    }

    public int GetConceptCardNextExp(int rarity, int lv)
    {
      return (int) this.mConceptCardLvTbl[rarity, lv - 1];
    }

    public int GetConceptCardLevelExp(int rarity, int lv)
    {
      int num = 0;
      for (int index = 0; index < lv; ++index)
        num += (int) this.mConceptCardLvTbl[rarity, index];
      return num;
    }

    public int CalcConceptCardLevel(int rarity, int totalExp, int levelCap)
    {
      int val2 = levelCap;
      int num = 0;
      int val1 = 0;
      for (int index = 0; index < val2; ++index)
      {
        num += this.GetConceptCardNextExp(rarity, index + 1);
        if (num <= totalExp)
          ++val1;
        else
          break;
      }
      return Math.Min(Math.Max(val1, 1), val2);
    }

    public ConceptCardConditionsParam GetConceptCardConditions(string iname)
    {
      if (string.IsNullOrEmpty(iname))
        return (ConceptCardConditionsParam) null;
      if (!this.mConceptCardConditions.ContainsKey(iname))
        return (ConceptCardConditionsParam) null;
      return this.mConceptCardConditions[iname];
    }

    public int GetConceptCardTrustMax(ConceptCardData data)
    {
      if (data == null)
        return (int) this.FixParam.CardTrustMax;
      return (int) this.FixParam.CardTrustMax + (int) this.FixParam.CardTrustMax * (int) data.AwakeCount;
    }

    public UnitGroupParam GetUnitGroup(string iname)
    {
      if (string.IsNullOrEmpty(iname))
        return (UnitGroupParam) null;
      if (!this.mUnitGroup.ContainsKey(iname))
        return (UnitGroupParam) null;
      return this.mUnitGroup[iname];
    }

    public JobGroupParam GetJobGroup(string iname)
    {
      if (string.IsNullOrEmpty(iname))
        return (JobGroupParam) null;
      if (!this.mJobGroup.ContainsKey(iname))
        return (JobGroupParam) null;
      return this.mJobGroup[iname];
    }

    public ConceptCardTrustRewardParam GetTrustReward(string iname)
    {
      if (string.IsNullOrEmpty(iname))
        return (ConceptCardTrustRewardParam) null;
      if (!this.mConceptCardTrustReward.ContainsKey(iname))
        return (ConceptCardTrustRewardParam) null;
      return this.mConceptCardTrustReward[iname];
    }

    public CustomTargetParam GetCustomTarget(string iname)
    {
      if (string.IsNullOrEmpty(iname))
        return (CustomTargetParam) null;
      if (!this.mCustomTarget.ContainsKey(iname))
        return (CustomTargetParam) null;
      return this.mCustomTarget[iname];
    }

    public List<ConceptCardParam> GetConceptCardParams()
    {
      return this.mConceptCard.Select<KeyValuePair<string, ConceptCardParam>, ConceptCardParam>((Func<KeyValuePair<string, ConceptCardParam>, ConceptCardParam>) (pair => pair.Value)).ToList<ConceptCardParam>();
    }

    public List<SkillAbilityDeriveParam> FindSkillAbilityDeriveParamWithArtifact(string artifactIname)
    {
      List<SkillAbilityDeriveParam> abilityDeriveParamList = new List<SkillAbilityDeriveParam>();
      if (this.mSkillAbilityDeriveParam == null)
        return abilityDeriveParamList;
      for (int index = 0; index < this.mSkillAbilityDeriveParam.Length; ++index)
      {
        if (this.mSkillAbilityDeriveParam[index].CheckContainsTriggerIname(ESkillAbilityDeriveConds.EquipArtifact, artifactIname))
          abilityDeriveParamList.Add(this.mSkillAbilityDeriveParam[index]);
      }
      return abilityDeriveParamList;
    }

    public List<SkillAbilityDeriveParam> FindAditionalSkillAbilityDeriveParam(SkillAbilityDeriveParam skillAbilityDeriveParam, ESkillAbilityDeriveConds triggerType, string triggerIname)
    {
      List<SkillAbilityDeriveParam> abilityDeriveParamList = new List<SkillAbilityDeriveParam>();
      if (this.mSkillAbilityDeriveParam == null)
        return abilityDeriveParamList;
      SkillAbilityDeriveTriggerParam[] array = ((IEnumerable<SkillAbilityDeriveTriggerParam>) skillAbilityDeriveParam.deriveTriggers).Where<SkillAbilityDeriveTriggerParam>((Func<SkillAbilityDeriveTriggerParam, bool>) (triggerParam => triggerParam.m_TriggerIname != triggerIname)).ToArray<SkillAbilityDeriveTriggerParam>();
      SkillAbilityDeriveTriggerParam deriveTriggerParam1 = new SkillAbilityDeriveTriggerParam(triggerType, triggerIname);
      for (int index = skillAbilityDeriveParam.m_OriginIndex + 1; index < this.mSkillAbilityDeriveParam.Length; ++index)
      {
        bool flag = false;
        foreach (SkillAbilityDeriveTriggerParam deriveTriggerParam2 in array)
        {
          if (this.mSkillAbilityDeriveParam[index].CheckContainsTriggerInames(new SkillAbilityDeriveTriggerParam[2]{ deriveTriggerParam2, deriveTriggerParam1 }))
          {
            abilityDeriveParamList.Add(this.mSkillAbilityDeriveParam[index]);
            flag = true;
            break;
          }
        }
        if (!flag)
        {
          if (this.mSkillAbilityDeriveParam[index].CheckContainsTriggerInames(new SkillAbilityDeriveTriggerParam[1]{ deriveTriggerParam1 }))
            abilityDeriveParamList.Add(this.mSkillAbilityDeriveParam[index]);
        }
      }
      return abilityDeriveParamList;
    }

    public List<SkillAbilityDeriveParam> FindAditionalSkillAbilityDeriveParam(SkillAbilityDeriveParam skillAbilityDeriveParam)
    {
      List<SkillAbilityDeriveParam> abilityDeriveParamList = new List<SkillAbilityDeriveParam>();
      if (skillAbilityDeriveParam == null)
        return abilityDeriveParamList;
      foreach (SkillAbilityDeriveTriggerParam deriveTrigger in skillAbilityDeriveParam.deriveTriggers)
      {
        foreach (SkillAbilityDeriveParam abilityDeriveParam in this.FindAditionalSkillAbilityDeriveParam(skillAbilityDeriveParam, deriveTrigger.m_TriggerType, deriveTrigger.m_TriggerIname))
        {
          if (!abilityDeriveParamList.Contains(abilityDeriveParam))
            abilityDeriveParamList.Add(abilityDeriveParam);
        }
      }
      return abilityDeriveParamList;
    }

    public Dictionary<string, SkillAbilityDeriveData> CreateSkillAbilityDeriveDataWithArtifacts(JobData[] jobData)
    {
      if (jobData == null || jobData.Length < 1)
        return (Dictionary<string, SkillAbilityDeriveData>) null;
      Dictionary<string, SkillAbilityDeriveData> dictionary = (Dictionary<string, SkillAbilityDeriveData>) null;
      for (int index = 0; index < jobData.Length; ++index)
      {
        if (jobData[index] != null)
        {
          SkillAbilityDeriveData dataWithArtifacts = this.CreateSkillAbilityDeriveDataWithArtifacts(((IEnumerable<ArtifactData>) jobData[index].ArtifactDatas).Where<ArtifactData>((Func<ArtifactData, bool>) (artifact => artifact != null)).Select<ArtifactData, string>((Func<ArtifactData, string>) (artifact => artifact.ArtifactParam.iname)).ToArray<string>());
          if (dataWithArtifacts != null)
          {
            if (dictionary == null)
              dictionary = new Dictionary<string, SkillAbilityDeriveData>();
            dictionary.Add(jobData[index].Param.iname, dataWithArtifacts);
          }
        }
      }
      return dictionary;
    }

    public List<SkillAbilityDeriveData> FindAllSkillAbilityDeriveDataWithArtifact(string artifactIname)
    {
      List<SkillAbilityDeriveData> abilityDeriveDataList = new List<SkillAbilityDeriveData>();
      foreach (SkillAbilityDeriveData skillAbilityDerive in this.mSkillAbilityDerives)
      {
        if (skillAbilityDerive.CheckContainsTriggerIname(ESkillAbilityDeriveConds.EquipArtifact, artifactIname))
          abilityDeriveDataList.Add(skillAbilityDerive);
      }
      return abilityDeriveDataList;
    }

    public bool ExistSkillAbilityDeriveDataWithArtifact(string artifactIname)
    {
      return this.FindAllSkillAbilityDeriveDataWithArtifact(artifactIname).Count > 0;
    }

    public SkillAbilityDeriveData CreateSkillAbilityDeriveDataWithArtifacts(string[] artifactInames)
    {
      if (artifactInames.Length < 1)
        return (SkillAbilityDeriveData) null;
      List<SkillAbilityDeriveData> abilityDeriveDataList = new List<SkillAbilityDeriveData>();
      List<SkillAbilityDeriveTriggerParam[]> combination = SkillAbilityDeriveTriggerParam.CreateCombination(((IEnumerable<string>) artifactInames).Select<string, SkillAbilityDeriveTriggerParam>((Func<string, SkillAbilityDeriveTriggerParam>) (iname => new SkillAbilityDeriveTriggerParam(ESkillAbilityDeriveConds.EquipArtifact, iname))).ToArray<SkillAbilityDeriveTriggerParam>());
      combination.Sort((Comparison<SkillAbilityDeriveTriggerParam[]>) ((triggers1, triggers2) => triggers2.Length.CompareTo(triggers1.Length)));
      foreach (SkillAbilityDeriveTriggerParam[] searchKeyTriggerParam in combination)
      {
        foreach (SkillAbilityDeriveData skillAbilityDerive in this.mSkillAbilityDerives)
        {
          if (skillAbilityDerive.CheckContainsTriggerInames(searchKeyTriggerParam))
            abilityDeriveDataList.Add(skillAbilityDerive);
        }
        if (abilityDeriveDataList.Count > 0)
        {
          if (searchKeyTriggerParam.Length == artifactInames.Length)
            break;
        }
      }
      SkillAbilityDeriveData abilityDeriveData1 = (SkillAbilityDeriveData) null;
      if (abilityDeriveDataList.Count > 0)
      {
        abilityDeriveData1 = new SkillAbilityDeriveData();
        SkillAbilityDeriveData abilityDeriveData2 = abilityDeriveDataList[0];
        List<SkillAbilityDeriveParam> additionalSkillAbilityDeriveParams = new List<SkillAbilityDeriveParam>();
        foreach (SkillAbilityDeriveParam abilityDeriveParam in abilityDeriveData2.m_AdditionalSkillAbilityDeriveParam)
        {
          if (!additionalSkillAbilityDeriveParams.Contains(abilityDeriveParam))
            additionalSkillAbilityDeriveParams.Add(abilityDeriveParam);
        }
        for (int index = 1; index < abilityDeriveDataList.Count; ++index)
        {
          if (!additionalSkillAbilityDeriveParams.Contains(abilityDeriveDataList[index].m_SkillAbilityDeriveParam))
            additionalSkillAbilityDeriveParams.Add(abilityDeriveDataList[index].m_SkillAbilityDeriveParam);
          foreach (SkillAbilityDeriveParam abilityDeriveParam in abilityDeriveDataList[index].m_AdditionalSkillAbilityDeriveParam)
          {
            if (!additionalSkillAbilityDeriveParams.Contains(abilityDeriveParam))
              additionalSkillAbilityDeriveParams.Add(abilityDeriveParam);
          }
        }
        abilityDeriveData1.Setup(abilityDeriveData2.m_SkillAbilityDeriveParam, additionalSkillAbilityDeriveParams);
      }
      return abilityDeriveData1;
    }

    public TowerScoreParam[] FindTowerScoreParam(string score_iname)
    {
      TowerScoreParam[] towerScoreParamArray = (TowerScoreParam[]) null;
      this.mTowerScores.TryGetValue(score_iname, out towerScoreParamArray);
      return towerScoreParamArray;
    }

    public GuildEmblemParam[] GetGuildEmblemes()
    {
      return this.mGuildEmblemParam.ToArray();
    }

    public GuildEmblemParam[] GetNoConditionsGuildEmblemes()
    {
      List<GuildEmblemParam> guildEmblemParamList = new List<GuildEmblemParam>();
      for (int index = 0; index < this.mGuildEmblemParam.Count; ++index)
      {
        if (this.mGuildEmblemParam[index].ConditionsType <= 0)
          guildEmblemParamList.Add(this.mGuildEmblemParam[index]);
      }
      return guildEmblemParamList.ToArray();
    }

    public GuildFacilityParam GetGuildFacility(string iname)
    {
      if (!this.mGuildFacilityDictionary.ContainsKey(iname))
        return (GuildFacilityParam) null;
      return this.mGuildFacilityDictionary[iname];
    }

    public GuildFacilityLvParam[] GetGuildFacilityLevelTable()
    {
      return this.mGuildFacilityLvParam;
    }

    public List<ArtifactParam> GetRecommendedArtifactParams(UnitData unitData, bool isMasterOnly)
    {
      if (isMasterOnly)
        return this.mRecommendedArtifactList.GetRecommendedArtifacts(unitData, this);
      List<ArtifactParam> recommendedArtifacts = this.mRecommendedArtifactList.GetRecommendedArtifacts(unitData, this);
      foreach (ArtifactParam artifactParam in this.GetArtifactsWithUnitUsableAbility(unitData))
      {
        if (!recommendedArtifacts.Contains(artifactParam))
          recommendedArtifacts.Add(artifactParam);
      }
      return recommendedArtifacts;
    }

    public List<ArtifactParam> GetArtifactsWithUnitUsableAbility(UnitData unitData)
    {
      List<ArtifactParam> artifactParamList = new List<ArtifactParam>();
      JobData currentJob = unitData.CurrentJob;
      if (unitData == null || this.mArtifactParam == null || this.mAbilityParam == null)
        return artifactParamList;
      List<ArtifactParam> mArtifactParam = this.mArtifactParam;
      // ISSUE: reference to a compiler-generated field
      if (MasterParam.\u003C\u003Ef__mg\u0024cache0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MasterParam.\u003C\u003Ef__mg\u0024cache0 = new Func<ArtifactParam, bool>(ArtifactParam.IsDetachableArtifact);
      }
      // ISSUE: reference to a compiler-generated field
      Func<ArtifactParam, bool> fMgCache0 = MasterParam.\u003C\u003Ef__mg\u0024cache0;
      List<ArtifactParam> list = mArtifactParam.Where<ArtifactParam>(fMgCache0).ToList<ArtifactParam>();
      for (int index1 = 0; index1 < list.Count; ++index1)
      {
        ArtifactParam artifactParam = list[index1];
        if (artifactParam.CheckEnableEquip(unitData, -1) && artifactParam.HasAbility)
        {
          bool flag = false;
          for (int index2 = 0; index2 < artifactParam.abil_inames.Length; ++index2)
          {
            AbilityParam abilityParam = this.GetAbilityParam(artifactParam.abil_inames[index2]);
            if (abilityParam.condition_units != null && ((IEnumerable<string>) abilityParam.condition_units).Contains<string>(unitData.UnitParam.iname) && abilityParam.CheckEnableUseAbility(unitData, unitData.JobIndex))
            {
              flag = true;
              break;
            }
          }
          if (flag)
            artifactParamList.Add(artifactParam);
        }
      }
      return artifactParamList;
    }

    public RaidPeriodParam GetActiveRaidPeriod(int begin_offset_hour = 0, int end_offset_hour = 0)
    {
      DateTime serverTime = TimeManager.ServerTime;
      for (int index = 0; index < this.mRaidPeriodParam.Count; ++index)
      {
        if (this.mRaidPeriodParam[index].BeginAt.AddHours((double) begin_offset_hour) <= serverTime && serverTime <= this.mRaidPeriodParam[index].EndAt.AddHours((double) end_offset_hour))
          return this.mRaidPeriodParam[index];
      }
      return (RaidPeriodParam) null;
    }

    public RaidAreaParam GetRaidArea(int area_id)
    {
      return this.mRaidAreaParam.Find((Predicate<RaidAreaParam>) (ra => ra.Id == area_id));
    }

    public RaidAreaParam GetRaidAreaByOrder(int period_id, int order)
    {
      return this.mRaidAreaParam.Find((Predicate<RaidAreaParam>) (ra =>
      {
        if (ra.PeriodId == period_id)
          return ra.Order == order;
        return false;
      }));
    }

    public int GetRaidAreaCount(int period_id)
    {
      return this.mRaidAreaParam.FindAll((Predicate<RaidAreaParam>) (ra => ra.PeriodId == period_id)).Count;
    }

    public RaidBossParam GetRaidBoss(int boss_id)
    {
      return this.mRaidBossParam.Find((Predicate<RaidBossParam>) (rbg => rbg.Id == boss_id));
    }

    public List<RaidBossParam> GetRaidBossAll(int period_id)
    {
      return this.mRaidBossParam.FindAll((Predicate<RaidBossParam>) (rbp => rbp.PeriodId == period_id));
    }

    public string GetRaidBeatReward(int beat_reward_id, int round)
    {
      RaidBeatRewardParam raidBeatRewardParam = this.mRaidBeatRewardParam.Find((Predicate<RaidBeatRewardParam>) (rbr => rbr.Id == beat_reward_id));
      if (raidBeatRewardParam == null || raidBeatRewardParam.Rewards.Count == 0)
        return string.Empty;
      RaidBeatRewardDataParam reward_data = raidBeatRewardParam.Rewards.Find((Predicate<RaidBeatRewardDataParam>) (rbrd => rbrd.Round == round));
      if (reward_data == null)
      {
        reward_data = raidBeatRewardParam.Rewards[0];
        raidBeatRewardParam.Rewards.ForEach((Action<RaidBeatRewardDataParam>) (rbrd => reward_data = reward_data.Round >= rbrd.Round ? reward_data : rbrd));
      }
      return reward_data.RewardId;
    }

    public List<RaidReward> GetRaidRewardList(string reward_id)
    {
      RaidRewardParam raidRewardParam = this.mRaidRewardParam.Find((Predicate<RaidRewardParam>) (rr => rr.Id == reward_id));
      if (raidRewardParam == null)
        return new List<RaidReward>();
      return raidRewardParam.Rewards;
    }

    public RaidPeriodParam GetRaidPeriod(int period_id)
    {
      return this.mRaidPeriodParam.Find((Predicate<RaidPeriodParam>) (param => param.Id == period_id));
    }

    public RaidCompleteRewardParam GetRaidCompleteReward(int comp_reward_id)
    {
      return this.mRaidCompleteRewardParam.Find((Predicate<RaidCompleteRewardParam>) (param => param.Id == comp_reward_id));
    }

    public List<ArenaRewardParam> GetArenaRewardParams()
    {
      if (this.mArenaRewardParams == null)
        this.mArenaRewardParams = new List<ArenaRewardParam>();
      return ArenaRewardParam.GetSortedRewardParams(this.mArenaRewardParams);
    }

    public InspSkillParam GetInspirationSkillParam(string iname)
    {
      if (this.mInspSkillDictionary == null || string.IsNullOrEmpty(iname) || !this.mInspSkillDictionary.ContainsKey(iname))
        return (InspSkillParam) null;
      return this.mInspSkillDictionary[iname];
    }

    public InspSkillCostParam GetInspSkillResetCost(int gen)
    {
      if (this.mInspSkillResetCostDictionary == null || !this.mInspSkillResetCostDictionary.ContainsKey(gen))
        return (InspSkillCostParam) null;
      return this.mInspSkillResetCostDictionary[gen];
    }

    public InspSkillCostParam GetInspSkillOpenCost(int count)
    {
      if (this.mInspSkillOpenCostDictionary == null || !this.mInspSkillOpenCostDictionary.ContainsKey(count))
        return (InspSkillCostParam) null;
      return this.mInspSkillOpenCostDictionary[count];
    }

    public int GetInspSkillMaxOpenCount()
    {
      if (this.mInspSkillOpenCostDictionary == null)
        return 0;
      return this.mInspSkillOpenCostDictionary.Count;
    }

    public int GetInspLvUpCostTotal(int id, int current_lv, int up)
    {
      if (this.mInspSkillLvUpCostParam == null || this.mInspSkillLvUpCostParam.Count <= 0)
        return -1;
      InspSkillLvUpCostParam skillLvUpCostParam = this.mInspSkillLvUpCostParam.Find((Predicate<InspSkillLvUpCostParam>) (item => item.id == id));
      if (skillLvUpCostParam == null || skillLvUpCostParam.costs == null || skillLvUpCostParam.costs.Count <= 0)
        return -1;
      int num1 = 0;
      int num2 = current_lv - 1;
      int num3 = num2 + up;
      for (int index1 = num2; index1 < num3; ++index1)
      {
        int index2 = index1;
        if (index2 >= skillLvUpCostParam.costs.Count)
          index2 = skillLvUpCostParam.costs.Count - 1;
        num1 += skillLvUpCostParam.costs[index2].gold;
      }
      return num1;
    }

    public List<UnitParam> GetAllPlayableUnitParams()
    {
      return this.mUnitParam.Where<UnitParam>((Func<UnitParam, bool>) (unit => !string.IsNullOrEmpty(unit.piece) || unit.IsHero())).ToList<UnitParam>();
    }
  }
}
