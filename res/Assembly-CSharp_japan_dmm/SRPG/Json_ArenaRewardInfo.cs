// Decompiled with JetBrains decompiler
// Type: SRPG.Json_ArenaRewardInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class Json_ArenaRewardInfo
  {
    public int gold;
    public int coin;
    public int arenacoin;
    public Json_Item[] items;

    public bool IsReward()
    {
      if (this.gold > 0 || this.coin > 0 || this.arenacoin > 0)
        return true;
      return this.items != null && this.items.Length > 0;
    }
  }
}
