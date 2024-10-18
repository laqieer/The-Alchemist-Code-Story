// Decompiled with JetBrains decompiler
// Type: SRPG.BuyCoinRewardParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class BuyCoinRewardParam
  {
    private string mId;
    private BuyCoinManager.PremiumRewadType mDrawType;
    private string mDrawIname;
    private string mGiftMessage;
    private List<BuyCoinRewardItemParam> mRewards = new List<BuyCoinRewardItemParam>();

    public string Id => this.mId;

    public BuyCoinManager.PremiumRewadType DrawType => this.mDrawType;

    public string DrawIname => this.mDrawIname;

    public string GiftMessage => this.mGiftMessage;

    public List<BuyCoinRewardItemParam> Reward => this.mRewards;

    public bool Deserialize(JSON_BuyCoinRewardParam json)
    {
      if (json == null)
        return false;
      this.mId = json.id;
      this.mDrawType = (BuyCoinManager.PremiumRewadType) json.draw_type;
      this.mDrawIname = json.draw_iname;
      this.mGiftMessage = json.gift_message;
      this.mRewards.Clear();
      if (json.rewards != null)
      {
        for (int index = 0; index < json.rewards.Length; ++index)
          this.mRewards.Add(new BuyCoinRewardItemParam()
          {
            Iname = json.rewards[index].iname,
            Type = (BuyCoinManager.PremiumRewadType) json.rewards[index].type,
            Num = json.rewards[index].num
          });
      }
      return true;
    }
  }
}
