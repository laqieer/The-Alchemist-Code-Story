// Decompiled with JetBrains decompiler
// Type: SRPG.ReplayQuestSectionList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(0, "更新", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "閲覧可能なストーリーがある", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(11, "閲覧可能なストーリーがない", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(12, "アイテムが選択された", FlowNode.PinTypes.Output, 102)]
  public class ReplayQuestSectionList : MonoBehaviour, IFlowInterface
  {
    private const int PIN_ID_REFRESH = 0;
    private const int PIN_ID_EXIST = 10;
    private const int PIN_ID_NOT_EXIST = 11;
    private const int PIN_ID_SELECT = 12;
    public ListItemEvents ItemTemplate;
    public GameObject ItemContainer;
    private bool isRefreshed;
    public ScrollRect ScrollRect;
    private List<ListItemEvents> mItems = new List<ListItemEvents>();

    public void Activated(int pinID)
    {
      if (pinID != 0)
        return;
      this.Refresh();
    }

    private void Start()
    {
      if (Object.op_Inequality((Object) this.ItemTemplate, (Object) null))
        ((Component) this.ItemTemplate).gameObject.SetActive(false);
      this.Refresh();
    }

    private void Refresh()
    {
      if (this.isRefreshed)
        return;
      this.isRefreshed = true;
      GameUtility.DestroyGameObjects<ListItemEvents>(this.mItems);
      this.mItems.Clear();
      if (Object.op_Equality((Object) this.ItemTemplate, (Object) null) || Object.op_Equality((Object) this.ItemContainer, (Object) null))
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      SectionParam[] array = ((IEnumerable<SectionParam>) instance.Sections).Reverse<SectionParam>().ToArray<SectionParam>();
      List<string> stringList1 = new List<string>();
      List<string> stringList2 = new List<string>();
      long serverTime = Network.GetServerTime();
      foreach (QuestParam availableQuest in instance.Player.AvailableQuests)
      {
        if (!string.IsNullOrEmpty(availableQuest.ChapterID) && !availableQuest.IsMulti && availableQuest.IsReplayDateUnlock(serverTime) && !stringList1.Contains(availableQuest.ChapterID) && (!string.IsNullOrEmpty(availableQuest.event_start) || !string.IsNullOrEmpty(availableQuest.event_clear)) && (availableQuest.state == QuestStates.Challenged || availableQuest.state == QuestStates.Cleared))
          stringList1.Add(availableQuest.ChapterID);
      }
      for (int index = 0; index < stringList1.Count; ++index)
      {
        ChapterParam area = instance.FindArea(stringList1[index]);
        if (area != null && !stringList2.Contains(area.section) && area.section != "WD_DAILY" && area.section != "WD_CHARA" && area.section != "WD_GENESIS" && area.section != "WD_ADVANCE" && area.section != "WD_RENTAL")
          stringList2.Add(area.section);
      }
      for (int index = 0; index < array.Length; ++index)
      {
        SectionParam sectionParam = array[index];
        if (stringList2.Contains(array[index].iname))
        {
          ListItemEvents listItemEvents1 = (ListItemEvents) null;
          if (!string.IsNullOrEmpty(sectionParam.prefabPath))
          {
            StringBuilder stringBuilder = GameUtility.GetStringBuilder();
            stringBuilder.Append("QuestSections/");
            stringBuilder.Append(sectionParam.prefabPath);
            listItemEvents1 = AssetManager.Load<ListItemEvents>(stringBuilder.ToString());
          }
          if (Object.op_Equality((Object) listItemEvents1, (Object) null))
            listItemEvents1 = this.ItemTemplate;
          ListItemEvents listItemEvents2 = Object.Instantiate<ListItemEvents>(listItemEvents1);
          DataSource.Bind<UIQuestSectionData>(((Component) listItemEvents2).gameObject, new UIQuestSectionData(array[index]));
          ((Component) listItemEvents2).transform.SetParent(this.ItemContainer.transform, false);
          ((Component) listItemEvents2).gameObject.SetActive(true);
          listItemEvents2.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemSelect);
          this.mItems.Add(listItemEvents2);
        }
      }
      if (Object.op_Inequality((Object) this.ScrollRect, (Object) null))
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
