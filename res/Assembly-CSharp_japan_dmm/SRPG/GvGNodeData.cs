// Decompiled with JetBrains decompiler
// Type: SRPG.GvGNodeData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class GvGNodeData
  {
    public int UniqueId { get; private set; }

    public int NodeId => this.NodeParam.Id;

    public GvGNodeParam NodeParam { get; private set; }

    public GvGNodeState State { get; private set; }

    public int GuildId { get; private set; }

    public int DeclaredGuildId { get; private set; }

    public DateTime CaptureTime { get; private set; }

    public DateTime AttackEnableTime { get; private set; }

    public int DefensePartyNum { get; private set; }

    public bool CanDeclare
    {
      get => this.State == GvGNodeState.OccupyOther || this.State == GvGNodeState.Npc;
    }

    public bool IsAttackWait => TimeManager.ServerTime < this.AttackEnableTime;

    public bool Deserialize(JSON_GvGNodeData json)
    {
      if (json == null)
        return false;
      this.UniqueId = json.iid;
      this.NodeParam = GvGNodeParam.GetNode(json.id);
      if (this.NodeParam == null)
        return false;
      this.State = (GvGNodeState) json.state;
      this.GuildId = json.gid;
      this.DeclaredGuildId = json.declared_gid;
      if (json.capture_time > 0L)
        this.CaptureTime = TimeManager.FromUnixTime(json.capture_time);
      if (json.attack_enable_time > 0L)
        this.AttackEnableTime = TimeManager.FromUnixTime(json.attack_enable_time);
      this.DefensePartyNum = json.defense_party_num;
      return true;
    }
  }
}
