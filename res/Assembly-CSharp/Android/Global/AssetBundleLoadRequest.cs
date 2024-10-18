// Decompiled with JetBrains decompiler
// Type: AssetBundleLoadRequest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;

public class AssetBundleLoadRequest : LoadRequest
{
  private UnityEngine.Object mAsset;
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
  }

  public AssetBundleLoadRequest(UnityEngine.Object _asset)
  {
    this.mAsset = _asset;
  }

  public override UnityEngine.Object asset
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
      return 0.0f;
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
    if (this.mLoadRequest == null)
      return;
    if (!this.mLoadRequest.isDone)
      return;
    try
    {
      this.mAsset = (object) this.mComponentClass == null ? this.mLoadRequest.asset : (UnityEngine.Object) (this.mLoadRequest.asset as GameObject).GetComponent(this.mComponentClass);
    }
    catch (Exception ex)
    {
      DebugUtility.LogError("(" + (object) ex.GetType() + ")" + ex.Message + " >>> AssetBundle:" + this.mAssetBundle.Name + " " + (!((UnityEngine.Object) this.mAssetBundle.AssetBundle == (UnityEngine.Object) null) ? (object) string.Empty : (object) "is null"));
    }
    this.mLoadRequest = (AssetBundleRequest) null;
    this.UntrackTextComponents(this.mAsset);
  }
}
