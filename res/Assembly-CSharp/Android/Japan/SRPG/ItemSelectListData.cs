// Decompiled with JetBrains decompiler
// Type: SRPG.ItemSelectListData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;

namespace SRPG
{
  public class ItemSelectListData
  {
    public List<ItemSelectListItemData> items;

    public void Deserialize(Json_ItemSelectResponse json)
    {
      if (json == null || json.select == null)
        return;
      this.items = new List<ItemSelectListItemData>();
      for (int index = 0; index < json.select.Length; ++index)
      {
        this.items.Add(new ItemSelectListItemData());
        this.items[index].Deserialize(json.select[index]);
        this.items[index].param = MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(this.items[index].iiname);
      }
    }
  }
}
