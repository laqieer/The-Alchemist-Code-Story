// Decompiled with JetBrains decompiler
// Type: SRPG.ToggledPulldownItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class ToggledPulldownItem : PulldownItem
  {
    public GameObject imageOn;
    public GameObject imageOff;

    public override void OnStatusChanged(bool enabled)
    {
      if ((UnityEngine.Object) this.imageOn != (UnityEngine.Object) null)
        this.imageOn.SetActive(enabled);
      if (!((UnityEngine.Object) this.imageOff != (UnityEngine.Object) null))
        return;
      this.imageOff.SetActive(!enabled);
    }
  }
}
