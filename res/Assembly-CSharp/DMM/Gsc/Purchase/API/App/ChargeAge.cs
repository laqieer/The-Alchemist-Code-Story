// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.API.App.ChargeAge
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Support.MiniJsonHelper;
using System;
using System.Collections.Generic;

#nullable disable
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

    public override string GetPath() => SDK.Configuration.Env.PurchaseApiPrefix + "/age";

    public override string GetMethod() => "POST";

    protected override Dictionary<string, object> GetParameters()
    {
      Dictionary<string, object> parameters = new Dictionary<string, object>();
      Dictionary<string, object> dictionary1 = parameters;
      Serializer instance1 = Serializer.Instance;
      // ISSUE: reference to a compiler-generated field
      if (ChargeAge.\u003C\u003Ef__mg\u0024cache0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ChargeAge.\u003C\u003Ef__mg\u0024cache0 = new Func<int, object>(Serializer.From<int>);
      }
      // ISSUE: reference to a compiler-generated field
      Func<int, object> fMgCache0 = ChargeAge.\u003C\u003Ef__mg\u0024cache0;
      object obj1 = instance1.Add<int>(fMgCache0).Serialize<int>(this.BirthDay);
      dictionary1["birth_day"] = obj1;
      Dictionary<string, object> dictionary2 = parameters;
      Serializer instance2 = Serializer.Instance;
      // ISSUE: reference to a compiler-generated field
      if (ChargeAge.\u003C\u003Ef__mg\u0024cache1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ChargeAge.\u003C\u003Ef__mg\u0024cache1 = new Func<int, object>(Serializer.From<int>);
      }
      // ISSUE: reference to a compiler-generated field
      Func<int, object> fMgCache1 = ChargeAge.\u003C\u003Ef__mg\u0024cache1;
      object obj2 = instance2.Add<int>(fMgCache1).Serialize<int>(this.BirthYear);
      dictionary2["birth_year"] = obj2;
      Dictionary<string, object> dictionary3 = parameters;
      Serializer instance3 = Serializer.Instance;
      // ISSUE: reference to a compiler-generated field
      if (ChargeAge.\u003C\u003Ef__mg\u0024cache2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ChargeAge.\u003C\u003Ef__mg\u0024cache2 = new Func<int, object>(Serializer.From<int>);
      }
      // ISSUE: reference to a compiler-generated field
      Func<int, object> fMgCache2 = ChargeAge.\u003C\u003Ef__mg\u0024cache2;
      object obj3 = instance3.Add<int>(fMgCache2).Serialize<int>(this.BirthMonth);
      dictionary3["birth_month"] = obj3;
      return parameters;
    }
  }
}
