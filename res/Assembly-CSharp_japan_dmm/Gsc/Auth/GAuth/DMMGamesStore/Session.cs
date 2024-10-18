// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.GAuth.DMMGamesStore.Session
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Auth.GAuth.DMMGamesStore.API.Request;
using Gsc.Core;
using Gsc.Device;
using Gsc.Network;
using Gsc.Tasks;
using System;
using System.Collections;
using System.Diagnostics;

#nullable disable
namespace Gsc.Auth.GAuth.DMMGamesStore
{
  public class Session : Gsc.Auth.GAuth.GAuth.Session
  {
    public Session(string envName, IAccountManager accountManager)
      : base(envName, accountManager)
    {
      RootObject.Instance.StartCoroutine(Session.UpdateAccessToken(this));
    }

    [DebuggerHidden]
    private static IEnumerator UpdateAccessToken(Session session)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      Session.\u003CUpdateAccessToken\u003Ec__Iterator0 accessTokenCIterator0 = new Session.\u003CUpdateAccessToken\u003Ec__Iterator0();
      return (IEnumerator) accessTokenCIterator0;
    }

    public override string DeviceID => Gsc.Auth.GAuth.DMMGamesStore.Device.Instance.ViewerId.ToString();

    public override bool CanRefreshToken(Type requestType)
    {
      return !requestType.Equals(typeof (Gsc.Auth.GAuth.DMMGamesStore.API.Request.AccessToken)) && !requestType.Equals(typeof (UpdateOnetimeToken));
    }

    public override IRefreshTokenTask GetRefreshTokenTask()
    {
      return (IRefreshTokenTask) new Session.RefreshTokenTask(this);
    }

    public override IWebTask RegisterEmailAddressAndPassword(
      string email,
      string password,
      bool disableValicationEmail,
      Action<RegisterEmailAddressAndPasswordResult> callback)
    {
      return (IWebTask) new Gsc.Auth.GAuth.DMMGamesStore.API.Request.RegisterEmailAddressAndPassword(Gsc.Auth.GAuth.DMMGamesStore.Device.Instance.ViewerId, Gsc.Auth.GAuth.DMMGamesStore.Device.Instance.OnetimeToken, email, password)
      {
        DisableValidationEmail = disableValicationEmail
      }.Send().OnResponse((VoidCallbackWithError<Gsc.Auth.GAuth.DMMGamesStore.API.Request.RegisterEmailAddressAndPassword.Response>) ((response, error) => callback(Session.GetRegisterEmailAddressWithPasswordResult(response, (Gsc.Auth.GAuth.GAuth.API.Response.ErrorResponse) error))));
    }

    private static RegisterEmailAddressAndPasswordResult GetRegisterEmailAddressWithPasswordResult(
      Gsc.Auth.GAuth.DMMGamesStore.API.Request.RegisterEmailAddressAndPassword.Response response,
      Gsc.Auth.GAuth.GAuth.API.Response.ErrorResponse error)
    {
      if (error == null)
        return new RegisterEmailAddressAndPasswordResult(RegisterEmailAddressAndPasswordResultCode.Success);
      RegisterEmailAddressAndPasswordResultCode resultCode = RegisterEmailAddressAndPasswordResultCode.UnknownError;
      switch (error.ErrorCode)
      {
        case "invalied_email":
          resultCode = RegisterEmailAddressAndPasswordResultCode.InvalidEmailAddress;
          break;
        case "invalied_password":
          resultCode = RegisterEmailAddressAndPasswordResultCode.InvalidPassword;
          break;
        case "duplicated_email":
          resultCode = RegisterEmailAddressAndPasswordResultCode.DuplicatedEmailAddress;
          break;
        default:
          if (error.data.Root.GetValueByPointer("/reason/email", (string) null) != null)
          {
            resultCode = RegisterEmailAddressAndPasswordResultCode.InvalidEmailAddress;
            break;
          }
          if (error.data.Root.GetValueByPointer("/reason/password", (string) null) != null)
          {
            resultCode = RegisterEmailAddressAndPasswordResultCode.InvalidPassword;
            break;
          }
          break;
      }
      return new RegisterEmailAddressAndPasswordResult(resultCode);
    }

    public override IWebTask AddDeviceWithEmailAddressAndPassword(
      string email,
      string password,
      Action<AddDeviceWithEmailAddressAndPasswordResult> callback)
    {
      return (IWebTask) new Gsc.Auth.GAuth.DMMGamesStore.API.Request.AddDeviceWithEmailAddressAndPassword(Gsc.Auth.GAuth.DMMGamesStore.Device.Instance.ViewerId, Gsc.Auth.GAuth.DMMGamesStore.Device.Instance.OnetimeToken, email, password).Send().OnResponse((VoidCallbackWithError<Gsc.Auth.GAuth.DMMGamesStore.API.Request.AddDeviceWithEmailAddressAndPassword.Response>) ((response, error) => callback(Session.GetAddDeviceWithEmailAddressAndPassword((Gsc.Auth.GAuth.GAuth.API.Response.ErrorResponse) error))));
    }

    private static AddDeviceWithEmailAddressAndPasswordResult GetAddDeviceWithEmailAddressAndPassword(
      Gsc.Auth.GAuth.GAuth.API.Response.ErrorResponse error)
    {
      if (error == null)
        return new AddDeviceWithEmailAddressAndPasswordResult(AddDeviceWithEmailAddressAndPasswordResultCode.Success);
      AddDeviceWithEmailAddressAndPasswordResultCode resultCode = AddDeviceWithEmailAddressAndPasswordResultCode.UnknownError;
      switch (error.ErrorCode)
      {
        case "missing_device_id":
          resultCode = AddDeviceWithEmailAddressAndPasswordResultCode.MissingDeviceId;
          break;
        case "missing_email_or_password":
          resultCode = AddDeviceWithEmailAddressAndPasswordResultCode.MissingEmailOrPassword;
          break;
        case "locked":
          resultCode = AddDeviceWithEmailAddressAndPasswordResultCode.Locked;
          break;
        default:
          if (error.data.Root.GetValueByPointer("/reason/email", (string) null) != null)
          {
            resultCode = AddDeviceWithEmailAddressAndPasswordResultCode.MissingEmailOrPassword;
            break;
          }
          if (error.data.Root.GetValueByPointer("/reason/password", (string) null) != null)
          {
            resultCode = AddDeviceWithEmailAddressAndPasswordResultCode.MissingEmailOrPassword;
            break;
          }
          break;
      }
      return resultCode == AddDeviceWithEmailAddressAndPasswordResultCode.Locked ? new AddDeviceWithEmailAddressAndPasswordResult(resultCode, error.data.Root["expires_in"].ToInt(), error.data.Root["trial_counter"].ToInt()) : new AddDeviceWithEmailAddressAndPasswordResult(resultCode);
    }

    public new class RefreshTokenTask : IRefreshTokenTask, ITask
    {
      private readonly Session session;

      public RefreshTokenTask(Session session) => this.session = session;

      public WebTaskResult Result { get; protected set; }

      public bool isDone { get; protected set; }

      public void OnStart()
      {
      }

      public void OnFinish()
      {
      }

      [DebuggerHidden]
      public IEnumerator Run()
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new Session.RefreshTokenTask.\u003CRun\u003Ec__Iterator0()
        {
          \u0024this = this
        };
      }
    }
  }
}
