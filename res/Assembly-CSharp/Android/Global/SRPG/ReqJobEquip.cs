// Decompiled with JetBrains decompiler
// Type: SRPG.ReqJobEquip
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
