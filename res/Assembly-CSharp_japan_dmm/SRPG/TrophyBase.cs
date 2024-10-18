// Decompiled with JetBrains decompiler
// Type: SRPG.TrophyBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class TrophyBase
  {
    protected List<TrophyState> mTrophyStates = new List<TrophyState>();
    protected Dictionary<string, List<TrophyState>> mTrophyStatesInameDict = new Dictionary<string, List<TrophyState>>();
    private long mMissionClearAt = -1;

    public List<TrophyState> TrophyStatesList => this.mTrophyStates;

    public void SetMissionClearAt(long unixTimeStamp) => this.mMissionClearAt = unixTimeStamp;

    public void ResetMissionClearAt() => this.mMissionClearAt = -1L;

    public DateTime GetMissionClearAt()
    {
      return this.mMissionClearAt < 0L ? TimeManager.ServerTime : TimeManager.FromUnixTime(this.mMissionClearAt);
    }

    public bool DictTryGetValue(string iname, out List<TrophyState> tState)
    {
      return this.mTrophyStatesInameDict.TryGetValue(iname, out tState);
    }

    public void ClearTrophies()
    {
      this.mTrophyStates.Clear();
      this.mTrophyStatesInameDict.Clear();
    }

    public void DeleteTrophies(JSON_TrophyProgress[] trophies)
    {
      if (trophies == null)
        return;
      for (int i = 0; i < trophies.Length; ++i)
        this.mTrophyStates.RemoveAll((Predicate<TrophyState>) (state => state.iname == trophies[i].iname));
      for (int index = 0; index < trophies.Length; ++index)
      {
        if (this.mTrophyStatesInameDict.ContainsKey(trophies[index].iname))
          this.mTrophyStatesInameDict.Remove(trophies[index].iname);
      }
    }

    public bool IsTrophyDirty()
    {
      for (int index = this.mTrophyStates.Count - 1; index >= 0; --index)
      {
        if (this.mTrophyStates[index].IsDirty)
          return true;
      }
      return false;
    }

    private void UpdateTrophyState(TrophyState st, int currentYMD)
    {
      if (!st.Param.IsDaily)
        return;
      int startYmd = st.StartYMD;
      int num = Math.Abs(currentYMD.FromYMD().Subtract(startYmd.FromYMD()).Days);
      if (st.IsEnded)
      {
        if (num < 1)
          return;
        this.ClearTrophyCounter(st);
      }
      else if (!st.IsCompleted)
      {
        if (num < 1)
          return;
        this.ClearTrophyCounter(st);
      }
      else if (num >= 2)
      {
        this.ClearTrophyCounter(st);
      }
      else
      {
        if (num < 1)
          return;
        for (int index = 0; index < st.Param.Objectives.Length; ++index)
        {
          if (st.Param.Objectives[index].type == TrophyConditionTypes.stamina)
          {
            this.ClearTrophyCounter(st);
            break;
          }
        }
      }
    }

    public int GetCompletedTrophiesCount()
    {
      List<TrophyParam> trophyParamList = new List<TrophyParam>(this.mTrophyStates.Count);
      for (int index = this.mTrophyStates.Count - 1; index >= 0; --index)
      {
        TrophyState mTrophyState = this.mTrophyStates[index];
        if (!mTrophyState.IsEnded && mTrophyState.IsCompleted)
          trophyParamList.Add(mTrophyState.Param);
      }
      return trophyParamList.Count;
    }

    public void MarkTrophiesEnded(TrophyParam[] trophies)
    {
      for (int index = 0; index < trophies.Length; ++index)
      {
        TrophyState trophyCounter = this.GetTrophyCounter(trophies[index], true);
        trophyCounter.IsEnded = true;
        trophyCounter.IsDirty = true;
        trophyCounter.RewardedAt = TimeManager.ServerTime;
      }
    }

    private void ClearTrophyCounter(TrophyState _st)
    {
      if (this.mTrophyStates.Contains(_st))
        this.mTrophyStates.Remove(_st);
      if (!this.mTrophyStatesInameDict.ContainsKey(_st.iname))
        return;
      this.mTrophyStatesInameDict[_st.iname].Remove(_st);
      if (this.mTrophyStatesInameDict[_st.iname].Count > 0)
        return;
      this.mTrophyStatesInameDict.Remove(_st.iname);
    }

    public void UpdateTrophyStates()
    {
      int ymd = TimeManager.ServerTime.ToYMD();
      List<TrophyState> mTrophyStates = this.mTrophyStates;
      for (int index = 0; index < mTrophyStates.Count; ++index)
      {
        if (mTrophyStates[index] != null)
          this.UpdateTrophyState(mTrophyStates[index], ymd);
      }
    }

    public TrophyState RegistTrophyStateDictByProg(TrophyParam _trophy, JSON_TrophyProgress _prog)
    {
      TrophyState trophyState = this.CreateTrophyState(_trophy);
      trophyState.Setup(_trophy, _prog);
      this.AddTrophyStateDict(trophyState);
      return trophyState;
    }

    public void CreateInheritingExtraTrophy(Dictionary<int, List<JSON_TrophyProgress>> progs)
    {
      TrophyParam[] trophies = MonoSingleton<GameManager>.Instance.MasterParam.Trophies;
      if (trophies == null)
        return;
      for (int index1 = 0; index1 < trophies.Length; ++index1)
      {
        TrophyParam param = trophies[index1];
        if (trophies[index1].Objectives[0].type.IsExtraClear())
        {
          int type = (int) trophies[index1].Objectives[0].type;
          if (progs.ContainsKey(type))
          {
            List<JSON_TrophyProgress> prog = progs[type];
            if (prog.Find((Predicate<JSON_TrophyProgress>) (x => x.iname == param.iname)) == null)
            {
              int num = 0;
              for (int index2 = 0; index2 < prog.Count; ++index2)
              {
                if (num < prog[index2].pts[0])
                  num = prog[index2].pts[0];
              }
              TrophyState trophyState = this.CreateTrophyState(param);
              this.SetTrophyCounter(trophyState.Param, 0, num);
              this.AddTrophyStateDict(trophyState);
            }
          }
        }
      }
    }

    public TrophyState GetTrophyCounter(TrophyParam trophy, bool daily_old_data = false)
    {
      List<TrophyState> trophyStateList;
      if (this.mTrophyStatesInameDict.TryGetValue(trophy.iname, out trophyStateList))
      {
        if (!trophy.IsDaily || daily_old_data)
          return trophyStateList[0];
        for (int index = 0; index < trophyStateList.Count; ++index)
        {
          if (trophyStateList[index].StartYMD == TimeManager.ServerTime.ToYMD())
            return trophyStateList[index];
        }
      }
      TrophyState trophyState = this.CreateTrophyState(trophy);
      this.AddTrophyStateDict(trophyState);
      return trophyState;
    }

    protected TrophyState CreateTrophyState(TrophyParam _trophy)
    {
      return new TrophyState()
      {
        iname = _trophy.iname,
        StartYMD = TimeManager.ServerTime.ToYMD(),
        Count = new int[_trophy.Objectives.Length],
        IsDirty = false,
        Param = _trophy
      };
    }

    protected void AddTrophyStateDict(TrophyState _state)
    {
      this.mTrophyStates.Add(_state);
      if (!this.mTrophyStatesInameDict.ContainsKey(_state.iname))
        this.mTrophyStatesInameDict.Add(_state.iname, new List<TrophyState>());
      this.mTrophyStatesInameDict[_state.iname].Add(_state);
      this.mTrophyStatesInameDict[_state.iname].Sort((Comparison<TrophyState>) ((a, b) => a.StartYMD - b.StartYMD));
    }

    private bool IsMakeTrophyPlate(TrophyParam trophy, TrophyState st, bool is_achievement)
    {
      return !trophy.IsInvisibleVip() && !trophy.IsInvisibleCard() && !trophy.IsInvisibleStamina() && (trophy.RequiredTrophies == null || TrophyParam.CheckRequiredTrophies(MonoSingleton<GameManager>.Instance, trophy, true)) && trophy.IsAvailablePeriod(TimeManager.ServerTime, is_achievement);
    }

    public void DailyAllCompleteCheck()
    {
      TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.dailyall);
      for (int index = trophiesOfType.Length - 1; index >= 0; --index)
      {
        TrophyObjective trophyObjective = trophiesOfType[index];
        if (this.IsDailyAllComplete())
          this.AddTrophyCounter(trophyObjective, 1);
      }
    }

    public bool IsDailyAllComplete()
    {
      TrophyParam[] trophies = MonoSingleton<GameManager>.Instance.Trophies;
      if (trophies == null || trophies.Length <= 0)
        return true;
      TrophyState[] trophyStateArray = new TrophyState[trophies.Length];
      for (int index = 0; index < trophies.Length; ++index)
        trophyStateArray[index] = !trophies[index].IsChallengeMission ? this.GetTrophyCounter(trophies[index]) : (TrophyState) null;
      for (int index1 = 0; index1 < trophies.Length; ++index1)
      {
        TrophyState st = trophyStateArray[index1];
        if (st != null && !st.IsCompleted)
        {
          TrophyParam trophy = trophies[index1];
          bool flag = false;
          for (int index2 = 0; index2 < trophy.Objectives.Length; ++index2)
          {
            if (trophy.Objectives[index2].type == TrophyConditionTypes.dailyall)
            {
              flag = true;
              break;
            }
          }
          if (!flag && trophy.DispType != TrophyDispType.Award && trophy.DispType != TrophyDispType.Hide && trophy.IsDaily && this.IsMakeTrophyPlate(trophy, st, false))
            return false;
        }
      }
      return true;
    }

    private bool CheckTrophyCount(
      TrophyParam trophyParam,
      int countIndex,
      int value,
      ref TrophyState state)
    {
      if (countIndex < 0 || value <= 0 || trophyParam == null || !trophyParam.IsAvailablePeriod(this.GetMissionClearAt(), false) || trophyParam.RequiredTrophies != null && !TrophyParam.CheckRequiredTrophies(MonoSingleton<GameManager>.Instance, trophyParam, trophyParam.IsChallengeMission))
        return false;
      state = this.GetTrophyCounter(trophyParam);
      if (state.IsEnded)
        return false;
      if (state.Count.Length <= countIndex)
        Array.Resize<int>(ref state.Count, countIndex + 1);
      return !state.IsCompleted;
    }

    private bool CheckDailyMissionDayChange(TrophyState state, int countIndex)
    {
      int ymd = this.GetMissionClearAt().ToYMD();
      return !state.Param.IsDaily || ymd <= state.StartYMD || state.IsCompleted;
    }

    public void AddTrophyCounter(TrophyObjective obj, int value)
    {
      this.AddTrophyCounter(obj.Param, obj.index, value);
    }

    public void AddTrophyCounter(TrophyParam trophyParam, int countIndex, int value)
    {
      if (!this.AddTrophyCounterExec(trophyParam, countIndex, value))
        return;
      this.DailyAllCompleteCheck();
    }

    private bool AddTrophyCounterExec(TrophyParam trophyParam, int countIndex, int value)
    {
      TrophyState state = (TrophyState) null;
      if (!this.CheckTrophyCount(trophyParam, countIndex, value, ref state))
        return false;
      int num = state.Count[countIndex];
      state.Count[countIndex] += value;
      if (!this.CheckDailyMissionDayChange(state, countIndex))
      {
        state.Count[countIndex] = num;
        return false;
      }
      state.IsDirty = true;
      MonoSingleton<GameManager>.Instance.update_trophy_interval.SetSyncNow();
      return state.IsCompleted;
    }

    public void SetTrophyCounter(TrophyObjective obj, int value)
    {
      this.SetTrophyCounter(obj.Param, obj.index, value);
    }

    private void SetTrophyCounter(TrophyParam trophyParam, int countIndex, int value)
    {
      if (!this.SetTrophyCounterExec(trophyParam, countIndex, value))
        return;
      this.DailyAllCompleteCheck();
    }

    private bool SetTrophyCounterExec(TrophyParam trophyParam, int countIndex, int value)
    {
      TrophyState state = (TrophyState) null;
      if (!this.CheckTrophyCount(trophyParam, countIndex, value, ref state) || state.Count[countIndex] == value)
        return false;
      int num = state.Count[countIndex];
      state.Count[countIndex] = value;
      if (!this.CheckDailyMissionDayChange(state, countIndex))
      {
        state.Count[countIndex] = num;
        return false;
      }
      state.IsDirty = true;
      MonoSingleton<GameManager>.Instance.update_trophy_interval.SetSyncNow();
      return state.IsCompleted;
    }
  }
}
