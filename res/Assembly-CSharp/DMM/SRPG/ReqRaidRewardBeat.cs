// Decompiled with JetBrains decompiler
// Type: SRPG.ReqRaidRewardBeat
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class ReqRaidRewardBeat : WebAPI
  {
    public ReqRaidRewardBeat(
      int area_id,
      int boss_id,
      int round,
      string uid,
      Network.ResponseCallback response)
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
      public Json_Artifact[] artifacts;
      public JSON_ConceptCard[] cards;
      public Json_Gift[] raid_beat_reward;
      public Json_Gift[] raid_damage_ratio_reward;
      public Json_Gift[] raid_damage_amount_reward;
      public JSON_TrophyProgress[] trophyprogs;
      public JSON_TrophyProgress[] bingoprogs;
    }
  }
}
