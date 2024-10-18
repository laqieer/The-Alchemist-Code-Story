// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.ISession
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using Gsc.Network;
using System;

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

    IWebTask RegisterEmailAddressAndPassword(string email, string password, bool disableValicationEmail, Action<RegisterEmailAddressAndPasswordResult> callback);

    IWebTask AddDeviceWithEmailAddressAndPassword(string email, string password, Action<AddDeviceWithEmailAddressAndPasswordResult> callback);
  }
}
