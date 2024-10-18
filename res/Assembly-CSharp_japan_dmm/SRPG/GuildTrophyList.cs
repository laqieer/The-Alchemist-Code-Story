// Decompiled with JetBrains decompiler
// Type: SRPG.GuildTrophyList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class GuildTrophyList : TrophyList
  {
    public override List<TrophyState> TrophyDailyData
    {
      get
      {
        return Object.op_Equality((Object) this.Trophy_Window, (Object) null) ? (List<TrophyState>) null : this.Trophy_Window.TrophyGuildDailyData;
      }
    }

    public override List<TrophyState> TrophyEndedData
    {
      get
      {
        return Object.op_Equality((Object) this.Trophy_Window, (Object) null) ? (List<TrophyState>) null : this.Trophy_Window.TrophyGuildEndedData;
      }
    }

    public override void OnItemSelect(GameObject go)
    {
      TrophyParam dataOfClass = DataSource.FindDataOfClass<TrophyParam>(go, (TrophyParam) null);
      if (dataOfClass == null)
        return;
      TrophyState trophyCounter = MonoSingleton<GameManager>.Instance.Player.GuildTrophyData.GetTrophyCounter(dataOfClass, true);
      if (!trophyCounter.IsEnded && trophyCounter.IsCompleted)
        this.GotoReward(dataOfClass);
      else
        this.SelectGotoShortCut(dataOfClass);
    }

    protected override void SelectGotoShortCut(TrophyParam param)
    {
      if (param == null || !(param is GuildTrophyParam guildTrophyParam) || guildTrophyParam.Objectives == null || guildTrophyParam.Objectives.Length <= 0 || !(guildTrophyParam.Objectives[0] is GuildTrophyObjective objective))
        return;
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      switch (objective.type)
      {
        case GuildTrophyConditionTypes.winquest:
          QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(objective.sval_base);
          string chapterId = quest == null ? (string) null : quest.ChapterID;
          ArchiveParam archiveByArea = MonoSingleton<GameManager>.Instance.FindArchiveByArea(chapterId);
          if (archiveByArea != null && archiveByArea.IsAvailable() && MonoSingleton<GameManager>.Instance.Player.CheckUnlock(UnlockTargets.Archive))
          {
            switch (quest.type)
            {
              case QuestTypes.Multi:
                if (LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.MultiPlay))
                  return;
                if (MonoSingleton<GameManager>.Instance.Player.IsQuestArchiveOpenByArea(chapterId))
                {
                  this.Trophy_Window.ActivateOutputLinks(2005);
                  return;
                }
                UIUtility.ConfirmBox(LocalizedText.Get("sys.ARCHIVE_CHANGE_SCENE_CONFIRM"), (UIUtility.DialogResultEvent) (yes_button => this.GotoEventQuestArchive()), (UIUtility.DialogResultEvent) null);
                return;
              case QuestTypes.Event:
                if (LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.EventQuest))
                  return;
                if (MonoSingleton<GameManager>.Instance.Player.IsQuestArchiveOpenByArea(chapterId))
                {
                  GlobalVars.SelectedQuestID = quest.iname;
                  GlobalVars.SelectedChapter.Set(chapterId);
                  GlobalVars.SelectedSection.Set("WD_DAILY");
                  GlobalVars.ReqEventPageListType = GlobalVars.EventQuestListType.EventQuestArchive;
                  FlowNode_Variable.Set("SHOW_CHAPTER", "0");
                  this.Trophy_Window.ActivateOutputLinks(2006);
                  return;
                }
                UIUtility.ConfirmBox(LocalizedText.Get("sys.ARCHIVE_CHANGE_SCENE_CONFIRM"), (UIUtility.DialogResultEvent) (yes_button => this.GotoEventQuestArchive()), (UIUtility.DialogResultEvent) null);
                return;
              default:
                return;
            }
          }
          else
          {
            QuestTypes quest_type = QuestTypes.Story;
            if (!QuestParam.TransSectionGotoQuest(objective.sval_base, out quest_type, new UIUtility.DialogResultEvent(((TrophyList) this).MsgBoxJumpToQuest)))
              break;
            switch (quest_type)
            {
              case QuestTypes.Event:
              case QuestTypes.Gps:
                if (LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.EventQuest))
                  return;
                this.Trophy_Window.ActivateOutputLinks(2006);
                return;
              case QuestTypes.Character:
                if (LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.CharacterQuest))
                  return;
                this.Trophy_Window.ActivateOutputLinks(2032);
                return;
              case QuestTypes.Tower:
                this.Trophy_Window.ActivateOutputLinks(2026);
                return;
              case QuestTypes.Beginner:
                this.Trophy_Window.ActivateOutputLinks(2033);
                return;
              case QuestTypes.MultiGps:
                if (LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.MultiPlay))
                  return;
                this.Trophy_Window.ActivateOutputLinks(2005);
                return;
              case QuestTypes.GenesisStory:
              case QuestTypes.GenesisBoss:
                if (LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.EventQuest))
                  return;
                this.Trophy_Window.ActivateOutputLinks(2040);
                return;
              case QuestTypes.AdvanceStory:
              case QuestTypes.AdvanceBoss:
                if (LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.EventQuest))
                  return;
                this.Trophy_Window.ActivateOutputLinks(2041);
                return;
              default:
                if (quest_type != QuestTypes.Multi)
                {
                  this.Trophy_Window.ActivateOutputLinks(2002);
                  return;
                }
                goto case QuestTypes.MultiGps;
            }
          }
        case GuildTrophyConditionTypes.winquest_by_area:
          if (objective.sval == null)
            break;
          for (int index = 0; index < objective.SvalCount; ++index)
          {
            ChapterParam area = MonoSingleton<GameManager>.Instance.FindArea(objective.sval[index]);
            if (area != null && area.quests != null && this.GotoQuests(area.quests.ToArray()))
              break;
          }
          break;
        case GuildTrophyConditionTypes.winquest_by_mode:
          this.GotoQuestMode(QuestParam.GetQuestDifficulties(objective.sval_base));
          break;
        case GuildTrophyConditionTypes.winevent:
          if (LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.EventQuest))
            break;
          QuestParam.GotoEventListQuest((string) null);
          this.Trophy_Window.ActivateOutputLinks(2006);
          break;
        case GuildTrophyConditionTypes.playcolo:
          this.GotoArena();
          break;
        case GuildTrophyConditionTypes.playmulti:
          if (LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.MultiPlay))
            break;
          this.Trophy_Window.ActivateOutputLinks(2005);
          break;
        case GuildTrophyConditionTypes.guild_attend:
        case GuildTrophyConditionTypes.gvg_btl_exec:
        case GuildTrophyConditionTypes.guildraid_attack:
          if (LevelLock.ShowLockMessage(player.Lv, player.VipRank, UnlockTargets.Guild))
            break;
          this.Trophy_Window.ActivateOutputLinks(2042);
          break;
      }
    }
  }
}
