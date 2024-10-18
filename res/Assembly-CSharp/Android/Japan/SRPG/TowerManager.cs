// Decompiled with JetBrains decompiler
// Type: SRPG.TowerManager
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
  [FlowNode.Pin(0, "初期化", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(7, "初期化（完了）", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(1, "更新", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(2, "ユニット回復 処理へ", FlowNode.PinTypes.Output, 3)]
  [FlowNode.Pin(3, "ユニット回復（幻晶石不足）", FlowNode.PinTypes.Output, 4)]
  [FlowNode.Pin(4, "一階から再挑戦 処理へ", FlowNode.PinTypes.Output, 5)]
  [FlowNode.Pin(5, "一階から再挑戦（幻晶石不足）", FlowNode.PinTypes.Output, 6)]
  [FlowNode.Pin(6, "一階から再挑戦（完了）", FlowNode.PinTypes.Output, 7)]
  [FlowNode.Pin(8, "現階層のミッションの進捗取得する？", FlowNode.PinTypes.Input, 8)]
  [FlowNode.Pin(9, "ミッションの進捗の取得が必要", FlowNode.PinTypes.Output, 9)]
  [FlowNode.Pin(10, "ミッションの進捗の取得は不要", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(11, "ミッションの進捗の取得した", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(12, "ミッションの進捗の取得した（完了）", FlowNode.PinTypes.Output, 12)]
  public class TowerManager : MonoBehaviour, IFlowInterface
  {
    private float mRefreshInterval = 1f;
    private const int PIN_ID_INITIALIZE = 0;
    private const int PIN_ID_REFRESH = 1;
    private const int PIN_ID_RECOVER_UNIT = 2;
    private const int PIN_ID_RECOVER_COIN_NOT_ENOUGH = 3;
    private const int PIN_ID_RESET_TOWER = 4;
    private const int PIN_ID_RESET_TOWER_COIN_NOT_ENOUGH = 5;
    private const int PIN_ID_RESET_TOWER_END = 6;
    private const int PIN_ID_INITIALIZE_END = 7;
    private const int PIN_ID_CHECK_REQUIRED_PROGRESS_REQUEST = 8;
    private const int PIN_ID_REQUIRED_PROGRESS_REQUEST = 9;
    private const int PIN_ID_UNREQUIRE_PROGRESS_REQUEST = 10;
    private const int PIN_ID_SET_REQUIRE_PROGRESS_RECIEVED = 11;
    private const int PIN_ID_SET_REQUIRE_PROGRESS_RECIEVED_END = 12;
    [HeaderBar("▼階層表示用のリスト")]
    [SerializeField]
    private TowerQuestList mTowerQuestList;
    [HeaderBar("▼階層の詳細表示用オブジェクト")]
    [SerializeField]
    private TowerQuestInfo mTowerQuestInfo;
    [HeaderBar("▼生存ユニット")]
    [SerializeField]
    private Text AliveUnits;
    [HeaderBar("▼無料ユニット回復までの時間表示用の親")]
    [SerializeField]
    private GameObject RecoverTimer;
    [SerializeField]
    private Text RecoverFreeTime;
    [SerializeField]
    private Text RecoverCost;
    [SerializeField]
    private Text RecoverCostFree;
    [SerializeField]
    private Button RecoverButton;
    [HeaderBar("▼無料ユニット回復までの時間表示用")]
    [SerializeField]
    private ImageArray TimerH10;
    [SerializeField]
    private ImageArray TimerH1;
    [SerializeField]
    private ImageArray TimerM10;
    [SerializeField]
    private ImageArray TimerM1;
    [SerializeField]
    private ImageArray TimerS10;
    [SerializeField]
    private ImageArray TimerS1;
    [HeaderBar("▼ボタン類")]
    [SerializeField]
    private Button DetailButton;
    [SerializeField]
    private Button CurrentButton;
    [SerializeField]
    private Button ChallengeButton;
    [SerializeField]
    private Button ResetButton;
    [SerializeField]
    private Button MissionButton;
    [SerializeField]
    private GameObject RankingButton;
    [SerializeField]
    private GameObject StatusButton;
    [HeaderBar("▼１階から再挑戦")]
    [SerializeField]
    private Text ResetText;
    [SerializeField]
    private Text ResetTextFree;
    [HeaderBar("▼制御用")]
    [SerializeField]
    private CanvasGroup mCanvasGroup;
    [SerializeField]
    private ScrollAutoFit mScrollAutoFit;
    private TowerParam mTowerParam;
    private long mRecoverTime;
    private bool initialized;
    private const string VARIABLE_KEY_EVENT_URL = "CAPTION_TOWER_EVENT_DETAIL";
    private GameObject mConfirmBox;
    private bool is_reset;
    private bool m_IsRequireMissionProgressUpdate;
    private TowerManager.MissionProgressRequestState m_MissionProgressState;

    private void Initialize()
    {
      this.AddClickListener(this.RecoverButton, new Action(this.OnClick_RecoverButton));
      this.AddClickListener(this.DetailButton, new Action(this.OnClick_Detail));
      this.AddClickListener(this.ChallengeButton, new Action(this.OnClick_Challenge));
      this.AddClickListener(this.CurrentButton, new Action(this.OnClick_Current));
      this.AddClickListener(this.ResetButton, new Action(this.OnClick_Reset));
      this.mScrollAutoFit.OnScrollBegin.AddListener(new UnityAction(this.OnScrollBegin));
      this.mTowerParam = MonoSingleton<GameManager>.Instance.FindTower(GlobalVars.SelectedTowerID);
      this.mRecoverTime = MonoSingleton<GameManager>.Instance.TowerResuponse.rtime;
      if (this.mTowerParam != null && (UnityEngine.Object) this.RankingButton != (UnityEngine.Object) null)
        this.RankingButton.SetActive(this.mTowerParam.is_view_ranking);
      if (this.mTowerParam != null && (UnityEngine.Object) this.StatusButton != (UnityEngine.Object) null)
        this.StatusButton.SetActive(this.mTowerParam.is_view_ranking);
      if (this.mTowerParam != null && !string.IsNullOrEmpty(this.mTowerParam.eventURL))
        FlowNode_Variable.Set("CAPTION_TOWER_EVENT_DETAIL", this.mTowerParam.eventURL);
      this.initialized = true;
      if (this.is_reset)
      {
        this.mTowerQuestList.ScrollToCurrentFloor(MonoSingleton<GameManager>.Instance.FindFirstTowerFloor(GlobalVars.SelectedTowerID));
        this.RefreshUI();
        this.mTowerQuestInfo.Refresh();
        this.is_reset = false;
        this.StartCoroutine(this.CheckLoadIcon());
      }
      MonoSingleton<GameManager>.Instance.Player.UpdateTowerTrophyStates();
    }

    [DebuggerHidden]
    private IEnumerator CheckLoadIcon()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new TowerManager.\u003CCheckLoadIcon\u003Ec__Iterator0() { \u0024this = this };
    }

    private void OnScrollBegin()
    {
      if ((UnityEngine.Object) this.ChallengeButton != (UnityEngine.Object) null)
        this.ChallengeButton.interactable = false;
      if (!((UnityEngine.Object) this.MissionButton != (UnityEngine.Object) null))
        return;
      this.MissionButton.interactable = false;
    }

    private void AddClickListener(Button button, Action clickListener)
    {
      if ((UnityEngine.Object) button == (UnityEngine.Object) null)
        return;
      button.onClick.AddListener(new UnityAction(clickListener.Invoke));
    }

    private void RemoveClickListener(Button button, Action clickListener)
    {
      if ((UnityEngine.Object) button == (UnityEngine.Object) null)
        return;
      button.onClick.RemoveListener(new UnityAction(clickListener.Invoke));
    }

    private void OnClick_RecoverButton()
    {
      if ((UnityEngine.Object) this.mConfirmBox != (UnityEngine.Object) null)
        return;
      int cost = MonoSingleton<GameManager>.Instance.TowerResuponse.CalcRecoverCost();
      if (MonoSingleton<GameManager>.Instance.Player.Coin < cost)
      {
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 3);
      }
      else
      {
        this.SetCanvasGroupIntaractable(false);
        string text = LocalizedText.Get("sys.MSG_TOWER_RECOVER", new object[1]{ (object) cost });
        this.mConfirmBox = UIUtility.ConfirmBoxTitle(string.Empty, text, (UIUtility.DialogResultEvent) (go =>
        {
          this.RecoverButton.interactable = false;
          this.mConfirmBox = (GameObject) null;
          cost = MonoSingleton<GameManager>.Instance.TowerResuponse.CalcRecoverCost();
          DataSource.Bind<TowerRecoverData>(this.gameObject, new TowerRecoverData(GlobalVars.SelectedTowerID, cost), false);
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 2);
          this.SetCanvasGroupIntaractable(true);
        }), (UIUtility.DialogResultEvent) (go =>
        {
          this.mConfirmBox = (GameObject) null;
          this.SetCanvasGroupIntaractable(true);
        }), (GameObject) null, true, -1, (string) null, (string) null);
      }
    }

    private void SetCanvasGroupIntaractable(bool value)
    {
      if (!((UnityEngine.Object) this.mCanvasGroup != (UnityEngine.Object) null))
        return;
      this.mCanvasGroup.interactable = value;
    }

    private void OnClick_Detail()
    {
    }

    private void OnClick_Challenge()
    {
    }

    private void OnClick_Current()
    {
      this.mTowerQuestList.ScrollToCurrentFloor();
    }

    private void OnClick_Reset()
    {
      if ((UnityEngine.Object) this.mConfirmBox != (UnityEngine.Object) null)
        return;
      int resetCost = (int) MonoSingleton<GameManager>.Instance.TowerResuponse.reset_cost;
      if (MonoSingleton<GameManager>.Instance.Player.Coin < resetCost)
      {
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 5);
      }
      else
      {
        this.SetCanvasGroupIntaractable(false);
        this.mConfirmBox = UIUtility.ConfirmBox(LocalizedText.Get("sys.TOWER_RESET_CHECK", new object[1]
        {
          (object) resetCost
        }), (UIUtility.DialogResultEvent) (go =>
        {
          this.RecoverButton.interactable = false;
          this.ResetButton.interactable = false;
          this.mConfirmBox = (GameObject) null;
          this.UpdateMissionProgressRequestState(MonoSingleton<GameManager>.Instance.FindFirstTowerFloor(GlobalVars.SelectedTowerID));
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 4);
          this.SetCanvasGroupIntaractable(true);
          this.is_reset = true;
        }), (UIUtility.DialogResultEvent) (go =>
        {
          this.mConfirmBox = (GameObject) null;
          this.SetCanvasGroupIntaractable(true);
        }), (GameObject) null, true, -1, (string) null, (string) null);
      }
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 0:
          this.Initialize();
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 7);
          break;
        case 1:
          this.RefreshUI();
          break;
        case 8:
          if (this.m_MissionProgressState == TowerManager.MissionProgressRequestState.NotInitialized)
            this.UpdateMissionProgressRequestState(MonoSingleton<GameManager>.Instance.FindFirstTowerFloor(GlobalVars.SelectedTowerID));
          if (this.m_MissionProgressState == TowerManager.MissionProgressRequestState.RequireProgressRequest)
          {
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 9);
            break;
          }
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
          break;
        case 11:
          this.m_MissionProgressState = TowerManager.MissionProgressRequestState.ReceivedProgress;
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 12);
          break;
      }
    }

    private void Update()
    {
      if (!this.initialized)
        return;
      this.mRefreshInterval -= Time.unscaledDeltaTime;
      if ((double) this.mRefreshInterval > 0.0)
        return;
      this.SetRecoverText();
      this.mRefreshInterval = 1f;
    }

    private void RefreshUI()
    {
      QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
      this.mRecoverTime = MonoSingleton<GameManager>.Instance.TowerResuponse.rtime;
      if (quest != null && (UnityEngine.Object) this.ChallengeButton != (UnityEngine.Object) null)
      {
        DataSource.Bind<QuestParam>(this.ChallengeButton.gameObject, quest, false);
        this.ChallengeButton.interactable = quest.IsQuestCondition() && quest.state != QuestStates.Cleared;
        GameParameter.UpdateAll(this.ChallengeButton.gameObject);
      }
      if ((UnityEngine.Object) this.RecoverButton != (UnityEngine.Object) null)
      {
        TowerResuponse towerResuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
        bool flag = towerResuponse.GetAvailableUnits().Count > 0 && towerResuponse.ExistDamagedUnit() || towerResuponse.GetDiedUnitNum() > 0;
        this.RecoverButton.interactable = flag && !towerResuponse.is_reset;
        if ((UnityEngine.Object) this.RecoverTimer != (UnityEngine.Object) null)
          this.RecoverTimer.SetActive(flag);
      }
      if ((UnityEngine.Object) this.ResetButton != (UnityEngine.Object) null)
      {
        TowerResuponse towerResuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
        this.ResetButton.gameObject.SetActive(towerResuponse.is_reset);
        this.ResetButton.interactable = towerResuponse.is_reset;
        this.ChallengeButton.gameObject.SetActive(!towerResuponse.is_reset);
        this.ResetText.text = towerResuponse.reset_cost.ToString();
        bool flag = towerResuponse.reset_cost == (short) 0;
        this.ResetText.gameObject.SetActive(!flag);
        this.ResetTextFree.gameObject.SetActive(flag);
      }
      if ((UnityEngine.Object) this.MissionButton != (UnityEngine.Object) null)
        this.MissionButton.interactable = true;
      this.SetAliveUnitsText();
      this.SetRecoverText();
    }

    private void SetAliveUnitsText()
    {
      if ((UnityEngine.Object) this.AliveUnits == (UnityEngine.Object) null)
        return;
      TowerResuponse towerResuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
      List<UnitData> availableUnits = towerResuponse.GetAvailableUnits();
      int diedUnitNum = towerResuponse.GetDiedUnitNum();
      this.AliveUnits.text = string.Format("{0}/{1}", (object) Mathf.Max(0, availableUnits.Count - diedUnitNum), (object) availableUnits.Count);
    }

    private void SetRecoverText()
    {
      int num = MonoSingleton<GameManager>.Instance.TowerResuponse.CalcRecoverCost();
      if ((UnityEngine.Object) this.RecoverCost != (UnityEngine.Object) null)
      {
        bool flag = num == 0;
        this.RecoverCost.gameObject.SetActive(!flag);
        this.RecoverCostFree.gameObject.SetActive(flag);
        if (num > 0)
          this.RecoverCost.text = num.ToString();
      }
      if (!((UnityEngine.Object) this.RecoverTimer != (UnityEngine.Object) null))
        return;
      TimeSpan timeSpan = TimeManager.FromUnixTime(this.mRecoverTime).AddMinutes(-1.0) - TimeManager.ServerTime;
      if (timeSpan.TotalMinutes < 0.0)
      {
        this.RecoverTimer.SetActive(false);
      }
      else
      {
        this.RecoverTimer.SetActive(true);
        this.UpdateTimer(timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
      }
    }

    private void UpdateMissionProgressRequestState(TowerFloorParam floorParam)
    {
      if (!string.IsNullOrEmpty(floorParam.mission))
        this.m_MissionProgressState = TowerManager.MissionProgressRequestState.RequireProgressRequest;
      else
        this.m_MissionProgressState = TowerManager.MissionProgressRequestState.UnrequireProgressRequest;
    }

    private void UpdateTimerText(int hours, int minutes, int seconds)
    {
      this.RecoverFreeTime.text = string.Format("{0:00}:{1:00}:{2:00}", (object) hours, (object) minutes, (object) seconds);
    }

    private void UpdateTimer(int hours, int minutes, int seconds)
    {
      if (hours < 0)
        hours = 0;
      if (minutes < 0)
        minutes = 0;
      if (seconds < 0)
        seconds = 0;
      hours %= 60;
      minutes %= 60;
      seconds %= 60;
      int num1 = hours / 10;
      int num2 = hours % 10;
      int num3 = minutes / 10;
      int num4 = minutes % 10;
      int num5 = seconds / 10;
      int num6 = seconds % 10;
      this.TimerH10.ImageIndex = num1 >= 10 ? 0 : num1;
      this.TimerH1.ImageIndex = num2 >= 10 ? 0 : num2;
      this.TimerM10.ImageIndex = num3 >= 10 ? 0 : num3;
      this.TimerM1.ImageIndex = num4 >= 10 ? 0 : num4;
      this.TimerS10.ImageIndex = num5 >= 10 ? 0 : num5;
      this.TimerS1.ImageIndex = num6 >= 10 ? 0 : num6;
    }

    private enum MissionProgressRequestState
    {
      NotInitialized,
      RequireProgressRequest,
      UnrequireProgressRequest,
      ReceivedProgress,
    }
  }
}
