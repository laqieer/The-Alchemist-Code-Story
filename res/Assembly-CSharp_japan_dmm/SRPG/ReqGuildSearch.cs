// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGuildSearch
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Text;

#nullable disable
namespace SRPG
{
  public class ReqGuildSearch : WebAPI
  {
    public ReqGuildSearch(
      long search_gid,
      string search_name,
      int is_auto,
      int lower_lv,
      int guild_lv_from,
      int guild_lv_to,
      int policy,
      int member_num_from,
      int member_num_to,
      int shop_lv_from,
      int shop_lv_to,
      Network.ResponseCallback response)
    {
      this.name = "guild/search";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      if (search_gid > 0L)
      {
        stringBuilder.Append("\"search_gid\":");
        stringBuilder.Append(search_gid);
        stringBuilder.Append(",");
      }
      if (!string.IsNullOrEmpty(search_name))
      {
        stringBuilder.Append("\"search_name\":\"");
        stringBuilder.Append(WebAPI.EscapeString(search_name));
        stringBuilder.Append("\"");
        stringBuilder.Append(",");
      }
      if (is_auto > 0)
      {
        int num = is_auto != 1 ? 0 : 1;
        stringBuilder.Append("\"is_auto\":");
        stringBuilder.Append(num);
        stringBuilder.Append(",");
      }
      if (lower_lv > 0)
      {
        stringBuilder.Append("\"lower_level\":");
        stringBuilder.Append(lower_lv);
        stringBuilder.Append(",");
      }
      if (guild_lv_from > 0)
      {
        stringBuilder.Append("\"guild_level_from\":");
        stringBuilder.Append(guild_lv_from);
        stringBuilder.Append(",");
      }
      if (guild_lv_to > 0)
      {
        stringBuilder.Append("\"guild_level_to\":");
        stringBuilder.Append(guild_lv_to);
        stringBuilder.Append(",");
      }
      if (policy > 0)
      {
        stringBuilder.Append("\"policy\":");
        stringBuilder.Append(policy);
        stringBuilder.Append(",");
      }
      if (member_num_from > 0)
      {
        stringBuilder.Append("\"member_num_from\":");
        stringBuilder.Append(member_num_from);
        stringBuilder.Append(",");
      }
      if (member_num_to > 0)
      {
        stringBuilder.Append("\"member_num_to\":");
        stringBuilder.Append(member_num_to);
        stringBuilder.Append(",");
      }
      if (shop_lv_from > 0)
      {
        stringBuilder.Append("\"shop_lv_from\":");
        stringBuilder.Append(shop_lv_from);
        stringBuilder.Append(",");
      }
      if (shop_lv_to > 0)
      {
        stringBuilder.Append("\"shop_lv_to\":");
        stringBuilder.Append(shop_lv_to);
        stringBuilder.Append(",");
      }
      if (stringBuilder.Length > 0)
        stringBuilder.Remove(stringBuilder.Length - 1, 1);
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
