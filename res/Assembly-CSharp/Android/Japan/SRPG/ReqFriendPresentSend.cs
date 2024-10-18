// Decompiled with JetBrains decompiler
// Type: SRPG.ReqFriendPresentSend
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqFriendPresentSend : WebAPI
  {
    public ReqFriendPresentSend(string url, Network.ResponseCallback response, string text, string trophyprog = null, string bingoprog = null)
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
