// Decompiled with JetBrains decompiler
// Type: SRPG.TrophyData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;

#nullable disable
namespace SRPG
{
  public class TrophyData : TrophyBase
  {
    public void OverwriteTrophyProgress(JSON_TrophyProgress[] trophyProgressList)
    {
      if (trophyProgressList == null)
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      for (int index = 0; index < trophyProgressList.Length; ++index)
      {
        JSON_TrophyProgress trophyProgress = trophyProgressList[index];
        if (trophyProgress != null)
        {
          TrophyParam trophy = instance.MasterParam.GetTrophy(trophyProgress.iname);
          if (trophy == null)
          {
            DebugUtility.LogWarning("存在しないミッション:" + trophyProgress.iname);
          }
          else
          {
            TrophyState trophyCounter = this.GetTrophyCounter(trophy);
            bool flag = trophyCounter.IsEnded || trophyCounter.IsCompleted;
            trophyCounter.Setup(trophy, trophyProgress);
            if (!flag && trophyCounter.IsCompleted)
              NotifyList.PushTrophy(trophy);
          }
        }
      }
    }

    public void RegistTrophyStateDictByProgExtra(JSON_TrophyProgress[] _prog)
    {
      if (_prog == null || _prog.Length <= 0)
        return;
      for (int index = 0; index < _prog.Length; ++index)
        this.RegistTrophyStateDictByProgExtra(MonoSingleton<GameManager>.Instance.MasterParam.GetTrophy(_prog[index].iname), _prog[index]);
    }

    public void RegistTrophyStateDictByProgExtra(TrophyParam _trophy, JSON_TrophyProgress _prog)
    {
      if (!this.mTrophyStatesInameDict.ContainsKey(_trophy.iname))
        this.AddTrophyStateDict(this.CreateTrophyState(_trophy));
      TrophyState trophyState = this.mTrophyStatesInameDict[_trophy.iname].Find((Predicate<TrophyState>) (x => x.iname == _trophy.iname));
      if (trophyState == null || trophyState.IsCompleted)
        return;
      for (int index = 0; index < _trophy.Objectives.Length && index < _prog.pts.Length && index < trophyState.Count.Length; ++index)
        trophyState.Count[index] = Math.Min(_prog.pts[index], _trophy.Objectives[index].ival);
      if (trophyState.IsCompleted && trophyState.Param.DispType == TrophyDispType.Award)
        NotifyList.PushAward(trophyState.Param);
      trophyState.StartYMD = _prog.ymd;
      trophyState.IsEnded = _prog.rewarded_at != 0;
      trophyState.IsDirty = true;
    }
  }
}
