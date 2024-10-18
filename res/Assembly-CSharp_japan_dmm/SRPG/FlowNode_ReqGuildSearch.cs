// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqGuildSearch
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Guild/ReqGuildSearch", 32741)]
  [FlowNode.Pin(1, "ギルド検索", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "ギルド検索完了", FlowNode.PinTypes.Output, 101)]
  public class FlowNode_ReqGuildSearch : FlowNode_Network
  {
    private const int PIN_INPUT_START_GUILD_SEARCH = 1;
    private const int PIN_OUTPUT_END_GUILD_SEARCH = 101;

    public override void OnActivate(int pinID)
    {
      if (pinID == 1)
        this.ExecRequest((WebAPI) new ReqGuildAutoJoin(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
      ((Behaviour) this).enabled = true;
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        int errCode = (int) Network.ErrCode;
        this.OnRetry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<FlowNode_ReqGuildSearch.Json_ReqGuildSearch> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_ReqGuildSearch.Json_ReqGuildSearch>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        try
        {
          if (jsonObject.body != null)
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.guild);
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          return;
        }
        this.ActivateOutputLinks(101);
        ((Behaviour) this).enabled = false;
      }
    }

    public class Json_ReqGuildSearch
    {
      public JSON_Guild guild;
    }
  }
}
