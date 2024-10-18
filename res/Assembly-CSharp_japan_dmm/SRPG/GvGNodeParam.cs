// Decompiled with JetBrains decompiler
// Type: SRPG.GvGNodeParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class GvGNodeParam : GvGMasterParam<JSON_GvGNodeParam>
  {
    public int Id { get; private set; }

    public int PeriodId { get; private set; }

    public string Name { get; private set; }

    public int Rank { get; private set; }

    public int InitPosition { get; private set; }

    public int DefenseMax { get; private set; }

    public string QuestId { get; private set; }

    public int FirstHalfPoint { get; private set; }

    public int Point { get; private set; }

    public string NodeRewardId { get; private set; }

    public int NPCPartyId { get; private set; }

    public List<int> AdjacentNode { get; private set; }

    public string ConsecutiveDebuffId { get; private set; }

    public int ConsecutiveDebuffMax { get; private set; }

    public override bool Deserialize(JSON_GvGNodeParam json)
    {
      if (json == null)
        return false;
      this.Id = json.id;
      this.PeriodId = json.period_id;
      this.Name = json.name;
      this.Rank = json.rank;
      this.InitPosition = json.init_position;
      this.DefenseMax = json.defense_max;
      this.QuestId = json.quest_id;
      this.Point = json.point;
      this.FirstHalfPoint = json.first_half_point;
      this.NodeRewardId = json.node_reward_id;
      this.NPCPartyId = json.npc_party_id;
      this.ConsecutiveDebuffId = json.consecutive_debuff_id;
      this.ConsecutiveDebuffMax = json.consecutive_debuff_max;
      this.AdjacentNode = new List<int>();
      if (json.adjacent_node != null)
      {
        for (int index = 0; index < json.adjacent_node.Length; ++index)
        {
          if (json.adjacent_node[index] != 0)
            this.AdjacentNode.Add(json.adjacent_node[index]);
        }
      }
      return true;
    }

    public GvGNPCPartyParam GetNpcParties()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) MonoSingleton<GameManager>.Instance, (UnityEngine.Object) null))
        return (GvGNPCPartyParam) null;
      List<GvGNPCPartyParam> gvGnpcPartyParam = MonoSingleton<GameManager>.Instance.mGvGNPCPartyParam;
      if (gvGnpcPartyParam != null)
        return gvGnpcPartyParam.Find((Predicate<GvGNPCPartyParam>) (p => p != null && p.Id == this.Id));
      DebugUtility.Log("<color=yellow>QuestParam/mGvGNPCPartyParam no data!</color>");
      return (GvGNPCPartyParam) null;
    }

    public GvGRewardParam GetRewardNode(string league)
    {
      return GvGNodeRewardParam.GetGvGRewardParam(this.NodeRewardId, league);
    }

    public static GvGNodeParam GetNode(int id)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) MonoSingleton<GameManager>.Instance, (UnityEngine.Object) null))
        return (GvGNodeParam) null;
      List<GvGNodeParam> mGvGnodeParam = MonoSingleton<GameManager>.Instance.mGvGNodeParam;
      if (mGvGnodeParam != null)
        return mGvGnodeParam.Find((Predicate<GvGNodeParam>) (node => node != null && node.Id == id));
      DebugUtility.Log("<color=yellow>QuestParam/mGvGNodeParam no data!</color>");
      return (GvGNodeParam) null;
    }

    public static List<GvGNodeParam> GetGvGNodeList(int periodId = 0)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) MonoSingleton<GameManager>.Instance, (UnityEngine.Object) null))
        return (List<GvGNodeParam>) null;
      if (periodId == 0)
      {
        GvGPeriodParam gvGperiod = GvGPeriodParam.GetGvGPeriod();
        if (gvGperiod == null)
          return (List<GvGNodeParam>) null;
        periodId = gvGperiod.Id;
      }
      List<GvGNodeParam> mGvGnodeParam = MonoSingleton<GameManager>.Instance.mGvGNodeParam;
      if (mGvGnodeParam != null)
        return mGvGnodeParam.FindAll((Predicate<GvGNodeParam>) (node => node != null && node.PeriodId == periodId));
      DebugUtility.Log("<color=yellow>QuestParam/mGvGNodeParam no data!</color>");
      return (List<GvGNodeParam>) null;
    }

    public static int GetLatestNodeCount()
    {
      List<GvGPeriodParam> mGvGperiodParam = MonoSingleton<GameManager>.Instance.mGvGPeriodParam;
      if (mGvGperiodParam == null)
      {
        DebugUtility.Log("<color=yellow>QuestParam/mGvGperiodParam no data!</color>");
        return 0;
      }
      int period_id = 0;
      for (int index = 0; index < mGvGperiodParam.Count; ++index)
      {
        if (period_id < index)
          period_id = index;
      }
      List<GvGNodeParam> mGvGnodeParam = MonoSingleton<GameManager>.Instance.mGvGNodeParam;
      if (mGvGnodeParam == null)
      {
        DebugUtility.Log("<color=yellow>QuestParam/mGvGNodeParam no data!</color>");
        return 0;
      }
      List<GvGNodeParam> all = mGvGnodeParam.FindAll((Predicate<GvGNodeParam>) (node => node != null && node.PeriodId == period_id));
      return all != null ? all.Count : 0;
    }
  }
}
