// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_GetBoolPlayerPrefs
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.NodeType("System/PlayerPrefs/GetBoolPlayerPrefs", 32741)]
  [FlowNode.Pin(0, "False", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(1, "True", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(2, "Check", FlowNode.PinTypes.Input, 2)]
  public class FlowNode_GetBoolPlayerPrefs : FlowNode
  {
    public string Name;
    private const int CHECK = 2;
    private const int GET_FALSE = 0;
    private const int GET_TRUE = 1;

    public override void OnActivate(int pinID)
    {
      if (pinID == 2)
        this.ActivateOutputLinks(PlayerPrefsUtility.GetInt(this.Name, 0) != 1 ? 0 : 1);
      else
        this.ActivateOutputLinks(0);
    }
  }
}
