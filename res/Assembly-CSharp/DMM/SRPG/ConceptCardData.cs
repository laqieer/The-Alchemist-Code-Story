// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using MessagePack;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  public class ConceptCardData
  {
    public const int CONCEPT_CARD_MAIN_SLOT_INDEX = 0;
    public const int MAX_CONCEPT_CARD_EQUIP_NUM = 2;
    public const int NO_EQUIPED_INDEX = 255;
    private OLong mUniqueID = (OLong) 0L;
    private OInt mLv;
    private OInt mExp;
    private OInt mTrust;
    private bool mFavorite;
    private int mTrustBonus;
    private int mTrustBonusCount;
    private OInt mAwakeCount;
    private ConceptCardParam mConceptCardParam;
    private List<ConceptCardEquipEffect> mEquipEffects;
    private BaseStatus mFixStatus = new BaseStatus();
    private BaseStatus mScaleSatus = new BaseStatus();
    private SkillData mLeaderSkill;
    private byte mEquipedSlotIndex = byte.MaxValue;

    public OLong UniqueID => this.mUniqueID;

    public OInt Rarity => (OInt) this.mConceptCardParam.rare;

    public OInt Lv => this.mLv;

    public OInt Exp => this.mExp;

    public OInt Trust => this.mTrust;

    public bool Favorite => this.mFavorite;

    public int TrustBonus => this.mTrustBonus;

    public int TrustBonusCount => this.mTrustBonusCount;

    public ConceptCardParam Param => this.mConceptCardParam;

    public List<ConceptCardEquipEffect> EquipEffects => this.mEquipEffects;

    public OInt CurrentLvCap => (OInt) (this.mConceptCardParam.lvcap + (int) this.AwakeLevel);

    public OInt LvCap => (OInt) (this.mConceptCardParam.lvcap + this.AwakeLevelCap);

    public OInt AwakeLevel
    {
      get
      {
        return (OInt) ((int) this.AwakeCount * (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardAwakeUnlockLevelCap);
      }
    }

    public bool IsEnableAwake => this.mConceptCardParam.IsEnableAwake;

    public int AwakeCountCap => this.mConceptCardParam.AwakeCountCap;

    public int AwakeLevelCap => this.mConceptCardParam.AwakeLevelCap;

    public SkillData LeaderSkill => this.mLeaderSkill;

    public int EquipedSlotIndex => (int) this.mEquipedSlotIndex;

    public OInt AwakeCount
    {
      get
      {
        if (this.IsEnableAwake)
        {
          RarityParam rarityParam = MonoSingleton<GameManager>.Instance.MasterParam.GetRarityParam((int) this.Rarity);
          if (rarityParam != null)
            return (OInt) Mathf.Min((int) this.mAwakeCount, (int) rarityParam.ConceptCardAwakeCountMax);
        }
        return (OInt) 0;
      }
    }

    public bool IsEquipedMainSlot => this.mEquipedSlotIndex == (byte) 0;

    public bool IsEquipedSubSlot
    {
      get => this.mEquipedSlotIndex > (byte) 0 && this.mEquipedSlotIndex != byte.MaxValue;
    }

    public int CurrentDecreaseEffectRate
    {
      get
      {
        GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
        return UnityEngine.Object.op_Equality((UnityEngine.Object) instanceDirect, (UnityEngine.Object) null) || instanceDirect.MasterParam == null || instanceDirect.MasterParam.FixParam == null || instanceDirect.MasterParam.FixParam.ConceptcardSlot2DecRate == null || (int) this.mAwakeCount < 0 || (int) this.mAwakeCount >= instanceDirect.MasterParam.FixParam.ConceptcardSlot2DecRate.Length ? 0 : instanceDirect.MasterParam.FixParam.ConceptcardSlot2DecRate[(int) this.mAwakeCount];
      }
    }

    public bool LeaderSkillIsAvailable()
    {
      return this.LeaderSkill != null && (int) this.AwakeCount >= this.AwakeCountCap && (int) this.Lv >= (int) this.LvCap;
    }

    public bool Deserialize(JSON_ConceptCard json)
    {
      this.mUniqueID = (OLong) json.iid;
      this.mExp = (OInt) json.exp;
      this.mTrust = (OInt) json.trust;
      this.mFavorite = json.fav != 0;
      this.mTrustBonus = json.trust_bonus;
      this.mAwakeCount = (OInt) json.plus;
      this.mConceptCardParam = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(json.iname);
      this.mLv = (OInt) this.CalcCardLevel();
      this.UpdateEquipEffect();
      if (!string.IsNullOrEmpty(this.Param.leader_skill))
      {
        this.mLeaderSkill = new SkillData();
        this.mLeaderSkill.Setup(this.Param.leader_skill, 1);
      }
      return true;
    }

    public JSON_ConceptCard Serialize()
    {
      return new JSON_ConceptCard()
      {
        iid = (long) this.mUniqueID,
        iname = this.mConceptCardParam == null ? string.Empty : this.mConceptCardParam.iname,
        exp = (int) this.mExp,
        trust = (int) this.mTrust,
        fav = !this.mFavorite ? 0 : 1,
        trust_bonus = this.mTrustBonus,
        plus = (int) this.mAwakeCount
      };
    }

    public ConceptCardData Clone()
    {
      ConceptCardData conceptCardData = new ConceptCardData();
      JSON_ConceptCard json = this.Serialize();
      conceptCardData.Deserialize(json);
      return conceptCardData;
    }

    public int SellGold
    {
      get
      {
        return this.Param.sell + ((int) this.Lv - 1) * this.Param.sell * MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardSellMul / 100;
      }
    }

    public int SellCoinItemNum
    {
      get
      {
        return (int) Math.Round((double) this.Param.coin_item * (double) MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardCoinRate((int) this.AwakeCount), MidpointRounding.AwayFromZero);
      }
    }

    public int MixExp
    {
      get
      {
        return this.Param.en_exp + ((int) this.Lv - 1) * this.Param.en_exp * MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardExpMul / 100;
      }
    }

    public void SetTrust(int trust) => this.mTrust = (OInt) trust;

    public void SetBonus(int bonus) => this.mTrustBonus = bonus;

    public void SetBonusCount(int bonusCount) => this.mTrustBonusCount = bonusCount;

    public void SetSlotIndex(int slotIndex) => this.mEquipedSlotIndex = (byte) slotIndex;

    public void ResetSlotIndex() => this.mEquipedSlotIndex = byte.MaxValue;

    private void UpdateEquipEffect()
    {
      this.mEquipEffects = (List<ConceptCardEquipEffect>) null;
      if (this.mConceptCardParam.effects != null && this.mConceptCardParam.effects.Length > 0)
      {
        this.mEquipEffects = new List<ConceptCardEquipEffect>();
        for (int index = 0; index < this.mConceptCardParam.effects.Length; ++index)
        {
          ConceptCardEquipEffect conceptCardEquipEffect = new ConceptCardEquipEffect();
          conceptCardEquipEffect.Setup(this, this.mConceptCardParam.effects[index], (int) this.Lv, (int) this.LvCap, (int) this.AwakeCount, this.AwakeCountCap);
          this.mEquipEffects.Add(conceptCardEquipEffect);
        }
      }
      this.UpdateStatus(ref this.mFixStatus, ref this.mScaleSatus);
    }

    private int CalcCardLevel()
    {
      return MonoSingleton<GameManager>.Instance.MasterParam.CalcConceptCardLevel((int) this.Rarity, (int) this.mExp, (int) this.CurrentLvCap);
    }

    public int GetExpToLevelMax()
    {
      return MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardLevelExp((int) this.Rarity, (int) this.CurrentLvCap) - (int) this.mExp;
    }

    public int GetTrustToLevelMax()
    {
      return MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardTrustMax(this) - (int) this.mTrust;
    }

    public List<ConceptCardEquipEffect> GetEnableEquipEffects(
      UnitData unit_data,
      JobData job_data,
      bool is_force = false)
    {
      List<ConceptCardEquipEffect> enableEquipEffects = new List<ConceptCardEquipEffect>();
      if (this.mEquipEffects == null)
        return enableEquipEffects;
      for (int index = 0; index < this.mEquipEffects.Count; ++index)
      {
        if (is_force || this.IsMatchConditions(unit_data.UnitParam, job_data, this.mEquipEffects[index].ConditionsIname))
          enableEquipEffects.Add(this.mEquipEffects[index]);
      }
      return enableEquipEffects;
    }

    public bool IsMatchConditions(UnitParam unit_param, JobData job_data, string conditions_iname)
    {
      ConceptCardConditionsParam conceptCardConditions = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardConditions(conditions_iname);
      return conceptCardConditions == null || conceptCardConditions.IsMatchConditions(unit_param, job_data);
    }

    public static ConceptCardData CreateConceptCardDataForDisplay(string iname)
    {
      ConceptCardData cardDataForDisplay = new ConceptCardData();
      cardDataForDisplay.Deserialize(new JSON_ConceptCard()
      {
        iid = 1L,
        iname = iname,
        exp = 0,
        trust = 0,
        fav = 0
      });
      return cardDataForDisplay;
    }

    public bool Filter(FilterConceptCardPrefs filter)
    {
      FilterConceptCardParam[] conceptCardParams = MonoSingleton<GameManager>.Instance.MasterParam.FilterConceptCardParams;
      for (int index1 = 0; index1 < conceptCardParams.Length; ++index1)
      {
        if (!filter.IsDisableFilterAll(conceptCardParams[index1].iname))
        {
          bool flag1 = false;
          int length = this.Param.concept_card_groups == null ? 0 : this.Param.concept_card_groups.Length;
          for (int index2 = 0; index2 < conceptCardParams[index1].conditions.Length; ++index2)
          {
            FilterConceptCardConditionParam condition = conceptCardParams[index1].conditions[index2];
            bool flag2 = filter.GetValue(condition.parent.iname, condition.cnds_iname);
            if (conceptCardParams[index1].IsEnableFilterType(eConceptCardFilterTypes.Rarity) && (int) this.Rarity == condition.rarity)
            {
              flag1 = true;
              if (!flag2)
                return false;
            }
            if (conceptCardParams[index1].IsEnableFilterType(eConceptCardFilterTypes.Birth))
            {
              if ((EBirth) this.Param.birth_id == condition.birth)
              {
                flag1 = true;
                if (!flag2)
                  return false;
              }
              if (this.Param.birth_id == 0 && condition.birth == EBirth.Other)
              {
                flag1 = true;
                if (!flag2)
                  return false;
              }
            }
            if (conceptCardParams[index1].IsEnableFilterType(eConceptCardFilterTypes.CardGroup) && length > 0 && Array.FindIndex<string>(this.Param.concept_card_groups, (Predicate<string>) (group => group == condition.card_group)) >= 0)
            {
              flag1 = true;
              if (!flag2)
              {
                --length;
                if (length <= 0)
                  return false;
              }
            }
          }
          if (!flag1)
            return false;
        }
      }
      return true;
    }

    public bool FilterEnhance(string filter_iname) => this.mConceptCardParam.iname == filter_iname;

    public long GetSortData(ConceptCardListSortWindow.Type type)
    {
      BaseStatus equipEffectStatus = this.GetNoConditionsEquipEffectStatus();
      switch (type)
      {
        case ConceptCardListSortWindow.Type.LEVEL:
          return (long) (int) this.Lv;
        case ConceptCardListSortWindow.Type.RARITY:
          return (long) (int) this.Rarity;
        case ConceptCardListSortWindow.Type.ATK:
          return (long) (int) equipEffectStatus.param.atk;
        case ConceptCardListSortWindow.Type.DEF:
          return (long) (int) equipEffectStatus.param.def;
        default:
          if (type == ConceptCardListSortWindow.Type.MAG)
            return (long) (int) equipEffectStatus.param.mag;
          if (type == ConceptCardListSortWindow.Type.MND)
            return (long) (int) equipEffectStatus.param.mnd;
          if (type == ConceptCardListSortWindow.Type.SPD)
            return (long) (int) equipEffectStatus.param.spd;
          if (type == ConceptCardListSortWindow.Type.LUCK)
            return (long) (int) equipEffectStatus.param.luk;
          if (type == ConceptCardListSortWindow.Type.HP)
            return (long) (int) equipEffectStatus.param.hp;
          if (type == ConceptCardListSortWindow.Type.TIME)
            return (long) this.UniqueID;
          if (type == ConceptCardListSortWindow.Type.TRUST)
            return (long) (int) this.Trust;
          return type == ConceptCardListSortWindow.Type.AWAKE ? (long) (int) this.AwakeCount : 0L;
      }
    }

    public int GetSortParam(ParamTypes types)
    {
      return this.mFixStatus == null ? 0 : this.mFixStatus[types];
    }

    public void UpdateStatus(ref BaseStatus fix, ref BaseStatus scale)
    {
      if (this.EquipEffects == null)
        return;
      for (int index = 0; index < this.EquipEffects.Count; ++index)
        this.EquipEffects[index].GetStatus(ref fix, ref scale);
    }

    public void GetStatus(ref BaseStatus fix, ref BaseStatus scale)
    {
      fix = this.mFixStatus;
      scale = this.mScaleSatus;
    }

    public List<ConceptCardEquipEffect> GetNoConditionsEquipEffects()
    {
      List<ConceptCardEquipEffect> conditionsEquipEffects = new List<ConceptCardEquipEffect>();
      if (this.mEquipEffects != null)
      {
        for (int index = 0; index < this.mEquipEffects.Count; ++index)
        {
          if (this.mEquipEffects[index].GetCondition() == null)
            conditionsEquipEffects.Add(this.mEquipEffects[index]);
        }
      }
      return conditionsEquipEffects;
    }

    public BaseStatus GetNoConditionsEquipEffectStatus()
    {
      BaseStatus equipEffectStatus = new BaseStatus();
      List<ConceptCardEquipEffect> conditionsEquipEffects = this.GetNoConditionsEquipEffects();
      for (int index = 0; index < conditionsEquipEffects.Count; ++index)
      {
        BaseStatus status = new BaseStatus();
        BaseStatus scale_status = new BaseStatus();
        SkillData.GetHomePassiveBuffStatus(conditionsEquipEffects[index].EquipSkill, EElement.None, ref status, ref scale_status);
        equipEffectStatus.Add(status);
      }
      return equipEffectStatus;
    }

    public List<SkillData> GetEnableCardSkills(UnitData unit)
    {
      List<SkillData> enableCardSkills = new List<SkillData>();
      if (unit == null)
        return enableCardSkills;
      List<ConceptCardEquipEffect> enable_effects = this.GetEnableEquipEffects(unit, unit.Jobs[unit.JobIndex]);
      for (int i = 0; i < enable_effects.Count; ++i)
      {
        if (enable_effects[i].CardSkill != null && enableCardSkills.FindIndex((Predicate<SkillData>) (skill => skill.SkillID == enable_effects[i].CardSkill.SkillID)) < 0)
          enableCardSkills.Add(enable_effects[i].CardSkill);
      }
      return enableCardSkills;
    }

    public List<BuffEffect> GetEnableCardSkillAddBuffs(UnitData unit, SkillParam parent_card_skill)
    {
      List<BuffEffect> cardSkillAddBuffs = new List<BuffEffect>();
      if (unit == null)
        return cardSkillAddBuffs;
      List<ConceptCardEquipEffect> enable_effects = this.GetEnableEquipEffects(unit, unit.Jobs[unit.JobIndex]);
      for (int i = 0; i < enable_effects.Count; ++i)
      {
        if (enable_effects[i].CardSkill != null && !(enable_effects[i].CardSkill.SkillID != parent_card_skill.iname))
        {
          if (enable_effects[i].AddCardSkillBuffEffectAwake != null)
          {
            if (cardSkillAddBuffs.FindIndex((Predicate<BuffEffect>) (eff => eff.param.iname == enable_effects[i].AddCardSkillBuffEffectAwake.param.iname)) < 0)
              cardSkillAddBuffs.Add(enable_effects[i].AddCardSkillBuffEffectAwake);
            else
              continue;
          }
          if (enable_effects[i].AddCardSkillBuffEffectLvMax != null && cardSkillAddBuffs.FindIndex((Predicate<BuffEffect>) (eff => eff.param.iname == enable_effects[i].AddCardSkillBuffEffectLvMax.param.iname)) < 0)
            cardSkillAddBuffs.Add(enable_effects[i].AddCardSkillBuffEffectLvMax);
        }
      }
      return cardSkillAddBuffs;
    }

    public List<ConceptCardEquipEffect> GetAbilities()
    {
      List<ConceptCardEquipEffect> abilities = new List<ConceptCardEquipEffect>();
      if (this.mEquipEffects == null)
        return abilities;
      for (int index = 0; index < this.mEquipEffects.Count; ++index)
      {
        ConceptCardEquipEffect mEquipEffect = this.mEquipEffects[index];
        if (mEquipEffect.Ability != null)
          abilities.Add(mEquipEffect);
      }
      return abilities;
    }

    public List<AbilityParam> GetMaxLerningAbilities()
    {
      List<AbilityParam> lerningAbilities = new List<AbilityParam>();
      if (this.Param.effects != null && this.Param.effects.Length > 0)
      {
        foreach (ConceptCardEffectsParam effect in this.Param.effects)
        {
          if (!string.IsNullOrEmpty(effect.abil_iname_lvmax) && !effect.abil_iname_lvmax.Equals(effect.abil_iname))
          {
            AbilityParam abilityParam = MonoSingleton<GameManager>.Instance.GetAbilityParam(effect.abil_iname_lvmax);
            if (abilityParam != null)
              lerningAbilities.Add(abilityParam);
          }
        }
      }
      return lerningAbilities;
    }

    public List<ConceptCardEquipEffect> GetCardSkills()
    {
      List<ConceptCardEquipEffect> cardSkills = new List<ConceptCardEquipEffect>();
      if (this.mEquipEffects == null)
        return cardSkills;
      for (int index = 0; index < this.mEquipEffects.Count; ++index)
      {
        ConceptCardEquipEffect mEquipEffect = this.mEquipEffects[index];
        if (mEquipEffect.CardSkill != null)
          cardSkills.Add(mEquipEffect);
      }
      return cardSkills;
    }

    public List<ConceptCardSkillDatailData> GetAbilityDatailData()
    {
      List<ConceptCardSkillDatailData> abilityDatailData = new List<ConceptCardSkillDatailData>();
      List<ConceptCardEquipEffect> abilities = this.GetAbilities();
      List<ConceptCardEquipEffect> cardSkills = this.GetCardSkills();
      for (int index = 0; index < cardSkills.Count; ++index)
      {
        SkillData skill = cardSkills[index].CardSkill;
        if (skill != null && abilityDatailData.FindIndex((Predicate<ConceptCardSkillDatailData>) (abi => abi.skill_data.SkillParam.iname == skill.SkillParam.iname)) <= -1)
          abilityDatailData.Add(new ConceptCardSkillDatailData(cardSkills[index], skill, ConceptCardDetailAbility.ShowType.Skill));
      }
      for (int index1 = 0; index1 < abilities.Count; ++index1)
      {
        AbilityData ability = abilities[index1].Ability;
        if (ability != null)
        {
          for (int index2 = 0; index2 < ability.LearningSkills.Length; ++index2)
          {
            LearningSkill learning_skill = ability.LearningSkills[index2];
            if (learning_skill != null)
            {
              ConceptCardDetailAbility.ShowType _type = ConceptCardDetailAbility.ShowType.Ability;
              SkillData data = ability.Skills.Find((Predicate<SkillData>) (x => x.SkillParam.iname == learning_skill.iname));
              if (data == null)
              {
                SkillParam skillParam = MonoSingleton<GameManager>.Instance.MasterParam.GetSkillParam(learning_skill.iname);
                data = new SkillData();
                data.Setup(skillParam.iname, 1);
                _type = ConceptCardDetailAbility.ShowType.LockSkill;
              }
              if (abilityDatailData.FindIndex((Predicate<ConceptCardSkillDatailData>) (abi => abi.skill_data.SkillParam.iname == data.SkillParam.iname)) <= -1)
                abilityDatailData.Add(new ConceptCardSkillDatailData(abilities[index1], data, _type, learning_skill));
            }
          }
        }
      }
      return abilityDatailData;
    }

    public ConceptCardTrustRewardItemParam GetReward()
    {
      ConceptCardTrustRewardParam trustReward = MonoSingleton<GameManager>.Instance.MasterParam.GetTrustReward(this.Param.trust_reward);
      return trustReward == null || trustReward.rewards == null || trustReward.rewards.Length <= 0 ? (ConceptCardTrustRewardItemParam) null : trustReward.rewards[0];
    }

    public UnitData GetOwner(bool is_include_over_write = false)
    {
      UnitData owner;
      if (is_include_over_write)
      {
        owner = MonoSingleton<GameManager>.Instance.Player.Units.Find((Predicate<UnitData>) (u => u.IsEquipConceptCard((long) this.UniqueID))) ?? UnitOverWriteUtility.GetOwner(this);
      }
      else
      {
        List<UnitData> units = MonoSingleton<GameManager>.Instance.Player.Units;
        owner = !UnitOverWriteUtility.IsNeedOverWrite((eOverWritePartyType) GlobalVars.OverWritePartyType) ? units.Find((Predicate<UnitData>) (u => u.IsEquipConceptCard((long) this.UniqueID))) : UnitOverWriteUtility.GetOwner(this, (eOverWritePartyType) GlobalVars.OverWritePartyType);
      }
      return owner;
    }

    public override string ToString() => base.ToString() + "(" + this.Param.name + ")";

    public static bool IsMainSlot(int conceptCardSlotIndex) => 0 == conceptCardSlotIndex;
  }
}
