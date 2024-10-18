// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGachaDiscount
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class ReqGachaDiscount : WebAPI
  {
    public ReqGachaDiscount(
      string gacha_id,
      string gacha_group_id,
      string discount_item_iname,
      Network.ResponseCallback response)
    {
      this.name = "gacha/discount/apply";
      this.body = WebAPI.GetRequestString<ReqGachaDiscount.RequestParam>(new ReqGachaDiscount.RequestParam()
      {
        gachaid = gacha_id,
        groupid = gacha_group_id,
        discount_iname = discount_item_iname
      });
      this.callback = response;
    }

    [Serializable]
    public class RequestParam
    {
      public string gachaid;
      public string groupid;
      public string discount_iname;
    }

    [Serializable]
    public class Response
    {
      public Json_Item[] items;
      public Json_GachaParam[] gachas;
    }
  }
}
