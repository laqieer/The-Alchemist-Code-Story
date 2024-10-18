// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_DestroyAsset
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
