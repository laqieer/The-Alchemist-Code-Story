// Decompiled with JetBrains decompiler
// Type: SRPG.GuildFacilityEffectParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace SRPG
{
  public class GuildFacilityEffectParam
  {
    public int lv;
    public string[] buff_effect;
    public int member_count;
    public int sub_master;

    public bool Deserialize(JSON_GuildFacilityEffectParam json)
    {
      this.lv = json.lv;
      this.member_count = json.mem_cnt;
      this.sub_master = json.sub_mas;
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
      int num = 0;
      if (this.member_count > 0)
        ++num;
      if (this.sub_master > 0)
        ++num;
      return num;
    }
  }
}
