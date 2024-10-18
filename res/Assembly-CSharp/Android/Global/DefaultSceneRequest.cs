// Decompiled with JetBrains decompiler
// Type: DefaultSceneRequest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class DefaultSceneRequest : SceneRequest
{
  private bool mSceneActivated;
  private AsyncOperation mRequest;
  private bool mAdditive;

  public DefaultSceneRequest(AsyncOperation request, bool additive)
  {
    request.allowSceneActivation = false;
    this.mAdditive = additive;
    this.mRequest = request;
  }

  public override bool ActivateScene()
  {
    if (this.mSceneActivated)
      return this.mSceneActivated;
    this.mRequest.allowSceneActivation = true;
    this.mSceneActivated = true;
    AssetManager.OnSceneActivate((SceneRequest) this);
    return true;
  }

  public override bool IsActivated
  {
    get
    {
      return this.mSceneActivated;
    }
  }

  public override bool isAdditive
  {
    get
    {
      return this.mAdditive;
    }
  }

  public override bool isDone
  {
    get
    {
      if (this.mSceneActivated)
        return this.mRequest.isDone;
      return false;
    }
  }

  public override bool canBeActivated
  {
    get
    {
      return (double) this.mRequest.progress >= 0.899999976158142;
    }
  }

  public override bool MoveNext()
  {
    if (this.mRequest != null)
      return !this.isDone;
    return false;
  }

  public override object Current
  {
    get
    {
      return (object) null;
    }
  }

  public override float progress
  {
    get
    {
      if (this.mRequest != null)
        return this.mRequest.progress;
      return 0.0f;
    }
  }
}
