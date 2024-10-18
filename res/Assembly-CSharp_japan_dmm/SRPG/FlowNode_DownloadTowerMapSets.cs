// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_DownloadTowerMapSets
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Tower/DownloadTowerMapSets", 32741)]
  [FlowNode.Pin(0, "ダウンロード開始", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 10)]
  public class FlowNode_DownloadTowerMapSets : FlowNode
  {
    [SerializeField]
    private int DownloadAssetNums = 10;

    public int DownloadAssetNum => this.DownloadAssetNums;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      ((Behaviour) this).enabled = true;
      if (Network.Mode == Network.EConnectMode.Online)
      {
        this.StartCoroutine(this.DownloadFloorParams());
      }
      else
      {
        this.ActivateOutputLinks(1);
        ((Behaviour) this).enabled = false;
      }
    }

    [DebuggerHidden]
    private IEnumerator DownloadFloorParams()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_DownloadTowerMapSets.\u003CDownloadFloorParams\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    [DebuggerHidden]
    private IEnumerator DownloadFloorParamAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_DownloadTowerMapSets.\u003CDownloadFloorParamAsync\u003Ec__Iterator1()
      {
        \u0024this = this
      };
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
