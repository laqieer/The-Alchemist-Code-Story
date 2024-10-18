// Decompiled with JetBrains decompiler
// Type: SRPG.ToggledPulldownItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class ToggledPulldownItem : PulldownItem
  {
    public GameObject imageOn;
    public GameObject imageOff;

    public override void OnStatusChanged(bool enabled)
    {
      if (Object.op_Inequality((Object) this.imageOn, (Object) null))
        this.imageOn.SetActive(enabled);
      if (!Object.op_Inequality((Object) this.imageOff, (Object) null))
        return;
      this.imageOff.SetActive(!enabled);
    }
  }
}
