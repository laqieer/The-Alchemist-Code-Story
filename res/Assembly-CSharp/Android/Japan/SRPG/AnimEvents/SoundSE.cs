// Decompiled with JetBrains decompiler
// Type: SRPG.AnimEvents.SoundSE
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG.AnimEvents
{
  public class SoundSE : AnimEvent
  {
    public CustomSound.EType SoundType;
    public string CueID;

    public override void OnStart(GameObject go)
    {
      if ((UnityEngine.Object) go == (UnityEngine.Object) null || string.IsNullOrEmpty(this.CueID))
        return;
      CustomSound customSound = go.GetComponent<CustomSound>();
      if ((UnityEngine.Object) customSound == (UnityEngine.Object) null)
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
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      UnityEngine.Object.Destroy((UnityEngine.Object) component);
    }
  }
}
