// Decompiled with JetBrains decompiler
// Type: UVScroll
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

[RequireComponent(typeof (Renderer))]
[DisallowMultipleComponent]
[AddComponentMenu("Rendering/UVScroll")]
public class UVScroll : MonoBehaviour
{
  private Vector2 mOffset = Vector2.zero;
  public Vector2 Speed;
  public Vector2 Limit;

  private void Update()
  {
    this.mOffset += Time.deltaTime * this.Speed;
    if ((double) this.Limit.x != 0.0 && (double) Mathf.Abs(this.mOffset.x) >= (double) this.Limit.x)
      this.mOffset.x %= this.Limit.x;
    if ((double) this.Limit.y != 0.0 && (double) Mathf.Abs(this.mOffset.y) >= (double) this.Limit.y)
      this.mOffset.y %= this.Limit.y;
    this.GetComponent<Renderer>().material.mainTextureOffset = this.mOffset;
  }
}
