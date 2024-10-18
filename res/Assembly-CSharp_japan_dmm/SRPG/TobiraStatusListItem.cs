// Decompiled with JetBrains decompiler
// Type: SRPG.TobiraStatusListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class TobiraStatusListItem : MonoBehaviour
  {
    [SerializeField]
    private Text m_TextTobiraName;
    [SerializeField]
    private ImageArray m_IconTobira;
    [SerializeField]
    private GameObject m_ObjectDetail;
    [SerializeField]
    private GameObject m_ObjectLock;
    [SerializeField]
    private GameObject m_ObjectCommingSoon;
    [SerializeField]
    private TobiraLearnSkill m_TobiraLearnSkillTemplate;
    [SerializeField]
    private RectTransform m_TobiraLearnSkillParent;
    [SerializeField]
    private StatusList m_StatusList;
    [SerializeField]
    private GameObject m_TobiraLvMaxObject;
    private TobiraParam.Category m_Category;
    [SerializeField]
    private GameObject m_PlusConceptCardSlot;

    private void Start()
    {
      GameUtility.SetGameObjectActive((Component) this.m_TobiraLearnSkillTemplate, false);
    }

    public void Setup(TobiraData tobiraData)
    {
      if (tobiraData == null)
      {
        DebugUtility.LogError("tobiraDataがnullの時はSetup(TobiraParam param)を使用してください");
      }
      else
      {
        this.m_Category = tobiraData.Param.TobiraCategory;
        this.m_TextTobiraName.text = TobiraParam.GetCategoryName(this.m_Category);
        this.m_IconTobira.ImageIndex = (int) this.m_Category;
        if (tobiraData.IsLearnedLeaderSkill)
        {
          TobiraLearnSkill listItem = this.CreateListItem();
          SkillData skill = new SkillData();
          skill.Setup(tobiraData.LearnedLeaderSkillIname, 1);
          listItem.Setup(skill);
        }
        List<AbilityData> newAbilitys = new List<AbilityData>();
        List<AbilityData> oldAbilitys = new List<AbilityData>();
        TobiraUtility.TryCraeteAbilityData(MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUnitID(tobiraData.Param.UnitIname), tobiraData.Param, tobiraData.Lv, newAbilitys, oldAbilitys);
        for (int index = 0; index < newAbilitys.Count; ++index)
          this.CreateListItem().Setup(newAbilitys[index]);
        BaseStatus add = new BaseStatus();
        BaseStatus scale = new BaseStatus();
        TobiraUtility.CalcTobiraParameter(tobiraData.Param.UnitIname, this.m_Category, tobiraData.Lv, ref add, ref scale);
        this.m_StatusList.SetValues(add, scale);
        GameUtility.SetGameObjectActive(this.m_ObjectDetail, true);
        GameUtility.SetGameObjectActive(this.m_ObjectLock, false);
        GameUtility.SetGameObjectActive(this.m_ObjectCommingSoon, false);
        if (tobiraData.Param.IsUnlockConceptCardSlot2)
          GameUtility.SetGameObjectActive(this.m_PlusConceptCardSlot, tobiraData.Param.Enable);
        else
          GameUtility.SetGameObjectActive(this.m_PlusConceptCardSlot, false);
      }
    }

    public void Setup(TobiraParam param)
    {
      this.m_Category = param.TobiraCategory;
      this.m_TextTobiraName.text = TobiraParam.GetCategoryName(this.m_Category);
      this.m_IconTobira.ImageIndex = (int) this.m_Category;
      if (param.Enable)
      {
        BaseStatus add = new BaseStatus();
        BaseStatus scale = new BaseStatus();
        TobiraUtility.CalcTobiraParameter(param.UnitIname, this.m_Category, 1, ref add, ref scale);
        this.m_StatusList.SetValues(add, scale, true);
      }
      GameUtility.SetGameObjectActive(this.m_ObjectDetail, param.Enable);
      GameUtility.SetGameObjectActive(this.m_ObjectLock, param.Enable);
      GameUtility.SetGameObjectActive(this.m_ObjectCommingSoon, !param.Enable);
      if (param.IsUnlockConceptCardSlot2)
        GameUtility.SetGameObjectActive(this.m_PlusConceptCardSlot, param.Enable);
      else
        GameUtility.SetGameObjectActive(this.m_PlusConceptCardSlot, false);
    }

    public void SetTobiraLvIsMax(bool isMax)
    {
      GameUtility.SetGameObjectActive(this.m_TobiraLvMaxObject, isMax);
    }

    private TobiraLearnSkill CreateListItem()
    {
      GameObject gameObject = Object.Instantiate<GameObject>(((Component) this.m_TobiraLearnSkillTemplate).gameObject);
      TobiraLearnSkill component = gameObject.GetComponent<TobiraLearnSkill>();
      gameObject.transform.SetParent((Transform) this.m_TobiraLearnSkillParent, false);
      gameObject.SetActive(true);
      return component;
    }
  }
}
