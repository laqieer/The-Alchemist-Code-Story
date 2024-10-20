﻿// Decompiled with JetBrains decompiler
// Type: SRPG.SyncColor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [RequireComponent(typeof (Graphic))]
  public class SyncColor : MonoBehaviour
  {
    private Graphic mGraphic;
    private Color mOriginColor;
    public CanvasRenderer Source;
    [BitMask]
    public SyncColor.ColorMasks ColorMask = SyncColor.ColorMasks.R | SyncColor.ColorMasks.G | SyncColor.ColorMasks.B | SyncColor.ColorMasks.A;
    public SyncColor.SyncType Type;

    private void Start()
    {
      this.mGraphic = ((Component) this).GetComponent<Graphic>();
      this.mOriginColor = this.mGraphic.color;
      this.Sync();
    }

    private void LateUpdate() => this.Sync();

    private void Sync()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.Source, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.mGraphic, (UnityEngine.Object) null))
        return;
      Color color1 = this.Source.GetColor();
      Color color2 = new Color();
      switch (this.Type)
      {
        case SyncColor.SyncType.Override:
          color2 = this.mGraphic.color;
          if ((this.ColorMask & SyncColor.ColorMasks.R) != (SyncColor.ColorMasks) 0)
            color2.r = color1.r;
          if ((this.ColorMask & SyncColor.ColorMasks.G) != (SyncColor.ColorMasks) 0)
            color2.g = color1.g;
          if ((this.ColorMask & SyncColor.ColorMasks.B) != (SyncColor.ColorMasks) 0)
            color2.b = color1.b;
          if ((this.ColorMask & SyncColor.ColorMasks.A) != (SyncColor.ColorMasks) 0)
          {
            color2.a = color1.a;
            break;
          }
          break;
        case SyncColor.SyncType.Multi:
          color2 = this.mOriginColor;
          if ((this.ColorMask & SyncColor.ColorMasks.R) != (SyncColor.ColorMasks) 0)
            color2.r *= color1.r;
          if ((this.ColorMask & SyncColor.ColorMasks.G) != (SyncColor.ColorMasks) 0)
            color2.g *= color1.g;
          if ((this.ColorMask & SyncColor.ColorMasks.B) != (SyncColor.ColorMasks) 0)
            color2.b *= color1.b;
          if ((this.ColorMask & SyncColor.ColorMasks.A) != (SyncColor.ColorMasks) 0)
          {
            color2.a *= color1.a;
            break;
          }
          break;
      }
      this.mGraphic.color = color2;
    }

    public void ForceOriginColorChange(Color color) => this.mOriginColor = color;

    public enum SyncType
    {
      Override,
      Multi,
    }

    [Flags]
    public enum ColorMasks
    {
      R = 1,
      G = 2,
      B = 4,
      A = 8,
    }
  }
}