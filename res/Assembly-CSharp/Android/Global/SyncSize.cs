// Decompiled with JetBrains decompiler
// Type: SyncSize
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

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
    this.mRect = this.transform as RectTransform;
    this.Sync();
  }

  private void LateUpdate()
  {
    this.Sync();
  }

  private void Sync()
  {
    if ((Object) this.Source == (Object) null || (Object) this.mRect == (Object) null)
      return;
    float num1 = this.Source.rect.width + this.ExtraW;
    float num2 = this.Source.rect.height + this.ExtraH;
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
