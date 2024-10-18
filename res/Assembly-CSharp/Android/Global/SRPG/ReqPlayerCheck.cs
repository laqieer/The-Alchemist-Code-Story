// Decompiled with JetBrains decompiler
// Type: SRPG.ReqPlayerCheck
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqPlayerCheck : WebAPI
  {
    public ReqPlayerCheck(string deviceid, string secretkey, Network.ResponseCallback response)
    {
      this.name = "player/chkplayer";
      this.body = "{";
      ReqPlayerCheck reqPlayerCheck1 = this;
      reqPlayerCheck1.body = reqPlayerCheck1.body + "\"ticket\":" + (object) Network.TicketID + ",";
      this.body += "\"access_token\":\"\",";
      this.body += "\"param\":{";
      ReqPlayerCheck reqPlayerCheck2 = this;
      reqPlayerCheck2.body = reqPlayerCheck2.body + "\"device_id\":\"" + deviceid + "\",";
      ReqPlayerCheck reqPlayerCheck3 = this;
      reqPlayerCheck3.body = reqPlayerCheck3.body + "\"secret_key\":\"" + secretkey + "\"";
      if (!string.IsNullOrEmpty(GameManager.GetIDFA))
      {
        ReqPlayerCheck reqPlayerCheck4 = this;
        reqPlayerCheck4.body = reqPlayerCheck4.body + ",\"idfa\":\"" + GameManager.GetIDFA + "\"";
      }
      this.body += "}";
      this.body += "}";
      this.callback = response;
    }
  }
}
