// Decompiled with JetBrains decompiler
// Type: SRPG.Json_ShopLineupItemDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  [Serializable]
  public class Json_ShopLineupItemDetail
  {
    public string iname;
    public string itype;

    public EShopItemType GetShopItemTypeWithIType() => ShopData.String2ShopItemType(this.itype);
  }
}
