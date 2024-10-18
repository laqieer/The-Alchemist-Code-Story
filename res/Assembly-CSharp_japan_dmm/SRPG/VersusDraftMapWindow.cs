// Decompiled with JetBrains decompiler
// Type: SRPG.VersusDraftMapWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class VersusDraftMapWindow : MonoBehaviour
  {
    private void Start()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      QuestParam quest = instance.FindQuest(instance.VSDraftQuestId);
      if (quest == null)
        return;
      DataSource.Bind<QuestParam>(((Component) this).gameObject, quest);
    }
  }
}
