// Decompiled with JetBrains decompiler
// Type: AssetBundleUnloader
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class AssetBundleUnloader : MonoBehaviour
{
  private static AssetBundleUnloader Inastance;
  private List<AsyncOperation> mAsyncOperationList = new List<AsyncOperation>();
  private bool mIsReservedUnload;
  private bool mIsForceUnload = true;
  private int mRestPassCount;
  private readonly int PASS_COUNT = 2;
  private bool mIsForceUnloadNow;

  public static bool IsForceUnloadNow => AssetBundleUnloader.Inastance.mIsForceUnloadNow;

  private void Awake()
  {
    AssetBundleUnloader.Inastance = this;
    this.mRestPassCount = this.PASS_COUNT;
  }

  private void LateUpdate()
  {
    this.mRestPassCount = !this.CanUnload() ? this.PASS_COUNT : this.mRestPassCount - 1;
    if (this.mRestPassCount > 0)
      return;
    this.ExecUnload();
    this.mRestPassCount = this.PASS_COUNT;
  }

  public static void ResetPassCount()
  {
    AssetBundleUnloader.Inastance.mRestPassCount = AssetBundleUnloader.Inastance.PASS_COUNT;
  }

  public static void AddAsyncOperation(AsyncOperation ao)
  {
    AssetBundleUnloader.Inastance.mAsyncOperationList.Add(ao);
  }

  public static void ReserveUnload(bool is_force)
  {
    if (AssetBundleUnloader.Inastance.mIsForceUnloadNow)
      return;
    AssetBundleUnloader.Inastance.mIsReservedUnload = true;
    if (!AssetBundleUnloader.Inastance.mIsForceUnload)
      return;
    AssetBundleUnloader.Inastance.mIsForceUnload = is_force;
  }

  public static void ReserveUnloadForce()
  {
    AssetBundleUnloader.Inastance.mIsForceUnloadNow = true;
    AssetBundleUnloader.Inastance.mIsReservedUnload = true;
    AssetBundleUnloader.Inastance.mIsForceUnload = true;
  }

  private bool CanUnload()
  {
    if (!this.mIsReservedUnload)
      return false;
    for (int index = 0; index < this.mAsyncOperationList.Count; ++index)
    {
      if (this.mAsyncOperationList[index] != null && !this.mAsyncOperationList[index].isDone)
        return false;
    }
    return !AssetManager.IsLoading;
  }

  private void ExecUnload()
  {
    AssetManager.Instance.UnloadUnusedAssetBundles(true, this.mIsForceUnload);
    this.mAsyncOperationList.Clear();
    this.mIsForceUnload = true;
    this.mIsReservedUnload = false;
    AssetBundleUnloader.Inastance.mIsForceUnloadNow = false;
  }
}
