// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_ShopListArray
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class JSON_ShopListArray
  {
    public JSON_ShopListArray.Shops[] shops;
    public Json_LimitedShopResponse first_shop_info;

    public class Shops
    {
      public int id;
      public string gname;
      public string gtype;
      public long yymmddhhmm;
      public string created_at;
      public string update_at;
      public JSON_ShopListArray.Shops.JSON_ShopListInfo info;
      public long start;
      public long end;
      public JSON_ShopListArray.Shops.JSON_UnlockInfo unlock;

      public class JSON_ShopListInfo
      {
        public string rare;
        public string title;
        public int portlv;
        public int unlock;
        public string msg;
        private JSON_ShopListArray.Shops.JSON_ShopListInfo.JSON_ShopListInfoCost cost;
        private int gold;

        public class JSON_ShopListInfoCost
        {
          private int gold;
        }
      }

      public class JSON_UnlockInfo
      {
        public int flg;
        public string message;
      }
    }
  }
}
