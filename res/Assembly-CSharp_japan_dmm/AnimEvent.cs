// Decompiled with JetBrains decompiler
// Type: AnimEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
public class AnimEvent : ScriptableObject
{
  public float Start;
  public float End;
  protected bool mIsEnd;

  public virtual void OnStart(GameObject go) => this.mIsEnd = false;

  public virtual void OnTick(GameObject go, float ratio)
  {
  }

  public virtual void OnEnd(GameObject go) => this.mIsEnd = true;

  public bool IsEnd => this.mIsEnd;
}
