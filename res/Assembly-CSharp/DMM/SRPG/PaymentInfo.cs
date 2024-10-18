// Decompiled with JetBrains decompiler
// Type: SRPG.PaymentInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class PaymentInfo
  {
    public string productId;
    public long at;
    private int _numMonghly;

    public PaymentInfo()
    {
    }

    public PaymentInfo(string productId_, int numMonthly_)
    {
      this.productId = productId_;
      this._numMonghly = numMonthly_;
      this.at = Network.GetServerTime();
    }

    public int numMonthly
    {
      get
      {
        DateTime dateTime1 = TimeManager.FromUnixTime(Network.GetServerTime());
        DateTime dateTime2 = TimeManager.FromUnixTime(this.at);
        if (dateTime1.Year > dateTime2.Year || dateTime1.Month > dateTime2.Month)
          this._numMonghly = 0;
        return this._numMonghly;
      }
    }

    public void AddNum(int num = 1)
    {
      this._numMonghly = this.numMonthly + num;
      this.at = Network.GetServerTime();
    }

    public bool Deserialize(Json_PaymentInfo json)
    {
      this.productId = json.pid;
      this._numMonghly = json.num_m;
      this.at = json.at;
      return true;
    }
  }
}
