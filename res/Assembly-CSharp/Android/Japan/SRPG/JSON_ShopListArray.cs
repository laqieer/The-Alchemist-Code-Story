﻿// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_ShopListArray
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class JSON_ShopListArray
  {
    public JSON_ShopListArray.Shops[] shops;

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