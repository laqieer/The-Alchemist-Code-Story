// Decompiled with JetBrains decompiler
// Type: SRPG.GvGUsedUnitData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class GvGUsedUnitData
  {
    public long uiid;
    public int remainDay;
    public DateTime usedDay;

    public void Deserialize(JSON_GvGUsedUnitData json)
    {
      if (json == null)
        return;
      this.uiid = (long) json.uiid;
      DateTime result1;
      if (!DateTime.TryParse(json.limit, out result1))
        return;
      result1 = result1.AddDays(1.0);
      DateTime result2;
      if (!DateTime.TryParse(json.used, out this.usedDay) || !DateTime.TryParse(TimeManager.ServerTime.ToString("yyyy/MM/dd 00:00:00"), out result2))
        return;
      this.remainDay = (result1 - result2).Days;
    }
  }
}
