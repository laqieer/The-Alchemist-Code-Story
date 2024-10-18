// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGoogleReview
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqGoogleReview : WebAPI
  {
    public ReqGoogleReview(Network.ResponseCallback response)
    {
      this.name = "serial/register/greview";
      this.body = WebAPI.GetRequestString(WebAPI.GetStringBuilder().ToString());
      this.callback = response;
    }
  }
}
