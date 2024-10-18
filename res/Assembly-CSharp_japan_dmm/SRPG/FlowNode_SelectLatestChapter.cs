// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SelectLatestChapter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("SRPG/クエスト選択", 32741)]
  [FlowNode.Pin(0, "In", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "Out", FlowNode.PinTypes.Output, 100)]
  public class FlowNode_SelectLatestChapter : FlowNode
  {
    public FlowNode_SelectLatestChapter.SelectModes Selection;
    private const int DEFSTARTSECTION = 1;

    public static void SelectLatestChapter()
    {
      List<int> intList = new List<int>(MonoSingleton<GameManager>.Instance.Sections.Length);
      for (int i = 0; i < MonoSingleton<GameManager>.Instance.Sections.Length; ++i)
      {
        if (MonoSingleton<GameManager>.Instance.Sections[i].storyPart > 0 && intList.Find((Predicate<int>) (p => p == MonoSingleton<GameManager>.Instance.Sections[i].storyPart)) == 0)
          intList.Add(MonoSingleton<GameManager>.Instance.Sections[i].storyPart);
      }
      intList.Sort();
      int num = 0;
      string iname = PlayerPrefsUtility.GetString(PlayerPrefsUtility.LAST_SELECTED_STORY_QUEST_ID, string.Empty);
      if (!string.IsNullOrEmpty(iname))
      {
        QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(iname);
        if (quest != null && quest.Chapter != null && quest.Chapter.sectionParam != null && quest.Chapter.sectionParam.storyPart > 0)
          num = quest.Chapter.sectionParam.storyPart;
      }
      int storyPart = num > 0 ? num : 1;
      QuestParam questParam1 = FlowNode_SelectLatestChapter.GetLastChapterQuest(storyPart);
      QuestParam questParam2 = questParam1;
      if (questParam1 != null && questParam1.state == QuestStates.Cleared)
      {
        for (int index = 0; index < intList.Count; ++index)
        {
          if (intList[index] != storyPart && intList[index] > 0)
          {
            questParam1 = FlowNode_SelectLatestChapter.GetLastChapterQuest(intList[index]);
            if (questParam1 != null && questParam1.state != QuestStates.Cleared)
              break;
          }
        }
        if (questParam1 != null && questParam1.state == QuestStates.Cleared)
          questParam1 = questParam2;
      }
      if (questParam1 == null)
        return;
      string chapterId = questParam1.ChapterID;
      ChapterParam[] chapters = MonoSingleton<GameManager>.Instance.Chapters;
      for (int index = 0; index < chapters.Length; ++index)
      {
        if (chapters[index].iname == chapterId)
        {
          GlobalVars.SelectedSection.Set(chapters[index].section);
          GlobalVars.SelectedChapter.Set(chapterId);
          if (chapters[index].sectionParam != null)
            GlobalVars.SelectedStoryPart.Set(chapters[index].sectionParam.storyPart);
          GlobalVars.SelectedQuestID = (string) null;
          break;
        }
      }
    }

    public static QuestParam GetLastChapterQuest(int storyPart)
    {
      QuestParam[] quests = MonoSingleton<GameManager>.Instance.Quests;
      QuestParam lastChapterQuest = (QuestParam) null;
      for (int index = 0; index < quests.Length; ++index)
      {
        if (quests[index].IsStory && (storyPart <= 0 || quests[index].Chapter == null || quests[index].Chapter.sectionParam == null || storyPart == quests[index].Chapter.sectionParam.storyPart))
        {
          lastChapterQuest = quests[index];
          if (quests[index].state != QuestStates.Cleared)
            break;
        }
      }
      return lastChapterQuest;
    }

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      switch (this.Selection)
      {
        case FlowNode_SelectLatestChapter.SelectModes.Latest:
          FlowNode_SelectLatestChapter.SelectLatestChapter();
          break;
        case FlowNode_SelectLatestChapter.SelectModes.DailyChapter:
          DateTime dateTime = TimeManager.FromUnixTime(Network.GetServerTime());
          GlobalVars.SelectedSection.Set("WD_DAILY");
          switch (dateTime.DayOfWeek)
          {
            case DayOfWeek.Sunday:
              GlobalVars.SelectedChapter.Set("AR_SUN");
              break;
            case DayOfWeek.Monday:
              GlobalVars.SelectedChapter.Set("AR_MON");
              break;
            case DayOfWeek.Tuesday:
              GlobalVars.SelectedChapter.Set("AR_TUE");
              break;
            case DayOfWeek.Wednesday:
              GlobalVars.SelectedChapter.Set("AR_WED");
              break;
            case DayOfWeek.Thursday:
              GlobalVars.SelectedChapter.Set("AR_THU");
              break;
            case DayOfWeek.Friday:
              GlobalVars.SelectedChapter.Set("AR_FRI");
              break;
            case DayOfWeek.Saturday:
              GlobalVars.SelectedChapter.Set("AR_SAT");
              break;
          }
          break;
        case FlowNode_SelectLatestChapter.SelectModes.DailySection:
          switch (GlobalVars.ReqEventPageListType)
          {
            case GlobalVars.EventQuestListType.Seiseki:
              GlobalVars.SelectedSection.Set("WD_SEISEKI");
              break;
            case GlobalVars.EventQuestListType.Babel:
              GlobalVars.SelectedSection.Set("WD_BABEL");
              break;
            default:
              GlobalVars.SelectedSection.Set("WD_DAILY");
              break;
          }
          GlobalVars.SelectedChapter.Set(string.Empty);
          break;
        case FlowNode_SelectLatestChapter.SelectModes.CharacterQuestSection:
          GlobalVars.SelectedSection.Set("WD_CHARA");
          GlobalVars.SelectedChapter.Set(string.Empty);
          break;
        default:
          return;
      }
      this.ActivateOutputLinks(100);
    }

    public enum SelectModes
    {
      Latest,
      DailyChapter,
      DailySection,
      CharacterQuestSection,
    }
  }
}
