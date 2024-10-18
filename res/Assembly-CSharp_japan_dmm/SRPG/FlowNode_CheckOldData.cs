// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_CheckOldData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Diagnostics;
using System.IO;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/Check/CheckOldData", 32741)]
  [FlowNode.Pin(100, "Check", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(101, "Delete", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(200, "Exist", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(201, "Not Exist", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(202, "Finish", FlowNode.PinTypes.Output, 0)]
  public class FlowNode_CheckOldData : FlowNode
  {
    private readonly int PINID_CHECK = 100;
    private readonly int PINID_DELETE = 101;
    private readonly int PINID_EXIST = 200;
    private readonly int PINID_NOT_EXIST = 201;
    private readonly int PINID_FINISH = 202;

    public override void OnActivate(int pinID)
    {
      if (!AssetManager.UseDLC || !GameUtility.Config_UseAssetBundles.Value)
        this.ActivateOutputLinks(this.PINID_NOT_EXIST);
      else if (pinID == this.PINID_CHECK)
      {
        if (this.IsExist())
          this.ActivateOutputLinks(this.PINID_EXIST);
        else
          this.ActivateOutputLinks(this.PINID_NOT_EXIST);
      }
      else
      {
        if (pinID != this.PINID_DELETE)
          return;
        CriticalSection.Enter();
        this.StartCoroutine(this.DeleteOldData());
      }
    }

    [DebuggerHidden]
    private IEnumerator DeleteOldData()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_CheckOldData.\u003CDeleteOldData\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    public static void DeleteThread(object param)
    {
      string path = param as string;
      if (!Directory.Exists(path))
        return;
      Directory.Delete(path, true);
    }

    private bool IsExist() => Directory.Exists(AssetDownloader.OldDownloadPath);
  }
}
