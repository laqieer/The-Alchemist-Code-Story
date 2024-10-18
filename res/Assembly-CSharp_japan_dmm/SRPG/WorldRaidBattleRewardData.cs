// Decompiled with JetBrains decompiler
// Type: SRPG.WorldRaidBattleRewardData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;

#nullable disable
namespace SRPG
{
  public class WorldRaidBattleRewardData
  {
    public RaidRewardType Type { get; private set; }

    public string Iname { get; private set; }

    public int Num { get; private set; }

    public bool Deserialize(JSON_WorldRaidBattleRewardData json)
    {
      if (json == null)
        return false;
      this.Type = (RaidRewardType) json.item_type;
      this.Iname = json.item_iname;
      this.Num = json.item_num;
      return true;
    }

    public Json_Gift ConvertToJsonGift()
    {
      Json_Gift jsonGift = new Json_Gift()
      {
        num = this.Num,
        iname = this.Iname
      };
      switch (this.Type)
      {
        case RaidRewardType.Gold:
          jsonGift.gold = this.Num;
          break;
        case RaidRewardType.Coin:
          jsonGift.coin = this.Num;
          break;
        case RaidRewardType.Artifact:
          ArtifactParam artifactParam = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(this.Iname);
          jsonGift.rare = artifactParam.rareini;
          break;
      }
      return jsonGift;
    }
  }
}
