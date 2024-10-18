// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGuildCreate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqGuildCreate : WebAPI
  {
    public ReqGuildCreate(GuildData new_guild, Network.ResponseCallback response)
    {
      this.name = "guild/create";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"name\":\"");
      stringBuilder.Append(WebAPI.EscapeString(new_guild.Name));
      stringBuilder.Append("\",");
      stringBuilder.Append("\"award_id\":\"");
      stringBuilder.Append(new_guild.Emblem);
      stringBuilder.Append("\",");
      stringBuilder.Append("\"lower_level\":");
      stringBuilder.Append(new_guild.EntryConditions.LowerLevel);
      stringBuilder.Append(",");
      stringBuilder.Append("\"board\":\"");
      stringBuilder.Append(WebAPI.EscapeString(new_guild.Board));
      stringBuilder.Append("\",");
      stringBuilder.Append("\"recruit_comment\":\"");
      stringBuilder.Append(WebAPI.EscapeString(new_guild.EntryConditions.Comment));
      stringBuilder.Append("\",");
      stringBuilder.Append("\"is_auto_approval\":");
      stringBuilder.Append(!new_guild.EntryConditions.IsAutoApproval ? 0 : 1);
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
