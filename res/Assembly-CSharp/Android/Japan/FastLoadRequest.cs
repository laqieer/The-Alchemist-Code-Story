// Decompiled with JetBrains decompiler
// Type: FastLoadRequest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FastLoadRequest : LoadRequest
{
  private static List<FastLoadRequest> mRequests = new List<FastLoadRequest>();
  private static double mLoadTime;
  private const double MaxLoadTimePerFrame = 0.00833333333333333;
  private Object mAsset;
  private bool mIsDone;
  private AssetBundleCache mAssetBundle;
  private AssetList.Item mAssetBundleNode;
  private string mAssetName;
  private System.Type mAssetType;

  public FastLoadRequest(AssetList.Item assetBundleNode, string assetName, System.Type assetType)
  {
    this.mAssetBundleNode = assetBundleNode;
    this.mAssetName = assetName;
    this.mAssetType = assetType;
    FastLoadRequest.mRequests.Add(this);
    this.UpdateLoading();
  }

  public static void UpdateAll()
  {
    if (FastLoadRequest.mRequests.Count <= 0)
      return;
    for (int index = 0; index < FastLoadRequest.mRequests.Count && FastLoadRequest.mLoadTime < 1.0 / 120.0; ++index)
    {
      FastLoadRequest mRequest = FastLoadRequest.mRequests[index];
      mRequest.UpdateLoading();
      if (mRequest.isDone)
        --index;
    }
    FastLoadRequest.mLoadTime = 0.0;
  }

  private void UpdateLoading()
  {
    try
    {
      if (this.isDone || FastLoadRequest.mLoadTime >= 1.0 / 120.0)
        return;
      DateTime now1 = DateTime.Now;
      if (this.mAssetBundleNode != null)
      {
        this.mAssetBundle = AssetManager.Instance.OpenAssetBundleAndDependencies(this.mAssetBundleNode, 0, (List<AssetBundleCache>) null);
        this.mAsset = this.mAssetBundle.AssetBundle.LoadAsset(Path.GetFileNameWithoutExtension(this.mAssetName));
        this.mAssetBundle = (AssetBundleCache) null;
        AssetManager.Instance.UnloadUnusedAssetBundles(false, false);
      }
      else
        this.mAsset = Resources.Load(AssetManager.ConvertEmbededResourcesPath(this.mAssetName), this.mAssetType);
      DateTime now2 = DateTime.Now;
      FastLoadRequest.mLoadTime += (now2 - now1).TotalSeconds;
      FastLoadRequest.mRequests.Remove(this);
      this.mIsDone = true;
      LoadRequest.UntrackTextComponents(this.mAsset);
    }
    catch (Exception ex)
    {
      Debug.Log((object) ("Exception: LoadFile[" + this.mAssetName + "]" + ex.ToString()));
      FastLoadRequest.mRequests.Remove(this);
      this.mIsDone = true;
    }
  }

  public override float progress
  {
    get
    {
      return !this.isDone ? 0.0f : 1f;
    }
  }

  public override bool isDone
  {
    get
    {
      return this.mIsDone;
    }
  }

  public override Object asset
  {
    get
    {
      return this.mAsset;
    }
  }

  public override bool MoveNext()
  {
    return !this.mIsDone;
  }

  public override void KeepSourceAlive()
  {
  }
}
