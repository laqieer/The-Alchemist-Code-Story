﻿// Decompiled with JetBrains decompiler
// Type: SRPG.RankingQuestUserData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;

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
    public string m_UID;

    public bool IsActionCountRanking
    {
      get
      {
        return this.m_RankingType == RankingQuestType.ActionCount;
      }
    }

    public void Deserialize(FlowNode_ReqQuestRanking.Json_OthersRankingData json)
    {
      this.m_PlayerName = json.name;
      this.m_Rank = json.rank;
      this.m_MainScore = json.main_score;
      this.m_SubScore = json.sub_score;
      this.m_UID = json.uid;
      this.m_UnitData = new UnitBuilder(json.unit_iname).SetExpByLevel(json.unit_lv).SetJob(string.Empty, json.job_lv).SetAwake(25).SetRarity(5).Build();
    }

    public void Deserialize(FlowNode_ReqQuestRanking.Json_OwnRankingData json)
    {
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if ((UnityEngine.Object) instanceDirect == (UnityEngine.Object) null)
        return;
      this.m_PlayerName = instanceDirect.Player.Name;
      this.m_Rank = json.rank;
      this.m_MainScore = json.main_score;
      this.m_SubScore = json.sub_score;
      this.m_UID = instanceDirect.DeviceId;
      this.m_UnitData = new UnitBuilder(json.unit.unit_iname).SetExpByLevel(json.unit.unit_lv).SetJob(string.Empty, json.unit.job_lv).SetAwake(25).SetRarity(5).Build();
    }

    public static RankingQuestUserData CreateRankingUserDataFromJson(FlowNode_ReqQuestRanking.Json_OwnRankingData json, RankingQuestType type)
    {
      RankingQuestUserData rankingQuestUserData = (RankingQuestUserData) null;
      if (json != null)
      {
        rankingQuestUserData = new RankingQuestUserData();
        rankingQuestUserData.Deserialize(json);
        rankingQuestUserData.m_RankingType = type;
      }
      return rankingQuestUserData;
    }

    public static RankingQuestUserData[] CreateRankingUserDataFromJson(FlowNode_ReqQuestRanking.Json_OthersRankingData[] json, RankingQuestType type)
    {
      RankingQuestUserData[] rankingQuestUserDataArray = new RankingQuestUserData[0];
      if (json != null)
      {
        rankingQuestUserDataArray = new RankingQuestUserData[json.Length];
        for (int index = 0; index < rankingQuestUserDataArray.Length; ++index)
        {
          rankingQuestUserDataArray[index] = new RankingQuestUserData();
          rankingQuestUserDataArray[index].Deserialize(json[index]);
          rankingQuestUserDataArray[index].m_RankingType = type;
        }
      }
      return rankingQuestUserDataArray;
    }
  }
}
