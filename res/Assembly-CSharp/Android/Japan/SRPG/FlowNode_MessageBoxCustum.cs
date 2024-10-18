// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MessageBoxCustum
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

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
            if ((UnityEngine.Object) this.parent == (UnityEngine.Object) null)
              DebugUtility.LogWarning("can not found gameObject:" + this.parentName);
          }
          string str = LocalizedText.Get(this.Text);
          if (this.richTag)
            str = LocalizedText.ReplaceTag(str);
          this.winGO = this.CreatePrefab(this.ResourcePath, LocalizedText.Get(this.Caption), str, (UIUtility.DialogResultEvent) (go =>
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

    private GameObject CreatePrefab(string resource_path, string title, string msg, UIUtility.DialogResultEvent ok_event_listener, GameObject go_parent, bool system_modal, int system_modal_priority)
    {
      Canvas canvas = UIUtility.PushCanvas(system_modal, system_modal_priority);
      if ((UnityEngine.Object) go_parent != (UnityEngine.Object) null)
        canvas.transform.SetParent(go_parent.transform);
      GameObject original = AssetManager.Load<GameObject>(resource_path);
      if (!(bool) ((UnityEngine.Object) original))
      {
        Debug.LogError((object) ("FlowNode_MessageBoxCustum/Load failed. '" + resource_path + "'"));
        return (GameObject) null;
      }
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original);
      if (!(bool) ((UnityEngine.Object) gameObject))
      {
        Debug.LogError((object) ("FlowNode_MessageBoxCustum/Instantiate failed. '" + resource_path + "'"));
        return (GameObject) null;
      }
      Win_Btn_Decide_Title_Flx component = gameObject.GetComponent<Win_Btn_Decide_Title_Flx>();
      if (!(bool) ((UnityEngine.Object) component))
      {
        Debug.LogError((object) "FlowNode_MessageBoxCustum/Component not attached. 'Win_Btn_Decide_Title_Flx'");
        return (GameObject) null;
      }
      component.transform.SetParent(canvas.transform, false);
      component.Text_Title.text = title;
      component.Text_Message.text = msg;
      component.OnClickYes = ok_event_listener;
      return component.gameObject;
    }
  }
}
