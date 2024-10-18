// Decompiled with JetBrains decompiler
// Type: SRPG.ArtifactSelectListItemData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
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
