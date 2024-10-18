// Decompiled with JetBrains decompiler
// Type: SRPG.GenesisParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class GenesisParam
  {
    private string mIname;
    private bool mIsValid;
    private DateTime mBeginAt;
    private DateTime mEndAt;

    public string Iname => this.mIname;

    public bool IsValid => this.mIsValid;

    public DateTime BeginAt => this.mBeginAt;

    public DateTime EndAt => this.mEndAt;

    public void Deserialize(JSON_GenesisParam json)
    {
      if (json == null)
        return;
      this.mIname = json.iname;
      this.mIsValid = json.is_valid != 0;
      this.mBeginAt = DateTime.MinValue;
      if (!string.IsNullOrEmpty(json.begin_at))
        DateTime.TryParse(json.begin_at, out this.mBeginAt);
      this.mEndAt = DateTime.MaxValue;
      if (string.IsNullOrEmpty(json.end_at))
        return;
      DateTime.TryParse(json.end_at, out this.mEndAt);
    }

    public bool IsWithinPeriod()
    {
      DateTime serverTime = TimeManager.ServerTime;
      return this.mBeginAt <= serverTime && serverTime <= this.mEndAt;
    }

    public static void Deserialize(ref List<GenesisParam> list, JSON_GenesisParam[] json)
    {
      if (json == null)
        return;
      if (list == null)
        list = new List<GenesisParam>(json.Length);
      list.Clear();
      for (int index = 0; index < json.Length; ++index)
      {
        GenesisParam genesisParam = new GenesisParam();
        genesisParam.Deserialize(json[index]);
        list.Add(genesisParam);
      }
    }
  }
}
