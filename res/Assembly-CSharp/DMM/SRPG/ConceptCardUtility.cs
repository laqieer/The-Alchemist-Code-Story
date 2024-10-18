// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardUtility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class ConceptCardUtility
  {
    public static bool IsEnableCardSkillForUnit(Unit target, SkillData card_skill)
    {
      if (target == null || card_skill == null || card_skill.SkillParam.condition != ESkillCondition.CardSkill)
        return false;
      BuffEffectParam buffEffectParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetBuffEffectParam(card_skill.SkillParam.target_buff_iname);
      return buffEffectParam != null && BuffEffect.CreateBuffEffect(buffEffectParam, card_skill.Rank, card_skill.GetRankCap()).CheckEnableBuffTarget(target);
    }

    public static bool IsGetUnitConceptCard(string iname)
    {
      if (string.IsNullOrEmpty(iname))
        return false;
      ConceptCardParam conceptCardParam = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(iname);
      return conceptCardParam != null && !string.IsNullOrEmpty(conceptCardParam.first_get_unit);
    }

    public static void GetExpParameter(
      int rarity,
      int exp,
      int current_lvcap,
      out int lv,
      out int nextExp,
      out int expTbl)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) MonoSingleton<GameManager>.Instance, (UnityEngine.Object) null) || MonoSingleton<GameManager>.Instance.MasterParam == null)
      {
        lv = 1;
        expTbl = 1;
        nextExp = 0;
      }
      else
      {
        lv = MonoSingleton<GameManager>.Instance.MasterParam.CalcConceptCardLevel(rarity, exp, current_lvcap);
        int conceptCardLevelExp = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardLevelExp(rarity, lv);
        if (lv < current_lvcap)
        {
          expTbl = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardNextExp(rarity, lv + 1);
          nextExp = expTbl - (exp - conceptCardLevelExp);
        }
        else
        {
          expTbl = 1;
          nextExp = 0;
        }
      }
    }

    public static List<ConceptCardSkillDatailData> CreateConceptCardSkillDatailData(
      AbilityData abilityData)
    {
      List<ConceptCardSkillDatailData> cardSkillDatailData = new List<ConceptCardSkillDatailData>();
      if (abilityData == null)
        return cardSkillDatailData;
      ConceptCardEquipEffect fromAbility = ConceptCardEquipEffect.CreateFromAbility(abilityData);
      for (int index = 0; index < abilityData.LearningSkills.Length; ++index)
      {
        LearningSkill learning_skill = abilityData.LearningSkills[index];
        if (learning_skill != null)
        {
          ConceptCardDetailAbility.ShowType _type = ConceptCardDetailAbility.ShowType.Ability;
          SkillData data = abilityData.Skills.Find((Predicate<SkillData>) (x => x.SkillParam.iname == learning_skill.iname));
          if (data == null)
          {
            SkillParam skillParam = MonoSingleton<GameManager>.Instance.MasterParam.GetSkillParam(learning_skill.iname);
            data = new SkillData();
            data.Setup(skillParam.iname, 1);
            _type = ConceptCardDetailAbility.ShowType.LockSkill;
          }
          if (cardSkillDatailData.FindIndex((Predicate<ConceptCardSkillDatailData>) (abi => abi.skill_data.SkillParam.iname == data.SkillParam.iname)) <= -1)
            cardSkillDatailData.Add(new ConceptCardSkillDatailData(fromAbility, data, _type, learning_skill));
        }
      }
      return cardSkillDatailData;
    }

    public static ConceptCardSkillDatailData CreateConceptCardSkillDatailData(SkillData groupSkill)
    {
      ConceptCardSkillDatailData cardSkillDatailData = (ConceptCardSkillDatailData) null;
      return groupSkill == null ? cardSkillDatailData : new ConceptCardSkillDatailData(ConceptCardEquipEffect.CreateFromGroupSkill(groupSkill), groupSkill, ConceptCardDetailAbility.ShowType.Skill);
    }

    public static ConceptCardData[] SetConceptCardData(
      ConceptCardData[] cards,
      int index,
      ConceptCardData cardData)
    {
      ConceptCardData[] destinationArray = new ConceptCardData[cards.Length];
      Array.Copy((Array) cards, (Array) destinationArray, destinationArray.Length);
      if (0 <= index && index < destinationArray.Length)
        destinationArray[index] = cardData;
      return destinationArray;
    }

    public static void SetDecreaseEffectRateText(Text text, int decreaseEffectRate)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) text, (UnityEngine.Object) null))
        return;
      int num = 100 - decreaseEffectRate;
      text.text = LocalizedText.Get("sys.CONCEPT_CARD_EQUIP_DETAIL_DECREASE_EFFECT_RATE", (object) num);
    }
  }
}
