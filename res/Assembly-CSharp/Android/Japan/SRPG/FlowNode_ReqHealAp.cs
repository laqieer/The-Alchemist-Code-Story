// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqHealAp
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;

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
        this.gameObject.GetComponent<HealAp>().now_ap.text = MonoSingleton<GameManager>.Instance.Player.Stamina.ToString();
        MonoSingleton<MySound>.Instance.PlaySEOneShot("SE_0011", 0.0f);
        this.Success();
      }
    }

    private void Success()
    {
      this.enabled = false;
      this.ActivateOutputLinks(1);
    }

    public override void OnActivate(int pinID)
    {
      if (pinID != 0 || this.enabled)
        return;
      ItemData dataOfClass = DataSource.FindDataOfClass<ItemData>(this.gameObject, (ItemData) null);
      HealAp component = this.gameObject.GetComponent<HealAp>();
      if (dataOfClass == null)
        return;
      this.enabled = true;
      this.ExecRequest((WebAPI) new ReqHealAp(dataOfClass.UniqueID, component.bar.UseItemNum, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
    }
  }
}
