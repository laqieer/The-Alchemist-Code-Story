// Decompiled with JetBrains decompiler
// Type: SRPG.ReqQRCodeAccess
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
