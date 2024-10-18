// Decompiled with JetBrains decompiler
// Type: SRPG.GvGPeriodParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class GvGPeriodParam : GvGMasterParam<JSON_GvGPeriodParam>
  {
    public int Id { get; private set; }

    public DateTime PrepareAt { get; private set; }

    public DateTime BeginAt { get; private set; }

    public DateTime EndAt { get; private set; }

    public DateTime ExitAt { get; private set; }

    public string DeclarationStartTime { get; private set; }

    public string DeclarationEndTime { get; private set; }

    public int DeclarationCoolMinites { get; private set; }

    public string BattleStartTime { get; private set; }

    public string BattleEndTime { get; private set; }

    public string BattleCoolStartTime { get; private set; }

    public string BattleCoolEndTime { get; private set; }

    public int DeclaredCoolMinutes { get; private set; }

    public int BattleCoolSeconds { get; private set; }

    public int DeclareNum { get; private set; }

    public int MapIndex { get; private set; }

    public int MatchingCountMin { get; private set; }

    public int MatchingCountMax { get; private set; }

    public int FirstOccupyNodeNum { get; private set; }

    public int DefenseUnitMin { get; private set; }

    public string URL { get; private set; }

    public string URL_TITLE { get; private set; }

    public List<string> RuleCycle { get; private set; }

    public override bool Deserialize(JSON_GvGPeriodParam json)
    {
      if (json == null)
        return false;
      this.Id = json.id;
      DateTime result;
      DateTime.TryParse(json.prepare_start_at, out result);
      this.PrepareAt = result;
      DateTime.TryParse(json.begin_at, out result);
      this.BeginAt = result;
      DateTime.TryParse(json.end_at, out result);
      this.EndAt = result;
      DateTime.TryParse(json.exit_at, out result);
      this.ExitAt = result;
      this.DeclarationStartTime = json.declaration_start_time;
      this.DeclarationEndTime = json.declaration_end_time;
      this.DeclarationCoolMinites = json.declaration_cool_minites;
      this.BattleStartTime = json.battle_start_time;
      this.BattleEndTime = json.battle_end_time;
      this.BattleCoolStartTime = json.battle_cool_start_time;
      this.BattleCoolEndTime = json.battle_cool_end_time;
      this.DeclaredCoolMinutes = json.declared_cool_minutes;
      this.BattleCoolSeconds = json.battle_cool_seconds;
      this.DeclareNum = json.declare_num;
      this.MapIndex = json.map_idx;
      this.MatchingCountMin = json.matching_count_min;
      this.MatchingCountMax = json.matching_count_max;
      this.FirstOccupyNodeNum = json.first_occupy_node_num;
      this.DefenseUnitMin = json.defense_unit_min;
      this.URL = json.url;
      this.URL_TITLE = json.url_title;
      this.RuleCycle = new List<string>();
      if (json.rule_cycle != null)
      {
        for (int index = 0; index < json.rule_cycle.Length; ++index)
          this.RuleCycle.Add(json.rule_cycle[index]);
      }
      return true;
    }

    public static GvGPeriodParam GetGvGPeriod()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) MonoSingleton<GameManager>.Instance, (UnityEngine.Object) null))
        return (GvGPeriodParam) null;
      List<GvGPeriodParam> mGvGperiodParam = MonoSingleton<GameManager>.Instance.mGvGPeriodParam;
      if (mGvGperiodParam == null)
      {
        DebugUtility.Log("<color=yellow>QuestParam/mGvGPeriodParam no data!</color>");
        return (GvGPeriodParam) null;
      }
      DateTime serverTime = TimeManager.ServerTime;
      for (int index = 0; index < mGvGperiodParam.Count; ++index)
      {
        if (mGvGperiodParam[index].BeginAt <= serverTime && serverTime < mGvGperiodParam[index].EndAt)
          return mGvGperiodParam[index];
      }
      DebugUtility.Log("<color=yellow>QuestParam/GetGvGPeriod data not Schedule!");
      return (GvGPeriodParam) null;
    }

    public static GvGPeriodParam GetGvGExitPeriod()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) MonoSingleton<GameManager>.Instance, (UnityEngine.Object) null))
        return (GvGPeriodParam) null;
      List<GvGPeriodParam> mGvGperiodParam = MonoSingleton<GameManager>.Instance.mGvGPeriodParam;
      if (mGvGperiodParam == null)
      {
        DebugUtility.Log("<color=yellow>QuestParam/mGvGPeriodParam no data!</color>");
        return (GvGPeriodParam) null;
      }
      DateTime serverTime = TimeManager.ServerTime;
      for (int index = 0; index < mGvGperiodParam.Count; ++index)
      {
        if (mGvGperiodParam[index].PrepareAt <= serverTime && serverTime < mGvGperiodParam[index].ExitAt)
          return mGvGperiodParam[index];
      }
      DebugUtility.Log("<color=yellow>QuestParam/GetGvGPeriod data not Schedule!");
      return (GvGPeriodParam) null;
    }

    public static GvGPeriodParam GetGvGPeriod(int id)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) MonoSingleton<GameManager>.Instance, (UnityEngine.Object) null))
        return (GvGPeriodParam) null;
      List<GvGPeriodParam> mGvGperiodParam = MonoSingleton<GameManager>.Instance.mGvGPeriodParam;
      if (mGvGperiodParam != null)
        return mGvGperiodParam.Find((Predicate<GvGPeriodParam>) (gpp => gpp.Id == id));
      DebugUtility.Log("<color=yellow>QuestParam/mGvGPeriodParam no data!</color>");
      return (GvGPeriodParam) null;
    }

    public static GvGPeriodParam GetGvGNearPeriod()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) MonoSingleton<GameManager>.Instance, (UnityEngine.Object) null))
        return (GvGPeriodParam) null;
      DateTime dateTime = DateTime.MinValue;
      List<GvGPeriodParam> mGvGperiodParam = MonoSingleton<GameManager>.Instance.mGvGPeriodParam;
      if (mGvGperiodParam == null || mGvGperiodParam.Count == 0)
      {
        DebugUtility.Log("<color=yellow>QuestParam/mGvGPeriodParam no data!</color>");
        return (GvGPeriodParam) null;
      }
      DateTime serverTime = TimeManager.ServerTime;
      GvGPeriodParam gvGnearPeriod = mGvGperiodParam[0];
      for (int index = 0; index < mGvGperiodParam.Count; ++index)
      {
        if (mGvGperiodParam[index].BeginAt <= serverTime && serverTime < mGvGperiodParam[index].EndAt)
          return mGvGperiodParam[index];
        if (!(mGvGperiodParam[index].BeginAt > serverTime) && mGvGperiodParam[index].BeginAt >= dateTime)
        {
          dateTime = mGvGperiodParam[index].BeginAt;
          gvGnearPeriod = mGvGperiodParam[index];
        }
      }
      return gvGnearPeriod;
    }

    public static bool IsGvGPrepare()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) MonoSingleton<GameManager>.Instance, (UnityEngine.Object) null))
        return false;
      List<GvGPeriodParam> mGvGperiodParam = MonoSingleton<GameManager>.Instance.mGvGPeriodParam;
      if (mGvGperiodParam == null)
      {
        DebugUtility.Log("<color=yellow>QuestParam/mGvGPeriodParam no data!</color>");
        return false;
      }
      DateTime serverTime = TimeManager.ServerTime;
      for (int index = 0; index < mGvGperiodParam.Count; ++index)
      {
        if (mGvGperiodParam[index].PrepareAt <= serverTime && serverTime < mGvGperiodParam[index].BeginAt)
          return true;
      }
      return false;
    }
  }
}
