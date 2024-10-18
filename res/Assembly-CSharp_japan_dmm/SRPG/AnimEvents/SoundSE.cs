// Decompiled with JetBrains decompiler
// Type: SRPG.AnimEvents.SoundSE
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG.AnimEvents
{
  public class SoundSE : AnimEvent
  {
    public CustomSound.EType SoundType;
    public string CueID;

    public override void OnStart(GameObject go)
    {
      if (Object.op_Equality((Object) go, (Object) null) || string.IsNullOrEmpty(this.CueID))
        return;
      CustomSound customSound = go.GetComponent<CustomSound>();
      if (Object.op_Equality((Object) customSound, (Object) null))
        customSound = go.AddComponent<CustomSound>();
      customSound.type = this.SoundType;
      customSound.cueID = this.CueID;
      customSound.LoopFlag = false;
      customSound.StopSec = 0.0f;
      customSound.PlayOnAwake = false;
      customSound.Play();
    }

    public override void OnEnd(GameObject go)
    {
      base.OnEnd(go);
      CustomSound component = go.GetComponent<CustomSound>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      Object.Destroy((Object) component);
    }
  }
}
