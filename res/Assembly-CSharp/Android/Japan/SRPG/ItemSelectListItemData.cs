// Decompiled with JetBrains decompiler
// Type: SRPG.ItemSelectListItemData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
