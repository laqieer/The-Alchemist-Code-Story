// Decompiled with JetBrains decompiler
// Type: SRPG.DrawCardInfoParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class DrawCardInfoParam
  {
    public OInt CardNum;
    public OInt MissNum;
    public string DcRewardId;
    private DrawCardRewardParam mDcReward;

    public DrawCardRewardParam DcReward
    {
      get
      {
        if (this.mDcReward == null && !string.IsNullOrEmpty(this.DcRewardId))
          this.mDcReward = DrawCardRewardParam.GetParam(this.DcRewardId);
        return this.mDcReward;
      }
    }

    public void Deserialize(JSON_DrawCardParam.DrawInfo json)
    {
      if (json == null)
        return;
      this.CardNum = (OInt) json.card_num;
      this.MissNum = (OInt) json.miss_num;
      this.DcRewardId = json.dc_reward_id;
    }
  }
}
