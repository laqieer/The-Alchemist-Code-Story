// Decompiled with JetBrains decompiler
// Type: SRPG.ReqAppGuardAuthentication
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqAppGuardAuthentication : WebAPI
  {
    public ReqAppGuardAuthentication(string uniqueClientID, Network.ResponseCallback response)
    {
      this.name = "appguard/auth";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"unique_client_id\":");
      stringBuilder.Append("\"");
      stringBuilder.Append(uniqueClientID);
      stringBuilder.Append("\"");
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
