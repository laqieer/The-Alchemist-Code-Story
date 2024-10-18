// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGuildLeave
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Text;

#nullable disable
namespace SRPG
{
  public class ReqGuildLeave : WebAPI
  {
    public ReqGuildLeave(string new_guild_master_uid, Network.ResponseCallback response)
    {
      this.name = "guild/leave";
      this.body = WebAPI.GetRequestString((string) null);
      if (!string.IsNullOrEmpty(new_guild_master_uid))
      {
        StringBuilder stringBuilder = WebAPI.GetStringBuilder();
        stringBuilder.Append("\"target_uid\":\"");
        stringBuilder.Append(new_guild_master_uid);
        stringBuilder.Append("\"");
        this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      }
      this.callback = response;
    }
  }
}
