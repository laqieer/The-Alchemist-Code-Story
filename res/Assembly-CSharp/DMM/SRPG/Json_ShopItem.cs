﻿// Decompiled with JetBrains decompiler
// Type: SRPG.Json_ShopItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class Json_ShopItem
  {
    public int id;
    public int sold;
    public Json_ShopItemDesc item;
    public Json_ShopItemCost cost;
    public Json_ShopItemDesc[] children;
    public int isreset;
    public long start;
    public long end;
    public int discount;
  }
}
