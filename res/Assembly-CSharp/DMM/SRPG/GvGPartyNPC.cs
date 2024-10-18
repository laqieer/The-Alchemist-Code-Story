// Decompiled with JetBrains decompiler
// Type: SRPG.GvGPartyNPC
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class GvGPartyNPC
  {
    public long UniqueID { get; private set; }

    public int HP { get; private set; }

    public bool Deserialize(JSON_GvGPartyNPC json)
    {
      if (json == null)
        return false;
      this.UniqueID = (long) json.iid;
      this.HP = json.hp;
      return true;
    }
  }
}
