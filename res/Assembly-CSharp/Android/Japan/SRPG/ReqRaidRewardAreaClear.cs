// Decompiled with JetBrains decompiler
// Type: SRPG.ReqRaidRewardAreaClear
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class ReqRaidRewardAreaClear : WebAPI
  {
    public ReqRaidRewardAreaClear(int area_id, int round, Network.ResponseCallback response)
    {
      this.name = "raidboss/reward/area_clear";
      this.body = WebAPI.GetRequestString<ReqRaidRewardAreaClear.RequestParam>(new ReqRaidRewardAreaClear.RequestParam()
      {
        area_id = area_id,
        round = round
      });
      this.callback = response;
    }

    [Serializable]
    public class RequestParam
    {
      public int area_id;
      public int round;
    }

    [Serializable]
    public class Response
    {
      public Json_PlayerData player;
      public Json_Item[] items;
      public Json_Unit[] units;
      public JSON_ConceptCard[] cards;
      public Json_Gift[] reward;
      public JSON_TrophyProgress[] trophyprogs;
      public JSON_TrophyProgress[] bingoprogs;
    }
  }
}
