// Decompiled with JetBrains decompiler
// Type: SRPG.UnitModelViewer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(100, "OnBack", FlowNode.PinTypes.Output, 100)]
  public class UnitModelViewer : MonoBehaviour, IFlowInterface
  {
    public static readonly string PLAYBACK_UNITVOICE_PREFAB_PATH = "UI/PlayBackUnitVoice";
    private readonly float TOOLTIP_POSITION_OFFSET_Y = 80f;
    private int wait_frame = 1;
    private Dictionary<int, int[]> mJobSetDatas = new Dictionary<int, int[]>();
    [SerializeField]
    private RectTransform JobIconParent;
    [SerializeField]
    private GameObject JobNameObject;
    [SerializeField]
    private GameObject TouchArea;
    [SerializeField]
    private SRPG_Button SkinButton;
    [SerializeField]
    private SRPG_Button ReactionButton;
    [SerializeField]
    private Button VoiceButton;
    [SerializeField]
    private GameObject VoiceUnlock;
    [SerializeField]
    private Tooltip Preafab_UnlockConditionsTooltip;
    public UnitModelViewer.ChangeJobSlotEvent OnChangeJobSlot;
    public UnitModelViewer.SkinSelectEvent OnSkinSelect;
    public UnitModelViewer.PlayReaction OnPlayReaction;
    private TouchControlArea mTouchControlArea;
    private Tooltip mUnlockConditionsTooltip;
    private bool mIsCreatedJobIconInstance;
    [SerializeField]
    private JobIconScrollListController mJobIconScrollListController;
    private List<UnitInventoryJobIcon> mUnitJobIconSetList;
    private ScrollClamped_JobIcons mScrollClampedJobIcons;
    private bool IsInitalized;
    private GameObject mPlayBackVoiceWindow;

    private List<UnitInventoryJobIcon> UnitJobIconSetList
    {
      get
      {
        if (this.mUnitJobIconSetList == null)
        {
          this.mUnitJobIconSetList = new List<UnitInventoryJobIcon>();
          for (int index = 0; index < this.mJobIconScrollListController.Items.Count; ++index)
            this.mUnitJobIconSetList.Add(this.mJobIconScrollListController.Items[index].job_icon);
        }
        return this.mUnitJobIconSetList;
      }
    }

    private ScrollClamped_JobIcons ScrollClampedJobIcons
    {
      get
      {
        if ((UnityEngine.Object) this.mScrollClampedJobIcons == (UnityEngine.Object) null)
        {
          this.mScrollClampedJobIcons = this.mJobIconScrollListController.GetComponent<ScrollClamped_JobIcons>();
          this.mScrollClampedJobIcons.OnFrameOutItem = new ScrollClamped_JobIcons.FrameOutItem(this.RefreshJobIcon);
        }
        return this.mScrollClampedJobIcons;
      }
    }

    public void Activated(int pinID)
    {
    }

    private void Start()
    {
      this.Initalize();
    }

    private void OnDestroy()
    {
      if (!((UnityEngine.Object) this.mPlayBackVoiceWindow != (UnityEngine.Object) null))
        return;
      UnityEngine.Object.Destroy((UnityEngine.Object) this.mPlayBackVoiceWindow);
    }

    private void OnDisable()
    {
      this.mIsCreatedJobIconInstance = false;
    }

    private void Update()
    {
      if (this.mIsCreatedJobIconInstance || this.UnitJobIconSetList.Count <= 0)
        return;
      if (this.wait_frame > 0)
      {
        --this.wait_frame;
      }
      else
      {
        this.Refresh(false);
        this.mIsCreatedJobIconInstance = true;
      }
    }

    public void Initalize()
    {
      if (this.IsInitalized)
        return;
      if ((UnityEngine.Object) this.TouchArea != (UnityEngine.Object) null)
      {
        TouchControlArea component = this.TouchArea.GetComponent<TouchControlArea>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null)
          this.mTouchControlArea = component;
      }
      if ((UnityEngine.Object) this.SkinButton != (UnityEngine.Object) null)
        this.SkinButton.onClick.AddListener(new UnityAction(this.OnSkinSelectClick));
      if ((UnityEngine.Object) this.ReactionButton != (UnityEngine.Object) null)
        this.ReactionButton.onClick.AddListener(new UnityAction(this.OnRectionClick));
      if ((UnityEngine.Object) this.VoiceButton != (UnityEngine.Object) null)
        this.VoiceButton.onClick.AddListener(new UnityAction(this.OnVoiceClick));
      if ((UnityEngine.Object) this.VoiceUnlock != (UnityEngine.Object) null)
        this.VoiceUnlock.SetActive(true);
      this.IsInitalized = true;
      this.mJobIconScrollListController.Init();
      this.mJobIconScrollListController.CreateInstance();
    }

    public void ResetTouchControlArea()
    {
      if (!((UnityEngine.Object) this.mTouchControlArea != (UnityEngine.Object) null))
        return;
      this.mTouchControlArea.Reset();
    }

    public void OnJobIconClick(GameObject target)
    {
      UnitEnhanceV3.Instance.OnJobSlotClick(target);
      DataSource.Bind<UnitData>(this.JobNameObject, UnitEnhanceV3.Instance.CurrentUnit, false);
      GameParameter.UpdateAll(this.gameObject);
      this.UpdateJobSlotStates(true);
      this.ScrollClampedJobIcons.Focus(target.transform.parent.gameObject, false, false, 0.0f);
    }

    private void OnSkinSelectClick()
    {
      if (this.OnSkinSelect == null)
        return;
      this.OnSkinSelect(this.SkinButton);
    }

    private void OnRectionClick()
    {
      if (this.OnPlayReaction == null)
        return;
      this.OnPlayReaction();
    }

    private void OnVoiceClick()
    {
      UnitData dataOfClass = DataSource.FindDataOfClass<UnitData>(this.gameObject, (UnitData) null);
      if (dataOfClass == null)
        DebugUtility.LogError("3DViewerにUnitDataがBindされていません.");
      else if (!dataOfClass.CheckUnlockPlaybackVoice())
        this.ShowUnlockConditionsTooltip(this.VoiceButton.gameObject);
      else
        this.StartCoroutine(this.OpenPlayBackUnitVoice());
    }

    private void OnVoiceClose()
    {
      this.mTouchControlArea.enabled = true;
    }

    private void UpdateJobSlotStates(bool immediate)
    {
      UnitInventoryJobIcon[] componentsInChildren = this.ScrollClampedJobIcons.GetComponentsInChildren<UnitInventoryJobIcon>();
      for (int index = 0; index < componentsInChildren.Length; ++index)
      {
        bool is_on = componentsInChildren[index].BaseJobIconButton.name == UnitEnhanceV3.Instance.CurrentUnit.JobIndex.ToString();
        componentsInChildren[index].SetSelectIconAnim(is_on);
      }
    }

    private int ClampJobIconIndex(int index)
    {
      if (index >= 0)
      {
        index %= this.mJobSetDatas.Count;
      }
      else
      {
        index = Mathf.Abs(index);
        index %= this.mJobSetDatas.Count;
        index = this.mJobSetDatas.Count - index;
        index %= this.mJobSetDatas.Count;
      }
      return index;
    }

    public void RefreshJobIcon(GameObject target, int job_index)
    {
      int index = this.ClampJobIconIndex(job_index);
      bool is_avtive_job = UnitEnhanceV3.Instance.CurrentUnit.JobIndex == index;
      target.GetComponent<UnitInventoryJobIcon>().SetParam(UnitEnhanceV3.Instance.CurrentUnit, this.mJobSetDatas[index][0], this.mJobSetDatas[index][1], is_avtive_job, UnitInventoryJobIcon.eViewMode.UNIT_VIEWER);
      this.UpdateJobSlotStates(true);
    }

    public void Refresh(bool is_hide = false)
    {
      UnitData unit = UnitEnhanceV3.Instance.CurrentUnit;
      DataSource.Bind<UnitData>(this.gameObject, unit, false);
      for (int index = 0; index < this.UnitJobIconSetList.Count; ++index)
        this.UnitJobIconSetList[index].ResetParam();
      JobSetParam[] cc_jobset_array = MonoSingleton<GameManager>.Instance.GetClassChangeJobSetParam(unit.UnitID);
      if (cc_jobset_array == null)
        cc_jobset_array = new JobSetParam[0];
      this.mJobSetDatas.Clear();
      int key = 0;
      for (int i = 0; i < unit.Jobs.Length; ++i)
      {
        int index = Array.FindIndex<JobSetParam>(cc_jobset_array, (Predicate<JobSetParam>) (jobset => jobset.job == unit.Jobs[i].JobID));
        if (index >= 0)
        {
          JobSetParam base_jobset_param = MonoSingleton<GameManager>.Instance.GetJobSetParam(cc_jobset_array[index].jobchange);
          if (Array.FindIndex<JobData>(unit.Jobs, (Predicate<JobData>) (job => job.JobID == base_jobset_param.job)) < 0)
          {
            int[] numArray = new int[2]{ i, -1 };
            this.mJobSetDatas.Add(key, numArray);
            ++key;
          }
        }
        else
        {
          int num = -1;
          for (int j = 0; j < cc_jobset_array.Length; ++j)
          {
            if (MonoSingleton<GameManager>.Instance.GetJobSetParam(cc_jobset_array[j].jobchange).job == unit.Jobs[i].JobID)
            {
              num = Array.FindIndex<JobData>(unit.Jobs, (Predicate<JobData>) (job => job.JobID == cc_jobset_array[j].job));
              break;
            }
          }
          int[] numArray = new int[2]{ i, num };
          this.mJobSetDatas.Add(key, numArray);
          ++key;
        }
      }
      for (int index = 0; index < this.mJobIconScrollListController.Items.Count; ++index)
        this.mJobIconScrollListController.Items[index].job_icon.gameObject.SetActive(false);
      if (this.mJobSetDatas.Count <= 2)
      {
        for (int job_index = 0; job_index < this.mJobSetDatas.Count; ++job_index)
        {
          this.mJobIconScrollListController.Items[job_index].job_icon.gameObject.SetActive(true);
          this.RefreshJobIcon(this.mJobIconScrollListController.Items[job_index].job_icon.gameObject, job_index);
        }
      }
      else
      {
        for (int index = 0; index < this.mJobIconScrollListController.Items.Count; ++index)
        {
          this.mJobIconScrollListController.Items[index].job_icon.gameObject.SetActive(true);
          int job_index = int.Parse(this.mJobIconScrollListController.Items[index].job_icon.name);
          this.RefreshJobIcon(this.mJobIconScrollListController.Items[index].job_icon.gameObject, job_index);
        }
      }
      this.mJobIconScrollListController.Repotision();
      List<GameObject> objects = new List<GameObject>();
      for (int index = 0; index < this.mJobIconScrollListController.Items.Count; ++index)
      {
        if (this.mJobIconScrollListController.Items[index].job_icon.BaseJobIconButton.name == UnitEnhanceV3.Instance.CurrentUnit.JobIndex.ToString())
          objects.Add(this.mJobIconScrollListController.Items[index].job_icon.gameObject);
      }
      if (objects.Count <= 0)
      {
        for (int index = 0; index < this.mJobIconScrollListController.Items.Count; ++index)
        {
          if (this.mJobIconScrollListController.Items[index].job_icon.CcJobButton.name == UnitEnhanceV3.Instance.CurrentUnit.JobIndex.ToString())
            objects.Add(this.mJobIconScrollListController.Items[index].job_icon.gameObject);
        }
      }
      GameObject gameObject = this.ScrollClampedJobIcons.Focus(objects, true, is_hide, 0.0f);
      if ((UnityEngine.Object) gameObject != (UnityEngine.Object) null)
      {
        UnitInventoryJobIcon component = gameObject.GetComponent<UnitInventoryJobIcon>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null)
          UnitEnhanceV3.Instance.OnJobSlotClick(component.BaseJobIconButton.gameObject);
      }
      this.mJobIconScrollListController.Step();
      DataSource.Bind<UnitData>(this.JobNameObject, unit, false);
      GameParameter.UpdateAll(this.gameObject);
      this.UpdateJobSlotStates(true);
      if ((UnityEngine.Object) this.SkinButton != (UnityEngine.Object) null)
        this.SkinButton.interactable = unit.IsSkinUnlocked();
      if (!((UnityEngine.Object) this.VoiceButton != (UnityEngine.Object) null) || !((UnityEngine.Object) this.VoiceUnlock != (UnityEngine.Object) null))
        return;
      this.VoiceUnlock.SetActive(!unit.CheckUnlockPlaybackVoice());
    }

    [DebuggerHidden]
    private IEnumerator OpenPlayBackUnitVoice()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitModelViewer.\u003COpenPlayBackUnitVoice\u003Ec__Iterator0() { \u0024this = this };
    }

    private void ShowUnlockConditionsTooltip(GameObject _target_obj)
    {
      if ((UnityEngine.Object) this.Preafab_UnlockConditionsTooltip == (UnityEngine.Object) null)
        return;
      if ((UnityEngine.Object) this.mUnlockConditionsTooltip == (UnityEngine.Object) null)
        this.mUnlockConditionsTooltip = UnityEngine.Object.Instantiate<Tooltip>(this.Preafab_UnlockConditionsTooltip);
      else
        this.mUnlockConditionsTooltip.ResetPosition();
      RectTransform component = _target_obj.GetComponent<RectTransform>();
      Tooltip.SetTooltipPosition(component, new Vector2()
      {
        x = 0.0f,
        y = component.sizeDelta.y / 2f + this.TOOLTIP_POSITION_OFFSET_Y
      });
      if (!((UnityEngine.Object) this.mUnlockConditionsTooltip.TooltipText != (UnityEngine.Object) null))
        return;
      this.mUnlockConditionsTooltip.TooltipText.text = LocalizedText.Get("sys.UNLOCK_CONDITIONS_PLAYBACK_FUNCTION");
    }

    public delegate void ChangeJobSlotEvent(int index, GameObject target);

    public delegate void SkinSelectEvent(SRPG_Button button);

    public delegate void PlayReaction();
  }
}
