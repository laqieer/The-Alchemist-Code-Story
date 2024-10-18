// Decompiled with JetBrains decompiler
// Type: SRPG.QuestChapterList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(100, "アイテムが選択された", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(2, "ロケーションのハイライトを戻す", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(200, "塔が選択された", FlowNode.PinTypes.Output, 200)]
  [FlowNode.Pin(50, "再読み込み完了", FlowNode.PinTypes.Output, 50)]
  [FlowNode.Pin(40, "セクション選択に戻す", FlowNode.PinTypes.Output, 40)]
  [FlowNode.Pin(401, "On Hard", FlowNode.PinTypes.Input, 4)]
  [FlowNode.Pin(400, "On Normal", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(4, "階層を上げる", FlowNode.PinTypes.Input, 4)]
  [FlowNode.Pin(3, "セクション決定", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(0, "再読み込み", FlowNode.PinTypes.Input, 40)]
  [FlowNode.Pin(1, "ワールドマップへ戻す", FlowNode.PinTypes.Input, 0)]
  public class QuestChapterList : MonoBehaviour, IFlowInterface
  {
    public bool Descending = true;
    public bool RefreshOnStart = true;
    public Vector2 DefaultScrollPos = new Vector2(0.0f, 1f);
    private List<ListItemEvents> mItems = new List<ListItemEvents>();
    public ListItemEvents ItemTemplate;
    public GameObject ItemContainer;
    public string WorldMapControllerID;
    public GameObject BackButton;
    [SerializeField]
    private GameObject BtnNormal;
    [SerializeField]
    private GameObject BtnElite;

    private WorldMapController mWorldMap
    {
      get
      {
        GameObject gameObject = GameObjectID.FindGameObject(this.WorldMapControllerID);
        if ((UnityEngine.Object) gameObject != (UnityEngine.Object) null)
          return gameObject.GetComponent<WorldMapController>();
        return (WorldMapController) null;
      }
    }

    private void Start()
    {
      if ((UnityEngine.Object) this.ItemTemplate != (UnityEngine.Object) null)
        this.ItemTemplate.gameObject.SetActive(false);
      if (!this.RefreshOnStart)
        return;
      this.Refresh();
    }

    private void ResetScroll()
    {
      if (!((UnityEngine.Object) this.ItemContainer != (UnityEngine.Object) null))
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
          if (!((UnityEngine.Object) instance != (UnityEngine.Object) null))
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

    private bool ChapterContainsPlayableQuest(ChapterParam chapter, ChapterParam[] allChapters, QuestParam[] availableQuests, long currentTime)
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
      if ((UnityEngine.Object) this.ItemTemplate == (UnityEngine.Object) null || (UnityEngine.Object) this.ItemContainer == (UnityEngine.Object) null)
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
        ListItemEvents original = (ListItemEvents) null;
        if (!string.IsNullOrEmpty(data.prefabPath))
        {
          StringBuilder stringBuilder = GameUtility.GetStringBuilder();
          stringBuilder.Append("QuestChapters/");
          stringBuilder.Append(data.prefabPath);
          original = AssetManager.Load<ListItemEvents>(stringBuilder.ToString());
        }
        if ((UnityEngine.Object) original == (UnityEngine.Object) null)
          original = this.ItemTemplate;
        QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(data.iname);
        ListItemEvents listItemEvents = UnityEngine.Object.Instantiate<ListItemEvents>(original);
        DataSource.Bind<TowerParam>(listItemEvents.gameObject, data);
        DataSource.Bind<QuestParam>(listItemEvents.gameObject, quest);
        listItemEvents.transform.SetParent(this.ItemContainer.transform, false);
        listItemEvents.gameObject.SetActive(true);
        listItemEvents.OnSelect = new ListItemEvents.ListItemEvent(this.OnTowerSelect);
        this.mItems.Add(listItemEvents);
      }
      int num = 0;
      for (int index1 = 0; index1 < chapterParamList.Count; ++index1)
      {
        ChapterParam data = chapterParamList[index1];
        ListItemEvents original = (ListItemEvents) null;
        if (!string.IsNullOrEmpty(data.prefabPath))
        {
          StringBuilder stringBuilder = GameUtility.GetStringBuilder();
          stringBuilder.Append("QuestChapters/");
          stringBuilder.Append(data.prefabPath);
          original = AssetManager.Load<ListItemEvents>(stringBuilder.ToString());
        }
        if ((UnityEngine.Object) original == (UnityEngine.Object) null)
          original = this.ItemTemplate;
        ListItemEvents listItemEvents = UnityEngine.Object.Instantiate<ListItemEvents>(original);
        DataSource.Bind<ChapterParam>(listItemEvents.gameObject, data);
        int total = 0;
        int completed = 0;
        foreach (QuestParam availableQuest in MonoSingleton<GameManager>.Instance.Player.AvailableQuests)
        {
          if (!(availableQuest.ChapterID != data.iname) && availableQuest.bonusObjective != null)
          {
            if (availableQuest.difficulty == QuestDifficulties.Elite)
              ++num;
            if (availableQuest.difficulty == GlobalVars.QuestDifficulty)
            {
              total += availableQuest.bonusObjective.Length;
              for (int index2 = 0; index2 < availableQuest.bonusObjective.Length; ++index2)
              {
                if ((availableQuest.clear_missions & 1 << index2) != 0)
                  ++completed;
              }
            }
          }
        }
        SGChapterItem component = listItemEvents.GetComponent<SGChapterItem>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null)
          component.SetProgress(total, completed);
        listItemEvents.transform.SetParent(this.ItemContainer.transform, false);
        listItemEvents.gameObject.SetActive(true);
        listItemEvents.OnSelect = new ListItemEvents.ListItemEvent(this.OnNodeSelect);
        this.mItems.Add(listItemEvents);
      }
      if ((UnityEngine.Object) this.BackButton != (UnityEngine.Object) null)
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
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.QUEST_OUT_OF_DATE"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
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
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.QUEST_OUT_OF_DATE"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      }
      else
      {
        GlobalVars.SelectedTowerID = dataOfClass.iname;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 200);
      }
    }
  }
}
