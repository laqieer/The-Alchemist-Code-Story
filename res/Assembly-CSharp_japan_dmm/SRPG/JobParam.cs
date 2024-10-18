// Decompiled with JetBrains decompiler
// Type: SRPG.JobParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using MessagePack;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  public class JobParam
  {
    public static readonly int MAX_JOB_RANK = 11;
    public string iname;
    public string name;
    public string expr;
    public string model;
    public string ac2d;
    public string modelp;
    public string pet;
    public string buki;
    public string origin;
    public JobTypes type;
    public RoleTypes role;
    public OInt mov;
    public OInt jmp;
    public string wepmdl;
    public string[] atkskill = new string[7];
    public string artifact;
    public string ai;
    public string master;
    public string fixed_ability;
    public string MapEffectAbility;
    public bool IsMapEffectRevReso;
    public string DescCharacteristic;
    public string DescOther;
    public StatusParam status = new StatusParam();
    public OInt avoid = (OInt) 0;
    public OInt inimp = (OInt) 0;
    public JobRankParam[] ranks = new JobRankParam[JobParam.MAX_JOB_RANK + 1];
    public string unit_image;
    private ArtifactParam m_DefaultArtifact;
    public eMovType MovType;
    public string[] tags;
    public string buff;
    private BitArray mFlags = new BitArray(2);

    public ArtifactParam DefaultArtifact => this.m_DefaultArtifact;

    public bool IsRiding => this.mFlags.Get(0);

    public bool IsFlyingPass => this.mFlags.Get(1);

    public bool Deserialize(JSON_JobParam json, MasterParam master_param)
    {
      if (json == null)
        return false;
      this.iname = json.iname;
      this.name = json.name;
      this.expr = json.expr;
      this.model = json.mdl;
      this.ac2d = json.ac2d;
      this.modelp = json.mdlp;
      this.pet = json.pet;
      this.buki = json.buki;
      this.origin = json.origin;
      this.type = (JobTypes) json.type;
      this.role = (RoleTypes) json.role;
      this.wepmdl = json.wepmdl;
      this.mov = (OInt) json.jmov;
      this.jmp = (OInt) json.jjmp;
      this.atkskill[0] = string.IsNullOrEmpty(json.atkskl) ? string.Empty : json.atkskl;
      this.atkskill[1] = string.IsNullOrEmpty(json.atkfi) ? string.Empty : json.atkfi;
      this.atkskill[2] = string.IsNullOrEmpty(json.atkwa) ? string.Empty : json.atkwa;
      this.atkskill[3] = string.IsNullOrEmpty(json.atkwi) ? string.Empty : json.atkwi;
      this.atkskill[4] = string.IsNullOrEmpty(json.atkth) ? string.Empty : json.atkth;
      this.atkskill[5] = string.IsNullOrEmpty(json.atksh) ? string.Empty : json.atksh;
      this.atkskill[6] = string.IsNullOrEmpty(json.atkda) ? string.Empty : json.atkda;
      this.fixed_ability = json.fixabl;
      this.artifact = json.artifact;
      this.ai = json.ai;
      this.master = json.master;
      this.MapEffectAbility = json.me_abl;
      this.IsMapEffectRevReso = json.is_me_rr != 0;
      this.DescCharacteristic = json.desc_ch;
      this.DescOther = json.desc_ot;
      this.status.hp = (OInt) json.hp;
      this.status.mp = (OShort) json.mp;
      this.status.atk = (OShort) json.atk;
      this.status.def = (OShort) json.def;
      this.status.mag = (OShort) json.mag;
      this.status.mnd = (OShort) json.mnd;
      this.status.dex = (OShort) json.dex;
      this.status.spd = (OShort) json.spd;
      this.status.cri = (OShort) json.cri;
      this.status.luk = (OShort) json.luk;
      this.avoid = (OInt) json.avoid;
      this.inimp = (OInt) json.inimp;
      Array.Clear((Array) this.ranks, 0, this.ranks.Length);
      if (json.ranks != null)
      {
        for (int index = 0; index < json.ranks.Length; ++index)
        {
          this.ranks[index] = new JobRankParam();
          if (!this.ranks[index].Deserialize(json.ranks[index]))
            return false;
        }
      }
      if (master_param != null)
        this.CreateBuffList(master_param);
      this.unit_image = json.unit_image;
      this.MovType = (eMovType) json.mov_type;
      this.mFlags.Set(0, json.is_riding != 0);
      this.mFlags.Set(1, json.no_pass == 0);
      if (json.tags != null && json.tags.Length > 0)
      {
        this.tags = new string[json.tags.Length];
        for (int index = 0; index < json.tags.Length; ++index)
          this.tags[index] = json.tags[index];
      }
      this.buff = json.buff;
      return true;
    }

    private void CreateBuffList(MasterParam master_param)
    {
      for (int index1 = 0; index1 < this.ranks.Length; ++index1)
      {
        if (this.ranks[index1] != null)
        {
          List<BuffEffect.BuffValues> list = new List<BuffEffect.BuffValues>();
          if (this.ranks[index1].equips != null || index1 != this.ranks.Length)
          {
            for (int index2 = 0; index2 < this.ranks[index1].equips.Length; ++index2)
            {
              if (!string.IsNullOrEmpty(this.ranks[index1].equips[index2]))
              {
                ItemParam itemParam = master_param.GetItemParam(this.ranks[index1].equips[index2]);
                if (itemParam != null && !string.IsNullOrEmpty(itemParam.skill))
                {
                  SkillData skillData = new SkillData();
                  skillData.Setup(itemParam.skill, 1, master: master_param);
                  skillData.BuffSkill(ESkillTiming.Passive, EElement.None, (BaseStatus) null, (BaseStatus) null, (BaseStatus) null, (BaseStatus) null, (BaseStatus) null, (BaseStatus) null, list: list);
                }
              }
            }
            if (list.Count > 0)
            {
              this.ranks[index1].buff_list = new BuffEffect.BuffValues[list.Count];
              for (int index3 = 0; index3 < list.Count; ++index3)
                this.ranks[index1].buff_list[index3] = list[index3];
            }
          }
        }
      }
    }

    public int GetJobChangeCost(int lv)
    {
      return this.ranks == null || lv < 0 || lv >= this.ranks.Length ? 0 : this.ranks[lv].JobChangeCost;
    }

    public string[] GetJobChangeItems(int lv)
    {
      return this.ranks == null || lv < 0 || lv >= this.ranks.Length ? (string[]) null : this.ranks[lv].JobChangeItems;
    }

    public int[] GetJobChangeItemNums(int lv)
    {
      return this.ranks == null || lv < 0 || lv >= this.ranks.Length ? (int[]) null : this.ranks[lv].JobChangeItemNums;
    }

    public int GetRankupCost(int lv)
    {
      return this.ranks == null || lv < 0 || lv >= this.ranks.Length ? 0 : this.ranks[lv].cost;
    }

    public string[] GetRankupItems(int lv)
    {
      return this.ranks == null || lv < 0 || lv >= this.ranks.Length ? (string[]) null : this.ranks[lv].equips;
    }

    public string GetRankupItemID(int lv, int index)
    {
      string[] rankupItems = this.GetRankupItems(lv);
      return rankupItems != null && 0 <= index && index < rankupItems.Length ? rankupItems[index] : (string) null;
    }

    public OString[] GetLearningAbilitys(int lv)
    {
      return this.ranks == null || lv < 0 || lv >= this.ranks.Length ? (OString[]) null : this.ranks[lv].learnings;
    }

    public int GetJobRankAvoidRate(int lv)
    {
      return this.ranks == null || lv < 0 || lv >= this.ranks.Length ? 0 : (int) this.avoid;
    }

    public int GetJobRankInitJewelRate(int lv)
    {
      return this.ranks == null || lv < 0 || lv >= this.ranks.Length ? 0 : (int) this.inimp;
    }

    public StatusParam GetJobRankStatus(int lv)
    {
      return this.ranks == null || lv < 0 || lv >= this.ranks.Length ? (StatusParam) null : this.status;
    }

    public BaseStatus GetJobTransfarStatus(int lv, EElement element)
    {
      if (this.ranks == null || lv < 0 || lv >= this.ranks.Length)
        return (BaseStatus) null;
      BaseStatus status = new BaseStatus();
      for (int index = 0; index < lv; ++index)
      {
        if (this.ranks[index].buff_list != null)
        {
          foreach (BuffEffect.BuffValues buff in this.ranks[index].buff_list)
          {
            bool flag = false;
            switch (buff.param_type)
            {
              case ParamTypes.UnitDefenseFire:
                flag = element != EElement.Fire;
                break;
              case ParamTypes.UnitDefenseWater:
                flag = element != EElement.Water;
                break;
              case ParamTypes.UnitDefenseWind:
                flag = element != EElement.Wind;
                break;
              case ParamTypes.UnitDefenseThunder:
                flag = element != EElement.Thunder;
                break;
              case ParamTypes.UnitDefenseShine:
                flag = element != EElement.Shine;
                break;
              case ParamTypes.UnitDefenseDark:
                flag = element != EElement.Dark;
                break;
            }
            if (!flag)
              BuffEffect.SetBuffValues(buff.param_type, buff.method_type, ref status, buff.value);
          }
        }
      }
      return status;
    }

    public static int GetJobRankCap(int unitRarity)
    {
      RarityParam rarityParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetRarityParam(unitRarity);
      return rarityParam != null ? (int) rarityParam.UnitJobLvCap : 1;
    }

    public int FindRankOfAbility(string abilityID)
    {
      if (this.ranks != null)
      {
        for (int rankOfAbility = 0; rankOfAbility < this.ranks.Length; ++rankOfAbility)
        {
          if (this.ranks[rankOfAbility].learnings != null && Array.FindIndex<OString>(this.ranks[rankOfAbility].learnings, (Predicate<OString>) (p => (string) p == abilityID)) != -1)
            return rankOfAbility;
        }
      }
      return -1;
    }

    public List<string> GetAllLearningAbilitys()
    {
      List<string> learningAbilitys1 = new List<string>();
      for (int lv = 1; lv < JobParam.MAX_JOB_RANK + 1; ++lv)
      {
        OString[] learningAbilitys2 = this.GetLearningAbilitys(lv);
        if (learningAbilitys2 != null)
        {
          for (int index = 0; index < learningAbilitys2.Length; ++index)
            learningAbilitys1.Add((string) learningAbilitys2[index]);
        }
      }
      return learningAbilitys1;
    }

    public static void UpdateCache(List<JobParam> jobParams)
    {
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instanceDirect, (UnityEngine.Object) null) || instanceDirect.MasterParam == null)
        return;
      for (int index = 0; index < jobParams.Count; ++index)
        jobParams[index].UpdateDefaultArtifactCache(instanceDirect.MasterParam);
    }

    private void UpdateDefaultArtifactCache(MasterParam master)
    {
      if (string.IsNullOrEmpty(this.artifact))
        return;
      this.m_DefaultArtifact = master.GetArtifactParam(this.artifact);
    }

    private enum eFlags
    {
      RIDING,
      FLYING_PASS,
      MAX,
    }
  }
}
