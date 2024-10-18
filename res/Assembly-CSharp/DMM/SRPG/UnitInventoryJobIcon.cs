// Decompiled with JetBrains decompiler
// Type: SRPG.UnitInventoryJobIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class UnitInventoryJobIcon : MonoBehaviour
  {
    private static readonly string ANIM_PARAM_JOB_ICON_UNLOCK_BOOL = "unlocked";
    private static readonly string ANIM_PARAM_JOB_ICON_HILIT_BOOL = "hilit";
    private static readonly string ANIM_PARAM_JOB_ICON_DISABLED_BOOL = "disabled";
    private static readonly string ANIM_PARAM_JOB_ICON_ON_BOOL = "on";
    private float SINGLE_ICON_HALF_SIZE_COEF = 0.67f;
    private int JOB_ICON_ROW_COUNT_MAX = 2;
    public UnitInventoryJobIcon.eViewMode mMode;
    [SerializeField]
    private SRPG_Button base_job_icon_button;
    [SerializeField]
    private SRPG_Button cc_job_icon_button;
    [SerializeField]
    private GameObject active_job_indicator;
    private List<SRPG_Button> mCcJobIconButtonList = new List<SRPG_Button>();

    public SRPG_Button BaseJobIconButton => this.base_job_icon_button;

    public List<SRPG_Button> CcJobButtonList => this.mCcJobIconButtonList;

    public float Width
    {
      get
      {
        float w = 0.0f;
        if (((Component) this.base_job_icon_button).gameObject.activeSelf)
        {
          RectTransform transform = ((Component) this.base_job_icon_button).transform as RectTransform;
          w += transform.sizeDelta.x;
        }
        this.mCcJobIconButtonList.ForEach((Action<SRPG_Button>) (_btn =>
        {
          if (!((Component) _btn).gameObject.activeSelf)
            return;
          w += ((RectTransform) ((Component) _btn).transform).sizeDelta.x;
        }));
        return w;
      }
    }

    public float HalfWidth
    {
      get
      {
        return this.mCcJobIconButtonList.FindAll((Predicate<SRPG_Button>) (_btn => ((Component) _btn).gameObject.activeSelf)).Count > 0 ? this.Width * 0.5f : this.Width * this.SINGLE_ICON_HALF_SIZE_COEF;
      }
    }

    public bool IsSingleIcon
    {
      get
      {
        return this.mCcJobIconButtonList.FindAll((Predicate<SRPG_Button>) (_btn => ((Component) _btn).gameObject.activeSelf)).Count == 0;
      }
    }

    public void ResetParam()
    {
      ((UnityEngine.Object) ((Component) this.base_job_icon_button).gameObject).name = "-1";
      ((UnityEngine.Object) ((Component) this.cc_job_icon_button).gameObject).name = "-1";
      this.mCcJobIconButtonList.ForEach((Action<SRPG_Button>) (_btn => UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) _btn).gameObject)));
      this.mCcJobIconButtonList.Clear();
      Animator component = ((Component) this.base_job_icon_button).GetComponent<Animator>();
      component.SetBool(UnitInventoryJobIcon.ANIM_PARAM_JOB_ICON_UNLOCK_BOOL, false);
      component.SetBool(UnitInventoryJobIcon.ANIM_PARAM_JOB_ICON_ON_BOOL, false);
    }

    public void SetParam(
      UnitData unit,
      int[] job_indexes,
      bool is_avtive_job,
      UnitInventoryJobIcon.eViewMode mode)
    {
      this.mMode = mode;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.active_job_indicator, (UnityEngine.Object) null))
        this.active_job_indicator.SetActive(this.mMode == UnitInventoryJobIcon.eViewMode.UNIT_DETAIL && is_avtive_job);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.base_job_icon_button, (UnityEngine.Object) null))
      {
        ((Component) this.base_job_icon_button).gameObject.SetActive(false);
        if (job_indexes.Length > 0 && job_indexes[0] >= 0)
          this.SetIcon(this.base_job_icon_button, unit, job_indexes[0], false);
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.cc_job_icon_button, (UnityEngine.Object) null))
        return;
      ((Component) this.cc_job_icon_button).gameObject.SetActive(false);
      this.mCcJobIconButtonList.ForEach((Action<SRPG_Button>) (_btn => UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) _btn).gameObject)));
      this.mCcJobIconButtonList.Clear();
      for (int index = 1; index < job_indexes.Length; ++index)
      {
        if (job_indexes[index] >= 0 && this.mCcJobIconButtonList.Count < this.JOB_ICON_ROW_COUNT_MAX - 1)
        {
          SRPG_Button slot = UnityEngine.Object.Instantiate<SRPG_Button>(this.cc_job_icon_button, ((Component) this.cc_job_icon_button).transform.parent);
          this.mCcJobIconButtonList.Add(slot);
          this.SetIcon(slot, unit, job_indexes[index], true);
        }
      }
    }

    private void SetIcon(SRPG_Button slot, UnitData unit, int job_data_index, bool is_cc_icon)
    {
      ((UnityEngine.Object) ((Component) slot).gameObject).name = job_data_index.ToString();
      JobData job = unit.Jobs[job_data_index];
      bool isActivated = job.IsActivated;
      bool is_hilit = !job.IsActivated && (unit.CheckJobUnlock(job_data_index, false) || unit.CheckJobRankUpAllEquip(job_data_index));
      bool is_disabled = !job.IsActivated && !unit.CheckJobUnlockable(job_data_index);
      ((Component) slot).gameObject.SetActive(true);
      this.SetAnimationParam(((Component) slot).GetComponent<Animator>(), job.IsActivated, isActivated, is_disabled, is_hilit);
      DataSource.Bind<JobData>(((Component) slot).gameObject, unit.Jobs[job_data_index]);
      GameParameter.UpdateAll(((Component) slot).gameObject);
      if (this.mMode != UnitInventoryJobIcon.eViewMode.UNIT_VIEWER || !is_cc_icon)
        return;
      ((Component) slot).gameObject.SetActive(false);
    }

    private void SetAnimationParam(
      Animator anim,
      bool is_activated,
      bool unlockable,
      bool is_disabled,
      bool is_hilit)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) anim, (UnityEngine.Object) null))
        return;
      if (this.mMode == UnitInventoryJobIcon.eViewMode.UNIT_DETAIL)
      {
        anim.SetBool(UnitInventoryJobIcon.ANIM_PARAM_JOB_ICON_UNLOCK_BOOL, unlockable);
        anim.SetBool(UnitInventoryJobIcon.ANIM_PARAM_JOB_ICON_HILIT_BOOL, is_hilit);
        anim.SetBool(UnitInventoryJobIcon.ANIM_PARAM_JOB_ICON_DISABLED_BOOL, is_disabled);
      }
      if (this.mMode != UnitInventoryJobIcon.eViewMode.UNIT_VIEWER)
        return;
      anim.SetBool(UnitInventoryJobIcon.ANIM_PARAM_JOB_ICON_UNLOCK_BOOL, is_activated);
      if (!is_activated)
        anim.SetBool(UnitInventoryJobIcon.ANIM_PARAM_JOB_ICON_UNLOCK_BOOL, unlockable);
      anim.SetBool(UnitInventoryJobIcon.ANIM_PARAM_JOB_ICON_DISABLED_BOOL, is_disabled);
    }

    public void SetSelectIconAnim(bool is_on)
    {
      ((Component) this.BaseJobIconButton).GetComponent<Animator>().SetBool(UnitInventoryJobIcon.ANIM_PARAM_JOB_ICON_ON_BOOL, is_on);
    }

    public bool IsDisabledBaseJobIcon()
    {
      return ((Component) this.BaseJobIconButton).GetComponent<Animator>().GetBool(UnitInventoryJobIcon.ANIM_PARAM_JOB_ICON_DISABLED_BOOL);
    }

    public enum eViewMode
    {
      NONE,
      UNIT_DETAIL,
      UNIT_VIEWER,
    }
  }
}
