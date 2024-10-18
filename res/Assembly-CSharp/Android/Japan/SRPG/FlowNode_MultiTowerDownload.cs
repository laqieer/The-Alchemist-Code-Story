// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MultiTowerDownload
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("Download/MultiTower", 32741)]
  [FlowNode.Pin(0, "ダウンロード開始", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 10)]
  public class FlowNode_MultiTowerDownload : FlowNode
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
      if (AssetManager.UseDLC && GameUtility.Config_UseAssetBundles.Value && AssetManager.AssetRevision > 0)
      {
        this.StartCoroutine(this.DownloadFloorParamAsync());
      }
      else
      {
        this.ActivateOutputLinks(1);
        this.enabled = false;
      }
    }

    [DebuggerHidden]
    private IEnumerator DownloadFloorParamAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_MultiTowerDownload.\u003CDownloadFloorParamAsync\u003Ec__Iterator0() { \u0024this = this };
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
      return (IEnumerator) new FlowNode_MultiTowerDownload.\u003CRequestDownloadFloors\u003Ec__Iterator1() { floorParams = floorParams };
    }
  }
}
