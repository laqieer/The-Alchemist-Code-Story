// Decompiled with JetBrains decompiler
// Type: SRPG.VersusDraftMapWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;

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
      DataSource.Bind<QuestParam>(this.gameObject, quest, false);
    }
  }
}
