// Decompiled with JetBrains decompiler
// Type: SRPG.ReqAdvanceRewardStarMission
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class ReqAdvanceRewardStarMission : WebAPI
  {
    public ReqAdvanceRewardStarMission(
      string area_id,
      QuestDifficulties difficult,
      int star_index,
      Network.ResponseCallback response)
    {
      this.name = "advance/reward/star_mission";
      this.body = WebAPI.GetRequestString<ReqAdvanceRewardStarMission.RequestParam>(new ReqAdvanceRewardStarMission.RequestParam()
      {
        area_id = area_id,
        difficulty = (int) difficult,
        star_index = star_index
      });
      this.callback = response;
    }

    [Serializable]
    public class RequestParam
    {
      public string area_id;
      public int difficulty;
      public int star_index;
    }

    [Serializable]
    public class Response
    {
      public ReqBtlCom.AdvanceStar[] advance_stars;
      public Json_Gift[] reward;
      public Json_PlayerData player;
      public Json_Item[] items;
      public Json_Unit[] units;
      public JSON_ConceptCard[] cards;
      public Json_Artifact[] artifacts;
      public JSON_TrophyProgress[] trophyprogs;
      public JSON_TrophyProgress[] bingoprogs;
    }
  }
}
