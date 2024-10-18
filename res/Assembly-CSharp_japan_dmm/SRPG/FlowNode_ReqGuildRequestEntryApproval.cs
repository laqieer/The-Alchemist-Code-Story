// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqGuildRequestEntryApproval
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Guild/ReqGuildRequestEntryApproval", 32741)]
  [FlowNode.Pin(1, "ギルド加入申請を承認", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "ギルド加入申請を拒否", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(3, "ギルド加入申請を一括拒否", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(101, "ギルド加入申請を承認完了", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "ギルド加入申請を拒否完了", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(103, "ギルド加入申請を一括拒否完了", FlowNode.PinTypes.Output, 103)]
  public class FlowNode_ReqGuildRequestEntryApproval : FlowNode_Network
  {
    private const int PIN_INPUT_START_GUILD_REQUEST_ENTRY_APPROVAL_OK = 1;
    private const int PIN_INPUT_START_GUILD_REQUEST_ENTRY_APPROVAL_NG = 2;
    private const int PIN_INPUT_START_GUILD_REQUEST_ENTRY_APPROVAL_NG_ALL = 3;
    private const int PIN_OUTPUT_END_GUILD_REQUEST_ENTRY_APPROVAL_OK = 101;
    private const int PIN_OUTPUT_END_GUILD_REQUEST_ENTRY_APPROVAL_NG = 102;
    private const int PIN_OUTPUT_END_GUILD_REQUEST_ENTRY_APPROVAL_NG_ALL = 103;
    private int mPinId;

    public override void OnActivate(int pinID)
    {
      GuildConfirm instance = GuildConfirm.Instance;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance, (UnityEngine.Object) null))
        return;
      this.mPinId = pinID;
      switch (pinID)
      {
        case 1:
          this.ExecRequest((WebAPI) new ReqGuildRequestEntryApprovalOK(instance.TargetMember.Uid, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          break;
        case 2:
          this.ExecRequest((WebAPI) new ReqGuildRequestEntryApprovalNG(new string[1]
          {
            instance.TargetMember.Uid
          }, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          break;
        case 3:
          string[] request_user_uids = new string[instance.TargetEntryRequests.Count];
          for (int index = 0; index < instance.TargetEntryRequests.Count; ++index)
            request_user_uids[index] = instance.TargetEntryRequests[index].Uid;
          this.ExecRequest((WebAPI) new ReqGuildRequestEntryApprovalNG(request_user_uids, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
          break;
      }
      ((Behaviour) this).enabled = true;
    }

    public override void OnSuccess(WWWResult www)
    {
      switch (this.mPinId)
      {
        case 1:
          this.OnSuccess_ApprovalOK(www);
          break;
        case 2:
          this.OnSuccess_ApprovalNG(www);
          break;
        case 3:
          this.OnSuccess_ApprovalNGAll(www);
          break;
      }
    }

    private void OnSuccess_ApprovalOK(WWWResult www)
    {
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.Guild_JoinedAlready:
            this.OnBack();
            break;
          case Network.EErrCode.Guild_NotFoundEntryRequest:
            this.OnBack();
            break;
          case Network.EErrCode.Guild_AlredyMember:
            this.OnBack();
            break;
          case Network.EErrCode.Guild_MemberMax:
            this.OnBack();
            break;
          case Network.EErrCode.Guild_EntryCoolTime:
            this.OnBack();
            break;
          default:
            this.OnRetry();
            break;
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<FlowNode_ReqGuildRequestEntryApproval.Json_ResGuild> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_ReqGuildRequestEntryApproval.Json_ResGuild>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        try
        {
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.guild);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) GuildManager.Instance, (UnityEngine.Object) null))
            GuildManager.Instance.SetEntryRequest(jsonObject.body.applied_member);
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

    private void OnSuccess_ApprovalNG(WWWResult www)
    {
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.Guild_JoinedAlready:
            this.OnBack();
            break;
          case Network.EErrCode.Guild_NotFoundEntryRequest:
            this.OnBack();
            break;
          case Network.EErrCode.Guild_AlredyMember:
            this.OnBack();
            break;
          default:
            this.OnRetry();
            break;
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<FlowNode_ReqGuildRequestEntryApproval.Json_ResGuild> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_ReqGuildRequestEntryApproval.Json_ResGuild>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        try
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) GuildManager.Instance, (UnityEngine.Object) null))
            GuildManager.Instance.SetEntryRequest(jsonObject.body.applied_member);
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          return;
        }
        this.ActivateOutputLinks(102);
        ((Behaviour) this).enabled = false;
      }
    }

    private void OnSuccess_ApprovalNGAll(WWWResult www)
    {
      if (Network.IsError)
      {
        int errCode = (int) Network.ErrCode;
        this.OnRetry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<FlowNode_ReqGuildRequestEntryApproval.Json_ResGuild> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_ReqGuildRequestEntryApproval.Json_ResGuild>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        try
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) GuildManager.Instance, (UnityEngine.Object) null))
            GuildManager.Instance.SetEntryRequest(jsonObject.body.applied_member);
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          return;
        }
        this.ActivateOutputLinks(103);
        ((Behaviour) this).enabled = false;
      }
    }

    public class Json_ResGuild
    {
      public JSON_Guild guild;
      public JSON_GuildMember[] applied_member;
    }
  }
}
