﻿// Decompiled with JetBrains decompiler
// Type: SRPG.RankingQuestParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;

namespace SRPG
{
  public class RankingQuestParam
  {
    public int schedule_id;
    public RankingQuestType type;
    public string iname;
    public int reward_id;
    public RankingQuestRewardParam rewardParam;
    public RankingQuestScheduleParam scheduleParam;

    public bool Deserialize(JSON_RankingQuestParam json)
    {
      this.schedule_id = json.schedule_id;
      if (Enum.GetNames(typeof (RankingQuestType)).Length > json.type)
        this.type = (RankingQuestType) json.type;
      else
        DebugUtility.LogError("定義されていない列挙値が指定されようとしました");
      this.iname = json.iname;
      this.reward_id = json.reward_id;
      return true;
    }

    public static RankingQuestParam FindRankingQuestParam(string targetQuestID, int scheduleID, RankingQuestType type)
    {
      RankingQuestParam rankingQuestParam = (RankingQuestParam) null;
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if ((UnityEngine.Object) instanceDirect == (UnityEngine.Object) null || instanceDirect.RankingQuestParams == null)
        return rankingQuestParam;
      return instanceDirect.RankingQuestParams.Find((Predicate<RankingQuestParam>) (param =>
      {
        if (param.schedule_id == scheduleID && param.type == type)
          return param.iname == targetQuestID;
        return false;
      }));
    }
  }
}