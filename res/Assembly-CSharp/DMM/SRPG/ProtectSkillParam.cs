// Decompiled with JetBrains decompiler
// Type: SRPG.ProtectSkillParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  public class ProtectSkillParam
  {
    public string mIname;
    public ProtectTypes mType;
    public DamageTypes mDamageType;
    public SkillRankUpValue mValue;
    public int mRange;
    public int mHeight;
    public int mInit;
    public int mMax;
    public const int ETERNAL_PROTECT_TURN = 99;

    public string Iname => this.mIname;

    public ProtectTypes Type => this.mType;

    public DamageTypes DamageType => this.mDamageType;

    public SkillRankUpValue Value => this.mValue;

    public int Range => this.mRange;

    public int Height => this.mHeight;

    public static void Deserialize(
      ref List<ProtectSkillParam> ref_params,
      JSON_ProtectSkillParam[] json)
    {
      if (ref_params == null)
        ref_params = new List<ProtectSkillParam>();
      ref_params.Clear();
      if (json == null)
        return;
      foreach (JSON_ProtectSkillParam json1 in json)
      {
        ProtectSkillParam protectSkillParam = new ProtectSkillParam();
        protectSkillParam.Deserialize(json1);
        ref_params.Add(protectSkillParam);
      }
    }

    public void Deserialize(JSON_ProtectSkillParam json)
    {
      if (json == null)
        return;
      this.mIname = json.iname;
      this.mType = (ProtectTypes) json.type;
      this.mDamageType = (DamageTypes) json.dmg_type;
      this.mValue = (SkillRankUpValue) null;
      this.mRange = json.range;
      this.mHeight = json.height;
      if (this.mType == ProtectTypes.None || this.mType != ProtectTypes.Turn && (this.mType == ProtectTypes.Turn || this.mDamageType == DamageTypes.None))
        return;
      this.mValue = new SkillRankUpValue();
      this.Value.ini = json.ini;
      this.Value.max = json.max;
    }

    public bool IsProtectEternal(int value) => this.Type == ProtectTypes.Turn && value >= 99;
  }
}
