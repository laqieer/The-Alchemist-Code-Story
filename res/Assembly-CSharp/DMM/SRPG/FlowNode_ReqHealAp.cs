// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqHealAp
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.NodeType("System/Player/ReqHealAp", 32741)]
  [FlowNode.Pin(0, "ReqHealAp", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_ReqHealAp : FlowNode_Network
  {
    public override void OnSuccess(WWWResult www)
    {
      WebAPI.JSON_BodyResponse<Json_ArenaPlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_ArenaPlayerDataAll>>(www.text);
      if (jsonObject.body == null)
      {
        this.OnRetry();
      }
      else
      {
        Network.RemoveAPI();
        MonoSingleton<GameManager>.Instance.Player.Deserialize(jsonObject.body.player);
        MonoSingleton<GameManager>.Instance.Player.Deserialize(jsonObject.body.items);
        ((Component) this).gameObject.GetComponent<HealAp>().now_ap.text = MonoSingleton<GameManager>.Instance.Player.Stamina.ToString();
        MonoSingleton<MySound>.Instance.PlaySEOneShot("SE_0011");
        this.Success();
      }
    }

    private void Success()
    {
      ((Behaviour) this).enabled = false;
      this.ActivateOutputLinks(1);
    }

    public override void OnActivate(int pinID)
    {
      if (pinID != 0 || ((Behaviour) this).enabled)
        return;
      ItemData dataOfClass = DataSource.FindDataOfClass<ItemData>(((Component) this).gameObject, (ItemData) null);
      HealAp component = ((Component) this).gameObject.GetComponent<HealAp>();
      if (dataOfClass == null)
        return;
      ((Behaviour) this).enabled = true;
      this.ExecRequest((WebAPI) new ReqHealAp(dataOfClass.UniqueID, component.bar.UseItemNum, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
    }
  }
}
