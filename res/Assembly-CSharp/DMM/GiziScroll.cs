// Decompiled with JetBrains decompiler
// Type: GiziScroll
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class GiziScroll : MonoBehaviour
{
  public Vector2 MinAspectSize = new Vector2(3f, 2f);
  public float MinScroll = 2f;
  public float MinOffset = 1.5f;
  [Space(10f)]
  public Vector2 MaxAspectSize = new Vector2(19.5f, 9f);
  public float MaxScroll = 1f;
  public float MaxOffset = 1.5f;
  [Space(10f)]
  public float MaxZ = 5f;
  public Camera ProjectionCamera;
  private float mScrollPos;
  private List<KeyValuePair<Transform, Vector3>> mChildren = new List<KeyValuePair<Transform, Vector3>>();

  public float ScrollPos
  {
    set
    {
      value = Mathf.Clamp01(value);
      if ((double) this.mScrollPos == (double) value)
        return;
      this.mScrollPos = value;
      this.Layout();
    }
    get => this.mScrollPos;
  }

  private void Start()
  {
    Transform transform = ((Component) this).transform;
    for (int index = 0; index < transform.childCount; ++index)
    {
      Transform child = transform.GetChild(index);
      AdjustTownIconPos component = ((Component) child).GetComponent<AdjustTownIconPos>();
      if (Object.op_Inequality((Object) component, (Object) null))
        component.AdjustIconPos();
      this.mChildren.Add(new KeyValuePair<Transform, Vector3>(child, child.localPosition));
    }
    this.Layout();
  }

  private void Layout()
  {
    Vector2 screenSize = SetCanvasBounds.GetScreenSize();
    float num1 = (float) ((double) this.ScrollPos * 2.0 - 1.0);
    float num2 = this.MinAspectSize.x / this.MinAspectSize.y;
    float num3 = this.MaxAspectSize.x / this.MaxAspectSize.y;
    float num4 = Mathf.Clamp01((float) (((double) (screenSize.x / screenSize.y) - (double) num2) / ((double) num3 - (double) num2)));
    float num5 = Mathf.Lerp(this.MinOffset, this.MaxOffset, num4);
    float num6 = Mathf.Lerp(this.MinScroll, this.MaxScroll, num4);
    for (int index = 0; index < this.mChildren.Count; ++index)
    {
      Transform key = this.mChildren[index].Key;
      float num7 = Mathf.Abs(key.localPosition.z / this.MaxZ);
      key.localPosition = Vector3.op_Subtraction(this.mChildren[index].Value, Vector3.op_Multiply(Vector3.right, (float) ((double) num1 * (double) num5 * (double) num7 + (double) num1 * (double) num6)));
      if (Object.op_Inequality((Object) this.ProjectionCamera, (Object) null))
      {
        UIProjector component = ((Component) key).GetComponent<UIProjector>();
        if (Object.op_Inequality((Object) component, (Object) null))
          component.PreCull(this.ProjectionCamera);
      }
    }
  }

  private void OnValidate() => this.ScrollPos = Mathf.Clamp(this.ScrollPos, -1f, 1f);
}
