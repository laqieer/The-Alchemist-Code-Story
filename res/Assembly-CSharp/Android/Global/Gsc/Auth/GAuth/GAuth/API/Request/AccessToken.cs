// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.GAuth.GAuth.API.Request.AccessToken
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using Gsc.Auth.GAuth.GAuth.API.Generic;
using Gsc.Network.Support.MiniJsonHelper;
using System;
using System.Collections.Generic;

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

    public override string GetPath()
    {
      return SDK.Configuration.Env.AuthApiPrefix + "/accesstoken";
    }

    public override string GetMethod()
    {
      return "POST";
    }

    protected override Dictionary<string, object> GetParameters()
    {
      return new Dictionary<string, object>()
      {
        ["secret_key"] = Serializer.Instance.Add<string>(new Func<string, object>(Serializer.From<string>)).Serialize<string>(this.SecretKey),
        ["device_id"] = Serializer.Instance.Add<string>(new Func<string, object>(Serializer.From<string>)).Serialize<string>(this.DeviceId),
        ["idfv"] = Serializer.Instance.Add<string>(new Func<string, object>(Serializer.From<string>)).Serialize<string>(this.Idfv),
        ["idfa"] = Serializer.Instance.Add<string>(new Func<string, object>(Serializer.From<string>)).Serialize<string>(this.Idfa)
      };
    }
  }
}
