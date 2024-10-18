// Decompiled with JetBrains decompiler
// Type: SRPG.ReqTowerBtlComResume
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class ReqTowerBtlComResume : WebAPI
  {
    public ReqTowerBtlComResume(long btlid, Network.ResponseCallback response)
    {
      this.name = "tower/btl/resume";
      this.body = "\"btlid\":" + (object) btlid;
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }
  }
}
