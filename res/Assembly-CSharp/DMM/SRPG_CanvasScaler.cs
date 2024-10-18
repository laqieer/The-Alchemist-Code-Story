// Decompiled with JetBrains decompiler
// Type: SRPG_CanvasScaler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#nullable disable
[AddComponentMenu("Layout/Canvas Scaler (SRPG)")]
[ExecuteInEditMode]
public class SRPG_CanvasScaler : CanvasScaler
{
  public static bool UseKuroObi = true;
  public const float MinScreenWidth = 1334f;
  public const float MinScreenHeight = 750f;

  protected virtual void Awake()
  {
    ((UIBehaviour) this).Awake();
    this.uiScaleMode = (CanvasScaler.ScaleMode) 1;
    if (SRPG_CanvasScaler.UseKuroObi)
    {
      this.screenMatchMode = (CanvasScaler.ScreenMatchMode) 1;
    }
    else
    {
      this.screenMatchMode = (CanvasScaler.ScreenMatchMode) 0;
      this.matchWidthOrHeight = 1f;
    }
    this.referenceResolution = new Vector2(1334f, 750f);
  }
}
