// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_YesNoDialogAddMessage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("UI/YesNoDialogAddMessage", 32741)]
  [FlowNode.Pin(10, "Open", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Yes", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(2, "No", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(11, "ForceClose", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(100, "Opened", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "ForceClosed", FlowNode.PinTypes.Output, 101)]
  public class FlowNode_YesNoDialogAddMessage : FlowNode
  {
    public string Title;
    public string Text;
    public bool systemModal;
    public int systemModalPriority;
    public GameObject parent;
    public string parentName;
    public bool richTag;
    public bool unscaledTime;
    public string yesText;
    public string noText;
    private GameObject winGO;
    private const int STARTADDPIN = 1000;
    [HeaderBar("文章を追加する際に改行をいれるかどうか")]
    [SerializeField]
    private bool m_txtAddLine = true;
    [SerializeField]
    private string m_TxtSeparator;
    [SerializeField]
    private int m_Num;
    [SerializeField]
    [HideInInspector]
    private FlowNode.Pin[] m_Pins = new FlowNode.Pin[0];
    [SerializeField]
    [HideInInspector]
    private string[] addText;
    private string m_AddText = string.Empty;

    public override string[] GetInfoLines()
    {
      if (string.IsNullOrEmpty(this.Text))
        return base.GetInfoLines();
      return new string[1]{ "Text is " + this.Text };
    }

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
          string text = LocalizedText.Get(this.Text) + this.m_AddText;
          if (this.richTag)
            text = LocalizedText.ReplaceTag(text);
          string yesText = !string.IsNullOrEmpty(this.yesText) ? this.yesText : (string) null;
          string noText = !string.IsNullOrEmpty(this.noText) ? this.noText : (string) null;
          this.winGO = !string.IsNullOrEmpty(this.Title) ? UIUtility.ConfirmBoxTitle(LocalizedText.Get(this.Title), text, new UIUtility.DialogResultEvent(this.OnClickOK), new UIUtility.DialogResultEvent(this.OnClickCancel), this.parent, this.systemModal, this.systemModalPriority, yesText, noText) : UIUtility.ConfirmBox(text, new UIUtility.DialogResultEvent(this.OnClickOK), new UIUtility.DialogResultEvent(this.OnClickCancel), this.parent, this.systemModal, this.systemModalPriority, yesText, noText);
          if (Object.op_Inequality((Object) this.winGO, (Object) null) && this.unscaledTime)
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
          if (string.IsNullOrEmpty(this.Title))
          {
            Win_Btn_DecideCancel_FL_C component = !Object.op_Equality((Object) this.winGO, (Object) null) ? this.winGO.GetComponent<Win_Btn_DecideCancel_FL_C>() : (Win_Btn_DecideCancel_FL_C) null;
            this.winGO = (GameObject) null;
            if (Object.op_Inequality((Object) component, (Object) null))
              component.BeginClose();
          }
          else
          {
            Win_Btn_YN_Title_Flx component = !Object.op_Equality((Object) this.winGO, (Object) null) ? this.winGO.GetComponent<Win_Btn_YN_Title_Flx>() : (Win_Btn_YN_Title_Flx) null;
            this.winGO = (GameObject) null;
            if (Object.op_Inequality((Object) component, (Object) null))
              component.BeginClose();
          }
          this.ActivateOutputLinks(101);
          break;
        default:
          if (pinID < 1000 || pinID - 1000 >= this.addText.Length)
            break;
          this.AddTextCheck(this.addText[pinID - 1000]);
          break;
      }
    }

    private void AddTextCheck(string str)
    {
      string str1 = LocalizedText.Get(str);
      if (this.m_AddText.IndexOf(str1) != -1)
        return;
      if (!string.IsNullOrEmpty(this.m_AddText))
        this.m_AddText += LocalizedText.Get(this.m_TxtSeparator);
      this.m_AddText += str1;
      if (!this.m_txtAddLine)
        return;
      this.m_AddText += "\n";
    }

    private void OnClickOK(GameObject go)
    {
      this.m_AddText = string.Empty;
      if (Object.op_Equality((Object) this.winGO, (Object) null))
        return;
      this.winGO = (GameObject) null;
      this.ActivateOutputLinks(1);
    }

    private void OnClickCancel(GameObject go)
    {
      this.m_AddText = string.Empty;
      if (Object.op_Equality((Object) this.winGO, (Object) null))
        return;
      this.winGO = (GameObject) null;
      this.ActivateOutputLinks(2);
    }

    public override FlowNode.Pin[] GetDynamicPins() => this.m_Pins;
  }
}
