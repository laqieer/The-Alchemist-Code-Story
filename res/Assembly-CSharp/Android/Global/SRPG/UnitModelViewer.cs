// Decompiled with JetBrains decompiler
// Type: SRPG.UnitModelViewer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
    private List<AnimatedToggle> mJobIcons = new List<AnimatedToggle>();
    private List<AnimatedToggle> mCCIcons = new List<AnimatedToggle>();
    private List<AnimatedToggle> mJobSlots = new List<AnimatedToggle>();
    public string JobIconUnlockBool = "unlocked";
    [SerializeField]
    private RectTransform JobIconParent;
    [SerializeField]
    private AnimatedToggle JobIconTemplate_Normal;
    [SerializeField]
    private AnimatedToggle JobIconTemplate_CC;
    [SerializeField]
    private GameObject JobNameObject;
    [SerializeField]
    private GameObject TouchArea;
    [SerializeField]
    private Button BackButton;
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
    private bool IsInitalized;
    private GameObject mPlayBackVoiceWindow;

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
      if ((UnityEngine.Object) this.BackButton != (UnityEngine.Object) null)
        this.BackButton.onClick.AddListener(new UnityAction(this.OnBack));
      if ((UnityEngine.Object) this.SkinButton != (UnityEngine.Object) null)
        this.SkinButton.onClick.AddListener(new UnityAction(this.OnSkinSelectClick));
      if ((UnityEngine.Object) this.ReactionButton != (UnityEngine.Object) null)
        this.ReactionButton.onClick.AddListener(new UnityAction(this.OnRectionClick));
      if ((UnityEngine.Object) this.VoiceButton != (UnityEngine.Object) null)
        this.VoiceButton.onClick.AddListener(new UnityAction(this.OnVoiceClick));
      if ((UnityEngine.Object) this.VoiceUnlock != (UnityEngine.Object) null)
        this.VoiceUnlock.SetActive(true);
      if ((UnityEngine.Object) this.JobIconTemplate_Normal != (UnityEngine.Object) null)
        this.JobIconTemplate_Normal.gameObject.SetActive(false);
      if ((UnityEngine.Object) this.JobIconTemplate_CC != (UnityEngine.Object) null)
        this.JobIconTemplate_CC.gameObject.SetActive(false);
      for (int index = 0; index < 4; ++index)
      {
        AnimatedToggle animatedToggle1 = UnityEngine.Object.Instantiate<AnimatedToggle>(this.JobIconTemplate_Normal);
        animatedToggle1.gameObject.SetActive(false);
        animatedToggle1.transform.SetParent((Transform) this.JobIconParent, false);
        animatedToggle1.OnClick = new AnimatedToggle.ClickEvent(this.OnJobIconClick);
        this.mJobIcons.Add(animatedToggle1);
        AnimatedToggle animatedToggle2 = UnityEngine.Object.Instantiate<AnimatedToggle>(this.JobIconTemplate_CC);
        animatedToggle2.gameObject.SetActive(false);
        animatedToggle2.transform.SetParent((Transform) this.JobIconParent, false);
        animatedToggle2.OnClick = new AnimatedToggle.ClickEvent(this.OnJobIconClick);
        this.mCCIcons.Add(animatedToggle2);
      }
      this.IsInitalized = true;
    }

    private void OnJobIconClick(AnimatedToggle toggle)
    {
      if (this.OnChangeJobSlot == null)
        return;
      this.OnChangeJobSlot(this.mJobSlots.IndexOf(toggle));
      GameParameter.UpdateAll(this.JobNameObject);
    }

    private void OnBack()
    {
      if ((UnityEngine.Object) this.mTouchControlArea != (UnityEngine.Object) null)
        this.mTouchControlArea.Reset();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
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

    public void Refresh(UnitData unit)
    {
      if (unit == null)
        return;
      DataSource.Bind<UnitData>(this.gameObject, unit);
      int index1 = 0;
      int index2 = 0;
      this.mJobSlots.Clear();
      Toggle.ToggleEvent toggleEvent = new Toggle.ToggleEvent();
      for (int jobNo = 0; jobNo < unit.Jobs.Length; ++jobNo)
      {
        JobData job = unit.Jobs[jobNo];
        if (job != null && job.Param != null)
        {
          if (unit.IsJobAvailable(jobNo))
          {
            this.mJobSlots.Add(this.mJobIcons[index1]);
            ++index1;
          }
          else
          {
            this.mJobSlots.Add(this.mCCIcons[index2]);
            ++index2;
          }
          AnimatedToggle mJobIcon = this.mJobIcons[this.mJobSlots.Count - 1];
          DataSource.Bind<JobData>(mJobIcon.gameObject, unit.Jobs[jobNo]);
          mJobIcon.gameObject.SetActive(true);
          mJobIcon.interactable = unit.CheckJobUnlockable(jobNo);
          Toggle.ToggleEvent onValueChanged = mJobIcon.onValueChanged;
          mJobIcon.onValueChanged = toggleEvent;
          mJobIcon.isOn = jobNo == unit.JobIndex;
          mJobIcon.onValueChanged = onValueChanged;
          mJobIcon.GetComponent<Animator>().SetBool(this.JobIconUnlockBool, job.IsActivated);
        }
      }
      for (int index3 = index2; index3 < this.mCCIcons.Count; ++index3)
        this.mCCIcons[index3].gameObject.SetActive(false);
      for (int index3 = index1; index3 < this.mJobIcons.Count; ++index3)
        this.mJobIcons[index3].gameObject.SetActive(false);
      if ((UnityEngine.Object) this.SkinButton != (UnityEngine.Object) null)
        this.SkinButton.interactable = unit.IsSkinUnlocked();
      DataSource.Bind<UnitData>(this.JobNameObject, unit);
      bool flag = true;
      for (int index3 = 0; index3 < this.mJobSlots.Count; ++index3)
      {
        this.mJobSlots[index3].isOn = unit.JobIndex == index3;
        Animator component = this.mJobSlots[index3].GetComponent<Animator>();
        int num = 0;
        do
        {
          component.Update(!flag ? 0.0f : 1f);
          ++num;
        }
        while (component.IsInTransition(0) && num < 10);
      }
      if ((UnityEngine.Object) this.VoiceButton != (UnityEngine.Object) null && (UnityEngine.Object) this.VoiceUnlock != (UnityEngine.Object) null)
        this.VoiceUnlock.SetActive(!unit.CheckUnlockPlaybackVoice());
      GameParameter.UpdateAll(this.gameObject);
    }

    [DebuggerHidden]
    private IEnumerator OpenPlayBackUnitVoice()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitModelViewer.\u003COpenPlayBackUnitVoice\u003Ec__Iterator14B() { \u003C\u003Ef__this = this };
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

    public delegate void ChangeJobSlotEvent(int index);

    public delegate void SkinSelectEvent(SRPG_Button button);

    public delegate void PlayReaction();
  }
}
