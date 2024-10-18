// Decompiled with JetBrains decompiler
// Type: SRPG.RankingQuestUserData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class RankingQuestUserData
  {
    public string m_PlayerName;
    public int m_PlayerLevel;
    public int m_Rank;
    public UnitData m_UnitData;
    public int m_MainScore;
    public int m_SubScore;
    public RankingQuestType m_RankingType;
    public ViewGuildData ViewGuild;
    public string m_UID;

    public bool IsActionCountRanking => this.m_RankingType == RankingQuestType.ActionCount;

    public void Deserialize(
      FlowNode_ReqQuestRanking.Json_OthersRankingData json)
    {
      this.m_PlayerName = json.name;
      this.m_Rank = json.rank;
      this.m_MainScore = json.main_score;
      this.m_SubScore = json.sub_score;
      this.m_UID = json.uid;
      this.m_UnitData = new UnitBuilder(json.unit_iname).SetExpByLevel(json.unit_lv).SetJob(string.Empty, json.job_lv).SetAwake(25).SetRarity(5).SetUnlockTobiraNum(7).Build();
      if (json.guild == null)
        return;
      this.ViewGuild = new ViewGuildData();
      this.ViewGuild.Deserialize(json.guild);
    }

    public void Deserialize(FlowNode_ReqQuestRanking.Json_OwnRankingData json)
    {
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if (Object.op_Equality((Object) instanceDirect, (Object) null))
        return;
      this.m_PlayerName = instanceDirect.Player.Name;
      this.m_Rank = json.rank;
      this.m_MainScore = json.main_score;
      this.m_SubScore = json.sub_score;
      this.m_UID = instanceDirect.DeviceId;
      this.m_UnitData = new UnitBuilder(json.unit.unit_iname).SetExpByLevel(json.unit.unit_lv).SetJob(string.Empty, json.unit.job_lv).SetAwake(25).SetRarity(5).SetUnlockTobiraNum(7).Build();
    }

    public static RankingQuestUserData CreateRankingUserDataFromJson(
      FlowNode_ReqQuestRanking.Json_OwnRankingData json,
      RankingQuestType type)
    {
      RankingQuestUserData userDataFromJson = (RankingQuestUserData) null;
      if (json != null)
      {
        userDataFromJson = new RankingQuestUserData();
        userDataFromJson.Deserialize(json);
        userDataFromJson.m_RankingType = type;
      }
      return userDataFromJson;
    }

    public static RankingQuestUserData[] CreateRankingUserDataFromJson(
      FlowNode_ReqQuestRanking.Json_OthersRankingData[] json,
      RankingQuestType type)
    {
      RankingQuestUserData[] userDataFromJson = new RankingQuestUserData[0];
      if (json != null)
      {
        userDataFromJson = new RankingQuestUserData[json.Length];
        for (int index = 0; index < userDataFromJson.Length; ++index)
        {
          userDataFromJson[index] = new RankingQuestUserData();
          userDataFromJson[index].Deserialize(json[index]);
          userDataFromJson[index].m_RankingType = type;
        }
      }
      return userDataFromJson;
    }
  }
}
