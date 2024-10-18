// Decompiled with JetBrains decompiler
// Type: SRPG.Flownode_GvGUsedItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.Events;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("GvG/GvGUsedItem", 32741)]
  [FlowNode.Pin(1, "Start Check", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "Ok", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "Cancel", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(103, "Used Item", FlowNode.PinTypes.Output, 103)]
  public class Flownode_GvGUsedItem : FlowNode
  {
    [SerializeField]
    private Win_Btn_YN_FL_C YesNoDialog;
    [SerializeField]
    private GameObject Parent;
    private const int PIN_INPUT_CHECK = 1;
    private const int PIN_OUTPUT_OK = 101;
    private const int PIN_OUTPUT_CANCEL = 102;
    private const int PIN_OUTPUT_NOITEM = 103;
    private GameObject dialogObject;

    protected override void OnDestroy()
    {
      if (Object.op_Inequality((Object) this.dialogObject, (Object) null))
        Object.Destroy((Object) this.dialogObject);
      base.OnDestroy();
    }

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      GvGManager instance = GvGManager.Instance;
      if (Object.op_Equality((Object) instance, (Object) null) || Object.op_Equality((Object) this.YesNoDialog, (Object) null) || Object.op_Equality((Object) this.Parent, (Object) null) || string.IsNullOrEmpty(instance.mUsedMessage))
      {
        this.ActivateOutputLinks(103);
      }
      else
      {
        string mUsedMessage = instance.mUsedMessage;
        instance.mUsedMessage = string.Empty;
        this.dialogObject = this.SetConfirmBox(mUsedMessage, this.Parent, (UIUtility.DialogResultEvent) (ok => this.ActivateOutputLinks(101)), (UIUtility.DialogResultEvent) (cancel => this.ActivateOutputLinks(102)));
      }
    }

    private GameObject SetConfirmBox(
      string text,
      GameObject parent,
      UIUtility.DialogResultEvent okEventListener,
      UIUtility.DialogResultEvent cancelEventListener)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      Flownode_GvGUsedItem.\u003CSetConfirmBox\u003Ec__AnonStorey0 confirmBoxCAnonStorey0 = new Flownode_GvGUsedItem.\u003CSetConfirmBox\u003Ec__AnonStorey0();
      // ISSUE: reference to a compiler-generated field
      confirmBoxCAnonStorey0.okEventListener = okEventListener;
      // ISSUE: reference to a compiler-generated field
      confirmBoxCAnonStorey0.cancelEventListener = cancelEventListener;
      // ISSUE: reference to a compiler-generated field
      confirmBoxCAnonStorey0.dialog = Object.Instantiate<Win_Btn_YN_FL_C>(this.YesNoDialog);
      // ISSUE: reference to a compiler-generated field
      if (Object.op_Equality((Object) confirmBoxCAnonStorey0.dialog, (Object) null))
        return (GameObject) null;
      if (Object.op_Inequality((Object) parent, (Object) null))
      {
        // ISSUE: reference to a compiler-generated field
        ((Component) confirmBoxCAnonStorey0.dialog).transform.SetParent(parent.transform, false);
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      ((UnityEvent) confirmBoxCAnonStorey0.dialog.Btn_Yes.onClick).AddListener(new UnityAction((object) confirmBoxCAnonStorey0, __methodptr(\u003C\u003Em__0)));
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      ((UnityEvent) confirmBoxCAnonStorey0.dialog.Btn_No.onClick).AddListener(new UnityAction((object) confirmBoxCAnonStorey0, __methodptr(\u003C\u003Em__1)));
      // ISSUE: reference to a compiler-generated field
      confirmBoxCAnonStorey0.dialog.Text_Message.text = text;
      // ISSUE: reference to a compiler-generated field
      return ((Component) confirmBoxCAnonStorey0.dialog).gameObject;
    }
  }
}
