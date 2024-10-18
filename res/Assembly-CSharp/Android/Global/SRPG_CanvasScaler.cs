// Decompiled with JetBrains decompiler
// Type: SRPG_CanvasScaler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[AddComponentMenu("Layout/Canvas Scaler (SRPG)")]
public class SRPG_CanvasScaler : CanvasScaler
{
  public const float MinScreenWidth = 1200f;
  public const float MinScreenHeight = 750f;
  public static bool UseKuroObi;

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
