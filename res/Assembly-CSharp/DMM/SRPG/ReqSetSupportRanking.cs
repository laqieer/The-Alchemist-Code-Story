// Decompiled with JetBrains decompiler
// Type: SRPG.ReqSetSupportRanking
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  public class ReqSetSupportRanking : WebAPI
  {
    public ReqSetSupportRanking(
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
    {
      this.name = "support/ranking";
      this.body = WebAPI.GetRequestString(string.Empty);
      this.callback = response;
      this.serializeCompressMethod = serializeCompressMethod;
    }

    [MessagePackObject(true)]
    [Serializable]
    public class Response
    {
      public string date;
      public JSON_SupportRankingUser user_ranking;
      public JSON_SupportRankingUnit unit_ranking;
    }
  }
}
