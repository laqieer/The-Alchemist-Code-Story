// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_CopyClipBoard
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/ClipBoard/CopyClipBoard", 32741)]
  [FlowNode.Pin(0, "コピー", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "成功", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(2, "失敗", FlowNode.PinTypes.Output, 2)]
  public class FlowNode_CopyClipBoard : FlowNode
  {
    [SerializeField]
    private Text Target;
    public string LocalizeText;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (string.IsNullOrEmpty(this.LocalizeText))
      {
        if (this.CopyFrom(this.Target))
          this.ActivateOutputLinks(1);
        else
          this.ActivateOutputLinks(2);
      }
      else if (this.CopyFrom(LocalizedText.Get(this.LocalizeText)))
        this.ActivateOutputLinks(1);
      else
        this.ActivateOutputLinks(2);
    }

    private bool CopyFrom(Text target)
    {
      return !Object.op_Equality((Object) target, (Object) null) && this.CopyFrom(target.text);
    }

    private bool CopyFrom(string text)
    {
      if (string.IsNullOrEmpty(text))
        return false;
      text = text.Replace("<br>", "\n");
      GUIUtility.systemCopyBuffer = text;
      return true;
    }
  }
}
