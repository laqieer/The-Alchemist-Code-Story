// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.Session
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using Gsc.Device;

namespace Gsc.Auth
{
  public static class Session
  {
    public static ISession DefaultSession { get; private set; }

    public static void Init(string envName, IAccountManager accountManager)
    {
      Session.DefaultSession = (ISession) new Gsc.Auth.GAuth.GAuth.Session(envName, accountManager);
    }
  }
}
