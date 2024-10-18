// Decompiled with JetBrains decompiler
// Type: SRPG.GuildFacilityData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class GuildFacilityData
  {
    private long mGid;
    private string mIname;
    private int mExp;
    private int mLevel;
    private GuildFacilityParam mParam;

    public long Gid
    {
      get
      {
        return this.mGid;
      }
    }

    public string Iname
    {
      get
      {
        return this.mIname;
      }
    }

    public int Exp
    {
      get
      {
        return this.mExp;
      }
    }

    public int Level
    {
      get
      {
        return this.mLevel;
      }
    }

    public GuildFacilityParam Param
    {
      get
      {
        return this.mParam;
      }
    }

    public bool Deserialize(JSON_GuildFacilityData json)
    {
      this.mGid = json.gid;
      this.mIname = json.facility_iname;
      this.mExp = json.exp;
      this.mLevel = json.level;
      this.mParam = MonoSingleton<GameManager>.Instance.MasterParam.GetGuildFacility(this.mIname);
      return true;
    }

    public GuildFacilityEffectParam GetEffect()
    {
      return this.mParam.GetEffect(this.mLevel);
    }

    public static int GetNeedExp(int from_lv, int to_lv, GuildFacilityParam.eFacilityType type)
    {
      GuildFacilityLvParam[] facilityLevelTable = MonoSingleton<GameManager>.Instance.MasterParam.GetGuildFacilityLevelTable();
      int num = 0;
      if (type != GuildFacilityParam.eFacilityType.NONE && type == GuildFacilityParam.eFacilityType.BASE_CAMP)
      {
        for (int index = from_lv; index < facilityLevelTable.Length && index < to_lv; ++index)
          num += facilityLevelTable[index].base_camp;
      }
      return num;
    }

    public static int GetLevelMax(GuildFacilityParam.eFacilityType type)
    {
      GuildFacilityLvParam[] facilityLevelTable = MonoSingleton<GameManager>.Instance.MasterParam.GetGuildFacilityLevelTable();
      int num = 0;
      if (type != GuildFacilityParam.eFacilityType.NONE && type == GuildFacilityParam.eFacilityType.BASE_CAMP)
      {
        for (int index = 0; index < facilityLevelTable.Length && (index == 0 || facilityLevelTable[index].base_camp > 0); ++index)
          ++num;
      }
      return num;
    }

    public static void SimlateEnhance(GuildFacilityData facility, int add_exp, out int new_level, out int rest_next_exp)
    {
      int num1 = facility.Exp + add_exp;
      int levelMax = GuildFacilityData.GetLevelMax(facility.Param.Type);
      GuildFacilityLvParam[] facilityLevelTable = MonoSingleton<GameManager>.Instance.MasterParam.GetGuildFacilityLevelTable();
      List<int> intList = new List<int>();
      if (facility.Param.Type == GuildFacilityParam.eFacilityType.BASE_CAMP)
      {
        for (int index = 0; index < facilityLevelTable.Length && index < levelMax; ++index)
          intList.Add(facilityLevelTable[index].base_camp);
      }
      new_level = 0;
      rest_next_exp = 0;
      if (intList.Count <= 0)
        return;
      int num2 = 0;
      for (int index = 0; index < intList.Count; ++index)
      {
        num1 -= intList[index];
        if (num1 >= 0)
          ++num2;
        else
          break;
      }
      new_level = num2;
      rest_next_exp = num1 <= 0 ? Mathf.Abs(num1) : 0;
    }
  }
}
