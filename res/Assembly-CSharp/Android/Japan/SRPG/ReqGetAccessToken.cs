// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGetAccessToken
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
      this.body += "}";
      this.body += "}";
      this.callback = response;
    }
  }
}
