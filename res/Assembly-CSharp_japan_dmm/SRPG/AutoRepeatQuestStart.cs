// Decompiled with JetBrains decompiler
// Type: SRPG.AutoRepeatQuestStart
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(10, "初期化", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(100, "初期化完了", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(11, "出撃をタップ", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(101, "周回リクエスト", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "AP回復", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(12, "表示更新", FlowNode.PinTypes.Input, 12)]
  [FlowNode.Pin(13, "時間計測バトル", FlowNode.PinTypes.Input, 13)]
  [FlowNode.Pin(103, "Raycastを有効化", FlowNode.PinTypes.Output, 103)]
  public class AutoRepeatQuestStart : MonoBehaviour, IFlowInterface
  {
    private const int PIN_IN_INIT = 10;
    private const int PIN_OT_INIT = 100;
    private const int PIN_IN_CLICK_START = 11;
    private const int PIN_OT_REQUEST = 101;
    private const int PIN_OT_HEAL_AP = 102;
    private const int PIN_IN_REFRESH = 12;
    private const int PIN_IN_START_QUEST = 13;
    private const int PIN_OT_CANVAS_BLOCK_RAYCAST_ON = 103;
    [SerializeField]
    private UnityEngine.UI.Text QuestClearTime;
    [SerializeField]
    private UnityEngine.UI.Text SelectAutoLapNum;
    [SerializeField]
    private Toggle UseNotification;
    [SerializeField]
    private Button AutoLapNumAdd;
    [SerializeField]
    private Button AutoLapNumSub;
    [SerializeField]
    private Toggle UseAPHealItem;
    [SerializeField]
    private ToggleGroup Group;
    [SerializeField]
    private Toggle UseSelectLapNum;
    [SerializeField]
    private Toggle UseSelectBoxFull;
    [SerializeField]
    private Toggle mPushNotifyToggle;
    [SerializeField]
    private UnityEngine.UI.Text mBoxSizeText;
    [SerializeField]
    private Button mStartButton;
    [SerializeField]
    private UnityEngine.UI.Text mMeasureText;
    [SerializeField]
    private UnityEngine.UI.Text mDisableReasonText;
    [SerializeField]
    private GameObject mDisableMask;
    [SerializeField]
    private GameObject mMeasureButton;
    [SerializeField]
    private GameObject mReMeasureButton;
    [SerializeField]
    private GameObject mFooterUI_Enable;
    [SerializeField]
    private GameObject mFooterUI_Disable;
    private int m_LapNum = 1;
    private int m_MaxLapNum = 99;
    private bool m_SelectBoxFull;
    private bool m_UseLocalNotification;
    private static AutoRepeatQuestStart mInstance;

    public int LapNum => this.m_LapNum;

    public bool IsUseAPHealItem
    {
      get
      {
        return UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UseAPHealItem, (UnityEngine.Object) null) && this.UseAPHealItem.isOn;
      }
    }

    public bool IsUseNotification => this.m_UseLocalNotification;

    public bool IsSelectBoxFull => this.m_SelectBoxFull;

    public static AutoRepeatQuestStart Instance => AutoRepeatQuestStart.mInstance;

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 10:
          this.Init();
          break;
        case 11:
          this.OnClickStart();
          break;
        case 12:
          this.Refresh();
          break;
        case 13:
          this.StartQuest();
          break;
      }
    }

    private void Awake()
    {
      AutoRepeatQuestStart.mInstance = this;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AutoLapNumAdd, (UnityEngine.Object) null))
      {
        ButtonHoldObserver buttonHoldObserver = ((Component) this.AutoLapNumAdd).gameObject.AddComponent<ButtonHoldObserver>();
        buttonHoldObserver.OnHoldStart = (ButtonHoldObserver.DelegateOnHoldEvent) (() => { });
        buttonHoldObserver.OnHoldUpdate = (ButtonHoldObserver.DelegateOnHoldEvent) (() => this.OnLapNumAdd(this.AutoLapNumAdd, false));
        buttonHoldObserver.OnHoldEnd = (ButtonHoldObserver.DelegateOnHoldEvent) (() => this.OnLapNumAdd(this.AutoLapNumAdd, true));
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AutoLapNumSub, (UnityEngine.Object) null))
      {
        ButtonHoldObserver buttonHoldObserver = ((Component) this.AutoLapNumSub).gameObject.AddComponent<ButtonHoldObserver>();
        buttonHoldObserver.OnHoldStart = (ButtonHoldObserver.DelegateOnHoldEvent) (() => { });
        buttonHoldObserver.OnHoldUpdate = (ButtonHoldObserver.DelegateOnHoldEvent) (() => this.OnLapNumSub(this.AutoLapNumSub, false));
        buttonHoldObserver.OnHoldEnd = (ButtonHoldObserver.DelegateOnHoldEvent) (() => this.OnLapNumSub(this.AutoLapNumSub, true));
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UseSelectBoxFull, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent<bool>) this.UseSelectBoxFull.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(OnSelectToggle)));
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UseSelectLapNum, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent<bool>) this.UseSelectLapNum.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(OnSelectToggle)));
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPushNotifyToggle, (UnityEngine.Object) null))
        return;
      // ISSUE: method pointer
      ((UnityEvent<bool>) this.mPushNotifyToggle.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(OnSelectTogglePushSetting)));
    }

    private void OnDestroy() => AutoRepeatQuestStart.mInstance = (AutoRepeatQuestStart) null;

    private void Init()
    {
      QuestParam quest = (QuestParam) null;
      if (!string.IsNullOrEmpty(GlobalVars.SelectedQuestID))
        quest = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
      if (quest == null)
      {
        DebugUtility.LogError("QuestParamが設定されていません.");
      }
      else
      {
        bool active = UnityEngine.Object.op_Inequality((UnityEngine.Object) PartyWindow2.Instance, (UnityEngine.Object) null);
        GameUtility.SetGameObjectActive(this.mMeasureButton, active);
        GameUtility.SetGameObjectActive(this.mReMeasureButton, active);
        this.RefreshUI_Enable_or_Disable(quest);
        int autoRepeatCountMax = MonoSingleton<GameManager>.Instance.MasterParam.FixParam.AutoRepeatCountMax;
        if (quest != null && autoRepeatCountMax > 0)
          this.m_MaxLapNum = Mathf.Min(this.m_MaxLapNum, QuestParam.GetRaidTicketCount_LimitMax(quest, autoRepeatCountMax));
        if (quest != null)
        {
          int num = quest.RequiredApWithPlayerLv(MonoSingleton<GameManager>.Instance.Player.Lv);
          this.m_LapNum = num > 0 ? Math.Max(1, Math.Min(this.m_MaxLapNum, MonoSingleton<GameManager>.Instance.Player.Stamina / num)) : this.m_MaxLapNum;
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.QuestClearTime, (UnityEngine.Object) null))
          this.QuestClearTime.text = quest.GetClearBestTime();
        this.RefreshSelectLapNumUI();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UseSelectLapNum, (UnityEngine.Object) null))
          GameUtility.SetToggle(this.UseSelectLapNum, true);
        this.Refresh();
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      }
    }

    private void Refresh()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBoxSizeText, (UnityEngine.Object) null))
        return;
      this.mBoxSizeText.text = MonoSingleton<GameManager>.Instance.Player.AutoRepeatQuestBox.Size.ToString();
    }

    private void StartQuest()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) PartyWindow2.Instance, (UnityEngine.Object) null))
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 103);
      else if (PartyWindow2.Instance.CurrentSupport != null)
      {
        UIUtility.ConfirmBox(LocalizedText.Get("sys.AUTO_REPEAT_QUEST_CAUTION_SELECT_SUPPORT"), (UIUtility.DialogResultEvent) (dialog =>
        {
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) PartyWindow2.Instance, (UnityEngine.Object) null))
            return;
          PartyWindow2.Instance.PrepareStartQuest();
        }), (UIUtility.DialogResultEvent) null);
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 103);
      }
      else
      {
        PartyWindow2.Instance.PrepareStartQuest();
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 103);
      }
    }

    private void OnLapNumAdd(Button button, bool is_refresh_btn_interactable)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) button, (UnityEngine.Object) null) || !((Selectable) button).interactable)
        return;
      this.m_LapNum = Mathf.Min(this.m_MaxLapNum, this.m_LapNum + 1);
      this.RefreshSelectLapNumUI(is_refresh_btn_interactable);
    }

    private void OnLapNumSub(Button button, bool is_refresh_btn_interactable)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) button, (UnityEngine.Object) null) || !((Selectable) button).interactable)
        return;
      this.m_LapNum = Mathf.Max(1, this.m_LapNum - 1);
      this.RefreshSelectLapNumUI(is_refresh_btn_interactable);
    }

    private void RefreshSelectLapNumUI(bool is_refresh_btn_interactable = true)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SelectAutoLapNum, (UnityEngine.Object) null))
        this.SelectAutoLapNum.text = this.m_LapNum.ToString();
      if (!is_refresh_btn_interactable)
        return;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AutoLapNumAdd, (UnityEngine.Object) null))
        ((Selectable) this.AutoLapNumAdd).interactable = this.m_LapNum < this.m_MaxLapNum;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AutoLapNumSub, (UnityEngine.Object) null))
        return;
      ((Selectable) this.AutoLapNumSub).interactable = this.m_LapNum > 1;
    }

    private void RefreshUI_Enable_or_Disable(QuestParam quest)
    {
      string disableReason = this.GetDisableReason(quest);
      bool flag1 = string.IsNullOrEmpty(disableReason);
      bool flag2 = quest.best_clear_time > 0;
      if (flag1)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mStartButton, (UnityEngine.Object) null))
          ((Selectable) this.mStartButton).interactable = true;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mDisableMask, (UnityEngine.Object) null))
          this.mDisableMask.SetActive(false);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mFooterUI_Enable, (UnityEngine.Object) null))
          this.mFooterUI_Enable.SetActive(true);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mFooterUI_Disable, (UnityEngine.Object) null))
          this.mFooterUI_Disable.SetActive(false);
      }
      else
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mStartButton, (UnityEngine.Object) null))
          ((Selectable) this.mStartButton).interactable = false;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mDisableMask, (UnityEngine.Object) null))
          this.mDisableMask.SetActive(true);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mDisableReasonText, (UnityEngine.Object) null))
          this.mDisableReasonText.text = disableReason;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mFooterUI_Enable, (UnityEngine.Object) null))
          this.mFooterUI_Enable.SetActive(false);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mFooterUI_Disable, (UnityEngine.Object) null))
          this.mFooterUI_Disable.SetActive(true);
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mMeasureText, (UnityEngine.Object) null))
        return;
      this.mMeasureText.text = LocalizedText.Get(!flag2 ? "sys.AUTO_REPEAT_QUEST_MEASURE_TEXT" : "sys.AUTO_REPEAT_QUEST_REMEASURE_TEXT");
    }

    private string GetDisableReason(QuestParam quest)
    {
      StringBuilder stringBuilder = GameUtility.GetStringBuilder();
      if (!quest.IsMissionCompleteALL())
      {
        stringBuilder.Append(LocalizedText.Get("sys.AUTO_REPEAT_QUEST_CAUTION_FAILED_START"));
        stringBuilder.Append(LocalizedText.Get("sys.AUTO_REPEAT_QUEST_CAUTION_FAILED_MISSION_NOT_COMPLETE"));
      }
      if (quest.best_clear_time <= 0)
      {
        if (string.IsNullOrEmpty(stringBuilder.ToString()))
          stringBuilder.Append(LocalizedText.Get("sys.AUTO_REPEAT_QUEST_CAUTION_FAILED_START"));
        stringBuilder.Append(LocalizedText.Get("sys.AUTO_REPEAT_QUEST_CAUTION_FAILED_NO_BESTTIME"));
      }
      if (!string.IsNullOrEmpty(stringBuilder.ToString()))
        stringBuilder.Append(LocalizedText.Get("sys.AUTO_REPEAT_QUEST_CAUTION_FAILED_END"));
      return stringBuilder.ToString();
    }

    private void OnSelectToggle(bool value)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.Group, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.UseSelectLapNum, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.UseSelectBoxFull, (UnityEngine.Object) null))
        return;
      List<Toggle> list = this.Group.ActiveToggles().ToList<Toggle>();
      if (list.Count != 1)
        return;
      Toggle toggle = list[0];
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) toggle, (UnityEngine.Object) this.UseSelectBoxFull))
      {
        ((Selectable) this.AutoLapNumAdd).interactable = false;
        ((Selectable) this.AutoLapNumSub).interactable = false;
        this.m_LapNum = 0;
        this.m_SelectBoxFull = true;
      }
      else
      {
        if (!UnityEngine.Object.op_Equality((UnityEngine.Object) toggle, (UnityEngine.Object) this.UseSelectLapNum))
          return;
        this.m_LapNum = int.Parse(this.SelectAutoLapNum.text);
        this.RefreshSelectLapNumUI();
        this.m_SelectBoxFull = false;
      }
    }

    private void OnSelectTogglePushSetting(bool value)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPushNotifyToggle, (UnityEngine.Object) null))
        return;
      this.m_UseLocalNotification = this.mPushNotifyToggle.isOn;
    }

    private void OnClickStart()
    {
      QuestParam questParam = (QuestParam) null;
      if (!string.IsNullOrEmpty(GlobalVars.SelectedQuestID))
        questParam = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
      if (questParam == null)
      {
        DebugUtility.LogError("QuestParamが設定されていません.");
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 103);
      }
      else
      {
        int pinID = 101;
        if (questParam.RequiredApWithPlayerLv(MonoSingleton<GameManager>.Instance.Player.Lv) > MonoSingleton<GameManager>.Instance.Player.Stamina && (!this.IsUseAPHealItem || !MonoSingleton<GameManager>.Instance.Player.IsHaveHealAPItems()))
          pinID = 102;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, pinID);
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 103);
      }
    }
  }
}
