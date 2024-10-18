// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.Session
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Device;

#nullable disable
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
