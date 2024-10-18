// Decompiled with JetBrains decompiler
// Type: SRPG.SRPG_ScrollRect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [AddComponentMenu("UI/ScrollRect (SRPG)")]
  public class SRPG_ScrollRect : ScrollRect
  {
    protected override void Awake()
    {
      base.Awake();
      if (!((UnityEngine.Object) this.verticalScrollbar != (UnityEngine.Object) null))
        return;
      this.verticalScrollbar.value = 1f;
    }
  }
}
