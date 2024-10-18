// Decompiled with JetBrains decompiler
// Type: SRPG.CollaboSkillQuestList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(10, "リスト切り替え", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(100, "クエストが選択された", FlowNode.PinTypes.Output, 100)]
  public class CollaboSkillQuestList : MonoBehaviour, IFlowInterface
  {
    public string DisableFlagName = "is_disable";
    private List<GameObject> mStoryQuestListItems = new List<GameObject>();
    private bool mIsStoryList = true;
    public UnitData CurrentUnit1;
    public UnitData CurrentUnit2;
    public Transform QuestList;
    public GameObject StoryQuestItemTemplate;
    public GameObject StoryQuestDisableItemTemplate;
    public GameObject QuestDetailTemplate;
    public GameObject CharacterImage1;
    public GameObject CharacterImage2;
    private GameObject mQuestDetail;
    public Image ListToggleButton;
    public Sprite StoryListSprite;
    private bool mListRefreshing;
    private bool mIsRestore;

    public bool IsRestore
    {
      get
      {
        return this.mIsRestore;
      }
      set
      {
        this.mIsRestore = value;
      }
    }

    protected virtual void Start()
    {
      if ((UnityEngine.Object) this.StoryQuestItemTemplate != (UnityEngine.Object) null)
        this.StoryQuestItemTemplate.gameObject.SetActive(false);
      this.UpdateToggleButton();
      this.RefreshQuestList();
    }

    public static List<QuestParam> GetCollaboSkillQuests(UnitData unitData1, UnitData unitData2)
    {
      List<QuestParam> questParamList = new List<QuestParam>();
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if ((UnityEngine.Object) instanceDirect == (UnityEngine.Object) null)
        return questParamList;
      QuestParam collaboSkillQuest = CollaboSkillQuestList.GetCollaboSkillQuest(unitData1, unitData2);
      if (collaboSkillQuest != null)
      {
        QuestParam[] quests = instanceDirect.Quests;
        for (int index = 0; index < quests.Length; ++index)
        {
          if (quests[index].ChapterID == collaboSkillQuest.ChapterID)
            questParamList.Add(quests[index]);
        }
      }
      return questParamList;
    }

    public static QuestParam GetCollaboSkillQuest(UnitData unitData1, UnitData unitData2)
    {
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if ((UnityEngine.Object) instanceDirect == (UnityEngine.Object) null)
        return (QuestParam) null;
      return CollaboSkillQuestList.GetLearnSkillQuest(instanceDirect.MasterParam.GetCollaboSkillData(unitData1.UnitParam.iname), unitData2);
    }

    private static QuestParam GetLearnSkillQuest(CollaboSkillParam csp, UnitData partner)
    {
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if ((UnityEngine.Object) instanceDirect == (UnityEngine.Object) null)
        return (QuestParam) null;
      if (csp == null || partner == null)
        return (QuestParam) null;
      CollaboSkillParam.LearnSkill learnSkill = csp.LearnSkillLists.Find((Predicate<CollaboSkillParam.LearnSkill>) (ls => ls.PartnerUnitIname == partner.UnitParam.iname));
      if (learnSkill != null)
        return instanceDirect.FindQuest(learnSkill.QuestIname);
      DebugUtility.LogError("learnSkill がnull");
      return (QuestParam) null;
    }

    private void CreateStoryList()
    {
      if ((UnityEngine.Object) MonoSingleton<GameManager>.GetInstanceDirect() == (UnityEngine.Object) null)
        return;
      List<QuestParam> collaboSkillQuests = CollaboSkillQuestList.GetCollaboSkillQuests(this.CurrentUnit1, this.CurrentUnit2);
      if (collaboSkillQuests == null)
      {
        DebugUtility.LogError(string.Format("連携スキルクエストが見つかりません:{0} × {1}", (object) this.CurrentUnit1.UnitParam.iname, (object) this.CurrentUnit2.UnitParam.iname));
      }
      else
      {
        QuestParam[] availableQuests = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
        for (int index = 0; index < collaboSkillQuests.Count; ++index)
        {
          QuestParam questParam = collaboSkillQuests[index];
          bool flag1 = questParam.IsDateUnlock(-1L);
          bool flag2 = Array.Find<QuestParam>(availableQuests, (Predicate<QuestParam>) (p => p == questParam)) != null;
          bool flag3 = questParam.state == QuestStates.Cleared;
          bool flag4 = flag1 && flag2 && !flag3;
          GameObject gameObject;
          if (flag2 || flag3)
          {
            gameObject = UnityEngine.Object.Instantiate<GameObject>(this.StoryQuestItemTemplate);
            gameObject.GetComponent<Button>().interactable = flag4;
          }
          else
            gameObject = UnityEngine.Object.Instantiate<GameObject>(this.StoryQuestDisableItemTemplate);
          gameObject.SetActive(true);
          gameObject.transform.SetParent(this.QuestList, false);
          DataSource.Bind<QuestParam>(gameObject, questParam, false);
          CharacterQuestListItem component1 = gameObject.GetComponent<CharacterQuestListItem>();
          if ((UnityEngine.Object) component1 != (UnityEngine.Object) null)
            component1.SetUp(this.CurrentUnit1, this.CurrentUnit2, questParam);
          ListItemEvents component2 = gameObject.GetComponent<ListItemEvents>();
          component2.OnSelect = new ListItemEvents.ListItemEvent(this.OnQuestSelect);
          component2.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnOpenItemDetail);
          component2.OnCloseDetail = new ListItemEvents.ListItemEvent(this.OnCloseItemDetail);
          this.mStoryQuestListItems.Add(gameObject);
        }
      }
    }

    private void RefreshQuestList()
    {
      if (this.mListRefreshing || (UnityEngine.Object) this.StoryQuestItemTemplate == (UnityEngine.Object) null || ((UnityEngine.Object) this.StoryQuestDisableItemTemplate == (UnityEngine.Object) null || (UnityEngine.Object) this.QuestList == (UnityEngine.Object) null))
        return;
      this.mListRefreshing = true;
      if (this.mStoryQuestListItems.Count <= 0)
        this.CreateStoryList();
      for (int index = 0; index < this.mStoryQuestListItems.Count; ++index)
        this.mStoryQuestListItems[index].SetActive(this.mIsStoryList);
      DataSource.Bind<UnitData>(this.CharacterImage1, this.CurrentUnit1, false);
      DataSource.Bind<UnitData>(this.CharacterImage2, this.CurrentUnit2, false);
      this.mListRefreshing = false;
    }

    private void OnQuestSelect(GameObject button)
    {
      List<GameObject> storyQuestListItems = this.mStoryQuestListItems;
      int index = storyQuestListItems.IndexOf(button.gameObject);
      QuestParam quest = DataSource.FindDataOfClass<QuestParam>(storyQuestListItems[index], (QuestParam) null);
      if (quest == null)
        return;
      if (!quest.IsDateUnlock(-1L))
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.DISABLE_QUEST_DATE_UNLOCK"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      else if (Array.Find<QuestParam>(MonoSingleton<GameManager>.Instance.Player.AvailableQuests, (Predicate<QuestParam>) (p => p == quest)) == null)
      {
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.DISABLE_QUEST_CHALLENGE"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      }
      else
      {
        GlobalVars.SelectedQuestID = quest.iname;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      }
    }

    private void OnCloseItemDetail(GameObject go)
    {
      if (!((UnityEngine.Object) this.mQuestDetail != (UnityEngine.Object) null))
        return;
      UnityEngine.Object.DestroyImmediate((UnityEngine.Object) this.mQuestDetail.gameObject);
      this.mQuestDetail = (GameObject) null;
    }

    private void OnOpenItemDetail(GameObject go)
    {
      QuestParam dataOfClass = DataSource.FindDataOfClass<QuestParam>(go, (QuestParam) null);
      if (!((UnityEngine.Object) this.mQuestDetail == (UnityEngine.Object) null) || dataOfClass == null)
        return;
      this.mQuestDetail = UnityEngine.Object.Instantiate<GameObject>(this.QuestDetailTemplate);
      DataSource.Bind<QuestParam>(this.mQuestDetail, dataOfClass, false);
      DataSource.Bind<UnitData>(this.mQuestDetail, this.CurrentUnit1, false);
      this.mQuestDetail.SetActive(true);
    }

    private void OnToggleButton()
    {
      if (this.mListRefreshing)
        return;
      this.mIsStoryList = !this.mIsStoryList;
      this.UpdateToggleButton();
      this.RefreshQuestList();
    }

    private void UpdateToggleButton()
    {
      if (!((UnityEngine.Object) this.ListToggleButton != (UnityEngine.Object) null))
        return;
      this.ListToggleButton.sprite = this.StoryListSprite;
    }

    public void Activated(int pinID)
    {
      if (pinID != 10)
        return;
      this.OnToggleButton();
    }
  }
}
