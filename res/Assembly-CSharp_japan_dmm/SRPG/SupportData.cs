﻿// Decompiled with JetBrains decompiler
// Type: SRPG.SupportData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;

#nullable disable
namespace SRPG
{
  public class SupportData
  {
    public UnitData Unit;
    public string FUID;
    public string PlayerName;
    public int PlayerLevel;
    public string UnitID;
    public int UnitLevel;
    public int UnitRarity;
    public string JobID;
    public int LeaderSkillLevel;
    public int Cost;
    public int mIsFriend;

    public UnitParam UnitParam
    {
      get
      {
        return !string.IsNullOrEmpty(this.UnitID) ? MonoSingleton<GameManager>.Instance.GetUnitParam(this.UnitID) : (UnitParam) null;
      }
    }

    public SkillParam LeaderSkill
    {
      get
      {
        if (this.Unit != null)
        {
          SkillData leaderSkill = this.Unit.LeaderSkill;
          if (leaderSkill != null)
            return leaderSkill.SkillParam;
        }
        return (SkillParam) null;
      }
    }

    public string UnitName => this.UnitParam.name;

    public EElement UnitElement => this.Unit.Element;

    public string IconPath
    {
      get
      {
        UnitParam unitParam = this.UnitParam;
        return unitParam == null ? (string) null : AssetPath.UnitSkinIconSmall(unitParam, this.Unit.GetSelectedSkin(), this.Unit.CurrentJob.JobID);
      }
    }

    public bool IsFriend() => this.mIsFriend == 1;

    public int GetCost() => this.Cost;

    public void Deserialize(Json_Support json)
    {
      this.FUID = json.fuid;
      this.PlayerName = json.name;
      this.PlayerLevel = json.lv;
      this.Cost = json.cost;
      if (json.unit != null)
      {
        Json_Unit unit = json.unit;
        this.UnitID = unit.iname;
        this.UnitLevel = unit.lv;
        this.UnitRarity = unit.rare;
        if (unit.select != null)
        {
          this.JobID = (string) null;
          for (int index = 0; index < unit.jobs.Length; ++index)
          {
            if (unit.jobs[index].iid == unit.select.job)
            {
              this.JobID = unit.jobs[index].iname;
              break;
            }
          }
        }
        this.LeaderSkillLevel = UnitParam.GetLeaderSkillLevel(this.UnitRarity, unit.plus);
        UnitData unitData = new UnitData();
        unitData.Deserialize(unit);
        this.Unit = unitData;
      }
      this.mIsFriend = json.isFriend;
    }
  }
}
