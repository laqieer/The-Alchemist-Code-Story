// Decompiled with JetBrains decompiler
// Type: SRPG.ArtifactSelectListData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;

namespace SRPG
{
  public class ArtifactSelectListData
  {
    public List<ArtifactSelectListItemData> items;

    public void Deserialize(Json_ArtifactSelectResponse json)
    {
      if (json == null || json.select == null)
        return;
      this.items = new List<ArtifactSelectListItemData>();
      for (int index = 0; index < json.select.Length; ++index)
      {
        this.items.Add(new ArtifactSelectListItemData());
        this.items[index].Deserialize(json.select[index]);
        this.items[index].param = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(this.items[index].iname);
      }
    }
  }
}
