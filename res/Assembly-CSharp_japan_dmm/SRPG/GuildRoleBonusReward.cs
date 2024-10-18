// Decompiled with JetBrains decompiler
// Type: SRPG.GuildRoleBonusReward
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class GuildRoleBonusReward
  {
    private RaidRewardType mType;
    private string mItemIname;
    private int mNum;

    public RaidRewardType type => this.mType;

    public string item_iname => this.mItemIname;

    public int num => this.mNum;

    public bool Deserialize(JSON_GUildRoleBonusReward json)
    {
      if (json == null)
        return false;
      this.mType = (RaidRewardType) json.item_type;
      this.mItemIname = json.item_iname;
      this.mNum = json.item_num;
      return true;
    }
  }
}
