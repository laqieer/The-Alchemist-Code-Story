﻿// Decompiled with JetBrains decompiler
// Type: SRPG.UnitGetWindowController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  public class UnitGetWindowController : MonoBehaviour
  {
    private UnitGetWindow mController;
    private bool mIsEnd;

    public bool IsEnd
    {
      get
      {
        return this.mIsEnd;
      }
    }

    public void Init(UnitGetParam rewards = null)
    {
      UnitGetParam unitGetParam = rewards == null ? GlobalVars.UnitGetReward : rewards;
      if (unitGetParam == null || unitGetParam.Params.Count <= 0)
      {
        this.mIsEnd = true;
      }
      else
      {
        bool flag = true;
        string[] unitIds = new string[unitGetParam.Params.Count];
        bool[] isConvert = new bool[unitGetParam.Params.Count];
        for (int index = 0; index < unitGetParam.Params.Count; ++index)
        {
          if (unitGetParam.Params[index].ItemType != EItemType.Unit)
          {
            unitIds[index] = string.Empty;
          }
          else
          {
            if (flag)
              flag = false;
            unitIds[index] = unitGetParam.Params[index].ItemId;
            isConvert[index] = unitGetParam.Params[index].IsConvert;
            if (!isConvert[index])
              DownloadUtility.DownloadUnit(unitGetParam.Params[index].UnitParam, (JobData[]) null);
          }
        }
        this.mIsEnd = flag;
        if (this.mIsEnd)
          return;
        this.StartCoroutine(this.SpawnEffectAsync(unitIds, isConvert));
      }
    }

    [DebuggerHidden]
    private IEnumerator SpawnEffectAsync(string[] unitIds, bool[] isConvert)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitGetWindowController.\u003CSpawnEffectAsync\u003Ec__Iterator144() { unitIds = unitIds, isConvert = isConvert, \u003C\u0024\u003EunitIds = unitIds, \u003C\u0024\u003EisConvert = isConvert, \u003C\u003Ef__this = this };
    }
  }
}
