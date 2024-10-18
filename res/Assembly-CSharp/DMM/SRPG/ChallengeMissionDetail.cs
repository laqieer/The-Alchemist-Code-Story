// Decompiled with JetBrains decompiler
// Type: SRPG.ChallengeMissionDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(0, "表示", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "キャンセル", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(1000, "ガチャへ移動", FlowNode.PinTypes.Output, 1001)]
  [FlowNode.Pin(1001, "クエスト選択へ移動", FlowNode.PinTypes.Output, 1002)]
  [FlowNode.Pin(1004, "ゴールド購入画面へ移動", FlowNode.PinTypes.Output, 1003)]
  [FlowNode.Pin(1002, "アビリティ強化画面へ移動", FlowNode.PinTypes.Output, 1004)]
  [FlowNode.Pin(1003, "装備強化画面へ移動", FlowNode.PinTypes.Output, 1005)]
  [FlowNode.Pin(1005, "マルチプレイへ移動", FlowNode.PinTypes.Output, 1006)]
  [FlowNode.Pin(1006, "エリートクエスト選択へ移動", FlowNode.PinTypes.Output, 1007)]
  [FlowNode.Pin(1007, "イベントクエスト選択へ移動", FlowNode.PinTypes.Output, 1008)]
  [FlowNode.Pin(1008, "アリーナへ移動", FlowNode.PinTypes.Output, 1009)]
  [FlowNode.Pin(1009, "ユニット強化画面へ移動", FlowNode.PinTypes.Output, 1101)]
  [FlowNode.Pin(1010, "ユニットアビリティセット画面へ移動", FlowNode.PinTypes.Output, 1102)]
  [FlowNode.Pin(1011, "ユニットジョブ画面へ移動", FlowNode.PinTypes.Output, 1103)]
  [FlowNode.Pin(1012, "ユニット選択へ移動", FlowNode.PinTypes.Output, 1104)]
  [FlowNode.Pin(1013, "アンナの店へ移動", FlowNode.PinTypes.Output, 1201)]
  [FlowNode.Pin(1014, "マリアの店へ移動", FlowNode.PinTypes.Output, 1202)]
  [FlowNode.Pin(1015, "ルイザの店へ移動", FlowNode.PinTypes.Output, 1203)]
  [FlowNode.Pin(1016, "ソウルショップへ移動", FlowNode.PinTypes.Output, 1204)]
  [FlowNode.Pin(1017, "ツアーの店へ移動", FlowNode.PinTypes.Output, 1205)]
  [FlowNode.Pin(1018, "闘技場交換所へ移動", FlowNode.PinTypes.Output, 1206)]
  [FlowNode.Pin(1019, "マルチ交換所へ移動", FlowNode.PinTypes.Output, 1207)]
  [FlowNode.Pin(1020, "魂の交換所へ移動", FlowNode.PinTypes.Output, 1208)]
  [FlowNode.Pin(1021, "武具の店へ移動", FlowNode.PinTypes.Output, 1209)]
  [FlowNode.Pin(1040, "限定ショップへ移動", FlowNode.PinTypes.Output, 1210)]
  [FlowNode.Pin(1041, "ポートショップへ移動", FlowNode.PinTypes.Output, 1211)]
  [FlowNode.Pin(1022, "武具錬成画面へ移動", FlowNode.PinTypes.Output, 1301)]
  [FlowNode.Pin(1023, "武具強化画面へ移動", FlowNode.PinTypes.Output, 1302)]
  [FlowNode.Pin(1024, "武具進化画面へ移動", FlowNode.PinTypes.Output, 1303)]
  [FlowNode.Pin(1025, "FgGID画面へ移動", FlowNode.PinTypes.Output, 1401)]
  [FlowNode.Pin(1026, "塔へ移動", FlowNode.PinTypes.Output, 1402)]
  [FlowNode.Pin(1027, "対戦へ移動", FlowNode.PinTypes.Output, 1403)]
  [FlowNode.Pin(1028, "初心者イベントクエスト選択へ移動", FlowNode.PinTypes.Output, 1501)]
  [FlowNode.Pin(1029, "初心者の館へ移動", FlowNode.PinTypes.Output, 1502)]
  [FlowNode.Pin(1030, "真理念装へ移動", FlowNode.PinTypes.Output, 1601)]
  [FlowNode.Pin(1031, "カルマへ移動", FlowNode.PinTypes.Output, 1602)]
  [FlowNode.Pin(1032, "ホームへ移動", FlowNode.PinTypes.Output, 1603)]
  [FlowNode.Pin(1033, "お知らせへ移動", FlowNode.PinTypes.Output, 1604)]
  [FlowNode.Pin(1034, "フレンド画面へ移動", FlowNode.PinTypes.Output, 1605)]
  [FlowNode.Pin(1035, "創世編へ移動", FlowNode.PinTypes.Output, 1606)]
  [FlowNode.Pin(1036, "新イベントへ移動", FlowNode.PinTypes.Output, 1607)]
  [FlowNode.Pin(1037, "アルケミィポートへ移動", FlowNode.PinTypes.Output, 1608)]
  [FlowNode.Pin(1038, "レイドへ移動", FlowNode.PinTypes.Output, 1609)]
  [FlowNode.Pin(1039, "ポートレイドへ移動", FlowNode.PinTypes.Output, 1610)]
  public class ChallengeMissionDetail : MonoBehaviour, IFlowInterface
  {
    public const int PIN_OPEN = 0;
    public const int PIN_CANCEL = 100;
    public const int PIN_GOTO_GACHA = 1000;
    public const int PIN_GOTO_QUEST = 1001;
    public const int PIN_GOTO_ABILITY = 1002;
    public const int PIN_GOTO_SOUBI = 1003;
    public const int PIN_GOTO_BUYGOLD = 1004;
    public const int PIN_GOTO_MULTI = 1005;
    public const int PIN_GOTO_ELITE = 1006;
    public const int PIN_GOTO_EVENT = 1007;
    public const int PIN_GOTO_ARENA = 1008;
    public const int PIN_GOTO_UNIT_STR = 1009;
    public const int PIN_GOTO_ABILITYSET = 1010;
    public const int PIN_GOTO_UNIT_JOB = 1011;
    public const int PIN_GOTO_UNIT = 1012;
    public const int PIN_GOTO_SHOP_NORMAL = 1013;
    public const int PIN_GOTO_SHOP_TABI = 1014;
    public const int PIN_GOTO_SHOP_KIMAGRE = 1015;
    public const int PIN_GOTO_SHOP_MONOZUKI = 1016;
    public const int PIN_GOTO_SHOP_TOUR = 1017;
    public const int PIN_GOTO_SHOP_ARENA = 1018;
    public const int PIN_GOTO_SHOP_MULTI = 1019;
    public const int PIN_GOTO_SHOP_KAKERA = 1020;
    public const int PIN_GOTO_SHOP_ARTIFACT = 1021;
    public const int PIN_GOTO_ATF_TRANS = 1022;
    public const int PIN_GOTO_ATF_STRTH = 1023;
    public const int PIN_GOTO_ATF_EVOLT = 1024;
    public const int PIN_GOTO_FGGID = 1025;
    public const int PIN_GOTO_TOWER = 1026;
    public const int PIN_GOTO_VERSUS = 1027;
    public const int PIN_GOTO_EVENT_BEGINEER = 1028;
    public const int PIN_GOTO_BEGINNERTOP = 1029;
    public const int PIN_GOTO_CONCEPTCARD = 1030;
    public const int PIN_GOTO_ORDEAL = 1031;
    public const int PIN_GOTO_HOME = 1032;
    public const int PIN_GOTO_NEWS = 1033;
    public const int PIN_GOTO_FRIEND = 1034;
    public const int PIN_GOTO_GENESIS = 1035;
    public const int PIN_GOTO_ADVANCE = 1036;
    public const int PIN_GOTO_GUILD = 1037;
    public const int PIN_GOTO_RAIDBOSS = 1038;
    public const int PIN_GOTO_GUILDRAIDBOSS = 1039;
    public const int PIN_GOTO_SHOP_LIMITED = 1040;
    public const int PIN_GOTO_SHOP_PORT = 1041;
    public Transform RewardItemHolder;
    public GameObject RewardItemTemplate;
    public Button ButtonCancel;
    public Button ButtonTry;
    [SerializeField]
    private Text CloseButtonText;
    private List<GameObject> mRewardItems = new List<GameObject>();

    public void Activated(int pinID)
    {
      if (pinID != 0)
        return;
      ((Component) this).gameObject.SetActive(true);
    }

    private void Awake()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RewardItemTemplate, (UnityEngine.Object) null))
        return;
      this.RewardItemTemplate.SetActive(false);
    }

    private void Start()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ButtonCancel, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.ButtonTry, (UnityEngine.Object) null))
      {
        ((Behaviour) this).enabled = false;
      }
      else
      {
        // ISSUE: method pointer
        ((UnityEvent) this.ButtonCancel.onClick).AddListener(new UnityAction((object) this, __methodptr(OnCancel)));
        // ISSUE: method pointer
        ((UnityEvent) this.ButtonTry.onClick).AddListener(new UnityAction((object) this, __methodptr(OnTry)));
      }
    }

    private void OnEnable()
    {
      this.UpdateReward(DataSource.FindDataOfClass<TrophyParam>(((Component) this).gameObject, (TrophyParam) null));
    }

    private void UpdateReward(TrophyParam trophy)
    {
      foreach (UnityEngine.Object mRewardItem in this.mRewardItems)
        UnityEngine.Object.Destroy(mRewardItem);
      this.mRewardItems.Clear();
      if (trophy == null || UnityEngine.Object.op_Equality((UnityEngine.Object) this.RewardItemTemplate, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.RewardItemHolder, (UnityEngine.Object) null))
        return;
      bool flag = ChallengeMissionDetail.IsChallengeable(trophy);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ButtonTry, (UnityEngine.Object) null))
        ((Component) this.ButtonTry).gameObject.SetActive(flag);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CloseButtonText, (UnityEngine.Object) null))
        this.CloseButtonText.text = !flag ? LocalizedText.Get("sys.BTN_CLOSE") : LocalizedText.Get("sys.CMD_CANCEL");
      if (trophy.Gold != 0)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.RewardItemTemplate);
        ChallengeMissionRewardItem component = gameObject.GetComponent<ChallengeMissionRewardItem>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          component.SetGold(trophy.Gold);
        gameObject.transform.SetParent(this.RewardItemHolder, false);
        gameObject.SetActive(true);
        this.mRewardItems.Add(gameObject);
      }
      if (trophy.Exp != 0)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.RewardItemTemplate);
        ChallengeMissionRewardItem component = gameObject.GetComponent<ChallengeMissionRewardItem>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          component.SetEXP(trophy.Exp);
        gameObject.transform.SetParent(this.RewardItemHolder, false);
        gameObject.SetActive(true);
        this.mRewardItems.Add(gameObject);
      }
      if (trophy.Coin != 0)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.RewardItemTemplate);
        ChallengeMissionRewardItem component = gameObject.GetComponent<ChallengeMissionRewardItem>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          component.SetCoin(trophy.Coin);
        gameObject.transform.SetParent(this.RewardItemHolder, false);
        gameObject.SetActive(true);
        this.mRewardItems.Add(gameObject);
      }
      if (trophy.Stamina != 0)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.RewardItemTemplate);
        ChallengeMissionRewardItem component = gameObject.GetComponent<ChallengeMissionRewardItem>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          component.SetStamina(trophy.Stamina);
        gameObject.transform.SetParent(this.RewardItemHolder, false);
        gameObject.SetActive(true);
        this.mRewardItems.Add(gameObject);
      }
      if (trophy.Items != null && trophy.Items.Length > 0)
      {
        foreach (TrophyParam.RewardItem rewardItem in trophy.Items)
        {
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.RewardItemTemplate);
          ChallengeMissionRewardItem component = gameObject.GetComponent<ChallengeMissionRewardItem>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
            component.SetItem(rewardItem.iname, rewardItem.Num);
          gameObject.transform.SetParent(this.RewardItemHolder, false);
          gameObject.SetActive(true);
          this.mRewardItems.Add(gameObject);
        }
      }
      if (trophy.ConceptCards == null || trophy.ConceptCards.Length <= 0)
        return;
      foreach (TrophyParam.RewardItem conceptCard in trophy.ConceptCards)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.RewardItemTemplate);
        ChallengeMissionRewardItem component = gameObject.GetComponent<ChallengeMissionRewardItem>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          component.SetConceptCard(conceptCard.iname, conceptCard.Num);
        gameObject.transform.SetParent(this.RewardItemHolder, false);
        gameObject.SetActive(true);
        this.mRewardItems.Add(gameObject);
      }
    }

    public static bool IsChallengeable(TrophyParam trophy)
    {
      if (trophy.DispType == TrophyDispType.HideChallenge || trophy.Objectives == null || trophy.Objectives.Length <= 0)
        return false;
      TrophyConditionTypes type = trophy.Objectives[0].type;
      switch (type)
      {
        case TrophyConditionTypes.vip:
        case TrophyConditionTypes.stamina:
        case TrophyConditionTypes.card:
        case TrophyConditionTypes.review:
        case TrophyConditionTypes.followtwitter:
        case TrophyConditionTypes.fggid:
        case TrophyConditionTypes.logincount:
          return false;
        default:
          if (type != TrophyConditionTypes.killenemy && type != TrophyConditionTypes.getitem)
            return true;
          goto case TrophyConditionTypes.vip;
      }
    }

    public static void SetGoToUnitListState(TrophyParam trophy)
    {
      if (trophy.Objectives[0].SvalCount == 1)
        ChallengeMissionDetail.SetGoToUnitListState(trophy.Objectives[0].sval_base);
      else
        GlobalVars.UnitDataForUnitList = (UnitData) null;
    }

    public static void SetGoToUnitListState(string unitName)
    {
      if (unitName == null)
      {
        GlobalVars.UnitDataForUnitList = (UnitData) null;
      }
      else
      {
        UnitData unitDataByUnitId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUnitID(unitName);
        if (unitDataByUnitId != null)
          GlobalVars.UnitDataForUnitList = unitDataByUnitId;
        else
          GlobalVars.UnitDataForUnitList = (UnitData) null;
      }
    }

    public static string[] SplitSvalByComma(TrophyParam trophy)
    {
      if (string.IsNullOrEmpty(trophy.Objectives[0].sval_base))
        return (string[]) null;
      char[] chArray = new char[1]{ ',' };
      return trophy.Objectives[0].sval_base.Split(chArray);
    }

    private void OnCancel() => FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);

    private void OnTry()
    {
      TrophyParam dataOfClass = DataSource.FindDataOfClass<TrophyParam>(((Component) this).gameObject, (TrophyParam) null);
      if (dataOfClass == null)
      {
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      }
      else
      {
        PlayerData player = MonoSingleton<GameManager>.Instance.Player;
        switch (dataOfClass.Objectives[0].type)
        {
          case TrophyConditionTypes.winquest:
          case TrophyConditionTypes.playerlv:
          case TrophyConditionTypes.winquestsoldier:
          case TrophyConditionTypes.losequest:
          case TrophyConditionTypes.damage_over:
          case TrophyConditionTypes.complete_all_quest_mission:
          case TrophyConditionTypes.complete_all_quest_mission_total:
          case TrophyConditionTypes.complete_all_mission_count:
          case TrophyConditionTypes.complete_story_mission_count:
            QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(dataOfClass.Objectives[0].sval_base);
            string chapterId = quest == null ? (string) null : quest.ChapterID;
            ArchiveParam archiveByArea1 = MonoSingleton<GameManager>.Instance.FindArchiveByArea(chapterId);
            if (archiveByArea1 != null && archiveByArea1.IsAvailable() && MonoSingleton<GameManager>.Instance.Player.CheckUnlock(UnlockTargets.Archive))
            {
              switch (quest.type)
              {
                case QuestTypes.Multi:
                  if (MonoSingleton<GameManager>.Instance.Player.IsQuestArchiveOpenByArea(chapterId))
                  {
                    if (LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.MultiPlay))
                      return;
                    FlowNode_GameObject.ActivateOutputLinks((Component) this, 1005);
                    return;
                  }
                  UIUtility.ConfirmBox(LocalizedText.Get("sys.ARCHIVE_CHANGE_SCENE_CONFIRM"), (UIUtility.DialogResultEvent) (yes_button =>
                  {
                    GlobalVars.SelectedSection.Set("WD_DAILY");
                    GlobalVars.ReqEventPageListType = GlobalVars.EventQuestListType.EventQuestArchive;
                    FlowNode_Variable.Set("SHOW_CHAPTER", "1");
                    FlowNode_GameObject.ActivateOutputLinks((Component) this, 1007);
                  }), (UIUtility.DialogResultEvent) null);
                  return;
                case QuestTypes.Event:
                  if (MonoSingleton<GameManager>.Instance.Player.IsQuestArchiveOpenByArea(chapterId))
                  {
                    UnlockTargets target = UnlockTargets.EventQuest;
                    if (LevelLock.IsNeedCheckUnlockConds(quest))
                      target = LevelLock.GetTargetByQuestId(quest.iname, UnlockTargets.EventQuest);
                    if (LevelLock.ShowLockMessage(player.Lv, player.VipRank, target))
                      return;
                    GlobalVars.SelectedQuestID = quest.iname;
                    GlobalVars.SelectedChapter.Set(chapterId);
                    GlobalVars.SelectedSection.Set("WD_DAILY");
                    GlobalVars.ReqEventPageListType = GlobalVars.EventQuestListType.EventQuestArchive;
                    FlowNode_Variable.Set("SHOW_CHAPTER", "0");
                    FlowNode_GameObject.ActivateOutputLinks((Component) this, 1007);
                    return;
                  }
                  UIUtility.ConfirmBox(LocalizedText.Get("sys.ARCHIVE_CHANGE_SCENE_CONFIRM"), (UIUtility.DialogResultEvent) (yes_button =>
                  {
                    GlobalVars.SelectedSection.Set("WD_DAILY");
                    GlobalVars.ReqEventPageListType = GlobalVars.EventQuestListType.EventQuestArchive;
                    FlowNode_Variable.Set("SHOW_CHAPTER", "1");
                    FlowNode_GameObject.ActivateOutputLinks((Component) this, 1007);
                  }), (UIUtility.DialogResultEvent) null);
                  return;
                default:
                  return;
              }
            }
            else
            {
              QuestTypes quest_type = QuestTypes.Story;
              if (!QuestParam.TransSectionGotoQuest(dataOfClass.Objectives[0].sval_base, out quest_type, new UIUtility.DialogResultEvent(this.MsgBoxJumpToQuest)))
                break;
              switch (quest_type)
              {
                case QuestTypes.Beginner:
                  FlowNode_GameObject.ActivateOutputLinks((Component) this, 1028);
                  return;
                case QuestTypes.MultiGps:
label_25:
                  FlowNode_GameObject.ActivateOutputLinks((Component) this, 1005);
                  return;
                case QuestTypes.GenesisStory:
                case QuestTypes.GenesisBoss:
                  FlowNode_GameObject.ActivateOutputLinks((Component) this, 1035);
                  return;
                case QuestTypes.AdvanceStory:
                case QuestTypes.AdvanceBoss:
                  FlowNode_GameObject.ActivateOutputLinks((Component) this, 1036);
                  return;
                default:
                  switch (quest_type)
                  {
                    case QuestTypes.Event:
                    case QuestTypes.Gps:
                      FlowNode_GameObject.ActivateOutputLinks((Component) this, 1007);
                      return;
                    case QuestTypes.Tower:
                      FlowNode_GameObject.ActivateOutputLinks((Component) this, 1026);
                      return;
                    default:
                      if (quest_type != QuestTypes.Multi)
                      {
                        FlowNode_GameObject.ActivateOutputLinks((Component) this, 1001);
                        return;
                      }
                      goto label_25;
                  }
              }
            }
          case TrophyConditionTypes.killenemy:
          case TrophyConditionTypes.getitem:
          case TrophyConditionTypes.vip:
          case TrophyConditionTypes.stamina:
          case TrophyConditionTypes.card:
          case TrophyConditionTypes.review:
          case TrophyConditionTypes.followtwitter:
          case TrophyConditionTypes.fggid:
          case TrophyConditionTypes.logincount:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
            break;
          case TrophyConditionTypes.winelite:
          case TrophyConditionTypes.loseelite:
            if (!QuestParam.TransSectionGotoElite(new UIUtility.DialogResultEvent(this.MsgBoxJumpToQuest)))
              break;
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1001);
            break;
          case TrophyConditionTypes.winevent:
          case TrophyConditionTypes.loseevent:
          case TrophyConditionTypes.exclear_fire:
          case TrophyConditionTypes.exclear_water:
          case TrophyConditionTypes.exclear_wind:
          case TrophyConditionTypes.exclear_thunder:
          case TrophyConditionTypes.exclear_light:
          case TrophyConditionTypes.exclear_dark:
          case TrophyConditionTypes.exclear_fire_nocon:
          case TrophyConditionTypes.exclear_water_nocon:
          case TrophyConditionTypes.exclear_wind_nocon:
          case TrophyConditionTypes.exclear_thunder_nocon:
          case TrophyConditionTypes.exclear_light_nocon:
          case TrophyConditionTypes.exclear_dark_nocon:
            if (LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.EventQuest))
              break;
            QuestParam.GotoEventListQuest((string) null);
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1007);
            break;
          case TrophyConditionTypes.gacha:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1000);
            break;
          case TrophyConditionTypes.multiplay:
          case TrophyConditionTypes.winmulti:
          case TrophyConditionTypes.winmultimore:
          case TrophyConditionTypes.winmultiless:
            if (LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.MultiPlay))
              break;
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1005);
            break;
          case TrophyConditionTypes.ability:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1002);
            break;
          case TrophyConditionTypes.soubi:
            if (player.CheckUnlock(UnlockTargets.EnhanceEquip))
            {
              FlowNode_GameObject.ActivateOutputLinks((Component) this, 1003);
              break;
            }
            LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.EnhanceEquip);
            break;
          case TrophyConditionTypes.buygold:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1004);
            break;
          case TrophyConditionTypes.arena:
          case TrophyConditionTypes.winarena:
          case TrophyConditionTypes.losearena:
            if (player.CheckUnlock(UnlockTargets.Arena))
            {
              FlowNode_GameObject.ActivateOutputLinks((Component) this, 1008);
              break;
            }
            LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.Arena);
            break;
          case TrophyConditionTypes.unitlevel:
          case TrophyConditionTypes.upunitlevel:
          case TrophyConditionTypes.makeunitlevel:
            ChallengeMissionDetail.SetGoToUnitListState(dataOfClass);
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1009);
            break;
          case TrophyConditionTypes.evolutionnum:
          case TrophyConditionTypes.limitbreak:
          case TrophyConditionTypes.evoltiontimes:
          case TrophyConditionTypes.changejob:
            ChallengeMissionDetail.SetGoToUnitListState(dataOfClass);
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1012);
            break;
          case TrophyConditionTypes.joblevel:
            string[] strArray1 = ChallengeMissionDetail.SplitSvalByComma(dataOfClass);
            if (strArray1 != null && strArray1.Length > 0)
              ChallengeMissionDetail.SetGoToUnitListState(strArray1[0]);
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1011);
            break;
          case TrophyConditionTypes.unitequip:
          case TrophyConditionTypes.upjoblevel:
          case TrophyConditionTypes.makejoblevel:
            ChallengeMissionDetail.SetGoToUnitListState(dataOfClass);
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1011);
            break;
          case TrophyConditionTypes.changeability:
            ChallengeMissionDetail.SetGoToUnitListState(dataOfClass);
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1010);
            break;
          case TrophyConditionTypes.makeabilitylevel:
            string[] strArray2 = ChallengeMissionDetail.SplitSvalByComma(dataOfClass);
            if (strArray2 != null && strArray2.Length > 0)
              ChallengeMissionDetail.SetGoToUnitListState(strArray2[0]);
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1002);
            break;
          case TrophyConditionTypes.buyatshop:
            this.GotoShop(dataOfClass);
            break;
          case TrophyConditionTypes.artifacttransmute:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1022);
            break;
          case TrophyConditionTypes.artifactstrength:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1023);
            break;
          case TrophyConditionTypes.artifactevolution:
          case TrophyConditionTypes.makeartifactlevel:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1024);
            break;
          case TrophyConditionTypes.wintower:
          case TrophyConditionTypes.losetower:
            QuestTypes quest_type1 = QuestTypes.Story;
            if (!QuestParam.TransSectionGotoTower(dataOfClass.Objectives[0].sval_base, out quest_type1))
              break;
            switch (quest_type1)
            {
              case QuestTypes.Gps:
                FlowNode_GameObject.ActivateOutputLinks((Component) this, 1007);
                return;
              case QuestTypes.Beginner:
                FlowNode_GameObject.ActivateOutputLinks((Component) this, 1028);
                return;
              default:
                if (quest_type1 != QuestTypes.Event)
                {
                  FlowNode_GameObject.ActivateOutputLinks((Component) this, 1026);
                  return;
                }
                goto case QuestTypes.Gps;
            }
          case TrophyConditionTypes.vs:
          case TrophyConditionTypes.vswin:
            if (player.CheckUnlock(UnlockTargets.MultiVS))
            {
              FlowNode_GameObject.ActivateOutputLinks((Component) this, 1027);
              break;
            }
            LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.MultiVS);
            break;
          case TrophyConditionTypes.read_tips:
          case TrophyConditionTypes.read_tips_count:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1029);
            break;
          case TrophyConditionTypes.up_conceptcard_level:
          case TrophyConditionTypes.up_conceptcard_level_target:
          case TrophyConditionTypes.limitbreak_conceptcard:
          case TrophyConditionTypes.limitbreak_conceptcard_target:
          case TrophyConditionTypes.up_conceptcard_trust:
          case TrophyConditionTypes.up_conceptcard_trust_target:
          case TrophyConditionTypes.max_conceptcard_trust:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1030);
            break;
          case TrophyConditionTypes.unlock_tobira_total:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1012);
            break;
          case TrophyConditionTypes.unlock_tobira_unit:
          case TrophyConditionTypes.envy_unlock_unit:
          case TrophyConditionTypes.sloth_unlock_unit:
          case TrophyConditionTypes.lust_unlock_unit:
          case TrophyConditionTypes.greed_unlock_unit:
          case TrophyConditionTypes.wrath_unlock_unit:
          case TrophyConditionTypes.gluttonny_unlock_unit:
          case TrophyConditionTypes.pride_unlock_unit:
            ChallengeMissionDetail.SetGoToUnitListState(dataOfClass);
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1012);
            break;
          case TrophyConditionTypes.send_present:
          case TrophyConditionTypes.friend_count:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1034);
            break;
          case TrophyConditionTypes.complete_event_mission_count:
            ChapterParam area1 = MonoSingleton<GameManager>.Instance.FindArea(dataOfClass.Objectives[0].sval_base);
            if (area1 == null)
            {
              if (LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.EventQuest))
                break;
              QuestParam.GotoEventListQuest((string) null);
              FlowNode_GameObject.ActivateOutputLinks((Component) this, 1007);
              break;
            }
            if (area1.IsArchiveQuest)
            {
              ArchiveParam archiveByArea2 = MonoSingleton<GameManager>.Instance.FindArchiveByArea(area1.iname);
              if (archiveByArea2 != null && archiveByArea2.IsAvailable() && MonoSingleton<GameManager>.Instance.Player.CheckUnlock(UnlockTargets.Archive))
              {
                if (MonoSingleton<GameManager>.Instance.Player.IsQuestArchiveOpenByArea(area1.iname))
                {
                  GlobalVars.SelectedQuestID = (string) null;
                  GlobalVars.SelectedChapter.Set(area1.iname);
                  GlobalVars.SelectedSection.Set("WD_DAILY");
                  GlobalVars.ReqEventPageListType = GlobalVars.EventQuestListType.EventQuestArchive;
                  FlowNode_Variable.Set("SHOW_CHAPTER", "0");
                  FlowNode_GameObject.ActivateOutputLinks((Component) this, 1007);
                  break;
                }
                UIUtility.ConfirmBox(LocalizedText.Get("sys.ARCHIVE_CHANGE_SCENE_CONFIRM"), (UIUtility.DialogResultEvent) (yes_button =>
                {
                  GlobalVars.SelectedSection.Set("WD_DAILY");
                  GlobalVars.ReqEventPageListType = GlobalVars.EventQuestListType.EventQuestArchive;
                  FlowNode_Variable.Set("SHOW_CHAPTER", "1");
                  FlowNode_GameObject.ActivateOutputLinks((Component) this, 1007);
                }), (UIUtility.DialogResultEvent) null);
                break;
              }
            }
            if (!EventQuestList.ChapterContainsPlayableQuest(area1, MonoSingleton<GameManager>.Instance.Chapters, MonoSingleton<GameManager>.Instance.Player.AvailableQuests, Network.GetServerTime()))
            {
              UIUtility.SystemMessage((string) null, LocalizedText.Get("sys.QUEST_UNAVAILABLE"), new UIUtility.DialogResultEvent(this.MsgBoxJumpToQuest));
              break;
            }
            if (LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.EventQuest))
              break;
            QuestParam.GotoEventListQuest(area1);
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1007);
            break;
          case TrophyConditionTypes.complete_ordeal_mission_count:
          case TrophyConditionTypes.clear_ordeal:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1031);
            break;
          case TrophyConditionTypes.view_news:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1033);
            break;
          case TrophyConditionTypes.makeunitandjoblevel:
            string[] strArray3 = ChallengeMissionDetail.SplitSvalByComma(dataOfClass);
            if (strArray3 != null && strArray3.Length > 0)
              ChallengeMissionDetail.SetGoToUnitListState(strArray3[0]);
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1009);
            break;
          case TrophyConditionTypes.consume_stamina:
          case TrophyConditionTypes.winquestunit:
          case TrophyConditionTypes.clear_questmission_count:
          case TrophyConditionTypes.winquestunit_support:
            if (!QuestParam.TransSectionGotoNormal())
              break;
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1001);
            break;
          case TrophyConditionTypes.consume_stamina_by_quest:
          case TrophyConditionTypes.clear_questmission_count_by_quest:
            TrophyObjective objective1 = dataOfClass.Objectives[0];
            if (objective1 == null || objective1.sval == null)
              break;
            int index1 = 0;
            while (index1 < objective1.sval.Count && !this.GotoQuests(objective1.sval[index1]))
              ++index1;
            break;
          case TrophyConditionTypes.consume_stamina_by_area:
          case TrophyConditionTypes.clear_questmission_count_by_area:
            TrophyObjective objective2 = dataOfClass.Objectives[0];
            if (objective2 == null || objective2.sval == null)
              break;
            for (int index2 = 0; index2 < objective2.sval.Count; ++index2)
            {
              ChapterParam area2 = MonoSingleton<GameManager>.Instance.FindArea(objective2.sval[index2]);
              if (area2 != null && area2.quests != null && this.GotoQuests(area2.quests.ToArray()))
                break;
            }
            break;
          case TrophyConditionTypes.consume_stamina_by_mode:
          case TrophyConditionTypes.clear_questmission_count_by_mode:
            TrophyObjective objective3 = dataOfClass.Objectives[0];
            if (objective3 == null)
              break;
            this.GotoQuestMode(QuestParam.GetQuestDifficulties(objective3.sval_base));
            break;
          case TrophyConditionTypes.winquestunit_by_quest:
          case TrophyConditionTypes.winquestunit_support_by_quest:
            TrophyObjective objective4 = dataOfClass.Objectives[0];
            if (objective4 == null || objective4.sval == null)
              break;
            if (objective4.SvalCount <= 1)
            {
              DebugUtility.LogError("トロフィー[" + objective4.Param.iname + "]のsvalにクエスト指定がありません.");
              break;
            }
            int index3 = 1;
            while (index3 < objective4.sval.Count && !this.GotoQuests(objective4.sval[index3]))
              ++index3;
            break;
          case TrophyConditionTypes.winquestunit_by_area:
          case TrophyConditionTypes.winquestunit_support_by_area:
            TrophyObjective objective5 = dataOfClass.Objectives[0];
            if (objective5 == null || objective5.sval == null)
              break;
            if (objective5.SvalCount <= 1)
            {
              DebugUtility.LogError("トロフィー[" + objective5.Param.iname + "]のsvalにエリア指定がありません.");
              break;
            }
            for (int index4 = 1; index4 < objective5.sval.Count; ++index4)
            {
              ChapterParam area3 = MonoSingleton<GameManager>.Instance.FindArea(objective5.sval[index4]);
              if (area3 != null && area3.quests != null && this.GotoQuests(area3.quests.ToArray()))
                break;
            }
            break;
          case TrophyConditionTypes.winquestunit_by_mode:
          case TrophyConditionTypes.winquestunit_support_by_mode:
            TrophyObjective objective6 = dataOfClass.Objectives[0];
            if (objective6 == null)
              break;
            if (objective6.SvalCount <= 1)
            {
              DebugUtility.LogError("トロフィー[" + objective6.Param.iname + "]のsvalに難易度指定がありません.");
              break;
            }
            this.GotoQuestMode(QuestParam.GetQuestDifficulties(objective6.sval[1]));
            break;
          case TrophyConditionTypes.join_guild:
          case TrophyConditionTypes.gvg:
            if (LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.Guild))
              break;
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1037);
            break;
          case TrophyConditionTypes.raidboss:
          case TrophyConditionTypes.raidboss_rescue_for_guildmember:
            if (MonoSingleton<GameManager>.Instance.MasterParam.GetRaidScheduleStatus() != RaidManager.RaidScheduleType.Close)
            {
              if (LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.Raid))
                break;
              FlowNode_GameObject.ActivateOutputLinks((Component) this, 1038);
              break;
            }
            UIUtility.SystemMessage((string) null, LocalizedText.Get("sys.RAID_ERROR_OUT_OF_PERIOD_MSG"), (UIUtility.DialogResultEvent) null);
            break;
          case TrophyConditionTypes.guildraid:
            if (LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.GuildRaid))
              break;
            if (!player.IsGuildAssign)
            {
              FlowNode_GameObject.ActivateOutputLinks((Component) this, 1037);
              break;
            }
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1039);
            break;
          case TrophyConditionTypes.over_total_combat_power:
          case TrophyConditionTypes.over_unit_combat_power:
          case TrophyConditionTypes.over_fire_combat_power:
          case TrophyConditionTypes.over_water_combat_power:
          case TrophyConditionTypes.over_thunder_combat_power:
          case TrophyConditionTypes.over_wind_combat_power:
          case TrophyConditionTypes.over_shine_combat_power:
          case TrophyConditionTypes.over_dark_combat_power:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1012);
            break;
          default:
            DebugUtility.Log(string.Format("未知の Trophy 条件 / {0}", (object) dataOfClass.Objectives[0].type));
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
            break;
        }
      }
    }

    private bool GotoQuests(QuestParam[] quests)
    {
      if (quests == null || quests.Length <= 0)
        return false;
      for (int index = 0; index < quests.Length; ++index)
      {
        if (this.GotoQuests(quests[index]))
          return true;
      }
      return false;
    }

    private bool GotoQuests(string iname)
    {
      return this.GotoQuests(MonoSingleton<GameManager>.Instance.FindQuest(iname));
    }

    private bool GotoQuests(QuestParam quest)
    {
      if (quest == null)
        return false;
      QuestTypes quest_type = QuestTypes.Story;
      if (!QuestParam.TransSectionGotoQuest(quest.iname, out quest_type, new UIUtility.DialogResultEvent(this.MsgBoxJumpToQuest)))
        return true;
      switch (quest_type)
      {
        case QuestTypes.Beginner:
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 1028);
          break;
        case QuestTypes.MultiGps:
label_9:
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 1005);
          break;
        case QuestTypes.GenesisStory:
        case QuestTypes.GenesisBoss:
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 1035);
          break;
        case QuestTypes.AdvanceStory:
        case QuestTypes.AdvanceBoss:
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 1036);
          break;
        default:
          switch (quest_type)
          {
            case QuestTypes.Event:
            case QuestTypes.Gps:
              FlowNode_GameObject.ActivateOutputLinks((Component) this, 1007);
              break;
            case QuestTypes.Tower:
              FlowNode_GameObject.ActivateOutputLinks((Component) this, 1026);
              break;
            default:
              if (quest_type != QuestTypes.Multi)
              {
                FlowNode_GameObject.ActivateOutputLinks((Component) this, 1001);
                break;
              }
              goto label_9;
          }
          break;
      }
      return true;
    }

    private void GotoQuestMode(QuestDifficulties difficulty)
    {
      switch (difficulty)
      {
        case QuestDifficulties.Normal:
          QuestParam.TransSectionGotoNormal();
          break;
        case QuestDifficulties.Elite:
          if (!QuestParam.TransSectionGotoElite(new UIUtility.DialogResultEvent(this.MsgBoxJumpToQuest)))
            return;
          break;
        case QuestDifficulties.Extra:
          if (!QuestParam.TransSectionGotoStoryExtra(new UIUtility.DialogResultEvent(this.MsgBoxJumpToQuest)))
            return;
          break;
      }
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 1001);
    }

    private void GotoShop(TrophyParam trophy)
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      EShopType type = EShopType.Normal;
      if (!string.IsNullOrEmpty(trophy.Objectives[0].sval_base))
      {
        char[] chArray = new char[1]{ ',' };
        string[] strArray = trophy.Objectives[0].sval_base.Split(chArray);
        type = !string.IsNullOrEmpty(strArray[0]) ? (EShopType) MonoSingleton<GameManager>.Instance.MasterParam.GetShopType(strArray[0]) : EShopType.Normal;
      }
      if (type >= EShopType.Normal && player.CheckUnlockShopType(type))
      {
        switch (type)
        {
          case EShopType.Normal:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1013);
            break;
          case EShopType.Tabi:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1014);
            break;
          case EShopType.Kimagure:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1015);
            break;
          case EShopType.Monozuki:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1016);
            break;
          case EShopType.Tour:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1017);
            break;
          case EShopType.Arena:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1018);
            break;
          case EShopType.Multi:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1019);
            break;
          case EShopType.AwakePiece:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1020);
            break;
          case EShopType.Artifact:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1021);
            break;
          case EShopType.Limited:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1040);
            break;
          case EShopType.Port:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 1041);
            break;
        }
      }
      if (type < EShopType.Normal)
        return;
      if (MonoSingleton<GameManager>.Instance.Player.CheckUnlockShopType(type))
        return;
      try
      {
        UnlockTargets unlockTargets = type.ToUnlockTargets();
        LevelLock.ShowLockMessage(player.Lv, player.VipRank, unlockTargets);
      }
      catch (Exception ex)
      {
      }
    }

    private void MsgBoxJumpToQuest(GameObject go)
    {
    }
  }
}
