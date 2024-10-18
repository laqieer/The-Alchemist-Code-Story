// Decompiled with JetBrains decompiler
// Type: SRPG.GenesisParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace SRPG
{
  public class GenesisParam
  {
    private string mIname;
    private bool mIsValid;
    private DateTime mBeginAt;
    private DateTime mEndAt;

    public string Iname
    {
      get
      {
        return this.mIname;
      }
    }

    public bool IsValid
    {
      get
      {
        return this.mIsValid;
      }
    }

    public DateTime BeginAt
    {
      get
      {
        return this.mBeginAt;
      }
    }

    public DateTime EndAt
    {
      get
      {
        return this.mEndAt;
      }
    }

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
      if (this.mBeginAt <= serverTime)
        return serverTime <= this.mEndAt;
      return false;
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
