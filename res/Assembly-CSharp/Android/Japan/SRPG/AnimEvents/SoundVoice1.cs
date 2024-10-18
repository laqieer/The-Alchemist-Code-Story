// Decompiled with JetBrains decompiler
// Type: SRPG.AnimEvents.SoundVoice1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG.AnimEvents
{
  public class SoundVoice1 : AnimEvent
  {
    public string SheetName;
    public string CueID;

    public override void OnStart(GameObject go)
    {
      if ((UnityEngine.Object) go == (UnityEngine.Object) null || string.IsNullOrEmpty(this.SheetName) || string.IsNullOrEmpty(this.CueID))
        return;
      CustomSound3 customSound3 = go.GetComponent<CustomSound3>();
      if ((UnityEngine.Object) customSound3 == (UnityEngine.Object) null)
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
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      UnityEngine.Object.Destroy((UnityEngine.Object) component);
    }
  }
}
