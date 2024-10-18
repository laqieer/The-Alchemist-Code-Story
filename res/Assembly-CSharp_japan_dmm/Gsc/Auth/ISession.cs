// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.ISession
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network;
using System;

#nullable disable
namespace Gsc.Auth
{
  public interface ISession
  {
    string SecretKey { get; }

    string DeviceID { get; }

    string AccessToken { get; }

    string UserAgent { get; }

    void DeleteAuthKeys();

    bool CanRefreshToken(Type requestType);

    IRefreshTokenTask GetRefreshTokenTask();

    IWebTask RegisterEmailAddressAndPassword(
      string email,
      string password,
      bool disableValicationEmail,
      Action<RegisterEmailAddressAndPasswordResult> callback);

    IWebTask AddDeviceWithEmailAddressAndPassword(
      string email,
      string password,
      Action<AddDeviceWithEmailAddressAndPasswordResult> callback);
  }
}
