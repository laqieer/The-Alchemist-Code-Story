// Decompiled with JetBrains decompiler
// Type: SRPG.Flownode_GvGNowPeriodFirst
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("GvG/GvGNowPeriodFirst", 32741)]
  [FlowNode.Pin(1, "IsFirstTime", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "Yes", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "No", FlowNode.PinTypes.Output, 102)]
  public class Flownode_GvGNowPeriodFirst : FlowNode
  {
    private const int PIN_INPUT_ISFIRST = 1;
    private const int PIN_OUTPUT_FIRST = 101;
    private const int PIN_OUTPUT_NOTFIRST = 102;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      GvGPeriodParam gvGperiod = GvGPeriodParam.GetGvGPeriod();
      bool flag = false;
      if (gvGperiod != null)
      {
        if (PlayerPrefsUtility.HasKey(PlayerPrefsUtility.PREFS_GVG_PERIOD))
        {
          if (PlayerPrefsUtility.GetInt(PlayerPrefsUtility.PREFS_GVG_PERIOD) < gvGperiod.Id)
            flag = true;
        }
        else
          flag = true;
      }
      if (flag)
      {
        PlayerPrefsUtility.SetInt(PlayerPrefsUtility.PREFS_GVG_PERIOD, gvGperiod.Id, true);
        this.ActivateOutputLinks(101);
      }
      else
        this.ActivateOutputLinks(102);
    }
  }
}
