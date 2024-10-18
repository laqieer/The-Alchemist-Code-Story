// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGuildRequestEntryApprovalNG
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqGuildRequestEntryApprovalNG : WebAPI
  {
    public ReqGuildRequestEntryApprovalNG(string[] request_user_uids, Network.ResponseCallback response)
    {
      this.name = "guild/apply/reject";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"target_uids\":[");
      for (int index = 0; index < request_user_uids.Length; ++index)
      {
        stringBuilder.Append("\"" + request_user_uids[index] + "\"");
        if (index != request_user_uids.Length - 1)
          stringBuilder.Append(",");
      }
      stringBuilder.Append("]");
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
