﻿// Decompiled with JetBrains decompiler
// Type: SRPG.DebugManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;

namespace SRPG
{
  [ExecuteInEditMode]
  [AddComponentMenu("Scripts/SRPG/Manager/Debug")]
  public class DebugManager : MonoSingleton<DebugManager>
  {
    private float mLastCollectNum;
    private int mAllocMem;
    private int mAllocPeak;

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
      this.mAllocMem = (int) Profiler.get_usedHeapSize();
      this.mAllocPeak = this.mAllocMem <= this.mAllocPeak ? this.mAllocPeak : this.mAllocMem;
    }

    public bool IsWebViewEnable()
    {
      if (!GameUtility.IsDebugBuild)
        return true;
      string operatingSystem = SystemInfo.operatingSystem;
      if (string.IsNullOrEmpty(operatingSystem))
        return true;
      Debug.Log((object) ("Android:" + operatingSystem));
      string[] strArray1 = operatingSystem.Split(' ');
      if (strArray1 == null)
        return true;
      for (int index1 = 0; index1 < strArray1.Length; ++index1)
      {
        string[] strArray2 = strArray1[index1].Split('.');
        if (strArray2 != null && strArray2.Length >= 2)
        {
          int[] numArray = new int[strArray2.Length];
          bool flag = true;
          for (int index2 = 0; index2 < strArray2.Length; ++index2)
          {
            if (!int.TryParse(strArray2[index2], out numArray[index2]))
            {
              flag = false;
              break;
            }
          }
          if (flag)
          {
            if (numArray[0] < 4 || numArray[0] == 4 && numArray[1] <= 3)
            {
              Debug.LogWarning((object) "WebView maybe crash");
              return false;
            }
            break;
          }
        }
      }
      return true;
    }
  }
}
