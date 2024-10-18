// Decompiled with JetBrains decompiler
// Type: SRPG.EventShopInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class EventShopInfo
  {
    public JSON_ShopListArray.Shops shops;
    public string shop_cost_iname;
    public bool btn_update;
    public string banner_sprite;

    public void Setup(JSON_ShopListArray.Shops _shops, Json_ShopMsgResponse _msg)
    {
      this.shops = _shops;
      if (_msg == null)
        return;
      this.banner_sprite = _msg.banner;
      this.shop_cost_iname = _msg.costiname;
      if (_msg.update == null)
        return;
      this.btn_update = _msg.update.Equals("on");
    }
  }
}
