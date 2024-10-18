// Decompiled with JetBrains decompiler
// Type: SRPG.ReqQRCodeAccess
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Text;

#nullable disable
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
