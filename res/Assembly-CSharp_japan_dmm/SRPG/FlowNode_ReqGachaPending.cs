// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqGachaPending
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("Gacha/ReqGachaPending", 32741)]
  [FlowNode.Pin(0, "Request(リクエスト)", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Request2(リクエスト2)", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(10, "Success(リクエスト成功)", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(11, "Failued(リクエスト失敗)", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(12, "NoPending(保留中の引き直し召喚がない)", FlowNode.PinTypes.Output, 12)]
  public class FlowNode_ReqGachaPending : FlowNode_Network
  {
    private const int PIN_IN_REQUEST = 0;
    private const int PIN_IN_REQUEST_FORCE = 1;
    private const int PIN_OT_SUCCESS = 10;
    private const int PIN_OT_FAILUED = 11;
    private const int PIN_OT_NO_PENDING = 12;
    private List<string> DownloadUnits = new List<string>();
    private List<string> DownloadArtifacts = new List<string>();
    private List<string> DownloadConceptCard = new List<string>();

    public override void OnActivate(int pinID)
    {
      ((Behaviour) this).enabled = true;
      this.ExecRequest((WebAPI) new ReqGachaPending(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
    }

    private void Success()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(10);
    }

    private void Failure()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(11);
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
        WebAPI.JSON_BodyResponse<Json_PendingGachaResponse> res = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PendingGachaResponse>>(www.text);
        DebugUtility.Assert(res != null, "res == null");
        Network.RemoveAPI();
        if (res.body == null)
          return;
        List<GachaDropData> gachaDropDataList = new List<GachaDropData>();
        foreach (Json_DropInfo json in res.body.add)
        {
          GachaDropData gachaDropData = new GachaDropData();
          if (gachaDropData.Deserialize(json))
            gachaDropDataList.Add(gachaDropData);
        }
        List<GachaDropData> a_dropMails = new List<GachaDropData>();
        if (res.body.add_mail != null)
        {
          foreach (Json_DropInfo json in res.body.add_mail)
          {
            GachaDropData gachaDropData = new GachaDropData();
            if (gachaDropData.Deserialize(json))
              a_dropMails.Add(gachaDropData);
          }
        }
        for (int index = 0; index < gachaDropDataList.Count; ++index)
        {
          if (gachaDropDataList[index].type == GachaDropData.Type.ConceptCard)
          {
            MonoSingleton<GameManager>.Instance.Player.OnDirtyConceptCardData();
            break;
          }
        }
        GachaReceiptData a_receipt = new GachaReceiptData();
        a_receipt.iname = res.body.gacha.gname;
        int a_redraw_rest = 0;
        if (res.body.rest > 0)
          a_redraw_rest = res.body.rest;
        GachaResultData.Init(gachaDropDataList, a_dropMails, a_receipt: a_receipt, a_is_pending: a_redraw_rest <= 0 ? 0 : 1, a_redraw_rest: a_redraw_rest);
        bool flag = false;
        if ((MonoSingleton<GameManager>.Instance.Player.TutorialFlags & 1L) != 0L)
        {
          string empty = string.Empty;
          if (a_redraw_rest > 0)
          {
            if (res.body.gacha.time_end > Network.GetServerTime())
              flag = true;
            else
              empty = LocalizedText.Get("sys.GACHA_REDRAW_CAUTION_OUTOF_PERIOD");
          }
          else
            empty = LocalizedText.Get("sys.GACHA_REDRAW_CAUTION_LIMIT_UPPER");
          if (!flag)
          {
            UIUtility.SystemMessage(empty, (UIUtility.DialogResultEvent) (go => this.ExecGacha(res.body.gacha.gname)), systemModal: true);
            return;
          }
        }
        this.StartCoroutine(this.AsyncGachaResultData(gachaDropDataList));
      }
    }

    private void ExecGacha(string _gname)
    {
      FlowNode_ExecGacha2 component = ((Component) this).GetComponent<FlowNode_ExecGacha2>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      GachaRequestParam _rparam = new GachaRequestParam(_gname);
      if (_rparam == null)
        return;
      component.OnExecGachaDecision(_rparam);
    }

    [DebuggerHidden]
    private IEnumerator AsyncGachaResultData(List<GachaDropData> drops)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_ReqGachaPending.\u003CAsyncGachaResultData\u003Ec__Iterator0()
      {
        drops = drops,
        \u0024this = this
      };
    }
  }
}
