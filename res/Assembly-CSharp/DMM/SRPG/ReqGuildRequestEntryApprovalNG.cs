// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGuildRequestEntryApprovalNG
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Text;

#nullable disable
namespace SRPG
{
  public class ReqGuildRequestEntryApprovalNG : WebAPI
  {
    public ReqGuildRequestEntryApprovalNG(
      string[] request_user_uids,
      Network.ResponseCallback response)
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
