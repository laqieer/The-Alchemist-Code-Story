// Decompiled with JetBrains decompiler
// Type: SRPG.QuestSectionList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(0, "ストーリーパート解放準備", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "アイテムが選択された", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "ストーリーパート表示", FlowNode.PinTypes.Output, 101)]
  public class QuestSectionList : MonoBehaviour, IFlowInterface
  {
    public bool RefreshOnStart = true;
    private List<ListItemEvents> mItems = new List<ListItemEvents>();
    public ListItemEvents ItemTemplate;
    public GameObject ItemContainer;
    public string WorldMapControllerID;
    public ImageArray StoryPartIcon;

    public void Activated(int pinID)
    {
      if (pinID != 0)
        return;
      this.Refresh();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
    }

    private void Start()
    {
      if ((UnityEngine.Object) this.ItemTemplate != (UnityEngine.Object) null)
        this.ItemTemplate.gameObject.SetActive(false);
      if (this.RefreshOnStart)
        this.Refresh();
      WorldMapController instance = WorldMapController.FindInstance(this.WorldMapControllerID);
      if (!((UnityEngine.Object) instance != (UnityEngine.Object) null))
        return;
      instance.SectionList = this;
    }

    public void Refresh()
    {
      GameUtility.DestroyGameObjects<ListItemEvents>(this.mItems);
      if ((UnityEngine.Object) this.ItemTemplate == (UnityEngine.Object) null || (UnityEngine.Object) this.ItemContainer == (UnityEngine.Object) null)
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
        if (!string.IsNullOrEmpty(availableQuests[index].ChapterID) && !stringList1.Contains(availableQuests[index].ChapterID) && (!availableQuests[index].IsMulti && availableQuests[index].IsDateUnlock(serverTime)))
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
      if (!((UnityEngine.Object) this.StoryPartIcon != (UnityEngine.Object) null))
        return;
      this.StoryPartIcon.ImageIndex = GlobalVars.SelectedStoryPart.Get() - 1;
    }

    private void OnItemSelect(GameObject go)
    {
      UIQuestSectionData dataOfClass = DataSource.FindDataOfClass<UIQuestSectionData>(go, (UIQuestSectionData) null);
      GlobalVars.SelectedSection.Set(dataOfClass.SectionID);
      WorldMapController instance1 = WorldMapController.FindInstance(this.WorldMapControllerID);
      if ((UnityEngine.Object) instance1 != (UnityEngine.Object) null)
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
