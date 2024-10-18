// Decompiled with JetBrains decompiler
// Type: SRPG.AppealGachaMaster
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class AppealGachaMaster
  {
    public string appeal_id;
    public long start_at;
    public long end_at;
    public bool is_new;

    public bool Deserialize(JSON_AppealGachaMaster json)
    {
      if (json == null)
        return false;
      this.appeal_id = json.fields.appeal_id;
      this.start_at = TimeManager.FromDateTime(DateTime.Parse(json.fields.start_at));
      this.end_at = TimeManager.FromDateTime(DateTime.Parse(json.fields.end_at));
      this.is_new = json.fields.flag_new != 0;
      return true;
    }
  }
}
