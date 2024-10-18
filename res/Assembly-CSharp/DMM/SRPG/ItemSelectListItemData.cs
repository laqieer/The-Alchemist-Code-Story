// Decompiled with JetBrains decompiler
// Type: SRPG.ItemSelectListItemData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class ItemSelectListItemData
  {
    public string iiname;
    public short id;
    public short num;
    public ItemParam param;

    public void Deserialize(Json_ItemSelectItem json)
    {
      this.iiname = json.iname;
      this.id = json.id;
      this.num = json.num;
    }
  }
}
