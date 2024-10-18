// Decompiled with JetBrains decompiler
// Type: SRPG.GvGLeagueManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class GvGLeagueManager : MonoBehaviour
  {
    private static GvGLeagueManager mInstance;

    public static GvGLeagueManager Instance => GvGLeagueManager.mInstance;

    public Dictionary<string, GvGLeagueManager.GvGLeagueDataList> LeagueData { get; set; }

    public GvGLeagueManager.GvGLeagueDataList AllLeagueData { get; set; }

    public GvGLeagueViewGuild MyGuildLeagueData { get; set; }

    private void Awake() => GvGLeagueManager.mInstance = this;

    private void OnDestroy() => GvGLeagueManager.mInstance = (GvGLeagueManager) null;

    public bool SetupLeagueData(JSON_GvGLeagueViewGuild[] league)
    {
      if (league == null)
        return false;
      if (this.LeagueData == null)
        this.LeagueData = new Dictionary<string, GvGLeagueManager.GvGLeagueDataList>();
      List<GvGLeagueManager.GvGLeagueDataList> gvGleagueDataListList = new List<GvGLeagueManager.GvGLeagueDataList>();
      int gid = MonoSingleton<GameManager>.Instance.Player.PlayerGuild.Gid;
      for (int index = 0; index < league.Length; ++index)
      {
        if (league[index].league != null)
        {
          string gvGleagueId = GvGLeagueParam.GetGvGLeagueId(league[index].league.rate);
          GvGLeagueManager.GvGLeagueDataList gvGleagueDataList = (GvGLeagueManager.GvGLeagueDataList) null;
          if (!this.LeagueData.TryGetValue(gvGleagueId, out gvGleagueDataList))
          {
            gvGleagueDataList = new GvGLeagueManager.GvGLeagueDataList();
            this.LeagueData.Add(gvGleagueId, gvGleagueDataList);
          }
          gvGleagueDataList.Deserialize(league[index]);
          if (gid == league[index].id)
            this.MyGuildLeagueData = gvGleagueDataList.FindGvGLeagueViewGuild(gid);
          if (!gvGleagueDataListList.Contains(gvGleagueDataList))
            gvGleagueDataListList.Add(gvGleagueDataList);
        }
      }
      for (int index = 0; index < gvGleagueDataListList.Count; ++index)
        gvGleagueDataListList[index].SortByRank();
      return true;
    }

    public bool SetupAllLeagueData(JSON_GvGLeagueViewGuild[] league)
    {
      if (league == null)
        return false;
      if (this.AllLeagueData == null)
        this.AllLeagueData = new GvGLeagueManager.GvGLeagueDataList();
      for (int index = 0; index < league.Length; ++index)
      {
        if (league[index].league != null)
          this.AllLeagueData.Deserialize(league[index]);
      }
      this.AllLeagueData.SortByRank();
      return true;
    }

    public void SetAllLeagueTotalCount(int totalCount)
    {
      if (this.AllLeagueData == null)
        this.AllLeagueData = new GvGLeagueManager.GvGLeagueDataList();
      this.AllLeagueData.TotalLeagueCount = totalCount;
    }

    public void SetLeagueTotalCount(string leagueID, int totalCount)
    {
      GvGLeagueManager.GvGLeagueDataList gvGleagueDataList = (GvGLeagueManager.GvGLeagueDataList) null;
      if (!this.LeagueData.TryGetValue(leagueID, out gvGleagueDataList))
        this.LeagueData.Add(leagueID, new GvGLeagueManager.GvGLeagueDataList()
        {
          TotalLeagueCount = totalCount
        });
      else
        gvGleagueDataList.TotalLeagueCount = totalCount;
    }

    public GvGLeagueManager.GvGLeagueDataList GetLeagueData(string leagueID)
    {
      GvGLeagueManager.GvGLeagueDataList leagueData = (GvGLeagueManager.GvGLeagueDataList) null;
      if (this.LeagueData == null)
        return (GvGLeagueManager.GvGLeagueDataList) null;
      if (string.IsNullOrEmpty(leagueID))
        return (GvGLeagueManager.GvGLeagueDataList) null;
      this.LeagueData.TryGetValue(leagueID, out leagueData);
      return leagueData;
    }

    public GvGLeagueViewGuild[] GetAllLeagueGuilds()
    {
      return this.AllLeagueData == null ? (GvGLeagueViewGuild[]) null : this.AllLeagueData.GetLeagueGuilds();
    }

    public GvGLeagueManager.GvGLeagueDataList GetAllLeagueData() => this.AllLeagueData;

    public GvGLeagueViewGuild GetGvGLeagueViewGuild(int guildID)
    {
      GvGLeagueViewGuild gleagueViewGuild = (GvGLeagueViewGuild) null;
      foreach (KeyValuePair<string, GvGLeagueManager.GvGLeagueDataList> keyValuePair in this.LeagueData)
      {
        gleagueViewGuild = keyValuePair.Value.FindGvGLeagueViewGuild(guildID);
        if (gleagueViewGuild != null)
          break;
      }
      return gleagueViewGuild;
    }

    public class GvGLeagueDataList
    {
      private List<GvGLeagueViewGuild> m_GvGLeagueData = new List<GvGLeagueViewGuild>();
      private int m_TotalCount;

      public int TotalLeagueCount
      {
        get => this.m_TotalCount;
        set => this.m_TotalCount = value;
      }

      public void Deserialize(JSON_GvGLeagueViewGuild json)
      {
        int index = this.m_GvGLeagueData.FindIndex((Predicate<GvGLeagueViewGuild>) (l => l.id == json.id));
        if (index != -1)
        {
          this.m_GvGLeagueData[index].Deserialize(json);
        }
        else
        {
          GvGLeagueViewGuild gleagueViewGuild = new GvGLeagueViewGuild();
          gleagueViewGuild.Deserialize(json);
          this.m_GvGLeagueData.Add(gleagueViewGuild);
        }
      }

      public void SortByRank()
      {
        this.m_GvGLeagueData.Sort((Comparison<GvGLeagueViewGuild>) ((left, right) => left.league.Rank.CompareTo(right.league.Rank)));
      }

      public GvGLeagueViewGuild FindGvGLeagueViewGuild(int guildID)
      {
        return this.m_GvGLeagueData.Find((Predicate<GvGLeagueViewGuild>) (guild => guild.id == guildID));
      }

      public GvGLeagueViewGuild[] GetLeagueGuilds() => this.m_GvGLeagueData.ToArray();
    }
  }
}
