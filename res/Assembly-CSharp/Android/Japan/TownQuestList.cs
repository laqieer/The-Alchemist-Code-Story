// Decompiled with JetBrains decompiler
// Type: TownQuestList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using SRPG;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

[FlowNode.NodeType("System/Quest/TownQuestList", 32741)]
[FlowNode.Pin(100, "ストーリークエストへ", FlowNode.PinTypes.Input, 100)]
[FlowNode.Pin(110, "イベントクエストへ", FlowNode.PinTypes.Input, 110)]
[FlowNode.Pin(115, "イベントクエスト書庫へ", FlowNode.PinTypes.Input, 115)]
[FlowNode.Pin(120, "聖石の追憶へ", FlowNode.PinTypes.Input, 120)]
[FlowNode.Pin(130, "バベル戦記へ", FlowNode.PinTypes.Input, 130)]
[FlowNode.Pin(140, "キークエストへ", FlowNode.PinTypes.Input, 140)]
[FlowNode.Pin(150, "塔へ", FlowNode.PinTypes.Input, 150)]
[FlowNode.Pin(160, "曜日、育成クエストへ", FlowNode.PinTypes.Input, 160)]
[FlowNode.Pin(170, "メニューを開いた：チャレンジ", FlowNode.PinTypes.Input, 170)]
[FlowNode.Pin(180, "章解放エフェクト設定", FlowNode.PinTypes.Input, 180)]
[FlowNode.Pin(190, "生成時にストーリーを開く", FlowNode.PinTypes.Input, 190)]
[FlowNode.Pin(200, "生成時にイベントを開く", FlowNode.PinTypes.Input, 200)]
[FlowNode.Pin(210, "生成時にチャレンジを開く", FlowNode.PinTypes.Input, 210)]
[FlowNode.Pin(220, "生成時", FlowNode.PinTypes.Input, 220)]
[FlowNode.Pin(1000, "パネル指定後", FlowNode.PinTypes.Output, 1000)]
[FlowNode.Pin(1010, "生成時にロビー以外を開いた", FlowNode.PinTypes.Output, 1010)]
public class TownQuestList : FlowNode
{
  private const int INPUT_CHANGE_STORY = 100;
  private const int INPUT_CHANGE_EVENT = 110;
  private const int INPUT_CHANGE_ARCHIVE = 115;
  private const int INPUT_CHANGE_SEISEKI = 120;
  private const int INPUT_CHANGE_BABEL = 130;
  private const int INPUT_CHANGE_KEY = 140;
  private const int INPUT_CHANGE_TOWER = 150;
  private const int INPUT_CHANGE_WEEKDAY = 160;
  private const int INPUT_CHECK_RELEASE_ORDEAL = 170;
  private const int INPUT_SETUP_CHAPTER_EFFECT = 180;
  private const int INPUT_OPEN_MENU_STORY = 190;
  private const int INPUT_OPEN_MENU_EVENT = 200;
  private const int INPUT_OPEN_MENU_CHALLENGE = 210;
  private const int INPUT_INITIALIZE = 220;
  private const int OUTPUT_OPEN_MENU = 1000;
  private const int OUTPUT_OPEN_SHORTCUT = 1010;
  private const string CHANGE_SCENE_STORY = "TO_STORY";
  private const string CHANGE_SCENE_EVENT = "TO_EVENT";
  private const string ENABLE_RANKING_QUEST = "ENABLE_RANKING_QUEST";
  [SerializeField]
  private GameObject mOrdealObj;
  [SerializeField]
  private GameObject mStoryReleaseEffectObj;
  [SerializeField]
  private List<GameObject> mPanelRootList;
  private static TownQuestList.PanelType mReqStartTimeShowPanel;

  public override void OnActivate(int pinID)
  {
    switch (pinID)
    {
      case 100:
        GlobalEvent.Invoke("TO_STORY", (object) this);
        break;
      case 110:
        QuestParam.GotoEventListChapter();
        GlobalEvent.Invoke("TO_EVENT", (object) this);
        break;
      case 115:
        GlobalVars.ReqEventPageListType = GlobalVars.EventQuestListType.EventQuestArchive;
        GlobalEvent.Invoke("TO_EVENT", (object) this);
        break;
      case 120:
        QuestParam.GotoEventListChapter();
        GlobalVars.ReqEventPageListType = GlobalVars.EventQuestListType.Seiseki;
        GlobalVars.SelectedSection.Set("WD_SEISEKI");
        GlobalEvent.Invoke("TO_EVENT", (object) this);
        break;
      case 130:
        QuestParam.GotoEventListChapter();
        GlobalVars.ReqEventPageListType = GlobalVars.EventQuestListType.Babel;
        GlobalVars.SelectedSection.Set("WD_BABEL");
        GlobalEvent.Invoke("TO_EVENT", (object) this);
        break;
      case 140:
        QuestParam.GotoEventListChapter();
        GlobalVars.ReqEventPageListType = GlobalVars.EventQuestListType.KeyQuest;
        GlobalEvent.Invoke("TO_EVENT", (object) this);
        break;
      case 150:
        QuestParam.GotoEventListChapter();
        GlobalVars.ReqEventPageListType = GlobalVars.EventQuestListType.Tower;
        GlobalEvent.Invoke("TO_EVENT", (object) this);
        break;
      case 160:
        QuestParam.GotoEventListChapter();
        GlobalVars.ReqEventPageListType = GlobalVars.EventQuestListType.DailyAndEnhance;
        GlobalEvent.Invoke("TO_EVENT", (object) this);
        break;
      case 170:
        this.OnAfterStartup(true);
        break;
      case 180:
        this.SetReleaseStoryPartAction();
        break;
      case 190:
        TownQuestList.mReqStartTimeShowPanel = TownQuestList.PanelType.storyQuest;
        this.ActivateOutputLinks(1000);
        break;
      case 200:
        TownQuestList.mReqStartTimeShowPanel = TownQuestList.PanelType.eventQuest;
        this.ActivateOutputLinks(1000);
        break;
      case 210:
        TownQuestList.mReqStartTimeShowPanel = TownQuestList.PanelType.Challenge;
        this.ActivateOutputLinks(1000);
        break;
      case 220:
        this.Initialize();
        break;
    }
  }

  private void Initialize()
  {
    if (TownQuestList.mReqStartTimeShowPanel == TownQuestList.PanelType.root)
      return;
    this.ShowTargetPanel(TownQuestList.mReqStartTimeShowPanel);
    TownQuestList.mReqStartTimeShowPanel = TownQuestList.PanelType.root;
    GlobalEvent.Invoke("ENABLE_RANKING_QUEST", (object) null);
    this.ActivateOutputLinks(1010);
  }

  private void ShowTargetPanel(TownQuestList.PanelType targetPanel)
  {
    if (this.mPanelRootList == null)
      return;
    for (int index = 0; index < this.mPanelRootList.Count; ++index)
    {
      if (targetPanel == (TownQuestList.PanelType) index)
        this.mPanelRootList[index].SetActive(true);
      else
        this.mPanelRootList[index].SetActive(false);
    }
    if (TownQuestList.mReqStartTimeShowPanel != TownQuestList.PanelType.Challenge)
      return;
    this.OnAfterStartup(true);
  }

  private void OnAfterStartup(bool success)
  {
    GameManager instance = MonoSingleton<GameManager>.Instance;
    UnlockParam lockState = ((IEnumerable<UnlockParam>) instance.MasterParam.Unlocks).FirstOrDefault<UnlockParam>((Func<UnlockParam, bool>) (unlock => unlock.UnlockTarget == UnlockTargets.Ordeal));
    if (lockState == null || PlayerPrefsUtility.HasKey(PlayerPrefsUtility.ORDEAL_RELEASE_ANIMATION_PLAYED) || instance.Player.Lv < lockState.PlayerLevel)
      return;
    PlayerPrefsUtility.SetInt(PlayerPrefsUtility.ORDEAL_RELEASE_ANIMATION_PLAYED, 1, false);
    this.StartCoroutine(this.OrdealReleaseAnimationCoroutine(this.mOrdealObj, lockState));
  }

  [DebuggerHidden]
  private IEnumerator OrdealReleaseAnimationCoroutine(GameObject obj, UnlockParam lockState)
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new TownQuestList.\u003COrdealReleaseAnimationCoroutine\u003Ec__Iterator0()
    {
      obj = obj,
      lockState = lockState
    };
  }

  private void SetReleaseStoryPartAction()
  {
    if (MonoSingleton<GameManager>.Instance.CheckReleaseStoryPart())
      this.mStoryReleaseEffectObj.SetActive(true);
    else
      this.mStoryReleaseEffectObj.SetActive(false);
  }

  public enum PanelType
  {
    root,
    storyQuest,
    eventQuest,
    Challenge,
    max,
  }
}
