// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.GAuth.GAuth.API.Request.AccessTokenVerify
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using Gsc.Auth.GAuth.GAuth.API.Generic;

namespace Gsc.Auth.GAuth.GAuth.API.Request
{
  public class AccessTokenVerify : GAuthRequest<AccessTokenVerify, Gsc.Auth.GAuth.GAuth.API.Response.AccessTokenVerify>
  {
    private const string ___path = "/v2/accesstoken/verify";

    public override string GetPath()
    {
      return SDK.Configuration.Env.AuthApiPrefix + "/v2/accesstoken/verify";
    }

    public override string GetMethod()
    {
      return "POST";
    }
  }
}
