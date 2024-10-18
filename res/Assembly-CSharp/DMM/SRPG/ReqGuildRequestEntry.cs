// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGuildRequestEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Text;

#nullable disable
namespace SRPG
{
  public class ReqGuildRequestEntry : WebAPI
  {
    public ReqGuildRequestEntry(long gid, Network.ResponseCallback response)
    {
      this.name = "guild/apply/send";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"gid\":");
      stringBuilder.Append(gid);
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
