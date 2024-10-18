// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_GUIRanking
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.NodeType("UI/GUIRanking", 32741)]
  [FlowNode.Pin(200, "CreateQuest", FlowNode.PinTypes.Input, 200)]
  [FlowNode.Pin(202, "CreateTowerMatch", FlowNode.PinTypes.Input, 202)]
  [FlowNode.Pin(201, "CreateArena", FlowNode.PinTypes.Input, 201)]
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
