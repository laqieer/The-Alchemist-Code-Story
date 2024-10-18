// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_AdvanceQuestOpenPopup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Advance/難易度、ボス解放ポップ", 32741)]
  [FlowNode.Pin(1, "In", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(11, "Out", FlowNode.PinTypes.Output, 11)]
  public class FlowNode_AdvanceQuestOpenPopup : FlowNode
  {
    private const int PIN_IN = 1;
    private const int PIN_OUT = 11;
    [SerializeField]
    private List<string> DifficultyTextKey;
    private bool mIsRunning;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1 || this.mIsRunning)
        return;
      this.StartCoroutine(this.PopupMessageOpen());
    }

    private List<QuestParam> GetOpenedDiffQuest()
    {
      List<QuestParam> openedDiffQuest = new List<QuestParam>();
      string last_played_quest = PlayerPrefsUtility.GetString(PlayerPrefsUtility.PREFS_KEY_ADVANCE_DIFF_OPEN_QUEST_CACHE, string.Empty);
      if (!string.IsNullOrEmpty(last_played_quest))
      {
        QuestParam[] quests = MonoSingleton<GameManager>.Instance.Quests;
        QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(last_played_quest);
        for (int index = 0; index < quests.Length; ++index)
        {
          if (!(quests[index].iname == last_played_quest) && (quests[index].IsAdvanceStory || quests[index].IsAdvanceBoss) && quests[index].cond_quests != null && !string.IsNullOrEmpty(Array.Find<string>(quests[index].cond_quests, (Predicate<string>) (cond_quest => cond_quest == last_played_quest))) && (quests[index].IsAdvanceBoss || quest == null || quest.IsAdvanceBoss || !(quest.ChapterID == quests[index].ChapterID) || quest.difficulty != quests[index].difficulty) && MonoSingleton<GameManager>.Instance.Player.IsQuestAvailable(quests[index].iname) && !openedDiffQuest.Contains(quests[index]))
            openedDiffQuest.Add(quests[index]);
        }
      }
      return openedDiffQuest;
    }

    private QuestParam GetOpenedBossQuest()
    {
      string iname = PlayerPrefsUtility.GetString(PlayerPrefsUtility.PREFS_KEY_ADVANCE_BOSS_OPEN_QUEST_CACHE, string.Empty);
      if (!string.IsNullOrEmpty(iname))
      {
        QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(iname);
        if (quest.type != QuestTypes.AdvanceStory)
          return (QuestParam) null;
        AdvanceEventParam eventParamFromAreaId = MonoSingleton<GameManager>.Instance.GetAdvanceEventParamFromAreaId(quest.ChapterID);
        if (eventParamFromAreaId != null && eventParamFromAreaId.IsBossLiberation(quest.difficulty))
          return eventParamFromAreaId.GetBossQuest(quest.difficulty);
      }
      return (QuestParam) null;
    }

    private QuestParam GetOpenedSkipQuest()
    {
      string iname = PlayerPrefsUtility.GetString(PlayerPrefsUtility.PREFS_KEY_ADVANCE_SKIP_OPEN_QUEST_CACHE, string.Empty);
      if (!string.IsNullOrEmpty(iname))
      {
        QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(iname);
        if (quest != null && quest.IsAdvanceBoss && quest.HasMission() && quest.IsMissionCompleteALL())
          return quest;
      }
      return (QuestParam) null;
    }

    [DebuggerHidden]
    private IEnumerator PopupMessageOpen()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_AdvanceQuestOpenPopup.\u003CPopupMessageOpen\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }
  }
}
