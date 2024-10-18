// Decompiled with JetBrains decompiler
// Type: SRPG.TimeRecoveryValue
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class TimeRecoveryValue
  {
    public OInt val;
    public OInt valMax;
    public OInt valRecover;
    public OLong interval;
    public OLong at;
    private float lastUpdateTime = -1f;

    public void Update()
    {
      if ((int) this.val >= (int) this.valMax || (double) this.lastUpdateTime == (double) Time.realtimeSinceStartup)
        return;
      this.lastUpdateTime = Time.realtimeSinceStartup;
      long num1 = Network.GetServerTime() - (long) this.at;
      long at = (long) this.at;
      long interval = (long) this.interval;
      int num2 = (int) (num1 / interval);
      this.at = (OLong) (at + (long) num2 * interval);
      this.val = (OInt) Math.Min((int) this.val + num2, (int) this.valMax);
    }

    public long GetNextRecoverySec()
    {
      if ((int) this.val >= (int) this.valMax)
        return 0;
      long num1 = Network.GetServerTime() - (long) this.at;
      int num2 = (int) (num1 / (long) this.interval);
      return (long) this.interval - (num1 - (long) this.interval * (long) num2);
    }

    public void SubValue(int subval)
    {
      this.at = (OLong) Network.GetServerTime();
      this.val = (OInt) Math.Max((int) this.val - subval, 0);
    }
  }
}
