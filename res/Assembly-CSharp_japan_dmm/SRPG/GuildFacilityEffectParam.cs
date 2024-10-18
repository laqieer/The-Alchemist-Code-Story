// Decompiled with JetBrains decompiler
// Type: SRPG.GuildFacilityEffectParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class GuildFacilityEffectParam
  {
    public int lv;
    public string[] buff_effect;
    public int member_count;
    public int sub_master;
    public int shop_count;

    public bool Deserialize(JSON_GuildFacilityEffectParam json)
    {
      this.lv = json.lv;
      this.member_count = json.mem_cnt;
      this.sub_master = json.sub_mas;
      this.shop_count = json.shop_cnt;
      this.buff_effect = (string[]) null;
      if (json.buff != null && json.buff.Length > 0)
      {
        this.buff_effect = new string[json.buff.Length];
        for (int index = 0; index < json.buff.Length; ++index)
          this.buff_effect[index] = json.buff[index];
      }
      return true;
    }

    public int GetEffectCount()
    {
      int effectCount = 0;
      if (this.member_count > 0)
        ++effectCount;
      if (this.sub_master > 0)
        ++effectCount;
      if (this.shop_count > 0)
        ++effectCount;
      return effectCount;
    }
  }
}
