// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_GUIRanking
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.NodeType("UI/GUIRanking", 32741)]
  [FlowNode.Pin(200, "CreateQuest", FlowNode.PinTypes.Input, 200)]
  [FlowNode.Pin(201, "CreateArena", FlowNode.PinTypes.Input, 201)]
  [FlowNode.Pin(202, "CreateTowerMatch", FlowNode.PinTypes.Input, 202)]
  public class FlowNode_GUIRanking : FlowNode_GUI
  {
    private UsageRateRanking.ViewInfoType type;

    public override void OnActivate(int pinID)
    {
      if (pinID == 200)
        this.type = UsageRateRanking.ViewInfoType.Quest;
      if (pinID == 201)
        this.type = UsageRateRanking.ViewInfoType.Arena;
      if (pinID == 202)
        this.type = UsageRateRanking.ViewInfoType.TowerMatch;
      if (pinID == 200 || pinID == 201 || pinID == 202)
        pinID = 100;
      base.OnActivate(pinID);
    }

    protected override void OnInstanceCreate()
    {
      base.OnInstanceCreate();
      UsageRateRanking componentInChildren = this.Instance.GetComponentInChildren<UsageRateRanking>();
      if ((UnityEngine.Object) componentInChildren == (UnityEngine.Object) null)
        return;
      componentInChildren.OnChangedToggle(this.type);
    }
  }
}
