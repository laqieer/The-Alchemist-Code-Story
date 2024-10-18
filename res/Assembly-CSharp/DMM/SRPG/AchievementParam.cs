// Decompiled with JetBrains decompiler
// Type: SRPG.AchievementParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
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

    public string AchievementID => string.Empty;
  }
}
