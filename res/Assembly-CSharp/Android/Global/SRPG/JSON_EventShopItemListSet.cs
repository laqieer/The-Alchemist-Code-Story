// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_EventShopItemListSet
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class JSON_EventShopItemListSet
  {
    public int id;
    public int sold;
    public Json_ShopItemDesc item;
    public JSON_EventShopItemListSet.Cost cost;
    public Json_ShopItemDesc[] children;
    public int isreset;

    public class Cost : Json_ShopItemCost
    {
      public string iname;
    }
  }
}
