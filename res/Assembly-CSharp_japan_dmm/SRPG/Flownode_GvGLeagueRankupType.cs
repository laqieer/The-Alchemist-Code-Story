// Decompiled with JetBrains decompiler
// Type: SRPG.Flownode_GvGLeagueRankupType
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("GvG/GvGLeagueRankupType", 32741)]
  [FlowNode.Pin(1, "Start", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "Change", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(103, "No Change", FlowNode.PinTypes.Output, 103)]
  public class Flownode_GvGLeagueRankupType : FlowNode
  {
    private const int PIN_INPUT_START = 1;
    private const int PIN_OUTPUT_CHANGE = 101;
    private const int PIN_OUTPUT_NOCHANGE = 103;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      GvGManager instance = GvGManager.Instance;
      if (!this.IsFirstTimeEffect() || Object.op_Equality((Object) instance, (Object) null) || instance.ResultLeague == null)
        this.ActivateOutputLinks(103);
      else if (instance.ResultLeague.From.Id == instance.ResultLeague.To.Id && instance.ResultLeague.From.Rate == instance.ResultLeague.To.Rate)
      {
        this.ActivateOutputLinks(103);
      }
      else
      {
        this.ActivateOutputLinks(101);
        GvGPeriodParam gvGperiod = GvGPeriodParam.GetGvGPeriod();
        if (gvGperiod == null)
          return;
        PlayerPrefsUtility.SetInt(PlayerPrefsUtility.PREFS_GVG_PERIOD_LEAGUE, gvGperiod.Id, true);
      }
    }

    private bool IsFirstTimeEffect()
    {
      bool flag = false;
      GvGPeriodParam gvGperiod = GvGPeriodParam.GetGvGPeriod();
      if (gvGperiod != null)
      {
        if (PlayerPrefsUtility.HasKey(PlayerPrefsUtility.PREFS_GVG_PERIOD_LEAGUE))
        {
          if (PlayerPrefsUtility.GetInt(PlayerPrefsUtility.PREFS_GVG_PERIOD_LEAGUE) != gvGperiod.Id)
            flag = true;
        }
        else
          flag = true;
      }
      return flag;
    }
  }
}
