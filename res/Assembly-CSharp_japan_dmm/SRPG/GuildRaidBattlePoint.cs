// Decompiled with JetBrains decompiler
// Type: SRPG.GuildRaidBattlePoint
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class GuildRaidBattlePoint
  {
    public int PT { get; private set; }

    public int Max { get; private set; }

    public int AP { get; private set; }

    public int DefBP { get; private set; }

    public bool Deserialize(JSON_GuildRaidBattlePoint json)
    {
      this.PT = json.pt;
      this.Max = json.max;
      this.AP = json.ap;
      this.DefBP = json.defbp;
      return true;
    }
  }
}
