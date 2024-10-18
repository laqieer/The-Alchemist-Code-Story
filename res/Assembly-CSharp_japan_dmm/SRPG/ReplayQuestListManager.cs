// Decompiled with JetBrains decompiler
// Type: SRPG.ReplayQuestListManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class ReplayQuestListManager : MonoBehaviour
  {
    private static ReplayQuestListManager mInstance;
    private List<string> mPlayableScenario;

    public static ReplayQuestListManager Instance => ReplayQuestListManager.mInstance;

    private void Awake() => ReplayQuestListManager.mInstance = this;

    public List<string> PlayableScenario
    {
      get
      {
        if (this.mPlayableScenario == null)
          this.mPlayableScenario = ReplayQuestListManager.GetPlayableReplayScenario();
        return this.mPlayableScenario;
      }
    }

    public static List<string> GetPlayableReplayScenario()
    {
      QuestParam[] availableQuests = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
      if (availableQuests == null || availableQuests.Length <= 0)
        return new List<string>();
      List<string> playableReplayScenario = new List<string>();
      long serverTime = Network.GetServerTime();
      foreach (QuestParam questParam in availableQuests)
      {
        if (!questParam.IsMulti && questParam.type != QuestTypes.Beginner && (questParam.state == QuestStates.Cleared || questParam.state == QuestStates.Challenged) && questParam.IsReplayDateUnlock(serverTime))
        {
          string eventStart = questParam.event_start;
          string eventClear = questParam.event_clear;
          if (!string.IsNullOrEmpty(eventStart) && (questParam.state == QuestStates.Cleared || questParam.state == QuestStates.Challenged) && !playableReplayScenario.Contains(eventStart))
            playableReplayScenario.Add(eventStart);
          if (!string.IsNullOrEmpty(eventClear) && questParam.state == QuestStates.Cleared && !playableReplayScenario.Contains(eventClear))
            playableReplayScenario.Add(eventClear);
        }
      }
      return playableReplayScenario;
    }

    private void OnDestroy() => ReplayQuestListManager.mInstance = (ReplayQuestListManager) null;
  }
}
