// Decompiled with JetBrains decompiler
// Type: SRPG.ReqUnitPieceShopBuypaid
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  public class ReqUnitPieceShopBuypaid : WebAPI
  {
    public ReqUnitPieceShopBuypaid(
      string shopIname,
      string iname,
      int buynum,
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod compress)
    {
      this.name = "shop/unitpiece/buy";
      this.body = WebAPI.GetRequestString<ReqUnitPieceShopBuypaid.RequestParam>(new ReqUnitPieceShopBuypaid.RequestParam()
      {
        shop_iname = shopIname,
        iname = iname,
        buynum = buynum
      });
      this.callback = response;
      this.serializeCompressMethod = compress;
    }

    [Serializable]
    public class RequestParam
    {
      public string shop_iname;
      public string iname;
      public int buynum;
    }

    [MessagePackObject(true)]
    [Serializable]
    public class Response
    {
      public Json_Item[] items;
      public Json_UnitPieceShopItem[] shopitems;
      public JSON_TrophyProgress[] trophyprogs;
      public JSON_TrophyProgress[] bingoprogs;
    }
  }
}
