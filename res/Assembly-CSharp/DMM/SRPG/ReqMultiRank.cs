// Decompiled with JetBrains decompiler
// Type: SRPG.ReqMultiRank
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class ReqMultiRank : WebAPI
  {
    public ReqMultiRank(string iname, Network.ResponseCallback response)
    {
      this.name = "btl/usedunit";
      this.body = "\"iname\":\"" + JsonEscape.Escape(iname) + "\"";
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }

    public class Json_MultiRankParam
    {
      public string unit_iname;
      public string job_iname;
    }

    public class Json_MultiRank
    {
      public ReqMultiRank.Json_MultiRankParam[] ranking;
      public int isReady;
    }
  }
}
