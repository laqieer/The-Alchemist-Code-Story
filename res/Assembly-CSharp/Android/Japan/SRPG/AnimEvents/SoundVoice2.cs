// Decompiled with JetBrains decompiler
// Type: SRPG.AnimEvents.SoundVoice2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG.AnimEvents
{
  public class SoundVoice2 : AnimEvent
  {
    public UnitVoice.ECharType CharType;
    public UnitVoice.eCollaboType CollaboType;
    public string DirectCharName;
    public string CueName;

    public override void OnStart(GameObject go)
    {
      if ((UnityEngine.Object) go == (UnityEngine.Object) null)
        return;
      UnitVoice unitVoice = go.GetComponent<UnitVoice>();
      if ((UnityEngine.Object) unitVoice == (UnityEngine.Object) null)
        unitVoice = go.AddComponent<UnitVoice>();
      unitVoice.CharType = this.CharType;
      unitVoice.CollaboType = this.CollaboType;
      unitVoice.DirectCharName = this.DirectCharName;
      unitVoice.CueName = this.CueName;
      unitVoice.PlayOnAwake = false;
      string directName = (string) null;
      string sheetName = (string) null;
      string cueName = (string) null;
      unitVoice.GetDefaultCharName(ref directName, ref sheetName, ref cueName);
      unitVoice.SetCharName(directName, sheetName, cueName);
      unitVoice.SetupCueName();
      unitVoice.Play();
    }

    public override void OnEnd(GameObject go)
    {
      base.OnEnd(go);
      UnitVoice component = go.GetComponent<UnitVoice>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      UnityEngine.Object.Destroy((UnityEngine.Object) component);
    }
  }
}
