// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqHealAp
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.NodeType("System/ReqHealAp", 32741)]
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
        this.ActivateOutputLinks(1);
      }
    }

    private void Start()
    {
    }

    private void Update()
    {
    }

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      ItemData dataOfClass = DataSource.FindDataOfClass<ItemData>(this.gameObject, (ItemData) null);
      HealAp component = this.gameObject.GetComponent<HealAp>();
      if (dataOfClass == null)
        return;
      this.ExecRequest((WebAPI) new ReqHealAp(dataOfClass.UniqueID, component.bar.UseItemNum, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
    }
  }
}
