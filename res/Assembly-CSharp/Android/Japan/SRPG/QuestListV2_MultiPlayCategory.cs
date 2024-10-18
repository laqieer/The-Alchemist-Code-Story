// Decompiled with JetBrains decompiler
// Type: SRPG.QuestListV2_MultiPlayCategory
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [AddComponentMenu("Multi/クエストカテゴリー一覧")]
  [FlowNode.Pin(0, "通常クエストを表示", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "イベントクエストを表示", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "GPSクエストを含めて表示", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "選択された", FlowNode.PinTypes.Output, 101)]
  public class QuestListV2_MultiPlayCategory : MonoBehaviour, IFlowInterface
  {
    [Description("リストアイテムとして使用するゲームオブジェクト")]
    public ListItemEvents ItemTemplate;
    [Description("詳細画面として使用するゲームオブジェクト")]
    public GameObject DetailTemplate;
    private GameObject mDetailInfo;
    public ScrollRect ScrollRect;

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 0:
          this.Refresh(false, QuestListV2_MultiPlayCategory.DISPLAY_QUEST_TYPE.Normal);
          break;
        case 1:
          this.Refresh(true, QuestListV2_MultiPlayCategory.DISPLAY_QUEST_TYPE.Normal);
          break;
        case 2:
          this.Refresh(false, QuestListV2_MultiPlayCategory.DISPLAY_QUEST_TYPE.WithGps);
          break;
      }
    }

    private void Awake()
    {
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

    private void Refresh(bool isEvent, QuestListV2_MultiPlayCategory.DISPLAY_QUEST_TYPE type = QuestListV2_MultiPlayCategory.DISPLAY_QUEST_TYPE.Normal)
    {
      GlobalVars.SelectedMultiPlayQuestIsEvent = isEvent;
      this.RefreshItems(type);
      if (!((UnityEngine.Object) this.ScrollRect != (UnityEngine.Object) null))
        return;
      ListExtras component = this.ScrollRect.GetComponent<ListExtras>();
      if ((UnityEngine.Object) component != (UnityEngine.Object) null)
        component.SetScrollPos(1f);
      else
        this.ScrollRect.normalizedPosition = Vector2.one;
    }

    private void RefreshItems(QuestListV2_MultiPlayCategory.DISPLAY_QUEST_TYPE dispMode)
    {
      Transform transform1 = this.transform;
      DateTime serverTime = TimeManager.ServerTime;
      for (int index = transform1.childCount - 1; index >= 0; --index)
      {
        Transform child = transform1.GetChild(index);
        if (!((UnityEngine.Object) child == (UnityEngine.Object) null) && child.gameObject.activeSelf)
          UnityEngine.Object.DestroyImmediate((UnityEngine.Object) child.gameObject);
      }
      if ((UnityEngine.Object) this.ItemTemplate == (UnityEngine.Object) null)
        return;
      List<QuestListV2_MultiPlayCategory.ChapterParamExtension> chapterParamExtensionList = new List<QuestListV2_MultiPlayCategory.ChapterParamExtension>();
      for (int index = 0; index < MonoSingleton<GameManager>.Instance.Quests.Length; ++index)
      {
        QuestParam quest = MonoSingleton<GameManager>.Instance.Quests[index];
        if (quest != null && (quest.type == QuestTypes.Multi || quest.IsMultiAreaQuest) && (quest.IsMultiEvent == GlobalVars.SelectedMultiPlayQuestIsEvent && quest.IsMultiVersus == (GlobalVars.SelectedMultiPlayRoomType == JSON_MyPhotonRoomParam.EType.VERSUS) && (!quest.IsMultiAreaQuest || dispMode == QuestListV2_MultiPlayCategory.DISPLAY_QUEST_TYPE.WithGps)) && (!quest.IsMultiAreaQuest || quest.gps_enable))
        {
          ChapterParam area = MonoSingleton<GameManager>.Instance.FindArea(quest.ChapterID);
          if (area != null)
          {
            bool flag = false;
            if (area.IsArchiveQuest)
            {
              if (!quest.hidden && MonoSingleton<GameManager>.Instance.Player.IsQuestArchiveOpenByArea(quest.ChapterID))
                flag = true;
              else
                continue;
            }
            else if (!quest.IsDateUnlock(-1L) || !area.IsAvailable(serverTime))
              continue;
            QuestListV2_MultiPlayCategory.ChapterParamExtension chapterParamExtension1 = chapterParamExtensionList.Find((Predicate<QuestListV2_MultiPlayCategory.ChapterParamExtension>) (a => a.ChapterParam.iname.Equals(area.iname)));
            if (chapterParamExtension1 != null)
            {
              chapterParamExtension1.IsQuestArchiveOpen |= flag;
            }
            else
            {
              QuestListV2_MultiPlayCategory.ChapterParamExtension chapterParamExtension2 = new QuestListV2_MultiPlayCategory.ChapterParamExtension(area);
              chapterParamExtension2.IsQuestArchiveOpen |= flag;
              chapterParamExtensionList.Add(chapterParamExtension2);
            }
          }
        }
      }
      Dictionary<string, int> indexList = new Dictionary<string, int>();
      for (int index = 0; index < chapterParamExtensionList.Count; ++index)
        indexList.Add(chapterParamExtensionList[index].ChapterParam.iname, index);
      chapterParamExtensionList.Sort((Comparison<QuestListV2_MultiPlayCategory.ChapterParamExtension>) ((x, y) =>
      {
        bool flag1 = x.ChapterParam.IsMultiGpsQuest();
        bool flag2 = y.ChapterParam.IsMultiGpsQuest();
        if (x.IsQuestArchiveOpen && !y.IsQuestArchiveOpen)
          return -1;
        if (!x.IsQuestArchiveOpen && y.IsQuestArchiveOpen)
          return 1;
        if (flag1 && !flag2)
          return -1;
        if (!flag1 && flag2)
          return 1;
        if (!indexList.ContainsKey(x.ChapterParam.iname) || !indexList.ContainsKey(y.ChapterParam.iname))
          return 0;
        return indexList[x.ChapterParam.iname] - indexList[y.ChapterParam.iname];
      }));
      for (int index = 0; index < chapterParamExtensionList.Count; ++index)
      {
        ListItemEvents original = (ListItemEvents) null;
        if (!string.IsNullOrEmpty(chapterParamExtensionList[index].ChapterParam.prefabPath))
        {
          StringBuilder stringBuilder = GameUtility.GetStringBuilder();
          stringBuilder.Append("QuestChapters/");
          stringBuilder.Append(chapterParamExtensionList[index].ChapterParam.prefabPath);
          original = AssetManager.Load<ListItemEvents>(stringBuilder.ToString());
        }
        if ((UnityEngine.Object) original == (UnityEngine.Object) null)
          original = this.ItemTemplate;
        ListItemEvents listItemEvents = UnityEngine.Object.Instantiate<ListItemEvents>(original);
        if (chapterParamExtensionList[index].ChapterParam.IsArchiveQuest)
        {
          ArchiveParam archiveByArea = MonoSingleton<GameManager>.Instance.FindArchiveByArea(chapterParamExtensionList[index].ChapterParam.iname);
          if (archiveByArea != null)
          {
            OpenedQuestArchive openedQuestArchive = MonoSingleton<GameManager>.Instance.Player.GetOpenedQuestArchive(archiveByArea.iname);
            DataSource.Bind<OpenedQuestArchive>(listItemEvents.gameObject, openedQuestArchive, false);
          }
        }
        DataSource.Bind<ChapterParam>(listItemEvents.gameObject, chapterParamExtensionList[index].ChapterParam, false);
        listItemEvents.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelectItem);
        listItemEvents.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnOpenItemDetail);
        listItemEvents.OnCloseDetail = new ListItemEvents.ListItemEvent(this.OnCloseItemDetail);
        Transform transform2 = listItemEvents.transform.Find("bg");
        if ((UnityEngine.Object) transform2 != (UnityEngine.Object) null)
        {
          Transform transform3 = transform2.Find("timer_base");
          if ((UnityEngine.Object) transform3 != (UnityEngine.Object) null && chapterParamExtensionList[index].ChapterParam.end <= 0L)
            transform3.gameObject.SetActive(false);
        }
        listItemEvents.transform.SetParent(transform1, false);
        listItemEvents.gameObject.SetActive(true);
      }
    }

    private void OnSelectItem(GameObject go)
    {
      ChapterParam dataOfClass = DataSource.FindDataOfClass<ChapterParam>(go, (ChapterParam) null);
      if (dataOfClass == null)
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      QuestParam[] availableQuests = instance.Player.AvailableQuests;
      bool flag = false;
      if (dataOfClass.IsArchiveQuest && instance.Player.IsQuestArchiveOpenByArea(dataOfClass.iname))
      {
        flag = false;
      }
      else
      {
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
          flag = true;
      }
      if (flag)
      {
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.QUEST_OUT_OF_DATE"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      }
      else
      {
        GlobalVars.SelectedMultiPlayArea = dataOfClass.iname;
        DebugUtility.Log("Select Play Area:" + GlobalVars.SelectedMultiPlayArea);
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
      }
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
    }

    private enum DISPLAY_QUEST_TYPE
    {
      Normal,
      WithGps,
      Max,
    }

    private class ChapterParamExtension
    {
      public readonly ChapterParam m_ChapterParam;
      public bool m_IsQuestArchiveOpen;

      public ChapterParamExtension(ChapterParam chapterParam)
      {
        this.m_ChapterParam = chapterParam;
      }

      public bool IsQuestArchiveOpen
      {
        get
        {
          return this.m_IsQuestArchiveOpen;
        }
        set
        {
          this.m_IsQuestArchiveOpen = value;
        }
      }

      public ChapterParam ChapterParam
      {
        get
        {
          return this.m_ChapterParam;
        }
      }
    }
  }
}
