// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.GAuth.AndApp.API.Request.AccessToken
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using Gsc.Auth.GAuth.GAuth.API.Generic;
using Gsc.Network.Support.MiniJsonHelper;
using System;
using System.Collections.Generic;

namespace Gsc.Auth.GAuth.AndApp.API.Request
{
  public class AccessToken : GAuthRequest<AccessToken, Gsc.Auth.GAuth.GAuth.API.Response.AccessToken>
  {
    private const string ___path = "{0}/authp-andapp/{1}/get_access_token";

    public AccessToken(string idToken)
    {
      this.IDToken = idToken;
    }

    public string IDToken { get; set; }

    public override string GetUrl()
    {
      return string.Format("{0}/authp-andapp/{1}/get_access_token", (object) SDK.Configuration.Env.NativeBaseUrl, (object) SDK.Configuration.AppName);
    }

    public override string GetPath()
    {
      return "{0}/authp-andapp/{1}/get_access_token";
    }

    public override string GetMethod()
    {
      return "POST";
    }

    protected override Dictionary<string, object> GetParameters()
    {
      Dictionary<string, object> dictionary1 = new Dictionary<string, object>();
      Dictionary<string, object> dictionary2 = dictionary1;
      string index = "andapp_idtoken";
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
      dictionary2[index] = obj;
      dictionary1["udid"] = (object) string.Empty;
      dictionary1["idfa"] = (object) string.Empty;
      dictionary1["idfv"] = (object) string.Empty;
      return dictionary1;
    }
  }
}
