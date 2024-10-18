// Decompiled with JetBrains decompiler
// Type: SRPG.ReqAppGuardAuthentication
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Text;

#nullable disable
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
