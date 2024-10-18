// Decompiled with JetBrains decompiler
// Type: SRPG.GuildSearchFilterConditionParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class GuildSearchFilterConditionParam
  {
    public string name;
    public int sval;
    public int sval_min;
    public int sval_max;

    public void Deserialize(JSON_GuildSearchFilterConditionParam json)
    {
      if (json == null)
        return;
      this.name = json.name;
      this.sval = json.sval;
      this.sval_min = json.sval_min;
      this.sval_max = json.sval_max;
    }
  }
}
