// Decompiled with JetBrains decompiler
// Type: ResourceLoadRequest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
public class ResourceLoadRequest : LoadRequest
{
  private Object mAsset;
  private ResourceRequest mLoadRequest;

  public ResourceLoadRequest()
  {
  }

  public ResourceLoadRequest(ResourceRequest request)
  {
    this.mLoadRequest = request;
    AssetBundleUnloader.AddAsyncOperation((AsyncOperation) request);
  }

  public ResourceLoadRequest(Object _asset) => this.mAsset = _asset;

  public override Object asset => this.mAsset;

  public override float progress
  {
    get
    {
      if (this.mLoadRequest != null)
        return ((AsyncOperation) this.mLoadRequest).progress;
      return Object.op_Inequality(this.mAsset, (Object) null) ? 1f : 0.0f;
    }
  }

  public override bool isDone
  {
    get
    {
      this.UpdateLoading();
      return this.mLoadRequest == null;
    }
  }

  public override bool MoveNext()
  {
    this.UpdateLoading();
    return this.mLoadRequest != null;
  }

  private void UpdateLoading()
  {
    if (this.mLoadRequest == null || !((AsyncOperation) this.mLoadRequest).isDone)
      return;
    this.mAsset = this.mLoadRequest.asset;
    this.mLoadRequest = (ResourceRequest) null;
    LoadRequest.UntrackTextComponents(this.mAsset);
  }
}
