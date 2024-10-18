// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_DownloadTowerMapSets
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(0, "ダウンロード開始", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("System/DownloadTowerMapSets", 32741)]
  public class FlowNode_DownloadTowerMapSets : FlowNode
  {
    [SerializeField]
    private int DownloadAssetNums = 10;

    public int DownloadAssetNum
    {
      get
      {
        return this.DownloadAssetNums;
      }
    }

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      this.enabled = true;
      if (Network.Mode == Network.EConnectMode.Online)
      {
        this.StartCoroutine(this.DownloadFloorParams());
      }
      else
      {
        this.ActivateOutputLinks(1);
        this.enabled = false;
      }
    }

    [DebuggerHidden]
    private IEnumerator DownloadFloorParams()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_DownloadTowerMapSets.\u003CDownloadFloorParams\u003Ec__IteratorC9() { \u003C\u003Ef__this = this };
    }

    [DebuggerHidden]
    private IEnumerator DownloadFloorParamAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_DownloadTowerMapSets.\u003CDownloadFloorParamAsync\u003Ec__IteratorCA() { \u003C\u003Ef__this = this };
    }

    public static void DownloadMapSets(List<TowerFloorParam> floorParams)
    {
      if (floorParams == null)
        return;
      for (int index = 0; index < floorParams.Count; ++index)
      {
        TowerFloorParam floorParam = floorParams[index];
        if (floorParam.map.Count > 0)
        {
          string mapSetName = floorParam.map[0].mapSetName;
          if (!string.IsNullOrEmpty(mapSetName))
            AssetManager.PrepareAssets(AssetPath.LocalMap(mapSetName));
        }
      }
    }
  }
}
