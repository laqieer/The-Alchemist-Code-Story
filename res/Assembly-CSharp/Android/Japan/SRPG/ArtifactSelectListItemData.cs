// Decompiled with JetBrains decompiler
// Type: SRPG.ArtifactSelectListItemData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
