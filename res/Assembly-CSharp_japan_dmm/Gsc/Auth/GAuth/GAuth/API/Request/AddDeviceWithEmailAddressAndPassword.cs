// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.GAuth.GAuth.API.Request.AddDeviceWithEmailAddressAndPassword
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Auth.GAuth.GAuth.API.Generic;
using Gsc.Network;
using Gsc.Network.Support.MiniJsonHelper;
using System;
using System.Collections.Generic;

#nullable disable
namespace Gsc.Auth.GAuth.GAuth.API.Request
{
  public class AddDeviceWithEmailAddressAndPassword : 
    GAuthRequest<AddDeviceWithEmailAddressAndPassword, Gsc.Auth.GAuth.GAuth.API.Response.AddDeviceWithEmailAddressAndPassword>
  {
    private const string ___path = "/auth/email/device";

    public AddDeviceWithEmailAddressAndPassword(string emailAddress, string password)
    {
      this.EmailAddress = emailAddress;
      this.Password = password;
    }

    public string EmailAddress { get; set; }

    public string Password { get; set; }

    public string Idfv { get; set; }

    public override string GetPath() => "/auth/email/device";

    public override string GetMethod() => "POST";

    protected override Dictionary<string, object> GetParameters()
    {
      Dictionary<string, object> parameters = new Dictionary<string, object>();
      Dictionary<string, object> dictionary1 = parameters;
      Serializer instance1 = Serializer.Instance;
      // ISSUE: reference to a compiler-generated field
      if (AddDeviceWithEmailAddressAndPassword.\u003C\u003Ef__mg\u0024cache0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        AddDeviceWithEmailAddressAndPassword.\u003C\u003Ef__mg\u0024cache0 = new Func<string, object>(Serializer.From<string>);
      }
      // ISSUE: reference to a compiler-generated field
      Func<string, object> fMgCache0 = AddDeviceWithEmailAddressAndPassword.\u003C\u003Ef__mg\u0024cache0;
      object obj1 = instance1.Add<string>(fMgCache0).Serialize<string>(this.EmailAddress);
      dictionary1["email"] = obj1;
      Dictionary<string, object> dictionary2 = parameters;
      Serializer instance2 = Serializer.Instance;
      // ISSUE: reference to a compiler-generated field
      if (AddDeviceWithEmailAddressAndPassword.\u003C\u003Ef__mg\u0024cache1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        AddDeviceWithEmailAddressAndPassword.\u003C\u003Ef__mg\u0024cache1 = new Func<string, object>(Serializer.From<string>);
      }
      // ISSUE: reference to a compiler-generated field
      Func<string, object> fMgCache1 = AddDeviceWithEmailAddressAndPassword.\u003C\u003Ef__mg\u0024cache1;
      object obj2 = instance2.Add<string>(fMgCache1).Serialize<string>(this.Password);
      dictionary2["password"] = obj2;
      Dictionary<string, object> dictionary3 = parameters;
      Serializer instance3 = Serializer.Instance;
      // ISSUE: reference to a compiler-generated field
      if (AddDeviceWithEmailAddressAndPassword.\u003C\u003Ef__mg\u0024cache2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        AddDeviceWithEmailAddressAndPassword.\u003C\u003Ef__mg\u0024cache2 = new Func<string, object>(Serializer.From<string>);
      }
      // ISSUE: reference to a compiler-generated field
      Func<string, object> fMgCache2 = AddDeviceWithEmailAddressAndPassword.\u003C\u003Ef__mg\u0024cache2;
      object obj3 = instance3.Add<string>(fMgCache2).Serialize<string>(this.Idfv);
      dictionary3["idfv"] = obj3;
      return parameters;
    }

    public override Type GetErrorResponseType() => typeof (Gsc.Auth.GAuth.GAuth.API.Response.ErrorResponse);

    public override WebTaskResult InquireResult(WebTaskResult result, WebInternalResponse response)
    {
      return response.StatusCode == 400 && response.Payload != null && response.Payload.Length > 0 && (response.ContentType == ContentType.ApplicationJson || response.ContentType == ContentType.ApplicationOctetStream_Json_AES) ? WebTaskResult.MustErrorHandle : result;
    }

    protected override bool IsParameterUseParam() => false;
  }
}
