// Decompiled with JetBrains decompiler
// Type: SRPG.ReqJobRankupAll
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqJobRankupAll : WebAPI
  {
    public ReqJobRankupAll(long iid_unit, string iname_jobset, bool is_cmn, Network.ResponseCallback response)
    {
      this.name = "unit/job/equip/lvupall";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"uiid\":");
      stringBuilder.Append(iid_unit);
      stringBuilder.Append(",\"jobset\":\"");
      stringBuilder.Append(iname_jobset);
      stringBuilder.Append("\"");
      stringBuilder.Append(",\"is_cmn\":");
      stringBuilder.Append(!is_cmn ? 0 : 1);
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }

    public ReqJobRankupAll(long iid_unit, string iname_jobset, bool is_cmn, int current_rank, int target_rank, int isEquips, Network.ResponseCallback response)
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
