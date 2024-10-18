// Decompiled with JetBrains decompiler
// Type: SRPG.UnitSelectListData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class UnitSelectListData
  {
    public List<UnitSelectListItemData> items;

    public void Deserialize(Json_UnitSelectResponse json)
    {
      if (json == null || json.select == null)
        return;
      this.items = new List<UnitSelectListItemData>();
      for (int index = 0; index < json.select.Length; ++index)
      {
        this.items.Add(new UnitSelectListItemData());
        this.items[index].Deserialize(json.select[index]);
        this.items[index].param = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(this.items[index].iname);
      }
    }
  }
}
