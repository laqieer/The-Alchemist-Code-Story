// Decompiled with JetBrains decompiler
// Type: SRPG.UnitModelViewer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(100, "OnBack", FlowNode.PinTypes.Output, 100)]
  public class UnitModelViewer : MonoBehaviour, IFlowInterface
  {
    private readonly float TOOLTIP_POSITION_OFFSET_Y = 80f;
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
    private int wait_frame = 1;
    private Dictionary<int, int[]> mJobSetDatas = new Dictionary<int, int[]>();
    [SerializeField]
    private JobIconScrollListController mJobIconScrollListController;
    private List<UnitInventoryJobIcon> mUnitJobIconSetList;
    private ScrollClamped_JobIcons mScrollClampedJobIcons;
    private bool IsInitalized;
    private GameObject mPlayBackVoiceWindow;
    public static readonly string PLAYBACK_UNITVOICE_PREFAB_PATH = "UI/PlayBackUnitVoice";

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
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mScrollClampedJobIcons, (UnityEngine.Object) null))
        {
          this.mScrollClampedJobIcons = ((Component) this.mJobIconScrollListController).GetComponent<ScrollClamped_JobIcons>();
          this.mScrollClampedJobIcons.OnFrameOutItem = new ScrollClamped_JobIcons.FrameOutItem(this.RefreshJobIcon);
        }
        return this.mScrollClampedJobIcons;
      }
    }

    public void Activated(int pinID)
    {
    }

    private void Start() => this.Initalize();

    private void OnDestroy()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPlayBackVoiceWindow, (UnityEngine.Object) null))
        return;
      UnityEngine.Object.Destroy((UnityEngine.Object) this.mPlayBackVoiceWindow);
    }

    private void OnDisable() => this.mIsCreatedJobIconInstance = false;

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
        this.Refresh();
        this.mIsCreatedJobIconInstance = true;
      }
    }

    public void Initalize()
    {
      if (this.IsInitalized)
        return;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TouchArea, (UnityEngine.Object) null))
      {
        TouchControlArea component = this.TouchArea.GetComponent<TouchControlArea>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          this.mTouchControlArea = component;
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SkinButton, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.SkinButton.onClick).AddListener(new UnityAction((object) this, __methodptr(OnSkinSelectClick)));
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ReactionButton, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.ReactionButton.onClick).AddListener(new UnityAction((object) this, __methodptr(OnRectionClick)));
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.VoiceButton, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.VoiceButton.onClick).AddListener(new UnityAction((object) this, __methodptr(OnVoiceClick)));
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.VoiceUnlock, (UnityEngine.Object) null))
        this.VoiceUnlock.SetActive(true);
      this.IsInitalized = true;
      this.mJobIconScrollListController.Init();
      this.mJobIconScrollListController.CreateInstance();
    }

    public void ResetTouchControlArea()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mTouchControlArea, (UnityEngine.Object) null))
        return;
      this.mTouchControlArea.Reset();
    }

    public void OnJobIconClick(GameObject target)
    {
      UnitEnhanceV3.Instance.OnJobSlotClick(target);
      DataSource.Bind<UnitData>(this.JobNameObject, UnitEnhanceV3.Instance.CurrentUnit);
      GameParameter.UpdateAll(((Component) this).gameObject);
      this.UpdateJobSlotStates(true);
      this.ScrollClampedJobIcons.Focus(((Component) target.transform.parent).gameObject);
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
      UnitData dataOfClass = DataSource.FindDataOfClass<UnitData>(((Component) this).gameObject, (UnitData) null);
      if (dataOfClass == null)
        DebugUtility.LogError("3DViewerにUnitDataがBindされていません.");
      else if (!dataOfClass.CheckUnlockPlaybackVoice())
        this.ShowUnlockConditionsTooltip(((Component) this.VoiceButton).gameObject);
      else
        this.StartCoroutine(this.OpenPlayBackUnitVoice());
    }

    private void OnVoiceClose() => ((Behaviour) this.mTouchControlArea).enabled = true;

    private void UpdateJobSlotStates(bool immediate)
    {
      UnitInventoryJobIcon[] componentsInChildren = ((Component) this.ScrollClampedJobIcons).GetComponentsInChildren<UnitInventoryJobIcon>();
      for (int index = 0; index < componentsInChildren.Length; ++index)
      {
        bool is_on = ((UnityEngine.Object) componentsInChildren[index].BaseJobIconButton).name == UnitEnhanceV3.Instance.CurrentUnit.JobIndex.ToString();
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
      int key = this.ClampJobIconIndex(job_index);
      bool is_avtive_job = UnitEnhanceV3.Instance.CurrentUnit.JobIndex == key;
      target.GetComponent<UnitInventoryJobIcon>().SetParam(UnitEnhanceV3.Instance.CurrentUnit, this.mJobSetDatas[key], is_avtive_job, UnitInventoryJobIcon.eViewMode.UNIT_VIEWER);
      this.UpdateJobSlotStates(true);
    }

    public void Refresh(bool is_hide = false)
    {
      UnitData unit = UnitEnhanceV3.Instance.CurrentUnit;
      DataSource.Bind<UnitData>(((Component) this).gameObject, unit);
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
        ((Component) this.mJobIconScrollListController.Items[index].job_icon).gameObject.SetActive(false);
      if (this.mJobSetDatas.Count <= 2)
      {
        for (int index = 0; index < this.mJobSetDatas.Count; ++index)
        {
          ((Component) this.mJobIconScrollListController.Items[index].job_icon).gameObject.SetActive(true);
          this.RefreshJobIcon(((Component) this.mJobIconScrollListController.Items[index].job_icon).gameObject, index);
        }
      }
      else
      {
        for (int index = 0; index < this.mJobIconScrollListController.Items.Count; ++index)
        {
          ((Component) this.mJobIconScrollListController.Items[index].job_icon).gameObject.SetActive(true);
          int job_index = int.Parse(((UnityEngine.Object) this.mJobIconScrollListController.Items[index].job_icon).name);
          this.RefreshJobIcon(((Component) this.mJobIconScrollListController.Items[index].job_icon).gameObject, job_index);
        }
      }
      this.mJobIconScrollListController.Repotision();
      List<GameObject> objects = new List<GameObject>();
      for (int index = 0; index < this.mJobIconScrollListController.Items.Count; ++index)
      {
        if (((UnityEngine.Object) this.mJobIconScrollListController.Items[index].job_icon.BaseJobIconButton).name == UnitEnhanceV3.Instance.CurrentUnit.JobIndex.ToString())
          objects.Add(((Component) this.mJobIconScrollListController.Items[index].job_icon).gameObject);
      }
      if (objects.Count <= 0)
      {
        for (int index = 0; index < this.mJobIconScrollListController.Items.Count; ++index)
        {
          if (this.mJobIconScrollListController.Items[index].job_icon.CcJobButtonList.FindIndex((Predicate<SRPG_Button>) (_btn => ((UnityEngine.Object) _btn).name == UnitEnhanceV3.Instance.CurrentUnit.JobIndex.ToString())) >= 0)
            objects.Add(((Component) this.mJobIconScrollListController.Items[index].job_icon).gameObject);
        }
      }
      GameObject gameObject = this.ScrollClampedJobIcons.Focus(objects, true, is_hide);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
      {
        UnitInventoryJobIcon component = gameObject.GetComponent<UnitInventoryJobIcon>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          UnitEnhanceV3.Instance.OnJobSlotClick(((Component) component.BaseJobIconButton).gameObject);
      }
      this.mJobIconScrollListController.Step();
      DataSource.Bind<UnitData>(this.JobNameObject, unit);
      GameParameter.UpdateAll(((Component) this).gameObject);
      this.UpdateJobSlotStates(true);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SkinButton, (UnityEngine.Object) null))
        ((Selectable) this.SkinButton).interactable = unit.IsSkinUnlocked();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.VoiceButton, (UnityEngine.Object) null) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.VoiceUnlock, (UnityEngine.Object) null))
        return;
      this.VoiceUnlock.SetActive(!unit.CheckUnlockPlaybackVoice());
    }

    [DebuggerHidden]
    private IEnumerator OpenPlayBackUnitVoice()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitModelViewer.\u003COpenPlayBackUnitVoice\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    private void ShowUnlockConditionsTooltip(GameObject _target_obj)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.Preafab_UnlockConditionsTooltip, (UnityEngine.Object) null))
        return;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mUnlockConditionsTooltip, (UnityEngine.Object) null))
        this.mUnlockConditionsTooltip = UnityEngine.Object.Instantiate<Tooltip>(this.Preafab_UnlockConditionsTooltip);
      else
        this.mUnlockConditionsTooltip.ResetPosition();
      RectTransform component = _target_obj.GetComponent<RectTransform>();
      Tooltip.SetTooltipPosition(component, new Vector2()
      {
        x = 0.0f,
        y = component.sizeDelta.y / 2f + this.TOOLTIP_POSITION_OFFSET_Y
      });
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mUnlockConditionsTooltip.TooltipText, (UnityEngine.Object) null))
        return;
      this.mUnlockConditionsTooltip.TooltipText.text = LocalizedText.Get("sys.UNLOCK_CONDITIONS_PLAYBACK_FUNCTION");
    }

    public delegate void ChangeJobSlotEvent(int index, GameObject target);

    public delegate void SkinSelectEvent(SRPG_Button button);

    public delegate void PlayReaction();
  }
}
