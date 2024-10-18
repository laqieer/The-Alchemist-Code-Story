// Decompiled with JetBrains decompiler
// Type: SRPG.ReqAdvanceBossBtlReq
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class ReqAdvanceBossBtlReq : WebAPI
  {
    public ReqAdvanceBossBtlReq(
      string area_id,
      string qid,
      QuestDifficulties difficulty,
      Network.ResponseCallback response)
    {
      this.name = "advance/raidboss/btl/req";
      this.body = WebAPI.GetRequestString<ReqAdvanceBossBtlReq.RequestParam>(new ReqAdvanceBossBtlReq.RequestParam()
      {
        area_id = area_id,
        qid = qid,
        difficulty = (int) difficulty
      });
      this.callback = response;
    }

    [Serializable]
    public class RequestParam
    {
      public string area_id;
      public string qid;
      public int difficulty;
    }
  }
}
