// Decompiled with JetBrains decompiler
// Type: SRPG.ReqBtlComResume
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqBtlComResume : WebAPI
  {
    public ReqBtlComResume(long btlid, Network.ResponseCallback response)
    {
      this.name = "btl/com/resume";
      this.body = "\"btlid\":" + (object) btlid;
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }
  }
}
