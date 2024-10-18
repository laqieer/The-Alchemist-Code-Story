// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_FgGWebView
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
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
          if (!Object.op_Inequality((Object) this.Target, (Object) null))
            break;
          ((Behaviour) this.mClientArea).enabled = true;
          this.StartCoroutine(this.OpenURL());
          break;
        case 2:
          if (!Object.op_Inequality((Object) this.Target, (Object) null))
            break;
          ((Behaviour) this.mClientArea).enabled = false;
          break;
      }
    }

    [DebuggerHidden]
    private IEnumerator OpenURL()
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      FlowNode_FgGWebView.\u003COpenURL\u003Ec__Iterator0 openUrlCIterator0 = new FlowNode_FgGWebView.\u003COpenURL\u003Ec__Iterator0();
      return (IEnumerator) openUrlCIterator0;
    }

    private Rect GetRect()
    {
      Vector3[] vector3Array = new Vector3[4];
      ((Graphic) this.mClientArea).rectTransform.GetWorldCorners(vector3Array);
      float num1 = 1f;
      float num2 = 1f;
      Rect rect;
      // ISSUE: explicit constructor call
      ((Rect) ref rect).\u002Ector(vector3Array[1].x * num1, ((float) Screen.height - vector3Array[1].y) * num2, (vector3Array[3].x - vector3Array[1].x) * num1, (vector3Array[1].y - vector3Array[3].y) * num2);
      return rect;
    }
  }
}
