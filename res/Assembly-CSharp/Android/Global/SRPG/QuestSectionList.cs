// Decompiled with JetBrains decompiler
// Type: SRPG.QuestSectionList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(3, "Disable drag", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "On right tap", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(2, "Enable drag", FlowNode.PinTypes.Input, 40)]
  [FlowNode.Pin(0, "On left tap", FlowNode.PinTypes.Input, 40)]
  [FlowNode.Pin(100, "アイテムが選択された", FlowNode.PinTypes.Output, 100)]
  public class QuestSectionList : MonoBehaviour, IFlowInterface
  {
    public bool RefreshOnStart = true;
    private List<ListItemEvents> mItems = new List<ListItemEvents>();
    public ListItemEvents ItemTemplate;
    public GameObject ItemContainer;
    public string WorldMapControllerID;
    private int currentSectionID;

    public void Activated(int pinID)
    {
    }

    private void Start()
    {
      if ((UnityEngine.Object) this.ItemTemplate != (UnityEngine.Object) null)
        this.ItemTemplate.gameObject.SetActive(false);
      if (!this.RefreshOnStart)
        return;
      this.Refresh();
    }

    private void Refresh()
    {
      GameUtility.DestroyGameObjects<ListItemEvents>(this.mItems);
      if ((UnityEngine.Object) this.ItemTemplate == (UnityEngine.Object) null || (UnityEngine.Object) this.ItemContainer == (UnityEngine.Object) null)
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      PlayerData player = instance.Player;
      SectionParam[] sections = instance.Sections;
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
      for (int index = 0; index < sections.Length; ++index)
      {
        SectionParam sectionParam = sections[index];
        if (sections[index].IsDateUnlock() && stringList2.Contains(sections[index].iname))
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
          DataSource.Bind<UIQuestSectionData>(listItemEvents.gameObject, new UIQuestSectionData(sections[index]));
          listItemEvents.transform.SetParent(this.ItemContainer.transform, false);
          listItemEvents.gameObject.SetActive(true);
          listItemEvents.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemSelect);
          SGWorldBannerItem component = listItemEvents.GetComponent<SGWorldBannerItem>();
          if ((UnityEngine.Object) component != (UnityEngine.Object) null)
          {
            ChapterParam area = MonoSingleton<GameManager>.Instance.FindArea((string) GlobalVars.SelectedChapter);
            if (area != null && area.section == sections[index].iname)
            {
              component.SetChapterText(area.name);
              this.currentSectionID = index;
            }
          }
          this.mItems.Add(listItemEvents);
        }
      }
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
