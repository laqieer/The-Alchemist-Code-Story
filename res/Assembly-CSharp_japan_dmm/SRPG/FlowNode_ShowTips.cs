// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ShowTips
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
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
