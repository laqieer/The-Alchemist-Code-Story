// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_DestroyAsset
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/DestroyAsset", 32741)]
  [FlowNode.Pin(0, "Start", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_DestroyAsset : FlowNode
  {
    public AssetBundleFlags flags;

    private void Start()
    {
    }

    public override void OnActivate(int pinID)
    {
      if (pinID == 0)
        AssetDownloader.DestroyAssetStart(this.flags);
      this.ActivateOutputLinks(1);
    }
  }
}
