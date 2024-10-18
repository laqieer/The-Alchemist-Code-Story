// Decompiled with JetBrains decompiler
// Type: ScrollContentsInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
public class ScrollContentsInfo : MonoBehaviour
{
  protected float mStartPosX;
  protected float mEndPosX;
  protected float mStartPosY;
  protected float mEndPosY;

  private void Start()
  {
    this.mStartPosX = 0.0f;
    this.mEndPosX = 0.0f;
    this.mStartPosY = 0.0f;
    this.mEndPosY = 0.0f;
  }

  public virtual Vector2 SetRangePos(Vector2 position) => Vector2.zero;

  public virtual bool CheckRangePos(float pos) => false;

  public virtual float GetNearIconPos(float pos) => pos;
}
