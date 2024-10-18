// Decompiled with JetBrains decompiler
// Type: SRPG.QuestListV2_MultiPlayCategory
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(402, "Disable filter animation", FlowNode.PinTypes.Input, 402)]
  [FlowNode.Pin(101, "選択された", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(401, "Enable filter animation", FlowNode.PinTypes.Input, 401)]
  [FlowNode.Pin(400, "SG Filter View Mode", FlowNode.PinTypes.Input, 400)]
  [FlowNode.Pin(1, "イベントクエストを表示", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(0, "通常クエストを表示", FlowNode.PinTypes.Input, 0)]
  [AddComponentMenu("Multi/クエストカテゴリー一覧")]
  public class QuestListV2_MultiPlayCategory : MonoBehaviour, IFlowInterface
  {
    [Description("リストアイテムとして使用するゲームオブジェクト")]
    public ListItemEvents ItemTemplate;
    [Description("詳細画面として使用するゲームオブジェクト")]
    public GameObject DetailTemplate;
    private GameObject mDetailInfo;
    public ScrollRect ScrollRect;
    private bool isQuestFilter;
    private bool isScaledDown;
    private int selectedBanner;
    [SerializeField]
    private Animator filterAnimator;

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 0:
          this.Refresh(false);
          break;
        case 1:
          this.Refresh(true);
          break;
        case 400:
          this.Refresh(false);
          this.ScaleDownBanners();
          break;
        case 401:
          this.filterAnimator.enabled = true;
          break;
        case 402:
          this.filterAnimator.enabled = false;
          break;
      }
    }

    private void Awake()
    {
      this.isQuestFilter = false;
      this.isScaledDown = false;
      GlobalVars.SelectedMultiPlayQuestIsEvent = false;
      if ((UnityEngine.Object) this.ItemTemplate != (UnityEngine.Object) null && this.ItemTemplate.gameObject.activeInHierarchy)
        this.ItemTemplate.gameObject.SetActive(false);
      if (!((UnityEngine.Object) this.DetailTemplate != (UnityEngine.Object) null) || !this.DetailTemplate.activeInHierarchy)
        return;
      this.DetailTemplate.SetActive(false);
    }

    private void Start()
    {
    }

    public void Refresh(bool isEvent)
    {
      GlobalVars.SelectedMultiPlayQuestIsEvent = isEvent;
      this.RefreshItems();
      if (!((UnityEngine.Object) this.ScrollRect != (UnityEngine.Object) null))
        return;
      ListExtras component = this.ScrollRect.GetComponent<ListExtras>();
      if ((UnityEngine.Object) component != (UnityEngine.Object) null)
        component.SetScrollPos(1f);
      else
        this.ScrollRect.normalizedPosition = Vector2.one;
    }

    private void RefreshItems()
    {
      Transform transform = this.transform;
      DateTime serverTime = TimeManager.ServerTime;
      for (int index = transform.childCount - 1; index >= 0; --index)
      {
        Transform child = transform.GetChild(index);
        if (!((UnityEngine.Object) child == (UnityEngine.Object) null) && child.gameObject.activeSelf)
          UnityEngine.Object.DestroyImmediate((UnityEngine.Object) child.gameObject);
      }
      if ((UnityEngine.Object) this.ItemTemplate == (UnityEngine.Object) null)
        return;
      List<ChapterParam> chapterParamList = new List<ChapterParam>();
      for (int index = 0; index < MonoSingleton<GameManager>.Instance.Quests.Length; ++index)
      {
        QuestParam quest = MonoSingleton<GameManager>.Instance.Quests[index];
        if (quest != null && quest.type == QuestTypes.Multi && (quest.IsMultiEvent == GlobalVars.SelectedMultiPlayQuestIsEvent && quest.IsMultiVersus == (GlobalVars.SelectedMultiPlayRoomType == JSON_MyPhotonRoomParam.EType.VERSUS)) && quest.IsDateUnlock(-1L))
        {
          ChapterParam area = MonoSingleton<GameManager>.Instance.FindArea(quest.ChapterID);
          if (area != null && area.IsAvailable(serverTime) && chapterParamList.Find((Predicate<ChapterParam>) (a => a.iname.Equals(area.iname))) == null)
            chapterParamList.Add(area);
        }
      }
      for (int index = 0; index < chapterParamList.Count; ++index)
      {
        ListItemEvents original = (ListItemEvents) null;
        if (!string.IsNullOrEmpty(chapterParamList[index].prefabPath))
        {
          StringBuilder stringBuilder = GameUtility.GetStringBuilder();
          stringBuilder.Append("QuestChapters/");
          stringBuilder.Append(chapterParamList[index].prefabPath);
          original = AssetManager.Load<ListItemEvents>(stringBuilder.ToString());
        }
        if ((UnityEngine.Object) original == (UnityEngine.Object) null)
          original = this.ItemTemplate;
        ListItemEvents listItemEvents = UnityEngine.Object.Instantiate<ListItemEvents>(original);
        DataSource.Bind<ChapterParam>(listItemEvents.gameObject, chapterParamList[index]);
        listItemEvents.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelectItem);
        listItemEvents.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnOpenItemDetail);
        listItemEvents.OnCloseDetail = new ListItemEvents.ListItemEvent(this.OnCloseItemDetail);
        Transform child1 = listItemEvents.transform.FindChild("bg");
        if ((UnityEngine.Object) child1 != (UnityEngine.Object) null)
        {
          Transform child2 = child1.FindChild("timer_base");
          if ((UnityEngine.Object) child2 != (UnityEngine.Object) null && chapterParamList[index].end <= 0L)
            child2.gameObject.SetActive(false);
        }
        listItemEvents.transform.SetParent(transform, false);
        listItemEvents.gameObject.SetActive(true);
      }
      if (!this.isQuestFilter)
        return;
      this.isScaledDown = false;
      this.ScaleDownBanners();
    }

    private void OnSelectItem(GameObject go)
    {
      ChapterParam dataOfClass = DataSource.FindDataOfClass<ChapterParam>(go, (ChapterParam) null);
      if (dataOfClass != null)
      {
        QuestParam[] availableQuests = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
        long serverTime = Network.GetServerTime();
        int num1 = 0;
        int num2 = 0;
        for (int index = 0; index < availableQuests.Length; ++index)
        {
          if (availableQuests[index].ChapterID == dataOfClass.iname && availableQuests[index].IsMulti)
          {
            ++num1;
            if (availableQuests[index].IsJigen && !availableQuests[index].IsDateUnlock(serverTime))
              ++num2;
          }
        }
        if (num1 > 0 && num1 == num2)
        {
          UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.QUEST_OUT_OF_DATE"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
          return;
        }
        GlobalVars.SelectedMultiPlayArea = dataOfClass.iname;
        DebugUtility.Log("Select Play Area:" + GlobalVars.SelectedMultiPlayArea);
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
      }
      if (!this.isQuestFilter)
        return;
      this.UpdateSelectedChildren();
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
      DataSource.Bind<QuestParam>(this.mDetailInfo, dataOfClass);
    }

    private void ScaleDownBanners()
    {
      if (this.isScaledDown)
        return;
      this.isScaledDown = true;
      this.isQuestFilter = true;
      Transform transform = this.transform;
      for (int index = transform.childCount - 1; index >= 0; --index)
      {
        Transform child = transform.GetChild(index);
        if (!((UnityEngine.Object) child == (UnityEngine.Object) null))
        {
          LayoutElement component = child.GetComponent<LayoutElement>();
          if ((UnityEngine.Object) component != (UnityEngine.Object) null)
          {
            component.minWidth *= 0.7f;
            component.minHeight *= 0.7f;
          }
        }
      }
    }

    private void UpdateSelectedChildren()
    {
      if (GlobalVars.SelectedMultiPlayArea == null)
        return;
      Transform transform = this.transform;
      for (int index = transform.childCount - 1; index >= 0; --index)
      {
        Transform child = transform.GetChild(index);
        if (!((UnityEngine.Object) child == (UnityEngine.Object) null))
        {
          ChapterParam dataOfClass = DataSource.FindDataOfClass<ChapterParam>(child.gameObject, (ChapterParam) null);
          if (dataOfClass != null)
          {
            if (dataOfClass.iname != GlobalVars.SelectedMultiPlayArea)
              child.GetComponentInChildren<Image>().color = child.GetComponentInChildren<Button>().colors.disabledColor;
            else
              child.GetComponentInChildren<Image>().color = child.GetComponentInChildren<Button>().colors.normalColor;
          }
        }
      }
    }
  }
}
