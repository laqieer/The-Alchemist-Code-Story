// Decompiled with JetBrains decompiler
// Type: SRPG.GuildRaidRankingWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(10, "ギルドランキング選択", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(11, "個人トータルランキング選択", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(12, "個人BOSSランキング選択", FlowNode.PinTypes.Input, 12)]
  [FlowNode.Pin(200, "個人トータルランキング読み込み", FlowNode.PinTypes.Output, 200)]
  [FlowNode.Pin(201, "個人BOSS読み込み", FlowNode.PinTypes.Output, 201)]
  [FlowNode.Pin(202, "ギルドランキング追加読み込み", FlowNode.PinTypes.Output, 202)]
  [FlowNode.Pin(203, "個人トータルランキング追加読み込み", FlowNode.PinTypes.Output, 203)]
  [FlowNode.Pin(204, "個人BOSS追加読み込み", FlowNode.PinTypes.Output, 204)]
  public class GuildRaidRankingWindow : MonoBehaviour, IFlowInterface
  {
    private const int PIN_INPUT_SELECT_GUILD = 10;
    private const int PIN_INPUT_SELECT_MEMBER = 11;
    private const int PIN_INPUT_SELECT_BOSS = 12;
    private const int PIN_OUTPUT_REFRESHTOTAL = 200;
    private const int PIN_OUTPUT_REFRESHBOSS = 201;
    private const int PIN_OUTPUT_REFRESHADDGUILD = 202;
    private const int PIN_OUTPUT_REFRESHADDTOTAL = 203;
    private const int PIN_OUTPUT_REFRESHADDBOSS = 204;
    private GuildRaidRankingWindow.SelectType mSelectType;
    private GuildRaidRankingWindow.BossType mBossType;
    [SerializeField]
    private GameObject mGuildObject;
    [SerializeField]
    private GameObject mGuildRankingTemplate;
    [SerializeField]
    private GameObject mMemberObject;
    [SerializeField]
    private GameObject mMemberTotalViewObject;
    [SerializeField]
    private GameObject mMemberBossViewObject;
    [SerializeField]
    private GameObject mMemberRankingTemplate;
    [SerializeField]
    private GameObject mMemberHistoryTemplate;
    [SerializeField]
    private GameObject mMemberRankingBossTemplate;
    [SerializeField]
    private GameObject mBossIcon;
    [SerializeField]
    private Text mBossName;
    [SerializeField]
    private Text mBossStage;
    [SerializeField]
    private GameObject mGuildButton;
    [SerializeField]
    private GameObject mMemberButton;
    [SerializeField]
    private Button mPagePrev;
    [SerializeField]
    private Button mPageNext;
    [SerializeField]
    private GameObject mTabObject;
    [SerializeField]
    private GameObject mTabTotal;
    [SerializeField]
    private GameObject mTabBoss01;
    [SerializeField]
    private GameObject mTabBoss02;
    [SerializeField]
    private GameObject mTabBoss03;
    [SerializeField]
    private GameObject mTabBoss04;
    [SerializeField]
    private GameObject mTabBoss05;
    [SerializeField]
    private float mSelScale = 1.1f;
    [SerializeField]
    private float mNonSelScale = 0.9f;
    [SerializeField]
    private ContentController mContentController;
    [SerializeField]
    private SRPG_ScrollRect ScrollGuild;
    [SerializeField]
    private RectTransform ScrollContentGuild;
    [SerializeField]
    private SRPG_ScrollRect ScrollTotal;
    [SerializeField]
    private RectTransform ScrollContentTotal;
    [SerializeField]
    private SRPG_ScrollRect ScrollBoss;
    [SerializeField]
    private RectTransform ScrollContentBoss;
    private List<GameObject> mRankingList;
    private GameObject mCurrentTab;
    private List<GuildRaidRankingGuildParam> mGuildRaidRankingGuildParamList = new List<GuildRaidRankingGuildParam>();
    private Vector2 mAnchorPosition = Vector2.zero;
    private SRPG_ScrollRect Scroll;
    private RectTransform ScrollContent;
    private bool IsLoading;
    private int currentPage;
    private int currentPageMax;

    private void Awake()
    {
      this.mBossType = GuildRaidRankingWindow.BossType.None;
      this.mCurrentTab = this.mTabTotal;
      GameUtility.SetGameObjectActive(this.mGuildButton, false);
      GameUtility.SetGameObjectActive(this.mMemberButton, false);
      GameUtility.SetGameObjectActive(this.mGuildObject, false);
      GameUtility.SetGameObjectActive(this.mMemberObject, false);
      if (Object.op_Inequality((Object) this.ScrollGuild, (Object) null))
        this.ScrollGuild.verticalNormalizedPosition = 1f;
      if (Object.op_Inequality((Object) this.ScrollTotal, (Object) null))
        this.ScrollTotal.verticalNormalizedPosition = 1f;
      if (!Object.op_Inequality((Object) this.ScrollBoss, (Object) null))
        return;
      this.ScrollBoss.verticalNormalizedPosition = 1f;
    }

    private void Update()
    {
      if (this.IsLoading || this.currentPage >= this.currentPageMax || !Object.op_Inequality((Object) this.Scroll, (Object) null) || !Object.op_Inequality((Object) this.ScrollContent, (Object) null) || (double) this.Scroll.verticalNormalizedPosition * (double) this.ScrollContent.sizeDelta.y >= 10.0)
        return;
      this.IsLoading = true;
      if (this.mSelectType == GuildRaidRankingWindow.SelectType.Guild)
      {
        if (GuildRaidManager.Instance.mRankingType == GuildRaidManager.GuildRaidRankingType.Current)
          ++GuildRaidManager.Instance.CurrentRankingPage;
        else
          ++GuildRaidManager.Instance.PreviousRankingPage;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 202);
      }
      else
      {
        if (GuildRaidManager.Instance.mRankingType == GuildRaidManager.GuildRaidRankingType.Current)
          ++GuildRaidManager.Instance.CurrentRankingPortPage;
        else
          ++GuildRaidManager.Instance.PreviousRankingPortPage;
        if (this.mBossType == GuildRaidRankingWindow.BossType.None)
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 203);
        else
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 204);
      }
    }

    private void SetupReloadScroll()
    {
      if (this.mSelectType == GuildRaidRankingWindow.SelectType.Guild)
      {
        this.Scroll = this.ScrollGuild;
        this.ScrollContent = this.ScrollContentGuild;
        if (GuildRaidManager.Instance.mRankingType == GuildRaidManager.GuildRaidRankingType.Current)
        {
          this.currentPage = GuildRaidManager.Instance.CurrentRankingPage;
          this.currentPageMax = GuildRaidManager.Instance.CurrentRankingPageTotal;
        }
        else
        {
          this.currentPage = GuildRaidManager.Instance.PreviousRankingPage;
          this.currentPageMax = GuildRaidManager.Instance.PreviousRankingPageTotal;
        }
      }
      else
      {
        if (this.mBossType == GuildRaidRankingWindow.BossType.None)
        {
          this.Scroll = this.ScrollTotal;
          this.ScrollContent = this.ScrollContentTotal;
        }
        else
        {
          this.Scroll = this.ScrollBoss;
          this.ScrollContent = this.ScrollContentBoss;
        }
        if (GuildRaidManager.Instance.mRankingType == GuildRaidManager.GuildRaidRankingType.Current)
        {
          this.currentPage = GuildRaidManager.Instance.CurrentRankingPortPage;
          this.currentPageMax = GuildRaidManager.Instance.CurrentRankingPortPageTotal;
        }
        else
        {
          this.currentPage = GuildRaidManager.Instance.PreviousRankingPortPage;
          this.currentPageMax = GuildRaidManager.Instance.PreviousRankingPortPageTotal;
        }
      }
      if (this.currentPage > 1)
        return;
      if (Object.op_Inequality((Object) this.ScrollGuild, (Object) null))
        this.ScrollGuild.verticalNormalizedPosition = 1f;
      if (Object.op_Inequality((Object) this.ScrollTotal, (Object) null))
        this.ScrollTotal.verticalNormalizedPosition = 1f;
      if (!Object.op_Inequality((Object) this.ScrollBoss, (Object) null))
        return;
      this.ScrollBoss.verticalNormalizedPosition = 1f;
    }

    public void Activated(int pinID)
    {
      bool flag = false;
      switch (pinID)
      {
        case 10:
          if (this.mSelectType != GuildRaidRankingWindow.SelectType.Guild)
            flag = true;
          this.mSelectType = GuildRaidRankingWindow.SelectType.Guild;
          this.mBossType = GuildRaidRankingWindow.BossType.None;
          break;
        case 11:
          if (this.mSelectType != GuildRaidRankingWindow.SelectType.Member)
            flag = true;
          this.mSelectType = GuildRaidRankingWindow.SelectType.Member;
          this.mBossType = GuildRaidRankingWindow.BossType.None;
          this.ToggleControl();
          break;
        case 12:
          if (this.mSelectType != GuildRaidRankingWindow.SelectType.Boss)
            flag = true;
          this.mSelectType = GuildRaidRankingWindow.SelectType.Boss;
          break;
      }
      if (flag)
      {
        if (Object.op_Inequality((Object) this.ScrollGuild, (Object) null))
          this.ScrollGuild.verticalNormalizedPosition = 1f;
        if (Object.op_Inequality((Object) this.ScrollTotal, (Object) null))
          this.ScrollTotal.verticalNormalizedPosition = 1f;
        if (Object.op_Inequality((Object) this.ScrollBoss, (Object) null))
          this.ScrollBoss.verticalNormalizedPosition = 1f;
      }
      this.Refresh();
    }

    private void Refresh()
    {
      GuildRaidManager instance = GuildRaidManager.Instance;
      if (this.mSelectType == GuildRaidRankingWindow.SelectType.Guild)
      {
        if (Object.op_Equality((Object) this.mGuildRankingTemplate, (Object) null) || Object.op_Equality((Object) instance, (Object) null))
          return;
        List<GuildRaidRanking> guildRaidRankingList;
        GuildRaidRanking data;
        if (GuildRaidManager.Instance.mRankingType == GuildRaidManager.GuildRaidRankingType.Current)
        {
          guildRaidRankingList = instance.CurrentRankingList;
          data = instance.CurrentRankingSelf;
        }
        else
        {
          guildRaidRankingList = instance.PreviousRankingList;
          data = instance.PreviousRankingSelf;
        }
        DataSource.Bind<GuildRaidRanking>(((Component) this).gameObject, data);
        if (Object.op_Inequality((Object) this.mGuildObject, (Object) null))
          this.mGuildObject.SetActive(true);
        if (Object.op_Inequality((Object) this.mMemberObject, (Object) null))
          this.mMemberObject.SetActive(false);
        this.SetChangeButton(this.mGuildButton, this.mMemberButton);
        if (guildRaidRankingList == null)
          return;
        this.mGuildRankingTemplate.SetActive(false);
        if (this.mRankingList == null)
          this.mRankingList = new List<GameObject>();
        for (int index = 0; index < this.mRankingList.Count; ++index)
          Object.Destroy((Object) this.mRankingList[index]);
        this.mRankingList.Clear();
        this.mGuildRaidRankingGuildParamList.Clear();
        ContentSource source = new ContentSource();
        for (int index = 0; index < guildRaidRankingList.Count; ++index)
        {
          if (guildRaidRankingList[index] != null)
          {
            GuildRaidRankingGuildParam rankingGuildParam = new GuildRaidRankingGuildParam();
            rankingGuildParam.mGuildRaidRanking = guildRaidRankingList[index];
            rankingGuildParam.Initialize(source);
            this.mGuildRaidRankingGuildParamList.Add(rankingGuildParam);
          }
        }
        if (Object.op_Inequality((Object) this.mContentController, (Object) null))
        {
          this.mAnchorPosition = this.mContentController.GetAnchorePos();
          source.SetTable((ContentSource.Param[]) this.mGuildRaidRankingGuildParamList.ToArray());
          this.mContentController.Initialize(source, this.mAnchorPosition);
          this.mContentController.ForceUpdate();
        }
        foreach (GuildRaidRankingGuildParam rankingGuildParam in this.mGuildRaidRankingGuildParamList)
          rankingGuildParam.Refresh();
      }
      else
      {
        GameObject gameObject1;
        List<GuildRaidRankingMember> raidRankingMemberList;
        GuildRaidRankingMember data;
        if (this.mSelectType == GuildRaidRankingWindow.SelectType.Boss)
        {
          gameObject1 = this.mMemberRankingBossTemplate;
          if (Object.op_Inequality((Object) this.mMemberTotalViewObject, (Object) null))
            this.mMemberTotalViewObject.SetActive(false);
          if (Object.op_Inequality((Object) this.mMemberBossViewObject, (Object) null))
            this.mMemberBossViewObject.SetActive(true);
          if (GuildRaidManager.Instance.mRankingType == GuildRaidManager.GuildRaidRankingType.Current)
          {
            raidRankingMemberList = instance.CurrentRankingPortList;
            data = instance.CurrentRankingPortSelf;
          }
          else
          {
            raidRankingMemberList = instance.PreviousRankingPortList;
            data = instance.PreviousRankingPortSelf;
          }
        }
        else
        {
          if (GuildRaidManager.Instance.mRankingType == GuildRaidManager.GuildRaidRankingType.Current)
          {
            gameObject1 = this.mMemberRankingTemplate;
            raidRankingMemberList = instance.CurrentRankingPortList;
            data = instance.CurrentRankingPortSelf;
          }
          else
          {
            gameObject1 = this.mMemberHistoryTemplate;
            raidRankingMemberList = instance.PreviousRankingPortList;
            data = instance.PreviousRankingPortSelf;
          }
          if (Object.op_Inequality((Object) this.mMemberTotalViewObject, (Object) null))
            this.mMemberTotalViewObject.SetActive(true);
          if (Object.op_Inequality((Object) this.mMemberBossViewObject, (Object) null))
            this.mMemberBossViewObject.SetActive(false);
        }
        if (Object.op_Equality((Object) gameObject1, (Object) null) || Object.op_Equality((Object) instance, (Object) null))
          return;
        if (instance.mRankingType == GuildRaidManager.GuildRaidRankingType.Previous)
          GameUtility.SetGameObjectActive(this.mTabObject, false);
        else
          GameUtility.SetGameObjectActive(this.mTabObject, true);
        DataSource.Bind<GuildRaidRankingMember>(((Component) this).gameObject, data);
        if (Object.op_Inequality((Object) this.mGuildObject, (Object) null))
          this.mGuildObject.SetActive(false);
        if (Object.op_Inequality((Object) this.mMemberObject, (Object) null))
          this.mMemberObject.SetActive(true);
        this.SetChangeButton(this.mMemberButton, this.mGuildButton);
        if (raidRankingMemberList == null)
          return;
        gameObject1.SetActive(false);
        if (this.mRankingList == null)
          this.mRankingList = new List<GameObject>();
        for (int index = 0; index < this.mRankingList.Count; ++index)
          Object.Destroy((Object) this.mRankingList[index]);
        this.mRankingList.Clear();
        if (this.mSelectType == GuildRaidRankingWindow.SelectType.Boss)
        {
          GuildRaidBossParam guildRaidBossParam = MonoSingleton<GameManager>.Instance.GetGuildRaidBossParam(GuildRaidManager.Instance.PeriodId, (int) this.mBossType);
          if (this.mBossType == GuildRaidRankingWindow.BossType.Boss01)
          {
            if (Object.op_Inequality((Object) this.mPagePrev, (Object) null))
              ((Selectable) this.mPagePrev).interactable = false;
          }
          else if (Object.op_Inequality((Object) this.mPagePrev, (Object) null))
            ((Selectable) this.mPagePrev).interactable = true;
          if (this.mBossType == GuildRaidRankingWindow.BossType.Boss05)
          {
            if (Object.op_Inequality((Object) this.mPageNext, (Object) null))
              ((Selectable) this.mPageNext).interactable = false;
          }
          else if (Object.op_Inequality((Object) this.mPageNext, (Object) null))
            ((Selectable) this.mPageNext).interactable = true;
          if (guildRaidBossParam != null)
          {
            if (Object.op_Inequality((Object) this.mBossIcon, (Object) null))
            {
              UnitParam unitParam = MonoSingleton<GameManager>.Instance.GetUnitParam(guildRaidBossParam.UnitIName);
              if (unitParam != null)
              {
                string str = AssetPath.UnitIconSmall(unitParam, (string) null);
                if (!string.IsNullOrEmpty(str))
                  GameUtility.RequireComponent<IconLoader>(this.mBossIcon).ResourcePath = str;
              }
            }
            if (Object.op_Inequality((Object) this.mBossName, (Object) null))
              this.mBossName.text = guildRaidBossParam.Name;
            if (Object.op_Inequality((Object) this.mBossStage, (Object) null))
              this.mBossStage.text = string.Format(LocalizedText.Get("sys.GUILDRAID_BOSSRANKING_STAGE"), (object) (int) this.mBossType);
          }
          for (int index = 0; index < instance.RankingPortBossList.Count; ++index)
          {
            if (instance.RankingPortBossList[index] != null)
            {
              GameObject gameObject2 = Object.Instantiate<GameObject>(gameObject1, gameObject1.transform.parent);
              DataSource.Bind<GuildRaidRankingMember>(gameObject2, instance.RankingPortBossList[index]);
              gameObject2.SetActive(true);
              this.mRankingList.Add(gameObject2);
            }
          }
        }
        else
        {
          for (int index1 = 0; index1 < raidRankingMemberList.Count; ++index1)
          {
            if (raidRankingMemberList[index1] != null)
            {
              GameObject gameObject3 = Object.Instantiate<GameObject>(gameObject1, gameObject1.transform.parent);
              DataSource.Bind<GuildRaidRankingMember>(gameObject3, raidRankingMemberList[index1]);
              gameObject3.SetActive(true);
              GuildRaidRankingWindowItem component1 = gameObject3.GetComponent<GuildRaidRankingWindowItem>();
              if (!Object.op_Equality((Object) component1, (Object) null))
              {
                GuildRaidRankingBossData mData = component1.mData;
                if (Object.op_Inequality((Object) mData, (Object) null) && Object.op_Inequality((Object) mData.templte, (Object) null) && raidRankingMemberList[index1].Bosses != null)
                {
                  mData.templte.SetActive(false);
                  for (int index2 = 0; index2 < raidRankingMemberList[index1].Bosses.Count; ++index2)
                  {
                    if (raidRankingMemberList[index1].Bosses[index2] != null && raidRankingMemberList[index1].Bosses[index2].Rank != -1)
                    {
                      GameObject gameObject4 = Object.Instantiate<GameObject>(mData.templte, mData.templte.transform.parent);
                      GuildRaidRankingBossData component2 = gameObject4.GetComponent<GuildRaidRankingBossData>();
                      UnitData unitDataForDisplay = UnitData.CreateUnitDataForDisplay(MonoSingleton<GameManager>.Instance.GetGuildRaidBossParam(raidRankingMemberList[index1].Bosses[index2].BossId).UnitIName);
                      DataSource.Bind<UnitData>(gameObject4, unitDataForDisplay);
                      component2.mRank.text = raidRankingMemberList[index1].Bosses[index2].Rank.ToString();
                      gameObject4.SetActive(true);
                    }
                  }
                }
                this.mRankingList.Add(gameObject3);
              }
            }
          }
        }
      }
      this.SetupReloadScroll();
      GameParameter.UpdateAll(((Component) this).gameObject);
      this.IsLoading = false;
    }

    private void SetChangeButton(GameObject select, GameObject noselect)
    {
      GameUtility.SetGameObjectActive(this.mGuildButton, true);
      GameUtility.SetGameObjectActive(this.mMemberButton, true);
      if (Object.op_Inequality((Object) select, (Object) null))
      {
        Transform component1 = select.GetComponent<Transform>();
        ImageArray component2 = select.GetComponent<ImageArray>();
        if (Object.op_Inequality((Object) component2, (Object) null) && component2.Images.Length > 0)
          component2.ImageIndex = 0;
        if (Object.op_Inequality((Object) component1, (Object) null))
          component1.localScale = new Vector3(this.mSelScale, this.mSelScale, this.mSelScale);
      }
      if (!Object.op_Inequality((Object) noselect, (Object) null))
        return;
      Transform component3 = noselect.GetComponent<Transform>();
      ImageArray component4 = noselect.GetComponent<ImageArray>();
      if (Object.op_Inequality((Object) component4, (Object) null) && component4.Images.Length > 1)
        component4.ImageIndex = 1;
      if (!Object.op_Inequality((Object) component3, (Object) null))
        return;
      component3.localScale = new Vector3(this.mNonSelScale, this.mNonSelScale, this.mNonSelScale);
    }

    public void OnClickTab(GameObject go)
    {
      if (Object.op_Equality((Object) this.mCurrentTab, (Object) go))
        return;
      this.mCurrentTab = go;
      if (Object.op_Equality((Object) go, (Object) this.mTabTotal))
        this.mBossType = GuildRaidRankingWindow.BossType.None;
      if (Object.op_Equality((Object) go, (Object) this.mTabBoss01))
        this.mBossType = GuildRaidRankingWindow.BossType.Boss01;
      if (Object.op_Equality((Object) go, (Object) this.mTabBoss02))
        this.mBossType = GuildRaidRankingWindow.BossType.Boss02;
      if (Object.op_Equality((Object) go, (Object) this.mTabBoss03))
        this.mBossType = GuildRaidRankingWindow.BossType.Boss03;
      if (Object.op_Equality((Object) go, (Object) this.mTabBoss04))
        this.mBossType = GuildRaidRankingWindow.BossType.Boss04;
      if (Object.op_Equality((Object) go, (Object) this.mTabBoss05))
        this.mBossType = GuildRaidRankingWindow.BossType.Boss05;
      int mBossType = (int) this.mBossType;
      if (mBossType != 0)
      {
        GuildRaidManager.Instance.RankingPortBossId = MonoSingleton<GameManager>.Instance.GetGuildRaidBossParam(GuildRaidManager.Instance.PeriodId, mBossType).Id;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 201);
      }
      else
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 200);
    }

    public void OnPrev()
    {
      if (this.mBossType == GuildRaidRankingWindow.BossType.Boss01)
        return;
      --this.mBossType;
      GuildRaidManager.Instance.RankingPortBossId = MonoSingleton<GameManager>.Instance.GetGuildRaidBossParam(GuildRaidManager.Instance.PeriodId, (int) this.mBossType).Id;
      this.ToggleControl();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 201);
    }

    public void OnNext()
    {
      if (this.mBossType == GuildRaidRankingWindow.BossType.Boss05)
        return;
      ++this.mBossType;
      GuildRaidManager.Instance.RankingPortBossId = MonoSingleton<GameManager>.Instance.GetGuildRaidBossParam(GuildRaidManager.Instance.PeriodId, (int) this.mBossType).Id;
      this.ToggleControl();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 201);
    }

    public void ToggleControl()
    {
      Toggle component1 = this.mTabTotal.GetComponent<Toggle>();
      if (Object.op_Inequality((Object) component1, (Object) null))
        component1.isOn = false;
      Toggle component2 = this.mTabBoss01.GetComponent<Toggle>();
      if (Object.op_Inequality((Object) component2, (Object) null))
        component2.isOn = false;
      Toggle component3 = this.mTabBoss02.GetComponent<Toggle>();
      if (Object.op_Inequality((Object) component3, (Object) null))
        component3.isOn = false;
      Toggle component4 = this.mTabBoss03.GetComponent<Toggle>();
      if (Object.op_Inequality((Object) component4, (Object) null))
        component4.isOn = false;
      Toggle component5 = this.mTabBoss04.GetComponent<Toggle>();
      if (Object.op_Inequality((Object) component5, (Object) null))
        component5.isOn = false;
      Toggle component6 = this.mTabBoss05.GetComponent<Toggle>();
      if (Object.op_Inequality((Object) component6, (Object) null))
        component6.isOn = false;
      switch (this.mBossType)
      {
        case GuildRaidRankingWindow.BossType.None:
          Toggle component7 = this.mTabTotal.GetComponent<Toggle>();
          if (!Object.op_Inequality((Object) component7, (Object) null))
            break;
          component7.isOn = true;
          break;
        case GuildRaidRankingWindow.BossType.Boss01:
          Toggle component8 = this.mTabBoss01.GetComponent<Toggle>();
          if (!Object.op_Inequality((Object) component8, (Object) null))
            break;
          component8.isOn = true;
          break;
        case GuildRaidRankingWindow.BossType.Boss02:
          Toggle component9 = this.mTabBoss02.GetComponent<Toggle>();
          if (!Object.op_Inequality((Object) component9, (Object) null))
            break;
          component9.isOn = true;
          break;
        case GuildRaidRankingWindow.BossType.Boss03:
          Toggle component10 = this.mTabBoss03.GetComponent<Toggle>();
          if (!Object.op_Inequality((Object) component10, (Object) null))
            break;
          component10.isOn = true;
          break;
        case GuildRaidRankingWindow.BossType.Boss04:
          Toggle component11 = this.mTabBoss04.GetComponent<Toggle>();
          if (!Object.op_Inequality((Object) component11, (Object) null))
            break;
          component11.isOn = true;
          break;
        case GuildRaidRankingWindow.BossType.Boss05:
          Toggle component12 = this.mTabBoss05.GetComponent<Toggle>();
          if (!Object.op_Inequality((Object) component12, (Object) null))
            break;
          component12.isOn = true;
          break;
      }
    }

    public enum SelectType
    {
      Guild,
      Member,
      Boss,
    }

    public enum BossType
    {
      None,
      Boss01,
      Boss02,
      Boss03,
      Boss04,
      Boss05,
    }
  }
}
