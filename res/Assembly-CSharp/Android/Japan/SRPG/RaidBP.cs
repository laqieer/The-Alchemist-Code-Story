// Decompiled with JetBrains decompiler
// Type: SRPG.RaidBP
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class RaidBP
  {
    private int mCurrent = 5;
    private int mMax = 5;
    private DateTime mAt;
    private int mAddBPMinutes;

    public RaidBP(int addBPMinutes)
    {
      this.mAddBPMinutes = addBPMinutes;
    }

    public int Current
    {
      get
      {
        return this.mCurrent;
      }
    }

    public int Max
    {
      get
      {
        return this.mMax;
      }
    }

    public DateTime At
    {
      get
      {
        return this.mAt;
      }
    }

    public bool Deserialize(Json_RaidBP json)
    {
      this.mCurrent = json.pt;
      this.mMax = json.max;
      this.mAt = TimeManager.FromUnixTime(json.at).AddMinutes((double) this.mAddBPMinutes);
      return true;
    }

    public void AddPoint()
    {
      ++this.mCurrent;
    }

    public void AddMinutes()
    {
      this.mAt = this.mAt.AddMinutes((double) this.mAddBPMinutes);
    }
  }
}
