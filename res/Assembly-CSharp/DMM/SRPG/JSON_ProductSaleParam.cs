// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_ProductSaleParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class JSON_ProductSaleParam
  {
    public int pk;
    public JSON_ProductSaleParam.Fields fields;

    public class Fields
    {
      public int id;
      public string product_id;
      public string platform;
      public string name;
      public string description;
      public int additional_free_coin;
      public int condition_type;
      public string condition_value;
    }
  }
}
