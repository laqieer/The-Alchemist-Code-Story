// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_ProductParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  public class JSON_ProductParam
  {
    public string id;
    public string product_id;
    public string platform;
    public string name;
    public string description;
    public int additional_paid_coin;
    public int additional_free_coin;
    public int remain_num;
    public JSON_ProductSaleInfo sale;
  }
}
