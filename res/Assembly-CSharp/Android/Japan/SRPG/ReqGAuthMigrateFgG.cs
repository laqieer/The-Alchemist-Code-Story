// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGAuthMigrateFgG
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqGAuthMigrateFgG : WebAPI
  {
    public ReqGAuthMigrateFgG(string secretKey, string deviceID, string mail, string password, Network.ResponseCallback response)
    {
      this.name = "gauth/achievement/mgrate";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"secret_key\":\"");
      stringBuilder.Append(secretKey);
      stringBuilder.Append("\",\"device_id\":\"");
      stringBuilder.Append(deviceID);
      stringBuilder.Append("\",\"email\":\"");
      stringBuilder.Append(mail);
      stringBuilder.Append("\",\"password\":\"");
      stringBuilder.Append(password);
      stringBuilder.Append("\"");
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
