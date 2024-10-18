// Decompiled with JetBrains decompiler
// Type: GiziScroll
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class GiziScroll : MonoBehaviour
{
  public Vector2 MinAspectSize = new Vector2(3f, 2f);
  public float MinScroll = 2f;
  public float MinOffset = 1.5f;
  [Space(10f)]
  public Vector2 MaxAspectSize = new Vector2(16f, 9f);
  public float MaxScroll = 1f;
  public float MaxOffset = 1.5f;
  [Space(10f)]
  public float MaxZ = 5f;
  private List<KeyValuePair<Transform, Vector3>> mChildren = new List<KeyValuePair<Transform, Vector3>>();
  public Camera ProjectionCamera;
  private float mScrollPos;

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
    get
    {
      return this.mScrollPos;
    }
  }

  private void Start()
  {
    Transform transform = this.transform;
    for (int index = 0; index < transform.childCount; ++index)
    {
      Transform child = transform.GetChild(index);
      this.mChildren.Add(new KeyValuePair<Transform, Vector3>(child, child.position));
    }
    this.Layout();
  }

  private void Layout()
  {
    float num1 = (float) ((double) this.ScrollPos * 2.0 - 1.0);
    float num2 = this.MinAspectSize.x / this.MinAspectSize.y;
    float num3 = this.MaxAspectSize.x / this.MaxAspectSize.y;
    float t = Mathf.Clamp01((float) (((double) ((float) Screen.width / (float) Screen.height) - (double) num2) / ((double) num3 - (double) num2)));
    float num4 = Mathf.Lerp(this.MinOffset, this.MaxOffset, t);
    float num5 = Mathf.Lerp(this.MinScroll, this.MaxScroll, t);
    for (int index = 0; index < this.mChildren.Count; ++index)
    {
      Transform key = this.mChildren[index].Key;
      float num6 = Mathf.Abs(key.position.z / this.MaxZ);
      key.position = this.mChildren[index].Value - Vector3.right * (float) ((double) num1 * (double) num4 * (double) num6 + (double) num1 * (double) num5);
      if ((Object) this.ProjectionCamera != (Object) null)
      {
        UIProjector component = key.GetComponent<UIProjector>();
        if ((Object) component != (Object) null)
          component.PreCull(this.ProjectionCamera);
      }
    }
  }

  private void OnValidate()
  {
    this.ScrollPos = Mathf.Clamp(this.ScrollPos, -1f, 1f);
  }
}
