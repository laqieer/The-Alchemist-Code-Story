// Decompiled with JetBrains decompiler
// Type: SRPG.ReqFgGAuth
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqFgGAuth : WebAPI
  {
    public ReqFgGAuth(Network.ResponseCallback response)
    {
      this.name = "achieve/auth";
      this.callback = response;
    }

    public enum eAuthStatus
    {
      None,
      Disable,
      NotSynchronized,
      Synchronized,
    }
  }
}
