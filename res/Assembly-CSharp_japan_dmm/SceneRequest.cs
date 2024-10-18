// Decompiled with JetBrains decompiler
// Type: SceneRequest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;

#nullable disable
public abstract class SceneRequest : IEnumerator
{
  public string SceneName;

  public abstract bool ActivateScene();

  public abstract bool IsActivated { get; }

  public abstract bool isDone { get; }

  public abstract bool canBeActivated { get; }

  public virtual void Reset()
  {
  }

  public abstract bool MoveNext();

  public abstract object Current { get; }

  public virtual float progress => 0.0f;

  public abstract bool isAdditive { get; }
}
