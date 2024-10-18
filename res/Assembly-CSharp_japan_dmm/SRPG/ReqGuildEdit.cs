// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGuildEdit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Text;

#nullable disable
namespace SRPG
{
  public class ReqGuildEdit : WebAPI
  {
    public ReqGuildEdit(GuildData new_data, Network.ResponseCallback response)
    {
      this.name = "guild/info/update";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      if (!string.IsNullOrEmpty(new_data.Name))
      {
        stringBuilder.Append("\"name\":\"");
        stringBuilder.Append(WebAPI.EscapeString(new_data.Name));
        stringBuilder.Append("\"");
        stringBuilder.Append(",");
      }
      if (!string.IsNullOrEmpty(new_data.Board))
      {
        stringBuilder.Append("\"board\":\"");
        stringBuilder.Append(WebAPI.EscapeString(new_data.Board));
        stringBuilder.Append("\"");
        stringBuilder.Append(",");
      }
      if (!string.IsNullOrEmpty(new_data.Emblem))
      {
        stringBuilder.Append("\"award_id\":\"");
        stringBuilder.Append(new_data.Emblem);
        stringBuilder.Append("\"");
        stringBuilder.Append(",");
      }
      if (new_data.EntryConditions != null && new_data.EntryConditions.LowerLevel >= 0)
      {
        stringBuilder.Append("\"lower_level\":");
        stringBuilder.Append(new_data.EntryConditions.LowerLevel);
        stringBuilder.Append(",");
      }
      if (new_data.EntryConditions != null)
      {
        stringBuilder.Append("\"is_auto_approval\":");
        stringBuilder.Append(!new_data.EntryConditions.IsAutoApproval ? 0 : 1);
        stringBuilder.Append(",");
      }
      if (!string.IsNullOrEmpty(new_data.EntryConditions.Comment))
      {
        stringBuilder.Append("\"recruit_comment\":\"");
        stringBuilder.Append(WebAPI.EscapeString(new_data.EntryConditions.Comment));
        stringBuilder.Append("\"");
        stringBuilder.Append(",");
      }
      if (new_data.EntryConditions != null && new_data.EntryConditions.Policy >= 0)
      {
        stringBuilder.Append("\"policy\":");
        stringBuilder.Append(new_data.EntryConditions.Policy);
        stringBuilder.Append(",");
      }
      stringBuilder.Append("\"gvg_join_status\":");
      stringBuilder.Append(new_data.GvGJoinStatus);
      stringBuilder.Append(",");
      if (stringBuilder.Length > 0)
        stringBuilder.Remove(stringBuilder.Length - 1, 1);
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
