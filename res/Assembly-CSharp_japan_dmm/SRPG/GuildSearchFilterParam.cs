// Decompiled with JetBrains decompiler
// Type: SRPG.GuildSearchFilterParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class GuildSearchFilterParam
  {
    public int id;
    public eGuildSearchFilterTypes filter_type;
    public GuildSearchFilterConditionParam[] condtions;

    public void Deserialize(JSON_GuildSearchFilterParam json)
    {
      if (json == null)
        return;
      this.id = json.id;
      this.filter_type = (eGuildSearchFilterTypes) json.filter_type;
      if (json.cnds == null)
        return;
      this.condtions = new GuildSearchFilterConditionParam[json.cnds.Length];
      for (int index = 0; index < json.cnds.Length; ++index)
      {
        GuildSearchFilterConditionParam filterConditionParam = new GuildSearchFilterConditionParam();
        filterConditionParam.Deserialize(json.cnds[index]);
        this.condtions[index] = filterConditionParam;
      }
    }

    public static void Deserialize(
      ref GuildSearchFilterParam[] param,
      JSON_GuildSearchFilterParam[] json)
    {
      if (json == null)
        return;
      param = new GuildSearchFilterParam[json.Length];
      for (int index = 0; index < json.Length; ++index)
      {
        GuildSearchFilterParam searchFilterParam = new GuildSearchFilterParam();
        searchFilterParam.Deserialize(json[index]);
        param[index] = searchFilterParam;
      }
    }

    public GuildSearchFilterConditionParam GetConditionsParam(int sval)
    {
      GuildSearchFilterConditionParam conditionsParam = (GuildSearchFilterConditionParam) null;
      if (this.condtions != null)
      {
        for (int index = 0; index < this.condtions.Length; ++index)
        {
          if (this.condtions[index].sval == sval)
          {
            conditionsParam = this.condtions[index];
            break;
          }
        }
      }
      return conditionsParam;
    }
  }
}
