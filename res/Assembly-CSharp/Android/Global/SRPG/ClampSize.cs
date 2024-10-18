// Decompiled with JetBrains decompiler
// Type: SRPG.ClampSize
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.EventSystems;

namespace SRPG
{
  [ExecuteInEditMode]
  public class ClampSize : UIBehaviour
  {
    public bool ClampX = true;
    public float XSize = 64f;
    public bool ClampY = true;
    public float YSize = 64f;
    private RectTransform mTransform;
    public RectTransform Target;
    public float XMargin;
    public float XPadding;
    public float YMargin;
    public float YPadding;

    protected override void Awake()
    {
      base.Awake();
    }

    protected override void OnRectTransformDimensionsChange()
    {
      base.OnRectTransformDimensionsChange();
      this.Resize();
    }

    public void Resize()
    {
      if ((UnityEngine.Object) this.mTransform == (UnityEngine.Object) null)
        this.mTransform = (RectTransform) this.transform;
      if ((UnityEngine.Object) this.Target == (UnityEngine.Object) null)
        return;
      if (!this.Target.IsChildOf((Transform) this.mTransform))
      {
        Debug.LogError((object) (this.Target.name + " is not child of " + this.name));
      }
      else
      {
        Vector2 size = this.mTransform.rect.size;
        if (this.ClampX)
        {
          float num = Mathf.Floor((size.x - this.XMargin - this.XPadding) / this.XSize) * this.XSize + this.XMargin;
          size.x = num;
        }
        if (this.ClampY)
        {
          float num = Mathf.Floor((size.y - this.YMargin - this.YPadding) / this.YSize) * this.YSize + this.YMargin;
          size.y = num;
        }
        this.Target.sizeDelta = size;
      }
    }
  }
}
