// Decompiled with JetBrains decompiler
// Type: SRPG.GachaPickupSelectParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class GachaPickupSelectParam
  {
    private int mSelectPickupNum;
    private List<GachaDropData> mPickupSelectList = new List<GachaDropData>();

    public int select_pickup_num => this.mSelectPickupNum;

    public List<GachaDropData> pickup_select_list => this.mPickupSelectList;

    public void Setup(int num, Json_GachaPickups[] jsons)
    {
      this.mSelectPickupNum = num;
      this.mPickupSelectList.Clear();
      for (int index = 0; index < jsons.Length; ++index)
      {
        Json_GachaPickups json = jsons[index];
        if (json != null)
        {
          GachaDropData gachaDropData = new GachaDropData();
          gachaDropData.Deserialize(json);
          this.mPickupSelectList.Add(gachaDropData);
        }
      }
    }
  }
}
