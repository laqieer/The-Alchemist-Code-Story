// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGetDeviceID
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class ReqGetDeviceID : WebAPI
  {
    public ReqGetDeviceID(string secretkey, string udid, Network.ResponseCallback response)
    {
      this.name = "gauth/register";
      this.body = "{";
      ReqGetDeviceID reqGetDeviceId1 = this;
      reqGetDeviceId1.body = reqGetDeviceId1.body + "\"ticket\":" + (object) Network.TicketID + ",";
      this.body += "\"access_token\":\"\",";
      this.body += "\"param\":{";
      ReqGetDeviceID reqGetDeviceId2 = this;
      reqGetDeviceId2.body = reqGetDeviceId2.body + "\"secret_key\":\"" + secretkey + "\",";
      ReqGetDeviceID reqGetDeviceId3 = this;
      reqGetDeviceId3.body = reqGetDeviceId3.body + "\"udid\":\"" + udid + "\"";
      this.body += "}";
      this.body += "}";
      this.callback = response;
    }
  }
}
