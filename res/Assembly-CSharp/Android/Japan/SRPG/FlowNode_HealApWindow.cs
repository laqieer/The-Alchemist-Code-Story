// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_HealApWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.NodeType("UI/HealApWindow", 32741)]
  [FlowNode.Pin(103, "Home", FlowNode.PinTypes.Input, 103)]
  [FlowNode.Pin(104, "Quest", FlowNode.PinTypes.Input, 104)]
  [FlowNode.Pin(105, "HealCoin", FlowNode.PinTypes.Output, 105)]
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
      if (MonoSingleton<GameManager>.Instance.Player.IsHaveHealAPItems())
      {
        base.OnCreatePinActive();
        this.mHealAp = this.Instance.GetComponentInChildren<HealAp>();
        this.mHealAp.Refresh(this.mIsQuest, this);
      }
      else
        this.HealCoin();
    }

    public void HealCoin()
    {
      this.ActivateOutputLinks(105);
    }
  }
}
