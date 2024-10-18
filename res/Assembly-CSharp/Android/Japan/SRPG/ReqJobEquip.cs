// Decompiled with JetBrains decompiler
// Type: SRPG.ReqJobEquip
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqJobEquip : WebAPI
  {
    public ReqJobEquip(long iid_job, long id_equip, Network.ResponseCallback response)
    {
      this.name = "unit/job/equip/set";
      this.body = "\"iid\":" + (object) iid_job + ",";
      ReqJobEquip reqJobEquip = this;
      reqJobEquip.body = reqJobEquip.body + "\"id_equip\":" + (object) id_equip;
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }
  }
}
