// Decompiled with JetBrains decompiler
// Type: SRPG.ReqJobRankupAll
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Text;

#nullable disable
namespace SRPG
{
  public class ReqJobRankupAll : WebAPI
  {
    public ReqJobRankupAll(
      long iid_unit,
      string iname_jobset,
      bool is_cmn,
      int current_rank,
      int target_rank,
      int isEquips,
      Network.ResponseCallback response)
    {
      this.name = "unit/job/equip/lvupall";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"uiid\":");
      stringBuilder.Append(iid_unit);
      stringBuilder.Append(",\"jobset\":\"");
      stringBuilder.Append(iname_jobset);
      stringBuilder.Append("\"");
      stringBuilder.Append(",");
      stringBuilder.Append("\"is_cmn\":");
      stringBuilder.Append(!is_cmn ? 0 : 1);
      stringBuilder.Append(",");
      stringBuilder.Append("\"current_rank\":");
      stringBuilder.Append(current_rank);
      stringBuilder.Append(",");
      stringBuilder.Append("\"target_rank\":");
      stringBuilder.Append(target_rank);
      if (isEquips == 1)
      {
        stringBuilder.Append(",");
        stringBuilder.Append("\"isEquips\":");
        stringBuilder.Append(isEquips);
      }
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
