// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_FgGBtn
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("FgGID/FgGBtn", 32741)]
  [FlowNode.Pin(10, "Input", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Output", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_FgGBtn : FlowNode
  {
    [FlowNode.ShowInInfo]
    public string ParameterName = "authStatus";
    [FlowNode.ShowInInfo]
    [FlowNode.DropTarget(typeof (GameObject), true)]
    public GameObject Target;
    public bool UpdateAnimator;

    public override string GetCaption()
    {
      return base.GetCaption() + ":" + this.ParameterName;
    }

    public override void OnActivate(int pinID)
    {
      Animator component = (!((UnityEngine.Object) this.Target != (UnityEngine.Object) null) ? this.gameObject : this.Target).GetComponent<Animator>();
      switch (MonoSingleton<GameManager>.Instance.AuthStatus)
      {
        case ReqFgGAuth.eAuthStatus.Disable:
          this.Target.SetActive(false);
          break;
        case ReqFgGAuth.eAuthStatus.NotSynchronized:
          if ((UnityEngine.Object) component != (UnityEngine.Object) null)
          {
            component.SetInteger(this.ParameterName, 2);
            if (this.UpdateAnimator)
            {
              component.Update(0.0f);
              break;
            }
            break;
          }
          break;
        case ReqFgGAuth.eAuthStatus.Synchronized:
          if ((UnityEngine.Object) component != (UnityEngine.Object) null)
          {
            component.SetInteger(this.ParameterName, 3);
            if (this.UpdateAnimator)
            {
              component.Update(0.0f);
              break;
            }
            break;
          }
          break;
        default:
          this.Target.SetActive(false);
          break;
      }
      this.ActivateOutputLinks(1);
    }
  }
}
