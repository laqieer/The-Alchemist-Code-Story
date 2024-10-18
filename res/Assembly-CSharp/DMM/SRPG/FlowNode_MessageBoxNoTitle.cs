// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MessageBoxNoTitle
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("UI/MessageBoxNoTitle", 32741)]
  [FlowNode.Pin(10, "Open", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Closed", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(11, "ForceClose", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(100, "Opened", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "ForceClosed", FlowNode.PinTypes.Output, 101)]
  public class FlowNode_MessageBoxNoTitle : FlowNode
  {
    public string Text;
    public bool systemModal;
    public int systemModalPriority;
    public GameObject parent;
    public string parentName;
    public bool unscaledTime;
    public bool richTag;
    private GameObject winGO;

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 10:
          if (!string.IsNullOrEmpty(this.parentName))
          {
            this.parent = GameObject.Find(this.parentName);
            if (Object.op_Equality((Object) this.parent, (Object) null))
              DebugUtility.LogWarning("can not found gameObject:" + this.parentName);
          }
          string str = LocalizedText.Get(this.Text);
          if (this.richTag)
            str = LocalizedText.ReplaceTag(str);
          this.winGO = UIUtility.SystemMessage(str, (UIUtility.DialogResultEvent) (go =>
          {
            if (!Object.op_Inequality((Object) this.winGO, (Object) null))
              return;
            this.winGO = (GameObject) null;
            this.ActivateOutputLinks(1);
          }), this.parent, this.systemModal, this.systemModalPriority);
          if (Object.op_Implicit((Object) this.winGO) && this.unscaledTime)
          {
            Animator component = this.winGO.GetComponent<Animator>();
            if (Object.op_Inequality((Object) component, (Object) null))
              component.updateMode = (AnimatorUpdateMode) 2;
          }
          this.ActivateOutputLinks(100);
          break;
        case 11:
          if (Object.op_Equality((Object) this.winGO, (Object) null))
            break;
          Win_Btn_Decide_Flx component1 = !Object.op_Equality((Object) this.winGO, (Object) null) ? this.winGO.GetComponent<Win_Btn_Decide_Flx>() : (Win_Btn_Decide_Flx) null;
          this.winGO = (GameObject) null;
          if (Object.op_Inequality((Object) component1, (Object) null))
            component1.BeginClose();
          this.ActivateOutputLinks(101);
          break;
      }
    }
  }
}
