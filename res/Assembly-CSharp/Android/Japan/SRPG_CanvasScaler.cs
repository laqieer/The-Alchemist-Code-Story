// Decompiled with JetBrains decompiler
// Type: SRPG_CanvasScaler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Layout/Canvas Scaler (SRPG)")]
[ExecuteInEditMode]
public class SRPG_CanvasScaler : CanvasScaler
{
  public static bool UseKuroObi;
  public const float MinScreenWidth = 1200f;
  public const float MinScreenHeight = 750f;

  protected override void Awake()
  {
    base.Awake();
    this.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
    if (SRPG_CanvasScaler.UseKuroObi)
    {
      this.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
    }
    else
    {
      this.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
      this.matchWidthOrHeight = 1f;
    }
    this.referenceResolution = new Vector2(1200f, 750f);
  }
}
