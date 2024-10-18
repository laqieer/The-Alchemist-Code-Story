// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGetAccessToken
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqGetAccessToken : WebAPI
  {
    public ReqGetAccessToken(string deviceid, string secretkey, Network.ResponseCallback response)
    {
      this.name = "gauth/accesstoken";
      this.body = "{";
      ReqGetAccessToken reqGetAccessToken1 = this;
      reqGetAccessToken1.body = reqGetAccessToken1.body + "\"ticket\":" + (object) Network.TicketID + ",";
      this.body += "\"access_token\":\"\",";
      this.body += "\"param\":{";
      ReqGetAccessToken reqGetAccessToken2 = this;
      reqGetAccessToken2.body = reqGetAccessToken2.body + "\"device_id\":\"" + deviceid + "\",";
      ReqGetAccessToken reqGetAccessToken3 = this;
      reqGetAccessToken3.body = reqGetAccessToken3.body + "\"secret_key\":\"" + secretkey + "\"";
      if (!string.IsNullOrEmpty(GameManager.GetIDFA))
      {
        ReqGetAccessToken reqGetAccessToken4 = this;
        reqGetAccessToken4.body = reqGetAccessToken4.body + ",\"idfa\":\"" + GameManager.GetIDFA + "\"";
      }
      this.body += "}";
      this.body += "}";
      this.callback = response;
    }
  }
}
