// Decompiled with JetBrains decompiler
// Type: SRPG.AnimEvents.ChangeBGM
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG.AnimEvents
{
  public class ChangeBGM : AnimEvent
  {
    public string BgmId = string.Empty;

    public override void OnStart(GameObject go)
    {
      if (string.IsNullOrEmpty(this.BgmId))
        SceneBattle.Instance.PlayBGM();
      else
        MonoSingleton<MySound>.Instance.PlayBGM(this.BgmId);
    }
  }
}
