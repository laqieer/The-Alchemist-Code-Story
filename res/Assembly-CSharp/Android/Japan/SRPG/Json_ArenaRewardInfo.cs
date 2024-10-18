// Decompiled with JetBrains decompiler
// Type: SRPG.Json_ArenaRewardInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
      if (this.items != null)
        return this.items.Length > 0;
      return false;
    }
  }
}
