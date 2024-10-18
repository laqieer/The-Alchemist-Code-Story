// Decompiled with JetBrains decompiler
// Type: SRPG.ReqAttachFacebookToDevice
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqAttachFacebookToDevice : WebAPI
  {
    public ReqAttachFacebookToDevice(string accesstoken, Network.ResponseCallback response)
    {
      this.name = "gauth/facebook/sso/device";
      this.body = "{";
      ReqAttachFacebookToDevice facebookToDevice1 = this;
      facebookToDevice1.body = facebookToDevice1.body + "\"ticket\":" + (object) Network.TicketID + ",";
      this.body += "\"access_token\":\"\",";
      this.body += "\"param\":{";
      ReqAttachFacebookToDevice facebookToDevice2 = this;
      facebookToDevice2.body = facebookToDevice2.body + "\"access_token\":\"" + accesstoken + "\"";
      this.body += "}";
      this.body += "}";
      this.callback = response;
    }
  }
}
