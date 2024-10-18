// Decompiled with JetBrains decompiler
// Type: SRPG.EventBannerScroll
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
using UnityEngine.EventSystems;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(48, "Refreshed", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(49, "ItemEmpty", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(10, "PageNext", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(11, "PagePrev", FlowNode.PinTypes.Input, 4)]
  [FlowNode.Pin(12, "Select", FlowNode.PinTypes.Input, 5)]
  [FlowNode.Pin(50, "ToStory", FlowNode.PinTypes.Output, 6)]
  [FlowNode.Pin(51, "ToEvent", FlowNode.PinTypes.Output, 7)]
  [FlowNode.Pin(55, "ToMulti", FlowNode.PinTypes.Output, 8)]
  [FlowNode.Pin(52, "ToShop", FlowNode.PinTypes.Output, 9)]
  [FlowNode.Pin(53, "ToGacha", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(54, "ToURL", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(56, "ToArena", FlowNode.PinTypes.Output, 12)]
  [FlowNode.Pin(57, "ToPVP", FlowNode.PinTypes.Output, 13)]
  [FlowNode.Pin(58, "ToOrdeal", FlowNode.PinTypes.Output, 14)]
  [FlowNode.Pin(59, "ToBeginner", FlowNode.PinTypes.Output, 15)]
  [FlowNode.Pin(60, "ToAdvanceEvent", FlowNode.PinTypes.Output, 16)]
  [FlowNode.Pin(61, "ToGenesisEvent", FlowNode.PinTypes.Output, 17)]
  [FlowNode.Pin(62, "ToGenesisEventTop", FlowNode.PinTypes.Output, 18)]
  [FlowNode.Pin(101, "SceneChange", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(110, "Select(ポップアップ開く)", FlowNode.PinTypes.Input, 110)]
  [FlowNode.Pin(111, "Selected(ポップアップ開く)", FlowNode.PinTypes.Output, 111)]
  public class EventBannerScroll : SRPG_ScrollRect, IFlowInterface
  {
    private const int PIN_REFRESH = 1;
    private const int PIN_PAGE_NEXT = 10;
    private const int PIN_PAGE_PREV = 11;
    private const int PIN_SELECT = 12;
    private const int PIN_REFRESHED = 48;
    private const int PIN_EMPTY = 49;
    private const int PIN_TO_STORY = 50;
    private const int PIN_TO_EVENT = 51;
    private const int PIN_TO_SHOP = 52;
    private const int PIN_TO_GACHA = 53;
    private const int PIN_TO_URL = 54;
    private const int PIN_TO_MULTI = 55;
    private const int PIN_TO_ARENA = 56;
    private const int PIN_TO_PVP = 57;
    private const int PIN_TO_ORDEAL = 58;
    private const int PIN_TO_BEGINNER = 59;
    private const int PIN_TO_ADVANCE = 60;
    private const int PIN_TO_GENESIS = 61;
    private const int PIN_TO_GENESIS_TOP = 62;
    private const int PIN_OUT_SCENE_CHANGE = 101;
    private const int PIN_OPEN_POPUP = 110;
    private const int PIN_OUT_OPEN_POPUP = 111;
    private const string BannerPathOfNormals = "Banners/";
    private const string BannerPathOfShop = "LimitedShopBanner/";
    public ToggleGroup PageToggleGroup;
    public GameObject TemplateBannerNormal;
    public GameObject TemplateBannerShop;
    public GameObject TemplatePageIcon;
    public float Interval = 4f;
    private bool mDragging;
    private int mPage;
    private float mElapsed;
    private IEnumerator mMove;

    private int PageCount => ((Transform) this.content).childCount - 1;

    private int PageNow => this.mPage;

    void IFlowInterface.Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          if (this.Refresh())
          {
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 48);
            break;
          }
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 49);
          break;
        case 10:
          if (this.mMove != null)
            break;
          this.mMove = this.movePage(this.mPage, this.mPage + 1);
          break;
        case 11:
          if (this.mMove != null)
            break;
          this.mMove = this.movePage(this.mPage, this.mPage - 1);
          break;
        case 12:
          BannerParam currentPageBannerParam1 = this.getCurrentPageBannerParam();
          PlayerData player = MonoSingleton<GameManager>.Instance.Player;
          bool flag = false;
          if (currentPageBannerParam1 != null)
          {
            switch (currentPageBannerParam1.type)
            {
              case BannerType.storyQuest:
                QuestParam quest1 = MonoSingleton<GameManager>.Instance.FindQuest(currentPageBannerParam1.sval);
                if (LevelLock.IsNeedCheckUnlockConds(quest1))
                {
                  UnlockTargets targetByQuestId = LevelLock.GetTargetByQuestId(quest1.iname, UnlockTargets.EventQuest);
                  if (LevelLock.ShowLockMessage(player.Lv, player.VipRank, targetByQuestId))
                    return;
                }
                if (this.setQuestVariables(currentPageBannerParam1.sval, true))
                {
                  GlobalVars.SelectedQuestID = (string) null;
                  FlowNode_GameObject.ActivateOutputLinks((Component) this, 50);
                  flag = true;
                  break;
                }
                break;
              case BannerType.eventQuest:
                QuestParam quest2 = MonoSingleton<GameManager>.Instance.FindQuest(currentPageBannerParam1.sval);
                if (LevelLock.IsNeedCheckUnlockConds(quest2))
                {
                  UnlockTargets targetByQuestId = LevelLock.GetTargetByQuestId(quest2.iname, UnlockTargets.EventQuest);
                  if (LevelLock.ShowLockMessage(player.Lv, player.VipRank, targetByQuestId))
                    return;
                }
                QuestParam.GotoEventListQuest(currentPageBannerParam1.sval);
                FlowNode_GameObject.ActivateOutputLinks((Component) this, 51);
                flag = true;
                break;
              case BannerType.multiQuest:
                if (player.CheckUnlock(UnlockTargets.MultiPlay))
                {
                  FlowNode_GameObject.ActivateOutputLinks((Component) this, 55);
                  flag = true;
                  break;
                }
                LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.MultiPlay);
                break;
              case BannerType.gacha:
                GlobalVars.SelectedGachaTableId = currentPageBannerParam1.sval;
                FlowNode_GameObject.ActivateOutputLinks((Component) this, 53);
                flag = true;
                break;
              case BannerType.shop:
                FlowNode_GameObject.ActivateOutputLinks((Component) this, 52);
                flag = true;
                break;
              case BannerType.url:
                if (!string.IsNullOrEmpty(currentPageBannerParam1.sval))
                  Application.OpenURL(currentPageBannerParam1.sval);
                FlowNode_GameObject.ActivateOutputLinks((Component) this, 54);
                break;
              case BannerType.towerQuest:
                if (player.CheckUnlock(UnlockTargets.TowerQuest))
                {
                  QuestParam.GotoEventListChapter();
                  GlobalVars.ReqEventPageListType = GlobalVars.EventQuestListType.Tower;
                  FlowNode_GameObject.ActivateOutputLinks((Component) this, 51);
                  flag = true;
                  break;
                }
                LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.TowerQuest);
                break;
              case BannerType.arena:
                if (player.CheckUnlock(UnlockTargets.Arena))
                {
                  FlowNode_GameObject.ActivateOutputLinks((Component) this, 56);
                  flag = true;
                  break;
                }
                LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.Arena);
                break;
              case BannerType.pvp:
                if (player.CheckUnlock(UnlockTargets.MultiVS))
                {
                  FlowNode_GameObject.ActivateOutputLinks((Component) this, 57);
                  flag = true;
                  break;
                }
                LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.MultiVS);
                break;
              case BannerType.ordealQuest:
                if (player.CheckUnlock(UnlockTargets.Ordeal))
                {
                  FlowNode_GameObject.ActivateOutputLinks((Component) this, 58);
                  flag = true;
                  break;
                }
                LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.Ordeal);
                break;
              case BannerType.beginner:
                FlowNode_GameObject.ActivateOutputLinks((Component) this, 59);
                flag = true;
                break;
              case BannerType.advanceQuest:
                if (LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.EventQuest))
                  return;
                QuestParam quest3 = MonoSingleton<GameManager>.Instance.FindQuest(currentPageBannerParam1.sval);
                if (quest3 == null || quest3.Chapter == null)
                  return;
                QuestParam.GotoAdvanceListQuest(quest3.Chapter);
                FlowNode_GameObject.ActivateOutputLinks((Component) this, 60);
                flag = true;
                break;
              case BannerType.genesisQuest:
                if (LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.EventQuest))
                  return;
                QuestParam quest4 = MonoSingleton<GameManager>.Instance.FindQuest(currentPageBannerParam1.sval);
                if (quest4 == null || quest4.Chapter == null)
                  return;
                if (!quest4.IsAvailable())
                {
                  FlowNode_GameObject.ActivateOutputLinks((Component) this, 62);
                  flag = true;
                  break;
                }
                QuestParam.GotoGenesisListQuest(quest4.Chapter);
                FlowNode_GameObject.ActivateOutputLinks((Component) this, 61);
                flag = true;
                break;
            }
          }
          if (flag)
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
          this.SendLogBannerName(currentPageBannerParam1.banr_sprite);
          break;
        case 110:
          BannerParam currentPageBannerParam2 = this.getCurrentPageBannerParam();
          if (currentPageBannerParam2 == null)
            break;
          EventPopup.InitTabType = currentPageBannerParam2.TabType;
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 111);
          break;
      }
    }

    private void SendLogBannerName(string banr_sprite)
    {
      FlowNode_SendLogMessage.SendLogGenerator dict = new FlowNode_SendLogMessage.SendLogGenerator();
      dict.AddCommon(true, false, false, true);
      dict.Add("msg", "banner");
      dict.Add("Image", banr_sprite);
      FlowNode_SendLogMessage.SendLogMessage(dict, "HomeBanner");
    }

    protected virtual void Start()
    {
      ((UIBehaviour) this).Start();
      this.inertia = false;
      this.movementType = (ScrollRect.MovementType) 0;
      this.horizontal = true;
      this.vertical = false;
      // ISSUE: method pointer
      ((UnityEvent<Vector2>) this.onValueChanged).AddListener(new UnityAction<Vector2>((object) this, __methodptr(OnValueChanged)));
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
      base.OnBeginDrag(eventData);
      this.mDragging = true;
      this.mElapsed = 0.0f;
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
      this.mDragging = false;
      base.OnEndDrag(eventData);
    }

    private void Update()
    {
      if (this.mDragging || this.mMove != null || ((Transform) this.content).childCount <= 1)
        return;
      this.mElapsed += Time.deltaTime;
      if ((double) this.mElapsed < (double) this.Interval)
        return;
      this.mMove = this.movePage(this.mPage, this.mPage + 1);
    }

    protected override void LateUpdate()
    {
      if (this.PageCount > 0)
      {
        if (this.mMove != null)
        {
          if (!this.mMove.MoveNext())
            this.mMove = (IEnumerator) null;
        }
        else if (!this.mDragging)
        {
          RectTransform[] rectTransformArray = new RectTransform[((Transform) this.content).childCount];
          for (int index = 0; index < ((Transform) this.content).childCount; ++index)
            rectTransformArray[index] = ((Transform) this.content).GetChild(index) as RectTransform;
          float num = 0.0f;
          Vector2 anchoredPosition = this.content.anchoredPosition;
          for (int index = 0; index < rectTransformArray.Length; ++index)
          {
            Rect rect = rectTransformArray[index].rect;
            float width = ((Rect) ref rect).width;
            if ((double) num + (double) width / 2.0 > -(double) anchoredPosition.x)
            {
              anchoredPosition.x = Mathf.Lerp(anchoredPosition.x, -num, 0.1f);
              this.SetContentAnchoredPosition(anchoredPosition);
              int page = index % this.PageCount;
              if (this.mPage != page)
              {
                this.onPageChanged(page);
                break;
              }
              break;
            }
            num += width;
          }
        }
      }
      base.LateUpdate();
    }

    private bool Refresh()
    {
      while (((Transform) this.content).childCount != 0)
        UnityEngine.Object.Destroy((UnityEngine.Object) ((Transform) this.content).GetChild(0));
      while (((Component) this.PageToggleGroup).transform.childCount != 0)
        UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) this.PageToggleGroup).transform.GetChild(0));
      BannerParam[] bannerParamArray = this.makeValidBannerParams();
      if (bannerParamArray.Length != 0)
      {
        for (int index1 = 0; index1 < bannerParamArray.Length + 1; ++index1)
        {
          int index2 = index1 % bannerParamArray.Length;
          GameObject gameObject1 = bannerParamArray[index2].type != BannerType.shop ? UnityEngine.Object.Instantiate<GameObject>(this.TemplateBannerNormal) : UnityEngine.Object.Instantiate<GameObject>(this.TemplateBannerShop);
          Vector3 localScale1 = gameObject1.transform.localScale;
          gameObject1.transform.SetParent((Transform) this.content);
          gameObject1.transform.localScale = localScale1;
          gameObject1.SetActive(true);
          DataSource.Bind<BannerParam>(gameObject1, bannerParamArray[index2]);
          if (index1 < bannerParamArray.Length)
          {
            GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.TemplatePageIcon);
            Vector3 localScale2 = gameObject1.transform.localScale;
            gameObject2.transform.SetParent(((Component) this.PageToggleGroup).transform);
            gameObject2.transform.localScale = localScale2;
            gameObject2.SetActive(true);
            if (index1 == 0)
              gameObject2.GetComponent<Toggle>().isOn = true;
          }
        }
      }
      this.mPage = 0;
      this.mElapsed = 0.0f;
      ((Component) this.PageToggleGroup).gameObject.SetActive(bannerParamArray.Length > 1);
      return bannerParamArray.Length != 0;
    }

    private BannerParam[] makeValidBannerParams()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      BannerParam[] banners = instance.MasterParam.Banners;
      if (banners == null)
        return new BannerParam[0];
      List<BannerParam> bannerParamList = new List<BannerParam>();
      GachaParam[] gachas = instance.Gachas;
      QuestParam[] availableQuests = instance.Player.AvailableQuests;
      QuestParam questParam1 = (QuestParam) null;
      QuestParam lastStoryQuest = instance.Player.FindLastStoryQuest();
      long serverTime = Network.GetServerTime();
      DateTime now = TimeManager.FromUnixTime(serverTime);
      for (int index = 0; index < banners.Length; ++index)
      {
        BannerParam banner = banners[index];
        if (!string.IsNullOrEmpty(banner.banner) && bannerParamList.FindIndex((Predicate<BannerParam>) (p => p.iname == banner.iname)) == -1 && banner.IsHomeBanner)
        {
          if (banner.type == BannerType.shop)
          {
            if (instance.IsLimitedShopOpen)
            {
              if (instance.LimitedShopList != null && !string.IsNullOrEmpty(banner.sval))
              {
                JSON_ShopListArray.Shops shops = Array.Find<JSON_ShopListArray.Shops>(instance.LimitedShopList, (Predicate<JSON_ShopListArray.Shops>) (p => p.gname == banner.sval));
                if (shops != null)
                {
                  banner.begin_at = TimeManager.FromUnixTime(shops.start).ToString();
                  banner.end_at = TimeManager.FromUnixTime(shops.end).ToString();
                  if (!banner.IsAvailablePeriod(now))
                    continue;
                }
                else
                  continue;
              }
              else if (!banner.IsAvailablePeriod(now))
                continue;
            }
            else
              continue;
          }
          else if (banner.type == BannerType.storyQuest)
          {
            if (lastStoryQuest != null)
            {
              if (string.IsNullOrEmpty(banner.sval))
              {
                questParam1 = lastStoryQuest;
                if (!banner.IsAvailablePeriod(now))
                  continue;
              }
              else
              {
                QuestParam questParam2 = Array.Find<QuestParam>(availableQuests, (Predicate<QuestParam>) (p => p.iname == banner.sval));
                if (questParam2 == null || questParam2.iname != lastStoryQuest.iname && questParam2.state == QuestStates.New)
                  questParam2 = lastStoryQuest;
                if (!questParam2.IsDateUnlock(serverTime))
                  continue;
              }
            }
            else
              continue;
          }
          else if (banner.type == BannerType.eventQuest || banner.type == BannerType.multiQuest || banner.type == BannerType.advanceQuest)
          {
            if (!string.IsNullOrEmpty(banner.sval))
            {
              QuestParam questParam3 = Array.Find<QuestParam>(availableQuests, (Predicate<QuestParam>) (p => p.iname == banner.sval));
              if (questParam3 == null || !questParam3.IsDateUnlock(serverTime))
                continue;
            }
            else if (!banner.IsAvailablePeriod(now))
              continue;
          }
          else if (banner.type == BannerType.genesisQuest)
          {
            if (!string.IsNullOrEmpty(banner.sval))
            {
              QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(banner.sval);
              if (quest == null || !quest.IsDateUnlock(serverTime))
                continue;
            }
          }
          else if (banner.type == BannerType.gacha)
          {
            if (!string.IsNullOrEmpty(banner.sval))
            {
              GachaParam gachaParam = Array.Find<GachaParam>(gachas, (Predicate<GachaParam>) (p => p.iname == banner.sval));
              if (gachaParam != null)
              {
                banner.begin_at = TimeManager.FromUnixTime(gachaParam.startat).ToString();
                banner.end_at = TimeManager.FromUnixTime(gachaParam.endat).ToString();
                if (!banner.IsAvailablePeriod(now))
                  continue;
              }
              else
                continue;
            }
            else if (!banner.IsAvailablePeriod(now))
              continue;
          }
          else if (banner.type == BannerType.url)
          {
            if (string.IsNullOrEmpty(banner.sval) || !banner.IsAvailablePeriod(now))
              continue;
          }
          else if (banner.type == BannerType.towerQuest)
          {
            if (!banner.IsAvailablePeriod(now))
              continue;
          }
          else if (banner.type == BannerType.arena)
          {
            if (!banner.IsAvailablePeriod(now))
              continue;
          }
          else if (banner.type == BannerType.pvp)
          {
            if (!banner.IsAvailablePeriod(now))
              continue;
          }
          else if (banner.type == BannerType.ordealQuest)
          {
            if (!string.IsNullOrEmpty(banner.sval))
            {
              QuestParam questParam4 = Array.Find<QuestParam>(availableQuests, (Predicate<QuestParam>) (p => p.iname == banner.sval));
              if (questParam4 == null || !questParam4.IsDateUnlock(serverTime))
                continue;
            }
            else if (!banner.IsAvailablePeriod(now))
              continue;
          }
          bannerParamList.Add(banner);
        }
      }
      for (int index1 = 0; index1 < bannerParamList.Count - 1; ++index1)
      {
        for (int index2 = index1 + 1; index2 < bannerParamList.Count; ++index2)
        {
          if (bannerParamList[index1].priority > bannerParamList[index2].priority)
          {
            BannerParam bannerParam = bannerParamList[index1];
            bannerParamList[index1] = bannerParamList[index2];
            bannerParamList[index2] = bannerParam;
          }
        }
      }
      return bannerParamList.ToArray();
    }

    [DebuggerHidden]
    private IEnumerator movePage(int from, int to)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new EventBannerScroll.\u003CmovePage\u003Ec__Iterator0()
      {
        to = to,
        from = from,
        \u0024this = this
      };
    }

    private void OnValueChanged(Vector2 value)
    {
      Rect rect1 = this.content.rect;
      double width1 = (double) ((Rect) ref rect1).width;
      Rect rect2 = this.viewport.rect;
      double width2 = (double) ((Rect) ref rect2).width;
      float num1 = (float) (width1 - width2);
      float num2 = -this.content.anchoredPosition.x;
      if ((double) num2 > (double) num1)
      {
        this.SetContentAnchoredPosition(new Vector2(-(num2 % num1), 0.0f));
      }
      else
      {
        if ((double) num2 >= 0.0)
          return;
        this.SetContentAnchoredPosition(new Vector2((float) -((double) num1 + (double) num2), 0.0f));
      }
    }

    private void onPageChanged(int page)
    {
      this.mPage = page;
      int num = 0;
      foreach (Toggle componentsInChild in ((Component) this.PageToggleGroup).GetComponentsInChildren<Toggle>())
      {
        componentsInChild.isOn = num == this.mPage;
        ++num;
      }
    }

    private float getPageOffset(int page)
    {
      RectTransform[] rectTransformArray = new RectTransform[((Transform) this.content).childCount];
      for (int index = 0; index < ((Transform) this.content).childCount; ++index)
        rectTransformArray[index] = ((Transform) this.content).GetChild(index) as RectTransform;
      float pageOffset = 0.0f;
      for (int index = 0; index < rectTransformArray.Length; ++index)
      {
        if (index == page)
          return pageOffset;
        double num = (double) pageOffset;
        Rect rect = rectTransformArray[index].rect;
        double width = (double) ((Rect) ref rect).width;
        pageOffset = (float) (num + width);
      }
      return 0.0f;
    }

    private BannerParam getCurrentPageBannerParam()
    {
      if (this.mPage >= ((Transform) this.content).childCount)
        return (BannerParam) null;
      DataSource component = ((Component) ((Transform) this.content).GetChild(this.mPage)).GetComponent<DataSource>();
      return UnityEngine.Object.op_Equality((UnityEngine.Object) component, (UnityEngine.Object) null) ? (BannerParam) null : component.FindDataOfClass<BannerParam>((BannerParam) null);
    }

    private float decelerate(float value)
    {
      return (float) (1.0 - (1.0 - (double) value) * (1.0 - (double) value) * (1.0 - (double) value));
    }

    private bool setQuestVariables(string questId, bool story)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      QuestParam[] availableQuests = instance.Player.AvailableQuests;
      for (int index = 0; index < availableQuests.Length; ++index)
      {
        if (availableQuests[index].iname == questId)
        {
          GlobalVars.SelectedSection.Set(availableQuests[index].Chapter.section);
          GlobalVars.SelectedChapter.Set(availableQuests[index].ChapterID);
          return true;
        }
      }
      if (story)
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
  }
}
