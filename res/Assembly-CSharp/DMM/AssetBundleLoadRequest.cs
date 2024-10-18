// Decompiled with JetBrains decompiler
// Type: AssetBundleLoadRequest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using UnityEngine;

#nullable disable
public class AssetBundleLoadRequest : LoadRequest
{
  private Object mAsset;
  private AssetBundleRequest mLoadRequest;
  private AssetBundleCache mAssetBundle;
  private System.Type mComponentClass;

  public AssetBundleLoadRequest()
  {
  }

  public AssetBundleLoadRequest(AssetBundleCache assetBundle, string assetName, System.Type assetType)
  {
    if (assetType.IsSubclassOf(typeof (Component)))
      this.mComponentClass = assetType;
    this.mAssetBundle = assetBundle;
    this.mLoadRequest = this.mAssetBundle.AssetBundle.LoadAssetAsync(assetName);
    AssetBundleUnloader.AddAsyncOperation((AsyncOperation) this.mLoadRequest);
  }

  public AssetBundleLoadRequest(Object _asset) => this.mAsset = _asset;

  public override Object asset => this.mAsset;

  public override float progress
  {
    get => this.mLoadRequest != null ? ((AsyncOperation) this.mLoadRequest).progress : 0.0f;
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
    if (this.mLoadRequest == null)
      return;
    if (!((AsyncOperation) this.mLoadRequest).isDone)
      return;
    try
    {
      this.mAsset = (object) this.mComponentClass == null ? this.mLoadRequest.asset : (Object) (this.mLoadRequest.asset as GameObject).GetComponent(this.mComponentClass);
    }
    catch (Exception ex)
    {
      DebugUtility.LogError("(" + (object) ex.GetType() + ")" + ex.Message + " >>> AssetBundle:" + this.mAssetBundle.Name + " " + (!Object.op_Equality((Object) this.mAssetBundle.AssetBundle, (Object) null) ? (object) string.Empty : (object) "is null"));
    }
    this.mLoadRequest = (AssetBundleRequest) null;
    LoadRequest.UntrackTextComponents(this.mAsset);
  }

  public override void KeepSourceAlive()
  {
  }
}
