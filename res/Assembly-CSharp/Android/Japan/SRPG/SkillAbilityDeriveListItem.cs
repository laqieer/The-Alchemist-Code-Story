// Decompiled with JetBrains decompiler
// Type: SRPG.SkillAbilityDeriveListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class SkillAbilityDeriveListItem : MonoBehaviour
  {
    [HeaderBar("▼派生アビリティ関連")]
    [SerializeField]
    private RectTransform m_AbilityDeriveListRoot;
    [SerializeField]
    private AbilityDeriveList m_AbilityDeriveListTemplate;
    [HeaderBar("▼派生スキル関連")]
    [SerializeField]
    private RectTransform m_SkillDeriveListRoot;
    [SerializeField]
    private SkillDeriveList m_SkillDeriveListTemplate;
    private List<SkillAbilityDeriveListItem.ViewContentSkillParam> m_ViewContentSkillParams;
    private List<SkillAbilityDeriveListItem.ViewContentAbilityParam> m_ViewContentAbilityParams;

    private void Start()
    {
      GameUtility.SetGameObjectActive((Component) this.m_SkillDeriveListTemplate, false);
      GameUtility.SetGameObjectActive((Component) this.m_AbilityDeriveListTemplate, false);
    }

    public void Setup(SkillAbilityDeriveParam skillAbilityDeriveParam)
    {
      this.m_ViewContentSkillParams = SkillAbilityDeriveListItem.CreateViewContentSkillParams(skillAbilityDeriveParam.SkillDeriveParams);
      this.m_ViewContentAbilityParams = SkillAbilityDeriveListItem.CreateViewContentAbilityParams(skillAbilityDeriveParam.AbilityDeriveParams);
      foreach (SkillAbilityDeriveListItem.ViewContentSkillParam contentSkillParam in this.m_ViewContentSkillParams)
        this.CreateListItem(contentSkillParam);
      foreach (SkillAbilityDeriveListItem.ViewContentAbilityParam contentAbilityParam in this.m_ViewContentAbilityParams)
        this.CreateListItem(contentAbilityParam);
      this.UpdateUIActive();
    }

    public void Setup(SkillAbilityDeriveData skillAbilityDeriveData)
    {
      List<SkillDeriveParam> skillDeriveParams = skillAbilityDeriveData.GetAvailableSkillDeriveParams();
      List<AbilityDeriveParam> abilityDeriveParams = skillAbilityDeriveData.GetAvailableAbilityDeriveParams();
      this.m_ViewContentSkillParams = SkillAbilityDeriveListItem.CreateViewContentSkillParams(skillDeriveParams);
      this.m_ViewContentAbilityParams = SkillAbilityDeriveListItem.CreateViewContentAbilityParams(abilityDeriveParams);
      foreach (SkillAbilityDeriveListItem.ViewContentSkillParam contentSkillParam in this.m_ViewContentSkillParams)
        this.CreateListItem(contentSkillParam);
      foreach (SkillAbilityDeriveListItem.ViewContentAbilityParam contentAbilityParam in this.m_ViewContentAbilityParams)
        this.CreateListItem(contentAbilityParam);
      this.UpdateUIActive();
    }

    private static List<SkillAbilityDeriveListItem.ViewContentAbilityParam> CreateViewContentAbilityParams(List<AbilityDeriveParam> deriveAbilityParams)
    {
      List<SkillAbilityDeriveListItem.ViewContentAbilityParam> contentAbilityParamList = new List<SkillAbilityDeriveListItem.ViewContentAbilityParam>();
      foreach (AbilityDeriveParam deriveAbilityParam in deriveAbilityParams)
      {
        AbilityDeriveParam abilityDeriveParam = deriveAbilityParam;
        SkillAbilityDeriveListItem.ViewContentAbilityParam contentAbilityParam = contentAbilityParamList.Find((Predicate<SkillAbilityDeriveListItem.ViewContentAbilityParam>) (content => content.m_BaseAbilityParam == abilityDeriveParam.m_BaseParam));
        if (contentAbilityParam == null)
        {
          contentAbilityParam = new SkillAbilityDeriveListItem.ViewContentAbilityParam();
          contentAbilityParam.m_AbilityDeriveParam = new List<AbilityDeriveParam>();
          contentAbilityParamList.Add(contentAbilityParam);
        }
        contentAbilityParam.m_BaseAbilityParam = abilityDeriveParam.m_BaseParam;
        contentAbilityParam.m_AbilityDeriveParam.Add(abilityDeriveParam);
      }
      return contentAbilityParamList;
    }

    private static List<SkillAbilityDeriveListItem.ViewContentSkillParam> CreateViewContentSkillParams(List<SkillDeriveParam> deriveSkillParams)
    {
      List<SkillAbilityDeriveListItem.ViewContentSkillParam> contentSkillParamList = new List<SkillAbilityDeriveListItem.ViewContentSkillParam>();
      foreach (SkillDeriveParam deriveSkillParam in deriveSkillParams)
      {
        SkillDeriveParam skillDeriveParam = deriveSkillParam;
        SkillAbilityDeriveListItem.ViewContentSkillParam contentSkillParam = contentSkillParamList.Find((Predicate<SkillAbilityDeriveListItem.ViewContentSkillParam>) (content => content.m_BaseSkillParam == skillDeriveParam.m_BaseParam));
        if (contentSkillParam == null)
        {
          contentSkillParam = new SkillAbilityDeriveListItem.ViewContentSkillParam();
          contentSkillParam.m_SkillDeriveParams = new List<SkillDeriveParam>();
          contentSkillParamList.Add(contentSkillParam);
        }
        contentSkillParam.m_BaseSkillParam = skillDeriveParam.m_BaseParam;
        contentSkillParam.m_SkillDeriveParams.Add(skillDeriveParam);
      }
      return contentSkillParamList;
    }

    private void CreateListItem(SkillAbilityDeriveListItem.ViewContentSkillParam viewContentSkillParam)
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.m_SkillDeriveListTemplate.gameObject);
      gameObject.transform.SetParent((Transform) this.m_SkillDeriveListRoot, false);
      gameObject.SetActive(true);
      gameObject.GetComponent<SkillDeriveList>().Setup(viewContentSkillParam.m_BaseSkillParam, viewContentSkillParam.m_SkillDeriveParams);
    }

    private void CreateListItem(SkillAbilityDeriveListItem.ViewContentAbilityParam viewContentAbilityParam)
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.m_AbilityDeriveListTemplate.gameObject);
      gameObject.transform.SetParent((Transform) this.m_AbilityDeriveListRoot, false);
      gameObject.SetActive(true);
      gameObject.GetComponent<AbilityDeriveList>().SetupWithAbilityParam(viewContentAbilityParam.m_BaseAbilityParam, viewContentAbilityParam.m_AbilityDeriveParam);
    }

    private void UpdateUIActive()
    {
      if (this.m_ViewContentAbilityParams.Count < 1)
        GameUtility.SetGameObjectActive((Component) this.m_AbilityDeriveListRoot, false);
      if (this.m_ViewContentSkillParams.Count >= 1)
        return;
      GameUtility.SetGameObjectActive((Component) this.m_SkillDeriveListRoot, false);
    }

    private class ViewContentAbilityParam
    {
      public AbilityParam m_BaseAbilityParam;
      public List<AbilityDeriveParam> m_AbilityDeriveParam;
    }

    private class ViewContentSkillParam
    {
      public SkillParam m_BaseSkillParam;
      public List<SkillDeriveParam> m_SkillDeriveParams;
    }
  }
}
