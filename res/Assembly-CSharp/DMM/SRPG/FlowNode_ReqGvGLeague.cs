// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqGvGLeague
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using Gsc.Network.Encoding;
using MessagePack;
using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("GvG/Req/GvGLeague", 32741)]
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "Success", FlowNode.PinTypes.Output, 101)]
  public class FlowNode_ReqGvGLeague : FlowNode_Network
  {
    protected const int PIN_IN_REQUEST = 1;
    protected const int PIN_OUT_SUCCESS = 101;
    private string mLeagueID;
    private int mGuildID;
    private int mStartRank = 1;
    private int mLimit = 20;

    private bool IsAllLeagueRequest => string.IsNullOrEmpty(this.mLeagueID);

    public void SetRequestAllLeague(int startRank, int limit)
    {
      this.mLeagueID = string.Empty;
      this.mGuildID = 0;
      this.mStartRank = startRank;
      this.mLimit = limit;
    }

    public void SetRequestMyLeague()
    {
      this.mLeagueID = GvGLeagueParam.GetGvGLeagueId(MonoSingleton<GameManager>.Instance.Player.GuildLeagueRate);
      this.mGuildID = MonoSingleton<GameManager>.Instance.Player.PlayerGuild.Gid;
      this.mStartRank = 0;
    }

    public void SetRequestMyLeague(int startRank, int limit)
    {
      this.mLeagueID = GvGLeagueParam.GetGvGLeagueId(MonoSingleton<GameManager>.Instance.Player.GuildLeagueRate);
      this.mGuildID = startRank != 0 ? 0 : MonoSingleton<GameManager>.Instance.Player.PlayerGuild.Gid;
      this.mStartRank = startRank;
      this.mLimit = limit;
    }

    public void SetRequesTargetLeague(string leagueID, int startRank, int limit)
    {
      this.mLeagueID = leagueID;
      this.mGuildID = 0;
      this.mStartRank = startRank;
      this.mLimit = limit;
    }

    public override void OnActivate(int pinID)
    {
      if (pinID != 1 || ((Behaviour) this).enabled)
        return;
      ((Behaviour) this).enabled = true;
      this.SerializeCompressMethod = EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK;
      if (MonoSingleton<GameManager>.Instance.Player.PlayerGuild == null)
        this.OnFailed();
      else
        this.ExecRequest((WebAPI) new ReqGvGLeague(this.mLeagueID, this.mGuildID, this.mStartRank, this.mLimit, new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), this.SerializeCompressMethod));
    }

    private void Success()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this, (UnityEngine.Object) null))
        return;
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(101);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this, (UnityEngine.Object) null))
      {
        SRPG.Network.RemoveAPI();
        SRPG.Network.ResetError();
      }
      else
      {
        ReqGvGLeague.Response response = (ReqGvGLeague.Response) null;
        bool flag = EncodingTypes.IsJsonSerializeCompressSelected(this.SerializeCompressMethod);
        if (!flag)
        {
          FlowNode_ReqGvGLeague.MP_Response mpResponse = SerializerCompressorHelper.Decode<FlowNode_ReqGvGLeague.MP_Response>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(this.SerializeCompressMethod));
          DebugUtility.Assert(mpResponse != null, "mp_res == null");
          SRPG.Network.EErrCode stat = (SRPG.Network.EErrCode) mpResponse.stat;
          string statMsg = mpResponse.stat_msg;
          if (stat != SRPG.Network.EErrCode.Success)
            SRPG.Network.SetServerMetaDataAsError(stat, statMsg);
          response = mpResponse.body;
        }
        if (SRPG.Network.IsError)
        {
          switch (SRPG.Network.ErrCode)
          {
            case SRPG.Network.EErrCode.Guild_NotJoined:
              SRPG.Network.RemoveAPI();
              SRPG.Network.ResetError();
              ((Behaviour) this).enabled = false;
              UIUtility.SystemMessage(SRPG.Network.ErrMsg, (UIUtility.DialogResultEvent) (go => GlobalEvent.Invoke("MENU_HOME", (object) null)), systemModal: true);
              break;
            case SRPG.Network.EErrCode.GvG_OutOfPeriod:
            case SRPG.Network.EErrCode.GvG_NotGvGEntry:
              SRPG.Network.RemoveAPI();
              SRPG.Network.ResetError();
              ((Behaviour) this).enabled = false;
              UIUtility.SystemMessage(SRPG.Network.ErrMsg, (UIUtility.DialogResultEvent) (go => GlobalEvent.Invoke("BACK_GUILD", (object) null)), systemModal: true);
              break;
            default:
              this.OnRetry();
              break;
          }
        }
        else
        {
          if (flag)
          {
            WebAPI.JSON_BodyResponse<ReqGvGLeague.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqGvGLeague.Response>>(www.text);
            DebugUtility.Assert(jsonObject != null, "res == null");
            if (jsonObject.body == null)
            {
              this.OnRetry();
              return;
            }
            response = jsonObject.body;
          }
          SRPG.Network.RemoveAPI();
          try
          {
            GvGLeagueManager instance = GvGLeagueManager.Instance;
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instance, (UnityEngine.Object) null))
            {
              if (this.IsAllLeagueRequest)
              {
                instance.SetupAllLeagueData(response.guilds);
                instance.SetAllLeagueTotalCount(response.total_count);
              }
              else
              {
                instance.SetupLeagueData(response.guilds);
                instance.SetLeagueTotalCount(this.mLeagueID, response.total_count);
              }
            }
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
            this.OnFailed();
            return;
          }
          this.Success();
        }
      }
    }

    [MessagePackObject(true)]
    public class MP_Response : WebAPI.JSON_BaseResponse
    {
      public ReqGvGLeague.Response body;
    }
  }
}
