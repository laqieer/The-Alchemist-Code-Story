// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.API.App.ChargeAge
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
      Dictionary<string, object> dictionary1 = new Dictionary<string, object>();
      Dictionary<string, object> dictionary2 = dictionary1;
      string index1 = "birth_day";
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
      dictionary2[index1] = obj1;
      Dictionary<string, object> dictionary3 = dictionary1;
      string index2 = "birth_year";
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
      dictionary3[index2] = obj2;
      Dictionary<string, object> dictionary4 = dictionary1;
      string index3 = "birth_month";
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
      dictionary4[index3] = obj3;
      return dictionary1;
    }
  }
}
