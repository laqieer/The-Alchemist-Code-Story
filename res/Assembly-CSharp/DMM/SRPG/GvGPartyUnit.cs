// Decompiled with JetBrains decompiler
// Type: SRPG.GvGPartyUnit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class GvGPartyUnit : UnitData
  {
    public int HP { get; private set; }

    public bool Deserialize(JSON_GvGPartyUnit json)
    {
      if (json == null || json.iid == 0L)
        return false;
      this.Deserialize((Json_Unit) json);
      this.IsNotFindUniqueID = true;
      this.HP = json.hp;
      return true;
    }

    public void SetHP(int hp) => this.HP = hp;
  }
}
