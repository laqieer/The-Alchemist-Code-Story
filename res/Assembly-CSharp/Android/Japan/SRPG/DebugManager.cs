// Decompiled with JetBrains decompiler
// Type: SRPG.DebugManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;
using UnityEngine.Profiling;

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

    public bool IsWebViewEnable()
    {
      return true;
    }
  }
}
