// Decompiled with JetBrains decompiler
// Type: SRPG.AchievementParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class AchievementParam
  {
    public int id;
    public string iname;
    public string ios;
    public string googleplay;

    public bool Deserialize(JSON_AchievementParam json)
    {
      if (json == null)
        return false;
      this.id = json.fields.id;
      this.iname = json.fields.iname;
      this.ios = json.fields.ios;
      this.googleplay = json.fields.googleplay;
      return true;
    }

    public string AchievementID
    {
      get
      {
        return string.Empty;
      }
    }
  }
}
