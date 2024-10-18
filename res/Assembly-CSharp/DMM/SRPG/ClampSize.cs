// Decompiled with JetBrains decompiler
// Type: SRPG.ClampSize
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.EventSystems;

#nullable disable
namespace SRPG
{
  [ExecuteInEditMode]
  public class ClampSize : UIBehaviour
  {
    private RectTransform mTransform;
    public RectTransform Target;
    public bool ClampX = true;
    public float XSize = 64f;
    public float XMargin;
    public float XPadding;
    public bool ClampY = true;
    public float YSize = 64f;
    public float YMargin;
    public float YPadding;

    protected virtual void Awake() => base.Awake();

    protected virtual void OnRectTransformDimensionsChange()
    {
      base.OnRectTransformDimensionsChange();
      this.Resize();
    }

    public void Resize()
    {
      if (Object.op_Equality((Object) this.mTransform, (Object) null))
        this.mTransform = (RectTransform) ((Component) this).transform;
      if (Object.op_Equality((Object) this.Target, (Object) null))
        return;
      if (!((Transform) this.Target).IsChildOf((Transform) this.mTransform))
      {
        Debug.LogError((object) (((Object) this.Target).name + " is not child of " + ((Object) this).name));
      }
      else
      {
        Rect rect = this.mTransform.rect;
        Vector2 size = ((Rect) ref rect).size;
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
