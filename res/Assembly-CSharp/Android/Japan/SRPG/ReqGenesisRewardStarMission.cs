// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGenesisRewardStarMission
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class ReqGenesisRewardStarMission : WebAPI
  {
    public ReqGenesisRewardStarMission(string area_id, QuestDifficulties difficult, int star_index, Network.ResponseCallback response)
    {
      this.name = "genesis/reward/star_mission";
      this.body = WebAPI.GetRequestString<ReqGenesisRewardStarMission.RequestParam>(new ReqGenesisRewardStarMission.RequestParam()
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
      public ReqBtlCom.GenesisStar[] genesis_stars;
      public Json_Gift[] reward;
      public Json_PlayerData player;
      public Json_Item[] items;
      public Json_Unit[] units;
      public JSON_ConceptCard[] cards;
      public JSON_TrophyProgress[] trophyprogs;
      public JSON_TrophyProgress[] bingoprogs;
    }
  }
}
