// Decompiled with JetBrains decompiler
// Type: SRPG.ReqRankMatchReport
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqRankMatchReport : WebAPI
  {
    public ReqRankMatchReport(string subject, string message, string replyTo, string id, Network.ResponseCallback response)
    {
      this.name = "vs/rankmatch/report";
      this.body = "{";
      ReqRankMatchReport reqRankMatchReport1 = this;
      reqRankMatchReport1.body = reqRankMatchReport1.body + "\"ticket\":" + (object) Network.TicketID + ",";
      this.body += "\"access_token\":\"\",";
      this.body += "\"param\":{";
      ReqRankMatchReport reqRankMatchReport2 = this;
      reqRankMatchReport2.body = reqRankMatchReport2.body + "\"id\":\"" + id + "\",";
      ReqRankMatchReport reqRankMatchReport3 = this;
      reqRankMatchReport3.body = reqRankMatchReport3.body + "\"subject\":\"" + subject + "\",";
      ReqRankMatchReport reqRankMatchReport4 = this;
      reqRankMatchReport4.body = reqRankMatchReport4.body + "\"message\":\"" + message + "\",";
      ReqRankMatchReport reqRankMatchReport5 = this;
      reqRankMatchReport5.body = reqRankMatchReport5.body + "\"replyTo\":\"" + replyTo + "\"";
      this.body += "}";
      this.body += "}";
      this.callback = response;
    }

    public class Response
    {
      public bool success;
    }
  }
}
