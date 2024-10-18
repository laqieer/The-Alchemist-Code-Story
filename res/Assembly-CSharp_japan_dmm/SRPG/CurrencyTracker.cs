// Decompiled with JetBrains decompiler
// Type: SRPG.CurrencyTracker
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;

#nullable disable
namespace SRPG
{
  public class CurrencyTracker
  {
    public int Gold;
    public int Coin;
    public int ArenaCoin;
    public int MultiCoin;

    public CurrencyTracker()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      this.Gold = player.Gold;
      this.Coin = player.Coin;
      this.ArenaCoin = player.ArenaCoin;
      this.MultiCoin = player.MultiCoin;
    }

    public void EndTracking()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      this.Gold = player.Gold - this.Gold;
      this.Coin = player.Coin - this.Coin;
      this.ArenaCoin = player.ArenaCoin - this.ArenaCoin;
      this.MultiCoin = player.MultiCoin - this.MultiCoin;
    }
  }
}
