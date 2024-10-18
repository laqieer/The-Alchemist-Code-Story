﻿// Decompiled with JetBrains decompiler
// Type: AssetBundleLoadRequest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

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
  }

  public AssetBundleLoadRequest(Object _asset)
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
    try
    {
      this.mAsset = this.mComponentClass == null ? this.mLoadRequest.asset : (Object) (this.mLoadRequest.asset as GameObject).GetComponent(this.mComponentClass);
    }
    catch (Exception ex)
    {
      DebugUtility.LogError("(" + (object) ex.GetType() + ")" + ex.Message + " >>> AssetBundle:" + this.mAssetBundle.Name + " " + (!((Object) this.mAssetBundle.AssetBundle == (Object) null) ? (object) string.Empty : (object) "is null"));
    }
    this.mLoadRequest = (AssetBundleRequest) null;
    LoadRequest.UntrackTextComponents(this.mAsset);
  }

  public override void KeepSourceAlive()
  {
  }
}
