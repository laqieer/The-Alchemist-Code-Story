// Decompiled with JetBrains decompiler
// Type: SRPG.AchievementParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
        return this.googleplay;
      }
    }
  }
}
