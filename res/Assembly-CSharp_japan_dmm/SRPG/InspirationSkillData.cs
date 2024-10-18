// Decompiled with JetBrains decompiler
// Type: SRPG.InspirationSkillData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  public class InspirationSkillData
  {
    private OLong mUniqueID;
    private OInt mSlot;
    private InspSkillParam mInspSkillParam;
    private OInt mLv;
    private OBool mIsSet;
    private AbilityData mAbilityData;

    public OLong UniqueID => this.mUniqueID;

    public OInt Slot => this.mSlot;

    public InspSkillParam InspSkillParam => this.mInspSkillParam;

    public OInt Lv => this.mLv;

    public OBool IsSet => this.mIsSet;

    public AbilityData AbilityData => this.mAbilityData;

    public bool Deserialize(Json_InspirationSkill json)
    {
      if (json == null)
        return false;
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      this.mUniqueID = (OLong) json.iid;
      this.mSlot = (OInt) json.slot;
      this.mInspSkillParam = instanceDirect.MasterParam.GetInspirationSkillParam(json.iname);
      if (this.mInspSkillParam == null)
        return false;
      this.mLv = (OInt) json.level;
      this.mIsSet = (OBool) (json.is_set == 1);
      if (this.mInspSkillParam.Ability == null)
        return false;
      int lvCap = this.mInspSkillParam.LvCap;
      this.mAbilityData = new AbilityData();
      this.mAbilityData.Setup((UnitData) null, 0L, this.mInspSkillParam.Ability.iname, this.LevelToAbilityExp((int) this.Lv), lvCap);
      this.mAbilityData.IsNoneCategory = true;
      return true;
    }

    private int LevelToAbilityExp(int lv) => lv <= 0 ? 0 : lv - 1;

    public Json_InspirationSkill ToJson()
    {
      return new Json_InspirationSkill()
      {
        iid = (long) this.mUniqueID,
        slot = (int) this.mSlot,
        iname = this.mInspSkillParam.Iname,
        level = (int) this.mLv,
        is_set = !(bool) this.mIsSet ? 0 : 1
      };
    }

    public bool IsIncludeSkill(SkillData skill)
    {
      return this.AbilityData != null && this.AbilityData.Skills != null && this.AbilityData.Skills.Count > 0 && this.AbilityData.Skills.FindIndex((Predicate<SkillData>) (data => data.SkillID == skill.SkillID)) >= 0;
    }
  }
}
