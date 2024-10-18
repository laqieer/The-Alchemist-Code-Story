// Decompiled with JetBrains decompiler
// Type: SRPG.TrophyStarMissionParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class TrophyStarMissionParam
  {
    public string Iname;
    public TrophyStarMissionParam.eStarMissionType Type;
    public DateTime BeginAt;
    public DateTime EndAt;
    private TrophyStarMissionParam.StarSetParam[] mStarSets;
    public static TrophyStarMissionParam.eStarMissionType SelectStarMissionType;
    public static int SelectDailyTreasureIndex = -1;
    public static int SelectWeeklyTreasureIndex = -1;

    public List<TrophyStarMissionParam.StarSetParam> StarSetList
    {
      get
      {
        return this.mStarSets != null ? new List<TrophyStarMissionParam.StarSetParam>((IEnumerable<TrophyStarMissionParam.StarSetParam>) this.mStarSets) : new List<TrophyStarMissionParam.StarSetParam>();
      }
    }

    public void Deserialize(JSON_TrophyStarMissionParam json)
    {
      if (json == null)
        return;
      this.Iname = json.iname;
      this.Type = (TrophyStarMissionParam.eStarMissionType) json.type;
      this.BeginAt = DateTime.MinValue;
      if (!string.IsNullOrEmpty(json.begin_at) && DateTime.TryParse(json.begin_at, out this.BeginAt))
        this.BeginAt = this.BeginAt.Date;
      this.EndAt = DateTime.MaxValue;
      if (!string.IsNullOrEmpty(json.end_at) && DateTime.TryParse(json.end_at, out this.EndAt))
        this.EndAt = this.EndAt.Date;
      this.mStarSets = (TrophyStarMissionParam.StarSetParam[]) null;
      if (json.stars == null || json.stars.Length == 0)
        return;
      this.mStarSets = new TrophyStarMissionParam.StarSetParam[json.stars.Length];
      for (int index = 0; index < json.stars.Length; ++index)
      {
        this.mStarSets[index] = new TrophyStarMissionParam.StarSetParam();
        this.mStarSets[index].Deserialize(json.stars[index]);
      }
    }

    public static void Deserialize(
      JSON_TrophyStarMissionParam[] json,
      ref Dictionary<string, TrophyStarMissionParam> dict)
    {
      if (json == null)
        return;
      if (dict == null)
        dict = new Dictionary<string, TrophyStarMissionParam>(json.Length);
      dict.Clear();
      for (int index = 0; index < json.Length; ++index)
      {
        TrophyStarMissionParam starMissionParam = new TrophyStarMissionParam();
        starMissionParam.Deserialize(json[index]);
        if (!dict.ContainsKey(json[index].iname))
          dict.Add(json[index].iname, starMissionParam);
      }
    }

    public static TrophyStarMissionParam GetParam(string key)
    {
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) MonoSingleton<GameManager>.Instance))
        return (TrophyStarMissionParam) null;
      Dictionary<string, TrophyStarMissionParam> trophyStarMissionDict = MonoSingleton<GameManager>.Instance.MasterParam.TrophyStarMissionDict;
      if (trophyStarMissionDict == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>TrophyStarMissionParam/GetParam no data!</color>"));
        return (TrophyStarMissionParam) null;
      }
      try
      {
        return trophyStarMissionDict[key];
      }
      catch (Exception ex)
      {
        throw new KeyNotFoundException<TrophyStarMissionParam>(key);
      }
    }

    public static bool EntryTrophyStarMission(ReqTrophyStarMission.StarMission star_mission)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) instance) || instance.Player == null)
        return false;
      if (star_mission == null)
        return true;
      PlayerData.TrophyStarMission trophyStarMissionInfo1 = instance.Player.TrophyStarMissionInfo;
      if (trophyStarMissionInfo1 != null)
      {
        trophyStarMissionInfo1.Update(star_mission);
      }
      else
      {
        instance.Player.TrophyStarMissionInfo = new PlayerData.TrophyStarMission();
        PlayerData.TrophyStarMission trophyStarMissionInfo2 = instance.Player.TrophyStarMissionInfo;
        trophyStarMissionInfo2.Update(star_mission);
        if (trophyStarMissionInfo2.Daily == null || trophyStarMissionInfo2.Daily.TsmParam == null || trophyStarMissionInfo2.Daily.Rewards == null || trophyStarMissionInfo2.Weekly == null || trophyStarMissionInfo2.Weekly.TsmParam == null || trophyStarMissionInfo2.Weekly.Rewards == null)
          return false;
      }
      return true;
    }

    public enum eStarMissionType
    {
      DAILY,
      WEEKLY,
    }

    public class StarSetParam
    {
      public OInt RequireStar;
      public string TsmRewardId;
      public int IconIndex;
      private TrophyStarMissionRewardParam mTsmReward;

      public TrophyStarMissionRewardParam TsmReward
      {
        get
        {
          if (this.mTsmReward == null && !string.IsNullOrEmpty(this.TsmRewardId))
            this.mTsmReward = TrophyStarMissionRewardParam.GetParam(this.TsmRewardId);
          return this.mTsmReward;
        }
      }

      public void Deserialize(JSON_TrophyStarMissionParam.StarSetParam json)
      {
        if (json == null)
          return;
        this.RequireStar = (OInt) json.require_star;
        this.TsmRewardId = json.reward_id;
        this.IconIndex = json.icon;
      }
    }
  }
}
