﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ReqSendSupportEmail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqSendSupportEmail : WebAPI
  {
    public ReqSendSupportEmail(string subject, string message, string replyTo, Network.ResponseCallback response)
    {
      this.name = "support/mail";
      this.body = "{";
      ReqSendSupportEmail sendSupportEmail1 = this;
      sendSupportEmail1.body = sendSupportEmail1.body + "\"ticket\":" + (object) Network.TicketID + ",";
      this.body += "\"access_token\":\"\",";
      this.body += "\"param\":{";
      ReqSendSupportEmail sendSupportEmail2 = this;
      sendSupportEmail2.body = sendSupportEmail2.body + "\"subject\":\"" + subject + "\",";
      ReqSendSupportEmail sendSupportEmail3 = this;
      sendSupportEmail3.body = sendSupportEmail3.body + "\"message\":\"" + message + "\",";
      ReqSendSupportEmail sendSupportEmail4 = this;
      sendSupportEmail4.body = sendSupportEmail4.body + "\"replyTo\":\"" + replyTo + "\"";
      this.body += "}";
      this.body += "}";
      this.callback = response;
    }
  }
}
