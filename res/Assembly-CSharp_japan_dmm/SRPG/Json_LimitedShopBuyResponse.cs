// Decompiled with JetBrains decompiler
// Type: SRPG.Json_LimitedShopBuyResponse
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class Json_LimitedShopBuyResponse
  {
    public Json_Currencies currencies;
    public JSON_LimitedShopItemListSet[] shopitems;
    public Json_Item[] items;
    public Json_MailInfo mail_info;
    public Json_ShopBuyConceptCard[] cards;
    public Json_Unit[] units;
    public int concept_count;
    public JSON_TrophyProgress[] trophyprogs;
    public JSON_TrophyProgress[] bingoprogs;
    public Json_Item[] runes;
    public int rune_storage_used;
  }
}
