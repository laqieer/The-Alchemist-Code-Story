// Decompiled with JetBrains decompiler
// Type: SRPG.EventPopup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(100, "Select", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(50, "ToStory", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(51, "ToEvent", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(52, "ToMulti", FlowNode.PinTypes.Output, 3)]
  [FlowNode.Pin(53, "ToShop", FlowNode.PinTypes.Output, 4)]
  [FlowNode.Pin(54, "ToGacha", FlowNode.PinTypes.Output, 5)]
  [FlowNode.Pin(55, "ToURL", FlowNode.PinTypes.Output, 6)]
  [FlowNode.Pin(56, "ToArena", FlowNode.PinTypes.Output, 7)]
  [FlowNode.Pin(57, "ToPVP", FlowNode.PinTypes.Output, 8)]
  [FlowNode.Pin(58, "ToOrdeal", FlowNode.PinTypes.Output, 9)]
  [FlowNode.Pin(59, "ToBeginner", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(60, "ToAdvance", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(61, "ToGenesis", FlowNode.PinTypes.Output, 12)]
  [FlowNode.Pin(62, "ToGenesisTop", FlowNode.PinTypes.Output, 13)]
  [FlowNode.Pin(63, "ToGuild", FlowNode.PinTypes.Output, 14)]
  public class EventPopup : MonoBehaviour, IFlowInterface
  {
    public const int INPUT_BANNER_SELECT = 100;
    public const int OUTPUT_BANNER_TO_STORY = 50;
    public const int OUTPUT_BANNER_TO_EVENT = 51;
    public const int OUTPUT_BANNER_TO_MULTI = 52;
    public const int OUTPUT_BANNER_TO_SHOP = 53;
    public const int OUTPUT_BANNER_TO_GACHA = 54;
    public const int OUTPUT_BANNER_TO_URL = 55;
    public const int OUTPUT_BANNER_TO_ARENA = 56;
    public const int OUTPUT_BANNER_TO_PVP = 57;
    public const int OUTPUT_BANNER_TO_ORDEAL = 58;
    public const int OUTPUT_BANNER_TO_BEGINNER = 59;
    public const int OUTPUT_BANNER_TO_ADVANCE = 60;
    public const int OUTPUT_BANNER_TO_GENESIS = 61;
    public const int OUTPUT_BANNER_TO_GENESIS_TOP = 62;
    public const int OUTPUT_BANNER_TO_GUILD_TOP = 63;
    [SerializeField]
    private GameObject EventBannerTemplate;
    [SerializeField]
    private Transform ListRoot;
    [SerializeField]
    private ContentScroller ScrollObj;
    [SerializeField]
    private ToggleGroup TabGroup;
    [SerializeField]
    private Toggle EventTab;
    [SerializeField]
    private Toggle CampaignTab;
    [SerializeField]
    private Toggle InfomationTab;
    private List<GameObject> m_EventBannerList = new List<GameObject>();
    private BannerParam[] m_EventList;
    private BannerParam[] m_CampaignList;
    private BannerParam[] m_InfomationList;
    public static BannerTabType InitTabType = BannerTabType.infomationTab;

    public void Activated(int pinID)
    {
      if (pinID != 100)
        return;
      this.OnSelect();
    }

    private void Awake()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.EventBannerTemplate, (UnityEngine.Object) null))
        return;
      this.EventBannerTemplate.SetActive(false);
    }

    private void Start()
    {
      if (EventPopup.InitTabType == BannerTabType.none)
        EventPopup.InitTabType = BannerTabType.infomationTab;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.EventTab, (UnityEngine.Object) null))
      {
        this.EventTab.isOn = EventPopup.InitTabType == BannerTabType.eventTab;
        // ISSUE: method pointer
        ((UnityEvent<bool>) this.EventTab.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(OnSelectTab)));
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CampaignTab, (UnityEngine.Object) null))
      {
        this.CampaignTab.isOn = EventPopup.InitTabType == BannerTabType.campaignTab;
        // ISSUE: method pointer
        ((UnityEvent<bool>) this.CampaignTab.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(OnSelectTab)));
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.InfomationTab, (UnityEngine.Object) null))
      {
        this.InfomationTab.isOn = EventPopup.InitTabType == BannerTabType.infomationTab;
        // ISSUE: method pointer
        ((UnityEvent<bool>) this.InfomationTab.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(OnSelectTab)));
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) MonoSingleton<GameManager>.Instance, (UnityEngine.Object) null))
        return;
      this.InitBannerList();
      this.Setup(this.GetBannerList(EventPopup.InitTabType));
    }

    private void OnDestroy() => EventPopup.InitTabType = BannerTabType.infomationTab;

    private void InitBannerList()
    {
      List<BannerParam> bannerParamList = new List<BannerParam>();
      bannerParamList.AddRange((IEnumerable<BannerParam>) EventPopup.MakeValidBannerParams(false));
      List<BannerParam> all1 = bannerParamList.FindAll((Predicate<BannerParam>) (banner => banner.TabType == BannerTabType.eventTab));
      if (all1 != null && all1.Count > 0)
        this.m_EventList = all1.ToArray();
      List<BannerParam> all2 = bannerParamList.FindAll((Predicate<BannerParam>) (banner => banner.TabType == BannerTabType.campaignTab));
      if (all2 != null && all2.Count > 0)
        this.m_CampaignList = all2.ToArray();
      List<BannerParam> all3 = bannerParamList.FindAll((Predicate<BannerParam>) (banner => banner.TabType == BannerTabType.infomationTab || banner.TabType == BannerTabType.none));
      if (all3 != null && all3.Count > 0)
        this.m_InfomationList = all3.ToArray();
      bannerParamList.Clear();
      all3.Clear();
    }

    private BannerParam[] GetBannerList(BannerTabType type)
    {
      BannerParam[] bannerList = (BannerParam[]) null;
      switch (type)
      {
        case BannerTabType.eventTab:
          bannerList = this.m_EventList;
          break;
        case BannerTabType.campaignTab:
          bannerList = this.m_CampaignList;
          break;
        case BannerTabType.infomationTab:
          bannerList = this.m_InfomationList;
          break;
      }
      return bannerList;
    }

    private void Setup(BannerParam[] _params)
    {
      GameUtility.DestroyGameObjects(this.m_EventBannerList);
      this.m_EventBannerList.Clear();
      if (_params == null || _params.Length <= 0)
        DebugUtility.Log("イベントバナーデータが存在しません");
      else if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.EventBannerTemplate, (UnityEngine.Object) null))
      {
        DebugUtility.LogError("テンプレートオブジェクトが指定されていません");
      }
      else
      {
        for (int index = 0; index < _params.Length; ++index)
        {
          BannerParam bannerParam = _params[index];
          int num = index;
          if (bannerParam != null)
          {
            GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.EventBannerTemplate);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
            {
              gameObject.transform.SetParent(this.ListRoot, false);
              EventPopupListItem component1 = gameObject.GetComponent<EventPopupListItem>();
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component1, (UnityEngine.Object) null))
                component1.SetupBannerParam(bannerParam);
              ButtonEvent component2 = gameObject.GetComponent<ButtonEvent>();
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component2, (UnityEngine.Object) null))
                component2.GetEvent("EVENTPOPUP_BANNER_SELECT")?.valueList.SetField("select", num);
              gameObject.SetActive(true);
              this.m_EventBannerList.Add(gameObject);
            }
          }
        }
      }
    }

    private void OnSelect()
    {
      this.Select((FlowNode_ButtonEvent.currentValue as SerializeValueList).GetInt("select"));
    }

    private void Select(int index)
    {
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) MonoSingleton<GameManager>.Instance))
        return;
      BannerParam[] bannerList = this.GetBannerList(this.GetCurrentTabType());
      if (bannerList == null)
        return;
      if (bannerList.Length < index)
      {
        DebugUtility.LogError("選択されたイベントバナーが正しくありません");
      }
      else
      {
        BannerParam bannerParam = bannerList[index];
        int pinID = -1;
        PlayerData player = MonoSingleton<GameManager>.Instance.Player;
        switch (bannerParam.type)
        {
          case BannerType.storyQuest:
            if (LevelLock.IsNeedCheckUnlockConds(MonoSingleton<GameManager>.Instance.FindQuest(bannerParam.sval)))
            {
              UnlockTargets targetByQuestId = LevelLock.GetTargetByQuestId(bannerParam.sval, UnlockTargets.EventQuest);
              if (LevelLock.ShowLockMessage(player.Lv, player.VipRank, targetByQuestId))
                return;
            }
            if (EventPopup.SetupQuestVariables(bannerParam.sval, true))
            {
              GlobalVars.SelectedQuestID = (string) null;
              pinID = 50;
              break;
            }
            break;
          case BannerType.eventQuest:
            UnlockTargets target = UnlockTargets.EventQuest;
            QuestParam quest1 = MonoSingleton<GameManager>.Instance.FindQuest(bannerParam.sval);
            if (quest1 != null && LevelLock.IsNeedCheckUnlockConds(quest1))
              target = LevelLock.GetTargetByQuestId(bannerParam.sval, UnlockTargets.EventQuest);
            if (LevelLock.ShowLockMessage(player.Lv, player.VipRank, target))
              return;
            QuestParam.GotoEventListQuest(bannerParam.sval);
            pinID = 51;
            break;
          case BannerType.multiQuest:
            if (player.CheckUnlock(UnlockTargets.MultiPlay))
            {
              pinID = 52;
              break;
            }
            LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.MultiPlay);
            break;
          case BannerType.gacha:
            GlobalVars.SelectedGachaTableId = bannerParam.sval;
            pinID = 54;
            break;
          case BannerType.shop:
            pinID = 53;
            break;
          case BannerType.url:
            if (!string.IsNullOrEmpty(bannerParam.sval))
              Application.OpenURL(bannerParam.sval);
            pinID = 55;
            break;
          case BannerType.towerQuest:
            if (player.CheckUnlock(UnlockTargets.TowerQuest))
            {
              QuestParam.GotoEventListChapter();
              GlobalVars.ReqEventPageListType = GlobalVars.EventQuestListType.Tower;
              pinID = 51;
              break;
            }
            LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.TowerQuest);
            break;
          case BannerType.arena:
            if (player.CheckUnlock(UnlockTargets.Arena))
            {
              pinID = 56;
              break;
            }
            LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.Arena);
            break;
          case BannerType.pvp:
            if (player.CheckUnlock(UnlockTargets.MultiVS))
            {
              pinID = 57;
              break;
            }
            LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.MultiVS);
            break;
          case BannerType.ordealQuest:
            if (player.CheckUnlock(UnlockTargets.Ordeal))
            {
              pinID = 58;
              break;
            }
            LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.Ordeal);
            break;
          case BannerType.beginner:
            pinID = 59;
            break;
          case BannerType.advanceQuest:
            if (LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.EventQuest))
              return;
            QuestParam quest2 = MonoSingleton<GameManager>.Instance.FindQuest(bannerParam.sval);
            if (quest2 == null || quest2.Chapter == null)
              return;
            QuestParam.GotoAdvanceListQuest(quest2.Chapter);
            pinID = 60;
            break;
          case BannerType.genesisQuest:
            if (LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.EventQuest))
              return;
            QuestParam quest3 = MonoSingleton<GameManager>.Instance.FindQuest(bannerParam.sval);
            if (quest3 == null || quest3.Chapter == null)
              return;
            if (!quest3.IsAvailable())
            {
              pinID = 62;
              break;
            }
            QuestParam.GotoGenesisListQuest(quest3.Chapter);
            pinID = 61;
            break;
          case BannerType.guild:
            if (LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.Guild))
              return;
            pinID = 63;
            break;
        }
        if (pinID == -1)
          return;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, pinID);
      }
    }

    public static bool SetupQuestVariables(string _questID, bool _is_story)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      QuestParam[] availableQuests = instance.Player.AvailableQuests;
      for (int index = 0; index < availableQuests.Length; ++index)
      {
        if (availableQuests[index].iname == _questID)
        {
          GlobalVars.SelectedSection.Set(availableQuests[index].Chapter.section);
          GlobalVars.SelectedChapter.Set(availableQuests[index].ChapterID);
          return true;
        }
      }
      if (_is_story)
      {
        QuestParam lastStoryQuest = instance.Player.FindLastStoryQuest();
        if (lastStoryQuest != null && lastStoryQuest.IsDateUnlock(Network.GetServerTime()))
        {
          GlobalVars.SelectedSection.Set(lastStoryQuest.Chapter.section);
          GlobalVars.SelectedChapter.Set(lastStoryQuest.ChapterID);
          return true;
        }
      }
      return false;
    }

    public static BannerParam[] MakeValidBannerParams(bool _is_home_banner = true)
    {
      List<BannerParam> bannerParamList = new List<BannerParam>();
      GameManager instance = MonoSingleton<GameManager>.Instance;
      BannerParam[] banners = instance.MasterParam.Banners;
      if (banners == null)
      {
        DebugUtility.LogError("バナーの設定がありません、有効なバナーを1つ以上設定してください");
        return (BannerParam[]) null;
      }
      QuestParam lastStoryQuest = instance.Player.FindLastStoryQuest();
      QuestParam[] availableQuests = instance.Player.AvailableQuests;
      long serverTime = Network.GetServerTime();
      DateTime now = TimeManager.FromUnixTime(serverTime);
      for (int index = 0; index < banners.Length; ++index)
      {
        BannerParam param = banners[index];
        bool flag = true;
        if (param != null && !string.IsNullOrEmpty(param.banner) && bannerParamList.FindIndex((Predicate<BannerParam>) (p => p.iname == param.iname)) == -1 && (!_is_home_banner || param.IsHomeBanner))
        {
          if (param.type == BannerType.shop)
          {
            if (instance.IsLimitedShopOpen)
            {
              if (instance.LimitedShopList != null && !string.IsNullOrEmpty(param.sval))
              {
                JSON_ShopListArray.Shops shops = Array.Find<JSON_ShopListArray.Shops>(instance.LimitedShopList, (Predicate<JSON_ShopListArray.Shops>) (p => p.gname == param.sval));
                if (shops != null)
                {
                  param.begin_at = TimeManager.FromUnixTime(shops.start).ToString();
                  param.end_at = TimeManager.FromUnixTime(shops.end).ToString();
                }
                else
                  continue;
              }
            }
            else
              continue;
          }
          else if (param.type == BannerType.storyQuest)
          {
            flag = false;
            if (lastStoryQuest != null)
            {
              QuestParam questParam;
              if (string.IsNullOrEmpty(param.sval))
              {
                questParam = lastStoryQuest;
              }
              else
              {
                questParam = Array.Find<QuestParam>(availableQuests, (Predicate<QuestParam>) (p => p.iname == param.sval));
                if (questParam == null || questParam.iname != lastStoryQuest.iname && questParam.state == QuestStates.New)
                  questParam = lastStoryQuest;
              }
              if (!questParam.IsDateUnlock(serverTime))
                continue;
            }
            else
              continue;
          }
          else if (param.type == BannerType.eventQuest || param.type == BannerType.multiQuest)
          {
            if (!string.IsNullOrEmpty(param.sval))
            {
              QuestParam questParam = Array.Find<QuestParam>(availableQuests, (Predicate<QuestParam>) (p => p.iname == param.sval));
              if (questParam == null || !questParam.IsDateUnlock(serverTime))
                continue;
            }
          }
          else if (param.type != BannerType.towerQuest && param.type != BannerType.gacha)
          {
            if (param.type == BannerType.url)
            {
              if (string.IsNullOrEmpty(param.sval))
                continue;
            }
            else if (param.type != BannerType.arena && param.type != BannerType.pvp && param.type != BannerType.ordealQuest)
            {
              if (param.type == BannerType.advanceQuest)
              {
                if (!string.IsNullOrEmpty(param.sval))
                {
                  QuestParam questParam = Array.Find<QuestParam>(availableQuests, (Predicate<QuestParam>) (p => p.iname == param.sval));
                  if (questParam == null || !questParam.IsDateUnlock(serverTime))
                    continue;
                }
              }
              else if (param.type == BannerType.genesisQuest && !string.IsNullOrEmpty(param.sval))
              {
                QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(param.sval);
                if (quest == null || !quest.IsDateUnlock(serverTime))
                  continue;
              }
            }
          }
          if (!flag || param.IsAvailablePeriod(now))
            bannerParamList.Add(param);
        }
      }
      bannerParamList.Sort((Comparison<BannerParam>) ((a, b) => a.priority - b.priority));
      return bannerParamList.ToArray();
    }

    private void OnSelectTab(bool is_on)
    {
      if (!is_on)
        return;
      this.Setup(this.GetBannerList(this.GetCurrentTabType()));
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ScrollObj, (UnityEngine.Object) null))
        return;
      this.ScrollObj.SetNormalizedPosition(Vector2.one, true);
    }

    private BannerTabType GetCurrentTabType()
    {
      BannerTabType currentTabType = BannerTabType.eventTab;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TabGroup, (UnityEngine.Object) null))
      {
        List<Toggle> list = this.TabGroup.ActiveToggles().ToList<Toggle>();
        if (list.Count == 1)
        {
          Toggle toggle = list[0];
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) toggle, (UnityEngine.Object) null))
          {
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.EventTab, (UnityEngine.Object) null) && UnityEngine.Object.op_Equality((UnityEngine.Object) toggle, (UnityEngine.Object) this.EventTab))
              currentTabType = BannerTabType.eventTab;
            else if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CampaignTab, (UnityEngine.Object) null) && UnityEngine.Object.op_Equality((UnityEngine.Object) toggle, (UnityEngine.Object) this.CampaignTab))
              currentTabType = BannerTabType.campaignTab;
            else if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.InfomationTab, (UnityEngine.Object) null) && UnityEngine.Object.op_Equality((UnityEngine.Object) toggle, (UnityEngine.Object) this.InfomationTab))
              currentTabType = BannerTabType.infomationTab;
          }
        }
      }
      return currentTabType;
    }
  }
}
