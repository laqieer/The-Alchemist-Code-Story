// Decompiled with JetBrains decompiler
// Type: SRPG.ShopItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;

#nullable disable
namespace SRPG
{
  public class ShopItem
  {
    public int id;
    public string iname;
    public int num;
    public ESaleType saleType;
    public bool is_soldout;
    public int saleValue;
    public int max_num;
    public int bougthnum;
    public Json_ShopItemDesc[] children;
    public bool is_reset;
    public long start;
    public long end;
    public int discount;
    protected EShopItemType shopItemType;
    public int step;

    public int remaining_num => this.max_num - this.bougthnum;

    public bool IsNotLimited => this.max_num == 0;

    public bool isSetSaleValue => this.saleValue > 0;

    public bool IsSet => this.children != null && this.children.Length > 0;

    public bool IsItem => this.shopItemType == EShopItemType.Item;

    public bool IsArtifact => this.shopItemType == EShopItemType.Artifact;

    public bool IsConceptCard => this.shopItemType == EShopItemType.ConceptCard;

    public EShopItemType ShopItemType => this.shopItemType;

    public bool Deserialize(Json_ShopItem json)
    {
      if (json == null || json.item == null || string.IsNullOrEmpty(json.item.iname) || json.cost == null || string.IsNullOrEmpty(json.cost.type))
        return false;
      this.id = json.id;
      this.iname = json.item.iname;
      this.num = json.item.num;
      this.max_num = json.item.maxnum;
      this.bougthnum = json.item.boughtnum;
      this.saleValue = json.cost.value;
      this.saleType = ShopData.String2SaleType(json.cost.type);
      this.is_reset = json.isreset == 1;
      this.is_soldout = json.sold > 0;
      this.start = json.start;
      this.end = json.end;
      this.discount = json.discount;
      this.step = json.item.step;
      this.children = (Json_ShopItemDesc[]) null;
      if (json.children != null)
      {
        this.children = json.children;
        foreach (Json_ShopItemDesc child in this.children)
        {
          if (child.IsConceptCard)
            MonoSingleton<GameManager>.Instance.Player.SetConceptCardNum(child.iname, child.has_count);
        }
      }
      if (json.children != null)
      {
        this.shopItemType = EShopItemType.Set;
      }
      else
      {
        this.shopItemType = ShopData.String2ShopItemType(json.item.itype);
        if (this.shopItemType == EShopItemType.Unknown)
          this.shopItemType = ShopData.Iname2ShopItemType(json.item.iname);
      }
      if (this.IsConceptCard)
        MonoSingleton<GameManager>.Instance.Player.SetConceptCardNum(this.iname, json.item.has_count);
      return true;
    }
  }
}
