// Decompiled with JetBrains decompiler
// Type: SRPG.SkillPowerUpResultContent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class SkillPowerUpResultContent : MonoBehaviour
  {
    [SerializeField]
    private SkillPowerUpResultContentStatus statusBase;
    [SerializeField]
    private Text skillNameText;
    private List<SkillPowerUpResultContentStatus> contentsList = new List<SkillPowerUpResultContentStatus>();

    public void Start() => ((Component) this.statusBase).gameObject.SetActive(false);

    public void SetData(
      SkillPowerUpResultContent.Param param,
      List<ParamTypes> dispTypeList,
      List<ParamTypes> dispTypeListMul)
    {
      foreach (Component contents in this.contentsList)
        UnityEngine.Object.Destroy((UnityEngine.Object) contents.gameObject);
      this.contentsList.Clear();
      this.skillNameText.text = param.skilName;
      foreach (SkillPowerUpResultContent.DispType dispType in param.GetAllBonusChangeType())
      {
        if (!dispType.isScale && dispTypeList.Contains(dispType.type))
        {
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(((Component) this.statusBase).gameObject);
          gameObject.transform.SetParent(((Component) this.statusBase).transform.parent, false);
          SkillPowerUpResultContentStatus component = gameObject.GetComponent<SkillPowerUpResultContentStatus>();
          component.SetData(param, dispType.type, false);
          gameObject.SetActive(true);
          this.contentsList.Add(component);
        }
        if (dispType.isScale && dispTypeListMul.Contains(dispType.type))
        {
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(((Component) this.statusBase).gameObject);
          gameObject.transform.SetParent(((Component) this.statusBase).transform.parent, false);
          SkillPowerUpResultContentStatus component = gameObject.GetComponent<SkillPowerUpResultContentStatus>();
          component.SetData(param, dispType.type, true);
          gameObject.SetActive(true);
          this.contentsList.Add(component);
        }
      }
    }

    public class DispType
    {
      public bool isScale;
      public ParamTypes type;

      public DispType(ParamTypes inType, bool inIsScale)
      {
        this.type = inType;
        this.isScale = inIsScale;
      }
    }

    public class Param
    {
      public BaseStatus prevParam;
      public BaseStatus prevParamMul;
      public BaseStatus currentParam;
      public BaseStatus currentParamMul;
      public BaseStatus prevParamBonus;
      public BaseStatus prevParamBonusMul;
      public BaseStatus currentParamBonus;
      public BaseStatus currentParamBonusMul;
      private List<ParamTypes> typeList = new List<ParamTypes>();
      private List<ParamTypes> typeListMul = new List<ParamTypes>();

      public Param(
        SkillData groupSkill,
        ConceptCardData currentCardData,
        int levelCap,
        int awakeCountCap,
        int prevLevel,
        int prevAwakeCount,
        bool includeMaxPowerUp)
      {
        this.skilName = groupSkill.Name;
        this.prevParam = new BaseStatus();
        this.prevParamMul = new BaseStatus();
        this.currentParam = new BaseStatus();
        this.currentParamMul = new BaseStatus();
        this.prevParamBonus = new BaseStatus();
        this.prevParamBonusMul = new BaseStatus();
        this.currentParamBonus = new BaseStatus();
        this.currentParamBonusMul = new BaseStatus();
        ConceptCardParam.GetSkillAllStatus(groupSkill.SkillID, levelCap, (int) currentCardData.Lv, ref this.currentParam, ref this.currentParamMul);
        ConceptCardParam.GetSkillAllStatus(groupSkill.SkillID, levelCap, prevLevel, ref this.prevParam, ref this.currentParamMul);
        SkillPowerUpResultContent.Param.GetBonusStatus(groupSkill, currentCardData, levelCap, awakeCountCap, (int) currentCardData.Lv, (int) currentCardData.AwakeCount, ref this.currentParamBonus, ref this.currentParamBonusMul, includeMaxPowerUp);
        SkillPowerUpResultContent.Param.GetBonusStatus(groupSkill, currentCardData, levelCap, awakeCountCap, prevLevel, prevAwakeCount, ref this.prevParamBonus, ref this.currentParamBonusMul, false);
        int length = Enum.GetValues(typeof (ParamTypes)).Length;
        for (int index1 = 0; index1 < length; ++index1)
        {
          switch (index1)
          {
            case 2:
              continue;
            case 153:
              for (int index2 = 0; index2 < this.currentParamBonus.tokkou.Count; ++index2)
              {
                int num1 = 0;
                int num2 = (int) this.currentParamBonus.tokkou[index2].value;
                if (this.prevParamBonus.tokkou.Count > index2)
                  num1 = (int) this.prevParamBonus.tokkou[index2].value;
                if (num1 != num2)
                {
                  this.typeList.Add(ParamTypes.Tokkou);
                  break;
                }
              }
              for (int index3 = 0; index3 < this.currentParamBonusMul.tokkou.Count; ++index3)
              {
                int num3 = 0;
                int num4 = (int) this.currentParamBonusMul.tokkou[index3].value;
                if (this.prevParamBonusMul.tokkou.Count > index3)
                  num3 = (int) this.prevParamBonusMul.tokkou[index3].value;
                if (num3 != num4)
                {
                  this.typeListMul.Add(ParamTypes.Tokkou);
                  break;
                }
              }
              continue;
            case 190:
              for (int index4 = 0; index4 < this.currentParamBonus.tokubou.Count; ++index4)
              {
                int num5 = 0;
                int num6 = (int) this.currentParamBonus.tokubou[index4].value;
                if (this.prevParamBonus.tokubou.Count > index4)
                  num5 = (int) this.prevParamBonus.tokubou[index4].value;
                if (num5 != num6)
                {
                  this.typeList.Add(ParamTypes.Tokubou);
                  break;
                }
              }
              for (int index5 = 0; index5 < this.currentParamBonusMul.tokubou.Count; ++index5)
              {
                int num7 = 0;
                int num8 = (int) this.currentParamBonusMul.tokubou[index5].value;
                if (this.prevParamBonusMul.tokubou.Count > index5)
                  num7 = (int) this.prevParamBonusMul.tokubou[index5].value;
                if (num7 != num8)
                {
                  this.typeListMul.Add(ParamTypes.Tokubou);
                  break;
                }
              }
              continue;
            default:
              ParamTypes type = (ParamTypes) index1;
              if (this.prevParamBonus[type] != this.currentParamBonus[type])
                this.typeList.Add(type);
              if (this.prevParamBonusMul[type] != this.currentParamBonusMul[type])
              {
                this.typeListMul.Add(type);
                continue;
              }
              continue;
          }
        }
      }

      public string skilName { get; private set; }

      public int GetBonusChangeTypeNum(bool scalingStatus)
      {
        return scalingStatus ? this.typeListMul.Count : this.typeList.Count;
      }

      public bool IsBonusParamChanged() => this.typeListMul.Count > 0 || this.typeList.Count > 0;

      [DebuggerHidden]
      public IEnumerable<SkillPowerUpResultContent.DispType> GetAllBonusChangeType()
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        SkillPowerUpResultContent.Param.\u003CGetAllBonusChangeType\u003Ec__Iterator0 allBonusChangeType = new SkillPowerUpResultContent.Param.\u003CGetAllBonusChangeType\u003Ec__Iterator0()
        {
          \u0024this = this
        };
        // ISSUE: reference to a compiler-generated field
        allBonusChangeType.\u0024PC = -2;
        return (IEnumerable<SkillPowerUpResultContent.DispType>) allBonusChangeType;
      }

      private static void GetBonusStatus(
        SkillData groupSkill,
        ConceptCardData currentCardData,
        int levelCap,
        int awakeCountCap,
        int level,
        int awakeCount,
        ref BaseStatus bonusAdd,
        ref BaseStatus bonusScale,
        bool includeMaxPowerUp)
      {
        if (currentCardData.EquipEffects == null)
          return;
        foreach (ConceptCardEquipEffect equipEffect in currentCardData.EquipEffects)
        {
          if (equipEffect.CardSkill != null && equipEffect.CardSkill.Name == groupSkill.Name)
          {
            equipEffect.GetAddCardSkillBuffStatusAwake(awakeCount, awakeCountCap, ref bonusAdd, ref bonusScale);
            if (!includeMaxPowerUp || level != levelCap || awakeCount != awakeCountCap)
              break;
            BaseStatus total_add = new BaseStatus();
            BaseStatus total_scale = new BaseStatus();
            equipEffect.GetAddCardSkillBuffStatusLvMax(level, levelCap, awakeCount, ref total_add, ref total_scale);
            bonusAdd.Add(total_add);
            bonusScale.Add(total_scale);
            break;
          }
        }
      }
    }
  }
}
