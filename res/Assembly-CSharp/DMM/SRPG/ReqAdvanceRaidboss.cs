// Decompiled with JetBrains decompiler
// Type: SRPG.ReqAdvanceRaidboss
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class ReqAdvanceRaidboss : WebAPI
  {
    public ReqAdvanceRaidboss(
      string area_id,
      QuestDifficulties difficulty,
      Network.ResponseCallback response)
    {
      this.name = "advance/raidboss";
      this.body = WebAPI.GetRequestString<ReqAdvanceRaidboss.RequestParam>(new ReqAdvanceRaidboss.RequestParam()
      {
        area_id = area_id,
        difficulty = (int) difficulty
      });
      this.callback = response;
    }

    [Serializable]
    public class RequestParam
    {
      public string area_id;
      public int difficulty;
    }

    [Serializable]
    public class Response
    {
      public int hp;
      public int round;
    }
  }
}
