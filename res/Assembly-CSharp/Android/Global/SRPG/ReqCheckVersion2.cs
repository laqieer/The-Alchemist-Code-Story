// Decompiled with JetBrains decompiler
// Type: SRPG.ReqCheckVersion2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqCheckVersion2 : WebAPI
  {
    public ReqCheckVersion2(string ver, Network.ResponseCallback response)
    {
      this.name = "chkver2";
      this.body = "{\"ver\":\"";
      this.body += ver;
      this.body += "\"}";
      this.callback = response;
    }
  }
}
