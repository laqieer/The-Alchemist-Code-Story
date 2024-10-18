// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.GAuth.AndApp.API.Request.AccessToken
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Auth.GAuth.GAuth.API.Generic;
using Gsc.Network.Support.MiniJsonHelper;
using System;
using System.Collections.Generic;

#nullable disable
namespace Gsc.Auth.GAuth.AndApp.API.Request
{
  public class AccessToken : GAuthRequest<AccessToken, Gsc.Auth.GAuth.GAuth.API.Response.AccessToken>
  {
    private const string ___path = "{0}/authp-andapp/{1}/get_access_token";

    public AccessToken(string idToken) => this.IDToken = idToken;

    public string IDToken { get; set; }

    public override string GetUrl()
    {
      return string.Format("{0}/authp-andapp/{1}/get_access_token", (object) SDK.Configuration.Env.NativeBaseUrl, (object) SDK.Configuration.AppName);
    }

    public override string GetPath() => "{0}/authp-andapp/{1}/get_access_token";

    public override string GetMethod() => "POST";

    protected override Dictionary<string, object> GetParameters()
    {
      Dictionary<string, object> parameters = new Dictionary<string, object>();
      Dictionary<string, object> dictionary = parameters;
      Serializer instance = Serializer.Instance;
      // ISSUE: reference to a compiler-generated field
      if (AccessToken.\u003C\u003Ef__mg\u0024cache0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        AccessToken.\u003C\u003Ef__mg\u0024cache0 = new Func<string, object>(Serializer.From<string>);
      }
      // ISSUE: reference to a compiler-generated field
      Func<string, object> fMgCache0 = AccessToken.\u003C\u003Ef__mg\u0024cache0;
      object obj = instance.Add<string>(fMgCache0).Serialize<string>(this.IDToken);
      dictionary["andapp_idtoken"] = obj;
      parameters["udid"] = (object) string.Empty;
      parameters["idfa"] = (object) string.Empty;
      parameters["idfv"] = (object) string.Empty;
      return parameters;
    }
  }
}
