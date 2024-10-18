// Decompiled with JetBrains decompiler
// Type: SRPG.QuestChapterList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(0, "再読み込み", FlowNode.PinTypes.Input, 40)]
  [FlowNode.Pin(1, "ワールドマップへ戻す", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(2, "ロケーションのハイライトを戻す", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(3, "セクション決定", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(4, "階層を上げる", FlowNode.PinTypes.Input, 4)]
  [FlowNode.Pin(40, "セクション選択に戻す", FlowNode.PinTypes.Output, 40)]
  [FlowNode.Pin(50, "再読み込み完了", FlowNode.PinTypes.Output, 50)]
  [FlowNode.Pin(100, "アイテムが選択された", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(200, "塔が選択された", FlowNode.PinTypes.Output, 200)]
  public class QuestChapterList : MonoBehaviour, IFlowInterface
  {
    public ListItemEvents ItemTemplate;
    public GameObject ItemContainer;
    public string WorldMapControllerID;
    public bool Descending = true;
    public bool RefreshOnStart = true;
    public GameObject BackButton;
    public Vector2 DefaultScrollPos = new Vector2(0.0f, 1f);
    private List<ListItemEvents> mItems = new List<ListItemEvents>();

    private WorldMapController mWorldMap
    {
      get
      {
        GameObject gameObject = GameObjectID.FindGameObject(this.WorldMapControllerID);
        return Object.op_Inequality((Object) gameObject, (Object) null) ? gameObject.GetComponent<WorldMapController>() : (WorldMapController) null;
      }
    }

    private void Start()
    {
      if (Object.op_Inequality((Object) this.ItemTemplate, (Object) null))
        ((Component) this.ItemTemplate).gameObject.SetActive(false);
      if (!this.RefreshOnStart)
        return;
      this.Refresh();
    }

    public void ResetScroll()
    {
      if (!Object.op_Inequality((Object) this.ItemContainer, (Object) null))
        return;
      ScrollRect[] componentsInParent = this.ItemContainer.GetComponentsInParent<ScrollRect>(true);
      if (componentsInParent.Length <= 0)
        return;
      componentsInParent[0].normalizedPosition = this.DefaultScrollPos;
    }

    private void OnBackClick()
    {
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 0:
          this.Refresh();
          break;
        case 1:
          WorldMapController instance = WorldMapController.FindInstance(this.WorldMapControllerID);
          if (!Object.op_Inequality((Object) instance, (Object) null))
            break;
          instance.GotoArea((string) null);
          break;
        case 3:
          GlobalVars.SelectedChapter.Set((string) null);
          this.Refresh();
          break;
        case 4:
          ChapterParam area = MonoSingleton<GameManager>.Instance.FindArea((string) GlobalVars.SelectedChapter);
          if (area != null)
          {
            if (area.parent != null)
              GlobalVars.SelectedChapter.Set(area.parent.iname);
            else
              GlobalVars.SelectedChapter.Set((string) null);
            this.Refresh();
            break;
          }
          this.ResetScroll();
          if (!string.IsNullOrEmpty((string) GlobalVars.SelectedSection) && this.IsSectionHidden((string) GlobalVars.SelectedSection))
          {
            GlobalVars.SelectedChapter.Set((string) null);
            this.Refresh();
            break;
          }
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 40);
          break;
      }
    }

    private bool IsSectionHidden(string iname)
    {
      SectionParam[] sections = MonoSingleton<GameManager>.Instance.Sections;
      for (int index = 0; index < sections.Length; ++index)
      {
        if (sections[index].iname == (string) GlobalVars.SelectedSection)
          return sections[index].hidden;
      }
      return false;
    }

    private bool ChapterContainsPlayableQuest(
      ChapterParam chapter,
      ChapterParam[] allChapters,
      QuestParam[] availableQuests,
      long currentTime)
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

    private bool IsChapterChildOf(ChapterParam child, ChapterParam parent)
    {
      for (; child != null; child = child.parent)
      {
        if (child == parent)
          return true;
      }
      return false;
    }

    private void Refresh()
    {
      GameUtility.DestroyGameObjects<ListItemEvents>(this.mItems);
      if (Object.op_Equality((Object) this.ItemTemplate, (Object) null) || Object.op_Equality((Object) this.ItemContainer, (Object) null))
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      ChapterParam[] chapters = instance.Chapters;
      List<ChapterParam> chapterParamList = new List<ChapterParam>((IEnumerable<ChapterParam>) chapters);
      QuestParam[] availableQuests = instance.Player.AvailableQuests;
      long serverTime = Network.GetServerTime();
      ChapterParam chapterParam = (ChapterParam) null;
      for (int index = chapterParamList.Count - 1; index >= 0; --index)
      {
        if ((string) GlobalVars.SelectedSection != chapterParamList[index].section)
          chapterParamList.RemoveAt(index);
      }
      if (!string.IsNullOrEmpty((string) GlobalVars.SelectedChapter))
      {
        chapterParam = instance.FindArea((string) GlobalVars.SelectedChapter);
        for (int index = chapterParamList.Count - 1; index >= 0; --index)
        {
          if (chapterParamList[index].parent == null || chapterParamList[index].parent.iname != (string) GlobalVars.SelectedChapter)
            chapterParamList.RemoveAt(index);
        }
      }
      else
      {
        for (int index = chapterParamList.Count - 1; index >= 0; --index)
        {
          if (chapterParamList[index].parent != null)
            chapterParamList.RemoveAt(index);
        }
      }
      for (int index = chapterParamList.Count - 1; index >= 0; --index)
      {
        if (!this.ChapterContainsPlayableQuest(chapterParamList[index], chapters, availableQuests, serverTime))
          chapterParamList.RemoveAt(index);
      }
      List<TowerParam> towerParamList = new List<TowerParam>();
      foreach (TowerParam tower in instance.Towers)
      {
        bool flag = false;
        for (int index = 0; index < availableQuests.Length; ++index)
        {
          if (availableQuests[index].type == QuestTypes.Tower && availableQuests[index].IsDateUnlock(serverTime))
          {
            flag = true;
            break;
          }
        }
        if (flag && (string.IsNullOrEmpty((string) GlobalVars.SelectedSection) || "WD_DAILY" == (string) GlobalVars.SelectedSection))
          towerParamList.Add(tower);
      }
      if (this.Descending)
        chapterParamList.Reverse();
      for (int index = 0; index < towerParamList.Count; ++index)
      {
        TowerParam data = towerParamList[index];
        ListItemEvents listItemEvents1 = (ListItemEvents) null;
        if (!string.IsNullOrEmpty(data.prefabPath))
        {
          StringBuilder stringBuilder = GameUtility.GetStringBuilder();
          stringBuilder.Append("QuestChapters/");
          stringBuilder.Append(data.prefabPath);
          listItemEvents1 = AssetManager.Load<ListItemEvents>(stringBuilder.ToString());
        }
        if (Object.op_Equality((Object) listItemEvents1, (Object) null))
          listItemEvents1 = this.ItemTemplate;
        QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(data.iname);
        ListItemEvents listItemEvents2 = Object.Instantiate<ListItemEvents>(listItemEvents1);
        DataSource.Bind<TowerParam>(((Component) listItemEvents2).gameObject, data);
        DataSource.Bind<QuestParam>(((Component) listItemEvents2).gameObject, quest);
        ((Component) listItemEvents2).transform.SetParent(this.ItemContainer.transform, false);
        ((Component) listItemEvents2).gameObject.SetActive(true);
        listItemEvents2.OnSelect = new ListItemEvents.ListItemEvent(this.OnTowerSelect);
        this.mItems.Add(listItemEvents2);
      }
      for (int index = 0; index < chapterParamList.Count; ++index)
      {
        ChapterParam data = chapterParamList[index];
        ListItemEvents listItemEvents3 = (ListItemEvents) null;
        if (!string.IsNullOrEmpty(data.prefabPath))
        {
          StringBuilder stringBuilder = GameUtility.GetStringBuilder();
          stringBuilder.Append("QuestChapters/");
          stringBuilder.Append(data.prefabPath);
          listItemEvents3 = AssetManager.Load<ListItemEvents>(stringBuilder.ToString());
        }
        if (Object.op_Equality((Object) listItemEvents3, (Object) null))
          listItemEvents3 = this.ItemTemplate;
        ListItemEvents listItemEvents4 = Object.Instantiate<ListItemEvents>(listItemEvents3);
        DataSource.Bind<ChapterParam>(((Component) listItemEvents4).gameObject, data);
        ((Component) listItemEvents4).transform.SetParent(this.ItemContainer.transform, false);
        ((Component) listItemEvents4).gameObject.SetActive(true);
        listItemEvents4.OnSelect = new ListItemEvents.ListItemEvent(this.OnNodeSelect);
        this.mItems.Add(listItemEvents4);
      }
      if (Object.op_Inequality((Object) this.BackButton, (Object) null))
      {
        if (chapterParam != null)
          this.BackButton.SetActive(true);
        else if (!string.IsNullOrEmpty((string) GlobalVars.SelectedSection))
          this.BackButton.SetActive(!this.IsSectionHidden((string) GlobalVars.SelectedSection));
      }
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 50);
    }

    private void OnNodeSelect(GameObject go)
    {
      ChapterParam dataOfClass = DataSource.FindDataOfClass<ChapterParam>(go, (ChapterParam) null);
      foreach (ChapterParam chapter in MonoSingleton<GameManager>.Instance.Chapters)
      {
        if (chapter.parent == dataOfClass)
        {
          GlobalVars.SelectedChapter.Set(dataOfClass.iname);
          this.Refresh();
          return;
        }
      }
      this.OnItemSelect(go);
    }

    private void OnItemSelect(GameObject go)
    {
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
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.QUEST_OUT_OF_DATE"), (UIUtility.DialogResultEvent) null);
      }
      else
      {
        GlobalVars.SelectedChapter.Set(dataOfClass.iname);
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      }
    }

    private void OnTowerSelect(GameObject go)
    {
      TowerParam dataOfClass = DataSource.FindDataOfClass<TowerParam>(go, (TowerParam) null);
      if (dataOfClass == null)
        return;
      QuestParam[] availableQuests = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
      long serverTime = Network.GetServerTime();
      int num1 = 0;
      int num2 = 0;
      for (int index = 0; index < availableQuests.Length; ++index)
      {
        if (availableQuests[index].type == QuestTypes.Tower && !(availableQuests[index].ChapterID != dataOfClass.iname) && !availableQuests[index].IsMulti)
        {
          ++num1;
          if (availableQuests[index].IsJigen && !availableQuests[index].IsDateUnlock(serverTime))
            ++num2;
        }
      }
      if (num1 > 0 && num1 == num2)
      {
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.QUEST_OUT_OF_DATE"), (UIUtility.DialogResultEvent) null);
      }
      else
      {
        GlobalVars.SelectedTowerID = dataOfClass.iname;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 200);
      }
    }
  }
}
