// Decompiled with JetBrains decompiler
// Type: SRPG.ArtifactSelectListItemData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ArtifactSelectListItemData
  {
    public string iname;
    public int id;
    public int num;
    public ArtifactParam param;

    public void Deserialize(Json_ArtifactSelectItem json)
    {
      this.iname = json.iname;
      this.id = (int) json.id;
      this.num = (int) json.num;
    }
  }
}
