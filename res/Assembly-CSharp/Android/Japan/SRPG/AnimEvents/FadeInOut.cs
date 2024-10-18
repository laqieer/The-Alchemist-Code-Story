// Decompiled with JetBrains decompiler
// Type: SRPG.AnimEvents.FadeInOut
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
