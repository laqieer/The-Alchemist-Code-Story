// Decompiled with JetBrains decompiler
// Type: SRPG.OrdealQuestList
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
  [FlowNode.Pin(0, "チーム情報更新", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "クエスト開始要求", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(1000, "クエスト開始", FlowNode.PinTypes.Output, 1000)]
  [FlowNode.Pin(1100, "チーム編成開始", FlowNode.PinTypes.Output, 1100)]
  [FlowNode.Pin(1200, "クエスト選択", FlowNode.PinTypes.Output, 1200)]
  public class OrdealQuestList : MonoBehaviour, IFlowInterface, IWebHelp
  {
    [SerializeField]
    private GameObject ItemContainer;
    [SerializeField]
    private UnityEngine.UI.Text QuestTypeText;
    [SerializeField]
    private GameObject ChapterScrollRect;
    [SerializeField]
    private GameObject DetailTemplate;
    [Space(10f)]
    [SerializeField]
    private GameObject ItemQuestContainer;
    [SerializeField]
    private GameObject ItemQuestTemplate;
    [SerializeField]
    private UnityEngine.UI.Text QuestListText;
    [SerializeField]
    private GameObject QuestScrollRect;
    [SerializeField]
    private GameObject DetailQuestTemplate;
    [Space(10f)]
    [SerializeField]
    private GameObject TeamPanelContainer;
    [SerializeField]
    private OrdealTeamPanel TeamPanelTemplate;
    [SerializeField]
    private Button StartButton;
    [SerializeField]
    private ListItemEvents MissionButton;
    [SerializeField]
    private Image BossImage;
    [SerializeField]
    private UnityEngine.UI.Text BossText;
    private List<ListItemEvents> mItems = new List<ListItemEvents>();
    private List<ListItemEvents> mListItems = new List<ListItemEvents>();
    private GameObject mDetailInfo;
    private ChapterParam mCurrentChapter;
    private QuestParam mCurrentQuest;
    private List<GameObject> mTeamPanels = new List<GameObject>();

    private void Awake()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TeamPanelTemplate, (UnityEngine.Object) null))
        ((Component) this.TeamPanelTemplate).gameObject.SetActive(false);
      GlobalVars.OrdealParties = new List<PartyEditData>();
      GlobalVars.OrdealSupports = new List<SupportData>();
      this.Refresh();
      this.RefreshQuestTypeText();
    }

    private void ResetScroll()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ItemContainer, (UnityEngine.Object) null))
        return;
      ScrollRect[] componentsInParent = this.ItemContainer.GetComponentsInParent<ScrollRect>(true);
      if (componentsInParent.Length <= 0)
        return;
      componentsInParent[0].verticalNormalizedPosition = 1f;
    }

    public void Activated(int pinID)
    {
      if (pinID != 0)
      {
        if (pinID != 1)
          return;
        this.StartQuest();
      }
      else
        this.LoadTeam();
    }

    private bool ChapterContainsPlayableQuest(
      ChapterParam chapter,
      ChapterParam[] allChapters,
      QuestParam[] availableQuests,
      long currentTime)
    {
      bool flag = false;
      for (int index = 0; index < allChapters.Length; ++index)
      {
        if (allChapters[index].parent == chapter)
        {
          if (this.ChapterContainsPlayableQuest(allChapters[index], allChapters, availableQuests, currentTime))
            return true;
          flag = true;
        }
      }
      if (!flag)
      {
        for (int index = 0; index < availableQuests.Length; ++index)
        {
          if (availableQuests[index].ChapterID == chapter.iname && !availableQuests[index].IsMulti && availableQuests[index].IsDateUnlock(currentTime))
            return true;
        }
      }
      return false;
    }

    private List<ChapterParam> GetAvailableChapters(
      ChapterParam[] allChapters,
      QuestParam[] questsAvailable,
      long currentTime,
      out ChapterParam currentChapter)
    {
      List<ChapterParam> availableChapters = new List<ChapterParam>();
      currentChapter = (ChapterParam) null;
      foreach (ChapterParam allChapter in allChapters)
      {
        if (allChapter.IsOrdealQuest())
        {
          availableChapters.Add(allChapter);
          if (allChapter.quests[0].state != QuestStates.Cleared)
            currentChapter = allChapter;
        }
      }
      if (currentChapter == null && availableChapters.Count > 0)
        currentChapter = availableChapters[0];
      for (int index = availableChapters.Count - 1; index >= 0; --index)
      {
        if (!this.ChapterContainsPlayableQuest(availableChapters[index], allChapters, questsAvailable, currentTime))
          availableChapters.RemoveAt(index);
      }
      return availableChapters;
    }

    private void Refresh()
    {
      GameUtility.DestroyGameObjects<ListItemEvents>(this.mItems);
      this.mItems.Clear();
      GameManager instance = MonoSingleton<GameManager>.Instance;
      ChapterParam currentChapter;
      List<ChapterParam> availableChapters = this.GetAvailableChapters(instance.Chapters, instance.Player.AvailableQuests, Network.GetServerTime(), out currentChapter);
      this.mCurrentChapter = currentChapter;
      for (int index = 0; index < availableChapters.Count; ++index)
      {
        ChapterParam data = availableChapters[index];
        if (!string.IsNullOrEmpty(data.prefabPath))
        {
          StringBuilder stringBuilder = GameUtility.GetStringBuilder();
          stringBuilder.Append("QuestChapters/");
          stringBuilder.Append(data.prefabPath);
          ListItemEvents listItemEvents1 = AssetManager.Load<ListItemEvents>(stringBuilder.ToString());
          if (!UnityEngine.Object.op_Equality((UnityEngine.Object) listItemEvents1, (UnityEngine.Object) null))
          {
            ListItemEvents listItemEvents2 = UnityEngine.Object.Instantiate<ListItemEvents>(listItemEvents1);
            foreach (ButtonEvent componentsInChild in ((Component) listItemEvents2).GetComponentsInChildren<ButtonEvent>(true))
              componentsInChild.syncEvent = this.ChapterScrollRect;
            DataSource.Bind<ChapterParam>(((Component) listItemEvents2).gameObject, data);
            if (data.quests != null && data.quests.Count > 0)
              DataSource.Bind<QuestParam>(((Component) listItemEvents2).gameObject, data.quests[0]);
            KeyQuestBanner component = ((Component) listItemEvents2).gameObject.GetComponent<KeyQuestBanner>();
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
              component.UpdateValue();
            ((Component) listItemEvents2).transform.SetParent(this.ItemContainer.transform, false);
            ((Component) listItemEvents2).gameObject.SetActive(true);
            listItemEvents2.OnSelect = new ListItemEvents.ListItemEvent(this.OnQuestListSelect);
            this.mItems.Add(listItemEvents2);
          }
        }
      }
      for (int index = 0; index < this.mItems.Count; ++index)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mItems[index], (UnityEngine.Object) null))
          ((Component) this.mItems[index]).gameObject.transform.SetSiblingIndex(index);
      }
      this.ResetScroll();
    }

    private void RefreshQuestTypeText()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.QuestTypeText, (UnityEngine.Object) null))
        return;
      this.QuestTypeText.text = LocalizedText.Get("sys.QUESTTYPE_ORDEAL");
    }

    private void StartQuest()
    {
      List<PartyEditData> ordealParties = GlobalVars.OrdealParties;
      List<SupportData> ordealSupports = GlobalVars.OrdealSupports;
      if (!PartyUtility.ValidateOrdealTeams(this.mCurrentQuest, ordealParties, ordealSupports, false) || PartyUtility.CheckWarningForOrdealTeams(ordealParties, (Action) (() => this.StartQuestConfirmDownload())))
        return;
      this.StartQuestConfirmDownload();
    }

    private void StartQuestConfirmDownload()
    {
      if (AssetDownloader.IsEnableShowSizeBeforeDownloading())
        AssetDownloader.StartConfirmDownloadQuestContentYesNo(this.GetBattleEntryUnits(), (List<ItemData>) null, this.mCurrentQuest, (UIUtility.DialogResultEvent) (obj_ok => this.DownloadApproved()), (UIUtility.DialogResultEvent) (obj_cancel => this.DownloadNotApproved()));
      else
        this.DownloadApproved();
    }

    private void DownloadApproved()
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 1000);
    }

    private void DownloadNotApproved()
    {
    }

    private void ResetMissionButton()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.MissionButton, (UnityEngine.Object) null))
        return;
      this.MissionButton.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnOpenItemDetail);
      DataSource.Bind<QuestParam>(((Component) this.MissionButton).gameObject, this.mCurrentQuest);
    }

    private void LoadBossData(QuestParam quest)
    {
      SpriteSheet spriteSheet = AssetManager.Load<SpriteSheet>("OrdealQuestList/OrdealQuestList_Images");
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) spriteSheet, (UnityEngine.Object) null))
        this.BossImage.sprite = spriteSheet.GetSprite(quest.iname);
      this.BossText.text = LocalizedText.Get("sys.ORDEAL_QUEST_BOSS_MESSAGE_" + quest.iname);
    }

    private void LoadTeam()
    {
      GameUtility.DestroyGameObjects(this.mTeamPanels);
      this.mTeamPanels.Clear();
      GlobalVars.OrdealParties = this.LoadTeamFromPlayerPrefs();
      List<PartyEditData> ordealParties = GlobalVars.OrdealParties;
      List<SupportData> ordealSupports = GlobalVars.OrdealSupports;
      for (int index = 0; index < ordealParties.Count; ++index)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        OrdealQuestList.\u003CLoadTeam\u003Ec__AnonStorey0 teamCAnonStorey0 = new OrdealQuestList.\u003CLoadTeam\u003Ec__AnonStorey0();
        // ISSUE: reference to a compiler-generated field
        teamCAnonStorey0.\u0024this = this;
        OrdealTeamPanel component = UnityEngine.Object.Instantiate<GameObject>(((Component) this.TeamPanelTemplate).gameObject).GetComponent<OrdealTeamPanel>();
        ((Component) component).gameObject.SetActive(true);
        foreach (UnitData unit in ordealParties[index].Units)
        {
          if (unit != null)
            component.Add(unit);
        }
        // ISSUE: reference to a compiler-generated field
        teamCAnonStorey0.index = index;
        // ISSUE: method pointer
        ((UnityEvent) component.Button.onClick).AddListener(new UnityAction((object) teamCAnonStorey0, __methodptr(\u003C\u003Em__0)));
        component.TeamName.text = ordealParties[index].Name;
        SupportData supportData = (SupportData) null;
        if (ordealSupports != null && index < ordealSupports.Count)
        {
          supportData = ordealSupports[index];
          component.SetSupport(supportData);
        }
        int num = PartyUtility.CalcTotalCombatPower(ordealParties[index], MonoSingleton<GameManager>.Instance.Player.Units, supportData);
        component.TotalCombatPower.text = num.ToString();
        this.mTeamPanels.Add(((Component) component).gameObject);
        ((Component) component).transform.SetParent(this.TeamPanelContainer.transform, false);
      }
      this.CheckPlayableTeams(this.mCurrentQuest, ordealParties, ordealSupports);
    }

    private void OnClickTeamPanel(int index)
    {
      GlobalVars.SelectedTeamIndex = index;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 1100);
    }

    private void CheckPlayableTeams(
      QuestParam quest,
      List<PartyEditData> teams,
      List<SupportData> supports = null)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.StartButton, (UnityEngine.Object) null))
        return;
      ((Selectable) this.StartButton).interactable = PartyUtility.ValidateOrdealTeams(quest, teams, supports, true);
    }

    private List<PartyEditData> LoadTeamFromPlayerPrefs()
    {
      int maxTeamCount = PartyWindow2.EditPartyTypes.Ordeal.GetMaxTeamCount();
      List<PartyEditData> teams = PartyUtility.LoadTeamPresets(PartyWindow2.EditPartyTypes.Ordeal, out int _) ?? new List<PartyEditData>();
      this.ValidateTeam(this.mCurrentQuest, teams, maxTeamCount);
      return teams;
    }

    private void ValidateTeam(QuestParam quest, List<PartyEditData> teams, int maxTeamCount)
    {
      bool flag = false;
      if (teams.Count > maxTeamCount)
      {
        teams = teams.Take<PartyEditData>(maxTeamCount).ToList<PartyEditData>();
        flag = true;
      }
      else if (teams.Count < maxTeamCount)
      {
        for (int count = teams.Count; count < maxTeamCount; ++count)
        {
          PartyData party = new PartyData(PlayerPartyTypes.Ordeal);
          teams.Add(new PartyEditData(PartyUtility.CreateOrdealPartyNameFromIndex(count), party));
        }
        flag = true;
      }
      if (!(flag | !PartyUtility.ResetToDefaultTeamIfNeededForOrdealQuest(quest, teams)))
        return;
      PartyUtility.SaveTeamPresets(PartyWindow2.EditPartyTypes.Ordeal, 0, teams);
    }

    private void OnQuestListSelect(GameObject go)
    {
      GameUtility.DestroyGameObjects<ListItemEvents>(this.mListItems);
      this.mListItems.Clear();
      ChapterParam dataOfClass = DataSource.FindDataOfClass<ChapterParam>(go, (ChapterParam) null);
      if (dataOfClass == null)
        return;
      QuestParam[] availableQuests = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
      long serverTime = Network.GetServerTime();
      int num1 = 0;
      int num2 = 0;
      for (int index = 0; index < availableQuests.Length; ++index)
      {
        if (availableQuests[index].ChapterID == dataOfClass.iname && !availableQuests[index].IsMulti)
        {
          ++num1;
          if (availableQuests[index].IsJigen && !availableQuests[index].IsDateUnlock(serverTime))
            ++num2;
        }
      }
      if (num1 > 0 && num1 == num2)
      {
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.QUEST_OUT_OF_DATE"), (UIUtility.DialogResultEvent) null);
      }
      else
      {
        if (dataOfClass.quests != null && dataOfClass.quests.Count > 0)
        {
          this.QuestListText.text = dataOfClass.quests[0].name;
          for (int index = 0; index < dataOfClass.quests.Count; ++index)
          {
            QuestParam quest = dataOfClass.quests[index];
            GameObject gameObject1 = (GameObject) null;
            if (!string.IsNullOrEmpty(quest.ItemLayout))
              gameObject1 = AssetManager.Load<GameObject>("QuestListItems/" + quest.ItemLayout);
            if (UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject1, (UnityEngine.Object) null))
              gameObject1 = this.ItemQuestTemplate;
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject1, (UnityEngine.Object) null))
            {
              GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject1);
              ((UnityEngine.Object) gameObject2).hideFlags = (HideFlags) 52;
              DataSource.Bind<QuestParam>(gameObject2, quest);
              RankingQuestParam availableRankingQuest = MonoSingleton<GameManager>.Instance.FindAvailableRankingQuest(quest.iname);
              DataSource.Bind<RankingQuestParam>(gameObject2, availableRankingQuest);
              DataSource.Bind<QuestParam>(gameObject2, quest);
              QuestCampaignData[] questCampaigns = MonoSingleton<GameManager>.Instance.FindQuestCampaigns(quest);
              DataSource.Bind<QuestCampaignData[]>(gameObject2, questCampaigns.Length != 0 ? questCampaigns : (QuestCampaignData[]) null);
              ListItemEvents component = gameObject2.GetComponent<ListItemEvents>();
              component.OnSelect = new ListItemEvents.ListItemEvent(this.OndetailSelect);
              component.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnOpenItemDetail);
              component.OnCloseDetail = new ListItemEvents.ListItemEvent(this.OnCloseItemDetail);
              gameObject2.transform.SetParent(this.ItemQuestContainer.transform, false);
              gameObject2.gameObject.SetActive(true);
              this.mListItems.Add(component);
            }
          }
        }
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ItemQuestContainer, (UnityEngine.Object) null))
          return;
        ScrollRect[] componentsInParent = this.ItemQuestContainer.GetComponentsInParent<ScrollRect>(true);
        if (componentsInParent.Length <= 0)
          return;
        componentsInParent[0].verticalNormalizedPosition = 1f;
      }
    }

    private void OndetailSelect(GameObject go)
    {
      this.mCurrentQuest = DataSource.FindDataOfClass<QuestParam>(go, (QuestParam) null);
      GlobalVars.SelectedQuestID = this.mCurrentQuest.iname;
      DataSource.Bind<QuestParam>(((Component) this).gameObject, this.mCurrentQuest);
      this.ResetMissionButton();
      this.LoadBossData(this.mCurrentQuest);
      this.LoadTeam();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 1200);
    }

    private void OnCloseItemDetail(GameObject go)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mDetailInfo, (UnityEngine.Object) null))
        return;
      UnityEngine.Object.DestroyImmediate((UnityEngine.Object) this.mDetailInfo.gameObject);
      this.mDetailInfo = (GameObject) null;
    }

    private void OnOpenItemDetail(GameObject go)
    {
      QuestParam dataOfClass = DataSource.FindDataOfClass<QuestParam>(go, (QuestParam) null);
      if (!UnityEngine.Object.op_Equality((UnityEngine.Object) this.mDetailInfo, (UnityEngine.Object) null) || dataOfClass == null)
        return;
      this.mDetailInfo = UnityEngine.Object.Instantiate<GameObject>(this.DetailTemplate);
      DataSource.Bind<QuestParam>(this.mDetailInfo, dataOfClass);
      QuestCampaignData[] questCampaigns = MonoSingleton<GameManager>.Instance.FindQuestCampaigns(dataOfClass);
      DataSource.Bind<QuestCampaignData[]>(this.mDetailInfo, questCampaigns.Length != 0 ? questCampaigns : (QuestCampaignData[]) null);
      this.mDetailInfo.SetActive(true);
    }

    public bool GetHelpURL(out string url, out string title)
    {
      if (this.mCurrentChapter != null && !string.IsNullOrEmpty(this.mCurrentChapter.helpURL))
      {
        title = this.mCurrentChapter.name;
        url = this.mCurrentChapter.helpURL;
        return true;
      }
      title = (string) null;
      url = (string) null;
      return false;
    }

    private List<UnitData> GetBattleEntryUnits()
    {
      List<UnitData> battleEntryUnits = new List<UnitData>();
      List<PartyEditData> ordealParties = GlobalVars.OrdealParties;
      for (int index1 = 0; index1 < ordealParties.Count; ++index1)
      {
        PartyEditData partyEditData = ordealParties[index1];
        for (int index2 = 0; index2 < partyEditData.Units.Length; ++index2)
        {
          UnitData unit = partyEditData.Units[index2];
          if (unit != null)
            battleEntryUnits.Add(unit);
        }
      }
      List<SupportData> ordealSupports = GlobalVars.OrdealSupports;
      for (int index = 0; index < ordealSupports.Count; ++index)
      {
        UnitData unit = ordealSupports[index] == null ? (UnitData) null : ordealSupports[index].Unit;
        if (unit != null)
          battleEntryUnits.Add(unit);
      }
      return battleEntryUnits;
    }
  }
}
