// Decompiled with JetBrains decompiler
// Type: SRPG.ReqRankMatchMake
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
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
      public string btl_ver;
    }
  }
}
