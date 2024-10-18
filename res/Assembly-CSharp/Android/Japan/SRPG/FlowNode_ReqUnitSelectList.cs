﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqUnitSelectList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.NodeType("System/Unit/ReqUnitSelectList", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_ReqUnitSelectList : FlowNode_Network
  {
    public UnitSelectListData mUnitSelectListData;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0 || this.enabled)
        return;
      if (Network.Mode == Network.EConnectMode.Online)
      {
        MailData mail = MonoSingleton<GameManager>.Instance.FindMail((long) GlobalVars.SelectedMailUniqueID);
        if (mail == null)
        {
          this.enabled = false;
        }
        else
        {
          this.enabled = true;
          this.ExecRequest((WebAPI) new ReqMailSelect(mail.Find(GiftTypes.SelectUnitItem).iname, ReqMailSelect.type.unit, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        }
      }
      else
      {
        this.Deserialize(this.DummyResponse());
        this.Success();
      }
    }

    private Json_UnitSelectResponse DummyResponse()
    {
      string[] strArray = new string[20]{ "UN_V2_VANEKIS", "UN_V2_AMIS", "UN_V2_ISHUNA", "UN_V2_MIZUCHI", "UN_V2_KAZAHAYA", "UN_V2_CIEL", "UN_V2_YUAN", "UN_V2_DECEL", "UN_V2_ENNIS", "UN_V2_ANNEROSE", "UN_V2_GAYN", "UN_V2_AYLLU", "UN_V2_SARAUZU", "UN_V2_RION", "UN_V2_PATTI", "UN_V2_ALMILA", "UN_V2_MICHAEL", "UN_V2_ARKILL", "UN_V2_KUON", "UN_V2_MIANNU" };
      int length = strArray.Length;
      Json_UnitSelectResponse unitSelectResponse = new Json_UnitSelectResponse();
      unitSelectResponse.select = new Json_UnitSelectItem[length];
      if ((UnityEngine.Object) MonoSingleton<GameManager>.GetInstanceDirect() == (UnityEngine.Object) null)
      {
        GameManager instance = MonoSingleton<GameManager>.Instance;
      }
      for (int index = 0; index < length; ++index)
        unitSelectResponse.select[index] = new Json_UnitSelectItem()
        {
          iname = strArray[index]
        };
      return unitSelectResponse;
    }

    private void Deserialize(Json_UnitSelectResponse json)
    {
      this.mUnitSelectListData = new UnitSelectListData();
      this.mUnitSelectListData.Deserialize(json);
      this.gameObject.GetComponent<GetUnitWindow>().RefreshPieceUnit(false, this.mUnitSelectListData);
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
        WebAPI.JSON_BodyResponse<Json_UnitSelectResponse> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_UnitSelectResponse>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          this.OnRetry();
        }
        else
        {
          Network.RemoveAPI();
          this.Deserialize(jsonObject.body);
          this.Success();
        }
      }
    }

    private void Success()
    {
      this.enabled = false;
      this.ActivateOutputLinks(1);
    }
  }
}
