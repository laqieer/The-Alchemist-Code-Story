// Decompiled with JetBrains decompiler
// Type: SRPG.ReqJobRankup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqJobRankup : WebAPI
  {
    public ReqJobRankup(long iid_job, Network.ResponseCallback response)
    {
      this.name = "unit/job/equip/lvup/";
      this.body = WebAPI.GetRequestString("\"iid\":" + (object) iid_job);
      this.callback = response;
    }
  }
}
