// Decompiled with JetBrains decompiler
// Type: UVScroll
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
[RequireComponent(typeof (Renderer))]
[DisallowMultipleComponent]
[AddComponentMenu("Rendering/UVScroll")]
public class UVScroll : MonoBehaviour
{
  public Vector2 Speed;
  private Vector2 mOffset = Vector2.zero;
  public Vector2 Limit;

  private void Update()
  {
    UVScroll uvScroll = this;
    uvScroll.mOffset = Vector2.op_Addition(uvScroll.mOffset, Vector2.op_Multiply(Time.deltaTime, this.Speed));
    if ((double) this.Limit.x != 0.0 && (double) Mathf.Abs(this.mOffset.x) >= (double) this.Limit.x)
      this.mOffset.x %= this.Limit.x;
    if ((double) this.Limit.y != 0.0 && (double) Mathf.Abs(this.mOffset.y) >= (double) this.Limit.y)
      this.mOffset.y %= this.Limit.y;
    ((Component) this).GetComponent<Renderer>().material.mainTextureOffset = this.mOffset;
  }
}
