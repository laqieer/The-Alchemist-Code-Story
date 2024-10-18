// Decompiled with JetBrains decompiler
// Type: SRPG.AdvanceRewardDataParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class AdvanceRewardDataParam
  {
    public int ItemType;
    public string ItemIname;
    public int ItemNum;

    public void Deserialize(JSON_AdvanceRewardDataParam json)
    {
      if (json == null)
        return;
      this.ItemType = json.item_type;
      this.ItemIname = json.item_iname;
      this.ItemNum = json.item_num;
    }
  }
}
