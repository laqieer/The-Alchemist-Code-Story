// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_HealApWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.Pin(105, "HealCoin", FlowNode.PinTypes.Output, 105)]
  [FlowNode.Pin(103, "Home", FlowNode.PinTypes.Input, 103)]
  [FlowNode.NodeType("UI/HealApWindow", 32741)]
  [FlowNode.Pin(104, "Quest", FlowNode.PinTypes.Input, 104)]
  public class FlowNode_HealApWindow : FlowNode_GUI
  {
    private bool mIsQuest;
    private HealAp mHealAp;

    public override void OnActivate(int pinID)
    {
      if (pinID == 103 || pinID == 104)
      {
        this.mIsQuest = pinID == 104;
        pinID = 100;
      }
      base.OnActivate(pinID);
    }

    protected override void OnCreatePinActive()
    {
      base.OnCreatePinActive();
      this.mHealAp = this.Instance.GetComponentInChildren<HealAp>();
      this.mHealAp.Refresh(this.mIsQuest, this);
    }

    public void HealCoin()
    {
      this.ActivateOutputLinks(105);
    }
  }
}
