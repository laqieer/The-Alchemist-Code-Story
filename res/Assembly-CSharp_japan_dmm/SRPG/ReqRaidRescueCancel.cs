// Decompiled with JetBrains decompiler
// Type: SRPG.ReqRaidRescueCancel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class ReqRaidRescueCancel : WebAPI
  {
    public ReqRaidRescueCancel(
      string uid,
      int area_id,
      int boss_id,
      int round,
      Network.ResponseCallback response)
    {
      this.name = "raidboss/rescue/cancel";
      this.body = WebAPI.GetRequestString<ReqRaidRescueCancel.RequestParam>(new ReqRaidRescueCancel.RequestParam()
      {
        uid = uid,
        area_id = area_id,
        boss_id = boss_id,
        round = round
      });
      this.callback = response;
    }

    [Serializable]
    public class RequestParam
    {
      public string uid;
      public int area_id;
      public int boss_id;
      public int round;
    }
  }
}
