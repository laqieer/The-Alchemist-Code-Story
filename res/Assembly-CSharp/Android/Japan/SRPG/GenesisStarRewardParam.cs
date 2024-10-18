// Decompiled with JetBrains decompiler
// Type: SRPG.GenesisStarRewardParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class GenesisStarRewardParam
  {
    public int NeedStarNum;
    public string RewardId;
    public bool IsReward;

    public void Deserialize(JSON_GenesisStarRewardParam json)
    {
      if (json == null)
        return;
      this.NeedStarNum = json.need;
      this.RewardId = json.reward_id;
    }
  }
}
