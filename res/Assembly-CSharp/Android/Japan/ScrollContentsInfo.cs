// Decompiled with JetBrains decompiler
// Type: ScrollContentsInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

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

  public virtual Vector2 SetRangePos(Vector2 position)
  {
    return Vector2.zero;
  }

  public virtual bool CheckRangePos(float pos)
  {
    return false;
  }

  public virtual float GetNearIconPos(float pos)
  {
    return pos;
  }
}
