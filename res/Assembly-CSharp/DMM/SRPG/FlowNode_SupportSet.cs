// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SupportSet
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/Player/SupportSet", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_SupportSet : FlowNode_Network
  {
    public GameObject UnitParent;

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        int errCode = (int) Network.ErrCode;
        this.OnFailed();
      }
      else
      {
        DataSource.Bind<UnitData>(this.UnitParent, MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID((long) GlobalVars.SelectedSupportUnitUniqueID));
        GameParameter.UpdateAll(((Component) this).gameObject);
        Network.RemoveAPI();
        this.ActivateOutputLinks(1);
      }
    }

    public override void OnActivate(int pinID)
    {
      this.ExecRequest((WebAPI) new ReqSetSupport(MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID((long) GlobalVars.SelectedSupportUnitUniqueID).UniqueID, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
    }

    private void OnUnitSelect(long uniqueID)
    {
    }
  }
}
