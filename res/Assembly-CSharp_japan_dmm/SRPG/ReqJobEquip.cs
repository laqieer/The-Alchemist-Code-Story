// Decompiled with JetBrains decompiler
// Type: SRPG.ReqJobEquip
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
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
