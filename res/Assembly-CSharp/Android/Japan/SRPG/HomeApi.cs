// Decompiled with JetBrains decompiler
// Type: SRPG.HomeApi
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class HomeApi : WebAPI
  {
    public HomeApi(bool isMultiPush, Network.ResponseCallback response)
    {
      this.name = "home";
      if (isMultiPush)
      {
        StringBuilder stringBuilder = WebAPI.GetStringBuilder();
        stringBuilder.Append("\"is_multi_push\":1");
        this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      }
      else
        this.body = WebAPI.GetRequestString((string) null);
      this.callback = response;
    }
  }
}
