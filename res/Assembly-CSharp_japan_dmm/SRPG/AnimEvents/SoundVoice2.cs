// Decompiled with JetBrains decompiler
// Type: SRPG.AnimEvents.SoundVoice2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
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
      if (Object.op_Equality((Object) go, (Object) null))
        return;
      UnitVoice unitVoice = go.GetComponent<UnitVoice>();
      if (Object.op_Equality((Object) unitVoice, (Object) null))
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
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      Object.Destroy((Object) component);
    }
  }
}
