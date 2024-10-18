// Decompiled with JetBrains decompiler
// Type: SRPG.Flownode_RaidCheckRescueWaitSeconds
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.NodeType("Raid/CheckRescueWaitSeconds", 32741)]
  [FlowNode.Pin(1, "Check", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "CanReload", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(102, "UseCache", FlowNode.PinTypes.Output, 3)]
  public class Flownode_RaidCheckRescueWaitSeconds : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      RaidManager instance = RaidManager.Instance;
      if ((UnityEngine.Object) instance == (UnityEngine.Object) null)
        this.ActivateOutputLinks(101);
      else if (instance.RaidRescueMemberList == null)
        this.ActivateOutputLinks(101);
      else
        this.ActivateOutputLinks(102);
    }
  }
}
