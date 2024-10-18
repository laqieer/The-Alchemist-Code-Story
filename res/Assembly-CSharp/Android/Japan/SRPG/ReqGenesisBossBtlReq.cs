// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGenesisBossBtlReq
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class ReqGenesisBossBtlReq : WebAPI
  {
    public ReqGenesisBossBtlReq(string area_id, string qid, QuestDifficulties difficulty, Network.ResponseCallback response)
    {
      this.name = "genesis/raidboss/btl/req";
      this.body = WebAPI.GetRequestString<ReqGenesisBossBtlReq.RequestParam>(new ReqGenesisBossBtlReq.RequestParam()
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
