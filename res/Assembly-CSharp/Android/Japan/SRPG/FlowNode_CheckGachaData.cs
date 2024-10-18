﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_CheckGachaData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace SRPG
{
  [FlowNode.NodeType("System/Check/CheckGachaData", 32741)]
  [FlowNode.Pin(0, "In", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "確認", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(10, "ダウンロード開始", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(11, "ダウンロード完了", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(100, "キャンセル", FlowNode.PinTypes.Output, 12)]
  [FlowNode.Pin(99, "エラー発生", FlowNode.PinTypes.Output, 99)]
  public class FlowNode_CheckGachaData : FlowNode
  {
    public List<string> DownloadUnits = new List<string>();
    public List<ArtifactParam> DownloadArtifacts = new List<ArtifactParam>();
    public const int PINID_IN = 0;
    private List<AssetList.Item> mQueue;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (!AssetManager.UseDLC || !GameUtility.Config_UseAssetBundles.Value)
      {
        this.ActivateOutputLinks(11);
      }
      else
      {
        if (this.enabled)
          return;
        GameManager instance = MonoSingleton<GameManager>.Instance;
        GachaParam[] gachas = instance.Gachas;
        for (int index1 = 0; index1 < gachas.Length; ++index1)
        {
          if (gachas[index1] != null)
          {
            if (gachas[index1].units != null && gachas[index1].units.Count > 0)
            {
              for (int index2 = 0; index2 < gachas[index1].units.Count; ++index2)
              {
                if (instance.Player.FindUnitDataByUnitID(gachas[index1].units[index2].iname) == null && this.DownloadUnits.IndexOf(gachas[index1].units[index2].iname) == -1)
                  this.DownloadUnits.Add(gachas[index1].units[index2].iname);
              }
            }
            if (gachas[index1].artifacts != null && gachas[index1].artifacts.Count > 0)
            {
              for (int index2 = 0; index2 < gachas[index1].artifacts.Count; ++index2)
              {
                ArtifactParam artifact = gachas[index1].artifacts[index2];
                if (artifact != null)
                {
                  string path = AssetPath.Artifacts(artifact);
                  if (!string.IsNullOrEmpty(path))
                  {
                    AssetList.Item itemByPath = AssetManager.AssetList.FindItemByPath(path);
                    if (itemByPath != null && !AssetManager.IsAssetInCache(itemByPath.IDStr))
                      this.DownloadArtifacts.Add(artifact);
                  }
                }
              }
            }
          }
        }
        this.enabled = true;
        this.StartCoroutine(this.AsyncWork(pinID == 1));
      }
    }

    [DebuggerHidden]
    private IEnumerator AsyncWork(bool confirm)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_CheckGachaData.\u003CAsyncWork\u003Ec__Iterator0() { confirm = confirm, \u0024this = this };
    }
  }
}
