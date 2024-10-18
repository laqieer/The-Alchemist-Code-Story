// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SelectLatestChapter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;

namespace SRPG
{
  [FlowNode.NodeType("SRPG/クエスト選択", 32741)]
  [FlowNode.Pin(0, "In", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "Out", FlowNode.PinTypes.Output, 100)]
  public class FlowNode_SelectLatestChapter : FlowNode
  {
    public FlowNode_SelectLatestChapter.SelectModes Selection;

    public static void SelectLatestChapter()
    {
      QuestParam[] quests = MonoSingleton<GameManager>.Instance.Quests;
      QuestParam questParam = (QuestParam) null;
      int num = 0;
      string iname = PlayerPrefsUtility.GetString(PlayerPrefsUtility.LAST_SELECTED_STORY_QUEST_ID, string.Empty);
      if (!string.IsNullOrEmpty(iname))
      {
        QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(iname);
        if (quest != null && quest.Chapter != null && (quest.Chapter.sectionParam != null && quest.Chapter.sectionParam.storyPart > 0))
        {
          num = quest.Chapter.sectionParam.storyPart;
          if ((int) GlobalVars.SelectedStoryPart != num && (int) GlobalVars.SelectedStoryPart > 0)
            num = (int) GlobalVars.SelectedStoryPart;
        }
      }
      for (int index = 0; index < quests.Length; ++index)
      {
        if (quests[index].IsStory && (num <= 0 || quests[index].Chapter == null || (quests[index].Chapter.sectionParam == null || num == quests[index].Chapter.sectionParam.storyPart)))
        {
          questParam = quests[index];
          if (quests[index].state != QuestStates.Cleared)
            break;
        }
      }
      if (questParam == null)
        return;
      string chapterId = questParam.ChapterID;
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
