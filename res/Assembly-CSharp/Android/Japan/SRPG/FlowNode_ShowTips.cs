// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ShowTips
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("Tips/ShowTips", 32741)]
  public class FlowNode_ShowTips : FlowNode_GUI
  {
    private const int PIN_ID_IN = 1;
    [SerializeField]
    private string Tips;

    protected override void OnCreatePinActive()
    {
      GlobalVars.SelectTips = this.Tips;
      base.OnCreatePinActive();
    }
  }
}
