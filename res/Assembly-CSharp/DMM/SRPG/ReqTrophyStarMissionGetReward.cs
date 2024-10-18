// Decompiled with JetBrains decompiler
// Type: SRPG.ReqTrophyStarMissionGetReward
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  public class ReqTrophyStarMissionGetReward : WebAPI
  {
    public ReqTrophyStarMissionGetReward(
      string tsm_iname,
      int get_index,
      int ymd,
      int daily_ymd,
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
    {
      this.name = "trophy/star_mission/add_reward";
      this.body = WebAPI.GetRequestString<ReqTrophyStarMissionGetReward.RequestParam>(new ReqTrophyStarMissionGetReward.RequestParam()
      {
        iname = tsm_iname,
        idx = get_index,
        ymd = ymd,
        daily_ymd = daily_ymd
      });
      this.callback = response;
      this.serializeCompressMethod = serializeCompressMethod;
    }

    [Serializable]
    public class RequestParam
    {
      public string iname;
      public int idx;
      public int ymd;
      public int daily_ymd;
    }

    [MessagePackObject(true)]
    [Serializable]
    public class Response
    {
      public ReqTrophyStarMission.StarMission star_mission;
      public Json_PlayerData player;
      public Json_Item[] items;
      public Json_Unit[] units;
      public ReqTrophyStarMissionGetReward.Response.JSON_StarMissionConceptCard[] cards;
      public Json_Artifact[] artifacts;
      public JSON_TrophyProgress[] trophyprogs;
      public JSON_TrophyProgress[] bingoprogs;

      [MessagePackObject(true)]
      [Serializable]
      public class JSON_StarMissionConceptCard
      {
        public string iname;
        public int num;
        public string get_unit;
        public int is_mail;
      }
    }
  }
}
