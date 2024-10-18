// Decompiled with JetBrains decompiler
// Type: SRPG.ReqQRCodeAccess
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqQRCodeAccess : WebAPI
  {
    public ReqQRCodeAccess(int campaign, string serial, Network.ResponseCallback response)
    {
      this.name = "qr/serial";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"campaign_id\":");
      stringBuilder.Append(campaign);
      stringBuilder.Append(",");
      stringBuilder.Append("\"code\":\"");
      stringBuilder.Append(serial);
      stringBuilder.Append("\"");
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
