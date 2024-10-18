// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqBtlComCont
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  public class FlowNode_ReqBtlComCont : FlowNode_Network
  {
    private FlowNode_ReqBtlComCont.OnSuccesDelegate mOnSuccessDelegate;

    public FlowNode_ReqBtlComCont.OnSuccesDelegate OnSuccessListeners
    {
      set
      {
        this.mOnSuccessDelegate = value;
      }
    }

    public override void OnActivate(int pinID)
    {
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.ContinueCostShort:
            this.OnBack();
            break;
          case Network.EErrCode.CantContinue:
            this.OnFailed();
            break;
          default:
            this.OnRetry();
            break;
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<BattleCore.Json_Battle> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<BattleCore.Json_Battle>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          this.OnRetry();
        }
        else
        {
          Network.RemoveAPI();
          this.mOnSuccessDelegate(jsonObject.body);
        }
      }
    }

    public delegate void OnSuccesDelegate(BattleCore.Json_Battle response);
  }
}
