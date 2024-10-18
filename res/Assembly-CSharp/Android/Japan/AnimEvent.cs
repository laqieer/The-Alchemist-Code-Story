// Decompiled with JetBrains decompiler
// Type: AnimEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class AnimEvent : ScriptableObject
{
  public float Start;
  public float End;
  protected bool mIsEnd;

  public virtual void OnStart(GameObject go)
  {
    this.mIsEnd = false;
  }

  public virtual void OnTick(GameObject go, float ratio)
  {
  }

  public virtual void OnEnd(GameObject go)
  {
    this.mIsEnd = true;
  }

  public bool IsEnd
  {
    get
    {
      return this.mIsEnd;
    }
  }
}
