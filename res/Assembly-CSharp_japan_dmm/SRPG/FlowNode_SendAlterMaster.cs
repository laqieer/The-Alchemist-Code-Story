// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SendAlterMaster
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Master/SendAlterCheck", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(11, "Failed", FlowNode.PinTypes.Output, 11)]
  public class FlowNode_SendAlterMaster : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (!AssetManager.UseDLC)
      {
        this.Success();
      }
      else
      {
        string digestHash = MonoSingleton<GameManager>.GetInstanceDirect().DigestHash;
        string alterCheckHash = MonoSingleton<GameManager>.GetInstanceDirect().AlterCheckHash;
        if (!string.IsNullOrEmpty(digestHash) && !string.IsNullOrEmpty(alterCheckHash))
        {
          this.ExecRequest((WebAPI) new ReqSendAlterData(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          ((Behaviour) this).enabled = true;
        }
        else
          this.Success();
      }
    }

    private void Success()
    {
      MonoSingleton<GameManager>.GetInstanceDirect().AlterCheckHash = (string) null;
      MonoSingleton<GameManager>.GetInstanceDirect().DigestHash = (string) null;
      MonoSingleton<GameManager>.GetInstanceDirect().PrevCheckHash = (string) null;
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(10);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        int errCode = (int) Network.ErrCode;
      }
      Network.RemoveAPI();
      PlayerPrefsUtility.SetString(PlayerPrefsUtility.ALTER_PREV_CHECK_HASH, MonoSingleton<GameManager>.Instance.AlterCheckHash);
      this.Success();
    }
  }
}
