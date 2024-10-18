// Decompiled with JetBrains decompiler
// Type: ResourceLoadRequest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

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
  }

  public ResourceLoadRequest(Object _asset)
  {
    this.mAsset = _asset;
  }

  public override Object asset
  {
    get
    {
      return this.mAsset;
    }
  }

  public override float progress
  {
    get
    {
      if (this.mLoadRequest != null)
        return this.mLoadRequest.progress;
      return this.mAsset != (Object) null ? 1f : 0.0f;
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
    if (this.mLoadRequest == null || !this.mLoadRequest.isDone)
      return;
    this.mAsset = this.mLoadRequest.asset;
    this.mLoadRequest = (ResourceRequest) null;
    this.UntrackTextComponents(this.mAsset);
  }
}
