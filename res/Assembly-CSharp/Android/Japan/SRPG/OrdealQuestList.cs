// Decompiled with JetBrains decompiler
// Type: SRPG.OrdealQuestList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(0, "チーム情報更新", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "クエスト開始要求", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(1000, "クエスト開始", FlowNode.PinTypes.Output, 1000)]
  [FlowNode.Pin(1100, "チーム編成開始", FlowNode.PinTypes.Output, 1100)]
  [FlowNode.Pin(1200, "クエスト選択", FlowNode.PinTypes.Output, 1200)]
  public class OrdealQuestList : MonoBehaviour, IFlowInterface, IWebHelp
  {
    private List<ListItemEvents> mItems = new List<ListItemEvents>();
    private List<ListItemEvents> mListItems = new List<ListItemEvents>();
    private List<GameObject> mTeamPanels = new List<GameObject>();
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
    private GameObject mDetailInfo;
    private ChapterParam mCurrentChapter;
    private QuestParam mCurrentQuest;

    private void Awake()
    {
      if ((UnityEngine.Object) this.TeamPanelTemplate != (UnityEngine.Object) null)
        this.TeamPanelTemplate.gameObject.SetActive(false);
      GlobalVars.OrdealParties = new List<PartyEditData>();
      GlobalVars.OrdealSupports = new List<SupportData>();
      this.Refresh();
      this.RefreshQuestTypeText();
    }

    private void ResetScroll()
    {
      if (!((UnityEngine.Object) this.ItemContainer != (UnityEngine.Object) null))
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

    private bool ChapterContainsPlayableQuest(ChapterParam chapter, ChapterParam[] allChapters, QuestParam[] availableQuests, long currentTime)
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

    private List<ChapterParam> GetAvailableChapters(ChapterParam[] allChapters, QuestParam[] questsAvailable, long currentTime, out ChapterParam currentChapter)
    {
      List<ChapterParam> chapterParamList = new List<ChapterParam>();
      currentChapter = (ChapterParam) null;
      foreach (ChapterParam allChapter in allChapters)
      {
        if (allChapter.IsOrdealQuest())
        {
          chapterParamList.Add(allChapter);
          if (allChapter.quests[0].state != QuestStates.Cleared)
            currentChapter = allChapter;
        }
      }
      if (currentChapter == null && chapterParamList.Count > 0)
        currentChapter = chapterParamList[0];
      for (int index = chapterParamList.Count - 1; index >= 0; --index)
      {
        if (!this.ChapterContainsPlayableQuest(chapterParamList[index], allChapters, questsAvailable, currentTime))
          chapterParamList.RemoveAt(index);
      }
      return chapterParamList;
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
          ListItemEvents original = AssetManager.Load<ListItemEvents>(stringBuilder.ToString());
          if (!((UnityEngine.Object) original == (UnityEngine.Object) null))
          {
            ListItemEvents listItemEvents = UnityEngine.Object.Instantiate<ListItemEvents>(original);
            foreach (ButtonEvent componentsInChild in listItemEvents.GetComponentsInChildren<ButtonEvent>(true))
              componentsInChild.syncEvent = this.ChapterScrollRect;
            DataSource.Bind<ChapterParam>(listItemEvents.gameObject, data, false);
            if (data.quests != null && data.quests.Count > 0)
              DataSource.Bind<QuestParam>(listItemEvents.gameObject, data.quests[0], false);
            KeyQuestBanner component = listItemEvents.gameObject.GetComponent<KeyQuestBanner>();
            if ((UnityEngine.Object) component != (UnityEngine.Object) null)
              component.UpdateValue();
            listItemEvents.transform.SetParent(this.ItemContainer.transform, false);
            listItemEvents.gameObject.SetActive(true);
            listItemEvents.OnSelect = new ListItemEvents.ListItemEvent(this.OnQuestListSelect);
            this.mItems.Add(listItemEvents);
          }
        }
      }
      for (int index = 0; index < this.mItems.Count; ++index)
      {
        if ((UnityEngine.Object) this.mItems[index] != (UnityEngine.Object) null)
          this.mItems[index].gameObject.transform.SetSiblingIndex(index);
      }
      this.ResetScroll();
    }

    private void RefreshQuestTypeText()
    {
      if (!((UnityEngine.Object) this.QuestTypeText != (UnityEngine.Object) null))
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
      if (AssetDownloader.IsEnableShowSizeBeforeDownloading)
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
      if (!((UnityEngine.Object) this.MissionButton != (UnityEngine.Object) null))
        return;
      this.MissionButton.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnOpenItemDetail);
      DataSource.Bind<QuestParam>(this.MissionButton.gameObject, this.mCurrentQuest, false);
    }

    private void LoadBossData(QuestParam quest)
    {
      SpriteSheet spriteSheet = AssetManager.Load<SpriteSheet>("OrdealQuestList/OrdealQuestList_Images");
      if ((UnityEngine.Object) spriteSheet != (UnityEngine.Object) null)
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
      for (int index1 = 0; index1 < ordealParties.Count; ++index1)
      {
        OrdealTeamPanel component = UnityEngine.Object.Instantiate<GameObject>(this.TeamPanelTemplate.gameObject).GetComponent<OrdealTeamPanel>();
        component.gameObject.SetActive(true);
        foreach (UnitData unit in ordealParties[index1].Units)
        {
          if (unit != null)
            component.Add(unit);
        }
        int index = index1;
        component.Button.onClick.AddListener((UnityAction) (() => this.OnClickTeamPanel(index)));
        component.TeamName.text = ordealParties[index1].Name;
        SupportData supportData = (SupportData) null;
        if (ordealSupports != null && index1 < ordealSupports.Count)
        {
          supportData = ordealSupports[index1];
          component.SetSupport(supportData);
        }
        int num = PartyUtility.CalcTotalAttack(ordealParties[index1], MonoSingleton<GameManager>.Instance.Player.Units, supportData, (List<UnitData>) null);
        component.TotalAtack.text = num.ToString();
        this.mTeamPanels.Add(component.gameObject);
        component.transform.SetParent(this.TeamPanelContainer.transform, false);
      }
      this.CheckPlayableTeams(this.mCurrentQuest, ordealParties, ordealSupports);
    }

    private void OnClickTeamPanel(int index)
    {
      GlobalVars.SelectedTeamIndex = index;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 1100);
    }

    private void CheckPlayableTeams(QuestParam quest, List<PartyEditData> teams, List<SupportData> supports = null)
    {
      if (!((UnityEngine.Object) this.StartButton != (UnityEngine.Object) null))
        return;
      this.StartButton.interactable = PartyUtility.ValidateOrdealTeams(quest, teams, supports, true);
    }

    private List<PartyEditData> LoadTeamFromPlayerPrefs()
    {
      int maxTeamCount = PartyWindow2.EditPartyTypes.Ordeal.GetMaxTeamCount();
      int lastSelectionIndex;
      List<PartyEditData> teams = PartyUtility.LoadTeamPresets(PartyWindow2.EditPartyTypes.Ordeal, out lastSelectionIndex, false) ?? new List<PartyEditData>();
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
      PartyUtility.SaveTeamPresets(PartyWindow2.EditPartyTypes.Ordeal, 0, teams, false);
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
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.QUEST_OUT_OF_DATE"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      }
      else
      {
        if (dataOfClass.quests != null && dataOfClass.quests.Count > 0)
        {
          this.QuestListText.text = dataOfClass.quests[0].name;
          for (int index = 0; index < dataOfClass.quests.Count; ++index)
          {
            QuestParam quest = dataOfClass.quests[index];
            GameObject original = (GameObject) null;
            if (!string.IsNullOrEmpty(quest.ItemLayout))
              original = AssetManager.Load<GameObject>("QuestListItems/" + quest.ItemLayout);
            if ((UnityEngine.Object) original == (UnityEngine.Object) null)
              original = this.ItemQuestTemplate;
            if ((UnityEngine.Object) original != (UnityEngine.Object) null)
            {
              GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original);
              gameObject.hideFlags = HideFlags.DontSave;
              DataSource.Bind<QuestParam>(gameObject, quest, false);
              RankingQuestParam availableRankingQuest = MonoSingleton<GameManager>.Instance.FindAvailableRankingQuest(quest.iname);
              DataSource.Bind<RankingQuestParam>(gameObject, availableRankingQuest, false);
              DataSource.Bind<QuestParam>(gameObject, quest, false);
              QuestCampaignData[] questCampaigns = MonoSingleton<GameManager>.Instance.FindQuestCampaigns(quest);
              DataSource.Bind<QuestCampaignData[]>(gameObject, questCampaigns.Length != 0 ? questCampaigns : (QuestCampaignData[]) null, false);
              ListItemEvents component = gameObject.GetComponent<ListItemEvents>();
              component.OnSelect = new ListItemEvents.ListItemEvent(this.OndetailSelect);
              component.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnOpenItemDetail);
              component.OnCloseDetail = new ListItemEvents.ListItemEvent(this.OnCloseItemDetail);
              gameObject.transform.SetParent(this.ItemQuestContainer.transform, false);
              gameObject.gameObject.SetActive(true);
              this.mListItems.Add(component);
            }
          }
        }
        if (!((UnityEngine.Object) this.ItemQuestContainer != (UnityEngine.Object) null))
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
      DataSource.Bind<QuestParam>(this.gameObject, this.mCurrentQuest, false);
      this.ResetMissionButton();
      this.LoadBossData(this.mCurrentQuest);
      this.LoadTeam();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 1200);
    }

    private void OnCloseItemDetail(GameObject go)
    {
      if (!((UnityEngine.Object) this.mDetailInfo != (UnityEngine.Object) null))
        return;
      UnityEngine.Object.DestroyImmediate((UnityEngine.Object) this.mDetailInfo.gameObject);
      this.mDetailInfo = (GameObject) null;
    }

    private void OnOpenItemDetail(GameObject go)
    {
      QuestParam dataOfClass = DataSource.FindDataOfClass<QuestParam>(go, (QuestParam) null);
      if (!((UnityEngine.Object) this.mDetailInfo == (UnityEngine.Object) null) || dataOfClass == null)
        return;
      this.mDetailInfo = UnityEngine.Object.Instantiate<GameObject>(this.DetailTemplate);
      DataSource.Bind<QuestParam>(this.mDetailInfo, dataOfClass, false);
      QuestCampaignData[] questCampaigns = MonoSingleton<GameManager>.Instance.FindQuestCampaigns(dataOfClass);
      DataSource.Bind<QuestCampaignData[]>(this.mDetailInfo, questCampaigns.Length != 0 ? questCampaigns : (QuestCampaignData[]) null, false);
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
      List<UnitData> unitDataList = new List<UnitData>();
      List<PartyEditData> ordealParties = GlobalVars.OrdealParties;
      for (int index1 = 0; index1 < ordealParties.Count; ++index1)
      {
        PartyEditData partyEditData = ordealParties[index1];
        for (int index2 = 0; index2 < partyEditData.Units.Length; ++index2)
        {
          UnitData unit = partyEditData.Units[index2];
          if (unit != null)
            unitDataList.Add(unit);
        }
      }
      List<SupportData> ordealSupports = GlobalVars.OrdealSupports;
      for (int index = 0; index < ordealSupports.Count; ++index)
      {
        UnitData unitData = ordealSupports[index] == null ? (UnitData) null : ordealSupports[index].Unit;
        if (unitData != null)
          unitDataList.Add(unitData);
      }
      return unitDataList;
    }
  }
}
