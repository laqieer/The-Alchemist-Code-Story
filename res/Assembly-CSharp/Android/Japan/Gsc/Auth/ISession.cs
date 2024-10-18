// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.ISession
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
