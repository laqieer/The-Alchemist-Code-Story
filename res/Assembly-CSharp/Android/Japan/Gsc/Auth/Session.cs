// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.Session
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using Gsc.Device;

namespace Gsc.Auth
{
  public static class Session
  {
    public static ISession DefaultSession { get; private set; }

    public static void Init(string envName, IAccountManager accountManager)
    {
      Session.DefaultSession = (ISession) new Gsc.Auth.GAuth.DMMGamesStore.Session(envName, accountManager);
    }
  }
}
