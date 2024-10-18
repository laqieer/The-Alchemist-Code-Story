// Decompiled with JetBrains decompiler
// Type: SRPG.GiftRecieveItemData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class GiftRecieveItemData
  {
    public string iname;
    public int rarity;
    public int num;
    public string name;
    public GiftTypes type;

    public void Set(string iname, GiftTypes giftTipe, int rarity, int num)
    {
      this.iname = iname;
      this.type = giftTipe;
      this.rarity = rarity;
      this.num = num;
    }
  }
}
