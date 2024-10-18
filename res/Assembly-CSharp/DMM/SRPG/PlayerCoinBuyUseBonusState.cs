// Decompiled with JetBrains decompiler
// Type: SRPG.PlayerCoinBuyUseBonusState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class PlayerCoinBuyUseBonusState
  {
    public string iname;
    public int total;

    public void Deserialize(JSON_PlayerCoinBuyUseBonusState bonus_state)
    {
      if (bonus_state == null)
        return;
      this.iname = bonus_state.iname;
      this.total = bonus_state.total;
    }
  }
}
