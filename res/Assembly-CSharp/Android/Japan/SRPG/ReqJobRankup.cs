// Decompiled with JetBrains decompiler
// Type: SRPG.ReqJobRankup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
