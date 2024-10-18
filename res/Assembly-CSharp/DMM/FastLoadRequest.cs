// Decompiled with JetBrains decompiler
// Type: FastLoadRequest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

#nullable disable
public class FastLoadRequest : LoadRequest
{
  private static double mLoadTime;
  private const double MaxLoadTimePerFrame = 0.0083333333333333332;
  private static List<FastLoadRequest> mRequests = new List<FastLoadRequest>();
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
        this.mAssetBundle = AssetManager.Instance.OpenAssetBundleAndDependencies(this.mAssetBundleNode, 0);
        this.mAsset = this.mAssetBundle.AssetBundle.LoadAsset(Path.GetFileNameWithoutExtension(this.mAssetName));
        this.mAssetBundle = (AssetBundleCache) null;
        AssetBundleUnloader.ResetPassCount();
        AssetBundleUnloader.ReserveUnload(false);
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

  public override float progress => !this.isDone ? 0.0f : 1f;

  public override bool isDone => this.mIsDone;

  public override Object asset => this.mAsset;

  public override bool MoveNext() => !this.mIsDone;

  public override void KeepSourceAlive()
  {
  }
}
