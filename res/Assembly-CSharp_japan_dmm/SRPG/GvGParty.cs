// Decompiled with JetBrains decompiler
// Type: SRPG.GvGParty
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class GvGParty
  {
    public int Id { get; private set; }

    public int WinNum { get; private set; }

    public int BeatNum { get; private set; }

    public GuildMemberData.eRole RoleId { get; private set; }

    public string PlayerName { get; private set; }

    public List<GvGPartyUnit> Units { get; private set; }

    public bool Deserialize(JSON_GvGParty json)
    {
      if (json == null)
        return false;
      this.Id = json.id;
      this.WinNum = json.win_num;
      this.BeatNum = json.beat_num;
      this.Units = new List<GvGPartyUnit>();
      if (json.is_npc == 0)
      {
        this.RoleId = (GuildMemberData.eRole) json.role;
        this.PlayerName = json.name;
        if (json.units == null)
        {
          DebugUtility.LogError("パーティユニットの情報がない");
          return false;
        }
        for (int index = 0; index < json.units.Length; ++index)
        {
          GvGPartyUnit gvGpartyUnit = new GvGPartyUnit();
          if (gvGpartyUnit.Deserialize(json.units[index]))
            this.Units.Add(gvGpartyUnit);
        }
      }
      else
      {
        this.RoleId = GuildMemberData.eRole.MEMBER;
        this.PlayerName = "NPC";
        if (json.npc_units == null)
        {
          DebugUtility.LogError("NPCパーティユニットの情報がない");
          return false;
        }
        for (int index = 0; index < json.npc_units.Length; ++index)
        {
          if (json.npc_units[index] != null)
          {
            GvGPartyUnit npcUnitData = GvGNPCUnitParam.CreateNPCUnitData(json.npc_units[index].iid);
            if (npcUnitData == null)
            {
              DebugUtility.LogError("存在しないNPCユニットが設定されている");
              return false;
            }
            npcUnitData.SetHP(json.npc_units[index].hp);
            this.Units.Add(npcUnitData);
          }
        }
      }
      return true;
    }
  }
}
