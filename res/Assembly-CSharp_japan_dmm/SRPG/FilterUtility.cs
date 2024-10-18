// Decompiled with JetBrains decompiler
// Type: SRPG.FilterUtility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class FilterUtility
  {
    private const char PREFS_KEY_SEPARATOR = ':';

    public static FilterUtility.FilterPrefs Load_UnitFilter()
    {
      FilterUtility.FilterPrefs filterPrefs = FilterUtility.FilterPrefs.Load("FilterUnit");
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) MonoSingleton<GameManager>.GetInstanceDirect(), (UnityEngine.Object) null))
      {
        DebugUtility.LogError("GameManager が null です。GameManager が生成されてから使用してください。");
        return filterPrefs;
      }
      List<string> all_keys = new List<string>();
      foreach (FilterUnitParam filterUnitParam in MonoSingleton<GameManager>.Instance.MasterParam.FilterUnitParams)
      {
        foreach (FilterUnitConditionParam condition in filterUnitParam.conditions)
        {
          filterPrefs.GetValue(condition.parent.iname, condition.cnds_iname);
          all_keys.Add(condition.PrefsKey);
        }
      }
      filterPrefs.RemoveKeys((Predicate<FilterUtility.FilterPrefsData>) (data =>
      {
        string temp_key = data.Key;
        return all_keys.Find((Predicate<string>) (key => key == temp_key)) == null;
      }));
      return filterPrefs;
    }

    public static bool MatchCondition(UnitParam unitParam, FilterUtility.FilterPrefs filter)
    {
      foreach (FilterUnitParam filterUnitParam in MonoSingleton<GameManager>.Instance.MasterParam.FilterUnitParams)
      {
        if (!filter.IsDisableFilterAll(filterUnitParam.iname))
        {
          bool flag = false;
          for (int index1 = 0; index1 < filterUnitParam.conditions.Length; ++index1)
          {
            FilterUnitConditionParam condition = filterUnitParam.conditions[index1];
            if (filter.GetValue(filterUnitParam.iname, condition.cnds_iname))
            {
              if (filterUnitParam.IsEnableFilterType(eFilterUnitTypes.RarityIni))
                flag |= (int) unitParam.rare == condition.rarity_ini;
              else if (filterUnitParam.IsEnableFilterType(eFilterUnitTypes.Birth))
                flag |= (EBirth) unitParam.birthID == condition.birth;
              else if (filterUnitParam.IsEnableFilterType(eFilterUnitTypes.Sex))
                flag |= unitParam.sex == condition.sex;
              else if (filterUnitParam.IsEnableFilterType(eFilterUnitTypes.UnitGroup))
                flag |= UnitGroupParam.IsInGroup(condition.unit_group_params, unitParam.iname);
              else if (filterUnitParam.IsEnableFilterType(eFilterUnitTypes.JobGroup))
                flag |= JobGroupParam.IsInGroup(condition.job_group_params, unitParam);
              else if (filterUnitParam.IsEnableFilterType(eFilterUnitTypes.DefArtifact))
              {
                for (int index2 = 0; index2 < condition.af_tags.Length; ++index2)
                {
                  flag |= unitParam.HasArtifactByTag(condition.af_tags[index2]);
                  if (flag)
                    break;
                }
              }
              if (flag)
                break;
            }
          }
          if (!flag)
            return false;
        }
      }
      return true;
    }

    public static void FilterUnit(ref List<UnitParam> units, FilterUtility.FilterPrefs filter)
    {
      FilterUnitParam[] filterUnitParams = MonoSingleton<GameManager>.Instance.MasterParam.FilterUnitParams;
      for (int index = 0; index < units.Count; ++index)
      {
        UnitParam unitParam = units[index];
        if (unitParam != null && !FilterUtility.MatchCondition(unitParam, filter))
          units.RemoveAt(index--);
      }
    }

    public static FilterUtility.FilterPrefs Load_ConceptCardFilter()
    {
      FilterUtility.FilterPrefs filterPrefs = FilterUtility.FilterPrefs.Load(PlayerPrefsUtility.FILTER_CONCEPT_CARD);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) MonoSingleton<GameManager>.GetInstanceDirect(), (UnityEngine.Object) null))
      {
        DebugUtility.LogError("GameManager が null です。GameManager が生成されてから使用してください。");
        return filterPrefs;
      }
      List<string> all_keys = new List<string>();
      foreach (FilterConceptCardParam conceptCardParam in MonoSingleton<GameManager>.Instance.MasterParam.FilterConceptCardParams)
      {
        foreach (FilterConceptCardConditionParam condition in conceptCardParam.conditions)
        {
          filterPrefs.GetValue(condition.parent.iname, condition.cnds_iname);
          all_keys.Add(condition.PrefsKey);
        }
      }
      filterPrefs.RemoveKeys((Predicate<FilterUtility.FilterPrefsData>) (data =>
      {
        string temp_key = data.Key;
        return all_keys.Find((Predicate<string>) (key => key == temp_key)) == null;
      }));
      return filterPrefs;
    }

    public static FilterUtility.FilterPrefs Load_RuneFilter()
    {
      FilterUtility.FilterPrefs filterPrefs = FilterUtility.FilterPrefs.Load(PlayerPrefsUtility.FILTER_RUNE);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) MonoSingleton<GameManager>.GetInstanceDirect(), (UnityEngine.Object) null))
      {
        DebugUtility.LogError("GameManager が null です。GameManager が生成されてから使用してください。");
        return filterPrefs;
      }
      List<string> all_keys = new List<string>();
      foreach (FilterRuneParam filterRuneParam in MonoSingleton<GameManager>.Instance.MasterParam.FilterRuneParams)
      {
        foreach (FilterRuneConditionParam condition in filterRuneParam.conditions)
        {
          filterPrefs.GetValue(condition.parent.iname, condition.cnds_iname);
          all_keys.Add(condition.PrefsKey);
        }
      }
      filterPrefs.RemoveKeys((Predicate<FilterUtility.FilterPrefsData>) (data =>
      {
        string temp_key = data.Key;
        return all_keys.Find((Predicate<string>) (key => key == temp_key)) == null;
      }));
      return filterPrefs;
    }

    public static bool RuneMatchCondition(BindRuneData runeData, FilterUtility.FilterPrefs filter)
    {
      foreach (FilterRuneParam filterRuneParam in MonoSingleton<GameManager>.Instance.MasterParam.FilterRuneParams)
      {
        if (!filter.IsDisableFilterAll(filterRuneParam.iname))
        {
          bool flag = false;
          for (int index1 = 0; index1 < filterRuneParam.conditions.Length; ++index1)
          {
            FilterRuneConditionParam condition = filterRuneParam.conditions[index1];
            if (filter.GetValue(filterRuneParam.iname, condition.cnds_iname))
            {
              if (filterRuneParam.IsEnableFilterType(eRuneFilterTypes.Rarity))
                flag |= runeData.Rune.Rarity == (int) condition.rarity;
              else if (filterRuneParam.IsEnableFilterType(eRuneFilterTypes.SetEff))
                flag |= runeData.Rune.RuneParam.seteff_type == (int) condition.set_eff;
              else if (filterRuneParam.IsEnableFilterType(eRuneFilterTypes.EvoStatue))
              {
                for (int index2 = 0; index2 < runeData.Rune.state.evo_state.Count; ++index2)
                {
                  if (runeData.Rune.state.evo_state[index2].evo_lot.type == (ParamTypes) condition.evo_status)
                  {
                    flag = true;
                    break;
                  }
                }
              }
              if (flag)
                break;
            }
          }
          if (!flag)
            return false;
        }
      }
      return true;
    }

    public static void FilterRune(ref List<BindRuneData> runes, FilterUtility.FilterPrefs filter)
    {
      for (int index = 0; index < runes.Count; ++index)
      {
        BindRuneData runeData = runes[index];
        if (runeData != null && !FilterUtility.RuneMatchCondition(runeData, filter))
          runes.RemoveAt(index--);
      }
    }

    [Serializable]
    public class JSON_FilterDataList
    {
      public FilterUtility.JSON_FilterData[] list;
    }

    [Serializable]
    public class JSON_FilterData
    {
      public string key;
      public int value;

      public bool Value
      {
        get => this.value == 1;
        set => this.value = !value ? 0 : 1;
      }
    }

    public class FilterPrefsData
    {
      private bool m_Value;
      private string m_MajorKey;
      private string m_MinorKey;

      public string Key => FilterUtility.FilterPrefs.MakeKey(this.m_MajorKey, this.m_MinorKey);

      public bool Value
      {
        get => this.m_Value;
        set => this.m_Value = value;
      }

      public string MajorKey
      {
        get => this.m_MajorKey;
        set => this.m_MajorKey = value;
      }

      public string MinorKey
      {
        get => this.m_MinorKey;
        set => this.m_MinorKey = value;
      }

      public bool Deserialize(FilterUtility.JSON_FilterData json)
      {
        this.m_Value = json.Value;
        if (string.IsNullOrEmpty(json.key))
        {
          DebugUtility.LogError("ヤバイ！ キーが空！");
          return false;
        }
        string[] strArray = json.key.Split(':');
        if (strArray.Length < 2)
        {
          DebugUtility.LogError("ヤバイ！ 区切り記号が見つからなかった！");
          return false;
        }
        this.m_MajorKey = strArray[0];
        this.m_MinorKey = strArray[1];
        return true;
      }

      public void Serialize(ref FilterUtility.JSON_FilterData json)
      {
        json = new FilterUtility.JSON_FilterData();
        json.key = this.Key;
        json.Value = this.m_Value;
      }
    }

    public class FilterPrefs
    {
      private string m_PrefsKey;
      private List<FilterUtility.FilterPrefsData> m_FilterPrefsDataList = new List<FilterUtility.FilterPrefsData>();

      public List<FilterUtility.FilterPrefsData> FilterPrefsDataList => this.m_FilterPrefsDataList;

      public static string MakeKey(string majorKey, string minorKey)
      {
        return majorKey + (object) ':' + minorKey;
      }

      public void SetValue(string majorKey, string minorKey, bool value)
      {
        FilterUtility.FilterPrefsData filterPrefsData = this.m_FilterPrefsDataList.Find((Predicate<FilterUtility.FilterPrefsData>) (data => data.MajorKey == majorKey && data.MinorKey == minorKey));
        if (filterPrefsData == null)
          return;
        filterPrefsData.Value = value;
      }

      public void SetValueAll(string majorKey, bool value)
      {
        for (int index = 0; index < this.m_FilterPrefsDataList.Count; ++index)
        {
          if (this.m_FilterPrefsDataList[index].MajorKey == majorKey)
            this.m_FilterPrefsDataList[index].Value = value;
        }
      }

      public void SetValueAll(bool value)
      {
        this.m_FilterPrefsDataList.ForEach((Action<FilterUtility.FilterPrefsData>) (data => data.Value = value));
      }

      public bool GetValue(string majorKey, string minorKey, bool defaultValue = false)
      {
        FilterUtility.FilterPrefsData filterPrefsData = this.m_FilterPrefsDataList.Find((Predicate<FilterUtility.FilterPrefsData>) (data => data.MajorKey == majorKey && data.MinorKey == minorKey));
        if (filterPrefsData != null)
          return filterPrefsData.Value;
        this.m_FilterPrefsDataList.Add(new FilterUtility.FilterPrefsData()
        {
          MajorKey = majorKey,
          MinorKey = minorKey,
          Value = defaultValue
        });
        return defaultValue;
      }

      public bool IsEnableFilterAll(string majorKey)
      {
        return this.m_FilterPrefsDataList.Find((Predicate<FilterUtility.FilterPrefsData>) (data => data.MajorKey == majorKey && !data.Value)) == null;
      }

      public bool IsEnableFilterAll()
      {
        return this.m_FilterPrefsDataList.Find((Predicate<FilterUtility.FilterPrefsData>) (data => !data.Value)) == null;
      }

      public bool IsDisableFilterAll(string majorKey)
      {
        return this.m_FilterPrefsDataList.Find((Predicate<FilterUtility.FilterPrefsData>) (data => data.MajorKey == majorKey && data.Value)) == null;
      }

      public bool IsDisableFilterAll()
      {
        return this.m_FilterPrefsDataList.Find((Predicate<FilterUtility.FilterPrefsData>) (data => data.Value)) == null;
      }

      public void RemoveKeys(Predicate<FilterUtility.FilterPrefsData> predicate)
      {
        this.m_FilterPrefsDataList.RemoveAll(predicate);
      }

      public void Deserialize(FilterUtility.JSON_FilterDataList json)
      {
        if (json == null || json.list == null)
          return;
        this.m_FilterPrefsDataList = new List<FilterUtility.FilterPrefsData>(json.list.Length);
        for (int index = 0; index < json.list.Length; ++index)
        {
          FilterUtility.FilterPrefsData filterPrefsData = new FilterUtility.FilterPrefsData();
          if (filterPrefsData.Deserialize(json.list[index]))
            this.m_FilterPrefsDataList.Add(filterPrefsData);
        }
      }

      public void Serialize(ref FilterUtility.JSON_FilterDataList json)
      {
        json = new FilterUtility.JSON_FilterDataList();
        json.list = new FilterUtility.JSON_FilterData[this.m_FilterPrefsDataList.Count];
        for (int index = 0; index < json.list.Length; ++index)
        {
          json.list[index] = new FilterUtility.JSON_FilterData();
          json.list[index].key = this.m_FilterPrefsDataList[index].Key;
          json.list[index].Value = this.m_FilterPrefsDataList[index].Value;
        }
      }

      public static void Save(FilterUtility.FilterPrefs filterPrefs, bool allOnIsAllOff)
      {
        if (allOnIsAllOff)
        {
          for (int index = 0; index < filterPrefs.m_FilterPrefsDataList.Count; ++index)
          {
            FilterUtility.FilterPrefsData filterPrefsData = filterPrefs.m_FilterPrefsDataList[index];
            if (filterPrefs.IsEnableFilterAll(filterPrefsData.MajorKey))
              filterPrefs.SetValueAll(filterPrefsData.MajorKey, false);
          }
        }
        FilterUtility.FilterPrefs.Save(filterPrefs);
      }

      public static void Save(FilterUtility.FilterPrefs filterPrefs)
      {
        FilterUtility.JSON_FilterDataList json1 = (FilterUtility.JSON_FilterDataList) null;
        filterPrefs.Serialize(ref json1);
        string json2 = JsonUtility.ToJson((object) json1);
        PlayerPrefsUtility.SetString(filterPrefs.m_PrefsKey, json2, true);
      }

      public static FilterUtility.FilterPrefs Load(string prefsKey)
      {
        FilterUtility.FilterPrefs filterPrefs = new FilterUtility.FilterPrefs();
        filterPrefs.m_PrefsKey = prefsKey;
        if (PlayerPrefsUtility.HasKey(prefsKey))
        {
          FilterUtility.JSON_FilterDataList json = JsonUtility.FromJson<FilterUtility.JSON_FilterDataList>(PlayerPrefsUtility.GetString(prefsKey, string.Empty));
          filterPrefs.Deserialize(json);
        }
        return filterPrefs;
      }
    }

    public class FilterBindData
    {
      public int Rarity;
      public string Name;
      public ArtifactTypes EquipType;
      public byte RuneSetEffectType;

      public FilterBindData()
      {
      }

      public FilterBindData(
        int rarity,
        string name,
        ArtifactTypes equip_type = ArtifactTypes.None,
        byte runeSetEffectType = 0)
      {
        this.Rarity = rarity;
        this.Name = name;
        this.EquipType = equip_type;
        this.RuneSetEffectType = runeSetEffectType;
      }

      public int RuneSetEffectIconIndex => (int) this.RuneSetEffectType - 1;
    }
  }
}
