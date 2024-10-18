// Decompiled with JetBrains decompiler
// Type: SRPG.CurrencyTracker
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;

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
