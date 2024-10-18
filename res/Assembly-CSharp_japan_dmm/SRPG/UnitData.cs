// Decompiled with JetBrains decompiler
// Type: SRPG.UnitData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using MessagePack;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  public class UnitData
  {
    public const int MAX_JOB = 4;
    public const int MAX_MASTER_ABILITY = 1;
    public const int MAX_JOB_ABILITY = 5;
    public const int MAX_EQUIP_ABILITY = 5;
    public const int MAX_USED_ABILITY = 11;
    public const int COMBAT_POWER_SPD_MUL_VALUE = 5;
    public const int COMBAT_POWER_HP_DIV_VALUE = 10;
    private long mUniqueID;
    private UnitParam mUnitParam;
    private BaseStatus mStatus = new BaseStatus();
    public float LastSyncTime;
    private OInt mLv = (OInt) 1;
    private OInt mExp = (OInt) 0;
    private OInt mRarity = (OInt) 0;
    private OInt mAwakeLv = (OInt) 0;
    private OInt mElement = (OInt) 0;
    private bool mFavorite;
    private EElement mSupportElement;
    private OBool mIsRental = (OBool) false;
    private OInt mRentalFavoritePoint = (OInt) 0;
    private string mRentalIname = string.Empty;
    private UnitRentalParam mUnitRentalParam;
    private bool mIsNotFindUniqueID;
    private JobData[] mJobs;
    private OInt mJobIndex = (OInt) 0;
    private long[] mPartyJobs;
    public UnitData.TemporaryFlags TempFlags;
    private List<ArtifactParam> mUnlockedSkins = new List<ArtifactParam>();
    private SkillData mLeaderSkill;
    private AbilityData mMasterAbility;
    private AbilityData mCollaboAbility;
    private AbilityData mMapEffectAbility;
    private List<AbilityData> mLearnAbilitys = new List<AbilityData>();
    private List<AbilityData> mBattleAbilitys = new List<AbilityData>(11);
    private List<SkillData> mBattleSkills = new List<SkillData>(11);
    private List<AbilityData> mTobiraMasterAbilitys = new List<AbilityData>();
    private SkillData mNormalAttackSkill;
    private QuestClearUnlockUnitDataParam mUnlockedLeaderSkill;
    private List<QuestClearUnlockUnitDataParam> mUnlockedAbilitys;
    private List<QuestClearUnlockUnitDataParam> mUnlockedSkills;
    private List<QuestClearUnlockUnitDataParam> mSkillUnlocks;
    private Dictionary<string, UnitJobOverwriteParam> mUnitJobOverwriteParams;
    private ConceptCardData[] mConceptCards = new ConceptCardData[2];
    private List<SRPG.TobiraData> mTobiraData = new List<SRPG.TobiraData>();
    private OInt mUnlockTobiraNum = (OInt) 0;
    private RuneData[] mEquipRunes = new RuneData[6];
    private Dictionary<string, SkillAbilityDeriveData> mJobSkillAbilityDeriveData;
    private bool mLeaderSkillIsConceptCard;
    private int mNumJobsAvailable;
    public UnitBadgeTypes BadgeState;
    private static BaseStatus UnitScaleStatus = new BaseStatus();
    private static BaseStatus WorkScaleStatus = new BaseStatus();
    private static readonly Dictionary<string, string[]> CONDITIONS_TARGET_NAMES = new Dictionary<string, string[]>()
    {
      {
        "UNIT_JOB_MASTER1",
        new string[3]{ "voice_0017", "voice_0018", "voice_0019" }
      },
      {
        "UNIT_LEVEL85",
        new string[10]
        {
          "chara_0002",
          "chara_0003",
          "chara_0004",
          "chara_0005",
          "chara_0006",
          "chara_0007",
          "chara_0008",
          "chara_0009",
          "chara_0010",
          "chara_0011"
        }
      },
      {
        "UNIT_LEVEL75",
        new string[8]
        {
          "sys_0001",
          "sys_0002",
          "sys_0003",
          "sys_0033",
          "sys_0035",
          "sys_0046",
          "sys_0047",
          "sys_0050"
        }
      },
      {
        "UNIT_LEVEL65",
        new string[9]
        {
          "sys_0009",
          "sys_0013",
          "sys_0015",
          "sys_0017",
          "sys_0019",
          "sys_0023",
          "sys_0024",
          "sys_0026",
          "sys_0028"
        }
      }
    };
    private List<QuestParam> mCharacterQuests;

    public QuestClearUnlockUnitDataParam QuestUnlockedLeaderSkill => this.mUnlockedLeaderSkill;

    public List<QuestClearUnlockUnitDataParam> QuestUnlockedAbilitys => this.mUnlockedAbilitys;

    public List<QuestClearUnlockUnitDataParam> QuestUnlockedSkills => this.mUnlockedSkills;

    public List<QuestClearUnlockUnitDataParam> SkillUnlocks => this.mSkillUnlocks;

    public bool IsMyUnit
    {
      get
      {
        return MonoSingleton<GameManager>.Instance.Player.Units.FindIndex((Predicate<UnitData>) (u => u.UniqueID == this.UniqueID)) >= 0;
      }
    }

    public long UniqueID => this.mUniqueID;

    public UnitParam UnitParam => this.mUnitParam;

    public string UnitID => this.UnitParam != null ? this.UnitParam.iname : (string) null;

    public BaseStatus Status => this.mStatus;

    public int Lv => (int) this.mLv;

    public int Exp => (int) this.mExp;

    public int Rarity => (int) this.mRarity;

    public int AwakeLv => (int) this.mAwakeLv;

    public EquipData[] CurrentEquips
    {
      get => this.CurrentJob != null ? this.CurrentJob.Equips : (EquipData[]) null;
    }

    public SkillData LeaderSkill => this.mLeaderSkill;

    public AbilityData MasterAbility => this.mMasterAbility;

    public AbilityData CollaboAbility => this.mCollaboAbility;

    public AbilityData MapEffectAbility => this.mMapEffectAbility;

    public List<AbilityData> TobiraMasterAbilitys => this.mTobiraMasterAbilitys;

    public long[] CurrentAbilitySlots
    {
      get => this.CurrentJob != null ? this.CurrentJob.AbilitySlots : (long[]) null;
    }

    public List<AbilityData> LearnAbilitys => this.mLearnAbilitys;

    public List<AbilityData> BattleAbilitys => this.mBattleAbilitys;

    public List<SkillData> BattleSkills => this.mBattleSkills;

    public SkillData CurrentLeaderSkill
    {
      get
      {
        return this.IsEquipConceptLeaderSkill() && this.mConceptCards[0] != null ? this.mConceptCards[0].LeaderSkill : this.LeaderSkill;
      }
    }

    public EElement Element => (EElement) (int) this.mElement;

    public JobTypes JobType
    {
      get
      {
        if (this.CurrentJob != null)
          return this.CurrentJob.JobType;
        return this.UnitParam.no_job_status != null ? this.UnitParam.no_job_status.jobtype : JobTypes.None;
      }
    }

    public RoleTypes RoleType
    {
      get
      {
        if (this.CurrentJob != null)
          return this.CurrentJob.RoleType;
        return this.UnitParam.no_job_status != null ? this.UnitParam.no_job_status.role : RoleTypes.None;
      }
    }

    public ConceptCardData[] ConceptCards
    {
      get => this.mConceptCards;
      set => this.mConceptCards = value;
    }

    public ConceptCardData MainConceptCard
    {
      get
      {
        return this.mConceptCards == null || this.mConceptCards.Length < 1 ? (ConceptCardData) null : this.mConceptCards[0];
      }
    }

    public RuneData[] EquipRunes
    {
      get => this.mEquipRunes;
      set => this.mEquipRunes = value;
    }

    public RuneData EquipRune(int index)
    {
      if (index < this.mEquipRunes.Length)
        return this.mEquipRunes[index];
      DebugUtility.LogError("EquipRune の index が" + (object) index + "で範囲外です");
      return (RuneData) null;
    }

    public bool IsRiding
    {
      get
      {
        return this.CurrentJob != null && this.CurrentJob.Param != null && this.CurrentJob.Param.IsRiding;
      }
    }

    public int NumJobsAvailable => this.mNumJobsAvailable;

    public bool IsJobAvailable(int jobNo) => 0 <= jobNo && jobNo < this.mNumJobsAvailable;

    public bool IsEquipConceptLeaderSkill()
    {
      ConceptCardData mainConceptCard = this.MainConceptCard;
      return mainConceptCard != null && mainConceptCard.LeaderSkillIsAvailable() && this.mLeaderSkillIsConceptCard;
    }

    public JobData CurrentJob
    {
      get
      {
        return this.mJobs == null || (int) this.mJobIndex < 0 || this.mJobs.Length <= (int) this.mJobIndex ? (JobData) null : this.mJobs[(int) this.mJobIndex];
      }
    }

    public string CurrentJobId => this.CurrentJob != null ? this.CurrentJob.JobID : string.Empty;

    public bool IsUnlockTobira
    {
      get
      {
        SRPG.TobiraData tobiraData = this.mTobiraData.Find((Predicate<SRPG.TobiraData>) (tobira => tobira.Param != null && tobira.Param.TobiraCategory == TobiraParam.Category.START));
        return tobiraData != null && tobiraData.IsUnlocked;
      }
    }

    public List<SRPG.TobiraData> TobiraData => this.mTobiraData;

    public int UnlockTobriaNum => (int) this.mUnlockTobiraNum;

    public JobData GetBaseJob(string jobID)
    {
      List<JobSetParam> baseJobs = this.FindBaseJobs(jobID);
      if (baseJobs == null || baseJobs.Count <= 0)
        return (JobData) null;
      for (int i = 0; i < this.mNumJobsAvailable; ++i)
      {
        if (baseJobs.FindIndex((Predicate<JobSetParam>) (baseJob => baseJob.job == this.mJobs[i].JobID)) >= 0)
          return this.mJobs[i];
      }
      return (JobData) null;
    }

    public List<JobData> GetBaseJobs(bool onlyActivated)
    {
      List<JobData> baseJobs = new List<JobData>();
      for (int index = 0; index < this.mJobs.Length; ++index)
      {
        if (this.GetBaseJob(this.mJobs[index].JobID) == null)
          baseJobs.Add(this.mJobs[index]);
      }
      return baseJobs;
    }

    public JobData FindJobDataBySkillData(SkillParam param)
    {
      if (param == null || this.mJobs == null)
        return (JobData) null;
      string hitAbliId = string.Empty;
      for (int index = 0; index < this.BattleAbilitys.Count; ++index)
      {
        if (Array.FindIndex<SkillData>(this.BattleAbilitys[index].Skills.ToArray(), (Predicate<SkillData>) (p => p.SkillID == param.iname)) != -1)
          hitAbliId = this.BattleAbilitys[index].AbilityID;
      }
      if (!string.IsNullOrEmpty(hitAbliId))
      {
        for (int index = 0; index < this.mJobs.Length; ++index)
        {
          JobData mJob = this.mJobs[index];
          if (mJob != null)
          {
            for (int rank = 0; rank < JobParam.MAX_JOB_RANK; ++rank)
            {
              OString[] learningAbilitys = mJob.GetLearningAbilitys(rank);
              if (learningAbilitys != null && Array.FindIndex<OString>(learningAbilitys, (Predicate<OString>) (name => (string) name == hitAbliId)) != -1)
                return mJob;
            }
            if (mJob.Param != null && mJob.Param.fixed_ability == hitAbliId)
              return mJob;
          }
        }
      }
      if (!string.IsNullOrEmpty(param.job) && this.mMasterAbility != null && this.mMasterAbility.LearningSkills != null)
      {
        for (int index1 = 0; index1 < this.mMasterAbility.LearningSkills.Length; ++index1)
        {
          LearningSkill learningSkill = this.mMasterAbility.LearningSkills[index1];
          if (learningSkill.iname != null && learningSkill.iname == param.iname)
          {
            for (int index2 = 0; index2 < this.mJobs.Length; ++index2)
            {
              if (this.mJobs[index2] != null && this.mJobs[index2].JobID == param.job)
                return this.mJobs[index2];
            }
          }
        }
      }
      if (!string.IsNullOrEmpty(param.job) && this.mMapEffectAbility != null && this.mMapEffectAbility.LearningSkills != null)
      {
        foreach (LearningSkill learningSkill in this.mMapEffectAbility.LearningSkills)
        {
          if (learningSkill.iname != null && learningSkill.iname == param.iname)
          {
            for (int index = 0; index < this.mJobs.Length; ++index)
            {
              if (this.mJobs[index] != null && this.mJobs[index].JobID == param.job)
                return this.mJobs[index];
            }
          }
        }
      }
      return (JobData) null;
    }

    public ArtifactData FindArtifactDataBySkillParam(SkillParam param)
    {
      if (param == null || this.mJobs == null)
        return (ArtifactData) null;
      for (int index1 = 0; index1 < this.mJobs.Length; ++index1)
      {
        JobData mJob = this.mJobs[index1];
        if (mJob != null && mJob.ArtifactDatas != null)
        {
          for (int index2 = 0; index2 < mJob.ArtifactDatas.Length; ++index2)
          {
            ArtifactData artifactData = mJob.ArtifactDatas[index2];
            if (artifactData != null && artifactData.LearningAbilities != null)
            {
              for (int index3 = 0; index3 < artifactData.LearningAbilities.Count; ++index3)
              {
                AbilityData learningAbility = artifactData.LearningAbilities[index3];
                if (learningAbility != null && learningAbility.LearningSkills != null)
                {
                  for (int index4 = 0; index4 < learningAbility.LearningSkills.Length; ++index4)
                  {
                    if (learningAbility.LearningSkills[index4].iname == param.iname)
                      return artifactData;
                  }
                }
              }
            }
          }
        }
      }
      return (ArtifactData) null;
    }

    public JobData[] Jobs => this.mJobs;

    public int JobCount => this.mJobs.Length;

    public int JobIndex => (int) this.mJobIndex;

    public string SexPrefix => this.UnitParam.SexPrefix;

    public bool IsFavorite
    {
      set => this.mFavorite = value;
      get => this.mFavorite;
    }

    public EElement SupportElement => this.mSupportElement;

    public bool IsRental => (bool) this.mIsRental;

    public int RentalFavoritePoint
    {
      get
      {
        if (!(bool) this.mIsRental || this.UnitRentalParam == null)
          return 0;
        int val1 = (int) this.mRentalFavoritePoint;
        if (this.UnitRentalParam.UnitQuestInfo != null && this.UnitRentalParam.UnitQuestInfo.Count > 0)
        {
          for (int index = 0; index < this.UnitRentalParam.UnitQuestInfo.Count; ++index)
          {
            RentalQuestInfo rentalQuestInfo = this.UnitRentalParam.UnitQuestInfo[index];
            if (rentalQuestInfo != null && rentalQuestInfo.Quest != null && rentalQuestInfo.Quest.state != QuestStates.Cleared)
            {
              val1 = Math.Min(val1, (int) rentalQuestInfo.Point);
              break;
            }
          }
        }
        return Math.Min(val1, (int) this.UnitRentalParam.PtMax);
      }
    }

    public string RentalIname => this.mRentalIname;

    private UnitRentalParam UnitRentalParam
    {
      get
      {
        if (this.mUnitRentalParam == null)
          this.mUnitRentalParam = UnitRentalParam.GetParam(this.mRentalIname);
        return this.mUnitRentalParam;
      }
    }

    public bool IsNotFindUniqueID
    {
      get => this.mIsNotFindUniqueID;
      set => this.mIsNotFindUniqueID = value;
    }

    public bool IsIntoUnit => this.UnitParam != null && this.UnitParam.IsStopped();

    public bool IsSkinUnlocked()
    {
      return this.mUnlockedSkins.Count + this.GetEnableConceptCardSkins().Length >= 1;
    }

    public bool IsSetSkin(int jobIndex = -1)
    {
      if (jobIndex == -1)
        jobIndex = (int) this.mJobIndex;
      return this.mJobs != null && this.mJobs[this.JobIndex] != null && !string.IsNullOrEmpty(this.mJobs[this.JobIndex].SelectedSkin);
    }

    public ArtifactParam GetSelectedSkinData(int jobIndex = -1)
    {
      if (jobIndex == -1)
        jobIndex = (int) this.mJobIndex;
      return this.mJobs == null || this.mJobs[jobIndex] == null || string.IsNullOrEmpty(this.mJobs[jobIndex].SelectedSkin) ? (ArtifactParam) null : Array.Find<ArtifactParam>(MonoSingleton<GameManager>.Instance.MasterParam.Artifacts.ToArray(), (Predicate<ArtifactParam>) (p => p.iname == this.mJobs[jobIndex].SelectedSkin));
    }

    public ArtifactParam GetSelectedSkin(int jobIndex = -1)
    {
      if (jobIndex == -1)
        jobIndex = (int) this.mJobIndex;
      return this.GetSelectedSkinData(jobIndex);
    }

    public ArtifactParam[] GetSelectedSkins()
    {
      if (this.Jobs == null && this.Jobs.Length < 1)
        return new ArtifactParam[0];
      ArtifactParam[] selectedSkins = new ArtifactParam[this.Jobs.Length];
      for (int jobIndex = 0; jobIndex < selectedSkins.Length; ++jobIndex)
        selectedSkins[jobIndex] = this.GetSelectedSkinData(jobIndex);
      return selectedSkins;
    }

    public void SetJobSkin(string afName, int jobIndex = -1, bool is_need_check = true)
    {
      if (jobIndex == -1)
        jobIndex = (int) this.mJobIndex;
      if (string.IsNullOrEmpty(afName))
      {
        this.mJobs[jobIndex].SelectedSkin = (string) null;
      }
      else
      {
        ArtifactParam af = Array.Find<ArtifactParam>(MonoSingleton<GameManager>.Instance.MasterParam.Artifacts.ToArray(), (Predicate<ArtifactParam>) (p => p.iname == afName));
        if (af == null)
          return;
        if (Array.FindIndex<ArtifactParam>(this.GetEnableConceptCardSkins(), (Predicate<ArtifactParam>) (artifact => artifact.iname == af.iname)) >= 0)
        {
          this.mJobs[jobIndex].SelectedSkin = afName;
        }
        else
        {
          if (is_need_check && (!af.CheckEnableEquip(this, jobIndex) || !this.CheckUsedSkin(af.iname)))
            return;
          this.mJobs[jobIndex].SelectedSkin = afName;
        }
      }
    }

    public void SetJobSkinAll(string afName, bool isSelf = true)
    {
      if (string.IsNullOrEmpty(afName))
      {
        this.ResetJobSkinAll();
      }
      else
      {
        ArtifactParam artifactParam = Array.Find<ArtifactParam>(MonoSingleton<GameManager>.Instance.MasterParam.Artifacts.ToArray(), (Predicate<ArtifactParam>) (p => p.iname == afName));
        if (artifactParam == null)
        {
          this.ResetJobSkinAll();
        }
        else
        {
          if (isSelf && !this.CheckUsedSkin(artifactParam.iname))
            return;
          for (int jobIndex = 0; jobIndex < this.mJobs.Length; ++jobIndex)
            this.mJobs[jobIndex].SelectedSkin = !artifactParam.CheckEnableEquip(this, jobIndex) ? (string) null : afName;
        }
      }
    }

    public void ResetJobSkin(int jobIndex = -1)
    {
      if (jobIndex == -1)
        jobIndex = (int) this.mJobIndex;
      this.mJobs[jobIndex].SelectedSkin = (string) null;
    }

    public void ResetJobSkinAll()
    {
      for (int index = 0; index < this.mJobs.Length; ++index)
        this.mJobs[index].SelectedSkin = (string) null;
    }

    public ArtifactParam[] GetSelectableSkins(JobParam jobParam)
    {
      if (jobParam == null)
        return (ArtifactParam[]) null;
      int index = Array.FindIndex<JobData>(this.mJobs, (Predicate<JobData>) (j => j.Param.iname == jobParam.iname));
      return index == -1 ? (ArtifactParam[]) null : this.GetSelectableSkins(index);
    }

    public ArtifactParam[] GetSelectableSkins(int jobIndex = -1)
    {
      if (jobIndex == -1)
        jobIndex = (int) this.mJobIndex;
      List<ArtifactParam> artifactParamList = new List<ArtifactParam>();
      for (int index = 0; index < this.mUnlockedSkins.Count; ++index)
      {
        ArtifactParam mUnlockedSkin = this.mUnlockedSkins[index];
        if (mUnlockedSkin != null && mUnlockedSkin.CheckEnableEquip(this, this.JobIndex))
          artifactParamList.Add(mUnlockedSkin);
      }
      return artifactParamList.Count >= 1 ? artifactParamList.ToArray() : new ArtifactParam[0];
    }

    public ArtifactParam[] GetAllSkins(int jobIndex = -1)
    {
      if (this.mUnitParam.skins == null)
        return new ArtifactParam[0];
      if (jobIndex == -1)
        jobIndex = (int) this.mJobIndex;
      List<ArtifactParam> artifactParamList = new List<ArtifactParam>();
      ArtifactParam[] array = MonoSingleton<GameManager>.Instance.MasterParam.Artifacts.ToArray();
      for (int i = 0; i < this.mUnitParam.skins.Length; ++i)
      {
        ArtifactParam artifactParam = Array.Find<ArtifactParam>(array, (Predicate<ArtifactParam>) (af => af.iname == this.mUnitParam.skins[i]));
        if (artifactParam != null && artifactParam.CheckEnableEquip(this, this.JobIndex))
          artifactParamList.Add(artifactParam);
      }
      return artifactParamList.Count >= 1 ? artifactParamList.ToArray() : new ArtifactParam[0];
    }

    public ArtifactParam[] GetEnableConceptCardSkins(int jobIndex = -1)
    {
      if (jobIndex == -1)
        jobIndex = (int) this.mJobIndex;
      List<ArtifactParam> artifactParamList = new List<ArtifactParam>();
      for (int index = 0; index < MonoSingleton<GameManager>.Instance.Player.SkinConceptCards.Count; ++index)
      {
        ConceptCardParam card_param = MonoSingleton<GameManager>.Instance.Player.SkinConceptCards[index].param;
        if (card_param.effects != null)
        {
          for (int j = 0; j < card_param.effects.Length; ++j)
          {
            if (!string.IsNullOrEmpty(card_param.effects[j].skin))
            {
              ConceptCardConditionsParam conceptCardConditions = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardConditions(card_param.effects[j].cnds_iname);
              if (conceptCardConditions == null || conceptCardConditions.IsMatchConditions(this.mUnitParam, this.mJobs[jobIndex]))
              {
                ArtifactParam artifactParam = MonoSingleton<GameManager>.Instance.MasterParam.Artifacts.Find((Predicate<ArtifactParam>) (art => art.iname == card_param.effects[j].skin));
                if (artifactParam != null && !artifactParamList.Contains(artifactParam))
                  artifactParamList.Add(artifactParam);
              }
            }
          }
        }
      }
      return artifactParamList.ToArray();
    }

    public QuestClearUnlockUnitDataParam[] UnlockedSkills
    {
      get
      {
        return this.mUnlockedSkills != null ? this.mUnlockedSkills.ToArray() : (QuestClearUnlockUnitDataParam[]) null;
      }
    }

    public string UnlockedSkillIds()
    {
      string empty = string.Empty;
      if (this.mUnlockedLeaderSkill != null)
        empty += this.mUnlockedLeaderSkill.iname;
      if (this.mUnlockedAbilitys != null)
      {
        for (int index = 0; index < this.mUnlockedAbilitys.Count; ++index)
          empty += string.IsNullOrEmpty(empty) ? this.mUnlockedAbilitys[index].iname : "," + this.mUnlockedAbilitys[index].iname;
      }
      if (this.mUnlockedSkills != null)
      {
        for (int index = 0; index < this.mUnlockedSkills.Count; ++index)
          empty += string.IsNullOrEmpty(empty) ? this.mUnlockedSkills[index].iname : "," + this.mUnlockedSkills[index].iname;
      }
      return empty;
    }

    public QuestClearUnlockUnitDataParam[] UnlockedSkillDiff(string oldIds)
    {
      string str1 = this.UnlockedSkillIds();
      string str2 = !string.IsNullOrEmpty(oldIds) ? oldIds : string.Empty;
      string str3 = !string.IsNullOrEmpty(str1) ? str1 : string.Empty;
      if (str2.Length >= str3.Length)
        return new QuestClearUnlockUnitDataParam[0];
      string[] array = str2.Split(',');
      string[] newUnlocks = str3.Split(',');
      MasterParam masterParam = MonoSingleton<GameManager>.Instance.MasterParam;
      List<QuestClearUnlockUnitDataParam> unlockUnitDataParamList = new List<QuestClearUnlockUnitDataParam>();
      for (int i = 0; i < newUnlocks.Length; ++i)
      {
        if (Array.FindIndex<string>(array, (Predicate<string>) (p => p == newUnlocks[i])) == -1)
        {
          QuestClearUnlockUnitDataParam unlockUnitData = masterParam.GetUnlockUnitData(newUnlocks[i]);
          if (unlockUnitData != null)
            unlockUnitDataParamList.Add(unlockUnitData);
        }
      }
      return unlockUnitDataParamList.ToArray();
    }

    public bool IsQuestClearUnlocked(string id, QuestClearUnlockUnitDataParam.EUnlockTypes type)
    {
      if (!string.IsNullOrEmpty(id))
      {
        QuestClearUnlockUnitDataParam unlockUnitDataParam = (QuestClearUnlockUnitDataParam) null;
        switch (type)
        {
          case QuestClearUnlockUnitDataParam.EUnlockTypes.Skill:
            if (this.mUnlockedSkills != null)
            {
              unlockUnitDataParam = this.mUnlockedSkills.Find((Predicate<QuestClearUnlockUnitDataParam>) (p => p.new_id == id && p.type == type));
              break;
            }
            break;
          case QuestClearUnlockUnitDataParam.EUnlockTypes.Ability:
          case QuestClearUnlockUnitDataParam.EUnlockTypes.MasterAbility:
            if (this.mUnlockedAbilitys != null)
            {
              unlockUnitDataParam = this.mUnlockedAbilitys.Find((Predicate<QuestClearUnlockUnitDataParam>) (p => p.new_id == id && p.type == type));
              break;
            }
            break;
          case QuestClearUnlockUnitDataParam.EUnlockTypes.LeaderSkill:
            if (this.mUnlockedLeaderSkill != null)
            {
              unlockUnitDataParam = this.mUnlockedLeaderSkill;
              break;
            }
            break;
        }
        if (unlockUnitDataParam != null)
          return true;
      }
      return false;
    }

    public List<string> GetQuestUnlockConditions(QuestParam quest)
    {
      if (this.mSkillUnlocks == null)
        return (List<string>) null;
      List<string> unlockConditions = new List<string>();
      for (int index = 0; index < this.mSkillUnlocks.Count; ++index)
      {
        QuestClearUnlockUnitDataParam mSkillUnlock = this.mSkillUnlocks[index];
        if (!(mSkillUnlock.uid != this.UnitID) && Array.FindIndex<string>(mSkillUnlock.qids, (Predicate<string>) (p => p == quest.iname)) != -1)
        {
          string condText = this.mSkillUnlocks[index].GetCondText(this.mUnitParam);
          if (!string.IsNullOrEmpty(condText))
            unlockConditions.Add(condText);
        }
      }
      return unlockConditions;
    }

    private Json_Job[] AppendUnlockedJobs(Json_Job[] jobs)
    {
      if (jobs == null || this.mUnitParam.jobsets == null)
        return jobs;
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      long val1 = 0;
      for (int index = 0; index < jobs.Length; ++index)
        val1 = Math.Max(val1, jobs[index].iid);
      List<JobSetParam> jobSetParamList = new List<JobSetParam>();
      for (int index = 0; index < this.mUnitParam.jobsets.Length; ++index)
      {
        JobSetParam jobSetParam = instanceDirect.GetJobSetParam(this.mUnitParam.jobsets[index]);
        if (jobSetParam != null)
          jobSetParamList.Add(jobSetParam);
      }
      JobSetParam[] changeJobSetParam = instanceDirect.GetClassChangeJobSetParam(this.mUnitParam.iname);
      if (changeJobSetParam != null)
      {
        List<JobSetParam> result = new List<JobSetParam>();
        List<JobSetParam> tmp = new List<JobSetParam>((IEnumerable<JobSetParam>) changeJobSetParam);
        tmp.ForEach((Action<JobSetParam>) (t =>
        {
          if (tmp.Find((Predicate<JobSetParam>) (ccjob => ccjob.iname == t.jobchange)) != null)
            return;
          result.Add(t);
        }));
        result.ForEach((Action<JobSetParam>) (r => tmp.Remove(r)));
        for (int i = 0; i < result.Count; ++i)
        {
          JobSetParam jobSetParam = tmp.Find((Predicate<JobSetParam>) (t => t.jobchange == result[i].iname));
          if (jobSetParam != null)
          {
            result.Add(jobSetParam);
            tmp.Remove(jobSetParam);
            if (tmp.Count <= 0)
              break;
          }
        }
        jobSetParamList.AddRange((IEnumerable<JobSetParam>) result);
      }
      List<Json_Job> jsonJobList = new List<Json_Job>((IEnumerable<Json_Job>) jobs);
      for (int index = 0; index < jobSetParamList.Count; ++index)
      {
        JobSetParam checkJobset = jobSetParamList[index];
        Json_Job jsonJob = Array.Find<Json_Job>(jobs, (Predicate<Json_Job>) (_job => _job != null && _job.iname == checkJobset.job));
        Json_Job removeTarget;
        if (jsonJob == null)
          jsonJobList.Add(new Json_Job()
          {
            iname = checkJobset.job,
            iid = ++val1
          });
        else if (!string.IsNullOrEmpty(checkJobset.jobchange) && jsonJob.rank != 0)
        {
          for (JobSetParam beforeJobset = instanceDirect.GetJobSetParam(checkJobset.jobchange); beforeJobset != null; beforeJobset = instanceDirect.GetJobSetParam(jobSetParamList.Find((Predicate<JobSetParam>) (_jobset => _jobset.job == removeTarget.iname)).jobchange))
          {
            removeTarget = jsonJobList.Find((Predicate<Json_Job>) (_job => _job.iname == beforeJobset.job));
            if (removeTarget != null)
              jsonJobList.Remove(removeTarget);
            else
              break;
          }
        }
      }
      return jsonJobList.ToArray();
    }

    private void SetSkinLockedJob(Json_Job[] jobs)
    {
      if (jobs == null)
        return;
      string str = string.Empty;
      for (int index = 0; index < jobs.Length; ++index)
      {
        if (jobs[index] != null && !string.IsNullOrEmpty(jobs[index].cur_skin))
        {
          str = jobs[index].cur_skin;
          break;
        }
      }
      if (string.IsNullOrEmpty(str))
        return;
      for (int index = 0; index < jobs.Length; ++index)
      {
        if (jobs[index] != null)
          jobs[index].cur_skin = str;
      }
    }

    public bool CheckUsedSkin(string afName)
    {
      if (string.IsNullOrEmpty(afName) || this.mUnlockedSkins == null || this.mUnlockedSkins.Count < 0)
        return false;
      bool flag = false;
      for (int index = 0; index < this.mUnlockedSkins.Count; ++index)
      {
        ArtifactParam mUnlockedSkin = this.mUnlockedSkins[index];
        if (mUnlockedSkin != null && mUnlockedSkin.iname == afName)
        {
          flag = true;
          break;
        }
      }
      return flag;
    }

    public void Deserialize(Json_Unit json)
    {
      if (json == null)
        throw new InvalidJSONException();
      this.UpdateSyncTime();
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      this.mUnitParam = instanceDirect.GetUnitParam(json.iname);
      this.mUniqueID = json.iid;
      this.mRarity = (OInt) Math.Min(Math.Max(json.rare, (int) this.mUnitParam.rare), (int) this.mUnitParam.raremax);
      this.mAwakeLv = (OInt) json.plus;
      this.mUnlockTobiraNum = (OInt) TobiraUtility.GetUnlockTobiraNum(json.doors);
      this.mElement = (OInt) (int) this.mUnitParam.element;
      this.mExp = (OInt) json.exp;
      this.mLv = (OInt) this.CalcLevel();
      this.mJobs = (JobData[]) null;
      this.mJobIndex = (OInt) 0;
      this.mFavorite = json.fav == 1;
      this.mSupportElement = (EElement) json.elem;
      if (json.concept_cards != null)
      {
        this.mLeaderSkillIsConceptCard = false;
        for (int index = 0; index < json.concept_cards.Length; ++index)
        {
          JSON_ConceptCard conceptCard = json.concept_cards[index];
          if (conceptCard != null && !conceptCard.IsEmptyDummyData && conceptCard.EnableLeaderSkill)
          {
            this.mLeaderSkillIsConceptCard = true;
            break;
          }
        }
      }
      this.mPartyJobs = (long[]) null;
      if (json.select != null && json.select.quests != null)
      {
        this.mPartyJobs = new long[17];
        for (int index = 0; index < json.select.quests.Length; ++index)
        {
          string qtype = json.select.quests[index].qtype;
          if (!string.IsNullOrEmpty(qtype))
            this.mPartyJobs[(int) PartyData.GetPartyTypeFromString(qtype)] = json.select.quests[index].jiid;
        }
      }
      this.mUnlockedLeaderSkill = (QuestClearUnlockUnitDataParam) null;
      this.mUnlockedAbilitys = (List<QuestClearUnlockUnitDataParam>) null;
      this.mUnlockedSkills = (List<QuestClearUnlockUnitDataParam>) null;
      this.mSkillUnlocks = (List<QuestClearUnlockUnitDataParam>) null;
      if (json.quest_clear_unlocks != null && json.quest_clear_unlocks.Length >= 1)
      {
        QuestClearUnlockUnitDataParam[] unlockUnitDataParamArray = new QuestClearUnlockUnitDataParam[json.quest_clear_unlocks.Length];
        for (int index = 0; index < json.quest_clear_unlocks.Length; ++index)
        {
          QuestClearUnlockUnitDataParam unlockUnitData = instanceDirect.MasterParam.GetUnlockUnitData(json.quest_clear_unlocks[index]);
          if (unlockUnitData != null)
            unlockUnitDataParamArray[index] = unlockUnitData;
        }
        if (unlockUnitDataParamArray.Length >= 1)
        {
          List<int> intList = new List<int>();
          for (int index1 = 0; index1 < unlockUnitDataParamArray.Length; ++index1)
          {
            if (unlockUnitDataParamArray[index1] != null && !unlockUnitDataParamArray[index1].add)
            {
              for (int index2 = 0; index2 < unlockUnitDataParamArray.Length; ++index2)
              {
                if (unlockUnitDataParamArray[index2] != null && index1 != index2 && unlockUnitDataParamArray[index1].old_id == unlockUnitDataParamArray[index2].new_id)
                {
                  if (unlockUnitDataParamArray[index2].add)
                  {
                    unlockUnitDataParamArray[index2] = (QuestClearUnlockUnitDataParam) null;
                    break;
                  }
                  intList.Add(index2);
                  break;
                }
              }
            }
          }
          for (int index = 0; index < intList.Count; ++index)
            unlockUnitDataParamArray[intList[index]] = (QuestClearUnlockUnitDataParam) null;
          for (int index = 0; index < unlockUnitDataParamArray.Length; ++index)
          {
            if (unlockUnitDataParamArray[index] != null)
            {
              if (unlockUnitDataParamArray[index].type == QuestClearUnlockUnitDataParam.EUnlockTypes.LeaderSkill)
              {
                if (this.mUnlockedLeaderSkill == null)
                  this.mUnlockedLeaderSkill = new QuestClearUnlockUnitDataParam();
                this.mUnlockedLeaderSkill = unlockUnitDataParamArray[index];
              }
              else if (unlockUnitDataParamArray[index].type == QuestClearUnlockUnitDataParam.EUnlockTypes.Ability || unlockUnitDataParamArray[index].type == QuestClearUnlockUnitDataParam.EUnlockTypes.MasterAbility)
              {
                if (this.mUnlockedAbilitys == null)
                  this.mUnlockedAbilitys = new List<QuestClearUnlockUnitDataParam>();
                this.mUnlockedAbilitys.Add(unlockUnitDataParamArray[index]);
              }
              else if (unlockUnitDataParamArray[index].type == QuestClearUnlockUnitDataParam.EUnlockTypes.Skill)
              {
                if (this.mUnlockedSkills == null)
                  this.mUnlockedSkills = new List<QuestClearUnlockUnitDataParam>();
                this.mUnlockedSkills.Add(unlockUnitDataParamArray[index]);
              }
            }
          }
        }
      }
      QuestClearUnlockUnitDataParam[] allUnlockUnitDatas = instanceDirect.MasterParam.GetAllUnlockUnitDatas();
      if (allUnlockUnitDatas != null)
      {
        this.mSkillUnlocks = new List<QuestClearUnlockUnitDataParam>();
        for (int index = 0; index < allUnlockUnitDatas.Length; ++index)
        {
          if (allUnlockUnitDatas[index].uid == this.UnitID)
            this.mSkillUnlocks.Add(allUnlockUnitDatas[index]);
        }
      }
      if (this.UnitParam.skins != null)
      {
        this.mUnlockedSkins.Clear();
        ArtifactParam[] array = instanceDirect.MasterParam.Artifacts.ToArray();
        for (int index = 0; index < this.UnitParam.skins.Length; ++index)
        {
          string skinName = this.UnitParam.skins[index];
          ArtifactParam artifactParam = Array.Find<ArtifactParam>(array, (Predicate<ArtifactParam>) (af => af.iname == skinName));
          if (artifactParam != null && instanceDirect.Player.ItemEntryExists(artifactParam.kakera))
            this.mUnlockedSkins.Add(artifactParam);
        }
      }
      if (json.jobs != null)
        json.jobs = this.AppendUnlockedJobs(json.jobs);
      if (json.jobs != null)
        this.SetSkinLockedJob(json.jobs);
      this.mMapEffectAbility = (AbilityData) null;
      if (json.jobs != null)
      {
        List<Json_Job> collection = new List<Json_Job>((IEnumerable<Json_Job>) json.jobs);
        List<Json_Job> jsonJobList = new List<Json_Job>(json.jobs.Length);
        JobSetParam[] changeJobSetParam = instanceDirect.GetClassChangeJobSetParam(this.mUnitParam.iname);
        for (int index = 0; index < this.mUnitParam.jobsets.Length; ++index)
        {
          for (JobSetParam targetJobset = instanceDirect.GetJobSetParam(this.mUnitParam.jobsets[index]); targetJobset != null; targetJobset = Array.Find<JobSetParam>(changeJobSetParam, (Predicate<JobSetParam>) (_cc => _cc.jobchange == targetJobset.iname)))
          {
            Json_Job jsonJob = collection.Find((Predicate<Json_Job>) (_job => _job.iname == targetJobset.job));
            if (jsonJob != null)
            {
              jsonJobList.Add(jsonJob);
              collection.Remove(jsonJob);
              break;
            }
          }
        }
        jsonJobList.AddRange((IEnumerable<Json_Job>) collection);
        json.jobs = jsonJobList.ToArray();
        this.mJobs = new JobData[json.jobs.Length];
        int newSize = 0;
        for (int index = 0; index < json.jobs.Length; ++index)
        {
          JobData jobData = new JobData();
          jobData.Deserialize(this, json.jobs[index]);
          this.mJobs[newSize] = jobData;
          ++newSize;
        }
        if (newSize != this.mJobs.Length)
          Array.Resize<JobData>(ref this.mJobs, newSize);
        for (int index = 0; index < this.mJobs.Length; ++index)
        {
          if (json.select != null && this.mJobs[index].UniqueID == json.select.job)
          {
            this.mJobIndex = (OInt) index;
            break;
          }
        }
        if (!string.IsNullOrEmpty(this.mJobs[(int) this.mJobIndex].Param.MapEffectAbility))
        {
          string mapEffectAbility = this.mJobs[(int) this.mJobIndex].Param.MapEffectAbility;
          this.mMapEffectAbility = new AbilityData();
          this.mMapEffectAbility.Setup(this, -1L, mapEffectAbility, 0);
          this.mMapEffectAbility.UpdateLearningsSkill(false);
          this.mMapEffectAbility.IsNoneCategory = true;
        }
      }
      else
      {
        this.mNormalAttackSkill = new SkillData();
        this.mNormalAttackSkill.Setup(this.UnitParam.no_job_status == null ? (string) null : this.UnitParam.no_job_status.default_skill, 1);
      }
      if (this.mConceptCards == null || this.mConceptCards.Length < 2)
      {
        this.mConceptCards = new ConceptCardData[2];
      }
      else
      {
        for (int index = 0; index < this.mConceptCards.Length; ++index)
          this.mConceptCards[index] = (ConceptCardData) null;
      }
      if (json.concept_cards != null && json.concept_cards.Length > 0)
      {
        for (int index = 0; index < json.concept_cards.Length; ++index)
        {
          if (json.concept_cards[index] != null && !json.concept_cards[index].IsEmptyDummyData)
          {
            ConceptCardData conceptCard = new ConceptCardData();
            conceptCard.Deserialize(json.concept_cards[index]);
            this.SetConceptCardByIndex(index, conceptCard);
          }
        }
      }
      this.mTobiraData.Clear();
      if (json.doors != null)
      {
        for (int index = 0; index < json.doors.Length; ++index)
        {
          Json_Tobira door = json.doors[index];
          SRPG.TobiraData tobiraData = new SRPG.TobiraData();
          tobiraData.Setup(json.iname, (TobiraParam.Category) door.category, door.lv);
          this.mTobiraData.Add(tobiraData);
        }
      }
      this.mMasterAbility = (AbilityData) null;
      if (json.abil != null && !string.IsNullOrEmpty(json.abil.iname))
      {
        this.mMasterAbility = new AbilityData();
        string iname = json.abil.iname;
        long iid = json.abil.iid;
        int exp = json.abil.exp;
        this.mMasterAbility.Setup(this, iid, iname, exp);
        this.mMasterAbility.IsNoneCategory = true;
      }
      this.mCollaboAbility = (AbilityData) null;
      if (json.c_abil != null && !string.IsNullOrEmpty(json.c_abil.iname))
      {
        this.mCollaboAbility = new AbilityData();
        this.mCollaboAbility.Setup(this, json.c_abil.iid, json.c_abil.iname, json.c_abil.exp);
        this.mCollaboAbility.IsNoneCategory = true;
        this.mCollaboAbility.UpdateLearningsSkillCollabo(json.c_abil.skills);
      }
      this.mTobiraMasterAbilitys.Clear();
      if (json.door_abils != null)
      {
        for (int index = 0; index < json.door_abils.Length; ++index)
        {
          Json_Ability doorAbil = json.door_abils[index];
          if (doorAbil != null && !string.IsNullOrEmpty(doorAbil.iname))
          {
            AbilityData abilityData = new AbilityData();
            abilityData.Setup(this, doorAbil.iid, doorAbil.iname, doorAbil.exp);
            abilityData.IsNoneCategory = true;
            this.mTobiraMasterAbilitys.Add(abilityData);
          }
        }
      }
      string overwriteLeaderSkill = TobiraUtility.GetOverwriteLeaderSkill(this.mTobiraData);
      string iname1 = this.mUnlockedLeaderSkill == null ? this.GetLeaderSkillIname((int) this.mRarity) : this.mUnlockedLeaderSkill.new_id;
      if (!string.IsNullOrEmpty(overwriteLeaderSkill))
        iname1 = overwriteLeaderSkill;
      this.mLeaderSkill = (SkillData) null;
      if (!string.IsNullOrEmpty(iname1))
      {
        this.mLeaderSkill = new SkillData();
        this.mLeaderSkill.Setup(iname1, 1);
      }
      for (int index = 0; index < this.mEquipRunes.Length; ++index)
        this.mEquipRunes[index] = (RuneData) null;
      if (json.runes != null)
      {
        for (int index = 0; index < json.runes.Length; ++index)
        {
          if (json.runes[index] != null)
          {
            RuneSlotIndex slotToIndex = RuneSlotIndex.CreateSlotToIndex(json.runes[index].slot);
            if ((int) (byte) slotToIndex < this.mEquipRunes.Length)
            {
              RuneData runeData = new RuneData();
              runeData.Deserialize(json.runes[index]);
              this.mEquipRunes[(int) (byte) slotToIndex] = runeData;
            }
          }
        }
      }
      this.mUnitJobOverwriteParams = instanceDirect.MasterParam.GetUnitJobOverwriteParamsForUnit(this.UnitID);
      this.mJobSkillAbilityDeriveData = instanceDirect.MasterParam.CreateSkillAbilityDeriveDataWithArtifacts(this.mJobs);
      this.UpdateAvailableJobs();
      this.UpdateUnitLearnAbilityAll();
      this.UpdateUnitBattleAbilityAll();
      this.CalcStatus();
      this.ResetCharacterQuestParams();
      this.FindCharacterQuestParams();
      this.mIsRental = (OBool) (json.rental == 1);
      this.mRentalFavoritePoint = (OInt) json.favpoint;
      this.mRentalIname = json.rental_iname;
    }

    public string Serialize()
    {
      string str1 = string.Empty + "{\"iid\":" + (object) this.UniqueID + ",\"iname\":\"" + this.UnitParam.iname + "\"" + ",\"rare\":" + (object) this.Rarity + ",\"plus\":" + (object) this.AwakeLv + ",\"lv\":" + (object) this.Lv + ",\"exp\":" + (object) this.Exp + ",\"fav\":" + (!this.IsFavorite ? "0" : "1") + ",\"rental\":" + (!this.IsRental ? "0" : "1") + ",\"favpoint\":" + (object) this.mRentalFavoritePoint + ",\"rental_iname\":\"" + this.mRentalIname + "\"";
      if (this.MasterAbility != null)
        str1 = str1 + ",\"abil\":{\"iid\":" + (object) this.MasterAbility.UniqueID + ",\"iname\":\"" + this.MasterAbility.Param.iname + "\",\"exp\":" + (object) this.MasterAbility.Exp + "}";
      if (this.CollaboAbility != null)
      {
        string str2 = str1 + ",\"c_abil\":{\"iid\":" + (object) this.CollaboAbility.UniqueID + ",\"iname\":\"" + this.CollaboAbility.Param.iname + "\",\"exp\":" + (object) this.CollaboAbility.Exp + ",\"skills\":[";
        for (int index = 0; index < this.CollaboAbility.Skills.Count; ++index)
        {
          SkillData skill = this.CollaboAbility.Skills[index];
          if (skill != null)
            str2 = str2 + (index == 0 ? string.Empty : ",") + "{\"iname\":\"" + skill.SkillParam.iname + "\"}";
        }
        str1 = str2 + "]}";
      }
      string str3 = string.Empty;
      if (this.mUnlockedAbilitys != null)
      {
        for (int index = 0; index < this.mUnlockedAbilitys.Count; ++index)
          str3 = str3 + (index <= 0 ? string.Empty : ",") + "\"" + this.mUnlockedAbilitys[index].iname + "\"";
      }
      if (this.mUnlockedSkills != null)
      {
        if (!string.IsNullOrEmpty(str3))
          str3 += ",";
        for (int index = 0; index < this.mUnlockedSkills.Count; ++index)
          str3 = str3 + (index <= 0 ? string.Empty : ",") + "\"" + this.mUnlockedSkills[index].iname + "\"";
      }
      if (this.mUnlockedLeaderSkill != null)
        str3 = str3 + (string.IsNullOrEmpty(str3) ? string.Empty : ",") + "\"" + this.mUnlockedLeaderSkill.iname + "\"";
      if (!string.IsNullOrEmpty(str3))
        str1 = str1 + ",\"quest_clear_unlocks\":[" + str3 + "]";
      if (this.Jobs != null && this.Jobs.Length > 0)
      {
        string str4 = str1 + ",\"jobs\":[";
        for (int index1 = 0; index1 < this.Jobs.Length; ++index1)
        {
          JobData job = this.Jobs[index1];
          if (index1 > 0)
            str4 += ",";
          string str5 = str4 + "{\"iid\":" + (object) job.UniqueID + ",\"iname\":\"" + job.JobID + "\",\"rank\":" + (object) job.Rank;
          if (job.Equips != null && job.Equips.Length > 0)
          {
            string str6 = str5 + ",\"equips\":[";
            for (int index2 = 0; index2 < job.Equips.Length; ++index2)
            {
              EquipData equip = job.Equips[index2];
              if (index2 > 0)
                str6 += ",";
              str6 = str6 + "{\"iid\":" + (object) equip.UniqueID + ",\"iname\":\"" + equip.ItemID + "\",\"exp\":" + (object) equip.Exp + "}";
            }
            str5 = str6 + "]";
          }
          if (job.LearnAbilitys != null && job.LearnAbilitys.Count > 0)
          {
            string str7 = str5 + ",\"abils\":[";
            for (int index3 = 0; index3 < job.LearnAbilitys.Count; ++index3)
            {
              AbilityData learnAbility = job.LearnAbilitys[index3];
              if (index3 > 0)
                str7 += ",";
              str7 = str7 + "{\"iid\":" + (object) learnAbility.UniqueID + ",\"iname\":\"" + learnAbility.Param.iname + "\",\"exp\":" + (object) learnAbility.Exp + "}";
            }
            str5 = str7 + "]";
          }
          if (job.AbilitySlots != null || job.Artifacts != null)
          {
            string str8 = str5 + ",\"select\":{";
            bool flag = false;
            if (job.AbilitySlots != null)
            {
              string str9 = str8 + "\"abils\":[";
              for (int index4 = 0; index4 < job.AbilitySlots.Length; ++index4)
              {
                if (index4 > 0)
                  str9 += ",";
                str9 += (string) (object) job.AbilitySlots[index4];
              }
              str8 = str9 + "]";
              flag = true;
            }
            if (job.Artifacts != null)
            {
              if (flag)
                str8 += ",";
              string str10 = str8 + "\"artifacts\":[";
              for (int index5 = 0; index5 < job.Artifacts.Length; ++index5)
              {
                if (index5 > 0)
                  str10 += ",";
                str10 += (string) (object) job.Artifacts[index5];
              }
              str8 = str10 + "]";
              flag = true;
            }
            if (job.ArtifactDatas != null)
            {
              if (flag)
                str8 += ",";
              string str11 = str8 + "\"inspiration_skills\":[";
              for (int index6 = 0; index6 < job.ArtifactDatas.Length; ++index6)
              {
                if (job.ArtifactDatas[index6] != null && job.ArtifactDatas[index6].InspirationSkillList != null)
                {
                  for (int index7 = 0; index7 < job.ArtifactDatas[index6].InspirationSkillList.Count; ++index7)
                  {
                    if (index7 > 0)
                      str11 += ",";
                    str11 = job.ArtifactDatas[index6].InspirationSkillList[index7] != null ? str11 + "{\"artifact_iid\":" + (object) job.ArtifactDatas[index6].UniqueID + ",\"iid\":" + (object) job.ArtifactDatas[index6].InspirationSkillList[index7].UniqueID + ",\"slot\":" + (object) job.ArtifactDatas[index6].InspirationSkillList[index7].Slot + ",\"iname\":\"" + job.ArtifactDatas[index6].InspirationSkillList[index7].InspSkillParam.Iname + "\"" + ",\"level\":" + (object) job.ArtifactDatas[index6].InspirationSkillList[index7].Lv + ",\"is_set\":" + (object) (!(bool) job.ArtifactDatas[index6].InspirationSkillList[index7].IsSet ? 0 : 1) + "}" : str11 + "null";
                  }
                }
              }
              str8 = str11 + "]";
            }
            str5 = str8 + "}";
          }
          if (job.ArtifactDatas != null)
          {
            string str12 = str5 + ",\"artis\":[";
            for (int index8 = 0; index8 < job.ArtifactDatas.Length; ++index8)
            {
              ArtifactData artifactData = job.ArtifactDatas[index8];
              if (index8 > 0)
                str12 += ",";
              if (artifactData == null)
              {
                str12 += "null";
              }
              else
              {
                string str13 = str12 + "{\"iid\":" + (object) artifactData.UniqueID + ",\"iname\":\"" + artifactData.ArtifactParam.iname + "\"" + ",\"exp\":" + (object) artifactData.Exp + ",\"rare\":" + (object) artifactData.Rarity + ",\"fav\":" + (object) (!artifactData.IsFavorite ? 0 : 1) + ",\"inspiration_skill_slots\":" + (object) artifactData.InspirationSkillSlotNum;
                if (artifactData.InspirationSkillList != null)
                {
                  string str14 = str13 + ",\"inspiration_skills\":[";
                  for (int index9 = 0; index9 < artifactData.InspirationSkillList.Count; ++index9)
                  {
                    InspirationSkillData inspirationSkill = artifactData.InspirationSkillList[index9];
                    if (index9 > 0)
                      str14 += ",";
                    str14 = inspirationSkill != null ? str14 + "{\"iid\":" + (object) inspirationSkill.UniqueID + ",\"slot\":" + (object) inspirationSkill.Slot + ",\"iname\":\"" + inspirationSkill.InspSkillParam.Iname + "\"" + ",\"level\":" + (object) inspirationSkill.Lv + ",\"is_set\":" + (object) (!(bool) inspirationSkill.IsSet ? 0 : 1) + "}" : str14 + "null";
                  }
                  str13 = str14 + "]";
                }
                str12 = str13 + "}";
              }
            }
            str5 = str12 + "]";
          }
          if (!string.IsNullOrEmpty(job.SelectedSkin))
            str5 = str5 + ",\"cur_skin\":\"" + job.SelectedSkin + "\"";
          str4 = str5 + "}";
        }
        str1 = str4 + "]";
      }
      long uniqueId = this.CurrentJob == null ? 0L : this.CurrentJob.UniqueID;
      if (uniqueId != 0L)
      {
        string str15 = str1 + ",\"select\":{";
        if (uniqueId != 0L)
        {
          str15 = str15 + "\"job\":" + (object) uniqueId;
          if (this.mPartyJobs != null && this.mPartyJobs.Length > 0)
          {
            string str16 = str15 + ",\"quests\":[";
            int num = 0;
            for (int type = 0; type < this.mPartyJobs.Length; ++type)
            {
              if (this.mPartyJobs[type] != 0L)
              {
                if (num > 0)
                  str16 += (string) (object) ',';
                str16 = str16 + "{\"qtype\":\"" + PartyData.GetStringFromPartyType((PlayerPartyTypes) type) + "\",\"jiid\":" + (object) this.mPartyJobs[type] + "}";
                ++num;
              }
            }
            str15 = str16 + "]";
          }
        }
        str1 = str15 + "}";
      }
      if (this.mTobiraData != null && this.mTobiraData.Count > 0)
      {
        str1 = str1 + "," + TobiraUtility.ToJsonString(this.mTobiraData);
        if (this.mTobiraMasterAbilitys.Count > 0)
        {
          string str17 = str1 + ",\"door_abils\":[";
          for (int index = 0; index < this.mTobiraMasterAbilitys.Count; ++index)
          {
            if (index > 0)
              str17 += ",";
            str17 = str17 + "{" + "\"iid\":" + (object) this.mTobiraMasterAbilitys[index].UniqueID + ",\"exp\":" + (object) this.mTobiraMasterAbilitys[index].Exp + ",\"iname\":\"" + this.mTobiraMasterAbilitys[index].Param.iname + "\"" + "}";
          }
          str1 = str17 + "]";
        }
      }
      if (this.mConceptCards == null || this.mConceptCards.Length < 2)
        this.mConceptCards = new ConceptCardData[2];
      string str18 = str1 + ",\"concept_cards\":[";
      for (int index = 0; index < this.mConceptCards.Length; ++index)
      {
        if (this.mConceptCards[index] == null)
        {
          if (index != 0)
            str18 += ",";
          str18 = str18 + "{" + "\"iid\":" + (object) 0 + "}";
        }
        else
        {
          if (index != 0)
            str18 += ",";
          str18 = str18 + "{" + "\"iid\":" + (object) this.mConceptCards[index].UniqueID + ",\"iname\":\"" + this.mConceptCards[index].Param.iname + "\"" + ",\"exp\":" + (object) this.mConceptCards[index].Exp + ",\"trust\":" + (object) this.mConceptCards[index].Trust + ",\"fav\":" + (object) (!this.mConceptCards[index].Favorite ? 0 : 1) + ",\"is_new\":0" + ",\"trust_bonus\":" + (object) this.mConceptCards[index].TrustBonus + ",\"plus\":" + (object) this.mConceptCards[index].AwakeCount + ",\"leaderskill\":" + (object) (!this.mLeaderSkillIsConceptCard ? 0 : 1) + "}";
        }
      }
      string str19 = str18 + "]";
      if (this.mEquipRunes != null)
      {
        List<Json_RuneData> jsonRuneDataList = new List<Json_RuneData>();
        for (int index = 0; index < this.mEquipRunes.Length; ++index)
        {
          if (this.mEquipRunes[index] != null)
          {
            Json_RuneData jsonRuneData = this.mEquipRunes[index].Serialize();
            if (jsonRuneData != null)
              jsonRuneDataList.Add(jsonRuneData);
          }
        }
        string[] strArray = new string[jsonRuneDataList.Count];
        for (int index = 0; index < strArray.Length; ++index)
          strArray[index] = JsonUtility.ToJson((object) jsonRuneDataList[index]);
        string str20 = ",\"runes\":[" + string.Join(",", strArray) + "]";
        str19 += str20;
      }
      return str19 + "}";
    }

    public string Serialize2()
    {
      string str1 = string.Empty + "{\"iid\":" + (object) this.UniqueID + ",\"iname\":\"" + this.UnitParam.iname + "\"" + ",\"rare\":" + (object) this.Rarity + ",\"plus\":" + (object) this.AwakeLv + ",\"lv\":" + (object) this.Lv + ",\"exp\":" + (object) this.Exp;
      if (this.MasterAbility != null)
        str1 = str1 + ",\"abil\":{\"iid\":" + (object) this.MasterAbility.UniqueID + ",\"iname\":\"" + this.MasterAbility.Param.iname + "\",\"exp\":" + (object) this.MasterAbility.Exp + "}";
      if (this.CollaboAbility != null)
      {
        string str2 = str1 + ",\"c_abil\":{\"iid\":" + (object) this.CollaboAbility.UniqueID + ",\"iname\":\"" + this.CollaboAbility.Param.iname + "\",\"exp\":" + (object) this.CollaboAbility.Exp + ",\"skills\":[";
        for (int index = 0; index < this.CollaboAbility.Skills.Count; ++index)
        {
          SkillData skill = this.CollaboAbility.Skills[index];
          if (skill != null)
            str2 = str2 + (index == 0 ? string.Empty : ",") + "{\"iname\":\"" + skill.SkillParam.iname + "\"}";
        }
        str1 = str2 + "]}";
      }
      string str3 = string.Empty;
      if (this.mUnlockedAbilitys != null)
      {
        for (int index = 0; index < this.mUnlockedAbilitys.Count; ++index)
          str3 = str3 + (index <= 0 ? string.Empty : ",") + "\"" + this.mUnlockedAbilitys[index].iname + "\"";
      }
      if (this.mUnlockedSkills != null)
      {
        if (!string.IsNullOrEmpty(str3))
          str3 += ",";
        for (int index = 0; index < this.mUnlockedSkills.Count; ++index)
          str3 = str3 + (index <= 0 ? string.Empty : ",") + "\"" + this.mUnlockedSkills[index].iname + "\"";
      }
      if (this.mUnlockedLeaderSkill != null)
        str3 = str3 + (string.IsNullOrEmpty(str3) ? string.Empty : ",") + "\"" + this.mUnlockedLeaderSkill.iname + "\"";
      if (!string.IsNullOrEmpty(str3))
        str1 = str1 + ",\"quest_clear_unlocks\":[" + str3 + "]";
      long uniqueId = this.CurrentJob == null ? 0L : this.CurrentJob.UniqueID;
      if (this.Jobs != null && this.Jobs.Length > 0)
      {
        string str4 = str1 + ",\"jobs\":[";
        for (int index1 = 0; index1 < this.Jobs.Length; ++index1)
        {
          JobData job = this.Jobs[index1];
          if (index1 > 0)
            str4 += ",";
          string str5 = str4 + "{\"iid\":" + (object) job.UniqueID + ",\"iname\":\"" + job.JobID + "\",\"rank\":" + (object) job.Rank;
          if (job.Equips != null && job.Equips.Length > 0)
          {
            string str6 = str5 + ",\"equips\":[";
            for (int index2 = 0; index2 < job.Equips.Length; ++index2)
            {
              EquipData equip = job.Equips[index2];
              if (index2 > 0)
                str6 += ",";
              str6 = str6 + "{\"iid\":" + (object) equip.UniqueID + ",\"iname\":\"" + equip.ItemID + "\",\"exp\":" + (object) equip.Exp + "}";
            }
            str5 = str6 + "]";
          }
          if (job.LearnAbilitys != null && job.LearnAbilitys.Count > 0)
          {
            string str7 = str5 + ",\"abils\":[";
            for (int index3 = 0; index3 < job.LearnAbilitys.Count; ++index3)
            {
              AbilityData learnAbility = job.LearnAbilitys[index3];
              if (index3 > 0)
                str7 += ",";
              str7 = str7 + "{\"iid\":" + (object) learnAbility.UniqueID + ",\"iname\":\"" + learnAbility.Param.iname + "\",\"exp\":" + (object) learnAbility.Exp + "}";
            }
            str5 = str7 + "]";
          }
          if ((job.AbilitySlots != null || job.Artifacts != null) && uniqueId == job.UniqueID)
          {
            string str8 = str5 + ",\"select\":{";
            bool flag = false;
            if (job.AbilitySlots != null)
            {
              string str9 = str8 + "\"abils\":[";
              for (int index4 = 0; index4 < job.AbilitySlots.Length; ++index4)
              {
                if (index4 > 0)
                  str9 += ",";
                str9 += (string) (object) job.AbilitySlots[index4];
              }
              str8 = str9 + "]";
              flag = true;
            }
            if (job.Artifacts != null)
            {
              if (flag)
                str8 += ",";
              string str10 = str8 + "\"artifacts\":[";
              for (int index5 = 0; index5 < job.Artifacts.Length; ++index5)
              {
                if (index5 > 0)
                  str10 += ",";
                str10 += (string) (object) job.Artifacts[index5];
              }
              str8 = str10 + "]";
              flag = true;
            }
            if (job.ArtifactDatas != null)
            {
              if (flag)
                str8 += ",";
              string str11 = str8 + "\"inspiration_skills\":[";
              for (int index6 = 0; index6 < job.ArtifactDatas.Length; ++index6)
              {
                if (job.ArtifactDatas[index6] != null && job.ArtifactDatas[index6].InspirationSkillList != null)
                {
                  for (int index7 = 0; index7 < job.ArtifactDatas[index6].InspirationSkillList.Count; ++index7)
                  {
                    if (index7 > 0)
                      str11 += ",";
                    str11 = job.ArtifactDatas[index6].InspirationSkillList[index7] != null ? str11 + "{\"artifact_iid\":" + (object) job.ArtifactDatas[index6].UniqueID + ",\"iid\":" + (object) job.ArtifactDatas[index6].InspirationSkillList[index7].UniqueID + ",\"slot\":" + (object) job.ArtifactDatas[index6].InspirationSkillList[index7].Slot + ",\"iname\":\"" + job.ArtifactDatas[index6].InspirationSkillList[index7].InspSkillParam.Iname + "\"" + ",\"level\":" + (object) job.ArtifactDatas[index6].InspirationSkillList[index7].Lv + ",\"is_set\":" + (object) (!(bool) job.ArtifactDatas[index6].InspirationSkillList[index7].IsSet ? 0 : 1) + "}" : str11 + "null";
                  }
                }
              }
              str8 = str11 + "]";
            }
            str5 = str8 + "}";
          }
          if (job.ArtifactDatas != null)
          {
            string str12 = str5 + ",\"artis\":[";
            if (job.UniqueID == uniqueId)
            {
              for (int index8 = 0; index8 < job.ArtifactDatas.Length; ++index8)
              {
                ArtifactData artifactData = job.ArtifactDatas[index8];
                if (index8 > 0)
                  str12 += ",";
                if (artifactData == null)
                {
                  str12 += "null";
                }
                else
                {
                  string str13 = str12 + "{\"iid\":" + (object) artifactData.UniqueID + ",\"iname\":\"" + artifactData.ArtifactParam.iname + "\"" + ",\"exp\":" + (object) artifactData.Exp + ",\"rare\":" + (object) artifactData.Rarity + ",\"inspiration_skill_slots\":" + (object) artifactData.InspirationSkillSlotNum;
                  if (artifactData.InspirationSkillList != null)
                  {
                    string str14 = str13 + ",\"inspiration_skills\":[";
                    for (int index9 = 0; index9 < artifactData.InspirationSkillList.Count; ++index9)
                    {
                      InspirationSkillData inspirationSkill = artifactData.InspirationSkillList[index9];
                      if (index9 > 0)
                        str14 += ",";
                      str14 = inspirationSkill != null ? str14 + "{\"iid\":" + (object) inspirationSkill.UniqueID + ",\"slot\":" + (object) inspirationSkill.Slot + ",\"iname\":\"" + inspirationSkill.InspSkillParam.Iname + "\"" + ",\"level\":" + (object) inspirationSkill.Lv + ",\"is_set\":" + (object) (!(bool) inspirationSkill.IsSet ? 0 : 1) + "}" : str14 + "null";
                    }
                    str13 = str14 + "]";
                  }
                  str12 = str13 + "}";
                }
              }
            }
            else
            {
              for (int index10 = 0; index10 < job.ArtifactDatas.Length; ++index10)
              {
                ArtifactData artifactData = job.ArtifactDatas[index10];
                if (index10 > 0)
                  str12 += ",";
                str12 = artifactData == null || artifactData != null && artifactData.ArtifactParam.type != ArtifactTypes.Arms ? str12 + "null" : str12 + "{\"iid\":" + (object) artifactData.UniqueID + ",\"iname\":\"" + artifactData.ArtifactParam.iname + "\"" + ",\"exp\":" + (object) artifactData.Exp + ",\"rare\":" + (object) artifactData.Rarity + ",\"fav\":" + (object) (!artifactData.IsFavorite ? 0 : 1) + "}";
              }
            }
            str5 = str12 + "]";
          }
          if (!string.IsNullOrEmpty(job.SelectedSkin) && uniqueId == job.UniqueID)
            str5 = str5 + ",\"cur_skin\":\"" + job.SelectedSkin + "\"";
          str4 = str5 + "}";
        }
        str1 = str4 + "]";
      }
      if (uniqueId != 0L)
      {
        string str15 = str1 + ",\"select\":{";
        if (uniqueId != 0L)
        {
          str15 = str15 + "\"job\":" + (object) uniqueId;
          if (this.mPartyJobs != null && this.mPartyJobs.Length > 0)
          {
            string str16 = str15 + ",\"quests\":[";
            int num = 0;
            for (int type = 0; type < this.mPartyJobs.Length; ++type)
            {
              if (this.mPartyJobs[type] != 0L)
              {
                if (num > 0)
                  str16 += (string) (object) ',';
                str16 = str16 + "{\"qtype\":\"" + PartyData.GetStringFromPartyType((PlayerPartyTypes) type) + "\",\"jiid\":" + (object) this.mPartyJobs[type] + "}";
                ++num;
              }
            }
            str15 = str16 + "]";
          }
        }
        str1 = str15 + "}";
      }
      if (this.mTobiraData != null && this.mTobiraData.Count > 0)
      {
        str1 = str1 + "," + TobiraUtility.ToJsonString(this.mTobiraData);
        if (this.mTobiraMasterAbilitys.Count > 0)
        {
          string str17 = str1 + ",\"door_abils\":[";
          for (int index = 0; index < this.mTobiraMasterAbilitys.Count; ++index)
          {
            if (index > 0)
              str17 += ",";
            str17 = str17 + "{" + "\"iid\":" + (object) this.mTobiraMasterAbilitys[index].UniqueID + ",\"exp\":" + (object) this.mTobiraMasterAbilitys[index].Exp + ",\"iname\":\"" + this.mTobiraMasterAbilitys[index].Param.iname + "\"" + "}";
          }
          str1 = str17 + "]";
        }
      }
      if (this.mConceptCards == null || this.mConceptCards.Length < 2)
        this.mConceptCards = new ConceptCardData[2];
      string str18 = str1 + ",\"concept_cards\":[";
      for (int index = 0; index < this.mConceptCards.Length; ++index)
      {
        if (this.mConceptCards[index] == null)
        {
          if (index != 0)
            str18 += ",";
          str18 = str18 + "{" + "\"iid\":" + (object) 0 + "}";
        }
        else
        {
          if (index != 0)
            str18 += ",";
          str18 = str18 + "{" + "\"iid\":" + (object) this.mConceptCards[index].UniqueID + ",\"iname\":\"" + this.mConceptCards[index].Param.iname + "\"" + ",\"exp\":" + (object) this.mConceptCards[index].Exp + ",\"trust\":" + (object) this.mConceptCards[index].Trust + ",\"fav\":" + (object) (!this.mConceptCards[index].Favorite ? 0 : 1) + ",\"is_new\":0" + ",\"trust_bonus\":" + (object) this.mConceptCards[index].TrustBonus + ",\"plus\":" + (object) this.mConceptCards[index].AwakeCount + ",\"leaderskill\":" + (object) (!this.mLeaderSkillIsConceptCard ? 0 : 1) + "}";
        }
      }
      string str19 = str18 + "]";
      if (this.mEquipRunes != null)
      {
        List<Json_RuneData> jsonRuneDataList = new List<Json_RuneData>();
        for (int index = 0; index < this.mEquipRunes.Length; ++index)
        {
          if (this.mEquipRunes[index] != null)
          {
            Json_RuneData jsonRuneData = this.mEquipRunes[index].Serialize();
            if (jsonRuneData != null)
              jsonRuneDataList.Add(jsonRuneData);
          }
        }
        string[] strArray = new string[jsonRuneDataList.Count];
        for (int index = 0; index < strArray.Length; ++index)
          strArray[index] = JsonUtility.ToJson((object) jsonRuneDataList[index]);
        string str20 = ",\"runes\":[" + string.Join(",", strArray) + "]";
        str19 += str20;
      }
      return str19 + "}";
    }

    public bool Setup(
      string unit_iname,
      int exp,
      int rare,
      int awakeLv,
      string job_iname = null,
      int jobrank = 1,
      EElement elem = EElement.None,
      int unlockTobiraNum = 0)
    {
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      this.mUnitParam = instanceDirect.GetUnitParam(unit_iname);
      DebugUtility.Assert(this.mUnitParam != null, "Failed UnitParam iname \"" + unit_iname + "\" not found.");
      this.mRarity = (OInt) Math.Min(Math.Max(rare, (int) this.mUnitParam.rare), (int) this.mUnitParam.raremax);
      this.mAwakeLv = (OInt) Math.Min(awakeLv, this.GetAwakeLevelCap());
      this.mUnlockTobiraNum = (OInt) unlockTobiraNum;
      this.mElement = (OInt) (int) elem;
      this.mExp = (OInt) exp;
      this.mLv = (OInt) this.CalcLevel();
      this.mJobs = (JobData[]) null;
      this.mJobIndex = (OInt) 0;
      if (this.mUnitParam.jobsets != null && this.mUnitParam.jobsets.Length > 0 && !string.IsNullOrEmpty(this.mUnitParam.jobsets[0]))
      {
        this.mJobs = new JobData[this.mUnitParam.jobsets.Length];
        int index = 0;
        int num = 0;
        for (; index < this.mUnitParam.jobsets.Length; ++index)
        {
          JobSetParam jobSetParam = instanceDirect.GetJobSetParam(this.mUnitParam.jobsets[index]);
          if (jobSetParam != null && !string.IsNullOrEmpty(jobSetParam.job))
          {
            JobData jobData = new JobData();
            Json_Job json = new Json_Job();
            json.iname = jobSetParam.job;
            json.rank = 1;
            if (json.iname == job_iname)
            {
              json.rank = jobrank;
              this.mJobIndex = (OInt) num;
            }
            try
            {
              jobData.Deserialize(this, json);
              this.mJobs[num++] = jobData;
            }
            catch (Exception ex)
            {
              DebugUtility.LogException(ex);
            }
          }
        }
      }
      else
      {
        this.mNormalAttackSkill = new SkillData();
        this.mNormalAttackSkill.Setup(this.UnitParam.no_job_status == null ? (string) null : this.UnitParam.no_job_status.default_skill, 1);
      }
      this.UpdateAvailableJobs();
      if (this.mJobs != null)
      {
        for (int index = 0; index < this.mJobs.Length; ++index)
          this.mJobs[index].UnlockSkillAll();
      }
      this.UpdateUnitLearnAbilityAll();
      this.UpdateUnitBattleAbilityAll();
      for (int index1 = 0; index1 < this.BattleAbilitys.Count; ++index1)
      {
        this.BattleAbilitys[index1].Setup(this, (long) (index1 + 1), this.BattleAbilitys[index1].AbilityID, this.BattleAbilitys[index1].Exp);
        for (int index2 = 0; index2 < this.BattleAbilitys[index1].Skills.Count; ++index2)
        {
          SkillData skill = this.BattleAbilitys[index1].Skills[index2];
          if (skill != null && !this.mBattleSkills.Contains(skill))
            this.mBattleSkills.Add(skill);
        }
      }
      this.mLeaderSkill = (SkillData) null;
      string leaderSkillIname = this.GetLeaderSkillIname((int) this.mRarity);
      if (!string.IsNullOrEmpty(leaderSkillIname))
      {
        this.mLeaderSkill = new SkillData();
        this.mLeaderSkill.Setup(leaderSkillIname, 1);
      }
      this.CalcStatus();
      return true;
    }

    public void Setup(UnitData src)
    {
      this.Deserialize(JSONParser.parseJSONObject<Json_Unit>(src.Serialize()));
    }

    public void Release()
    {
      this.mJobs = (JobData[]) null;
      this.mStatus = (BaseStatus) null;
    }

    public void ShowTooltip(
      GameObject targetGO,
      bool allowJobChange = false,
      UnitJobDropdown.ParentObjectEvent updateValue = null)
    {
      PlayerPartyTypes dataOfClass1 = DataSource.FindDataOfClass<PlayerPartyTypes>(targetGO, PlayerPartyTypes.Max);
      GameObject root = UnityEngine.Object.Instantiate<GameObject>(AssetManager.Load<GameObject>("UI/UnitTooltip_1"));
      UnitData data = new UnitData();
      data.Setup(this);
      data.TempFlags = this.TempFlags;
      DataSource.Bind<UnitData>(root, data);
      DataSource.Bind<bool>(root, allowJobChange);
      DataSource.Bind<PlayerPartyTypes>(root, dataOfClass1);
      GlobalVars.SelectedLSChangeUnitUniqueID.Set(data.UniqueID);
      if (dataOfClass1 == PlayerPartyTypes.Support)
      {
        eOverWritePartyType dataOfClass2 = DataSource.FindDataOfClass<eOverWritePartyType>(targetGO, eOverWritePartyType.None);
        GlobalVars.OverWritePartyType.Set(dataOfClass2);
      }
      UnitJobDropdown componentInChildren1 = root.GetComponentInChildren<UnitJobDropdown>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren1, (UnityEngine.Object) null))
      {
        bool flag = (data.TempFlags & UnitData.TemporaryFlags.AllowJobChange) != (UnitData.TemporaryFlags) 0 && allowJobChange && dataOfClass1 != PlayerPartyTypes.Max;
        ((Component) componentInChildren1).gameObject.SetActive(true);
        componentInChildren1.UpdateValue = updateValue;
        Selectable component1 = ((Component) componentInChildren1).gameObject.GetComponent<Selectable>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component1, (UnityEngine.Object) null))
          component1.interactable = flag;
        Image component2 = ((Component) componentInChildren1).gameObject.GetComponent<Image>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component2, (UnityEngine.Object) null))
          ((Graphic) component2).color = !flag ? new Color(0.5f, 0.5f, 0.5f) : Color.white;
      }
      ArtifactSlots componentInChildren2 = root.GetComponentInChildren<ArtifactSlots>();
      AbilitySlots componentInChildren3 = root.GetComponentInChildren<AbilitySlots>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren2, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren3, (UnityEngine.Object) null))
      {
        bool enable = (data.TempFlags & UnitData.TemporaryFlags.AllowJobChange) != (UnitData.TemporaryFlags) 0 && allowJobChange && dataOfClass1 != PlayerPartyTypes.Max;
        if (!GlobalVars.IsTutorialEnd)
          enable = false;
        componentInChildren2.Refresh(enable);
        componentInChildren3.Refresh(enable);
      }
      ConceptCardSlots componentInChildren4 = root.GetComponentInChildren<ConceptCardSlots>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren4, (UnityEngine.Object) null))
      {
        bool editMode = (data.TempFlags & UnitData.TemporaryFlags.AllowJobChange) != (UnitData.TemporaryFlags) 0 && allowJobChange && dataOfClass1 != PlayerPartyTypes.Max;
        if (!GlobalVars.IsTutorialEnd)
          editMode = false;
        componentInChildren4.Refresh(editMode);
      }
      GameParameter.UpdateAll(root);
    }

    public int GetRarityCap() => (int) this.UnitParam.raremax;

    public int GetRarityLevelCap(int rarity)
    {
      MasterParam masterParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam;
      GrowParam growParam = masterParam.GetGrowParam((string) this.UnitParam.grow);
      int val1 = growParam == null ? 0 : growParam.GetLevelCap();
      RarityParam rarityParam = masterParam.GetRarityParam(rarity);
      if (rarityParam != null)
        val1 = Math.Min(val1, (int) rarityParam.UnitLvCap);
      return val1;
    }

    public int GetLevelCap(bool bPlayerLvCap = false)
    {
      int val1 = this.GetRarityLevelCap(this.Rarity) + this.AwakeLv + TobiraUtility.GetAdditionalUnitLevelCapWithUnlockNum(this.UnlockTobriaNum);
      if (bPlayerLvCap)
        val1 = Math.Min(val1, MonoSingleton<GameManager>.Instance.Player.Lv);
      return val1;
    }

    public int CalcLevel()
    {
      return MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.CalcUnitLevel((int) this.mExp, this.GetLevelCap());
    }

    public int GetExp()
    {
      return (int) this.mExp - MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetUnitLevelExp((int) this.mLv);
    }

    public int GetNextLevel() => Math.Min(this.Lv + 1, this.GetLevelCap());

    public int GetNextExp()
    {
      MasterParam masterParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam;
      int levelCap = this.GetLevelCap();
      int num = 0;
      for (int index = 0; index < levelCap; ++index)
      {
        num += masterParam.GetUnitNextExp(index + 1);
        if (num > (int) this.mExp)
          return num - (int) this.mExp;
      }
      return 0;
    }

    public bool CheckGainExp() => this.Exp < this.GetGainExpCap();

    public int GetGainExpCap(int playerLv)
    {
      int levelCap = this.GetLevelCap();
      int gainExpCap;
      if (levelCap > playerLv)
      {
        int lv = playerLv + 1;
        gainExpCap = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetUnitLevelExp(lv) - 1;
      }
      else
        gainExpCap = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetUnitLevelExp(levelCap);
      return gainExpCap;
    }

    public int GetGainExpCap() => this.GetGainExpCap(MonoSingleton<GameManager>.Instance.Player.Lv);

    public void GainExp(int exp, int playerLv)
    {
      int gainExpCap = this.GetGainExpCap(playerLv);
      int mLv = (int) this.mLv;
      this.mExp = (OInt) Math.Min((int) this.mExp + exp, gainExpCap);
      this.mLv = (OInt) this.CalcLevel();
      if (mLv == (int) this.mLv)
        return;
      this.CalcStatus();
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      instanceDirect.Player.OnUnitLevelChange(this.UnitID, (int) this.mLv - mLv, (int) this.mLv);
      instanceDirect.Player.OnUnitLevelAndJobLevelChange(this.UnitID, (int) this.mLv, this.mJobs);
    }

    public void CalcStatus()
    {
      this.mLv = (OInt) this.CalcLevel();
      this.CalcStatus((int) this.mLv, (int) this.mJobIndex, ref this.mStatus);
    }

    public void CalcStatus(int lv, int jobNo, ref BaseStatus status, int disableJobMasterJobNo = -1)
    {
      BaseStatus src1 = (BaseStatus) null;
      BaseStatus src2 = (BaseStatus) null;
      BaseStatus comp1 = (BaseStatus) null;
      BaseStatus comp2 = (BaseStatus) null;
      UnitData.UnitScaleStatus.Clear();
      UnitData.WorkScaleStatus.Clear();
      UnitParam.CalcUnitLevelStatus(this.UnitParam, lv, ref status);
      JobData jobData = this.GetJobData(jobNo);
      if (jobData != null)
      {
        StatusParam statusParam1 = status.param;
        statusParam1.mov = (OShort) ((int) statusParam1.mov + (int) jobData.Param.mov);
        StatusParam statusParam2 = status.param;
        statusParam2.jmp = (OShort) ((int) statusParam2.jmp + (int) jobData.Param.jmp);
        UnitJobOverwriteParam jobOverwriteParam = this.GetUnitJobOverwriteParam(jobData.JobID);
        if (jobOverwriteParam != null)
          status.AddRate(jobOverwriteParam.mStatus);
        else
          status.AddRate(jobData.GetJobRankStatus());
        status.Add(jobData.GetJobTransfarStatus(this.Element));
        foreach (EquipData equip in jobData.Equips)
        {
          if (equip != null && equip.IsValid() && equip.IsEquiped())
          {
            UnitData.WorkScaleStatus.Clear();
            SkillData.GetHomePassiveBuffStatus(equip.Skill, this.Element, ref status, ref UnitData.WorkScaleStatus);
            UnitData.UnitScaleStatus.Add(UnitData.WorkScaleStatus);
          }
        }
        if (jobData.Artifacts != null && jobData.Artifacts.Length != 0)
        {
          src1 = src1 ?? new BaseStatus();
          src2 = src2 ?? new BaseStatus();
          comp1 = comp1 ?? new BaseStatus();
          comp2 = comp2 ?? new BaseStatus();
          src1.Clear();
          src2.Clear();
          comp1.Clear();
          comp2.Clear();
          for (int index = 0; index < jobData.Artifacts.Length; ++index)
          {
            if (jobData.Artifacts[index] != 0L)
            {
              ArtifactData artifactData = jobData.ArtifactDatas[index];
              if (artifactData != null && artifactData.EquipSkill != null)
              {
                comp1.Clear();
                comp2.Clear();
                UnitData.WorkScaleStatus.Clear();
                SkillData.GetHomePassiveBuffStatus(artifactData.EquipSkill, this.Element, ref comp1, ref comp2, ref comp2, ref comp1, ref UnitData.WorkScaleStatus);
                src1.ReplaceHighest(comp1);
                src2.ReplaceLowest(comp2);
                UnitData.UnitScaleStatus.Add(UnitData.WorkScaleStatus);
              }
            }
          }
          status.Add(src1);
          status.Add(src2);
        }
        if (!string.IsNullOrEmpty(jobData.SelectedSkin))
        {
          ArtifactData selectedSkinData = jobData.GetSelectedSkinData();
          if (selectedSkinData != null && selectedSkinData.EquipSkill != null)
          {
            UnitData.WorkScaleStatus.Clear();
            SkillData.GetHomePassiveBuffStatus(selectedSkinData.EquipSkill, this.Element, ref status, ref UnitData.WorkScaleStatus);
            UnitData.UnitScaleStatus.Add(UnitData.WorkScaleStatus);
          }
        }
        if (this.mConceptCards != null)
        {
          BaseStatus src3 = src1 ?? new BaseStatus();
          BaseStatus src4 = src2 ?? new BaseStatus();
          BaseStatus comp3 = comp1 ?? new BaseStatus();
          comp2 = comp2 ?? new BaseStatus();
          src3.Clear();
          src4.Clear();
          comp3.Clear();
          comp2.Clear();
          for (int index1 = 0; index1 < this.mConceptCards.Length; ++index1)
          {
            if (this.mConceptCards[index1] != null)
            {
              List<ConceptCardEquipEffect> enableEquipEffects = this.mConceptCards[index1].GetEnableEquipEffects(this, jobData);
              for (int index2 = 0; index2 < enableEquipEffects.Count; ++index2)
              {
                comp3.Clear();
                comp2.Clear();
                UnitData.WorkScaleStatus.Clear();
                SkillData.GetHomePassiveBuffStatus(enableEquipEffects[index2].EquipSkill, this.Element, ref comp3, ref comp2, ref comp2, ref comp3, ref UnitData.WorkScaleStatus, true);
                src3.ReplaceHighest(comp3);
                src4.ReplaceLowest(comp2);
                UnitData.UnitScaleStatus.Add(UnitData.WorkScaleStatus);
              }
            }
          }
          status.Add(src3);
          status.Add(src4);
        }
        UnitData.WorkScaleStatus.Clear();
        jobData.AddJobBaseStatus(this, ref status, ref UnitData.WorkScaleStatus);
        UnitData.UnitScaleStatus.Add(UnitData.WorkScaleStatus);
      }
      else
      {
        StatusParam statusParam3 = status.param;
        statusParam3.mov = (OShort) ((int) statusParam3.mov + (this.mUnitParam.no_job_status == null ? 0 : (int) this.mUnitParam.no_job_status.mov));
        StatusParam statusParam4 = status.param;
        statusParam4.jmp = (OShort) ((int) statusParam4.jmp + (this.mUnitParam.no_job_status == null ? 0 : (int) this.mUnitParam.no_job_status.jmp));
      }
      if (this.mEquipRunes != null)
      {
        for (int index = 0; index < this.mEquipRunes.Length; ++index)
        {
          if (this.mEquipRunes[index] != null && (long) this.mEquipRunes[index].UniqueID != 0L)
          {
            UnitData.WorkScaleStatus.Clear();
            this.mEquipRunes[index].AddRuneBaseStatus(this.Element, ref status, ref UnitData.WorkScaleStatus);
            UnitData.UnitScaleStatus.Add(UnitData.WorkScaleStatus);
          }
        }
        List<RuneSetEff> enableRuneSetEffects = RuneUtility.GetEnableRuneSetEffects(this.mEquipRunes);
        if (enableRuneSetEffects != null)
        {
          for (int index = 0; index < enableRuneSetEffects.Count; ++index)
          {
            UnitData.WorkScaleStatus.Clear();
            enableRuneSetEffects[index].AddRuneSetEffectBaseStatus(this.Element, ref status, ref UnitData.WorkScaleStatus, false);
            UnitData.UnitScaleStatus.Add(UnitData.WorkScaleStatus);
          }
        }
      }
      if (this.mTobiraData != null)
      {
        List<SkillData> parameterBuffSkills = TobiraUtility.GetParameterBuffSkills(this.mTobiraData);
        for (int index = 0; index < parameterBuffSkills.Count; ++index)
        {
          UnitData.WorkScaleStatus.Clear();
          SkillData.GetHomePassiveBuffStatus(parameterBuffSkills[index], this.Element, ref status, ref UnitData.WorkScaleStatus);
          UnitData.UnitScaleStatus.Add(UnitData.WorkScaleStatus);
        }
      }
      if (this.Jobs != null)
      {
        for (int index = 0; index < this.Jobs.Length; ++index)
        {
          SkillData jobMaster = this.Jobs[index].JobMaster;
          if (jobMaster != null && disableJobMasterJobNo != index)
          {
            UnitData.WorkScaleStatus.Clear();
            SkillData.GetHomePassiveBuffStatus(jobMaster, this.Element, ref status, ref UnitData.WorkScaleStatus);
            UnitData.UnitScaleStatus.Add(UnitData.WorkScaleStatus);
          }
        }
      }
      UnitParam.CalcUnitElementStatus(this.Element, ref status);
      for (int index = 0; index < this.BattleSkills.Count; ++index)
      {
        UnitData.WorkScaleStatus.Clear();
        SkillData.GetHomePassiveBuffStatus(this.BattleSkills[index], this.Element, ref status, ref UnitData.WorkScaleStatus);
        UnitData.UnitScaleStatus.Add(UnitData.WorkScaleStatus);
      }
      status.AddRate(UnitData.UnitScaleStatus);
      status.param.ApplyMinVal();
    }

    public int GetCombination() => 7 + this.AwakeLv;

    public int GetCombinationRange()
    {
      if (this.AwakeLv > 20)
        return 3;
      return this.AwakeLv > 10 ? 2 : 1;
    }

    public string GetLeaderSkillIname(int rarity)
    {
      return rarity < 0 || this.mUnitParam.leader_skills.Length <= rarity ? (string) null : this.mUnitParam.leader_skills[rarity];
    }

    public SkillData GetAttackSkill() => this.GetAttackSkill((int) this.mJobIndex);

    public SkillData GetAttackSkill(int jobNo)
    {
      JobData jobData = this.GetJobData(jobNo);
      return jobData != null ? jobData.GetAttackSkill() : this.mNormalAttackSkill;
    }

    public int GetAttackRangeMin(SkillData skill)
    {
      return this.GetAttackRangeMin((int) this.mJobIndex, skill);
    }

    public int GetAttackRangeMin(int jobNo, SkillData skill)
    {
      SkillData skillData = skill;
      if (skillData == null || skillData.IsReactionSkill() && skillData.IsBattleSkill())
      {
        skillData = this.GetAttackSkill(jobNo);
        if (skillData == null)
          return 0;
      }
      return skillData.RangeMin;
    }

    public int GetAttackRangeMax(SkillData skill)
    {
      return this.GetAttackRangeMax((int) this.mJobIndex, skill);
    }

    public int GetAttackRangeMax(int jobNo, SkillData skill)
    {
      SkillData skillData = skill;
      if (skillData == null || skillData.IsReactionSkill() && skillData.IsBattleSkill())
      {
        skillData = this.GetAttackSkill(jobNo);
        if (skillData == null)
          return 0;
      }
      return skillData.RangeMax;
    }

    public int GetAttackScope(SkillData skill) => this.GetAttackScope((int) this.mJobIndex, skill);

    public int GetAttackScope(int jobNo, SkillData skill)
    {
      SkillData skillData = skill;
      if (skillData == null || skillData.IsReactionSkill() && skillData.IsBattleSkill())
      {
        skillData = this.GetAttackSkill(jobNo);
        if (skillData == null)
          return 0;
      }
      return skillData.Scope;
    }

    public int GetAttackHeight(SkillData skill = null, bool is_range = false)
    {
      return this.GetAttackHeight((int) this.mJobIndex, skill, is_range);
    }

    public int GetAttackHeight(int jobNo, SkillData skill, bool is_range)
    {
      SkillData skillData = skill;
      if (skillData == null || skillData.IsReactionSkill() && skillData.IsBattleSkill())
      {
        skillData = this.GetAttackSkill(jobNo);
        if (skillData == null)
          return 0;
      }
      int attackHeight = skillData.EnableAttackGridHeight;
      if (is_range && skill.TeleportType != eTeleportType.None)
        attackHeight = skillData.TeleportHeight;
      return attackHeight;
    }

    public int GetMoveCount() => (int) this.mStatus.param.mov;

    public int GetMoveHeight() => (int) this.mStatus.param.jmp;

    public RecipeParam GetRarityUpRecipe() => this.GetRarityUpRecipe(this.Rarity);

    public RecipeParam GetRarityUpRecipe(int rarity)
    {
      return this.UnitParam != null ? this.UnitParam.GetRarityUpRecipe(rarity) : (RecipeParam) null;
    }

    public bool UnitRarityUp()
    {
      this.mRarity = (OInt) Math.Min((int) ++this.mRarity, this.GetRarityCap());
      string leaderSkillIname = this.GetLeaderSkillIname((int) this.mRarity);
      if (!string.IsNullOrEmpty(leaderSkillIname))
      {
        if (this.mLeaderSkill == null)
          this.mLeaderSkill = new SkillData();
        this.mLeaderSkill.Setup(leaderSkillIname, 1);
      }
      this.CalcStatus();
      return true;
    }

    public bool CheckUnitRarityUp()
    {
      if (this.GetRarityLevelCap(this.Rarity) > this.Lv || this.GetRarityCap() <= this.Rarity)
        return false;
      RecipeParam rarityUpRecipe = this.GetRarityUpRecipe();
      if (rarityUpRecipe == null)
        return false;
      for (int index = 0; index < rarityUpRecipe.items.Length; ++index)
      {
        RecipeItem recipeItem = rarityUpRecipe.items[index];
        if (recipeItem != null && !string.IsNullOrEmpty(recipeItem.iname))
        {
          ItemData itemDataByItemId = MonoSingleton<GameManager>.GetInstanceDirect().Player.FindItemDataByItemID(recipeItem.iname);
          if (itemDataByItemId == null || itemDataByItemId.Num < recipeItem.num)
            return false;
        }
      }
      return true;
    }

    public int GetRarityUpCost()
    {
      RarityParam rarityParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetRarityParam(this.Rarity);
      return rarityParam != null ? (int) rarityParam.UnitRarityUpCost : 0;
    }

    public bool CanUnlockTobira()
    {
      return MonoSingleton<GameManager>.Instance.MasterParam.CanUnlockTobira(this.UnitID) && !this.IsRental;
    }

    public bool MeetsTobiraConditions(TobiraParam.Category category)
    {
      return this.MeetsTobiraConditions(category, out List<UnitData.TobiraConditioError> _);
    }

    public bool MeetsTobiraConditions(
      TobiraParam.Category category,
      out List<UnitData.TobiraConditioError> errors)
    {
      errors = new List<UnitData.TobiraConditioError>();
      TobiraConditionParam[] conditionsForUnit = MonoSingleton<GameManager>.Instance.MasterParam.GetTobiraConditionsForUnit(this.UnitID, category);
      if (conditionsForUnit == null)
        return false;
      foreach (TobiraConditionParam tobiraConditionParam in conditionsForUnit)
      {
        TobiraConditionParam cond = tobiraConditionParam;
        switch (cond.CondType)
        {
          case TobiraConditionParam.ConditionType.Unit:
            UnitData unit = this;
            if (!string.IsNullOrEmpty(cond.CondUnit.UnitIname))
            {
              unit = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUnitID(cond.CondUnit.UnitIname);
              if (unit == null)
              {
                errors.Add(new UnitData.TobiraConditioError(UnitData.TobiraConditioError.ErrorType.UnitNone));
                break;
              }
            }
            if (cond.CondUnit.Level > unit.Lv)
              errors.Add(new UnitData.TobiraConditioError(UnitData.TobiraConditioError.ErrorType.UnitLevel));
            if (cond.CondUnit.AwakeLevel > unit.AwakeLv)
              errors.Add(new UnitData.TobiraConditioError(UnitData.TobiraConditioError.ErrorType.UnitAwakeLevel));
            if (category != TobiraParam.Category.START)
            {
              if (cond.CondUnit.Jobs != null && cond.CondUnit.Jobs.Count > 0 && !cond.CondUnit.IsJobLvClear(unit))
                errors.Add(new UnitData.TobiraConditioError(UnitData.TobiraConditioError.ErrorType.UnitJobLevel));
              SRPG.TobiraData tobiraData = unit.TobiraData.Find((Predicate<SRPG.TobiraData>) (tobira => tobira.Param.TobiraCategory == cond.CondUnit.TobiraCategory));
              if (tobiraData == null || cond.CondUnit.TobiraLv > tobiraData.Lv)
              {
                errors.Add(new UnitData.TobiraConditioError(UnitData.TobiraConditioError.ErrorType.UnitTobiraLevel));
                break;
              }
              break;
            }
            break;
          case TobiraConditionParam.ConditionType.Quest:
            if (category != TobiraParam.Category.START && !MonoSingleton<GameManager>.Instance.Player.IsQuestCleared(cond.CondIname))
            {
              errors.Add(new UnitData.TobiraConditioError(UnitData.TobiraConditioError.ErrorType.Quest));
              break;
            }
            break;
        }
      }
      return errors.Count == 0;
    }

    public bool UnitAwaking()
    {
      this.mAwakeLv = (OInt) Math.Min((int) ++this.mAwakeLv, this.GetAwakeLevelCap());
      return true;
    }

    public bool CheckUnitAwaking()
    {
      if (this.GetAwakeLevelCap() <= (int) this.mAwakeLv)
        return false;
      int num = this.GetPieces() + this.GetElementPieces() + this.GetCommonPieces();
      return this.GetAwakeNeedPieces() <= num;
    }

    public bool CheckUnitAwaking(int awakelv)
    {
      int awakeLevelCap = this.GetAwakeLevelCap();
      if (awakeLevelCap < (int) this.mAwakeLv || awakeLevelCap < awakelv)
        return false;
      int num = this.GetPieces() + this.GetElementPieces() + this.GetCommonPieces();
      for (int mAwakeLv = (int) this.mAwakeLv; mAwakeLv < awakelv; ++mAwakeLv)
      {
        int awakeNeedPieces = MonoSingleton<GameManager>.Instance.MasterParam.GetAwakeNeedPieces(mAwakeLv);
        if (awakeNeedPieces > num)
          return false;
        num -= awakeNeedPieces;
      }
      return true;
    }

    public int GetPieces()
    {
      UnitParam unitParam = this.UnitParam;
      if (string.IsNullOrEmpty(unitParam.piece))
        return 0;
      ItemData itemDataByItemId = MonoSingleton<GameManager>.GetInstanceDirect().Player.FindItemDataByItemID(unitParam.piece);
      return itemDataByItemId == null ? 0 : itemDataByItemId.Num;
    }

    public ItemParam GetElementPieceParam()
    {
      FixParam fixParam = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
      string key = string.Empty;
      switch (this.Element)
      {
        case EElement.Fire:
          key = (string) fixParam.CommonPieceFire;
          break;
        case EElement.Water:
          key = (string) fixParam.CommonPieceWater;
          break;
        case EElement.Wind:
          key = (string) fixParam.CommonPieceWind;
          break;
        case EElement.Thunder:
          key = (string) fixParam.CommonPieceThunder;
          break;
        case EElement.Shine:
          key = (string) fixParam.CommonPieceShine;
          break;
        case EElement.Dark:
          key = (string) fixParam.CommonPieceDark;
          break;
      }
      return string.IsNullOrEmpty(key) ? (ItemParam) null : MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(key);
    }

    public ItemData GetElementPieceData()
    {
      ItemParam elementPieceParam = this.GetElementPieceParam();
      return elementPieceParam == null ? (ItemData) null : MonoSingleton<GameManager>.GetInstanceDirect().Player.FindItemDataByItemParam(elementPieceParam);
    }

    public int GetElementPieces()
    {
      if (this.IsRental)
        return 0;
      ItemData elementPieceData = this.GetElementPieceData();
      return elementPieceData == null ? 0 : elementPieceData.Num;
    }

    public ItemParam GetCommonPieceParam()
    {
      FixParam fixParam = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
      return string.IsNullOrEmpty((string) fixParam.CommonPieceAll) ? (ItemParam) null : MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam((string) fixParam.CommonPieceAll);
    }

    public ItemData GetCommonPieceData()
    {
      ItemParam commonPieceParam = this.GetCommonPieceParam();
      return commonPieceParam == null ? (ItemData) null : MonoSingleton<GameManager>.GetInstanceDirect().Player.FindItemDataByItemParam(commonPieceParam);
    }

    public int GetCommonPieces()
    {
      if (this.IsRental)
        return 0;
      ItemData commonPieceData = this.GetCommonPieceData();
      return commonPieceData == null ? 0 : commonPieceData.Num;
    }

    public string GetUnlockTobiraElementID()
    {
      FixParam fixParam = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
      switch (this.Element)
      {
        case EElement.Fire:
          return (string) fixParam.TobiraUnlockElem[0];
        case EElement.Water:
          return (string) fixParam.TobiraUnlockElem[1];
        case EElement.Wind:
          return (string) fixParam.TobiraUnlockElem[2];
        case EElement.Thunder:
          return (string) fixParam.TobiraUnlockElem[3];
        case EElement.Shine:
          return (string) fixParam.TobiraUnlockElem[4];
        case EElement.Dark:
          return (string) fixParam.TobiraUnlockElem[5];
        default:
          return string.Empty;
      }
    }

    public string GetUnlockTobiraBirthID()
    {
      FixParam fixParam = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
      if (this.mUnitParam.birthID == 0)
        return string.Empty;
      if (this.mUnitParam.birthID == 100)
      {
        int index = fixParam.TobiraUnlockBirth.Length - 1;
        return (string) fixParam.TobiraUnlockBirth[index];
      }
      int index1 = this.mUnitParam.birthID - 1;
      if (index1 >= 0)
        return (string) fixParam.TobiraUnlockBirth[index1];
      DebugUtility.LogError("インデックスが有効範囲内にありません");
      return string.Empty;
    }

    public int GetChangePieces()
    {
      RarityParam rarityParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetRarityParam((int) this.UnitParam.rare);
      string s = FlowNode_Variable.Get("UNIT_SELECT_TICKET");
      if (!string.IsNullOrEmpty(s) && int.Parse(s) == 1)
        return (int) rarityParam.UnitSelectChangePieceNum;
      return rarityParam != null ? (int) rarityParam.UnitChangePieceNum : 0;
    }

    public int GetAwakeNeedPieces()
    {
      return MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetAwakeNeedPieces(this.AwakeLv);
    }

    public int GetAwakeCost() => 0;

    public int GetAwakeLevelCap()
    {
      RarityParam rarityParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetRarityParam(this.Rarity);
      return rarityParam != null ? (int) rarityParam.UnitAwakeLvCap : 0;
    }

    public JobParam GetJobParam(int jobNo) => this.GetJobData(jobNo)?.Param;

    public JobData GetJobData(int jobNo)
    {
      JobData[] jobs = this.Jobs;
      return jobs == null || jobNo < 0 || jobs.Length <= jobNo ? (JobData) null : jobs[jobNo];
    }

    public JobData GetClassChangeJobData(int jobNo)
    {
      JobData[] jobs = this.Jobs;
      if (jobs == null || jobNo < 0 || this.mNumJobsAvailable <= jobNo)
        return (JobData) null;
      JobParam classChangeJobParam = this.GetClassChangeJobParam(jobNo);
      for (int numJobsAvailable = this.mNumJobsAvailable; numJobsAvailable < jobs.Length; ++numJobsAvailable)
      {
        if (jobs[numJobsAvailable].Param == classChangeJobParam)
          return jobs[numJobsAvailable];
      }
      return (JobData) null;
    }

    public EquipData[] GetRankupEquips(int jobNo) => this.GetJobData(jobNo)?.Equips;

    public EquipData GetRankupEquipData(int jobNo, int slot)
    {
      JobData jobData = this.GetJobData(jobNo);
      if (jobData == null || jobData.Equips == null)
        return (EquipData) null;
      return slot < 0 || slot >= jobData.Equips.Length ? (EquipData) null : jobData.Equips[slot];
    }

    public bool CheckEnableEquipSlot(int jobNo, int slot)
    {
      JobData jobData = this.GetJobData(jobNo);
      return jobData != null && jobData.CheckEnableEquipSlot(slot);
    }

    public bool CheckEnableEnhanceEquipment()
    {
      for (int index1 = 0; index1 < this.Jobs.Length; ++index1)
      {
        JobData job = this.Jobs[index1];
        if (job != null && job.IsActivated)
        {
          EquipData[] equips = job.Equips;
          if (equips != null)
          {
            for (int index2 = 0; index2 < equips.Length; ++index2)
            {
              if (equips[index2] != null && equips[index2].IsValid() && equips[index2].IsEquiped())
              {
                int rank = equips[index2].Rank;
                int rankCap = equips[index2].GetRankCap();
                if (rankCap > 1 && rank < rankCap)
                  return true;
              }
            }
          }
        }
      }
      return false;
    }

    public JobSetParam GetJobSetParam(int jobNo)
    {
      return jobNo < 0 || jobNo >= this.mJobs.Length ? (JobSetParam) null : this.GetJobSetParam2(this.mJobs[jobNo].JobID);
    }

    public JobSetParam GetJobSetParam(string jobID)
    {
      if (this.UnitParam.jobsets == null)
        return (JobSetParam) null;
      MasterParam masterParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam;
      for (int index = 0; index < this.UnitParam.jobsets.Length; ++index)
      {
        if (!string.IsNullOrEmpty(this.UnitParam.jobsets[index]))
        {
          for (JobSetParam jobSetParam = masterParam.GetJobSetParam(this.UnitParam.jobsets[index]); jobSetParam != null; jobSetParam = masterParam.GetJobSetParam(jobSetParam.jobchange))
          {
            if (jobSetParam.job == jobID)
              return jobSetParam;
          }
        }
      }
      return (JobSetParam) null;
    }

    public JobSetParam GetJobSetParam2(string jobID)
    {
      if (this.UnitParam.jobsets == null)
        return (JobSetParam) null;
      MasterParam masterParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam;
      List<string> stringList = new List<string>();
      for (int index = 0; index < this.UnitParam.jobsets.Length; ++index)
        stringList.Add(this.UnitParam.jobsets[index]);
      JobSetParam[] changeJobSetParam = masterParam.GetClassChangeJobSetParam(this.UnitParam.iname);
      if (changeJobSetParam != null && changeJobSetParam.Length > 0)
      {
        for (int index = 0; index < changeJobSetParam.Length; ++index)
          stringList.Add(changeJobSetParam[index].iname);
      }
      for (int index = 0; index < stringList.Count; ++index)
      {
        if (!string.IsNullOrEmpty(stringList[index]))
        {
          for (JobSetParam jobSetParam = masterParam.GetJobSetParam(stringList[index]); jobSetParam != null; jobSetParam = masterParam.GetJobSetParam(jobSetParam.jobchange))
          {
            if (jobSetParam.job == jobID)
              return jobSetParam;
          }
        }
      }
      return (JobSetParam) null;
    }

    public int GetJobRankCap() => JobParam.GetJobRankCap(this.Rarity);

    public int GetJobLevelByJobID(string iname)
    {
      for (int index = 0; index < this.Jobs.Length; ++index)
      {
        if (this.Jobs[index].Param.iname == iname)
          return this.Jobs[index].Rank;
      }
      return 0;
    }

    public bool CheckJobUnlockable(int jobNo)
    {
      JobData jobData = this.GetJobData(jobNo);
      if (jobData == null)
        return false;
      if (jobData.IsActivated)
        return true;
      JobSetParam jobSetParam = this.GetJobSetParam(jobNo);
      if (jobSetParam == null || this.Rarity < jobSetParam.lock_rarity || this.AwakeLv < jobSetParam.lock_awakelv)
        return false;
      if (jobSetParam.lock_jobs != null)
      {
        for (int index1 = 0; index1 < jobSetParam.lock_jobs.Length; ++index1)
        {
          if (jobSetParam.lock_jobs[index1] != null)
          {
            string iname = jobSetParam.lock_jobs[index1].iname;
            bool flag = false;
            for (int index2 = 0; index2 < this.mJobs.Length; ++index2)
            {
              if (this.mJobs[index2] != null && this.mJobs[index2].JobID == iname)
              {
                flag = true;
                break;
              }
            }
            if (flag && jobSetParam.lock_jobs[index1].lv > this.GetJobLevelByJobID(iname))
              return false;
          }
        }
      }
      return true;
    }

    public bool CheckJobUnlock(int jobNo, bool canCreate)
    {
      return this.CheckJobUnlockable(jobNo) && this.GetJobData(jobNo).CheckJobRankUp(this, canCreate);
    }

    public bool CheckJobRankUp() => this.CheckJobRankUp((int) this.mJobIndex);

    public bool CheckJobRankUp(int jobNo) => this.CheckJobRankUpInternal(jobNo, false);

    public bool CheckJobRankUpAllEquip() => this.CheckJobRankUpAllEquip((int) this.mJobIndex);

    public bool CheckJobRankUpAllEquip(int jobNo, bool useCommon = true)
    {
      return this.CheckJobRankUpInternal(jobNo, true, useCommon);
    }

    private bool CheckJobRankUpInternal(int jobNo, bool canCreate, bool useCommon = true)
    {
      JobData jobData = this.GetJobData(jobNo);
      return jobData.IsActivated ? jobData.CheckJobRankUp(this, canCreate, useCommon) : this.CheckJobUnlock(jobNo, canCreate);
    }

    public JobParam GetClassChangeJobParam() => this.GetClassChangeJobParam((int) this.mJobIndex);

    public JobParam GetClassChangeJobParam(int jobNo)
    {
      JobSetParam classChangeJobSet = this.GetClassChangeJobSet(jobNo);
      return classChangeJobSet != null ? MonoSingleton<GameManager>.GetInstanceDirect().GetJobParam(classChangeJobSet.job) : (JobParam) null;
    }

    public void ReserveTemporaryJobs()
    {
      for (int jobNo = 0; jobNo < this.mJobs.Length; ++jobNo)
      {
        JobSetParam classChangeJobSet = this.GetClassChangeJobSet(jobNo);
        if (classChangeJobSet != null)
        {
          bool flag = false;
          for (int index = 0; index < this.mJobs.Length; ++index)
          {
            if (this.mJobs[index].JobID == classChangeJobSet.job)
            {
              flag = true;
              break;
            }
          }
          if (!flag)
          {
            JobData jobData = new JobData();
            jobData.Deserialize(this, new Json_Job()
            {
              iname = classChangeJobSet.job
            });
            Array.Resize<JobData>(ref this.mJobs, this.mJobs.Length + 1);
            this.mJobs[this.mJobs.Length - 1] = jobData;
          }
        }
      }
    }

    public JobSetParam GetClassChangeJobSet(int jobNo)
    {
      if (this.UnitParam.jobsets == null)
        return (JobSetParam) null;
      JobData jobData = this.GetJobData(jobNo);
      if (jobData == null || jobNo >= this.UnitParam.jobsets.Length)
        return (JobSetParam) null;
      for (JobSetParam jobSetParam = MonoSingleton<GameManager>.GetInstanceDirect().GetJobSetParam(this.UnitParam.jobsets[jobNo]); jobSetParam != null; jobSetParam = MonoSingleton<GameManager>.GetInstanceDirect().GetJobSetParam(jobSetParam.jobchange))
      {
        if (jobSetParam.job == jobData.JobID)
          return MonoSingleton<GameManager>.GetInstanceDirect().GetJobSetParam(jobSetParam.jobchange);
      }
      return (JobSetParam) null;
    }

    public bool CheckJobClassChange(int jobNo)
    {
      JobData jobData = this.GetJobData(jobNo);
      return jobData != null && this.CheckJobRankUpAllEquip(Array.IndexOf<JobData>(this.mJobs, jobData));
    }

    public bool CheckClassChangeJobExist() => this.CheckClassChangeJobExist((int) this.mJobIndex);

    public bool CheckClassChangeJobExist(int jobNo)
    {
      if (this.UnitParam.jobsets == null || this.GetJobData(jobNo) == null)
        return false;
      JobSetParam classChangeJobSet = this.GetClassChangeJobSet(jobNo);
      if (classChangeJobSet == null || this.Rarity < classChangeJobSet.lock_rarity || this.AwakeLv < classChangeJobSet.lock_awakelv)
        return false;
      if (classChangeJobSet.lock_jobs != null)
      {
        for (int index1 = 0; index1 < classChangeJobSet.lock_jobs.Length; ++index1)
        {
          if (classChangeJobSet.lock_jobs[index1] != null)
          {
            string iname = classChangeJobSet.lock_jobs[index1].iname;
            bool flag = false;
            for (int index2 = 0; index2 < this.mJobs.Length; ++index2)
            {
              if (this.mJobs[index2] != null && this.mJobs[index2].JobID == iname)
              {
                flag = true;
                break;
              }
            }
            if (flag && classChangeJobSet.lock_jobs[index1].lv > this.GetJobLevelByJobID(iname))
              return false;
          }
        }
      }
      return true;
    }

    public void SetJob(PlayerPartyTypes type)
    {
      JobData jobFor = this.GetJobFor(type);
      if (jobFor == this.CurrentJob)
        return;
      this.SetJob(jobFor);
    }

    public void SetJob(JobData job)
    {
      int jobNo = Array.IndexOf<JobData>(this.mJobs, job);
      if (0 > jobNo)
        return;
      this.SetJobIndex(jobNo);
    }

    public void SetJobIndex(int jobNo)
    {
      this.mJobIndex = (OInt) jobNo;
      this.UpdateUnitLearnAbilityAll();
      this.UpdateUnitBattleAbilityAll();
      this.CalcStatus();
    }

    public void JobUnlock(int jobNo) => this.JobRankUp(jobNo);

    public void JobClassChange(int jobNo)
    {
      JobData jobData1 = this.GetJobData(jobNo);
      JobData baseJob = this.GetBaseJob(jobData1.JobID);
      if (baseJob == null || jobData1 == null)
        return;
      jobData1.JobRankUp();
      JobData jobData2 = this.CurrentJob;
      List<JobData> jobDataList = new List<JobData>((IEnumerable<JobData>) this.mJobs);
      jobDataList.Remove(jobData1);
      jobDataList[jobDataList.IndexOf(baseJob)] = jobData1;
      this.mJobs = jobDataList.ToArray();
      if (jobData2 == baseJob)
        jobData2 = jobData1;
      this.mJobIndex = (OInt) Array.IndexOf<JobData>(this.mJobs, jobData2);
      this.ReserveTemporaryJobs();
      this.UpdateAvailableJobs();
      this.UpdateUnitLearnAbilityAll();
      this.UpdateUnitBattleAbilityAll();
      this.CalcStatus();
    }

    public void JobRankUp(int jobNo)
    {
      JobData jobData = this.GetJobData(jobNo);
      if (jobData == null)
        return;
      jobData.JobRankUp();
      this.ReserveTemporaryJobs();
      this.UpdateAvailableJobs();
      this.UpdateUnitLearnAbilityAll();
      this.UpdateUnitBattleAbilityAll();
      this.CalcStatus();
    }

    public List<ItemData> GetJobRankUpReturnItemData(int jobNo, bool ignoreEquiped = false)
    {
      JobData jobData = this.GetJobData(jobNo);
      if (jobData == null)
        return (List<ItemData>) null;
      List<ItemData> upReturnItemData = new List<ItemData>();
      for (int index = 0; index < jobData.Equips.Length; ++index)
      {
        EquipData equip = jobData.Equips[index];
        if (equip != null && equip.IsValid() && (equip.IsEquiped() || ignoreEquiped))
        {
          if (jobData.Rank >= JobParam.MAX_JOB_RANK && equip.IsEquiped())
          {
            ItemData itemData = this.CreateItemData(equip.ItemID, 1);
            if (itemData != null)
              upReturnItemData.Add(itemData);
            else
              continue;
          }
          List<ItemData> returnItemList = equip.GetReturnItemList();
          if (returnItemList != null)
          {
            foreach (ItemData itemData in returnItemList)
              upReturnItemData.Add(itemData);
          }
        }
      }
      return upReturnItemData;
    }

    private ItemData CreateItemData(string iname, int num)
    {
      Json_Item json = new Json_Item();
      json.iname = iname;
      json.num = num;
      ItemData itemData = new ItemData();
      itemData.Deserialize(json);
      return itemData;
    }

    private void UpdateAvailableJobs()
    {
      this.mNumJobsAvailable = 0;
      if (this.mJobs == null)
        return;
      for (int index = 0; index < this.mJobs.Length; ++index)
      {
        if (this.mJobs[index] != null)
        {
          List<JobSetParam> beforeJobSetList = this.FindBaseJobs(this.mJobs[index].JobID);
          if (beforeJobSetList == null || beforeJobSetList.Count <= 0)
            ++this.mNumJobsAvailable;
          else if (Array.Find<JobData>(this.mJobs, (Predicate<JobData>) (_job => beforeJobSetList.FindIndex((Predicate<JobSetParam>) (_before => _before.job == _job.JobID)) >= 0)) == null)
            ++this.mNumJobsAvailable;
        }
      }
    }

    private JobSetParam FindClassChangeBase2(string jobID)
    {
      UnitParam unitParam = this.UnitParam;
      MasterParam masterParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam;
      JobSetParam[] changeJobSetParam = masterParam.GetClassChangeJobSetParam(unitParam.iname);
      if (changeJobSetParam == null)
        return (JobSetParam) null;
      for (int index1 = 0; index1 < changeJobSetParam.Length; ++index1)
      {
        JobSetParam jobSetParam1 = changeJobSetParam[index1];
        if (jobSetParam1.job == jobID)
        {
          JobSetParam jobSetParam2 = masterParam.GetJobSetParam(jobSetParam1.jobchange);
          if (jobSetParam2 != null)
          {
            for (int index2 = 0; index2 < unitParam.jobsets.Length; ++index2)
            {
              JobSetParam jobSetParam3 = masterParam.GetJobSetParam(unitParam.jobsets[index2]);
              if (jobSetParam3 != null && jobSetParam3.job == jobSetParam2.job)
                return jobSetParam3;
            }
            break;
          }
          break;
        }
      }
      return (JobSetParam) null;
    }

    private List<JobSetParam> FindBaseJobs(string jobID)
    {
      List<JobSetParam> baseJobs = new List<JobSetParam>();
      MasterParam masterParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam;
      JobSetParam[] changeJobSetParam = masterParam.GetClassChangeJobSetParam(this.UnitParam.iname);
      if (changeJobSetParam == null)
        return baseJobs;
      JobSetParam jobSetParam1 = Array.Find<JobSetParam>(changeJobSetParam, (Predicate<JobSetParam>) (_cc => _cc.job == jobID));
      if (jobSetParam1 == null)
        return baseJobs;
      for (JobSetParam jobSetParam2 = masterParam.GetJobSetParam(jobSetParam1.jobchange); jobSetParam2 != null; jobSetParam2 = masterParam.GetJobSetParam(jobSetParam2.jobchange))
        baseJobs.Add(jobSetParam2);
      return baseJobs;
    }

    public void SetEquipAbility(int slot, long iid)
    {
      this.SetEquipAbility((int) this.mJobIndex, slot, iid);
    }

    public void SetEquipAbility(int jobIndex, int slot, long iid)
    {
      this.GetJobData(jobIndex)?.SetAbilitySlot(slot, this.GetAbilityData(iid));
      this.UpdateUnitBattleAbilityAll(jobIndex);
      this.CalcStatus();
    }

    private void UpdateUnitLearnAbilityAll()
    {
      this.UpdateUnitLearnAbilityAll((int) this.mJobIndex);
    }

    private void UpdateUnitLearnAbilityAll(int jobIndex)
    {
      this.mLearnAbilitys.Clear();
      JobData jobData = this.GetJobData(jobIndex);
      if (jobData != null)
      {
        for (int index = 0; index < jobData.LearnAbilitys.Count; ++index)
        {
          AbilityData learnAbility = jobData.LearnAbilitys[index];
          if (learnAbility != null)
            this.mLearnAbilitys.Add(learnAbility);
        }
      }
      if (this.mMasterAbility != null)
        this.mLearnAbilitys.Add(this.mMasterAbility);
      if (this.mMapEffectAbility != null)
        this.mLearnAbilitys.Add(this.mMapEffectAbility);
      if (this.mTobiraMasterAbilitys == null)
        return;
      for (int index = 0; index < this.mTobiraMasterAbilitys.Count; ++index)
      {
        if (this.mTobiraMasterAbilitys[index] != null)
          this.mLearnAbilitys.Add(this.mTobiraMasterAbilitys[index]);
      }
    }

    public void AddUnitBattleAbilityTransform()
    {
      this.AddBattleAbilityArtifacts((int) this.mJobIndex);
      this.AddBattleAbilitySkin((int) this.mJobIndex);
      this.AddBattleAbilityConceptCard((int) this.mJobIndex);
      this.AddBattleAbilityTobira(true);
      this.RefrectionSkillAndAbility((int) this.mJobIndex);
    }

    private void UpdateUnitBattleAbilityAll()
    {
      this.UpdateUnitBattleAbilityAll((int) this.mJobIndex);
    }

    private void UpdateUnitBattleAbilityAll(int jobIndex)
    {
      this.mBattleAbilitys.ForEach((Action<AbilityData>) (ability => ability.ResetDeriveAbility()));
      this.mBattleAbilitys.Clear();
      this.mBattleSkills.Clear();
      JobData jobData = this.GetJobData(jobIndex);
      if (jobData != null)
      {
        for (int index1 = 0; index1 < jobData.AbilitySlots.Length; ++index1)
        {
          AbilityData abilityData = this.GetAbilityData(jobData.AbilitySlots[index1]);
          if (abilityData != null && !this.mBattleAbilitys.Contains(abilityData))
          {
            this.mBattleAbilitys.Add(abilityData);
            if (abilityData.Skills != null)
            {
              for (int index2 = 0; index2 < abilityData.Skills.Count; ++index2)
              {
                SkillData skill = abilityData.Skills[index2];
                if (skill != null && !this.mBattleSkills.Contains(skill))
                  this.mBattleSkills.Add(skill);
              }
            }
          }
        }
        this.AddBattleAbilityArtifacts(jobIndex);
        this.AddBattleAbilitySkin(jobIndex);
      }
      this.AddBattleAbilityConceptCard(jobIndex);
      if (this.mMasterAbility != null && !this.mBattleAbilitys.Contains(this.mMasterAbility))
      {
        this.mBattleAbilitys.Add(this.mMasterAbility);
        if (this.mMasterAbility.Skills != null)
        {
          for (int index = 0; index < this.mMasterAbility.Skills.Count; ++index)
          {
            SkillData skill = this.mMasterAbility.Skills[index];
            if (skill != null && !this.mBattleSkills.Contains(skill))
              this.mBattleSkills.Add(skill);
          }
        }
      }
      if (this.mCollaboAbility != null && !this.mBattleAbilitys.Contains(this.mCollaboAbility))
      {
        this.mBattleAbilitys.Add(this.mCollaboAbility);
        if (this.mCollaboAbility.Skills != null)
        {
          foreach (SkillData skill in this.mCollaboAbility.Skills)
          {
            if (skill != null && !this.mBattleSkills.Contains(skill))
              this.mBattleSkills.Add(skill);
          }
        }
      }
      if (this.mMapEffectAbility != null && !this.mBattleAbilitys.Contains(this.mMapEffectAbility))
      {
        this.mBattleAbilitys.Add(this.mMapEffectAbility);
        if (this.mMapEffectAbility.Skills != null)
        {
          foreach (SkillData skill in this.mMapEffectAbility.Skills)
          {
            if (skill != null && !this.mBattleSkills.Contains(skill))
              this.mBattleSkills.Add(skill);
          }
        }
      }
      this.AddBattleAbilityTobira();
      this.RefrectionSkillAndAbility(jobIndex);
    }

    private void AddBattleAbilityArtifacts(int jobIndex)
    {
      JobData jobData = this.GetJobData(jobIndex);
      if (jobData == null || jobData.Artifacts == null)
        return;
      for (int index1 = 0; index1 < jobData.Artifacts.Length; ++index1)
      {
        if (jobData.Artifacts[index1] != 0L)
        {
          ArtifactData artifactData = jobData.ArtifactDatas[index1];
          if (artifactData != null)
          {
            if (artifactData.LearningAbilities != null)
            {
              for (int index2 = 0; index2 < artifactData.LearningAbilities.Count; ++index2)
              {
                AbilityData learningAbility = artifactData.LearningAbilities[index2];
                if (learningAbility != null && learningAbility.CheckEnableUseAbility(this, jobIndex) && !this.mBattleAbilitys.Contains(learningAbility))
                {
                  this.mBattleAbilitys.Add(learningAbility);
                  if (learningAbility.Skills != null)
                  {
                    for (int index3 = 0; index3 < learningAbility.Skills.Count; ++index3)
                    {
                      SkillData skill = learningAbility.Skills[index3];
                      if (skill != null && !this.mBattleSkills.Contains(skill))
                        this.mBattleSkills.Add(skill);
                    }
                  }
                }
              }
            }
            if (artifactData.IsInspiration())
            {
              InspirationSkillData inspirationSkillData = artifactData.GetCurrentInspirationSkillData();
              if (inspirationSkillData != null)
              {
                AbilityData abilityData = inspirationSkillData.AbilityData;
                if (abilityData != null && !this.mBattleAbilitys.Contains(abilityData) && abilityData.CheckEnableUseAbility(this, jobIndex) && abilityData.Skills != null)
                {
                  bool flag = false;
                  for (int index4 = 0; index4 < abilityData.Skills.Count; ++index4)
                  {
                    SkillData skill = abilityData.Skills[index4];
                    if (skill != null && !this.mBattleSkills.Contains(skill))
                    {
                      this.mBattleSkills.Add(skill);
                      flag = true;
                    }
                  }
                  if (flag)
                    this.mBattleAbilitys.Add(abilityData);
                }
              }
            }
          }
        }
      }
    }

    private void AddBattleAbilitySkin(int jobIndex)
    {
      JobData jobData = this.GetJobData(jobIndex);
      if (jobData == null || string.IsNullOrEmpty(jobData.SelectedSkin))
        return;
      ArtifactData selectedSkinData = jobData.GetSelectedSkinData();
      if (selectedSkinData == null || selectedSkinData.LearningAbilities == null)
        return;
      for (int index1 = 0; index1 < selectedSkinData.LearningAbilities.Count; ++index1)
      {
        AbilityData learningAbility = selectedSkinData.LearningAbilities[index1];
        if (learningAbility != null && learningAbility.CheckEnableUseAbility(this, jobIndex) && !this.mBattleAbilitys.Contains(learningAbility))
        {
          this.mBattleAbilitys.Add(learningAbility);
          if (learningAbility.Skills != null)
          {
            for (int index2 = 0; index2 < learningAbility.Skills.Count; ++index2)
            {
              SkillData skill = learningAbility.Skills[index2];
              if (skill != null && !this.mBattleSkills.Contains(skill))
                this.mBattleSkills.Add(skill);
            }
          }
        }
      }
    }

    private void AddBattleAbilityConceptCard(int jobIndex)
    {
      JobData jobData = this.GetJobData(jobIndex);
      if (jobData == null || this.mConceptCards == null)
        return;
      for (int index1 = 0; index1 < this.mConceptCards.Length; ++index1)
      {
        if (this.mConceptCards[index1] != null)
        {
          List<ConceptCardEquipEffect> enableEquipEffects = this.mConceptCards[index1].GetEnableEquipEffects(this, jobData);
          for (int index2 = 0; index2 < enableEquipEffects.Count; ++index2)
          {
            AbilityData ability = enableEquipEffects[index2].Ability;
            if (ability != null && !this.mBattleAbilitys.Contains(ability) && this.mBattleAbilitys.FindIndex((Predicate<AbilityData>) (btl_abil => btl_abil.Param.iname == ability.Param.iname)) < 0)
            {
              this.mBattleAbilitys.Add(ability);
              if (ability.Skills != null)
              {
                for (int index3 = 0; index3 < ability.Skills.Count; ++index3)
                {
                  SkillData skill = ability.Skills[index3];
                  if (skill != null && !this.mBattleSkills.Contains(skill) && this.mBattleSkills.FindIndex((Predicate<SkillData>) (btl_skill => btl_skill.SkillParam.iname == skill.SkillParam.iname)) < 0)
                    this.mBattleSkills.Add(skill);
                }
              }
            }
          }
        }
      }
    }

    private void AddBattleAbilityTobira(bool transform = false)
    {
      if (this.mTobiraMasterAbilitys == null)
        return;
      for (int index = 0; index < this.mTobiraMasterAbilitys.Count; ++index)
      {
        AbilityData tobiraMasterAbility = this.mTobiraMasterAbilitys[index];
        if (tobiraMasterAbility != null && !this.mBattleAbilitys.Contains(tobiraMasterAbility) && (!transform || tobiraMasterAbility.SlotType != EAbilitySlot.Support))
        {
          this.mBattleAbilitys.Add(tobiraMasterAbility);
          if (tobiraMasterAbility.Skills != null)
          {
            foreach (SkillData skill in tobiraMasterAbility.Skills)
            {
              if (skill != null && !this.mBattleSkills.Contains(skill))
                this.mBattleSkills.Add(skill);
            }
          }
        }
      }
    }

    private void RefrectionSkillAndAbility(int jobIndex)
    {
      this.RefrectionSkillAndAbility(this.mBattleAbilitys, this.mBattleSkills, jobIndex);
    }

    public void RefrectionSkillAndAbility(
      List<AbilityData> ability_list,
      List<SkillData> skill_list,
      int jobIndex = -1)
    {
      foreach (SkillData skill in skill_list)
      {
        if (!string.IsNullOrEmpty(skill.ReplaceSkillId))
        {
          skill.Setup(skill.ReplaceSkillId, skill.Rank, skill.GetRankCap());
          skill.ReplaceSkillId = (string) null;
        }
        string iname1 = skill.SkillParam.iname;
        string iname2 = this.SearchReplacementSkill(iname1);
        if (iname2 != null)
        {
          skill.Setup(iname2, skill.Rank, skill.GetRankCap());
          skill.ReplaceSkillId = iname1;
        }
      }
      if (jobIndex < 0)
        jobIndex = (int) this.mJobIndex;
      JobData jobData = this.GetJobData(jobIndex);
      SkillAbilityDeriveData skillAbilityDeriveData;
      if (jobData == null || this.mJobSkillAbilityDeriveData == null || !this.mJobSkillAbilityDeriveData.TryGetValue(jobData.Param.iname, out skillAbilityDeriveData))
        return;
      UnitData.RefrectionDeriveSkillAndAbility(skillAbilityDeriveData, ability_list, skill_list);
    }

    public string SearchReplacementSkill(string skill_id)
    {
      foreach (SkillData mBattleSkill in this.mBattleSkills)
      {
        if (mBattleSkill.SkillParam.effect_type == SkillEffectTypes.EffReplace && mBattleSkill.SkillParam.ReplaceTargetIdLists != null && mBattleSkill.SkillParam.ReplaceChangeIdLists != null)
        {
          for (int index = 0; index < mBattleSkill.SkillParam.ReplaceTargetIdLists.Count; ++index)
          {
            if (mBattleSkill.SkillParam.ReplaceTargetIdLists[index] == skill_id && index < mBattleSkill.SkillParam.ReplaceChangeIdLists.Count)
              return mBattleSkill.SkillParam.ReplaceChangeIdLists[index];
          }
        }
      }
      return (string) null;
    }

    public string SearchAbilityReplacementSkill(string ability_id)
    {
      foreach (SkillData mBattleSkill in this.mBattleSkills)
      {
        if (mBattleSkill.SkillParam.effect_type == SkillEffectTypes.EffReplace && mBattleSkill.SkillParam.AbilityReplaceTargetIdLists != null && mBattleSkill.SkillParam.AbilityReplaceChangeIdLists != null)
        {
          for (int index = 0; index < mBattleSkill.SkillParam.AbilityReplaceTargetIdLists.Count; ++index)
          {
            if (mBattleSkill.SkillParam.AbilityReplaceTargetIdLists[index] == ability_id && index < mBattleSkill.SkillParam.AbilityReplaceChangeIdLists.Count)
              return mBattleSkill.SkillParam.AbilityReplaceChangeIdLists[index];
          }
        }
      }
      return (string) null;
    }

    public List<AbilityData> SearchDerivedAbilitys(AbilityData baseAbility)
    {
      List<AbilityData> abilityDataList = new List<AbilityData>();
      if (this.CurrentJob != null && this.mJobSkillAbilityDeriveData != null)
      {
        SkillAbilityDeriveData abilityDeriveData = (SkillAbilityDeriveData) null;
        if (this.mJobSkillAbilityDeriveData.TryGetValue(this.CurrentJob.Param.iname, out abilityDeriveData))
        {
          List<AbilityDeriveParam> abilityDeriveParams = abilityDeriveData.GetAvailableAbilityDeriveParams();
          List<SkillDeriveParam> skillDeriveParams = abilityDeriveData.GetAvailableSkillDeriveParams();
          List<AbilityDeriveParam> all = abilityDeriveParams.FindAll((Predicate<AbilityDeriveParam>) (abilityDeriveParam => abilityDeriveParam.BaseAbilityIname == baseAbility.Param.iname));
          List<SkillData> skillDataList = new List<SkillData>();
          foreach (AbilityDeriveParam deriveParam in all)
          {
            AbilityData deriveAbility = baseAbility.CreateDeriveAbility(deriveParam);
            abilityDataList.Add(deriveAbility);
            if (deriveAbility.Skills != null)
            {
              foreach (SkillData skill1 in deriveAbility.Skills)
              {
                SkillData derivedAbilitySkill = skill1;
                if (skillDataList.Find((Predicate<SkillData>) (skill => skill.SkillParam.iname == derivedAbilitySkill.SkillParam.iname)) == null)
                  skillDataList.Add(derivedAbilitySkill);
              }
            }
          }
          foreach (SkillDeriveParam skillDeriveParam1 in skillDeriveParams)
          {
            SkillDeriveParam skillDeriveParam = skillDeriveParam1;
            SkillData skillData = skillDataList.Find((Predicate<SkillData>) (battleSkill => battleSkill.SkillParam.iname == skillDeriveParam.m_BaseParam.iname));
            if (skillData != null)
            {
              SkillData deriveSkill = skillData.CreateDeriveSkill(skillDeriveParam);
              if (skillData.OwnerAbiliy != null)
                skillData.OwnerAbiliy.AddDeriveSkill(deriveSkill);
            }
          }
        }
      }
      return abilityDataList;
    }

    public List<SkillData> SearchDerivedSkills(SkillData baseSkill)
    {
      List<SkillData> skillDataList = new List<SkillData>();
      if (this.CurrentJob != null && this.mJobSkillAbilityDeriveData != null)
      {
        SkillAbilityDeriveData abilityDeriveData = (SkillAbilityDeriveData) null;
        if (this.mJobSkillAbilityDeriveData.TryGetValue(this.CurrentJob.Param.iname, out abilityDeriveData))
        {
          foreach (SkillDeriveParam skillDeriveParam1 in abilityDeriveData.GetAvailableSkillDeriveParams())
          {
            SkillDeriveParam skillDeriveParam = skillDeriveParam1;
            if (!(baseSkill.SkillParam.iname != skillDeriveParam.BaseSkillIname) && !skillDataList.Exists((Predicate<SkillData>) (skill => skill.SkillParam.iname == skillDeriveParam.DeriveSkillIname)))
            {
              SkillData deriveSkill = baseSkill.CreateDeriveSkill(skillDeriveParam);
              skillDataList.Add(deriveSkill);
            }
          }
        }
      }
      return skillDataList;
    }

    public SkillAbilityDeriveData GetSkillAbilityDeriveData(
      JobData target_job = null,
      ArtifactTypes replace_target_slot = ArtifactTypes.None,
      ArtifactParam new_artifact = null)
    {
      JobData job = target_job != null ? target_job : this.CurrentJob;
      int artifactSlotIndex = JobData.GetArtifactSlotIndex(replace_target_slot);
      List<ArtifactParam> artifactParamList = new List<ArtifactParam>();
      for (int slot = 0; slot < 3; ++slot)
      {
        ArtifactParam artifactParam = this.GetEquipArtifactParam(slot, job);
        if (new_artifact != null && slot == artifactSlotIndex)
          artifactParam = new_artifact;
        if (artifactParam != null)
          artifactParamList.Add(artifactParam);
      }
      string[] artifactInames = new string[artifactParamList.Count];
      for (int index = 0; index < artifactParamList.Count; ++index)
        artifactInames[index] = artifactParamList[index].iname;
      return MonoSingleton<GameManager>.Instance.MasterParam.CreateSkillAbilityDeriveDataWithArtifacts(artifactInames);
    }

    private static void RefrectionDeriveSkillAndAbility(
      SkillAbilityDeriveData skillAbilityDeriveData,
      List<AbilityData> refAbilitys,
      List<SkillData> refSkills)
    {
      if (skillAbilityDeriveData == null || refAbilitys == null || refSkills == null)
        return;
      List<AbilityDeriveParam> abilityDeriveParams = skillAbilityDeriveData.GetAvailableAbilityDeriveParams();
      List<SkillDeriveParam> skillDeriveParams = skillAbilityDeriveData.GetAvailableSkillDeriveParams();
      List<AbilityData> abilityDataList = new List<AbilityData>();
      List<SkillData> skillDataList = new List<SkillData>();
      foreach (AbilityDeriveParam abilityDeriveParam1 in abilityDeriveParams)
      {
        AbilityDeriveParam abilityDeriveParam = abilityDeriveParam1;
        AbilityData abilityData = (AbilityData) null;
        int index1 = refAbilitys.FindIndex((Predicate<AbilityData>) (battleAbility => battleAbility.Param.iname == abilityDeriveParam.m_BaseParam.iname));
        if (index1 != -1)
          abilityData = refAbilitys[index1];
        if (abilityData != null)
        {
          AbilityData deriveAbility = abilityData.CreateDeriveAbility(abilityDeriveParam);
          abilityData.AddDeriveAbility(deriveAbility);
          if (abilityData.Skills != null)
            abilityData.Skills.ForEach((Action<SkillData>) (skill => refSkills.Remove(skill)));
          if (deriveAbility.Skills != null)
          {
            for (int index2 = 0; index2 < deriveAbility.Skills.Count; ++index2)
            {
              SkillData skill = deriveAbility.Skills[index2];
              if (skill != null && !refSkills.Contains(skill) && refSkills.FindIndex((Predicate<SkillData>) (btl_skill => btl_skill.SkillParam.iname == skill.SkillParam.iname)) < 0)
                refSkills.Add(skill);
            }
          }
          if (!abilityDataList.Contains(abilityData))
            abilityDataList.Add(abilityData);
          refAbilitys.Insert(index1, deriveAbility);
        }
      }
      foreach (SkillDeriveParam skillDeriveParam1 in skillDeriveParams)
      {
        SkillDeriveParam skillDeriveParam = skillDeriveParam1;
        SkillData skillData = (SkillData) null;
        int index = refSkills.FindIndex((Predicate<SkillData>) (battleSkill => battleSkill.SkillParam.iname == skillDeriveParam.m_BaseParam.iname));
        if (index != -1)
          skillData = refSkills[index];
        if (skillData != null)
        {
          SkillData deriveSkill = skillData.CreateDeriveSkill(skillDeriveParam);
          if (skillData.OwnerAbiliy != null)
            skillData.OwnerAbiliy.AddDeriveSkill(deriveSkill);
          if (!skillDataList.Contains(skillData))
            skillDataList.Add(skillData);
          refSkills.Insert(index, deriveSkill);
        }
      }
      abilityDataList.ForEach((Action<AbilityData>) (ability => refAbilitys.Remove(ability)));
      skillDataList.ForEach((Action<SkillData>) (skill => refSkills.Remove(skill)));
    }

    public void UpdateAbilityRankUp()
    {
      this.UpdateUnitLearnAbilityAll();
      this.UpdateUnitBattleAbilityAll();
      this.CalcStatus();
    }

    public void UpdateConceptCardChanged()
    {
      this.UpdateUnitLearnAbilityAll();
      this.UpdateUnitBattleAbilityAll();
      this.CalcStatus();
    }

    public void UpdateRuneChanged()
    {
      this.UpdateUnitLearnAbilityAll();
      this.UpdateUnitBattleAbilityAll();
      this.CalcStatus();
    }

    public List<AbilityData> CreateLearnAbilitys()
    {
      return this.CreateLearnAbilitys((int) this.mJobIndex);
    }

    public List<AbilityData> CreateLearnAbilitys(int jobIndex)
    {
      List<AbilityData> learnAbilitys = new List<AbilityData>();
      JobData jobData = this.GetJobData(jobIndex);
      if (jobData != null)
      {
        for (int index = 0; index < jobData.LearnAbilitys.Count; ++index)
        {
          if (!learnAbilitys.Contains(jobData.LearnAbilitys[index]))
            learnAbilitys.Add(jobData.LearnAbilitys[index]);
        }
      }
      if (this.mMasterAbility != null && !learnAbilitys.Contains(this.mMasterAbility))
        learnAbilitys.Add(this.mMasterAbility);
      if (this.mMapEffectAbility != null && !learnAbilitys.Contains(this.mMapEffectAbility))
        learnAbilitys.Add(this.mMapEffectAbility);
      if (this.mTobiraMasterAbilitys != null)
      {
        for (int index = 0; index < this.mTobiraMasterAbilitys.Count; ++index)
        {
          AbilityData tobiraMasterAbility = this.mTobiraMasterAbilitys[index];
          if (tobiraMasterAbility != null && !learnAbilitys.Contains(tobiraMasterAbility))
            learnAbilitys.Add(tobiraMasterAbility);
        }
      }
      return learnAbilitys;
    }

    public List<AbilityData> GetAllLearnedAbilities(bool enableDerive = false)
    {
      List<AbilityData> refAbilitys = new List<AbilityData>(32);
      for (int index1 = 0; index1 < this.mJobs.Length; ++index1)
      {
        JobData mJob = this.mJobs[index1];
        if (mJob != null && mJob.IsActivated)
        {
          for (int index2 = 0; index2 < mJob.LearnAbilitys.Count; ++index2)
          {
            AbilityData learnAbility = mJob.LearnAbilitys[index2];
            if (learnAbility != null && learnAbility.IsValid() && !refAbilitys.Contains(learnAbility))
              refAbilitys.Add(learnAbility);
          }
        }
      }
      if (this.mMasterAbility != null && !refAbilitys.Contains(this.mMasterAbility))
        refAbilitys.Add(this.mMasterAbility);
      if (this.mMapEffectAbility != null && !refAbilitys.Contains(this.mMapEffectAbility))
        refAbilitys.Add(this.mMapEffectAbility);
      if (this.mTobiraMasterAbilitys != null)
      {
        for (int index = 0; index < this.mTobiraMasterAbilitys.Count; ++index)
        {
          AbilityData tobiraMasterAbility = this.mTobiraMasterAbilitys[index];
          if (tobiraMasterAbility != null && !refAbilitys.Contains(tobiraMasterAbility))
            refAbilitys.Add(tobiraMasterAbility);
        }
      }
      if (enableDerive && this.mJobSkillAbilityDeriveData != null)
      {
        JobData currentJob = this.CurrentJob;
        SkillAbilityDeriveData skillAbilityDeriveData = (SkillAbilityDeriveData) null;
        if (this.mJobSkillAbilityDeriveData.TryGetValue(currentJob.Param.iname, out skillAbilityDeriveData))
        {
          List<SkillData> refSkills = new List<SkillData>();
          foreach (AbilityData abilityData in refAbilitys)
          {
            if (abilityData.Skills != null)
            {
              foreach (SkillData skill in abilityData.Skills)
              {
                if (skill != null && !refSkills.Contains(skill))
                  refSkills.Add(skill);
              }
            }
          }
          UnitData.RefrectionDeriveSkillAndAbility(skillAbilityDeriveData, refAbilitys, refSkills);
        }
      }
      return refAbilitys;
    }

    public AbilityData[] CreateEquipAbilitys() => this.CreateEquipAbilitys((int) this.mJobIndex);

    public AbilityData[] CreateEquipAbilitys(int jobIndex)
    {
      AbilityData[] equipAbilitys = new AbilityData[5];
      Array.Clear((Array) equipAbilitys, 0, equipAbilitys.Length);
      JobData jobData = this.GetJobData(jobIndex);
      if (jobData != null)
      {
        for (int index = 0; index < jobData.AbilitySlots.Length; ++index)
        {
          long abilitySlot = jobData.AbilitySlots[index];
          equipAbilitys[index] = this.GetAbilityData(abilitySlot);
        }
      }
      return equipAbilitys;
    }

    public List<SkillData> CreateEquipSkills() => this.CreateEquipSkills((int) this.mJobIndex);

    public List<SkillData> CreateEquipSkills(int jobIndex)
    {
      List<SkillData> equipSkills = new List<SkillData>();
      JobData jobData = this.GetJobData(jobIndex);
      if (jobData != null)
      {
        for (int index1 = 0; index1 < jobData.AbilitySlots.Length; ++index1)
        {
          AbilityData abilityData = this.GetAbilityData(jobData.AbilitySlots[index1]);
          if (abilityData != null)
          {
            for (int index2 = 0; index2 < abilityData.Skills.Count; ++index2)
            {
              SkillData skill = abilityData.Skills[index2];
              if (skill != null && !equipSkills.Contains(skill))
                equipSkills.Add(skill);
            }
          }
        }
      }
      return equipSkills;
    }

    public AbilityData GetAbilityData(long iid)
    {
      if (iid <= 0L)
        return (AbilityData) null;
      for (int jobNo = 0; jobNo < this.mJobs.Length; ++jobNo)
      {
        JobData jobData = this.GetJobData(jobNo);
        if (jobData != null)
        {
          for (int index = 0; index < jobData.LearnAbilitys.Count; ++index)
          {
            AbilityData learnAbility = jobData.LearnAbilitys[index];
            if (learnAbility != null && learnAbility.IsValid() && learnAbility.UniqueID == iid)
              return learnAbility;
          }
        }
      }
      if (this.mMasterAbility != null && this.mMasterAbility.UniqueID == iid)
        return this.mMasterAbility;
      if (this.mMapEffectAbility != null && this.mMapEffectAbility.UniqueID == iid)
        return this.mMapEffectAbility;
      if (this.mTobiraMasterAbilitys != null)
      {
        for (int index = 0; index < this.mTobiraMasterAbilitys.Count; ++index)
        {
          AbilityData tobiraMasterAbility = this.mTobiraMasterAbilitys[index];
          if (tobiraMasterAbility != null && tobiraMasterAbility.UniqueID == iid)
            return tobiraMasterAbility;
        }
      }
      return (AbilityData) null;
    }

    public SkillData GetSkillData(string iname)
    {
      int jobIndex = 0;
      return this.GetSkillData(iname, ref jobIndex);
    }

    public SkillData GetSkillData(string iname, ref int jobIndex)
    {
      for (int jobNo = 0; jobNo < this.mJobs.Length; ++jobNo)
      {
        JobData jobData = this.GetJobData(jobNo);
        if (jobData != null)
        {
          for (int index1 = 0; index1 < jobData.LearnAbilitys.Count; ++index1)
          {
            AbilityData learnAbility = jobData.LearnAbilitys[index1];
            if (learnAbility != null && learnAbility.IsValid())
            {
              for (int index2 = 0; index2 < learnAbility.Skills.Count; ++index2)
              {
                SkillData skill = learnAbility.Skills[index2];
                if (skill != null && skill.IsValid())
                {
                  if (!(skill.SkillID != iname))
                    return skill;
                  string str = this.SearchReplacementSkill(iname);
                  if (!string.IsNullOrEmpty(str) && skill.SkillID == str || skill.IsDerivedSkill && skill.m_BaseSkillIname == iname)
                    return skill;
                }
              }
            }
          }
        }
      }
      if (this.mMasterAbility != null)
      {
        foreach (SkillData skill in this.mMasterAbility.Skills)
        {
          if (skill != null && skill.IsValid() && !(skill.SkillID != iname))
            return skill;
        }
        if (this.mMasterAbility.IsDeriveBaseAbility)
        {
          SkillData skillDataInSkills = this.mMasterAbility.DerivedAbility.FindSkillDataInSkills(iname);
          if (skillDataInSkills != null)
            return skillDataInSkills;
        }
      }
      if (this.mCollaboAbility != null)
      {
        foreach (SkillData skill in this.mCollaboAbility.Skills)
        {
          if (skill != null && skill.IsValid() && !(skill.SkillID != iname))
            return skill;
        }
        if (this.mCollaboAbility.IsDeriveBaseAbility)
        {
          SkillData skillDataInSkills = this.mCollaboAbility.FindSkillDataInSkills(iname);
          if (skillDataInSkills != null)
            return skillDataInSkills;
        }
      }
      if (this.mMapEffectAbility != null)
      {
        foreach (SkillData skill in this.mMapEffectAbility.Skills)
        {
          if (skill != null && skill.IsValid() && !(skill.SkillID != iname))
            return skill;
        }
        if (this.mMapEffectAbility.IsDeriveBaseAbility)
        {
          SkillData skillDataInSkills = this.mMapEffectAbility.FindSkillDataInSkills(iname);
          if (skillDataInSkills != null)
            return skillDataInSkills;
        }
      }
      if (this.mTobiraMasterAbilitys != null)
      {
        for (int index = 0; index < this.mTobiraMasterAbilitys.Count; ++index)
        {
          AbilityData tobiraMasterAbility = this.mTobiraMasterAbilitys[index];
          if (tobiraMasterAbility != null)
          {
            foreach (SkillData skill in tobiraMasterAbility.Skills)
            {
              if (skill != null && skill.IsValid() && !(skill.SkillID != iname))
                return skill;
            }
          }
        }
      }
      return (SkillData) null;
    }

    public int GetSkillUsedCost(SkillData skill) => this.GetSkillUsedCost(skill.SkillParam);

    public int GetSkillUsedCost(SkillParam skill)
    {
      int skillUsedCost = skill.cost;
      if (skill.effect_type != SkillEffectTypes.GemsGift && skill.type != ESkillType.Attack && skill.type != ESkillType.Item && !skill.IsNoUsedJewelBuff())
      {
        int num = Math.Max(skillUsedCost + (int) this.Status[BattleBonus.UsedJewel], 0);
        skillUsedCost = Math.Max(num + num * (int) this.Status[BattleBonus.UsedJewelRate] / 100, 0);
      }
      return skillUsedCost;
    }

    public UnitJobOverwriteParam GetUnitJobOverwriteParam(string job_iname)
    {
      UnitJobOverwriteParam jobOverwriteParam = (UnitJobOverwriteParam) null;
      if (this.mUnitJobOverwriteParams != null)
        this.mUnitJobOverwriteParams.TryGetValue(job_iname, out jobOverwriteParam);
      return jobOverwriteParam;
    }

    public override string ToString() => this.UnitParam.name + "(" + this.GetType().Name + ")";

    public void SetVirtualAwakeLv(int target_awake_lv) => this.mAwakeLv = (OInt) target_awake_lv;

    public bool CheckEnableEquipment(ItemParam item)
    {
      if (item == null || item.type != EItemType.Equip)
        return false;
      for (int jobNo = 0; jobNo < this.NumJobsAvailable; ++jobNo)
      {
        JobData jobData = this.GetJobData(jobNo);
        if (jobData != null && jobData.Equips != null)
        {
          for (int index = 0; index < jobData.Equips.Length; ++index)
          {
            EquipData equip = jobData.Equips[index];
            if (equip != null && equip.IsValid() && !equip.IsEquiped() && equip.ItemParam == item && equip.ItemParam.equipLv <= this.Lv)
              return true;
          }
        }
      }
      return false;
    }

    public void UpdateBadge()
    {
      this.SetBadgeState(UnitBadgeTypes.EnableAwaking, this.CheckUnitAwaking());
    }

    private void SetBadgeState(UnitBadgeTypes type, bool flag)
    {
      if (flag)
        this.BadgeState |= type;
      else
        this.BadgeState &= ~type;
    }

    private bool CheckEnableEquipmentBadge()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      for (int jobNo = 0; jobNo < this.NumJobsAvailable; ++jobNo)
      {
        JobData jobData = this.GetJobData(jobNo);
        if (jobData != null && jobData.Equips != null && jobData.IsActivated)
        {
          for (int index = 0; index < jobData.Equips.Length; ++index)
          {
            EquipData equip = jobData.Equips[index];
            if (equip != null && equip.IsValid() && !equip.IsEquiped() && (player.HasItem(equip.ItemID) || player.CheckEnableCreateItem(equip.ItemParam)) && equip.ItemParam.equipLv <= this.Lv)
              return true;
          }
        }
      }
      return false;
    }

    public ArtifactParam GetEquipArmArtifact() => this.GetEquipArtifactParam(0);

    public ArtifactParam GetEquipArtifactParam(int slot, JobData job = null)
    {
      JobData job1 = job;
      if (job == null)
        job1 = this.CurrentJob;
      if (job != null)
      {
        ArtifactData equipArtifactData = this.GetEquipArtifactData(slot, job1);
        if (equipArtifactData != null)
          return equipArtifactData.ArtifactParam;
        if (slot == 0)
          return MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetArtifactParam(job1.Param.artifact);
      }
      return (ArtifactParam) null;
    }

    public ArtifactData GetEquipArtifactData(int slot, JobData job = null)
    {
      JobData jobData = job;
      if (job == null)
        jobData = this.CurrentJob;
      return jobData != null && jobData.Artifacts != null && jobData.Artifacts.Length > slot ? this.GetSortedArtifactDatas(jobData.ArtifactDatas)[slot] : (ArtifactData) null;
    }

    public ArtifactData[] GetSortedArtifactDatas(ArtifactData[] artifacts)
    {
      if (artifacts == null || artifacts.Length <= 0)
        return new ArtifactData[3];
      Dictionary<int, List<ArtifactData>> dictionary = new Dictionary<int, List<ArtifactData>>();
      for (int index = 0; index < artifacts.Length; ++index)
      {
        if (artifacts[index] != null)
        {
          int type = (int) artifacts[index].ArtifactParam.type;
          if (!dictionary.ContainsKey(type))
            dictionary.Add(type, new List<ArtifactData>());
          dictionary[type].Add(artifacts[index]);
        }
      }
      List<ArtifactData> artifactDataList = new List<ArtifactData>();
      int length = Enum.GetNames(typeof (ArtifactTypes)).Length;
      for (int key = 1; key <= length; ++key)
      {
        if (dictionary.ContainsKey(key))
          artifactDataList.AddRange((IEnumerable<ArtifactData>) dictionary[key]);
      }
      int num = 3 - artifactDataList.Count;
      if (num > 0)
      {
        for (int index = 0; index < num; ++index)
          artifactDataList.Add((ArtifactData) null);
      }
      return artifactDataList.ToArray();
    }

    public bool SetEquipArtifactData(int slot, ArtifactData artifact)
    {
      return this.SetEquipArtifactData((int) this.mJobIndex, slot, artifact);
    }

    public bool SetEquipArtifactData(int job_index, int slot, ArtifactData artifact, bool is_calc = true)
    {
      JobData jobData = this.GetJobData(job_index);
      if (jobData == null || !jobData.SetEquipArtifact(slot, artifact))
        return false;
      this.UpdateArtifact(job_index, is_calc);
      return true;
    }

    public void UpdateArtifact(int job_index, bool is_calc = true, bool refreshSkillAbilityDeriveData = false)
    {
      if (refreshSkillAbilityDeriveData)
        this.mJobSkillAbilityDeriveData = MonoSingleton<GameManager>.Instance.MasterParam.CreateSkillAbilityDeriveDataWithArtifacts(this.mJobs);
      this.UpdateUnitLearnAbilityAll(job_index);
      this.UpdateUnitBattleAbilityAll(job_index);
      if (!is_calc)
        return;
      this.CalcStatus((int) this.mLv, job_index, ref this.mStatus);
    }

    public void SetUniqueID(long uniqueID) => this.mUniqueID = uniqueID;

    private void UpdateSyncTime() => this.LastSyncTime = Time.realtimeSinceStartup;

    public string GetUnitSkinVoiceSheetName(int jobIndex = -1)
    {
      if (jobIndex == -1)
        jobIndex = this.JobIndex;
      ArtifactParam selectedSkinData = this.GetSelectedSkinData(jobIndex);
      JobData job = this.Jobs == null || this.Jobs[jobIndex] == null ? (JobData) null : this.Jobs[jobIndex];
      return AssetPath.UnitVoiceFileName(this.UnitParam, selectedSkinData, this.UnitParam.GetJobVoice(job == null ? string.Empty : job.JobID));
    }

    public string GetUnitSkinVoiceCueName(int jobIndex = -1)
    {
      return AssetPath.UnitVoiceFileName(this.UnitParam, jobVoice: string.Empty);
    }

    public string GetUnitJobVoiceSheetName(int jobIndex = -1)
    {
      if (jobIndex == -1)
        jobIndex = this.JobIndex;
      JobData job = this.Jobs == null || this.Jobs[jobIndex] == null ? (JobData) null : this.Jobs[jobIndex];
      return AssetPath.UnitVoiceFileName(this.UnitParam, jobVoice: this.UnitParam.GetJobVoice(job == null ? string.Empty : job.JobID));
    }

    public UnitData.UnitPlaybackVoiceData GetUnitPlaybackVoiceData()
    {
      string skinVoiceSheetName = this.GetUnitSkinVoiceSheetName();
      if (string.IsNullOrEmpty(skinVoiceSheetName))
      {
        DebugUtility.LogError("UnitDataにボイス設定が存在しません");
        return (UnitData.UnitPlaybackVoiceData) null;
      }
      string sheetName = "VO_" + skinVoiceSheetName;
      string cueNamePrefix = this.GetUnitSkinVoiceCueName() + "_";
      MySound.Voice _voice = new MySound.Voice(sheetName, skinVoiceSheetName, cueNamePrefix);
      CriAtomExAcb acb = _voice.FindAcb(sheetName);
      if (acb == null)
      {
        DebugUtility.LogError("Acbファイルが存在しません");
        return (UnitData.UnitPlaybackVoiceData) null;
      }
      CriAtomEx.CueInfo[] cueInfoList = acb.GetCueInfoList();
      if (cueInfoList == null || cueInfoList.Length <= 0)
      {
        DebugUtility.LogError("CueInfoが存在しません");
        return (UnitData.UnitPlaybackVoiceData) null;
      }
      UnitData.UnitPlaybackVoiceData playbackVoiceData = new UnitData.UnitPlaybackVoiceData();
      playbackVoiceData.Init(this, _voice, cueInfoList, skinVoiceSheetName);
      return playbackVoiceData;
    }

    public bool CheckUnlockPlaybackVoice() => this.Rarity + 1 >= 5;

    public UnitData.CharacterQuestUnlockProgress SaveUnlockProgress()
    {
      UnitData.CharacterQuestParam charaEpisodeData = this.GetCurrentCharaEpisodeData();
      if (charaEpisodeData == null)
        return (UnitData.CharacterQuestUnlockProgress) null;
      return new UnitData.CharacterQuestUnlockProgress()
      {
        Level = this.Lv,
        Rarity = this.Rarity,
        CondQuest = charaEpisodeData.Param,
        ClearUnlocksCond = this.IsChQuestParentUnlocked(charaEpisodeData.Param)
      };
    }

    public bool IsQuestUnlocked(UnitData.CharacterQuestUnlockProgress progress)
    {
      UnitData.CharacterQuestParam charaEpisodeData = this.GetCurrentCharaEpisodeData();
      bool flag = this.IsChQuestParentUnlocked(charaEpisodeData.Param);
      int ulvmin = charaEpisodeData.Param.EntryConditionCh.ulvmin;
      int num = charaEpisodeData.Param.EntryConditionCh.rmin - 1;
      return flag && ulvmin <= this.Lv && num <= this.Rarity && (progress == null || progress.CondQuest.iname == charaEpisodeData.Param.iname && (!progress.ClearUnlocksCond || ulvmin > progress.Level || num > progress.Rarity));
    }

    public int CharacterQuestRarity => UnitParam.MASTER_QUEST_RARITY - 1;

    public bool IsOpenCharacterQuest()
    {
      OInt characterQuestRarity = (OInt) this.CharacterQuestRarity;
      OInt masterQuestLv = (OInt) UnitParam.MASTER_QUEST_LV;
      return (int) characterQuestRarity > -1 && (int) masterQuestLv > 0 && this.Rarity >= (int) characterQuestRarity && this.Lv >= (int) masterQuestLv && this.IsSetCharacterQuest();
    }

    public List<QuestParam> FindCondQuests() => this.mCharacterQuests;

    public UnitData.CharacterQuestParam GetCurrentCharaEpisodeData()
    {
      if (!this.IsSetCharacterQuest())
        return (UnitData.CharacterQuestParam) null;
      UnitData.CharacterQuestParam[] charaEpisodeList = this.GetCharaEpisodeList();
      if (charaEpisodeList == null)
        return (UnitData.CharacterQuestParam) null;
      for (int index = 0; index < charaEpisodeList.Length; ++index)
      {
        if (charaEpisodeList[index].Param.state != QuestStates.Cleared)
          return charaEpisodeList[index];
      }
      return (UnitData.CharacterQuestParam) null;
    }

    public UnitData.CharacterQuestParam[] GetCharaEpisodeList()
    {
      if (!this.IsSetCharacterQuest())
        return (UnitData.CharacterQuestParam[]) null;
      List<QuestParam> questParamList = new List<QuestParam>();
      List<QuestParam> condQuests = this.FindCondQuests();
      if (condQuests.Count <= 0)
        return (UnitData.CharacterQuestParam[]) null;
      UnitData.CharacterQuestParam[] charaEpisodeList = new UnitData.CharacterQuestParam[condQuests.Count];
      for (int index = 0; index < condQuests.Count; ++index)
      {
        UnitData.CharacterQuestParam characterQuestParam = new UnitData.CharacterQuestParam();
        characterQuestParam.EpisodeNum = index + 1;
        characterQuestParam.Param = condQuests[index];
        characterQuestParam.EpisodeTitle = condQuests[index].name;
        characterQuestParam.IsNew = condQuests[index].state == QuestStates.New;
        string empty = string.Empty;
        characterQuestParam.IsAvailable = condQuests[index].IsEntryQuestConditionCh(this, ref empty);
        charaEpisodeList[index] = characterQuestParam;
      }
      return charaEpisodeList;
    }

    public bool OpenNewCharacterEpisodeOnLevelUp(
      int beforeLv,
      UnitData.CharacterQuestParam targetQuset = null)
    {
      if (beforeLv >= this.Lv)
        return false;
      UnitData.CharacterQuestParam characterQuestParam = targetQuset != null ? targetQuset : this.GetCurrentCharaEpisodeData();
      if (characterQuestParam == null || !this.IsChQuestParentUnlocked(characterQuestParam.Param) || characterQuestParam.Param.EntryConditionCh == null)
        return false;
      int ulvmin = characterQuestParam.Param.EntryConditionCh.ulvmin;
      return ulvmin <= this.Lv && ulvmin > beforeLv && characterQuestParam.IsAvailable;
    }

    public bool OpenCharacterQuestOnLevelUp(int beforeLv)
    {
      if (beforeLv >= this.Lv)
        return false;
      OInt masterQuestLv = (OInt) UnitParam.MASTER_QUEST_LV;
      return (int) masterQuestLv > 0 && this.IsSetCharacterQuest() && (int) masterQuestLv > beforeLv && (int) masterQuestLv <= this.Lv;
    }

    public bool OpenCharacterQuestOnQuestResult(QuestParam startParam, int beforeLv)
    {
      UnitData.CharacterQuestParam charaEpisodeData = this.GetCurrentCharaEpisodeData();
      return charaEpisodeData != null && this.IsChQuestParentUnlocked(charaEpisodeData.Param) && (startParam.iname != charaEpisodeData.Param.iname && charaEpisodeData.IsAvailable || this.OpenNewCharacterEpisodeOnLevelUp(beforeLv, charaEpisodeData));
    }

    public bool OpenCharacterQuestOnRarityUp(int beforeRarity)
    {
      if (beforeRarity >= this.Rarity)
        return false;
      OInt oint = (OInt) (UnitParam.MASTER_QUEST_RARITY - 1);
      return this.IsSetCharacterQuest() && (int) oint > beforeRarity && (int) oint <= this.Rarity;
    }

    public bool IsSetCharacterQuest()
    {
      return this.mCharacterQuests != null && this.mCharacterQuests.Count > 0;
    }

    public void FindCharacterQuestParams()
    {
      if (this.mCharacterQuests != null)
        return;
      this.mCharacterQuests = new List<QuestParam>();
      string str = "WD_CHARA";
      QuestParam[] quests = MonoSingleton<GameManager>.Instance.Quests;
      for (int index = 0; index < quests.Length; ++index)
      {
        if (quests[index].world == str && quests[index].ChapterID == this.UnitID)
          this.mCharacterQuests.Add(quests[index]);
      }
    }

    public void ResetCharacterQuestParams() => this.mCharacterQuests = (List<QuestParam>) null;

    public bool IsChQuestParentUnlocked(QuestParam quest)
    {
      if (quest == null)
        return false;
      bool flag = true;
      List<AbilityData> learnedAbilities = this.GetAllLearnedAbilities();
      List<QuestClearUnlockUnitDataParam> all = this.SkillUnlocks.FindAll((Predicate<QuestClearUnlockUnitDataParam>) (p => p.qids != null && -1 != Array.FindIndex<string>(p.qids, (Predicate<string>) (s => s == quest.iname))));
      if (all == null)
        return true;
      for (int index = 0; index < all.Count; ++index)
      {
        QuestClearUnlockUnitDataParam unlock = all[index];
        AbilityData abilityData = (AbilityData) null;
        if (!unlock.qcnd)
        {
          switch (unlock.type)
          {
            case QuestClearUnlockUnitDataParam.EUnlockTypes.Skill:
              abilityData = learnedAbilities.Find((Predicate<AbilityData>) (p => p.AbilityID == unlock.parent_id));
              flag = abilityData != null;
              break;
            case QuestClearUnlockUnitDataParam.EUnlockTypes.Ability:
              flag = Array.Find<JobData>(this.Jobs, (Predicate<JobData>) (p => p.JobID == unlock.parent_id && p.IsActivated)) != null;
              break;
          }
          if (flag && !unlock.add)
          {
            switch (unlock.type)
            {
              case QuestClearUnlockUnitDataParam.EUnlockTypes.Skill:
                flag = Array.Find<LearningSkill>(abilityData.LearningSkills, (Predicate<LearningSkill>) (p => p.iname == unlock.old_id)) != null;
                break;
              case QuestClearUnlockUnitDataParam.EUnlockTypes.Ability:
                flag = learnedAbilities.Find((Predicate<AbilityData>) (p => p.AbilityID == unlock.old_id)) != null;
                break;
              case QuestClearUnlockUnitDataParam.EUnlockTypes.MasterAbility:
                flag = this.MasterAbility != null && this.MasterAbility.AbilityID == unlock.old_id;
                break;
            }
          }
          if (!flag)
            return false;
        }
      }
      return true;
    }

    public JobData GetJobFor(PlayerPartyTypes type) => this.CurrentJob;

    public void SetJobFor(PlayerPartyTypes type, JobData job)
    {
      if (this.mPartyJobs == null)
        this.mPartyJobs = new long[17];
      else if (this.mPartyJobs.Length != 17)
        Array.Resize<long>(ref this.mPartyJobs, 17);
      this.mPartyJobs[(int) type] = job.UniqueID;
    }

    public bool CheckCommon(int index, int slot)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      JobData job = this.Jobs[index];
      string rankupItem = job.GetRankupItems(job.Rank)[slot];
      ItemData itemDataByItemId1 = instance.Player.FindItemDataByItemID(rankupItem);
      ItemParam itemParam = instance.GetItemParam(rankupItem);
      if (itemParam == null || !itemParam.IsCommon || itemDataByItemId1 != null && itemDataByItemId1.Num >= 1)
        return false;
      ItemParam commonEquip = instance.MasterParam.GetCommonEquip(itemParam, job.Rank == 0);
      if (commonEquip == null)
        return false;
      ItemData itemDataByItemId2 = instance.Player.FindItemDataByItemID(commonEquip.iname);
      if (itemDataByItemId2 == null || instance.MasterParam.FixParam.EquipCommonPieceNum == null)
        return false;
      int num1 = (int) instance.MasterParam.FixParam.EquipCommonPieceNum[itemParam.rare];
      int num2 = job.Rank <= 0 ? 1 : num1;
      return itemDataByItemId2 != null && itemDataByItemId2.Num >= num2;
    }

    public string UnlockedCollaboSkillIds()
    {
      string empty = string.Empty;
      if (this.mCollaboAbility != null)
      {
        for (int index = 0; index < this.mCollaboAbility.Skills.Count; ++index)
        {
          if (!string.IsNullOrEmpty(empty))
            empty += ",";
          empty += this.mCollaboAbility.Skills[index].SkillParam.iname;
        }
      }
      return empty;
    }

    public bool IsThrow => this.UnitParam != null && this.UnitParam.IsThrow();

    public static int CompareTo_Iname(UnitData unit1, UnitData unit2)
    {
      return unit2.UnitParam.iname.CompareTo(unit1.UnitParam.iname);
    }

    public static int CompareTo_Lv(UnitData unit1, UnitData unit2) => unit2.Lv - unit1.Lv;

    public static int CompareTo_JobRank(UnitData unit1, UnitData unit2)
    {
      return unit2.CurrentJob.Rank - unit1.CurrentJob.Rank;
    }

    public static int CompareTo_Rarity(UnitData unit1, UnitData unit2)
    {
      return unit2.Rarity - unit1.Rarity;
    }

    public static int CompareTo_RarityInit(UnitData unit1, UnitData unit2)
    {
      return (int) unit2.UnitParam.rare - (int) unit1.UnitParam.rare;
    }

    public static int CompareTo_RarityMax(UnitData unit1, UnitData unit2)
    {
      return (int) unit2.UnitParam.raremax - (int) unit1.UnitParam.raremax;
    }

    public bool IsKnockBack => this.UnitParam != null && this.UnitParam.IsKnockBack();

    public bool IsChanging => this.UnitParam != null && this.UnitParam.IsChanging();

    public int CalcTotalParameter()
    {
      return 0 + GameUtility.CalcRateRoundDown((long) (int) this.mStatus.param.hp, 10L) + (int) this.mStatus.param.atk + (int) this.mStatus.param.def + (int) this.mStatus.param.mag + (int) this.mStatus.param.mnd + (int) this.mStatus.param.spd * 5 + (int) this.mStatus.param.dex + (int) this.mStatus.param.cri + (int) this.mStatus.param.luk;
    }

    public SRPG.TobiraData GetTobiraData(TobiraParam.Category category)
    {
      return this.mTobiraData.Find((Predicate<SRPG.TobiraData>) (tobira => tobira.Param != null && tobira.Param.TobiraCategory == category));
    }

    public bool CheckTobiraIsUnlocked(TobiraParam.Category category)
    {
      SRPG.TobiraData tobiraData = this.GetTobiraData(category);
      return tobiraData != null && tobiraData.IsUnlocked;
    }

    public void SetTobiraData(List<SRPG.TobiraData> tobiraData) => this.mTobiraData = tobiraData;

    public void DtuTobiraCopyTo(UnitData from_unit_data)
    {
      if (from_unit_data == null)
        return;
      this.mTobiraData = from_unit_data.mTobiraData;
      this.mUnlockTobiraNum = from_unit_data.mUnlockTobiraNum;
      this.mTobiraMasterAbilitys = from_unit_data.mTobiraMasterAbilitys;
    }

    public void DtuSetupSkillAbilityDerive()
    {
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) instanceDirect))
        return;
      this.mJobSkillAbilityDeriveData = instanceDirect.MasterParam.CreateSkillAbilityDeriveDataWithArtifacts(this.mJobs);
    }

    public void OverWrite(UnitOverWriteData over_write_data, eOverWritePartyType ow_party_type)
    {
      if (over_write_data == null)
        return;
      int index1 = Array.FindIndex<JobData>(this.mJobs, (Predicate<JobData>) (job => job.UniqueID == over_write_data.JobUniqueID));
      if (index1 >= 0)
        this.SetJobIndex(index1);
      if (over_write_data.Abilities != null)
      {
        for (int slot = 0; slot < over_write_data.Abilities.Length; ++slot)
        {
          AbilityData abilityData = this.GetAbilityData(over_write_data.Abilities[slot]);
          this.CurrentJob.SetAbilitySlot(slot, abilityData);
        }
      }
      this.UpdateUnitBattleAbilityAll();
      for (int index2 = 0; index2 < this.mJobs.Length; ++index2)
      {
        for (int slot = 0; slot < this.mJobs[index2].Artifacts.Length; ++slot)
          this.mJobs[index2].SetEquipArtifact(slot, (ArtifactData) null, true);
      }
      if (over_write_data.Artifacts != null)
      {
        for (int slot = 0; slot < over_write_data.Artifacts.Length; ++slot)
        {
          ArtifactData artifactByUniqueId = MonoSingleton<GameManager>.Instance.Player.FindArtifactByUniqueID(over_write_data.Artifacts[slot]);
          this.CurrentJob.SetEquipArtifact(slot, artifactByUniqueId, true);
        }
      }
      ConceptCardData[] conceptCardDataArray = new ConceptCardData[2];
      if (over_write_data.ConceptCards != null)
      {
        ConceptCardData conceptCardData = (ConceptCardData) null;
        for (int index3 = 0; index3 < over_write_data.ConceptCards.Length; ++index3)
        {
          if (over_write_data.ConceptCards[index3] != null && conceptCardDataArray.Length > index3)
          {
            ConceptCardData conceptCardByUniqueId = MonoSingleton<GameManager>.Instance.Player.FindConceptCardByUniqueID((long) over_write_data.ConceptCards[index3].UniqueID);
            if (conceptCardByUniqueId != null)
            {
              conceptCardData = conceptCardByUniqueId.Clone();
            }
            else
            {
              string str = over_write_data.ConceptCards[index3].Param == null ? string.Empty : over_write_data.ConceptCards[index3].Param.iname;
              DebugUtility.LogError(string.Format("真理念装データが見つからなかった。⇒ iid:{0}({1})", (object) over_write_data.ConceptCards[index3].UniqueID, (object) str));
            }
            conceptCardDataArray[index3] = conceptCardData;
          }
        }
      }
      for (int index4 = 0; index4 < conceptCardDataArray.Length; ++index4)
        this.SetConceptCardByIndex(index4, conceptCardDataArray[index4]);
      this.mLeaderSkillIsConceptCard = over_write_data.LeaderSkillIsConceptCard;
      if (UnitOverWriteUtility.IsSupportPartyType(ow_party_type))
        this.mLeaderSkillIsConceptCard = false;
      this.CalcStatus();
    }

    public bool IsEquipConceptCard(long iid)
    {
      if (this.mConceptCards == null || iid == 0L)
        return false;
      for (int index = 0; index < this.mConceptCards.Length; ++index)
      {
        if (this.mConceptCards[index] != null && (long) this.mConceptCards[index].UniqueID == iid)
          return true;
      }
      return false;
    }

    public int FindIndexOfConceptCard(long iid)
    {
      if (this.mConceptCards == null || iid == 0L)
        return -1;
      for (int indexOfConceptCard = 0; indexOfConceptCard < this.mConceptCards.Length; ++indexOfConceptCard)
      {
        if (this.mConceptCards[indexOfConceptCard] != null && (long) this.mConceptCards[indexOfConceptCard].UniqueID == iid)
          return indexOfConceptCard;
      }
      return -1;
    }

    public ConceptCardData GetConceptCardByIndex(int index)
    {
      if (this.mConceptCards == null)
        return (ConceptCardData) null;
      if (index < 0)
        return (ConceptCardData) null;
      return this.mConceptCards.Length <= index ? (ConceptCardData) null : this.mConceptCards[index];
    }

    public void SetConceptCardByIndex(int index, ConceptCardData conceptCard)
    {
      if (this.mConceptCards == null || index < 0 || this.mConceptCards.Length <= index)
        return;
      if (this.mConceptCards[index] != null)
        this.mConceptCards[index].ResetSlotIndex();
      conceptCard?.SetSlotIndex(index);
      this.mConceptCards[index] = conceptCard;
    }

    public void DetachConceptCard(long iid)
    {
      if (this.mConceptCards == null || iid == 0L)
        return;
      for (int index = 0; index < this.mConceptCards.Length; ++index)
      {
        if (this.mConceptCards[index] != null && (long) this.mConceptCards[index].UniqueID == iid)
        {
          this.SetConceptCardByIndex(index, (ConceptCardData) null);
          break;
        }
      }
    }

    public void SetConceptCardMainSlot(ConceptCardData conceptCard)
    {
      this.SetConceptCardByIndex(0, conceptCard);
    }

    public void DetachConceptCardMainSlot()
    {
      this.SetConceptCardByIndex(0, (ConceptCardData) null);
    }

    public bool IsUnlockConceptCardSlot(int slotIndex)
    {
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instanceDirect, (UnityEngine.Object) null) || instanceDirect.MasterParam == null || instanceDirect.MasterParam.FixParam == null || slotIndex < 0 || 2 <= slotIndex)
        return false;
      FixParam fixParam = instanceDirect.MasterParam.FixParam;
      if (ConceptCardData.IsMainSlot(slotIndex))
        return true;
      if (fixParam.ConceptcardSlot2UnlockTobira == TobiraParam.Category.START)
        return false;
      SRPG.TobiraData tobiraData = this.GetTobiraData(fixParam.ConceptcardSlot2UnlockTobira);
      return tobiraData != null && tobiraData.IsMaxLv;
    }

    public static UnitData CreateUnitDataForDisplay(string iname)
    {
      if (string.IsNullOrEmpty(iname))
      {
        DebugUtility.LogError("inameが指定されていません.");
        return (UnitData) null;
      }
      UnitParam unitParam = MonoSingleton<GameManager>.Instance.GetUnitParam(iname);
      if (unitParam != null)
        return UnitData.CreateUnitDataForDisplay(unitParam);
      DebugUtility.LogError("iname:" + iname + "のUnitParamが存在しません.");
      return (UnitData) null;
    }

    public static UnitData CreateUnitDataForDisplay(UnitParam uparam)
    {
      if (uparam == null)
      {
        DebugUtility.LogError("UnitParamが指定されていません.");
        return (UnitData) null;
      }
      UnitData unitDataForDisplay = new UnitData();
      Json_Unit json = new Json_Unit()
      {
        iid = 1,
        iname = uparam.iname,
        exp = 0,
        lv = 1,
        plus = 0,
        rare = 0,
        select = new Json_UnitSelectable()
      };
      json.select.job = 0L;
      json.jobs = (Json_Job[]) null;
      json.abil = (Json_MasterAbility) null;
      if (uparam.jobsets != null && uparam.jobsets.Length > 0)
      {
        List<Json_Job> jsonJobList = new List<Json_Job>(uparam.jobsets.Length);
        int num = 1;
        for (int index = 0; index < uparam.jobsets.Length; ++index)
        {
          JobSetParam jobSetParam = MonoSingleton<GameManager>.Instance.GetJobSetParam(uparam.jobsets[index]);
          if (jobSetParam != null)
            jsonJobList.Add(new Json_Job()
            {
              iid = (long) num++,
              iname = jobSetParam.job,
              rank = 0,
              equips = (Json_Equip[]) null,
              abils = (Json_Ability[]) null
            });
        }
        json.jobs = jsonJobList.ToArray();
      }
      unitDataForDisplay.Deserialize(json);
      unitDataForDisplay.SetUniqueID(1L);
      unitDataForDisplay.JobRankUp(0);
      return unitDataForDisplay;
    }

    public bool IsSameUnit(string unit_iname)
    {
      if (string.IsNullOrEmpty(unit_iname))
        return false;
      UnitSameGroupParam unitSameGroup = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitSameGroup(unit_iname);
      return unitSameGroup != null && unitSameGroup.IsInGroup(unit_iname);
    }

    [Flags]
    public enum TemporaryFlags
    {
      TemporaryUnitData = 1,
      AllowJobChange = 2,
    }

    public class TobiraConditioError
    {
      public UnitData.TobiraConditioError.ErrorType Type;

      public TobiraConditioError(UnitData.TobiraConditioError.ErrorType type) => this.Type = type;

      public enum ErrorType
      {
        None,
        Quest,
        UnitNone,
        UnitLevel,
        UnitAwakeLevel,
        UnitJobLevel,
        UnitTobiraLevel,
        UnitOpenAllJobs,
      }
    }

    public class Json_PlaybackVoiceData
    {
      public int playback_voice_unlocked;
      public List<string> cue_names = new List<string>();
    }

    public class UnitVoiceCueInfo
    {
      public CriAtomEx.CueInfo cueInfo;
      public string voice_name;
      public bool has_conditions;
      public bool is_locked;
      public bool is_new;
      public string unlock_conditions_text;
      public int number;
    }

    public class UnitPlaybackVoiceData
    {
      private UnitData unit;
      private MySound.Voice voice;
      private List<UnitData.UnitVoiceCueInfo> voice_cue_list;

      public MySound.Voice Voice => this.voice;

      public List<UnitData.UnitVoiceCueInfo> VoiceCueList => this.voice_cue_list;

      public void Init(
        UnitData _unit,
        MySound.Voice _voice,
        CriAtomEx.CueInfo[] _cueInfo,
        string _voice_name)
      {
        this.unit = _unit;
        this.voice = _voice;
        this.voice_cue_list = new List<UnitData.UnitVoiceCueInfo>();
        List<UnitData.UnitVoiceCueInfo> collection1 = new List<UnitData.UnitVoiceCueInfo>();
        List<UnitData.UnitVoiceCueInfo> collection2 = new List<UnitData.UnitVoiceCueInfo>();
        List<UnitData.UnitVoiceCueInfo> collection3 = new List<UnitData.UnitVoiceCueInfo>();
        List<UnitData.UnitVoiceCueInfo> collection4 = new List<UnitData.UnitVoiceCueInfo>();
        try
        {
          UnitData.Json_PlaybackVoiceData playbackVoiceData = JsonUtility.FromJson<UnitData.Json_PlaybackVoiceData>(PlayerPrefsUtility.GetString(this.unit.UniqueID.ToString(), string.Empty));
          string empty1 = string.Empty;
          string empty2 = string.Empty;
          for (int index = 0; index < _cueInfo.Length; ++index)
          {
            string[] strArray = _cueInfo[index].name.Split('_');
            if (strArray.Length >= 2)
            {
              string str1 = strArray[strArray.Length - 2];
              string s = strArray[strArray.Length - 1];
              bool success = false;
              string _tmp_name = str1 + "_" + s;
              string str2 = LocalizedText.Get("playback_list." + _tmp_name.ToUpper(), ref success);
              if (success)
              {
                UnitData.UnitVoiceCueInfo _info = new UnitData.UnitVoiceCueInfo();
                _info.cueInfo = _cueInfo[index];
                _info.voice_name = str2;
                _info.is_locked = false;
                _info.is_new = false;
                _info.number = int.Parse(s);
                this.SetConditions(_tmp_name, ref _info);
                if (_info.has_conditions)
                {
                  _info.is_locked = !this.CheckConditions(this.unit, _tmp_name);
                  if (!_info.is_locked && (playbackVoiceData == null || playbackVoiceData.cue_names.Count <= 0 || !playbackVoiceData.cue_names.Contains(_cueInfo[index].name)))
                    _info.is_new = true;
                  if (playbackVoiceData != null && playbackVoiceData.cue_names.Contains(_cueInfo[index].name))
                    _info.is_locked = false;
                }
                switch (str1)
                {
                  case "chara":
                    collection1.Add(_info);
                    continue;
                  case "voice":
                    collection1.Add(_info);
                    continue;
                  case "battle":
                    collection2.Add(_info);
                    continue;
                  case "sys":
                    collection3.Add(_info);
                    continue;
                  default:
                    collection4.Add(_info);
                    continue;
                }
              }
            }
          }
          collection1.Sort((Comparison<UnitData.UnitVoiceCueInfo>) ((a, b) => a.number - b.number));
          collection2.Sort((Comparison<UnitData.UnitVoiceCueInfo>) ((a, b) => a.number - b.number));
          collection3.Sort((Comparison<UnitData.UnitVoiceCueInfo>) ((a, b) => a.number - b.number));
          collection4.Sort((Comparison<UnitData.UnitVoiceCueInfo>) ((a, b) => a.number - b.number));
          this.voice_cue_list.AddRange((IEnumerable<UnitData.UnitVoiceCueInfo>) collection1);
          this.voice_cue_list.AddRange((IEnumerable<UnitData.UnitVoiceCueInfo>) collection2);
          this.voice_cue_list.AddRange((IEnumerable<UnitData.UnitVoiceCueInfo>) collection3);
          this.voice_cue_list.AddRange((IEnumerable<UnitData.UnitVoiceCueInfo>) collection4);
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
        }
      }

      public void Cleanup()
      {
        if (this.voice == null)
          return;
        this.voice.StopAll();
        this.voice.Cleanup();
        this.voice = (MySound.Voice) null;
      }

      private void SetConditions(string _tmp_name, ref UnitData.UnitVoiceCueInfo _info)
      {
        _info.has_conditions = false;
        foreach (string key in UnitData.CONDITIONS_TARGET_NAMES.Keys)
        {
          if (this.ContainsArray(UnitData.CONDITIONS_TARGET_NAMES[key], _tmp_name))
          {
            _info.has_conditions = true;
            _info.unlock_conditions_text = this.GetUnlockConditionsText(key);
            break;
          }
        }
      }

      private string GetUnlockConditionsText(string _dictionary_key)
      {
        switch (_dictionary_key)
        {
          case "UNIT_LEVEL85":
            return string.Format(LocalizedText.Get("sys.UNLOCK_CONDITIONS_PLAYBACK_UNITVOICE_FORMAT_UNIT_LV"), (object) 85);
          case "UNIT_LEVEL75":
            return string.Format(LocalizedText.Get("sys.UNLOCK_CONDITIONS_PLAYBACK_UNITVOICE_FORMAT_UNIT_LV"), (object) 75);
          case "UNIT_LEVEL65":
            return string.Format(LocalizedText.Get("sys.UNLOCK_CONDITIONS_PLAYBACK_UNITVOICE_FORMAT_UNIT_LV"), (object) 65);
          case "UNIT_JOB_MASTER1":
            return string.Format(LocalizedText.Get("sys.UNLOCK_CONDITIONS_PLAYBACK_UNITVOICE_FORMAT_JOB_MASTER"));
          default:
            return string.Empty;
        }
      }

      private bool CheckConditions(UnitData _unit, string _tmp_name)
      {
        return _unit.CheckUnlockPlaybackVoice() && this.CheckConditionsUnitLevel(_tmp_name, 85) && this.CheckConditionsUnitLevel(_tmp_name, 75) && this.CheckConditionsUnitLevel(_tmp_name, 65) && this.CheckConditionsJobMaster1(_tmp_name);
      }

      private bool CheckConditionsUnitLevel(string _tmp_name, int _level)
      {
        return !this.ContainsArray(UnitData.CONDITIONS_TARGET_NAMES["UNIT_LEVEL" + _level.ToString()], _tmp_name) || this.unit.Lv >= _level;
      }

      private bool CheckConditionsJobMaster1(string _tmp_name)
      {
        if (!this.ContainsArray(UnitData.CONDITIONS_TARGET_NAMES["UNIT_JOB_MASTER1"], _tmp_name))
          return true;
        for (int index = 0; index < this.unit.Jobs.Length; ++index)
        {
          if (this.unit.Jobs[index].CheckJobMaster())
            return true;
        }
        return false;
      }

      private bool ContainsArray(string[] _array, string _target)
      {
        for (int index = 0; index < _array.Length; ++index)
        {
          if (_array[index] == _target)
            return true;
        }
        return false;
      }
    }

    public class CharacterQuestUnlockProgress
    {
      public int Level;
      public int Rarity;
      public QuestParam CondQuest;
      public bool ClearUnlocksCond;
    }

    public class CharacterQuestParam
    {
      public int EpisodeNum;
      public string EpisodeTitle;
      public bool IsNew;
      public bool IsAvailable;
      public QuestParam Param;
    }
  }
}
