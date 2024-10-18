// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.GAuth.GAuth.API.Request.AccessToken
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Auth.GAuth.GAuth.API.Generic;
using Gsc.Network.Support.MiniJsonHelper;
using System;
using System.Collections.Generic;

#nullable disable
namespace Gsc.Auth.GAuth.GAuth.API.Request
{
  public class AccessToken : GAuthRequest<AccessToken, Gsc.Auth.GAuth.GAuth.API.Response.AccessToken>
  {
    private const string ___path = "/accesstoken";

    public AccessToken(string secretKey, string deviceId)
    {
      this.SecretKey = secretKey;
      this.DeviceId = deviceId;
    }

    public string SecretKey { get; set; }

    public string Idfv { get; set; }

    public string Idfa { get; set; }

    public string DeviceId { get; set; }

    public override string GetPath() => SDK.Configuration.Env.AuthApiPrefix + "/accesstoken";

    public override string GetMethod() => "POST";

    protected override Dictionary<string, object> GetParameters()
    {
      Dictionary<string, object> parameters = new Dictionary<string, object>();
      Dictionary<string, object> dictionary1 = parameters;
      Serializer instance1 = Serializer.Instance;
      // ISSUE: reference to a compiler-generated field
      if (AccessToken.\u003C\u003Ef__mg\u0024cache0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        AccessToken.\u003C\u003Ef__mg\u0024cache0 = new Func<string, object>(Serializer.From<string>);
      }
      // ISSUE: reference to a compiler-generated field
      Func<string, object> fMgCache0 = AccessToken.\u003C\u003Ef__mg\u0024cache0;
      object obj1 = instance1.Add<string>(fMgCache0).Serialize<string>(this.SecretKey);
      dictionary1["secret_key"] = obj1;
      Dictionary<string, object> dictionary2 = parameters;
      Serializer instance2 = Serializer.Instance;
      // ISSUE: reference to a compiler-generated field
      if (AccessToken.\u003C\u003Ef__mg\u0024cache1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        AccessToken.\u003C\u003Ef__mg\u0024cache1 = new Func<string, object>(Serializer.From<string>);
      }
      // ISSUE: reference to a compiler-generated field
      Func<string, object> fMgCache1 = AccessToken.\u003C\u003Ef__mg\u0024cache1;
      object obj2 = instance2.Add<string>(fMgCache1).Serialize<string>(this.DeviceId);
      dictionary2["device_id"] = obj2;
      Dictionary<string, object> dictionary3 = parameters;
      Serializer instance3 = Serializer.Instance;
      // ISSUE: reference to a compiler-generated field
      if (AccessToken.\u003C\u003Ef__mg\u0024cache2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        AccessToken.\u003C\u003Ef__mg\u0024cache2 = new Func<string, object>(Serializer.From<string>);
      }
      // ISSUE: reference to a compiler-generated field
      Func<string, object> fMgCache2 = AccessToken.\u003C\u003Ef__mg\u0024cache2;
      object obj3 = instance3.Add<string>(fMgCache2).Serialize<string>(this.Idfv);
      dictionary3["idfv"] = obj3;
      Dictionary<string, object> dictionary4 = parameters;
      Serializer instance4 = Serializer.Instance;
      // ISSUE: reference to a compiler-generated field
      if (AccessToken.\u003C\u003Ef__mg\u0024cache3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        AccessToken.\u003C\u003Ef__mg\u0024cache3 = new Func<string, object>(Serializer.From<string>);
      }
      // ISSUE: reference to a compiler-generated field
      Func<string, object> fMgCache3 = AccessToken.\u003C\u003Ef__mg\u0024cache3;
      object obj4 = instance4.Add<string>(fMgCache3).Serialize<string>(this.Idfa);
      dictionary4["idfa"] = obj4;
      return parameters;
    }
  }
}
