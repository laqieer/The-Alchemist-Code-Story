// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqGenesisRaidboss
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;

namespace SRPG
{
  [FlowNode.NodeType("Genesis/Req/Raidboss", 32741)]
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "Success", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "OutOfPeriod", FlowNode.PinTypes.Output, 102)]
  public class FlowNode_ReqGenesisRaidboss : FlowNode_Network
  {
    protected const int PIN_IN_REQUEST = 1;
    protected const int PIN_OUT_SUCCESS = 101;
    protected const int PIN_OUT_OUT_OF_PERIOD = 102;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      this.enabled = true;
      GenesisChapterManager instance = GenesisChapterManager.Instance;
      this.ExecRequest((WebAPI) new ReqGenesisRaidboss(instance.CurrentChapterParam.AreaId, instance.BossDifficulty, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
    }

    private void Success()
    {
      this.enabled = false;
      this.ActivateOutputLinks(101);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        if (Network.ErrCode == Network.EErrCode.Genesis_OutOfPeriod)
        {
          Network.RemoveAPI();
          Network.ResetError();
          this.ActivateOutputLinks(102);
          this.enabled = false;
        }
        else
          this.OnRetry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<ReqGenesisRaidboss.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqGenesisRaidboss.Response>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          this.OnRetry();
        }
        else
        {
          try
          {
            GenesisBossInfo.Instance.SetBossHP(jsonObject.body.hp);
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
            this.OnRetry();
            return;
          }
          Network.RemoveAPI();
          this.Success();
        }
      }
    }
  }
}
