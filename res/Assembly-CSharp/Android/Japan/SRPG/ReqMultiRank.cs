// Decompiled with JetBrains decompiler
// Type: SRPG.ReqMultiRank
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
