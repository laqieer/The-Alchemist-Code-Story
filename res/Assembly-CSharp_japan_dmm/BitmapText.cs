// Decompiled with JetBrains decompiler
// Type: BitmapText
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#nullable disable
public class BitmapText : Text
{
  public BitmapText.FontScale AutoSize;
  private bool mDelayUpdate;

  public virtual Material defaultMaterial => Graphic.defaultGraphicMaterial;

  private void ResizeFont()
  {
    if (0 > this.AutoSize.CountMin || this.AutoSize.CountMin >= this.AutoSize.CountMax || this.AutoSize.SizeMin <= 0 || this.AutoSize.SizeMax <= 0)
      return;
    this.fontSize = (int) Mathf.Lerp((float) this.AutoSize.SizeMin, (float) this.AutoSize.SizeMax, (float) (base.text.Length - this.AutoSize.CountMin) / (float) (this.AutoSize.CountMax - this.AutoSize.CountMin));
  }

  protected virtual void Start()
  {
    ((UIBehaviour) this).Start();
    this.ResizeFont();
  }

  private void Update()
  {
    if (!this.mDelayUpdate || !this.IsParentCanvasActive)
      return;
    this.mDelayUpdate = false;
    ((Behaviour) this).enabled = !((Behaviour) this).enabled;
    ((Behaviour) this).enabled = !((Behaviour) this).enabled;
  }

  protected virtual void OnTransformParentChanged()
  {
    ((MaskableGraphic) this).OnTransformParentChanged();
  }

  public virtual string text
  {
    get => base.text;
    set
    {
      if (((Behaviour) this).enabled)
      {
        if (!this.mDelayUpdate && !this.IsParentCanvasActive)
          this.mDelayUpdate = true;
        if (!this.mDelayUpdate)
        {
          ((Behaviour) this).enabled = false;
          ((Behaviour) this).enabled = true;
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
      for (CanvasGroup componentInParent = ((Component) this).GetComponentInParent<CanvasGroup>(); Object.op_Inequality((Object) componentInParent, (Object) null); componentInParent = !Object.op_Inequality((Object) parent1, (Object) null) ? (CanvasGroup) null : ((Component) parent1).GetComponentInParent<CanvasGroup>())
      {
        if ((double) componentInParent.alpha <= 0.0)
          return false;
        parent1 = ((Component) componentInParent).transform.parent;
      }
      Transform parent2;
      for (Canvas componentInParent = ((Component) this).GetComponentInParent<Canvas>(); Object.op_Inequality((Object) componentInParent, (Object) null); componentInParent = !Object.op_Inequality((Object) parent2, (Object) null) ? (Canvas) null : ((Component) parent2).GetComponentInParent<Canvas>())
      {
        if (!((Behaviour) componentInParent).enabled)
          return false;
        parent2 = ((Component) componentInParent).transform.parent;
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
