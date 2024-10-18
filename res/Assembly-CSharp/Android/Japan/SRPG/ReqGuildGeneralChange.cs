// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGuildGeneralChange
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqGuildGeneralChange : WebAPI
  {
    public ReqGuildGeneralChange(string target_user_uid, Network.ResponseCallback response)
    {
      this.name = "guild/appoint/general";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"target_uid\":\"");
      stringBuilder.Append(target_user_uid);
      stringBuilder.Append("\"");
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
