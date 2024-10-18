// Decompiled with JetBrains decompiler
// Type: SRPG.DebugManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using UnityEngine;
using UnityEngine.Profiling;

#nullable disable
namespace SRPG
{
  [ExecuteInEditMode]
  [AddComponentMenu("Scripts/SRPG/Manager/Debug")]
  public class DebugManager : MonoSingleton<DebugManager>
  {
    private float mLastCollectNum;
    private long mAllocMem;
    private long mAllocPeak;

    public bool IsShowed { set; get; }

    public bool IsShowedInEditor { set; get; }

    protected override void Initialize()
    {
      this.IsShowed = true;
      this.IsShowedInEditor = false;
      UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) this);
    }

    private void Update()
    {
      if (!this.IsShowed || !Application.isPlaying && !this.IsShowedInEditor)
        return;
      if ((double) this.mLastCollectNum == (double) GC.CollectionCount(0))
        ;
      this.mAllocMem = Profiler.usedHeapSizeLong;
      this.mAllocPeak = this.mAllocMem <= this.mAllocPeak ? this.mAllocPeak : this.mAllocMem;
    }

    public bool IsWebViewEnable() => true;
  }
}
