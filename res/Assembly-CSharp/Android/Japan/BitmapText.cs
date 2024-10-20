﻿// Decompiled with JetBrains decompiler
// Type: BitmapText
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.UI;

public class BitmapText : Text
{
  public BitmapText.FontScale AutoSize;
  private bool mDelayUpdate;

  public override Material defaultMaterial
  {
    get
    {
      return Graphic.defaultGraphicMaterial;
    }
  }

  private void ResizeFont()
  {
    if (0 > this.AutoSize.CountMin || this.AutoSize.CountMin >= this.AutoSize.CountMax || (this.AutoSize.SizeMin <= 0 || this.AutoSize.SizeMax <= 0))
      return;
    this.fontSize = (int) Mathf.Lerp((float) this.AutoSize.SizeMin, (float) this.AutoSize.SizeMax, (float) (this.text.Length - this.AutoSize.CountMin) / (float) (this.AutoSize.CountMax - this.AutoSize.CountMin));
  }

  protected override void Start()
  {
    base.Start();
    this.ResizeFont();
  }

  private void Update()
  {
    if (!this.mDelayUpdate || !this.IsParentCanvasActive)
      return;
    this.mDelayUpdate = false;
    this.enabled = !this.enabled;
    this.enabled = !this.enabled;
  }

  protected override void OnTransformParentChanged()
  {
    base.OnTransformParentChanged();
  }

  public override string text
  {
    get
    {
      return base.text;
    }
    set
    {
      if (this.enabled)
      {
        if (!this.mDelayUpdate && !this.IsParentCanvasActive)
          this.mDelayUpdate = true;
        if (!this.mDelayUpdate)
        {
          this.enabled = false;
          this.enabled = true;
        }
      }
      base.text = value;
      this.ResizeFont();
    }
  }

  private bool IsParentCanvasActive
  {
    get
    {
      Transform parent1;
      for (CanvasGroup canvasGroup = this.GetComponentInParent<CanvasGroup>(); (Object) canvasGroup != (Object) null; canvasGroup = !((Object) parent1 != (Object) null) ? (CanvasGroup) null : parent1.GetComponentInParent<CanvasGroup>())
      {
        if ((double) canvasGroup.alpha <= 0.0)
          return false;
        parent1 = canvasGroup.transform.parent;
      }
      Transform parent2;
      for (Canvas canvas = this.GetComponentInParent<Canvas>(); (Object) canvas != (Object) null; canvas = !((Object) parent2 != (Object) null) ? (Canvas) null : parent2.GetComponentInParent<Canvas>())
      {
        if (!canvas.enabled)
          return false;
        parent2 = canvas.transform.parent;
      }
      return true;
    }
  }

  [Serializable]
  public struct FontScale
  {
    public int CountMin;
    public int SizeMin;
    public int CountMax;
    public int SizeMax;
  }
}