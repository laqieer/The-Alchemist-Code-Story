// Decompiled with JetBrains decompiler
// Type: SRPG.CoinBuyUseBonusItemParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class CoinBuyUseBonusItemParam
  {
    private eCoinBuyUseBonusRewardType type;
    private string iname;
    private int num;

    public eCoinBuyUseBonusRewardType Type => this.type;

    public string Item => this.iname;

    public int Num => this.num;

    public void Deserialize(JSON_CoinBuyUseBonusItemParam json)
    {
      this.type = (eCoinBuyUseBonusRewardType) json.type;
      this.iname = json.iname;
      this.num = json.num;
    }
  }
}
