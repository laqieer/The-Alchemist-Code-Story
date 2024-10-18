// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_GvGNodeParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  [Serializable]
  public class JSON_GvGNodeParam : JSON_GvGMasterParam
  {
    public int id;
    public int period_id;
    public string name;
    public int rank;
    public int init_position;
    public int defense_max;
    public string quest_id;
    public int first_half_point;
    public int point;
    public string node_reward_id;
    public int npc_party_id;
    public string consecutive_debuff_id;
    public int consecutive_debuff_max;
    public int[] adjacent_node;
  }
}
