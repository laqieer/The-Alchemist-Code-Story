// Decompiled with JetBrains decompiler
// Type: SRPG.ScenarioReplayNextWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class ScenarioReplayNextWindow : MonoBehaviour
  {
    private void Start()
    {
      if (string.IsNullOrEmpty((string) GlobalVars.ReplaySelectedNextQuestID))
        return;
      DataSource.Bind<QuestParam>(((Component) this).gameObject, MonoSingleton<GameManager>.Instance.FindQuest((string) GlobalVars.ReplaySelectedNextQuestID));
      GameParameter.UpdateAll(((Component) this).gameObject);
    }
  }
}
