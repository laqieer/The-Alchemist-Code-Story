// Decompiled with JetBrains decompiler
// Type: SRPG.ReqTowerFloorRanking
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Text;

#nullable disable
namespace SRPG
{
  public class ReqTowerFloorRanking : WebAPI
  {
    public ReqTowerFloorRanking(
      string tower_iname,
      string floor_iname,
      Network.ResponseCallback response)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      this.name = "tower/floor/ranking";
      stringBuilder.Append(WebAPI.KeyValueToString(nameof (tower_iname), tower_iname));
      stringBuilder.Append(",");
      stringBuilder.Append(WebAPI.KeyValueToString(nameof (floor_iname), floor_iname));
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }

    [Serializable]
    public class Json_Response
    {
      public ReqTowerRank.JSON_TowerRankParam[] speed;
      public ReqTowerRank.JSON_TowerRankParam[] technical;
      public ReqTowerFloorRanking.Json_Score score;
    }

    [Serializable]
    public class Json_Score
    {
      public int turn_num;
      public int died_num;
      public int retire_num;
      public int recovery_num;
      public int reset_num;
      public int lose_num;
      public int challenge_num;
    }
  }
}
