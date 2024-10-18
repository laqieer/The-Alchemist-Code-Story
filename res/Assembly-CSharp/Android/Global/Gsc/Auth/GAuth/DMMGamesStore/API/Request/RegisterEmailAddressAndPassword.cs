// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.GAuth.DMMGamesStore.API.Request.RegisterEmailAddressAndPassword
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using Gsc.Auth.GAuth.GAuth.API.Generic;
using Gsc.DOM;
using Gsc.Network;
using Gsc.Network.Support.MiniJsonHelper;
using System;
using System.Collections.Generic;

namespace Gsc.Auth.GAuth.DMMGamesStore.API.Request
{
  public class RegisterEmailAddressAndPassword : Gsc.Network.Request<RegisterEmailAddressAndPassword, RegisterEmailAddressAndPassword.Response>
  {
    private const string ___path = "{0}/dmm-auth-proxy/{1}/register";

    public RegisterEmailAddressAndPassword(int viewerId, string onetimeToken, string emailAddress, string password)
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

    public bool DisableValidationEmail { get; set; }

    public override string GetUrl()
    {
      return string.Format("{0}/dmm-auth-proxy/{1}/register", (object) SDK.Configuration.Env.NativeBaseUrl, (object) SDK.Configuration.AppName);
    }

    public override string GetPath()
    {
      return "{0}/dmm-auth-proxy/{1}/register";
    }

    public override string GetMethod()
    {
      return "POST";
    }

    protected override Dictionary<string, object> GetParameters()
    {
      return new Dictionary<string, object>()
      {
        ["dmm_viewer_id"] = Serializer.Instance.Add<int>(new Func<int, object>(Serializer.From<int>)).Serialize<int>(this.ViewerID),
        ["dmm_onetime_token"] = Serializer.Instance.Add<string>(new Func<string, object>(Serializer.From<string>)).Serialize<string>(this.OnetimeToken),
        ["email"] = Serializer.Instance.Add<string>(new Func<string, object>(Serializer.From<string>)).Serialize<string>(this.EmailAddress),
        ["password"] = Serializer.Instance.Add<string>(new Func<string, object>(Serializer.From<string>)).Serialize<string>(this.Password),
        ["disable_validation_email"] = Serializer.Instance.Add<bool>(new Func<bool, object>(Serializer.From<bool>)).Serialize<bool>(this.DisableValidationEmail)
      };
    }

    public override Type GetErrorResponseType()
    {
      return typeof (Gsc.Auth.GAuth.GAuth.API.Response.ErrorResponse);
    }

    public class Response : GAuthResponse<RegisterEmailAddressAndPassword.Response>
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
