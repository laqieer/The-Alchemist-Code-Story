// Decompiled with JetBrains decompiler
// Type: SRPG.ReqFriendPresentSend
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Text;

#nullable disable
namespace SRPG
{
  public class ReqFriendPresentSend : WebAPI
  {
    public ReqFriendPresentSend(
      string url,
      Network.ResponseCallback response,
      string text,
      string trophyprog = null,
      string bingoprog = null)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append(text);
      if (!string.IsNullOrEmpty(trophyprog))
        stringBuilder.Append(trophyprog);
      if (!string.IsNullOrEmpty(bingoprog))
      {
        if (!string.IsNullOrEmpty(trophyprog))
          stringBuilder.Append(",");
        stringBuilder.Append(bingoprog);
      }
      this.name = url;
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
