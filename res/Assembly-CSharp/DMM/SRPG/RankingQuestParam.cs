// Decompiled with JetBrains decompiler
// Type: SRPG.RankingQuestParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;

#nullable disable
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

    public static RankingQuestParam FindRankingQuestParam(
      string targetQuestID,
      int scheduleID,
      RankingQuestType type)
    {
      RankingQuestParam rankingQuestParam = (RankingQuestParam) null;
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      return UnityEngine.Object.op_Equality((UnityEngine.Object) instanceDirect, (UnityEngine.Object) null) || instanceDirect.RankingQuestParams == null ? rankingQuestParam : instanceDirect.RankingQuestParams.Find((Predicate<RankingQuestParam>) (param => param.schedule_id == scheduleID && param.type == type && param.iname == targetQuestID));
    }
  }
}
