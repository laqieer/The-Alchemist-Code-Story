// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_FgGWebView
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.NodeType("FgGID/FgGWebView", 32741)]
  [FlowNode.Pin(1, "Enable", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(2, "Disable", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(3, "Finished", FlowNode.PinTypes.Output, 100)]
  public class FlowNode_FgGWebView : FlowNode
  {
    private const int PIN_ID_ENABLE = 1;
    private const int PIN_ID_DISABLE = 2;
    private const int PIN_ID_FINISHED = 3;
    [FlowNode.ShowInInfo]
    [FlowNode.DropTarget(typeof (GameObject), true)]
    public GameObject Target;
    public string URL;
    public RawImage mClientArea;

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 1:
          if (!((UnityEngine.Object) this.Target != (UnityEngine.Object) null))
            break;
          this.mClientArea.enabled = true;
          this.OpenURL();
          break;
        case 2:
          if (!((UnityEngine.Object) this.Target != (UnityEngine.Object) null))
            break;
          this.mClientArea.enabled = false;
          break;
      }
    }

    private void OpenURL()
    {
    }
  }
}
