// Decompiled with JetBrains decompiler
// Type: SceneRequest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections;

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

  public virtual float progress
  {
    get
    {
      return 0.0f;
    }
  }

  public abstract bool isAdditive { get; }
}
