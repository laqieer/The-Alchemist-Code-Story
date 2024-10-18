// Decompiled with JetBrains decompiler
// Type: BundleSceneRequest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.SceneManagement;

#nullable disable
public class BundleSceneRequest : SceneRequest
{
  private bool mSceneActivated;
  private bool mAdditive;
  private LoadRequest mRequest;

  public BundleSceneRequest(LoadRequest request, bool additive)
  {
    this.mRequest = request;
    this.mAdditive = additive;
  }

  public override bool ActivateScene()
  {
    if (!this.isDone || this.mSceneActivated)
      return this.mSceneActivated;
    if (!this.mAdditive)
    {
      SceneAssetBundleLoader.SceneBundle = this.mRequest.asset;
      SceneManager.LoadScene("EmptyScene");
    }
    else if (Object.op_Inequality(this.mRequest.asset, (Object) null))
      Object.Instantiate(this.mRequest.asset);
    this.mSceneActivated = true;
    return true;
  }

  public override bool IsActivated => this.mSceneActivated;

  public override bool isAdditive => this.mAdditive;

  public override bool canBeActivated => this.mRequest.isDone;

  public override bool isDone => this.mSceneActivated && (double) this.mRequest.progress >= 1.0;

  public override bool MoveNext() => this.mRequest != null && !this.mRequest.isDone;

  public override object Current => (object) null;

  public override float progress => this.mRequest != null ? this.mRequest.progress : 0.0f;
}
