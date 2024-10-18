// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_CopyClipBoard
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

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
      if ((UnityEngine.Object) target == (UnityEngine.Object) null)
        return false;
      return this.CopyFrom(target.text);
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
