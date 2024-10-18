// Decompiled with JetBrains decompiler
// Type: SRPG.SkillDeriveList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class SkillDeriveList : MonoBehaviour
  {
    [HeaderBar("▼派生元スキルのバナーが属するリストの親")]
    [SerializeField]
    private GameObject m_BaseSkillRoot;
    [SerializeField]
    private GameObject m_DisableBaseObject;
    [HeaderBar("▼派生先スキル関連のオブジェクトの親")]
    [SerializeField]
    private GameObject m_DeriveObjectRoot;
    [SerializeField]
    private RectTransform m_DeriveSkillListRoot;
    [SerializeField]
    private GameObject m_DeriveSkillTemplate;
    [SerializeField]
    [BitMask]
    private SkillDeriveList.ShowFlags m_ShowFlags;
    private List<GameObject> m_DeriveSkills;

    public bool HasDerive
    {
      get
      {
        if (this.m_DeriveSkills != null)
          return this.m_DeriveSkills.Count > 0;
        return false;
      }
    }

    public bool IsShowBaseSkill
    {
      get
      {
        return this.ShowFlags_IsOn(SkillDeriveList.ShowFlags.ShowBaseSkill);
      }
      set
      {
        this.ShowFlags_Set(SkillDeriveList.ShowFlags.ShowBaseSkill, value);
      }
    }

    public bool IsDisableBaseSkillInteractable
    {
      get
      {
        return this.ShowFlags_IsOn(SkillDeriveList.ShowFlags.DisableBaseSkillInteractable);
      }
    }

    private void Start()
    {
      GameUtility.SetGameObjectActive(this.m_DeriveSkillTemplate, false);
    }

    public void Setup(SkillParam baseSkill, List<SkillDeriveParam> skillDeriveParams)
    {
      if (skillDeriveParams != null && skillDeriveParams.Count > 0)
      {
        foreach (SkillDeriveParam skillDeriveParam in skillDeriveParams)
          this.CreateDeriveSkillItem(skillDeriveParam);
      }
      else
        this.IsShowBaseSkill = true;
      if ((UnityEngine.Object) this.m_BaseSkillRoot != (UnityEngine.Object) null)
        DataSource.Bind<SkillParam>(this.m_BaseSkillRoot, baseSkill, false);
      this.UpdateUIActive();
    }

    private void CreateDeriveSkillItem(SkillDeriveParam skillDeriveParam)
    {
      if (this.m_DeriveSkills == null)
        this.m_DeriveSkills = new List<GameObject>();
      if (!((UnityEngine.Object) this.m_DeriveSkillTemplate != (UnityEngine.Object) null))
        return;
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.m_DeriveSkillTemplate);
      DataSource.Bind<SkillAbilityDeriveParam>(gameObject, skillDeriveParam.m_SkillAbilityDeriveParam, false);
      DataSource.Bind<SkillParam>(gameObject, skillDeriveParam.m_DeriveParam, false);
      DataSource.Bind<SkillDeriveParam>(gameObject, skillDeriveParam, false);
      gameObject.SetActive(true);
      gameObject.transform.SetParent((Transform) this.m_DeriveSkillListRoot, false);
      this.m_DeriveSkills.Add(gameObject);
    }

    public void SwitchBaseSkillEnable(bool enable)
    {
      this.IsShowBaseSkill = enable;
      this.UpdateUIActive();
    }

    private void UpdateUIActive()
    {
      if (this.IsShowBaseSkill)
      {
        GameUtility.SetGameObjectActive(this.m_BaseSkillRoot, true);
        if (this.HasDerive)
        {
          GameUtility.SetGameObjectActive(this.m_DeriveObjectRoot, true);
          int num = this.m_DeriveSkills.Count - 1;
          for (int index = 0; index < this.m_DeriveSkills.Count; ++index)
          {
            DeriveListItem component = this.m_DeriveSkills[index].GetComponent<DeriveListItem>();
            if ((UnityEngine.Object) component != (UnityEngine.Object) null)
              component.SetLineActive(true, index != num);
          }
        }
        else
          GameUtility.SetGameObjectActive(this.m_DeriveObjectRoot, false);
        if (!this.IsDisableBaseSkillInteractable)
          return;
        GameUtility.SetGameObjectActive(this.m_DisableBaseObject, this.HasDerive);
      }
      else
      {
        if (this.HasDerive)
        {
          GameUtility.SetGameObjectActive(this.m_BaseSkillRoot, false);
          for (int index = 0; index < this.m_DeriveSkills.Count; ++index)
          {
            DeriveListItem component = this.m_DeriveSkills[index].GetComponent<DeriveListItem>();
            if ((UnityEngine.Object) component != (UnityEngine.Object) null)
              component.SetLineActive(false, false);
          }
        }
        else
          GameUtility.SetGameObjectActive(this.m_BaseSkillRoot, true);
        if (!this.IsDisableBaseSkillInteractable)
          return;
        GameUtility.SetGameObjectActive(this.m_DisableBaseObject, this.HasDerive);
      }
    }

    private bool ShowFlags_IsOn(SkillDeriveList.ShowFlags flags)
    {
      return (this.m_ShowFlags & flags) != (SkillDeriveList.ShowFlags) 0;
    }

    private bool ShowFlags_IsOff(SkillDeriveList.ShowFlags flags)
    {
      return (this.m_ShowFlags & flags) == (SkillDeriveList.ShowFlags) 0;
    }

    private void ShowFlags_SetOn(SkillDeriveList.ShowFlags flags)
    {
      this.m_ShowFlags |= flags;
    }

    private void ShowFlags_SetOff(SkillDeriveList.ShowFlags flags)
    {
      this.m_ShowFlags &= ~flags;
    }

    private void ShowFlags_Set(SkillDeriveList.ShowFlags flags, bool value)
    {
      if (value)
        this.ShowFlags_SetOn(flags);
      else
        this.ShowFlags_SetOff(flags);
    }

    [Flags]
    private enum ShowFlags
    {
      ShowBaseSkill = 1,
      DisableBaseSkillInteractable = 2,
    }
  }
}
