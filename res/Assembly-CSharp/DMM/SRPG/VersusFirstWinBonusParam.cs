// Decompiled with JetBrains decompiler
// Type: SRPG.VersusFirstWinBonusParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class VersusFirstWinBonusParam
  {
    public int id;
    public DateTime begin_at;
    public DateTime end_at;
    public VersusWinBonusRewardParam[] rewards;

    public bool Deserialize(JSON_VersusFirstWinBonus json)
    {
      if (json == null)
        return false;
      this.id = json.id;
      if (json.rewards != null)
      {
        int length = json.rewards.Length;
        this.rewards = new VersusWinBonusRewardParam[length];
        if (this.rewards != null)
        {
          for (int index = 0; index < length; ++index)
          {
            this.rewards[index] = new VersusWinBonusRewardParam();
            this.rewards[index].type = (VERSUS_REWARD_TYPE) Enum.ToObject(typeof (VERSUS_REWARD_TYPE), json.rewards[index].item_type);
            this.rewards[index].iname = json.rewards[index].item_iname;
            this.rewards[index].num = json.rewards[index].item_num;
          }
        }
      }
      try
      {
        if (!string.IsNullOrEmpty(json.begin_at))
          this.begin_at = DateTime.Parse(json.begin_at);
        if (!string.IsNullOrEmpty(json.end_at))
          this.end_at = DateTime.Parse(json.end_at);
      }
      catch (Exception ex)
      {
        DebugUtility.LogError(ex.Message);
        return false;
      }
      return true;
    }
  }
}
