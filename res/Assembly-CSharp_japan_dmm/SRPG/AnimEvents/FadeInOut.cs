// Decompiled with JetBrains decompiler
// Type: SRPG.AnimEvents.FadeInOut
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG.AnimEvents
{
  public class FadeInOut : AnimEvent
  {
    public bool IsFadeOut;
    public bool IsAdditive;
    public Color FadeColor = new Color(0.0f, 0.0f, 0.0f, 1f);

    public override void OnStart(GameObject go)
    {
      if (this.IsFadeOut)
        this.FadeColor = Color.clear;
      FadeController.Instance.FadeTo(this.FadeColor, this.End - this.Start, !this.IsAdditive ? 2 : 1);
    }
  }
}
