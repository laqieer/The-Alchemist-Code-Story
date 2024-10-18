// Decompiled with JetBrains decompiler
// Type: SRPG.InspirationSkillData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;

namespace SRPG
{
  public class InspirationSkillData
  {
    private OLong mUniqueID;
    private OInt mSlot;
    private InspSkillParam mInspSkillParam;
    private OInt mLv;
    private OBool mIsSet;
    private AbilityData mAbilityData;

    public OLong UniqueID
    {
      get
      {
        return this.mUniqueID;
      }
    }

    public OInt Slot
    {
      get
      {
        return this.mSlot;
      }
    }

    public InspSkillParam InspSkillParam
    {
      get
      {
        return this.mInspSkillParam;
      }
    }

    public OInt Lv
    {
      get
      {
        return this.mLv;
      }
    }

    public OBool IsSet
    {
      get
      {
        return this.mIsSet;
      }
    }

    public AbilityData AbilityData
    {
      get
      {
        return this.mAbilityData;
      }
    }

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

    private int LevelToAbilityExp(int lv)
    {
      if (lv <= 0)
        return 0;
      return lv - 1;
    }

    public Json_InspirationSkill ToJson()
    {
      return new Json_InspirationSkill() { iid = (long) this.mUniqueID, slot = (int) this.mSlot, iname = this.mInspSkillParam.Iname, level = (int) this.mLv, is_set = !(bool) this.mIsSet ? 0 : 1 };
    }

    public bool IsIncludeSkill(SkillData skill)
    {
      if (this.AbilityData == null || this.AbilityData.Skills == null || this.AbilityData.Skills.Count <= 0)
        return false;
      return this.AbilityData.Skills.FindIndex((Predicate<SkillData>) (data => data.SkillID == skill.SkillID)) >= 0;
    }
  }
}
