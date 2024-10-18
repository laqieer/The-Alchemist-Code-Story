// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqGuildInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Guild/ReqGuildInfo", 32741)]
  [FlowNode.Pin(1, "ギルド情報取得", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "ギルド情報取得完了", FlowNode.PinTypes.Output, 101)]
  public class FlowNode_ReqGuildInfo : FlowNode_Network
  {
    private const int PIN_INPUT_START_GUILD_INFO = 1;
    private const int PIN_OUTPUT_END_GUILD_INFO = 101;
    private long mGuildId;
    private GuildData mGuildData;

    public GuildData GuildData => this.mGuildData;

    public void SetParam(long guild_id) => this.mGuildId = guild_id;

    public void ResetParam()
    {
      this.mGuildId = 0L;
      this.mGuildData = (GuildData) null;
    }

    public override void OnActivate(int pinID)
    {
      if (this.mGuildId <= 0L)
        return;
      if (pinID == 1)
      {
        this.ExecRequest((WebAPI) new ReqGuildInfo(this.mGuildId, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        this.ResetParam();
      }
      ((Behaviour) this).enabled = true;
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        if (Network.ErrCode == Network.EErrCode.Guild_NotFound)
          this.OnBack();
        else
          this.OnRetry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<FlowNode_ReqGuildInfo.Json_ResGuildInfo> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_ReqGuildInfo.Json_ResGuildInfo>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        try
        {
          if (jsonObject.body.guild_recommend != null)
          {
            this.mGuildData = new GuildData();
            this.mGuildData.Deserialize(jsonObject.body.guild_recommend);
          }
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

    public class Json_ResGuildInfo
    {
      public JSON_Guild guild_recommend;
    }
  }
}
