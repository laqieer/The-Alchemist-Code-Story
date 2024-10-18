// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.GAuth.GAuth.API.Request.RegisterEmailAddressAndPassword
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
      Dictionary<string, object> dictionary1 = new Dictionary<string, object>();
      Dictionary<string, object> dictionary2 = dictionary1;
      string index1 = "email";
      Serializer instance1 = Serializer.Instance;
      // ISSUE: reference to a compiler-generated field
      if (RegisterEmailAddressAndPassword.\u003C\u003Ef__mg\u0024cache0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RegisterEmailAddressAndPassword.\u003C\u003Ef__mg\u0024cache0 = new Func<string, object>(Serializer.From<string>);
      }
      // ISSUE: reference to a compiler-generated field
      Func<string, object> fMgCache0 = RegisterEmailAddressAndPassword.\u003C\u003Ef__mg\u0024cache0;
      object obj1 = instance1.Add<string>(fMgCache0).Serialize<string>(this.EmailAddress);
      dictionary2[index1] = obj1;
      Dictionary<string, object> dictionary3 = dictionary1;
      string index2 = "password";
      Serializer instance2 = Serializer.Instance;
      // ISSUE: reference to a compiler-generated field
      if (RegisterEmailAddressAndPassword.\u003C\u003Ef__mg\u0024cache1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RegisterEmailAddressAndPassword.\u003C\u003Ef__mg\u0024cache1 = new Func<string, object>(Serializer.From<string>);
      }
      // ISSUE: reference to a compiler-generated field
      Func<string, object> fMgCache1 = RegisterEmailAddressAndPassword.\u003C\u003Ef__mg\u0024cache1;
      object obj2 = instance2.Add<string>(fMgCache1).Serialize<string>(this.Password);
      dictionary3[index2] = obj2;
      Dictionary<string, object> dictionary4 = dictionary1;
      string index3 = "disable_validation_email";
      Serializer instance3 = Serializer.Instance;
      // ISSUE: reference to a compiler-generated field
      if (RegisterEmailAddressAndPassword.\u003C\u003Ef__mg\u0024cache2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RegisterEmailAddressAndPassword.\u003C\u003Ef__mg\u0024cache2 = new Func<bool, object>(Serializer.From<bool>);
      }
      // ISSUE: reference to a compiler-generated field
      Func<bool, object> fMgCache2 = RegisterEmailAddressAndPassword.\u003C\u003Ef__mg\u0024cache2;
      object obj3 = instance3.Add<bool>(fMgCache2).Serialize<bool>(this.DisableValidationEmail);
      dictionary4[index3] = obj3;
      Dictionary<string, object> dictionary5 = dictionary1;
      string index4 = "device_id";
      Serializer instance4 = Serializer.Instance;
      // ISSUE: reference to a compiler-generated field
      if (RegisterEmailAddressAndPassword.\u003C\u003Ef__mg\u0024cache3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RegisterEmailAddressAndPassword.\u003C\u003Ef__mg\u0024cache3 = new Func<string, object>(Serializer.From<string>);
      }
      // ISSUE: reference to a compiler-generated field
      Func<string, object> fMgCache3 = RegisterEmailAddressAndPassword.\u003C\u003Ef__mg\u0024cache3;
      object obj4 = instance4.Add<string>(fMgCache3).Serialize<string>(this.DeviceId);
      dictionary5[index4] = obj4;
      Dictionary<string, object> dictionary6 = dictionary1;
      string index5 = "secret_key";
      Serializer instance5 = Serializer.Instance;
      // ISSUE: reference to a compiler-generated field
      if (RegisterEmailAddressAndPassword.\u003C\u003Ef__mg\u0024cache4 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RegisterEmailAddressAndPassword.\u003C\u003Ef__mg\u0024cache4 = new Func<string, object>(Serializer.From<string>);
      }
      // ISSUE: reference to a compiler-generated field
      Func<string, object> fMgCache4 = RegisterEmailAddressAndPassword.\u003C\u003Ef__mg\u0024cache4;
      object obj5 = instance5.Add<string>(fMgCache4).Serialize<string>(this.SecretKey);
      dictionary6[index5] = obj5;
      return dictionary1;
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
