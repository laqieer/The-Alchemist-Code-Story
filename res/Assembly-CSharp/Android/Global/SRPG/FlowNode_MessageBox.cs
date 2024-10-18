// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MessageBox
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(101, "ForceClosed", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(100, "Opened", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(11, "ForceClose", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(1, "Closed", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(10, "Open", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("UI/MessageBox", 32741)]
  public class FlowNode_MessageBox : FlowNode
  {
    public string Caption;
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
          this.winGO = UIUtility.SystemMessage(LocalizedText.Get(this.Caption), str, (UIUtility.DialogResultEvent) (go =>
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
          Win_Btn_Decide_Title_Flx btnDecideTitleFlx = !((UnityEngine.Object) this.winGO == (UnityEngine.Object) null) ? this.winGO.GetComponent<Win_Btn_Decide_Title_Flx>() : (Win_Btn_Decide_Title_Flx) null;
          this.winGO = (GameObject) null;
          if ((UnityEngine.Object) btnDecideTitleFlx != (UnityEngine.Object) null)
            btnDecideTitleFlx.BeginClose();
          this.ActivateOutputLinks(101);
          break;
      }
    }
  }
}
