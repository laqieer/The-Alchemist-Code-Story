// Decompiled with JetBrains decompiler
// Type: SRPG.ItemSelectListItemData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
