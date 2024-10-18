// Decompiled with JetBrains decompiler
// Type: SRPG.GuildFacilityData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class GuildFacilityData
  {
    private long mGid;
    private string mIname;
    private long mExp;
    private int mLevel;
    private int mInvestPoint;
    private GuildFacilityParam mParam;

    public long Gid => this.mGid;

    public string Iname => this.mIname;

    public long Exp => this.mExp;

    public int Level => this.mLevel;

    public int InvestPoint => this.mInvestPoint;

    public GuildFacilityParam Param => this.mParam;

    public bool Deserialize(JSON_GuildFacilityData json)
    {
      this.mGid = json.gid;
      this.mIname = json.facility_iname;
      this.mExp = json.exp;
      this.mLevel = json.level;
      this.mInvestPoint = json.invest_point;
      this.mParam = MonoSingleton<GameManager>.Instance.MasterParam.GetGuildFacility(this.mIname);
      return true;
    }

    public GuildFacilityEffectParam GetEffect() => this.mParam.GetEffect(this.mLevel);

    public static long GetNeedExp(int from_lv, int to_lv, GuildFacilityParam.eFacilityType type)
    {
      GuildFacilityLvParam[] facilityLevelTable = MonoSingleton<GameManager>.Instance.MasterParam.GetGuildFacilityLevelTable();
      long needExp = 0;
      switch (type)
      {
        case GuildFacilityParam.eFacilityType.BASE_CAMP:
          for (int index = from_lv; index < facilityLevelTable.Length && index < to_lv; ++index)
            needExp += (long) facilityLevelTable[index].base_camp;
          break;
        case GuildFacilityParam.eFacilityType.GUILD_SHOP:
          for (int index = from_lv; index < facilityLevelTable.Length && index < to_lv; ++index)
            needExp += facilityLevelTable[index].guild_shop;
          break;
      }
      return needExp;
    }

    public static int GetLevelMax(GuildFacilityParam.eFacilityType type)
    {
      GuildFacilityLvParam[] facilityLevelTable = MonoSingleton<GameManager>.Instance.MasterParam.GetGuildFacilityLevelTable();
      int levelMax = 0;
      switch (type)
      {
        case GuildFacilityParam.eFacilityType.BASE_CAMP:
          for (int index = 0; index < facilityLevelTable.Length && (index == 0 || facilityLevelTable[index].base_camp > 0); ++index)
            ++levelMax;
          break;
        case GuildFacilityParam.eFacilityType.GUILD_SHOP:
          for (int index = 0; index < facilityLevelTable.Length && (index == 0 || facilityLevelTable[index].guild_shop > 0L); ++index)
            ++levelMax;
          break;
      }
      return levelMax;
    }

    public static void SimlateEnhance(
      GuildFacilityData facility,
      long add_exp,
      out int new_level,
      out long rest_next_exp)
    {
      long num1 = facility.Exp + add_exp;
      int levelMax = GuildFacilityData.GetLevelMax(facility.Param.Type);
      GuildFacilityLvParam[] facilityLevelTable = MonoSingleton<GameManager>.Instance.MasterParam.GetGuildFacilityLevelTable();
      List<long> longList = new List<long>();
      switch (facility.Param.Type)
      {
        case GuildFacilityParam.eFacilityType.BASE_CAMP:
          for (int index = 0; index < facilityLevelTable.Length && index < levelMax; ++index)
            longList.Add((long) facilityLevelTable[index].base_camp);
          break;
        case GuildFacilityParam.eFacilityType.GUILD_SHOP:
          for (int index = 0; index < facilityLevelTable.Length && index < levelMax; ++index)
            longList.Add(facilityLevelTable[index].guild_shop);
          break;
      }
      new_level = 0;
      rest_next_exp = 0L;
      if (longList.Count <= 0)
        return;
      int num2 = 0;
      for (int index = 0; index < longList.Count; ++index)
      {
        num1 -= longList[index];
        if (num1 >= 0L)
          ++num2;
        else
          break;
      }
      new_level = num2;
      rest_next_exp = num1 <= 0L ? Math.Abs(num1) : 0L;
    }
  }
}
