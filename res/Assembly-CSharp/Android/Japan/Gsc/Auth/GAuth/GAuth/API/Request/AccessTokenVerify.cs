// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.GAuth.GAuth.API.Request.AccessTokenVerify
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
