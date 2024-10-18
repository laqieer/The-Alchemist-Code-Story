// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.GAuth.AndApp.API.Request.AccessToken
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
      return new Dictionary<string, object>()
      {
        ["andapp_idtoken"] = Serializer.Instance.Add<string>(new Func<string, object>(Serializer.From<string>)).Serialize<string>(this.IDToken),
        ["udid"] = (object) string.Empty,
        ["idfa"] = (object) string.Empty,
        ["idfv"] = (object) string.Empty
      };
    }
  }
}
