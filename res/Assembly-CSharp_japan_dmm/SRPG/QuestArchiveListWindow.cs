// Decompiled with JetBrains decompiler
// Type: SRPG.QuestArchiveListWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "TabChange", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(10, "開放ボタンが押下された", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(11, "挑戦ボタンが押下された", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(12, "イベント詳細ボタンがクリックされた", FlowNode.PinTypes.Input, 12)]
  [FlowNode.Pin(100, "クエストを開放するか？", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(110, "既に開放中のクエストの出撃へ", FlowNode.PinTypes.Output, 110)]
  [FlowNode.Pin(120, "開放時間の過ぎたクエストが選択された", FlowNode.PinTypes.Output, 120)]
  [FlowNode.Pin(130, "イベント詳細を開く", FlowNode.PinTypes.Output, 130)]
  public class QuestArchiveListWindow : MonoBehaviour, IFlowInterface
  {
    public const int INPUT_PIN_REFRESH = 1;
    public const int INPUT_PIN_TAB_CHANGE = 2;
    public const int INPUT_PIN_QUEST_OPEN_BTN = 10;
    public const int INPUT_PIN_QUEST_CHALLENGE_BTN = 11;
    public const int INPUT_PIN_QUEST_DETAILS_BTN = 12;
    public const int OUTPUT_PIN_QUEST_OPEN_CONFIRM = 100;
    public const int OUTPUT_PIN_QUEST_OPENED_GO_CHALLENGE = 110;
    public const int OUTPUT_PIN_QUEST_CLOSED_ALREADY = 120;
    public const int OUTPUT_PIN_QUEST_OPEN_EVENT_DETAILS = 130;
    [SerializeField]
    private GameObject QuestArchiveTemplate;
    [SerializeField]
    private Transform ListRoot;
    [SerializeField]
    private ItemIcon KeyItemCount;
    private List<GameObject> mQuestList = new List<GameObject>();
    private QuestArchiveListWindow.ETabType mCurrentTab;
    private GameManager gm;

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          this.RefreshList();
          break;
        case 2:
          int tabtype = (FlowNode_ButtonEvent.currentValue as SerializeValueList).GetInt("tabtype");
          if (!Enum.IsDefined(typeof (QuestArchiveListWindow.ETabType), (object) tabtype))
            break;
          this.ChangeTab((QuestArchiveListWindow.ETabType) tabtype);
          break;
        case 10:
          ArchiveParam dataOfClass1 = DataSource.FindDataOfClass<ArchiveParam>((FlowNode_ButtonEvent.currentValue as SerializeValueList).GetGameObject("_self"), (ArchiveParam) null);
          if (dataOfClass1 != null)
          {
            if (!dataOfClass1.ArePrerequsiteQuestsCleared())
            {
              UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.DISABLE_QUEST_CHALLENGE"), (UIUtility.DialogResultEvent) null);
              break;
            }
            if (!dataOfClass1.IsAvailable())
            {
              FlowNode_GameObject.ActivateOutputLinks((Component) this, 120);
              break;
            }
          }
          GlobalVars.SelectedArchiveID = dataOfClass1.iname;
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
          break;
        case 11:
          ChapterParam dataOfClass2 = DataSource.FindDataOfClass<ChapterParam>((FlowNode_ButtonEvent.currentValue as SerializeValueList).GetGameObject("_self"), (ChapterParam) null);
          ArchiveParam archiveByArea = MonoSingleton<GameManager>.Instance.FindArchiveByArea(dataOfClass2.iname);
          if (archiveByArea != null && (!archiveByArea.IsAvailable() || !this.gm.Player.IsQuestArchiveOpen(archiveByArea.iname)))
          {
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 120);
            break;
          }
          GlobalVars.SelectedChapter.Set(dataOfClass2.iname);
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 110);
          break;
        case 12:
          FlowNode_Variable.Set("ARCHIVE_AREA_URL", DataSource.FindDataOfClass<ChapterParam>((FlowNode_ButtonEvent.currentValue as SerializeValueList).GetGameObject("_self"), (ChapterParam) null).helpURL);
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 130);
          break;
      }
    }

    private void Awake()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.QuestArchiveTemplate, (UnityEngine.Object) null))
        this.QuestArchiveTemplate.SetActive(false);
      this.gm = MonoSingleton<GameManager>.Instance;
      this.mCurrentTab = QuestArchiveListWindow.ETabType.Encounter;
    }

    private void ChangeTab(QuestArchiveListWindow.ETabType tabtype)
    {
      if (this.mCurrentTab == tabtype)
        return;
      this.mCurrentTab = tabtype;
      this.RefreshList();
    }

    private void RefreshList()
    {
      foreach (UnityEngine.Object mQuest in this.mQuestList)
        UnityEngine.Object.Destroy(mQuest);
      this.mQuestList.Clear();
      int num = 0;
      foreach (ArchiveParam archive in MonoSingleton<GameManager>.Instance.Archives)
      {
        if (archive.IsAvailable())
        {
          if (num++ == 0 && archive.keys != null && archive.keys.Count > 0)
          {
            KeyItem key = archive.keys[0];
            if (key == null || string.IsNullOrEmpty(key.iname) || key.num == 0)
              break;
            ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(key.iname);
            ItemData itemDataByItemParam = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemParam(itemParam);
            DataSource.Bind<ItemParam>(((Component) this.KeyItemCount).gameObject, itemParam);
            DataSource.Bind<ItemData>(((Component) this.KeyItemCount).gameObject, itemDataByItemParam);
            DataSource.Bind<KeyItem>(((Component) this.KeyItemCount).gameObject, key);
            this.KeyItemCount.UpdateValue();
          }
          if (this.mCurrentTab == QuestArchiveListWindow.ETabType.Encounter && archive.type == ArchiveTypes.Encounter || this.mCurrentTab == QuestArchiveListWindow.ETabType.Season && archive.type == ArchiveTypes.Season || this.mCurrentTab == QuestArchiveListWindow.ETabType.Artifact && archive.type == ArchiveTypes.Artifact)
          {
            string iname = string.Empty;
            if (!string.IsNullOrEmpty(archive.area_iname))
              iname = archive.area_iname;
            else if (!string.IsNullOrEmpty(archive.area_iname_multi))
              iname = archive.area_iname_multi;
            GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.QuestArchiveTemplate);
            QuestArchiveListItem component = gameObject.GetComponent<QuestArchiveListItem>();
            ChapterParam area = this.gm.FindArea(iname);
            OpenedQuestArchive openedQuestArchive = this.gm.Player.GetOpenedQuestArchive(archive.iname);
            UnitParam unit1Param = (UnitParam) null;
            if (!string.IsNullOrEmpty(archive.unit1))
              unit1Param = this.gm.GetUnitParam(archive.unit1);
            UnitParam unit2Param = (UnitParam) null;
            if (!string.IsNullOrEmpty(archive.unit2))
              unit2Param = this.gm.GetUnitParam(archive.unit2);
            component.SetupParams(archive, openedQuestArchive, area, unit1Param, unit2Param);
            gameObject.transform.SetParent(this.ListRoot, false);
            this.mQuestList.Add(gameObject);
            gameObject.SetActive(true);
          }
        }
      }
    }

    public enum ETabType
    {
      Encounter,
      Season,
      Artifact,
    }
  }
}
