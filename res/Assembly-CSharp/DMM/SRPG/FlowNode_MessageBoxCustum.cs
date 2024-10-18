// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MessageBoxCustum
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("UI/MessageBoxCustum", 32741)]
  [FlowNode.Pin(10, "Open", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Closed", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(11, "ForceClose", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(100, "Opened", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "ForceClosed", FlowNode.PinTypes.Output, 101)]
  public class FlowNode_MessageBoxCustum : FlowNode
  {
    [StringIsResourcePath(typeof (GameObject))]
    public string ResourcePath;
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
            if (Object.op_Equality((Object) this.parent, (Object) null))
              DebugUtility.LogWarning("can not found gameObject:" + this.parentName);
          }
          string str = LocalizedText.Get(this.Text);
          if (this.richTag)
            str = LocalizedText.ReplaceTag(str);
          this.winGO = this.CreatePrefab(this.ResourcePath, LocalizedText.Get(this.Caption), str, (UIUtility.DialogResultEvent) (go =>
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
          Win_Btn_Decide_Title_Flx component1 = !Object.op_Equality((Object) this.winGO, (Object) null) ? this.winGO.GetComponent<Win_Btn_Decide_Title_Flx>() : (Win_Btn_Decide_Title_Flx) null;
          this.winGO = (GameObject) null;
          if (Object.op_Inequality((Object) component1, (Object) null))
            component1.BeginClose();
          this.ActivateOutputLinks(101);
          break;
      }
    }

    private GameObject CreatePrefab(
      string resource_path,
      string title,
      string msg,
      UIUtility.DialogResultEvent ok_event_listener,
      GameObject go_parent,
      bool system_modal,
      int system_modal_priority)
    {
      Canvas canvas = UIUtility.PushCanvas(system_modal, system_modal_priority);
      if (Object.op_Inequality((Object) go_parent, (Object) null))
        ((Component) canvas).transform.SetParent(go_parent.transform);
      GameObject gameObject1 = AssetManager.Load<GameObject>(resource_path);
      if (!Object.op_Implicit((Object) gameObject1))
      {
        Debug.LogError((object) ("FlowNode_MessageBoxCustum/Load failed. '" + resource_path + "'"));
        return (GameObject) null;
      }
      GameObject gameObject2 = Object.Instantiate<GameObject>(gameObject1);
      if (!Object.op_Implicit((Object) gameObject2))
      {
        Debug.LogError((object) ("FlowNode_MessageBoxCustum/Instantiate failed. '" + resource_path + "'"));
        return (GameObject) null;
      }
      Win_Btn_Decide_Title_Flx component = gameObject2.GetComponent<Win_Btn_Decide_Title_Flx>();
      if (!Object.op_Implicit((Object) component))
      {
        Debug.LogError((object) "FlowNode_MessageBoxCustum/Component not attached. 'Win_Btn_Decide_Title_Flx'");
        return (GameObject) null;
      }
      ((Component) component).transform.SetParent(((Component) canvas).transform, false);
      component.Text_Title.text = title;
      component.Text_Message.text = msg;
      component.OnClickYes = ok_event_listener;
      return ((Component) component).gameObject;
    }
  }
}
