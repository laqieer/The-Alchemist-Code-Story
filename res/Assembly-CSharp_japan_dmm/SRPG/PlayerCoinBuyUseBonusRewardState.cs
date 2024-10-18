// Decompiled with JetBrains decompiler
// Type: SRPG.PlayerCoinBuyUseBonusRewardState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class PlayerCoinBuyUseBonusRewardState
  {
    public string iname;
    public int num;
    public DateTime rewarded_at;

    public bool IsReceived => this.rewarded_at != DateTime.MinValue;

    public void Deserialize(JSON_PlayerCoinBuyUseBonusRewardState json)
    {
      if (json == null)
        return;
      this.iname = json.iname;
      this.num = json.num;
      this.rewarded_at = DateTime.MinValue;
      if (json.rewarded_at <= 0L)
        return;
      this.rewarded_at = TimeManager.FromUnixTime(json.rewarded_at);
    }
  }
}
