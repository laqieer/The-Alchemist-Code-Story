// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_DestroyAsset
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.Pin(0, "Start", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("System/DestroyAsset", 32741)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_DestroyAsset : FlowNode
  {
    public AssetBundleFlags flags;

    public override void OnActivate(int pinID)
    {
      if (pinID == 0)
        AssetDownloader.DestroyAssetStart(this.flags);
      this.ActivateOutputLinks(1);
    }
  }
}
