// Decompiled with JetBrains decompiler
// Type: SRPG.ReqRaidBtlReq
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class ReqRaidBtlReq : WebAPI
  {
    public ReqRaidBtlReq(
      int area_id,
      int boss_id,
      int round,
      string uid,
      Network.ResponseCallback response)
    {
      this.name = "raidboss/btl/req";
      this.body = WebAPI.GetRequestString<ReqRaidBtlReq.RequestParam>(new ReqRaidBtlReq.RequestParam()
      {
        area_id = area_id,
        boss_id = boss_id,
        round = round,
        uid = uid
      });
      this.callback = response;
    }

    [Serializable]
    public class RequestParam
    {
      public int area_id;
      public int boss_id;
      public int round;
      public string uid;
    }
  }
}
