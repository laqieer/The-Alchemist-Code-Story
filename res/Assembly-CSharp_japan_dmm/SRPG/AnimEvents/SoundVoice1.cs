// Decompiled with JetBrains decompiler
// Type: SRPG.AnimEvents.SoundVoice1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG.AnimEvents
{
  public class SoundVoice1 : AnimEvent
  {
    public string SheetName;
    public string CueID;

    public override void OnStart(GameObject go)
    {
      if (Object.op_Equality((Object) go, (Object) null) || string.IsNullOrEmpty(this.SheetName) || string.IsNullOrEmpty(this.CueID))
        return;
      CustomSound3 customSound3 = go.GetComponent<CustomSound3>();
      if (Object.op_Equality((Object) customSound3, (Object) null))
        customSound3 = go.AddComponent<CustomSound3>();
      customSound3.sheetName = this.SheetName;
      customSound3.cueID = this.CueID;
      customSound3.PlayFunction = CustomSound3.EPlayFunction.VoicePlay;
      customSound3.CueSheetHandlePlayCategory = MySound.EType.VOICE;
      customSound3.CueSheetHandlePlayLoopType = MySound.CueSheetHandle.ELoopFlag.DEFAULT;
      customSound3.StopOnPlay = false;
      customSound3.StopOnDisable = false;
      customSound3.StopSec = 0.0f;
      customSound3.DelayPlaySec = 0.0f;
      customSound3.PlayOnEnable = true;
      customSound3.Play();
    }

    public override void OnEnd(GameObject go)
    {
      base.OnEnd(go);
      CustomSound3 component = go.GetComponent<CustomSound3>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      Object.Destroy((Object) component);
    }
  }
}
