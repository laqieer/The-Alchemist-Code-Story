// Decompiled with JetBrains decompiler
// Type: SRPG.ViewGuildData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class ViewGuildData
  {
    public int id;
    public string name;
    public string award_id;
    public int level;
    public int count;
    public int max_count;

    public void Deserialize(JSON_ViewGuild json)
    {
      this.id = json.id;
      this.name = json.name;
      this.award_id = json.award_id;
      this.level = json.level;
      this.count = json.count;
      this.max_count = json.max_count;
    }
  }
}
