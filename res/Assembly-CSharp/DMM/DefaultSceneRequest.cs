// Decompiled with JetBrains decompiler
// Type: DefaultSceneRequest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
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
    AssetBundleUnloader.AddAsyncOperation(request);
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

  public override bool IsActivated => this.mSceneActivated;

  public override bool isAdditive => this.mAdditive;

  public override bool isDone => this.mSceneActivated && this.mRequest.isDone;

  public override bool canBeActivated => (double) this.mRequest.progress >= 0.89999997615814209;

  public override bool MoveNext() => this.mRequest != null && !this.isDone;

  public override object Current => (object) null;

  public override float progress => this.mRequest != null ? this.mRequest.progress : 0.0f;
}
