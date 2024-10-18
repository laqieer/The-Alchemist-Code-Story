// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.GAuth.GAuth.Session
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Device;
using Gsc.Network;
using Gsc.Tasks;
using MiniJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

#nullable disable
namespace Gsc.Auth.GAuth.GAuth
{
  public class Session : ISession
  {
    public readonly string EnvName;
    private readonly IAccountManager accountManager;
    private static readonly string userAgentCache = Json.Serialize((object) new Dictionary<string, object>()
    {
      {
        "device_model",
        (object) DeviceInfo.DeviceModel
      },
      {
        "device_vendor",
        (object) DeviceInfo.DeviceVendor
      },
      {
        "os_info",
        (object) DeviceInfo.OperatingSystem
      },
      {
        "cpu_info",
        (object) DeviceInfo.ProcessorType
      },
      {
        "memory_size",
        (object) (((double) (DeviceInfo.SystemMemorySize >> 10) / 1000.0).ToString() + "GB")
      }
    });

    public Session(string envName, IAccountManager accountManager)
    {
      this.EnvName = envName;
      this.accountManager = accountManager;
    }

    public string AccessToken { get; protected set; }

    public virtual string DeviceID => this.accountManager.GetDeviceId(this.EnvName);

    public virtual string SecretKey => this.accountManager.GetSecretKey(this.EnvName);

    public virtual string UserAgent => Session.userAgentCache;

    public void DeleteAuthKeys() => this.accountManager.Remove(this.EnvName);

    public virtual bool CanRefreshToken(Type requestType)
    {
      return !requestType.Equals(typeof (Gsc.Auth.GAuth.GAuth.API.Request.AccessToken));
    }

    public virtual IRefreshTokenTask GetRefreshTokenTask()
    {
      return (IRefreshTokenTask) new Session.RefreshTokenTask(this);
    }

    public virtual IWebTask RegisterEmailAddressAndPassword(
      string email,
      string password,
      bool disableValicationEmail,
      Action<RegisterEmailAddressAndPasswordResult> callback)
    {
      return (IWebTask) new Gsc.Auth.GAuth.GAuth.API.Request.RegisterEmailAddressAndPassword(this.DeviceID, this.SecretKey, email, password)
      {
        DisableValidationEmail = disableValicationEmail
      }.Send().OnResponse((VoidCallbackWithError<Gsc.Auth.GAuth.GAuth.API.Response.RegisterEmailAddressAndPassword>) ((response, error) => callback(Session.GetRegisterEmailAddressWithPasswordResult(response, (Gsc.Auth.GAuth.GAuth.API.Response.ErrorResponse) error))));
    }

    private static RegisterEmailAddressAndPasswordResult GetRegisterEmailAddressWithPasswordResult(
      Gsc.Auth.GAuth.GAuth.API.Response.RegisterEmailAddressAndPassword response,
      Gsc.Auth.GAuth.GAuth.API.Response.ErrorResponse error)
    {
      if (error == null)
        return new RegisterEmailAddressAndPasswordResult(RegisterEmailAddressAndPasswordResultCode.Success);
      RegisterEmailAddressAndPasswordResultCode resultCode = RegisterEmailAddressAndPasswordResultCode.UnknownError;
      switch (error.ErrorCode)
      {
        case "000":
          resultCode = RegisterEmailAddressAndPasswordResultCode.InvalidEmailAddress;
          break;
        case "001":
          resultCode = RegisterEmailAddressAndPasswordResultCode.InvalidPassword;
          break;
        case "002":
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

    public virtual IWebTask AddDeviceWithEmailAddressAndPassword(
      string email,
      string password,
      Action<AddDeviceWithEmailAddressAndPasswordResult> callback)
    {
      return (IWebTask) new Gsc.Auth.GAuth.GAuth.API.Request.AddDeviceWithEmailAddressAndPassword(email, password)
      {
        Idfv = Gsc.Auth.GAuth.GAuth.Device.Instance.ID
      }.Send().OnResponse((VoidCallbackWithError<Gsc.Auth.GAuth.GAuth.API.Response.AddDeviceWithEmailAddressAndPassword>) ((response, error) =>
      {
        AddDeviceWithEmailAddressAndPasswordResult addressAndPassword = Session.GetAddDeviceWithEmailAddressAndPassword((Gsc.Auth.GAuth.GAuth.API.Response.ErrorResponse) error);
        if (addressAndPassword == AddDeviceWithEmailAddressAndPasswordResultCode.Success)
          this.accountManager.SetKeyPair(this.EnvName, response.SecretKey, response.DeviceId);
        callback(addressAndPassword);
      }));
    }

    private static AddDeviceWithEmailAddressAndPasswordResult GetAddDeviceWithEmailAddressAndPassword(
      Gsc.Auth.GAuth.GAuth.API.Response.ErrorResponse error)
    {
      if (error == null)
        return new AddDeviceWithEmailAddressAndPasswordResult(AddDeviceWithEmailAddressAndPasswordResultCode.Success);
      AddDeviceWithEmailAddressAndPasswordResultCode resultCode = AddDeviceWithEmailAddressAndPasswordResultCode.UnknownError;
      switch (error.ErrorCode)
      {
        case "000":
          resultCode = AddDeviceWithEmailAddressAndPasswordResultCode.MissingDeviceId;
          break;
        case "001":
          resultCode = AddDeviceWithEmailAddressAndPasswordResultCode.MissingEmailOrPassword;
          break;
        case "002":
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

    public class AccessTokenChecker : MonoBehaviour
    {
      private const float FAILED_POLLING_INTERVAL = 30f;
      private const WebTaskAttribute TASK_ATTRIBUTES = WebTaskAttribute.Reliable | WebTaskAttribute.Silent | WebTaskAttribute.Parallel;
      private const WebTaskResult ACCEPT_RESULTS = WebTaskResult.kLocalResult | WebTaskResult.kGrobalResult | WebTaskResult.kCreticalError | WebTaskResult.Maintenance | WebTaskResult.UpdateApplication;
      private bool isRunning;
      private int cachedInstanceId;

      private void Awake() => this.cachedInstanceId = ((Object) this).GetInstanceID();

      private void OnApplicationFocus(bool focusState)
      {
        bool flag = focusState && this.cachedInstanceId == ((Object) this).GetInstanceID();
        if (this.isRunning || !flag || WebQueue.defaultQueue.isPause)
          return;
        this.StartCoroutine(this.CheckToken());
      }

      [DebuggerHidden]
      private IEnumerator CheckToken()
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new Session.AccessTokenChecker.\u003CCheckToken\u003Ec__Iterator0()
        {
          \u0024this = this
        };
      }
    }

    public class RefreshTokenTask : IRefreshTokenTask, ITask
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
