// Decompiled with JetBrains decompiler
// Type: SRPG.ReqMigrateFacebookToDevice
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqMigrateFacebookToDevice : WebAPI
  {
    public ReqMigrateFacebookToDevice(string secretkey, string udid, string accesstoken, Network.ResponseCallback response)
    {
      this.name = "gauth/facebook/sso/migrate";
      this.body = "{";
      ReqMigrateFacebookToDevice facebookToDevice1 = this;
      facebookToDevice1.body = facebookToDevice1.body + "\"ticket\":" + (object) Network.TicketID + ",";
      this.body += "\"access_token\":\"\",";
      this.body += "\"param\":{";
      ReqMigrateFacebookToDevice facebookToDevice2 = this;
      facebookToDevice2.body = facebookToDevice2.body + "\"secret_key\":\"" + secretkey + "\",";
      ReqMigrateFacebookToDevice facebookToDevice3 = this;
      facebookToDevice3.body = facebookToDevice3.body + "\"device_id\":\"" + udid + "\",";
      ReqMigrateFacebookToDevice facebookToDevice4 = this;
      facebookToDevice4.body = facebookToDevice4.body + "\"access_token\":\"" + accesstoken + "\"";
      this.body += "}";
      this.body += "}";
      this.callback = response;
    }
  }
}
