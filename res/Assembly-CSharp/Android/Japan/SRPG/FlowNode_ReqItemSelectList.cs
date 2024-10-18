﻿// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqItemSelectList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.NodeType("Mail/ReqItemSelectList", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_ReqItemSelectList : FlowNode_Network
  {
    public GetItemWindow mGetItemWindow;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0 || this.enabled)
        return;
      if (Network.Mode != Network.EConnectMode.Offline)
      {
        MailData mail = MonoSingleton<GameManager>.Instance.FindMail((long) GlobalVars.SelectedMailUniqueID);
        if (mail == null)
        {
          this.enabled = false;
        }
        else
        {
          this.enabled = true;
          this.ExecRequest((WebAPI) new ReqMailSelect(mail.Find(GiftTypes.SelectItem).iname, ReqMailSelect.type.item, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        }
      }
      else
      {
        this.Deserialize(this.DummyResponse());
        this.Success();
      }
    }

    private Json_ItemSelectResponse DummyResponse()
    {
      string[] strArray = new string[2]{ "IT_SET_EQUP_MIT_03", "IT_SET_EQUP_MIT_03" };
      int length = strArray.Length;
      Json_ItemSelectResponse itemSelectResponse = new Json_ItemSelectResponse();
      itemSelectResponse.select = new Json_ItemSelectItem[length];
      if ((UnityEngine.Object) MonoSingleton<GameManager>.GetInstanceDirect() == (UnityEngine.Object) null)
      {
        GameManager instance = MonoSingleton<GameManager>.Instance;
      }
      for (int index = 0; index < length; ++index)
        itemSelectResponse.select[index] = new Json_ItemSelectItem()
        {
          iname = strArray[index]
        };
      return itemSelectResponse;
    }

    private void Deserialize(Json_ItemSelectResponse json)
    {
      ItemSelectListData itemSelectListData = new ItemSelectListData();
      itemSelectListData.Deserialize(json);
      this.mGetItemWindow.Refresh(itemSelectListData.items.ToArray());
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
        WebAPI.JSON_BodyResponse<Json_ItemSelectResponse> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_ItemSelectResponse>>(www.text);
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
