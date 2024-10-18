// Decompiled with JetBrains decompiler
// Type: SRPG.SkillAntiShieldParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  public class SkillAntiShieldParam
  {
    private string mIname;
    private bool mIsIgnore;
    private SkillRankUpValueShort mIgnoreRate;
    private bool mIsDestroy;
    private SkillRankUpValueShort mDestroyRate;

    public string Iname => this.mIname;

    public bool IsIgnore => this.mIsIgnore;

    public SkillRankUpValueShort IgnoreRate => this.mIgnoreRate;

    public bool IsDestroy => this.mIsDestroy;

    public SkillRankUpValueShort DestroyRate => this.mDestroyRate;

    public void Deserialize(JSON_SkillAntiShieldParam json)
    {
      if (json == null)
        return;
      this.mIname = json.iname;
      this.mIsIgnore = json.is_ignore != 0;
      this.mIgnoreRate = new SkillRankUpValueShort();
      this.mIgnoreRate.ini = json.ignore_rate_ini;
      this.mIgnoreRate.max = json.ignore_rate_max;
      this.mIsDestroy = json.is_destroy != 0;
      this.mDestroyRate = new SkillRankUpValueShort();
      this.mDestroyRate.ini = json.destroy_rate_ini;
      this.mDestroyRate.max = json.destroy_rate_max;
    }

    public static void Deserialize(
      ref List<SkillAntiShieldParam> list,
      JSON_SkillAntiShieldParam[] json)
    {
      if (json == null)
        return;
      if (list == null)
        list = new List<SkillAntiShieldParam>(json.Length);
      list.Clear();
      for (int index = 0; index < json.Length; ++index)
      {
        SkillAntiShieldParam skillAntiShieldParam = new SkillAntiShieldParam();
        skillAntiShieldParam.Deserialize(json[index]);
        list.Add(skillAntiShieldParam);
      }
    }
  }
}
