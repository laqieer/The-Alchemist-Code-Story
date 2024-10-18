// Decompiled with JetBrains decompiler
// Type: SRPG.GvGRewardDetailParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class GvGRewardDetailParam
  {
    public RaidRewardType Type { get; private set; }

    public string IName { get; private set; }

    public int Num { get; private set; }

    public bool Deserialize(JSON_GvGRewardDetailParam json)
    {
      if (json == null)
        return false;
      this.Type = (RaidRewardType) json.item_type;
      this.IName = json.item_iname;
      this.Num = json.item_num;
      return true;
    }
  }
}
