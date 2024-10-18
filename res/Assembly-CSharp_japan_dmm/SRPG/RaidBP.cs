// Decompiled with JetBrains decompiler
// Type: SRPG.RaidBP
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class RaidBP
  {
    private int mCurrent = 5;
    private int mMax = 5;
    private DateTime mAt;
    private int mAddBPMinutes;

    public RaidBP(int addBPMinutes) => this.mAddBPMinutes = addBPMinutes;

    public int Current => this.mCurrent;

    public int Max => this.mMax;

    public DateTime At => this.mAt;

    public bool Deserialize(Json_RaidBP json)
    {
      this.mCurrent = json.pt;
      this.mMax = json.max;
      this.mAt = TimeManager.FromUnixTime(json.at).AddMinutes((double) this.mAddBPMinutes);
      return true;
    }

    public void AddPoint() => ++this.mCurrent;

    public void AddMinutes() => this.mAt = this.mAt.AddMinutes((double) this.mAddBPMinutes);
  }
}
