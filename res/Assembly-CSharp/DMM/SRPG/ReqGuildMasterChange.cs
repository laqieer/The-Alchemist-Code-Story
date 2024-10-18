// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGuildMasterChange
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Text;

#nullable disable
namespace SRPG
{
  public class ReqGuildMasterChange : WebAPI
  {
    public ReqGuildMasterChange(string target_user_uid, Network.ResponseCallback response)
    {
      this.name = "guild/transfer/master";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"target_uid\":\"");
      stringBuilder.Append(target_user_uid);
      stringBuilder.Append("\"");
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
