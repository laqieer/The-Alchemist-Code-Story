// Decompiled with JetBrains decompiler
// Type: SRPG.Json_UnitPieceShopItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  [Serializable]
  public class Json_UnitPieceShopItem
  {
    public string itype;
    public string iname;
    public int sold;
    public int maxnum;
    public int boughtnum;
    public int cost_num;
    public int expired_at;
    public int has_next_step;
  }
}
