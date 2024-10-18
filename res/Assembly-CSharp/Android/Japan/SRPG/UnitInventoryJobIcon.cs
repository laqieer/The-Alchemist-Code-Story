// Decompiled with JetBrains decompiler
// Type: SRPG.UnitInventoryJobIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class UnitInventoryJobIcon : MonoBehaviour
  {
    private static readonly string ANIM_PARAM_JOB_ICON_UNLOCK_BOOL = "unlocked";
    private static readonly string ANIM_PARAM_JOB_ICON_HILIT_BOOL = "hilit";
    private static readonly string ANIM_PARAM_JOB_ICON_DISABLED_BOOL = "disabled";
    private static readonly string ANIM_PARAM_JOB_ICON_ON_BOOL = "on";
    private float SINGLE_ICON_HALF_SIZE_COEF = 0.67f;
    public UnitInventoryJobIcon.eViewMode mMode;
    [SerializeField]
    private SRPG_Button base_job_icon_button;
    [SerializeField]
    private SRPG_Button cc_job_icon_button;
    [SerializeField]
    private GameObject active_job_indicator;

    public SRPG_Button BaseJobIconButton
    {
      get
      {
        return this.base_job_icon_button;
      }
    }

    public SRPG_Button CcJobButton
    {
      get
      {
        return this.cc_job_icon_button;
      }
    }

    public float Width
    {
      get
      {
        float num = 0.0f;
        if (this.base_job_icon_button.gameObject.activeSelf)
        {
          RectTransform transform = this.base_job_icon_button.transform as RectTransform;
          num += transform.sizeDelta.x;
        }
        if (this.cc_job_icon_button.gameObject.activeSelf)
        {
          RectTransform transform = this.cc_job_icon_button.transform as RectTransform;
          num += transform.sizeDelta.x;
        }
        return num;
      }
    }

    public float HalfWidth
    {
      get
      {
        if (this.cc_job_icon_button.gameObject.activeSelf)
          return this.Width * 0.5f;
        return this.Width * this.SINGLE_ICON_HALF_SIZE_COEF;
      }
    }

    public bool IsSingleIcon
    {
      get
      {
        return !this.cc_job_icon_button.gameObject.activeSelf;
      }
    }

    public void ResetParam()
    {
      this.base_job_icon_button.gameObject.name = "-1";
      this.cc_job_icon_button.gameObject.name = "-1";
      Animator component = this.base_job_icon_button.GetComponent<Animator>();
      component.SetBool(UnitInventoryJobIcon.ANIM_PARAM_JOB_ICON_UNLOCK_BOOL, false);
      component.SetBool(UnitInventoryJobIcon.ANIM_PARAM_JOB_ICON_ON_BOOL, false);
    }

    public void SetParam(UnitData unit, int base_job_index, int cc_job_index, bool is_avtive_job, UnitInventoryJobIcon.eViewMode mode)
    {
      this.mMode = mode;
      if (this.mMode == UnitInventoryJobIcon.eViewMode.UNIT_DETAIL)
        this.active_job_indicator.SetActive(is_avtive_job);
      else if ((UnityEngine.Object) this.active_job_indicator != (UnityEngine.Object) null)
        this.active_job_indicator.SetActive(false);
      if (base_job_index <= -1 && cc_job_index >= 0)
      {
        if (!((UnityEngine.Object) this.base_job_icon_button != (UnityEngine.Object) null))
          return;
        this.cc_job_icon_button.gameObject.SetActive(false);
        this.SetIcon(this.base_job_icon_button, unit, cc_job_index, false);
      }
      else
      {
        if ((UnityEngine.Object) this.base_job_icon_button != (UnityEngine.Object) null)
        {
          this.base_job_icon_button.gameObject.SetActive(false);
          if (base_job_index >= 0)
            this.SetIcon(this.base_job_icon_button, unit, base_job_index, false);
        }
        if (!((UnityEngine.Object) this.cc_job_icon_button != (UnityEngine.Object) null))
          return;
        this.cc_job_icon_button.gameObject.SetActive(false);
        if (cc_job_index < 0)
          return;
        this.SetIcon(this.cc_job_icon_button, unit, cc_job_index, true);
      }
    }

    private void SetIcon(SRPG_Button slot, UnitData unit, int job_data_index, bool is_cc_icon)
    {
      slot.gameObject.name = job_data_index.ToString();
      JobData job = unit.Jobs[job_data_index];
      bool isActivated = job.IsActivated;
      bool is_hilit = !job.IsActivated && (unit.CheckJobUnlock(job_data_index, false) || unit.CheckJobRankUpAllEquip(job_data_index, true));
      bool is_disabled = !job.IsActivated && !unit.CheckJobUnlockable(job_data_index);
      slot.gameObject.SetActive(true);
      this.SetAnimationParam(slot.GetComponent<Animator>(), job.IsActivated, isActivated, is_disabled, is_hilit);
      DataSource.Bind<JobData>(slot.gameObject, unit.Jobs[job_data_index], false);
      GameParameter.UpdateAll(slot.gameObject);
      if (this.mMode != UnitInventoryJobIcon.eViewMode.UNIT_VIEWER || !is_cc_icon)
        return;
      slot.gameObject.SetActive(false);
    }

    private void SetAnimationParam(Animator anim, bool is_activated, bool unlockable, bool is_disabled, bool is_hilit)
    {
      if ((UnityEngine.Object) anim == (UnityEngine.Object) null)
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
      this.BaseJobIconButton.GetComponent<Animator>().SetBool(UnitInventoryJobIcon.ANIM_PARAM_JOB_ICON_ON_BOOL, is_on);
    }

    public bool IsDisabledBaseJobIcon()
    {
      return this.BaseJobIconButton.GetComponent<Animator>().GetBool(UnitInventoryJobIcon.ANIM_PARAM_JOB_ICON_DISABLED_BOOL);
    }

    public enum eViewMode
    {
      NONE,
      UNIT_DETAIL,
      UNIT_VIEWER,
    }
  }
}
