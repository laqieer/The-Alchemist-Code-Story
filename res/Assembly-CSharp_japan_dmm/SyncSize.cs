// Decompiled with JetBrains decompiler
// Type: SyncSize
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
[DisallowMultipleComponent]
public class SyncSize : MonoBehaviour
{
  public RectTransform Source;
  public float ExtraW;
  public float ExtraH;
  private float mLastWidth;
  private float mLastHeight;
  private RectTransform mRect;

  private void Start()
  {
    this.mRect = ((Component) this).transform as RectTransform;
    this.Sync();
  }

  private void LateUpdate() => this.Sync();

  private void Sync()
  {
    if (Object.op_Equality((Object) this.Source, (Object) null) || Object.op_Equality((Object) this.mRect, (Object) null))
      return;
    Rect rect1 = this.Source.rect;
    float num1 = ((Rect) ref rect1).width + this.ExtraW;
    Rect rect2 = this.Source.rect;
    float num2 = ((Rect) ref rect2).height + this.ExtraH;
    if ((double) num1 == (double) this.mLastWidth && (double) num2 == (double) this.mLastHeight)
      return;
    this.mLastWidth = num1;
    this.mLastHeight = num2;
    this.mRect.sizeDelta = new Vector2()
    {
      x = this.mLastWidth,
      y = this.mLastHeight
    };
  }
}
