// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGAuthFgGDevice
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Text;

#nullable disable
namespace SRPG
{
  public class ReqGAuthFgGDevice : WebAPI
  {
    public ReqGAuthFgGDevice(
      string deviceID,
      string mail,
      string password,
      Network.ResponseCallback response)
    {
      this.name = "gauth/fggid/device";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"idfv\":\"");
      stringBuilder.Append(deviceID);
      stringBuilder.Append("\",\"email\":\"");
      stringBuilder.Append(mail);
      stringBuilder.Append("\",\"password\":\"");
      stringBuilder.Append(password);
      stringBuilder.Append("\"");
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }

    public class Json_FggDevice
    {
      public string device_id;
      public string secret_key;
    }
  }
}
