// Decompiled with JetBrains decompiler
// Type: SRPG.ReplayQuestSectionList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(0, "更新", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "閲覧可能なストーリーがある", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(11, "閲覧可能なストーリーがない", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(12, "アイテムが選択された", FlowNode.PinTypes.Output, 102)]
  public class ReplayQuestSectionList : MonoBehaviour, IFlowInterface
  {
    private List<ListItemEvents> mItems = new List<ListItemEvents>();
    private const int PIN_ID_REFRESH = 0;
    private const int PIN_ID_EXIST = 10;
    private const int PIN_ID_NOT_EXIST = 11;
    private const int PIN_ID_SELECT = 12;
    public ListItemEvents ItemTemplate;
    public GameObject ItemContainer;
    private bool isRefreshed;
    public ScrollRect ScrollRect;

    public void Activated(int pinID)
    {
      if (pinID != 0)
        return;
      this.Refresh();
    }

    private void Start()
    {
      if ((UnityEngine.Object) this.ItemTemplate != (UnityEngine.Object) null)
        this.ItemTemplate.gameObject.SetActive(false);
      this.Refresh();
    }

    private void Refresh()
    {
      if (this.isRefreshed)
        return;
      this.isRefreshed = true;
      GameUtility.DestroyGameObjects<ListItemEvents>(this.mItems);
      this.mItems.Clear();
      if ((UnityEngine.Object) this.ItemTemplate == (UnityEngine.Object) null || (UnityEngine.Object) this.ItemContainer == (UnityEngine.Object) null)
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      SectionParam[] array = ((IEnumerable<SectionParam>) instance.Sections).Reverse<SectionParam>().ToArray<SectionParam>();
      List<string> stringList1 = new List<string>();
      List<string> stringList2 = new List<string>();
      long serverTime = Network.GetServerTime();
      foreach (QuestParam availableQuest in instance.Player.AvailableQuests)
      {
        if (!string.IsNullOrEmpty(availableQuest.ChapterID) && !availableQuest.IsMulti && (availableQuest.IsReplayDateUnlock(serverTime) && !stringList1.Contains(availableQuest.ChapterID)) && ((!string.IsNullOrEmpty(availableQuest.event_start) || !string.IsNullOrEmpty(availableQuest.event_clear)) && (availableQuest.state == QuestStates.Challenged || availableQuest.state == QuestStates.Cleared)))
          stringList1.Add(availableQuest.ChapterID);
      }
      for (int index = 0; index < stringList1.Count; ++index)
      {
        ChapterParam area = instance.FindArea(stringList1[index]);
        if (area != null && !stringList2.Contains(area.section) && (area.section != "WD_DAILY" && area.section != "WD_CHARA") && area.section != "WD_GENESIS")
          stringList2.Add(area.section);
      }
      for (int index = 0; index < array.Length; ++index)
      {
        SectionParam sectionParam = array[index];
        if (stringList2.Contains(array[index].iname))
        {
          ListItemEvents original = (ListItemEvents) null;
          if (!string.IsNullOrEmpty(sectionParam.prefabPath))
          {
            StringBuilder stringBuilder = GameUtility.GetStringBuilder();
            stringBuilder.Append("QuestSections/");
            stringBuilder.Append(sectionParam.prefabPath);
            original = AssetManager.Load<ListItemEvents>(stringBuilder.ToString());
          }
          if ((UnityEngine.Object) original == (UnityEngine.Object) null)
            original = this.ItemTemplate;
          ListItemEvents listItemEvents = UnityEngine.Object.Instantiate<ListItemEvents>(original);
          DataSource.Bind<UIQuestSectionData>(listItemEvents.gameObject, new UIQuestSectionData(array[index]), false);
          listItemEvents.transform.SetParent(this.ItemContainer.transform, false);
          listItemEvents.gameObject.SetActive(true);
          listItemEvents.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemSelect);
          this.mItems.Add(listItemEvents);
        }
      }
      if ((UnityEngine.Object) this.ScrollRect != (UnityEngine.Object) null)
        this.ScrollRect.normalizedPosition = Vector2.one;
      if (this.mItems.Count > 0)
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
      else
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 11);
    }

    private void OnItemSelect(GameObject go)
    {
      UIQuestSectionData dataOfClass = DataSource.FindDataOfClass<UIQuestSectionData>(go, (UIQuestSectionData) null);
      if (dataOfClass == null)
        return;
      GlobalVars.ReplaySelectedSection.Set(dataOfClass.SectionID);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 12);
    }
  }
}
