// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqShopLineup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("System/Shop/ReqShopLineup", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_ReqShopLineup : FlowNode_Network
  {
    private EShopType mShopType;
    [SerializeField]
    private GameObject Target;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0 || this.enabled)
        return;
      string gname;
      switch (GlobalVars.ShopType)
      {
        case EShopType.Event:
          gname = GlobalVars.EventShopItem.shops.gname;
          break;
        case EShopType.Limited:
          gname = GlobalVars.LimitedShopItem.shops.gname;
          break;
        default:
          this.mShopType = GlobalVars.ShopType;
          gname = this.mShopType.ToString();
          break;
      }
      this.ExecRequest((WebAPI) new ReqShopLineup(gname, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
      this.enabled = true;
    }

    private void Success()
    {
      this.enabled = false;
      this.ActivateOutputLinks(1);
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
        WebAPI.JSON_BodyResponse<Json_ShopLineup> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_ShopLineup>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          this.OnRetry();
        }
        else
        {
          Network.RemoveAPI();
          if ((UnityEngine.Object) this.Target == (UnityEngine.Object) null)
            return;
          ShopLineupWindow component = this.Target.GetComponent<ShopLineupWindow>();
          if ((UnityEngine.Object) component == (UnityEngine.Object) null)
            return;
          component.SetItemInames(jsonObject.body.shopitems);
          this.Success();
        }
      }
    }
  }
}
