// Decompiled with JetBrains decompiler
// Type: SRPG.ArtifactData
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
  public class ArtifactData
  {
    private OLong mUniqueID = (OLong) 0L;
    private ArtifactParam mArtifactParam;
    private RarityParam mRarityParam;
    private OInt mRarity = (OInt) 0;
    private OInt mLv = (OInt) 1;
    private OInt mExp;
    private bool mFavorite;
    private SkillData mEquipSkill;
    private SkillData mBattleEffectSkill;
    private List<AbilityData> mLearningAbilities;
    private OInt mInspirationSkillSlotNum;
    private List<InspirationSkillData> mInspirationSkillList;
    private static BaseStatus WorkScaleStatus = new BaseStatus();

    public int Exp => (int) this.mExp;

    public OLong UniqueID => this.mUniqueID;

    public ArtifactParam ArtifactParam => this.mArtifactParam;

    public RarityParam RarityParam => this.mRarityParam;

    public OInt Rarity => this.mRarity;

    public OInt RarityCap => (OInt) this.mArtifactParam.raremax;

    public OInt Lv => this.mLv;

    public OInt LvCap => (OInt) this.GetLevelCap();

    public ItemParam Kakera
    {
      get => MonoSingleton<GameManager>.Instance.GetItemParam(this.mArtifactParam.kakera);
    }

    public ItemData KakeraData
    {
      get => MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemParam(this.Kakera);
    }

    public ItemParam RarityKakera
    {
      get
      {
        FixParam fixParam = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
        if (fixParam.ArtifactRarePiece == null)
          return (ItemParam) null;
        return fixParam.ArtifactRarePiece.Length <= (int) this.Rarity ? (ItemParam) null : MonoSingleton<GameManager>.Instance.GetItemParam((string) fixParam.ArtifactRarePiece[(int) this.Rarity]);
      }
    }

    public ItemData RarityKakeraData
    {
      get => MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemParam(this.RarityKakera);
    }

    public ItemParam CommonKakera
    {
      get
      {
        return MonoSingleton<GameManager>.Instance.GetItemParam((string) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.ArtifactCommonPiece);
      }
    }

    public ItemData CommonKakeraData
    {
      get => MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemParam(this.CommonKakera);
    }

    public bool IsFavorite
    {
      get => this.mFavorite;
      set => this.mFavorite = value;
    }

    public SkillData EquipSkill => this.mEquipSkill;

    public SkillData BattleEffectSkill => this.mBattleEffectSkill;

    public List<AbilityData> LearningAbilities => this.mLearningAbilities;

    public OInt InspirationSkillSlotNum => this.mInspirationSkillSlotNum;

    public List<InspirationSkillData> InspirationSkillList => this.mInspirationSkillList;

    public void Deserialize(Json_Artifact json)
    {
      if (json == null || string.IsNullOrEmpty(json.iname))
      {
        this.Reset();
      }
      else
      {
        GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
        this.mArtifactParam = instanceDirect.MasterParam.GetArtifactParam(json.iname);
        this.mUniqueID = (OLong) json.iid;
        this.mRarity = (OInt) Math.Min(Math.Max(json.rare, this.mArtifactParam.rareini), this.mArtifactParam.raremax);
        this.mRarityParam = instanceDirect.GetRarityParam((int) this.mRarity);
        this.mExp = (OInt) json.exp;
        this.mLv = (OInt) this.GetLevelFromExp((int) this.mExp);
        this.mFavorite = json.fav != 0;
        this.UpdateEquipEffect();
        this.UpdateLearningAbilities();
        this.mInspirationSkillSlotNum = (OInt) json.inspiration_skill_slots;
        this.mInspirationSkillList = new List<InspirationSkillData>();
        if (json.inspiration_skills == null || json.inspiration_skills.Length <= 0)
          return;
        for (int index = 0; index < json.inspiration_skills.Length; ++index)
        {
          InspirationSkillData inspirationSkillData = new InspirationSkillData();
          if (inspirationSkillData.Deserialize(json.inspiration_skills[index]))
            this.mInspirationSkillList.Add(inspirationSkillData);
        }
      }
    }

    public void DeserializeInspSkill(Json_InspirationSkill json)
    {
      InspirationSkillData skill = new InspirationSkillData();
      if (!skill.Deserialize(json))
        return;
      int index = this.mInspirationSkillList.FindIndex((Predicate<InspirationSkillData>) (inspskill => (long) inspskill.UniqueID == (long) skill.UniqueID));
      if (index < 0)
        this.mInspirationSkillList.Add(skill);
      else
        this.mInspirationSkillList[index] = skill;
    }

    public void RemoveInspSkill(long uniqueId)
    {
      int index = this.mInspirationSkillList.FindIndex((Predicate<InspirationSkillData>) (inspskill => (long) inspskill.UniqueID == uniqueId));
      if (index < 0)
        return;
      this.mInspirationSkillList.RemoveAt(index);
    }

    public void Reset()
    {
      this.mArtifactParam = (ArtifactParam) null;
      this.mRarityParam = (RarityParam) null;
      this.mRarity = (OInt) 0;
      this.mExp = (OInt) 0;
      this.mLv = (OInt) 1;
      this.mUniqueID = (OLong) 0L;
      this.mFavorite = false;
    }

    public ArtifactData Copy()
    {
      ArtifactData artifactData = new ArtifactData();
      Json_Artifact json = new Json_Artifact();
      json.iname = this.mArtifactParam.iname;
      json.iid = (long) this.mUniqueID;
      json.rare = (int) this.mRarity;
      json.exp = (int) this.mExp;
      json.fav = !this.mFavorite ? 0 : 1;
      json.inspiration_skill_slots = (int) this.mInspirationSkillSlotNum;
      json.inspiration_skills = new Json_InspirationSkill[this.mInspirationSkillList.Count];
      for (int index = 0; index < this.mInspirationSkillList.Count; ++index)
        json.inspiration_skills[index] = this.mInspirationSkillList[index].ToJson();
      artifactData.Deserialize(json);
      return artifactData;
    }

    public bool IsValid() => this.mArtifactParam != null;

    private void UpdateLearningAbilities(bool bRarityUp = false)
    {
      if (!this.IsValid() || this.mArtifactParam == null || this.mArtifactParam.abil_inames == null)
        return;
      if (this.mLearningAbilities == null)
        this.mLearningAbilities = new List<AbilityData>(this.mArtifactParam.abil_inames.Length);
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      for (int index = 0; index < this.mArtifactParam.abil_inames.Length; ++index)
      {
        if ((int) this.mLv >= this.mArtifactParam.abil_levels[index] && (int) this.mRarity >= this.mArtifactParam.abil_rareties[index])
        {
          AbilityParam param = instanceDirect.GetAbilityParam(this.mArtifactParam.abil_inames[index]);
          if (param != null)
          {
            AbilityData abilityData1 = this.mLearningAbilities.Find((Predicate<AbilityData>) (p => p.Param == param));
            if (abilityData1 == null)
            {
              AbilityData abilityData2 = new AbilityData();
              abilityData2.Setup((UnitData) null, 0L, param.iname, (int) this.mRarity);
              abilityData2.IsNoneCategory = true;
              abilityData2.IsHideList = this.mArtifactParam.abil_shows[index] == 0;
              this.mLearningAbilities.Add(abilityData2);
            }
            else if (bRarityUp)
              abilityData1.Setup((UnitData) null, 0L, param.iname, (int) this.mRarity);
          }
        }
      }
    }

    public List<AbilityData> GetEnableAbitilies(UnitData unit, int job_index)
    {
      if (this.mLearningAbilities == null || this.mLearningAbilities.Count == 0)
        return (List<AbilityData>) null;
      List<AbilityData> enableAbitilies = new List<AbilityData>(this.mLearningAbilities.Count);
      for (int index = 0; index < this.mLearningAbilities.Count; ++index)
      {
        if (this.mLearningAbilities[index].CheckEnableUseAbility(unit, job_index))
          enableAbitilies.Add(this.mLearningAbilities[index]);
      }
      return enableAbitilies;
    }

    public bool CheckEnableEquip(UnitData unit, int jobIndex = -1)
    {
      return this.IsValid() && this.mArtifactParam.CheckEnableEquip(unit, jobIndex);
    }

    private void UpdateEquipEffect()
    {
      RarityParam rarityParam = MonoSingleton<GameManager>.GetInstanceDirect().GetRarityParam(this.mArtifactParam.raremax);
      int rankcap = 1;
      if (rarityParam != null)
        rankcap = (int) rarityParam.ArtifactLvCap;
      if (this.mArtifactParam.equip_effects != null && (int) this.mRarity >= 0 && (int) this.mRarity < this.mArtifactParam.equip_effects.Length)
      {
        if (!string.IsNullOrEmpty(this.mArtifactParam.equip_effects[(int) this.mRarity]))
        {
          if (this.mEquipSkill == null)
            this.mEquipSkill = new SkillData();
          this.mEquipSkill.Setup(this.mArtifactParam.equip_effects[(int) this.mRarity], (int) this.mLv, rankcap);
        }
      }
      else
        this.mEquipSkill = (SkillData) null;
      if (this.mArtifactParam.attack_effects != null && (int) this.mRarity >= 0 && (int) this.mRarity < this.mArtifactParam.attack_effects.Length)
      {
        if (string.IsNullOrEmpty(this.mArtifactParam.attack_effects[(int) this.mRarity]))
          return;
        if (this.mBattleEffectSkill == null)
          this.mBattleEffectSkill = new SkillData();
        this.mBattleEffectSkill.Setup(this.mArtifactParam.attack_effects[(int) this.mRarity], (int) this.mLv, rankcap);
      }
      else
        this.mBattleEffectSkill = (SkillData) null;
    }

    public int GetLevelCap()
    {
      return this.mRarityParam == null ? 1 : (int) this.mRarityParam.ArtifactLvCap;
    }

    public int GetLevelFromExp(int exp)
    {
      return Math.Min(ArtifactData.StaticCalcLevelFromExp(exp), this.GetLevelCap());
    }

    public static int StaticCalcLevelFromExp(int exp)
    {
      int num1 = 0;
      int num2 = 0;
      OInt[] artifactExpTable = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetArtifactExpTable();
      int num3 = artifactExpTable.Length + 1;
      for (int index = 0; index < num3; ++index)
      {
        num2 += (int) artifactExpTable[index + 1];
        if (num2 <= exp)
          ++num1;
        else
          break;
      }
      return num1 + 1;
    }

    public int GetTotalExpFromLevel(int lv)
    {
      return ArtifactData.StaticCalcExpFromLevel(Math.Min(lv, this.GetLevelCap()));
    }

    public static int StaticCalcExpFromLevel(int lv)
    {
      int num = 0;
      OInt[] artifactExpTable = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetArtifactExpTable();
      for (int index = 0; index < lv; ++index)
        num += (int) artifactExpTable[index];
      return num;
    }

    public int GetNextExpFromLevel(int lv)
    {
      int index = Math.Max(Math.Min(lv, this.GetLevelCap()) - 1, 0);
      OInt[] artifactExpTable = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetArtifactExpTable();
      return artifactExpTable == null ? 0 : (int) artifactExpTable[index];
    }

    public int GetShowExp() => (int) this.mExp - this.GetTotalExpFromLevel((int) this.mLv);

    public int GetNextExp() => this.GetTotalExpFromLevel((int) this.mLv + 1) - (int) this.mExp;

    public void GainExp(ItemData item, int num)
    {
      if (item == null || item.Num < num)
        return;
      ItemParam itemParam = item.Param;
      if (itemParam == null || itemParam.type != EItemType.ExpUpArtifact)
        return;
      this.GainExp(itemParam.value * num);
      item.Used(num);
    }

    public void GainExp(int exp)
    {
      int totalExpFromLevel = this.GetTotalExpFromLevel(this.GetLevelCap());
      int mLv = (int) this.mLv;
      this.mExp = (OInt) Math.Min((int) this.mExp + exp, totalExpFromLevel);
      this.mLv = (OInt) this.GetLevelFromExp((int) this.mExp);
      if ((int) this.mLv == mLv)
        return;
      this.LevelUp();
    }

    public int GetGainExpCap() => this.GetTotalExpFromLevel(this.GetLevelCap());

    public void LevelUp()
    {
      this.UpdateEquipEffect();
      this.UpdateLearningAbilities();
    }

    public int GetKakeraCreateNum()
    {
      return this.mRarityParam == null ? 0 : (int) this.mRarityParam.ArtifactCreatePieceNum;
    }

    public int GetKakeraNeedNum()
    {
      return this.mRarityParam == null ? 0 : (int) this.mRarityParam.ArtifactGouseiPieceNum;
    }

    public int GetKakeraChangeNum()
    {
      return this.mRarityParam == null ? 0 : (int) this.mRarityParam.ArtifactChangePieceNum;
    }

    public bool CheckEnableCreate()
    {
      ItemData kakeraData = this.KakeraData;
      return kakeraData != null && kakeraData.Num > 0 && this.GetKakeraCreateNum() <= kakeraData.Num && MonoSingleton<GameManager>.GetInstanceDirect().Player.Gold >= (int) this.mRarityParam.ArtifactCreateCost;
    }

    public int GetKakeraNumForRarityUp()
    {
      int result = 0;
      Action<ItemData> action = (Action<ItemData>) (itemData =>
      {
        if (itemData == null)
          return;
        result += itemData.Num;
      });
      action(this.KakeraData);
      action(this.RarityKakeraData);
      action(this.CommonKakeraData);
      return result;
    }

    public List<ItemData> GetKakeraDataListForRarityUp()
    {
      List<ItemData> result = new List<ItemData>();
      Action<ItemData> action = (Action<ItemData>) (itemData =>
      {
        if (itemData == null)
          return;
        result.Add(itemData);
      });
      action(this.KakeraData);
      action(this.RarityKakeraData);
      action(this.CommonKakeraData);
      return result;
    }

    public ArtifactData.RarityUpResults CheckEnableRarityUp()
    {
      ArtifactData.RarityUpResults rarityUpResults = ArtifactData.RarityUpResults.Success;
      if ((int) this.mRarity >= (int) this.RarityCap)
        rarityUpResults |= ArtifactData.RarityUpResults.RarityMaxed;
      if ((int) this.mLv < (int) this.LvCap)
        rarityUpResults |= ArtifactData.RarityUpResults.NoLv;
      if (this.GetKakeraNumForRarityUp() < this.GetKakeraNeedNum())
        rarityUpResults |= ArtifactData.RarityUpResults.NoKakera;
      if (MonoSingleton<GameManager>.GetInstanceDirect().Player.Gold < (int) this.mRarityParam.ArtifactRarityUpCost)
        rarityUpResults |= ArtifactData.RarityUpResults.NoGold;
      return rarityUpResults;
    }

    public bool ConsumeKakera(int num)
    {
      int kakeraNumForRarityUp = this.GetKakeraNumForRarityUp();
      if (num < 1 || kakeraNumForRarityUp < num)
        return false;
      ItemData[] itemDataArray = new ItemData[3]
      {
        this.KakeraData,
        this.RarityKakeraData,
        this.CommonKakeraData
      };
      int val2 = num;
      for (int index = 0; index < itemDataArray.Length; ++index)
      {
        if (itemDataArray[index] != null)
        {
          if (val2 >= 1)
          {
            int num1 = Math.Min(itemDataArray[index].Num, val2);
            val2 -= num1;
          }
          else
            break;
        }
      }
      return val2 < 1;
    }

    public void RarityUp()
    {
      if ((int) this.mLv < this.GetLevelCap())
        return;
      int kakeraNumForRarityUp = this.GetKakeraNumForRarityUp();
      int kakeraNeedNum = this.GetKakeraNeedNum();
      if (kakeraNumForRarityUp < kakeraNeedNum)
        return;
      if (!this.ConsumeKakera(kakeraNeedNum))
      {
        DebugUtility.LogWarning("カケラが不足している場合");
      }
      else
      {
        GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
        int artifactRarityUpCost = (int) this.mRarityParam.ArtifactRarityUpCost;
        if (instanceDirect.Player.Gold < artifactRarityUpCost)
          return;
        instanceDirect.Player.GainGold(-artifactRarityUpCost);
        this.mRarity = (OInt) Math.Min(Math.Max((int) ++this.mRarity, this.mArtifactParam.rareini), this.mArtifactParam.raremax);
        this.mRarityParam = instanceDirect.GetRarityParam((int) this.mRarity);
        this.UpdateEquipEffect();
        this.UpdateLearningAbilities(true);
      }
    }

    public int GetSellPrice()
    {
      int sell = this.mArtifactParam.sell;
      int num = 100;
      if (this.mRarityParam != null)
        num = (int) this.mRarityParam.ArtifactCostRate;
      return sell * num / 100;
    }

    public void GetHomePassiveBuffStatus(
      ref BaseStatus fixed_status,
      ref BaseStatus scale_status,
      UnitData user = null,
      int job_index = 0,
      bool bCheckCondition = true)
    {
      fixed_status.Clear();
      scale_status.Clear();
      ArtifactData.WorkScaleStatus.Clear();
      SkillData.GetHomePassiveBuffStatus(this.EquipSkill, EElement.None, ref fixed_status, ref ArtifactData.WorkScaleStatus);
      scale_status.Add(ArtifactData.WorkScaleStatus);
      if (this.mLearningAbilities == null)
        return;
      for (int index1 = 0; index1 < this.mLearningAbilities.Count; ++index1)
      {
        AbilityData mLearningAbility = this.mLearningAbilities[index1];
        if ((user == null || mLearningAbility.CheckEnableUseAbility(user, job_index) || bCheckCondition) && mLearningAbility.Skills != null)
        {
          for (int index2 = 0; index2 < mLearningAbility.Skills.Count; ++index2)
          {
            SkillData skill = mLearningAbility.Skills[index2];
            ArtifactData.WorkScaleStatus.Clear();
            SkillData.GetHomePassiveBuffStatus(skill, EElement.None, ref fixed_status, ref ArtifactData.WorkScaleStatus);
            scale_status.Add(ArtifactData.WorkScaleStatus);
          }
        }
      }
    }

    public bool CheckEquiped()
    {
      UnitData unit = (UnitData) null;
      JobData job = (JobData) null;
      return MonoSingleton<GameManager>.Instance.Player.FindOwner(this, out unit, out job);
    }

    public bool IsInspiration()
    {
      return !string.IsNullOrEmpty(this.mArtifactParam.insp_skill) && (int) this.mRarity >= 4 && (int) this.mLv >= (int) this.mRarityParam.ArtifactLvCap;
    }

    public bool UsableInspirationSkillLvUp(
      ArtifactParam source_artifact_param,
      AbilityParam source_ability_param)
    {
      if (!this.IsInspiration())
        return false;
      if (this.mInspirationSkillList != null && this.mInspirationSkillList.Count > 0)
      {
        for (int index = 0; index < this.mInspirationSkillList.Count; ++index)
        {
          InspirationSkillData inspirationSkill = this.mInspirationSkillList[index];
          if (inspirationSkill != null && inspirationSkill.AbilityData != null && inspirationSkill.AbilityData.Param != null && inspirationSkill.AbilityData.Param == source_ability_param)
            return true;
        }
      }
      return this.ArtifactParam == source_artifact_param && this.ArtifactParam.insp_lv_bonus > 0;
    }

    public InspirationSkillData GetCurrentInspirationSkillData()
    {
      return this.InspirationSkillList == null || this.InspirationSkillList.Count <= 0 ? (InspirationSkillData) null : this.InspirationSkillList.Find((Predicate<InspirationSkillData>) (insp => (bool) insp.IsSet));
    }

    public InspirationSkillData GetInspirationSkillDataBySlot(int slot_no)
    {
      return this.InspirationSkillList == null || this.InspirationSkillList.Count <= 0 ? (InspirationSkillData) null : this.InspirationSkillList.Find((Predicate<InspirationSkillData>) (insp => (int) insp.Slot == slot_no));
    }

    public bool IsEquipped(bool is_include_over_write = false)
    {
      UnitData unit = (UnitData) null;
      JobData job = (JobData) null;
      return MonoSingleton<GameManager>.Instance.Player.FindOwner(this, out unit, out job, is_include_over_write);
    }

    public int GetNextOpenInspSlot()
    {
      int num = (int) this.InspirationSkillSlotNum + 1;
      int skillMaxOpenCount = MonoSingleton<GameManager>.Instance.MasterParam.GetInspSkillMaxOpenCount();
      return num > skillMaxOpenCount ? -1 : num;
    }

    public int GetTotalInspLvUpCount(InspSkillParam base_skill_param, List<ArtifactData> materials)
    {
      int totalInspLvUpCount = 0;
      foreach (ArtifactData material in materials)
        totalInspLvUpCount += this.GetInspLvUpCount(base_skill_param, material);
      return totalInspLvUpCount;
    }

    private int GetInspLvUpCount(InspSkillParam base_skill_param, ArtifactData material)
    {
      int inspLvUpCount = 0;
      if (this.ArtifactParam.iname == material.ArtifactParam.iname)
        inspLvUpCount += material.ArtifactParam.insp_lv_bonus;
      if (material.InspirationSkillList == null || material.InspirationSkillList.Count <= 0)
        return inspLvUpCount;
      List<InspirationSkillData> all = material.InspirationSkillList.FindAll((Predicate<InspirationSkillData>) (item => item.InspSkillParam.SkillID == base_skill_param.SkillID));
      if (all == null || all.Count <= 0)
        return inspLvUpCount;
      foreach (InspirationSkillData inspirationSkillData in all)
        inspLvUpCount += (int) inspirationSkillData.Lv;
      return inspLvUpCount;
    }

    public List<ArtifactData> InspMaterialListSort(
      InspSkillParam base_skill_param,
      List<ArtifactData> materials)
    {
      List<ArtifactData> artifactDataList = new List<ArtifactData>((IEnumerable<ArtifactData>) materials);
      if (base_skill_param == null)
        return artifactDataList;
      List<ArtifactData> all = materials.FindAll((Predicate<ArtifactData>) (item => item.ArtifactParam.iname == this.ArtifactParam.iname));
      if (all != null)
      {
        all.Sort((Comparison<ArtifactData>) ((a, b) => this.GetInspLvUpCount(base_skill_param, a) - this.GetInspLvUpCount(base_skill_param, b)));
        foreach (ArtifactData artifactData in all)
          artifactDataList.Remove(artifactData);
      }
      artifactDataList.Sort((Comparison<ArtifactData>) ((a, b) => this.GetInspLvUpCount(base_skill_param, a) - this.GetInspLvUpCount(base_skill_param, b)));
      if (all != null)
        artifactDataList.AddRange((IEnumerable<ArtifactData>) all);
      return artifactDataList;
    }

    public bool GetOwner(List<UnitData> units, out UnitData owner, out JobData owner_job)
    {
      owner = (UnitData) null;
      owner_job = (JobData) null;
      for (int index1 = 0; index1 < units.Count; ++index1)
      {
        for (int index2 = 0; index2 < units[index1].Jobs.Length; ++index2)
        {
          for (int index3 = 0; index3 < units[index1].Jobs[index2].Artifacts.Length; ++index3)
          {
            if (units[index1].Jobs[index2].Artifacts[index3] == (long) this.UniqueID)
            {
              owner = units[index1];
              owner_job = units[index1].Jobs[index2];
              return true;
            }
          }
        }
      }
      return false;
    }

    public static ArtifactData CreateArtifactDataForDisplay(string iname, int rarity = -1)
    {
      if (string.IsNullOrEmpty(iname))
      {
        DebugUtility.LogError("inameが指定されていません.");
        return (ArtifactData) null;
      }
      ArtifactParam artifactParam = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(iname);
      if (artifactParam != null)
        return ArtifactData.CreateArtifactDataForDisplay(artifactParam, rarity);
      DebugUtility.LogError("iname:" + iname + "のArtifactParamが存在しません.");
      return (ArtifactData) null;
    }

    public static ArtifactData CreateArtifactDataForDisplay(ArtifactParam param, int rarity = -1)
    {
      if (param == null)
      {
        DebugUtility.LogError("ArtifactParamが指定されていません.");
        return (ArtifactData) null;
      }
      ArtifactData artifactDataForDisplay = new ArtifactData();
      artifactDataForDisplay.Deserialize(new Json_Artifact()
      {
        iid = 1L,
        exp = 0,
        iname = param.iname,
        fav = 0,
        rare = Math.Min(Math.Max(param.rareini, rarity), param.raremax)
      });
      return artifactDataForDisplay;
    }

    [Flags]
    public enum RarityUpResults
    {
      Success = 0,
      NoLv = 1,
      NoGold = 2,
      NoKakera = 4,
      RarityMaxed = 8,
    }
  }
}
