// Decompiled with JetBrains decompiler
// Type: SRPG.AnimEvents.FadeInOut
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG.AnimEvents
{
  public class FadeInOut : AnimEvent
  {
    public Color FadeColor = new Color(0.0f, 0.0f, 0.0f, 1f);
    public bool IsFadeOut;
    public bool IsAdditive;

    public override void OnStart(GameObject go)
    {
      if (this.IsFadeOut)
        this.FadeColor = Color.clear;
      FadeController.Instance.FadeTo(this.FadeColor, this.End - this.Start, !this.IsAdditive ? 2 : 1);
    }
  }
}
