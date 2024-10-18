// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MessageBoxNoTitle
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

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
            if ((UnityEngine.Object) this.parent == (UnityEngine.Object) null)
              DebugUtility.LogWarning("can not found gameObject:" + this.parentName);
          }
          string str = LocalizedText.Get(this.Text);
          if (this.richTag)
            str = LocalizedText.ReplaceTag(str);
          this.winGO = UIUtility.SystemMessage(str, (UIUtility.DialogResultEvent) (go =>
          {
            if (!((UnityEngine.Object) this.winGO != (UnityEngine.Object) null))
              return;
            this.winGO = (GameObject) null;
            this.ActivateOutputLinks(1);
          }), this.parent, this.systemModal, this.systemModalPriority);
          if ((bool) ((UnityEngine.Object) this.winGO) && this.unscaledTime)
          {
            Animator component = this.winGO.GetComponent<Animator>();
            if ((UnityEngine.Object) component != (UnityEngine.Object) null)
              component.updateMode = AnimatorUpdateMode.UnscaledTime;
          }
          this.ActivateOutputLinks(100);
          break;
        case 11:
          if ((UnityEngine.Object) this.winGO == (UnityEngine.Object) null)
            break;
          Win_Btn_Decide_Flx winBtnDecideFlx = !((UnityEngine.Object) this.winGO == (UnityEngine.Object) null) ? this.winGO.GetComponent<Win_Btn_Decide_Flx>() : (Win_Btn_Decide_Flx) null;
          this.winGO = (GameObject) null;
          if ((UnityEngine.Object) winBtnDecideFlx != (UnityEngine.Object) null)
            winBtnDecideFlx.BeginClose();
          this.ActivateOutputLinks(101);
          break;
      }
    }
  }
}
