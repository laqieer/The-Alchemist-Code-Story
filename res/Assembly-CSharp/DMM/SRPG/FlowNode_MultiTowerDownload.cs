// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MultiTowerDownload
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
  [FlowNode.NodeType("Download/MultiTower", 32741)]
  [FlowNode.Pin(0, "ダウンロード開始", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 10)]
  public class FlowNode_MultiTowerDownload : FlowNode
  {
    [SerializeField]
    private int DownloadAssetNums = 10;

    public int DownloadAssetNum => this.DownloadAssetNums;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      ((Behaviour) this).enabled = true;
      if (AssetManager.UseDLC && GameUtility.Config_UseAssetBundles.Value && AssetManager.AssetRevision > 0)
      {
        this.StartCoroutine(this.DownloadFloorParamAsync());
      }
      else
      {
        this.ActivateOutputLinks(1);
        ((Behaviour) this).enabled = false;
      }
    }

    [DebuggerHidden]
    private IEnumerator DownloadFloorParamAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_MultiTowerDownload.\u003CDownloadFloorParamAsync\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    public static void DownloadMapSets(List<MultiTowerFloorParam> floorParams)
    {
      if (floorParams == null)
        return;
      for (int index1 = 0; index1 < floorParams.Count; ++index1)
      {
        if (floorParams[index1].map != null)
        {
          for (int index2 = 0; index2 < floorParams[index1].map.Count; ++index2)
          {
            string mapSetName = floorParams[index1].map[index2].mapSetName;
            if (!string.IsNullOrEmpty(mapSetName))
              AssetManager.PrepareAssets(AssetPath.LocalMap(mapSetName));
          }
        }
      }
    }

    [DebuggerHidden]
    private IEnumerator RequestDownloadFloors(List<MultiTowerFloorParam> floorParams)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_MultiTowerDownload.\u003CRequestDownloadFloors\u003Ec__Iterator1()
      {
        floorParams = floorParams
      };
    }
  }
}
