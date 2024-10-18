// Decompiled with JetBrains decompiler
// Type: SRPG.ReqRankMatchMake
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqRankMatchMake : WebAPI
  {
    public ReqRankMatchMake(Network.ResponseCallback response = null)
    {
      this.name = "vs/rankmatch/make";
      this.body = WebAPI.GetRequestString(string.Empty);
      this.callback = response;
    }

    public class Response
    {
      public string token;
      public string owner_name;
    }
  }
}
