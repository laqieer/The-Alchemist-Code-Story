// Decompiled with JetBrains decompiler
// Type: SRPG.ReqRegisterDeviceToFacebook
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqRegisterDeviceToFacebook : WebAPI
  {
    public ReqRegisterDeviceToFacebook(string secretkey, string udid, string accesstoken, Network.ResponseCallback response)
    {
      this.name = "gauth/facebook/sso/relate_account";
      this.body = "{";
      ReqRegisterDeviceToFacebook deviceToFacebook1 = this;
      deviceToFacebook1.body = deviceToFacebook1.body + "\"ticket\":" + (object) Network.TicketID + ",";
      this.body += "\"access_token\":\"\",";
      this.body += "\"param\":{";
      ReqRegisterDeviceToFacebook deviceToFacebook2 = this;
      deviceToFacebook2.body = deviceToFacebook2.body + "\"secret_key\":\"" + secretkey + "\",";
      ReqRegisterDeviceToFacebook deviceToFacebook3 = this;
      deviceToFacebook3.body = deviceToFacebook3.body + "\"device_id\":\"" + udid + "\",";
      ReqRegisterDeviceToFacebook deviceToFacebook4 = this;
      deviceToFacebook4.body = deviceToFacebook4.body + "\"access_token\":\"" + accesstoken + "\"";
      this.body += "}";
      this.body += "}";
      this.callback = response;
    }
  }
}
