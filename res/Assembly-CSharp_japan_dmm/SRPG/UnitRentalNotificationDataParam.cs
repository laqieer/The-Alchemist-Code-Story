// Decompiled with JetBrains decompiler
// Type: SRPG.UnitRentalNotificationDataParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class UnitRentalNotificationDataParam
  {
    private int[] mPers;
    private string mSysId;

    public List<int> PerList
    {
      get => this.mPers != null ? new List<int>((IEnumerable<int>) this.mPers) : new List<int>();
    }

    public string SysId => this.mSysId;

    public void Deserialize(JSON_UnitRentalNotificationDataParam json)
    {
      if (json == null)
        return;
      if (json.pers != null)
      {
        this.mPers = new int[json.pers.Length];
        for (int index = 0; index < json.pers.Length; ++index)
          this.mPers[index] = json.pers[index];
      }
      this.mSysId = json.sys_id;
    }
  }
}
