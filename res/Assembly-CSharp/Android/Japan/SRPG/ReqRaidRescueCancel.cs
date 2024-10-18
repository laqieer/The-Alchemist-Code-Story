// Decompiled with JetBrains decompiler
// Type: SRPG.ReqRaidRescueCancel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class ReqRaidRescueCancel : WebAPI
  {
    public ReqRaidRescueCancel(string uid, int area_id, int boss_id, int round, Network.ResponseCallback response)
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
