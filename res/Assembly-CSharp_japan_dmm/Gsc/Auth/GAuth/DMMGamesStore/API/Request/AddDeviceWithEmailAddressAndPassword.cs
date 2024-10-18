﻿// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.GAuth.DMMGamesStore.API.Request.AddDeviceWithEmailAddressAndPassword
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Auth.GAuth.GAuth.API.Generic;
using Gsc.DOM;
using Gsc.Network;
using Gsc.Network.Support.MiniJsonHelper;
using System;
using System.Collections.Generic;

#nullable disable
namespace Gsc.Auth.GAuth.DMMGamesStore.API.Request
{
  public class AddDeviceWithEmailAddressAndPassword : 
    Gsc.Network.Request<AddDeviceWithEmailAddressAndPassword, AddDeviceWithEmailAddressAndPassword.Response>
  {
    private const string ___path = "{0}/dmm-auth-proxy/{1}/add_device";

    public AddDeviceWithEmailAddressAndPassword(
      int viewerId,
      string onetimeToken,
      string emailAddress,
      string password)
    {
      this.ViewerID = viewerId;
      this.OnetimeToken = onetimeToken;
      this.EmailAddress = emailAddress;
      this.Password = password;
    }

    public int ViewerID { get; set; }

    public string OnetimeToken { get; set; }

    public string EmailAddress { get; set; }

    public string Password { get; set; }

    public override string GetUrl()
    {
      return string.Format("{0}/dmm-auth-proxy/{1}/add_device", (object) SDK.Configuration.Env.NativeBaseUrl, (object) SDK.Configuration.AppName);
    }

    public override string GetPath() => "{0}/dmm-auth-proxy/{1}/add_device";

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
        AddDeviceWithEmailAddressAndPassword.\u003C\u003Ef__mg\u0024cache0 = new Func<int, object>(Serializer.From<int>);
      }
      // ISSUE: reference to a compiler-generated field
      Func<int, object> fMgCache0 = AddDeviceWithEmailAddressAndPassword.\u003C\u003Ef__mg\u0024cache0;
      object obj1 = instance1.Add<int>(fMgCache0).Serialize<int>(this.ViewerID);
      dictionary1["dmm_viewer_id"] = obj1;
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
      object obj2 = instance2.Add<string>(fMgCache1).Serialize<string>(this.OnetimeToken);
      dictionary2["dmm_onetime_token"] = obj2;
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
      object obj3 = instance3.Add<string>(fMgCache2).Serialize<string>(this.EmailAddress);
      dictionary3["email"] = obj3;
      Dictionary<string, object> dictionary4 = parameters;
      Serializer instance4 = Serializer.Instance;
      // ISSUE: reference to a compiler-generated field
      if (AddDeviceWithEmailAddressAndPassword.\u003C\u003Ef__mg\u0024cache3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        AddDeviceWithEmailAddressAndPassword.\u003C\u003Ef__mg\u0024cache3 = new Func<string, object>(Serializer.From<string>);
      }
      // ISSUE: reference to a compiler-generated field
      Func<string, object> fMgCache3 = AddDeviceWithEmailAddressAndPassword.\u003C\u003Ef__mg\u0024cache3;
      object obj4 = instance4.Add<string>(fMgCache3).Serialize<string>(this.Password);
      dictionary4["password"] = obj4;
      parameters["idfv"] = (object) string.Empty;
      return parameters;
    }

    public override Type GetErrorResponseType() => typeof (Gsc.Auth.GAuth.GAuth.API.Response.ErrorResponse);

    public class Response : GAuthResponse<AddDeviceWithEmailAddressAndPassword.Response>
    {
      public Response(WebInternalResponse response)
      {
        using (IDocument document = this.Parse(response))
          this.IsSucceeded = document.Root["is_succeeded"].ToBool();
      }

      public bool IsSucceeded { get; private set; }
    }
  }
}
