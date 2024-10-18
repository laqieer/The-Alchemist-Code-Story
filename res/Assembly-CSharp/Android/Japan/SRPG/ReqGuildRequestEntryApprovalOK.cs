// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGuildRequestEntryApprovalOK
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqGuildRequestEntryApprovalOK : WebAPI
  {
    public ReqGuildRequestEntryApprovalOK(string request_user_uid, Network.ResponseCallback response)
    {
      this.name = "guild/apply/accept";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"target_uid\":\"");
      stringBuilder.Append(request_user_uid);
      stringBuilder.Append("\"");
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
