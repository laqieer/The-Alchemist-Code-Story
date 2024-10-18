// Decompiled with JetBrains decompiler
// Type: SRPG.ReqUnitPieceShopItemList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  public class ReqUnitPieceShopItemList : WebAPI
  {
    public ReqUnitPieceShopItemList(
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod compress)
    {
      this.name = "shop/unitpiece";
      this.body = WebAPI.GetRequestString(string.Empty);
      this.callback = response;
      this.serializeCompressMethod = compress;
    }

    [MessagePackObject(true)]
    [Serializable]
    public class Response
    {
      public string shop_iname;
      public string cost_iname;
      public Json_UnitPieceShopItem[] shopitems;
    }
  }
}
