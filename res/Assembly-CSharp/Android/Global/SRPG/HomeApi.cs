// Decompiled with JetBrains decompiler
// Type: SRPG.HomeApi
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
