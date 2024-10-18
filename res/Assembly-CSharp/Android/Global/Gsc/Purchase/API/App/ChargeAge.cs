// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.API.App.ChargeAge
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using Gsc.Network.Support.MiniJsonHelper;
using System;
using System.Collections.Generic;

namespace Gsc.Purchase.API.App
{
  public class ChargeAge : GenericRequest<ChargeAge, Gsc.Purchase.API.Response.ChargeAge>
  {
    private const string ___path = "/age";

    public ChargeAge(int birthMonth, int birthYear)
    {
      this.BirthMonth = birthMonth;
      this.BirthYear = birthYear;
    }

    public int BirthDay { get; set; }

    public int BirthYear { get; set; }

    public int BirthMonth { get; set; }

    public override string GetPath()
    {
      return SDK.Configuration.Env.PurchaseApiPrefix + "/age";
    }

    public override string GetMethod()
    {
      return "POST";
    }

    protected override Dictionary<string, object> GetParameters()
    {
      return new Dictionary<string, object>()
      {
        ["birth_day"] = Serializer.Instance.Add<int>(new Func<int, object>(Serializer.From<int>)).Serialize<int>(this.BirthDay),
        ["birth_year"] = Serializer.Instance.Add<int>(new Func<int, object>(Serializer.From<int>)).Serialize<int>(this.BirthYear),
        ["birth_month"] = Serializer.Instance.Add<int>(new Func<int, object>(Serializer.From<int>)).Serialize<int>(this.BirthMonth)
      };
    }
  }
}
