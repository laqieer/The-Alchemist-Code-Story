// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGenesisBossBtlSkip
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class ReqGenesisBossBtlSkip : WebAPI
  {
    public ReqGenesisBossBtlSkip(
      string area_id,
      string qid,
      QuestDifficulties difficulty,
      int ticket,
      Network.ResponseCallback response,
      string trophyprog = null,
      string bingoprog = null)
    {
      this.name = "genesis/raidboss/skip";
      this.body = WebAPI.GetRequestString<ReqGenesisBossBtlSkip.RequestParam>(new ReqGenesisBossBtlSkip.RequestParam()
      {
        area_id = area_id,
        qid = qid,
        difficulty = (int) difficulty,
        ticket = ticket
      });
      if (!string.IsNullOrEmpty(trophyprog) && this.body.IndexOf("\"btlendparam\"") != -1)
        this.body = this.body.Insert(this.body.IndexOf("\"btlendparam\""), trophyprog + ",");
      if (!string.IsNullOrEmpty(bingoprog) && this.body.IndexOf("\"btlendparam\"") != -1)
        this.body = this.body.Insert(this.body.IndexOf("\"btlendparam\""), bingoprog + ",");
      this.callback = response;
    }

    [Serializable]
    public class RequestParam
    {
      public string area_id;
      public string qid;
      public int difficulty;
      public int ticket;
    }
  }
}
