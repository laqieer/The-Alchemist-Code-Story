// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.GAuth.GAuth.API.Request.AccessTokenVerify
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Auth.GAuth.GAuth.API.Generic;

#nullable disable
namespace Gsc.Auth.GAuth.GAuth.API.Request
{
  public class AccessTokenVerify : GAuthRequest<AccessTokenVerify, Gsc.Auth.GAuth.GAuth.API.Response.AccessTokenVerify>
  {
    private const string ___path = "/v2/accesstoken/verify";

    public override string GetPath()
    {
      return SDK.Configuration.Env.AuthApiPrefix + "/v2/accesstoken/verify";
    }

    public override string GetMethod() => "POST";
  }
}
