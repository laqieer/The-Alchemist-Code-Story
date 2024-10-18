// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.GAuth.GAuth.API.Request.Migrate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using Gsc.Auth.GAuth.GAuth.API.Generic;
using Gsc.Network.Support.MiniJsonHelper;
using System;
using System.Collections.Generic;

namespace Gsc.Auth.GAuth.GAuth.API.Request
{
  public class Migrate : GAuthRequest<Migrate, Gsc.Auth.GAuth.GAuth.API.Response.Migrate>
  {
    private const string ___path = "/migrate";

    public Migrate(string passcode, string secretKey, string deviceId)
    {
      this.Passcode = passcode;
      this.SecretKey = secretKey;
      this.DeviceId = deviceId;
    }

    public string Passcode { get; set; }

    public string SecretKey { get; set; }

    public string DeviceId { get; set; }

    public override string GetPath()
    {
      return SDK.Configuration.Env.AuthApiPrefix + "/migrate";
    }

    public override string GetMethod()
    {
      return "POST";
    }

    protected override Dictionary<string, object> GetParameters()
    {
      Dictionary<string, object> dictionary1 = new Dictionary<string, object>();
      Dictionary<string, object> dictionary2 = dictionary1;
      string index1 = "passcode";
      Serializer instance1 = Serializer.Instance;
      // ISSUE: reference to a compiler-generated field
      if (Migrate.\u003C\u003Ef__mg\u0024cache0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Migrate.\u003C\u003Ef__mg\u0024cache0 = new Func<string, object>(Serializer.From<string>);
      }
      // ISSUE: reference to a compiler-generated field
      Func<string, object> fMgCache0 = Migrate.\u003C\u003Ef__mg\u0024cache0;
      object obj1 = instance1.Add<string>(fMgCache0).Serialize<string>(this.Passcode);
      dictionary2[index1] = obj1;
      Dictionary<string, object> dictionary3 = dictionary1;
      string index2 = "secret_key";
      Serializer instance2 = Serializer.Instance;
      // ISSUE: reference to a compiler-generated field
      if (Migrate.\u003C\u003Ef__mg\u0024cache1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Migrate.\u003C\u003Ef__mg\u0024cache1 = new Func<string, object>(Serializer.From<string>);
      }
      // ISSUE: reference to a compiler-generated field
      Func<string, object> fMgCache1 = Migrate.\u003C\u003Ef__mg\u0024cache1;
      object obj2 = instance2.Add<string>(fMgCache1).Serialize<string>(this.SecretKey);
      dictionary3[index2] = obj2;
      Dictionary<string, object> dictionary4 = dictionary1;
      string index3 = "device_id";
      Serializer instance3 = Serializer.Instance;
      // ISSUE: reference to a compiler-generated field
      if (Migrate.\u003C\u003Ef__mg\u0024cache2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Migrate.\u003C\u003Ef__mg\u0024cache2 = new Func<string, object>(Serializer.From<string>);
      }
      // ISSUE: reference to a compiler-generated field
      Func<string, object> fMgCache2 = Migrate.\u003C\u003Ef__mg\u0024cache2;
      object obj3 = instance3.Add<string>(fMgCache2).Serialize<string>(this.DeviceId);
      dictionary4[index3] = obj3;
      return dictionary1;
    }
  }
}
