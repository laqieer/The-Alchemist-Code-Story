// Decompiled with JetBrains decompiler
// Type: SceneRequest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections;

public abstract class SceneRequest : IEnumerator
{
  public abstract bool ActivateScene();

  public abstract bool IsActivated { get; }

  public abstract bool isDone { get; }

  public abstract bool canBeActivated { get; }

  public virtual void Reset()
  {
  }

  public abstract bool MoveNext();

  public abstract object Current { get; }

  public virtual float progress
  {
    get
    {
      return 0.0f;
    }
  }

  public abstract bool isAdditive { get; }
}
