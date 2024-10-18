// Decompiled with JetBrains decompiler
// Type: SRPG.JobData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using MessagePack;
using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  public class JobData
  {
    public const int MAX_RANKUP_EQUIPS = 6;
    public const int MAX_LARNING_ABILITY = 8;
    public const int MAX_ABILITY_SLOT = 5;
    public const int MAX_ARTIFACT_SLOT = 3;
    public const int FIXED_ABILITY_SLOT_INDEX = 0;
    private UnitData mOwner;
    private long mUniqueID;
    private JobParam mJobParam;
    private OInt mRank = (OInt) 0;
    private SkillData mNormalAttackSkill = new SkillData();
    private SkillData mJobMaster;
    private EquipData[] mEquips = new EquipData[6];
    private List<AbilityData> mLearnAbilitys = new List<AbilityData>();
    public static EAbilitySlot[] ABILITY_SLOT_TYPES = new EAbilitySlot[5]
    {
      EAbilitySlot.Action,
      EAbilitySlot.Action,
      EAbilitySlot.Reaction,
      EAbilitySlot.Support,
      EAbilitySlot.Support
    };
    private long[] mAbilitySlots = new long[5];
    public static ArtifactTypes[] ARTIFACT_SLOT_TYPES = new ArtifactTypes[3]
    {
      ArtifactTypes.Arms,
      ArtifactTypes.Armor,
      ArtifactTypes.Accessory
    };
    private long[] mArtifacts = new long[3];
    private ArtifactData[] mArtifactDatas = new ArtifactData[3];
    private string mSelectSkin;
    private ArtifactData mSelectSkinData;

    public JobData()
    {
      for (int index = 0; index < this.mEquips.Length; ++index)
        this.mEquips[index] = new EquipData();
    }

    public static int GetArtifactSlotIndex(ArtifactTypes type)
    {
      for (int artifactSlotIndex = 0; artifactSlotIndex < JobData.ARTIFACT_SLOT_TYPES.Length; ++artifactSlotIndex)
      {
        if (type == JobData.ARTIFACT_SLOT_TYPES[artifactSlotIndex])
          return artifactSlotIndex;
      }
      return -1;
    }

    public UnitData Owner => this.mOwner;

    public JobParam Param => this.mJobParam;

    public long UniqueID
    {
      get => this.mUniqueID;
      set => this.mUniqueID = value;
    }

    public string JobID => this.Param != null ? this.Param.iname : (string) null;

    public string Name => this.Param != null ? this.Param.name : (string) null;

    public int Rank => (int) this.mRank;

    public JobTypes JobType => this.Param != null ? this.Param.type : JobTypes.Attacker;

    public RoleTypes RoleType => this.Param != null ? this.Param.role : RoleTypes.Zenei;

    public string JobResourceID => this.Param != null ? this.Param.model : (string) null;

    public EquipData[] Equips => this.mEquips;

    public List<AbilityData> LearnAbilitys => this.mLearnAbilitys;

    public long[] AbilitySlots => this.mAbilitySlots;

    public long[] Artifacts => this.mArtifacts;

    public ArtifactData[] ArtifactDatas => this.mArtifactDatas;

    public string SelectedSkin
    {
      get => this.mSelectSkin;
      set => this.mSelectSkin = value;
    }

    public ArtifactData SelectSkinData
    {
      get => this.mSelectSkinData;
      set => this.mSelectSkinData = value;
    }

    public bool IsActivated => this.Rank > 0;

    public SkillData JobMaster => this.CheckJobMaster() ? this.mJobMaster : (SkillData) null;

    public void Deserialize(UnitData owner, Json_Job json)
    {
      this.mJobParam = json != null ? MonoSingleton<GameManager>.GetInstanceDirect().GetJobParam(json.iname) : throw new InvalidJSONException();
      this.mUniqueID = json.iid;
      this.mRank = (OInt) json.rank;
      this.mOwner = owner;
      this.mSelectSkin = json.cur_skin;
      for (int index = 0; index < this.mEquips.Length; ++index)
        this.mEquips[index].Setup(this.mJobParam.GetRankupItemID((int) this.mRank, index));
      if (json.equips != null)
      {
        for (int index = 0; index < json.equips.Length; ++index)
          this.mEquips[index].Equip(json.equips[index]);
      }
      if (!string.IsNullOrEmpty(this.Param.atkskill[0]))
        this.mNormalAttackSkill.Setup(this.Param.atkskill[0], 1);
      else
        this.mNormalAttackSkill.Setup(this.Param.atkskill[(int) owner.UnitParam.element], 1);
      if (!string.IsNullOrEmpty(this.Param.master) && MonoSingleton<GameManager>.Instance.MasterParam.FixParam.IsJobMaster)
      {
        if (this.mJobMaster == null)
          this.mJobMaster = new SkillData();
        this.mJobMaster.Setup(this.Param.master, 1);
      }
      if (json.abils != null)
      {
        Array.Sort<Json_Ability>(json.abils, (Comparison<Json_Ability>) ((src, dsc) => (int) (src.iid - dsc.iid)));
        for (int index = 0; index < json.abils.Length; ++index)
        {
          AbilityData abilityData = new AbilityData();
          string iname = json.abils[index].iname;
          long iid = json.abils[index].iid;
          int exp = json.abils[index].exp;
          abilityData.Setup(this.mOwner, iid, iname, exp);
          this.mLearnAbilitys.Add(abilityData);
        }
      }
      Array.Clear((Array) this.mAbilitySlots, 0, this.mAbilitySlots.Length);
      if (json.select != null && json.select.abils != null)
      {
        for (int index = 0; index < json.select.abils.Length && index < this.mAbilitySlots.Length; ++index)
          this.mAbilitySlots[index] = json.select.abils[index];
      }
      for (int index = 0; index < this.mArtifactDatas.Length; ++index)
        this.mArtifactDatas[index] = (ArtifactData) null;
      Array.Clear((Array) this.mArtifacts, 0, this.mArtifacts.Length);
      if (json.select != null && json.select.artifacts != null)
      {
        for (int index = 0; index < json.select.artifacts.Length && index < this.mArtifacts.Length; ++index)
          this.mArtifacts[index] = json.select.artifacts[index];
      }
      if (json.artis != null)
      {
        for (int i = 0; i < json.artis.Length; ++i)
        {
          if (json.artis[i] != null && !string.IsNullOrEmpty(json.artis[i].iname))
          {
            int index = Array.IndexOf<long>(this.mArtifacts, json.artis[i].iid);
            if (index >= 0)
            {
              if (json.select.inspiration_skills != null && json.select.inspiration_skills.Length > 0)
              {
                Json_InspirationSkillExt[] all = Array.FindAll<Json_InspirationSkillExt>(json.select.inspiration_skills, (Predicate<Json_InspirationSkillExt>) (inspskil => inspskil.artifact_iid == json.artis[i].iid));
                if (all != null && all.Length > 0)
                  json.artis[i].inspiration_skills = (Json_InspirationSkill[]) all;
              }
              ArtifactData artifactData = new ArtifactData();
              artifactData.Deserialize(json.artis[i]);
              this.mArtifactDatas[index] = artifactData;
            }
          }
        }
      }
      if (string.IsNullOrEmpty(json.cur_skin))
        return;
      ArtifactData artifactData1 = new ArtifactData();
      artifactData1.Deserialize(new Json_Artifact()
      {
        iname = json.cur_skin
      });
      this.mSelectSkinData = artifactData1;
    }

    public void UnlockSkillAll()
    {
      for (int index = 0; index < this.LearnAbilitys.Count; ++index)
        this.LearnAbilitys[index].UpdateLearningsSkill(false);
    }

    public SkillData GetAttackSkill() => this.mNormalAttackSkill;

    public int GetJobRankAvoidRate() => this.GetJobRankAvoidRate(this.Rank);

    public int GetJobRankAvoidRate(int rank)
    {
      return this.Param != null ? this.Param.GetJobRankAvoidRate(rank) : 0;
    }

    public int GetJobRankInitJewelRate() => this.GetJobRankInitJewelRate(this.Rank);

    public int GetJobRankInitJewelRate(int rank)
    {
      return this.Param != null ? this.Param.GetJobRankInitJewelRate(rank) : 0;
    }

    public StatusParam GetJobRankStatus() => this.GetJobRankStatus(this.Rank);

    public StatusParam GetJobRankStatus(int rank)
    {
      return this.Param != null ? this.Param.GetJobRankStatus(rank) : (StatusParam) null;
    }

    public BaseStatus GetJobTransfarStatus(EElement element)
    {
      return this.GetJobTransfarStatus(this.Rank, element);
    }

    public BaseStatus GetJobTransfarStatus(int rank, EElement element)
    {
      return this.Param != null ? this.Param.GetJobTransfarStatus(rank, element) : (BaseStatus) null;
    }

    public OString[] GetLearningAbilitys(int rank)
    {
      return this.Param != null ? this.Param.GetLearningAbilitys(rank) : (OString[]) null;
    }

    public int GetJobChangeCost(int rank)
    {
      return this.Param != null ? this.Param.GetJobChangeCost(rank) : 0;
    }

    public string[] GetJobChangeItems(int rank)
    {
      return this.Param != null ? this.Param.GetJobChangeItems(rank) : (string[]) null;
    }

    public int[] GetJobChangeItemNums(int rank)
    {
      return this.Param != null ? this.Param.GetJobChangeItemNums(rank) : (int[]) null;
    }

    public int GetRankupCost(int rank) => this.Param != null ? this.Param.GetRankupCost(rank) : 0;

    public string[] GetRankupItems(int rank)
    {
      return this.Param != null ? this.Param.GetRankupItems(rank) : (string[]) null;
    }

    public string GetRankupItemID(int rank, int index)
    {
      return this.Param != null ? this.Param.GetRankupItemID(rank, index) : (string) null;
    }

    public int FindEquipSlotByItemID(string iname)
    {
      int equipSlotByItemId = -1;
      for (int index = 0; index < 6; ++index)
      {
        string rankupItemId = this.GetRankupItemID(this.Rank, index);
        if (!string.IsNullOrEmpty(rankupItemId) && !(rankupItemId != iname))
        {
          equipSlotByItemId = index;
          break;
        }
      }
      return equipSlotByItemId;
    }

    public bool CheckEnableEquipSlot(int index)
    {
      return index >= 0 && index < this.mEquips.Length && this.mEquips[index] != null && !this.mEquips[index].IsEquiped() && (this.Owner == null || this.GetEnableEquipUnitLevel(index) <= this.Owner.Lv);
    }

    public bool Equip(int index)
    {
      if (!this.CheckEnableEquipSlot(index))
        return false;
      string rankupItemId = this.GetRankupItemID(this.Rank, index);
      this.mEquips[index].Setup(rankupItemId);
      this.mEquips[index].Equip((Json_Equip) null);
      return true;
    }

    public void JobRankUp()
    {
      ++this.mRank;
      for (int index = 0; index < this.mEquips.Length; ++index)
        this.mEquips[index].Setup(this.GetRankupItemID(this.Rank, index));
      if ((int) this.mRank == 1 && !string.IsNullOrEmpty(this.mJobParam.fixed_ability) && this.mLearnAbilitys.Find((Predicate<AbilityData>) (p => p.AbilityID == this.mJobParam.fixed_ability)) == null)
      {
        AbilityData ability = new AbilityData();
        List<AbilityData> learnedAbilities = this.mOwner.GetAllLearnedAbilities();
        string fixedAbility = this.mJobParam.fixed_ability;
        long val1 = 0;
        int exp = 0;
        for (int index = 0; index < learnedAbilities.Count; ++index)
          val1 = Math.Max(val1, learnedAbilities[index].UniqueID);
        long iid = val1 + 1L;
        try
        {
          ability.Setup(this.mOwner, iid, fixedAbility, exp);
          this.mLearnAbilitys.Add(ability);
          this.SetAbilitySlot(0, ability);
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
        }
      }
      OString[] learningAbilitys = this.GetLearningAbilitys((int) this.mRank);
      if (learningAbilitys == null)
        return;
      for (int index1 = 0; index1 < learningAbilitys.Length; ++index1)
      {
        string abilityID = (string) learningAbilitys[index1];
        if (!string.IsNullOrEmpty(abilityID) && this.mLearnAbilitys.Find((Predicate<AbilityData>) (p => p.AbilityID == abilityID)) == null)
        {
          AbilityData abilityData = new AbilityData();
          string iname = abilityID;
          List<AbilityData> learnedAbilities = this.mOwner.GetAllLearnedAbilities();
          long val1 = 0;
          int exp = 0;
          for (int index2 = 0; index2 < learnedAbilities.Count; ++index2)
            val1 = Math.Max(val1, learnedAbilities[index2].UniqueID);
          long iid = val1 + 1L;
          try
          {
            abilityData.Setup(this.mOwner, iid, iname, exp);
            this.mLearnAbilitys.Add(abilityData);
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
          }
        }
      }
    }

    public int GetJobRankCap(UnitData self) => JobParam.GetJobRankCap(self.Rarity);

    public bool CheckJobRankUp(UnitData self, bool canCreate = false, bool useCommon = true)
    {
      if (this.Param == null || this.Rank >= this.GetJobRankCap(self))
        return false;
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      NeedEquipItemList needEquipItemList = new NeedEquipItemList();
      if (canCreate)
      {
        if (!player.CheckEnableCreateEquipItemAll(self, this.mEquips, !useCommon ? (NeedEquipItemList) null : needEquipItemList) && !needEquipItemList.IsEnoughCommon())
          return false;
      }
      else
      {
        for (int index = 0; index < 6; ++index)
        {
          if (this.mEquips[index] == null || !this.mEquips[index].IsEquiped())
            return false;
        }
      }
      return true;
    }

    public bool[] CheckJobEquip(UnitData self)
    {
      bool[] equipFlags = new bool[6];
      if (this.Param == null)
        return equipFlags;
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      NeedEquipItemList item_list = new NeedEquipItemList();
      player.GetEnableCreateEquipItemAll(self, this.mEquips, ref equipFlags, item_list);
      return equipFlags;
    }

    public void GetAllEquipOnly(
      ref int cost,
      ref Dictionary<string, int> equips,
      ref Dictionary<string, int> consumes,
      ref int target_rank,
      ref bool can_jobmaster,
      ref bool can_jobmax,
      NeedEquipItemList item_list = null,
      bool all = false)
    {
      bool[] equipFlags = new bool[6];
      MonoSingleton<GameManager>.Instance.Player.GetEnableCreateEquipItemAll(this.Owner, this.Equips, ref equipFlags, ref consumes, ref cost, item_list);
    }

    public bool CanAllEquip(
      ref int cost,
      ref Dictionary<string, int> equips,
      ref Dictionary<string, int> consumes,
      ref int target_rank,
      ref bool can_jobmaster,
      ref bool can_jobmax,
      NeedEquipItemList item_list = null,
      bool all = false)
    {
      return all ? MonoSingleton<GameManager>.Instance.Player.CheckEnable2(this.Owner, this.Equips, ref consumes, ref cost, ref target_rank, ref can_jobmaster, ref can_jobmax) : MonoSingleton<GameManager>.Instance.Player.CheckEnableCreateEquipItemAll(this.Owner, this.Equips, ref consumes, ref cost, item_list);
    }

    public bool CanAllEquip(
      ref int cost,
      ref Dictionary<string, int> equips,
      ref Dictionary<string, int> consumes,
      NeedEquipItemList item_list = null)
    {
      return MonoSingleton<GameManager>.Instance.Player.CheckEnableCreateEquipItemAll(this.Owner, this.Equips, ref consumes, ref cost, item_list);
    }

    public int GetEnableEquipUnitLevel(int slot)
    {
      return this.mEquips[slot] == null || this.mEquips[slot].ItemParam == null ? 0 : this.mEquips[slot].ItemParam.equipLv;
    }

    public void SetAbilitySlot(int slot, AbilityData ability)
    {
      DebugUtility.Assert(slot >= 0 && slot < this.mAbilitySlots.Length, "EquipAbility Out of Length");
      long num = 0;
      if (ability != null)
      {
        AbilityParam abilityParam = ability.Param;
        if (abilityParam.slot != JobData.ABILITY_SLOT_TYPES[slot])
        {
          DebugUtility.LogError("指定スロットに対応するアビリティではない");
          return;
        }
        if (slot != 0 && abilityParam.is_fixed)
        {
          DebugUtility.LogError("指定スロットには固定アビリティは装備不可能");
          return;
        }
        num = ability.UniqueID;
      }
      this.mAbilitySlots[slot] = num;
    }

    public bool SetEquipArtifact(int slot, ArtifactData artifact, bool is_force = false)
    {
      DebugUtility.Assert(slot >= 0 && slot < this.mArtifacts.Length, "SetArtifact Out of Length");
      long num = 0;
      if (artifact != null)
      {
        if (!is_force && !this.CheckEquipArtifact(slot, artifact))
          return false;
        num = (long) artifact.UniqueID;
      }
      this.mArtifacts[slot] = num;
      this.mArtifactDatas[slot] = artifact;
      return true;
    }

    public bool CheckEquipArtifact(int slot, ArtifactData artifact)
    {
      if (artifact == null || this.mArtifacts == null || slot < 0 || slot >= this.mArtifacts.Length || this.Owner == null)
        return false;
      int jobIndex = Array.IndexOf<JobData>(this.Owner.Jobs, this);
      if (jobIndex < 0 || !artifact.CheckEnableEquip(this.Owner, jobIndex))
        return false;
      ArtifactParam artifactParam = artifact.ArtifactParam;
      if (artifactParam.type == ArtifactTypes.Accessory || artifactParam.type == JobData.ARTIFACT_SLOT_TYPES[slot])
        return true;
      DebugUtility.LogError("ArtifactSlot mismatch");
      return false;
    }

    public ArtifactData GetSelectedSkinData()
    {
      if (this.mSelectSkinData != null && this.mSelectSkin == this.mSelectSkinData.ArtifactParam.iname)
        return this.mSelectSkinData;
      ArtifactParam artifactParam = Array.Find<ArtifactParam>(MonoSingleton<GameManager>.Instance.MasterParam.Artifacts.ToArray(), (Predicate<ArtifactParam>) (a => a.iname == this.mSelectSkin));
      if (artifactParam == null)
        return (ArtifactData) null;
      ArtifactData artifactData = new ArtifactData();
      artifactData.Deserialize(new Json_Artifact()
      {
        iname = artifactParam.iname
      });
      this.mSelectSkinData = artifactData;
      return this.mSelectSkinData;
    }

    public bool CheckJobMaster()
    {
      if (!MonoSingleton<GameManager>.Instance.MasterParam.FixParam.IsJobMaster || this.Equips == null || this.Rank < JobParam.MAX_JOB_RANK)
        return false;
      bool flag = true;
      for (int index = 0; index < this.Equips.Length; ++index)
      {
        EquipData equip = this.Equips[index];
        if (equip == null || !equip.IsValid() || !equip.IsEquiped())
        {
          flag = false;
          break;
        }
      }
      return flag;
    }

    public void AddJobBaseStatus(
      UnitData self,
      ref BaseStatus addStatus,
      ref BaseStatus scaleStatus)
    {
      if (this.Param == null || string.IsNullOrEmpty(this.Param.buff) || self == null)
        return;
      BuffEffect buffEffect = (BuffEffect) null;
      BuffEffectParam buffEffectParam = MonoSingleton<GameManager>.Instance.MasterParam.GetBuffEffectParam(this.Param.buff);
      if (buffEffectParam != null)
        buffEffect = BuffEffect.CreateBuffEffect(buffEffectParam, this.Rank, this.GetJobRankCap(self));
      if (buffEffect == null)
        return;
      BaseStatus total_add = new BaseStatus();
      BaseStatus total_scale = new BaseStatus();
      buffEffect.GetBaseStatus(ref total_add, ref total_scale);
      if (addStatus != null)
        addStatus.Add(total_add);
      if (scaleStatus == null)
        return;
      scaleStatus.Add(total_scale);
    }
  }
}
