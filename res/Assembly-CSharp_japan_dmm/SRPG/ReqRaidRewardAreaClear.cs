// Decompiled with JetBrains decompiler
// Type: SRPG.ReqRaidRewardAreaClear
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
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
      public Json_Artifact[] artifacts;
      public Json_Gift[] reward;
      public JSON_TrophyProgress[] trophyprogs;
      public JSON_TrophyProgress[] bingoprogs;
    }
  }
}
