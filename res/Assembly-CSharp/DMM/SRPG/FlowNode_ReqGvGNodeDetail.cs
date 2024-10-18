// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqGvGNodeDetail
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
  [FlowNode.NodeType("GvG/Req/GvGNodeDetail", 32741)]
  [FlowNode.Pin(1, "Request", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "Success", FlowNode.PinTypes.Output, 101)]
  public class FlowNode_ReqGvGNodeDetail : FlowNode_Network
  {
    protected const int PIN_IN_REQUEST = 1;
    protected const int PIN_OUT_SUCCESS = 101;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1 || ((Behaviour) this).enabled)
        return;
      ((Behaviour) this).enabled = true;
      this.SerializeCompressMethod = EncodingTypes.ESerializeCompressMethod.TYPED_MESSAGE_PACK;
      if (MonoSingleton<GameManager>.Instance.Player.PlayerGuild == null)
      {
        this.OnFailed();
      }
      else
      {
        GvGManager instance1 = GvGManager.Instance;
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance1, (UnityEngine.Object) null))
        {
          this.OnFailed();
        }
        else
        {
          GvGNodeDetail instance2 = GvGNodeDetail.Instance;
          GvGDefenseSettings instance3 = GvGDefenseSettings.Instance;
          int currentPage;
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instance2, (UnityEngine.Object) null))
            currentPage = instance2.CurrentPage;
          else if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instance3, (UnityEngine.Object) null))
          {
            currentPage = instance3.CurrentPage;
          }
          else
          {
            this.OnFailed();
            return;
          }
          this.ExecRequest((WebAPI) new ReqGvGNodeDetail(instance1.SelectNodeId, MonoSingleton<GameManager>.Instance.Player.PlayerGuild.Gid, GvGManager.GvGGroupId, currentPage, new SRPG.Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback), this.SerializeCompressMethod));
        }
      }
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
      ReqGvGNodeDetail.Response response = (ReqGvGNodeDetail.Response) null;
      bool flag = EncodingTypes.IsJsonSerializeCompressSelected(this.SerializeCompressMethod);
      if (!flag)
      {
        FlowNode_ReqGvGNodeDetail.MP_Response mpResponse = SerializerCompressorHelper.Decode<FlowNode_ReqGvGNodeDetail.MP_Response>(www.rawResult, true, EncodingTypes.GetCompressModeFromSerializeCompressMethod(this.SerializeCompressMethod));
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
          WebAPI.JSON_BodyResponse<ReqGvGNodeDetail.Response> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqGvGNodeDetail.Response>>(www.text);
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
          GvGNodeDetail instance1 = GvGNodeDetail.Instance;
          GvGDefenseSettings instance2 = GvGDefenseSettings.Instance;
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instance1, (UnityEngine.Object) null))
            instance1.SetupDefenseParties(response.defenses, response.totalPage, response.total_beat_num);
          else if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instance2, (UnityEngine.Object) null))
            instance2.SetupDefenseParties(response.defenses, response.totalPage, response.total_beat_num);
          else
            this.OnFailed();
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

    public static JSON_GvGPartyUnit _CreateTestUnit(long iid, string unitIname)
    {
      UnitParam unitParam = MonoSingleton<GameManager>.Instance.GetUnitParam(unitIname);
      int num1 = Random.Range(0, 3);
      JSON_GvGPartyUnit testUnit = new JSON_GvGPartyUnit();
      testUnit.iid = iid;
      testUnit.iname = unitIname;
      JSON_GvGPartyUnit jsonGvGpartyUnit = testUnit;
      int num2;
      switch (num1)
      {
        case 0:
          num2 = -1;
          break;
        case 1:
          num2 = 0;
          break;
        default:
          num2 = 100;
          break;
      }
      jsonGvGpartyUnit.hp = num2;
      testUnit.rare = 5;
      testUnit.plus = 25;
      testUnit.lv = 1;
      testUnit.exp = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitLevelExp(Random.Range(1, 86));
      testUnit.jobs = new Json_Job[1]
      {
        new Json_Job()
        {
          iid = iid * 10L + 1L,
          iname = unitParam.GetJobId(0),
          rank = 1,
          equips = new Json_Equip[JobRankParam.MAX_RANKUP_EQUIPS],
          select = new Json_JobSelectable()
          {
            abils = new long[5],
            artifacts = new long[3]
          }
        }
      };
      testUnit.select = new Json_UnitSelectable()
      {
        job = iid * 10L + 1L
      };
      testUnit.fav = 0;
      testUnit.elem = 0;
      testUnit.concept_cards = new JSON_ConceptCard[2]
      {
        new JSON_ConceptCard()
        {
          iid = iid * 10L + 1L,
          iname = "TS_ENVYRIA_VETTEL_01",
          plus = 5,
          exp = 342342342,
          leaderskill = 1
        },
        new JSON_ConceptCard() { iid = 0L }
      };
      return testUnit;
    }

    [MessagePackObject(true)]
    public class MP_Response : WebAPI.JSON_BaseResponse
    {
      public ReqGvGNodeDetail.Response body;
    }
  }
}
