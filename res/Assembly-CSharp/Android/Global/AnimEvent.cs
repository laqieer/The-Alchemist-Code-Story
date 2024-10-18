// Decompiled with JetBrains decompiler
// Type: AnimEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class AnimEvent : ScriptableObject
{
  public float Start;
  public float End;

  public virtual void OnStart(GameObject go)
  {
  }

  public virtual void OnTick(GameObject go, float ratio)
  {
  }

  public virtual void OnEnd(GameObject go)
  {
  }

  public virtual void UpdatePreview(GameObject go, float time)
  {
  }
}
