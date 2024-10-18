// Decompiled with JetBrains decompiler
// Type: SRPG.ReqRaidRewardBeat
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class ReqRaidRewardBeat : WebAPI
  {
    public ReqRaidRewardBeat(int area_id, int boss_id, int round, string uid, Network.ResponseCallback response)
    {
      this.name = "raidboss/reward/beat";
      this.body = WebAPI.GetRequestString<ReqRaidRewardBeat.RequestParam>(new ReqRaidRewardBeat.RequestParam()
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

    [Serializable]
    public class Response
    {
      public Json_PlayerData player;
      public Json_Item[] items;
      public Json_Unit[] units;
      public JSON_ConceptCard[] cards;
      public Json_Gift[] raid_beat_reward;
      public Json_Gift[] raid_damage_ratio_reward;
      public JSON_TrophyProgress[] trophyprogs;
      public JSON_TrophyProgress[] bingoprogs;
    }
  }
}
