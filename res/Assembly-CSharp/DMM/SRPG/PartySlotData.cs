// Decompiled with JetBrains decompiler
// Type: SRPG.PartySlotData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class PartySlotData
  {
    public PartySlotType Type;
    public PartySlotIndex Index;
    public string UnitName;
    public bool IsSettable;

    public PartySlotData(
      PartySlotType type,
      string unitName,
      PartySlotIndex index,
      bool isSettable = false)
    {
      this.Type = type;
      this.Index = index;
      this.UnitName = unitName;
      this.IsSettable = isSettable;
    }

    public override string ToString()
    {
      string str = nameof (PartySlotData) + "\n" + "    枠 : ";
      switch (this.Type)
      {
        case PartySlotType.Free:
          str += "自由";
          break;
        case PartySlotType.Locked:
          str += "出撃不可";
          break;
        case PartySlotType.Forced:
          str += "強制出撃";
          break;
        case PartySlotType.ForcedHero:
          str += "強制出撃(主人公)";
          break;
        case PartySlotType.Npc:
          str += "NPC";
          break;
        case PartySlotType.NpcHero:
          str += "NPC(主人公)";
          break;
      }
      return str + "\n" + "    要素 : " + Enum.GetName(typeof (PartySlotIndex), (object) this.Index) + "\n" + "    ユニット名 : " + this.UnitName;
    }
  }
}
