// Decompiled with JetBrains decompiler
// Type: SRPG.QuestSectionList
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
  [FlowNode.Pin(0, "ストーリーパート解放準備", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "アイテムが選択された", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "ストーリーパート表示", FlowNode.PinTypes.Output, 101)]
  public class QuestSectionList : MonoBehaviour, IFlowInterface
  {
    public ListItemEvents ItemTemplate;
    public GameObject ItemContainer;
    public ScrollRect Scroll;
    public string WorldMapControllerID;
    public bool RefreshOnStart = true;
    public ImageArray StoryPartIcon;
    private List<ListItemEvents> mItems = new List<ListItemEvents>();

    public void Activated(int pinID)
    {
      if (pinID != 0)
        return;
      this.Refresh();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
    }

    private void Start()
    {
      if (Object.op_Inequality((Object) this.ItemTemplate, (Object) null))
        ((Component) this.ItemTemplate).gameObject.SetActive(false);
      if (!this.RefreshOnStart)
        return;
      this.Refresh();
    }

    public void Refresh()
    {
      GameUtility.DestroyGameObjects<ListItemEvents>(this.mItems);
      if (Object.op_Equality((Object) this.ItemTemplate, (Object) null) || Object.op_Equality((Object) this.ItemContainer, (Object) null))
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      PlayerData player = instance.Player;
      SectionParam[] array = ((IEnumerable<SectionParam>) instance.Sections).Reverse<SectionParam>().ToArray<SectionParam>();
      List<string> stringList1 = new List<string>();
      List<string> stringList2 = new List<string>();
      long serverTime = Network.GetServerTime();
      QuestParam[] availableQuests = player.AvailableQuests;
      for (int index = 0; index < availableQuests.Length; ++index)
      {
        if (!string.IsNullOrEmpty(availableQuests[index].ChapterID) && !stringList1.Contains(availableQuests[index].ChapterID) && !availableQuests[index].IsMulti && availableQuests[index].IsDateUnlock(serverTime))
          stringList1.Add(availableQuests[index].ChapterID);
      }
      for (int index = 0; index < stringList1.Count; ++index)
      {
        ChapterParam area = instance.FindArea(stringList1[index]);
        if (area != null && !stringList2.Contains(area.section))
          stringList2.Add(area.section);
      }
      for (int index = 0; index < array.Length; ++index)
      {
        SectionParam sectionParam = array[index];
        if (array[index].IsDateUnlock() && stringList2.Contains(array[index].iname) && sectionParam.storyPart == GlobalVars.SelectedStoryPart.Get())
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
      if (!Object.op_Inequality((Object) this.StoryPartIcon, (Object) null))
        return;
      int num = GlobalVars.SelectedStoryPart.Get();
      if (num == 100)
        num = 1;
      this.StoryPartIcon.ImageIndex = Mathf.Max(0, num - 1);
    }

    public void ResetScroll()
    {
      if (!Object.op_Inequality((Object) this.ItemContainer, (Object) null) || !Object.op_Inequality((Object) this.Scroll, (Object) null))
        return;
      this.Scroll.normalizedPosition = new Vector2(0.0f, 1f);
    }

    private void OnItemSelect(GameObject go)
    {
      UIQuestSectionData dataOfClass = DataSource.FindDataOfClass<UIQuestSectionData>(go, (UIQuestSectionData) null);
      GlobalVars.SelectedSection.Set(dataOfClass.SectionID);
      WorldMapController instance1 = WorldMapController.FindInstance(this.WorldMapControllerID);
      if (Object.op_Inequality((Object) instance1, (Object) null))
      {
        GameManager instance2 = MonoSingleton<GameManager>.Instance;
        for (int index = 0; index < instance2.Chapters.Length; ++index)
        {
          if (instance2.Chapters[index].section == dataOfClass.SectionID)
          {
            instance1.GotoArea(instance2.Chapters[index].world);
            break;
          }
        }
      }
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
    }
  }
}
