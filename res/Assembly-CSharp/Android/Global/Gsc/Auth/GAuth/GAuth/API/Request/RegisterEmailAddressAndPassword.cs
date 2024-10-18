// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.GAuth.GAuth.API.Request.RegisterEmailAddressAndPassword
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using Gsc.Auth.GAuth.GAuth.API.Generic;
using Gsc.Network;
using Gsc.Network.Support.MiniJsonHelper;
using System;
using System.Collections.Generic;

namespace Gsc.Auth.GAuth.GAuth.API.Request
{
  public class RegisterEmailAddressAndPassword : GAuthRequest<RegisterEmailAddressAndPassword, Gsc.Auth.GAuth.GAuth.API.Response.RegisterEmailAddressAndPassword>
  {
    private const string ___path = "/auth/email/register";

    public RegisterEmailAddressAndPassword(string deviceId, string secretKey, string emailAddress, string password)
    {
      this.DeviceId = deviceId;
      this.SecretKey = secretKey;
      this.EmailAddress = emailAddress;
      this.Password = password;
    }

    public string DeviceId { get; set; }

    public string SecretKey { get; set; }

    public string EmailAddress { get; set; }

    public string Password { get; set; }

    public bool DisableValidationEmail { get; set; }

    public override string GetPath()
    {
      return "/auth/email/register";
    }

    public override string GetMethod()
    {
      return "POST";
    }

    protected override Dictionary<string, object> GetParameters()
    {
      return new Dictionary<string, object>()
      {
        ["email"] = Serializer.Instance.Add<string>(new Func<string, object>(Serializer.From<string>)).Serialize<string>(this.EmailAddress),
        ["password"] = Serializer.Instance.Add<string>(new Func<string, object>(Serializer.From<string>)).Serialize<string>(this.Password),
        ["disable_validation_email"] = Serializer.Instance.Add<bool>(new Func<bool, object>(Serializer.From<bool>)).Serialize<bool>(this.DisableValidationEmail),
        ["device_id"] = Serializer.Instance.Add<string>(new Func<string, object>(Serializer.From<string>)).Serialize<string>(this.DeviceId),
        ["secret_key"] = Serializer.Instance.Add<string>(new Func<string, object>(Serializer.From<string>)).Serialize<string>(this.SecretKey)
      };
    }

    public override Type GetErrorResponseType()
    {
      return typeof (Gsc.Auth.GAuth.GAuth.API.Response.ErrorResponse);
    }

    public override WebTaskResult InquireResult(WebTaskResult result, WebInternalResponse response)
    {
      if (response.StatusCode == 400 && response.Payload != null && (response.Payload.Length > 0 && response.ContentType == ContentType.ApplicationJson))
        return WebTaskResult.MustErrorHandle;
      return result;
    }

    protected override bool IsParameterUseParam()
    {
      return false;
    }
  }
}
