// Decompiled with JetBrains decompiler
// Type: SRPG.ReqLoginPack
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqLoginPack : WebAPI
  {
    public ReqLoginPack(Network.ResponseCallback response, bool relogin = false)
    {
      this.name = "login/param";
      this.body = "\"relogin\":";
      this.body += (string) (object) (!relogin ? 0 : 1);
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }
  }
}
