// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SupportSet
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;

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
        DataSource.Bind<UnitData>(this.UnitParent, MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID((long) GlobalVars.SelectedSupportUnitUniqueID), false);
        GameParameter.UpdateAll(this.gameObject);
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
