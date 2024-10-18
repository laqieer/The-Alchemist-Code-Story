// Decompiled with JetBrains decompiler
// Type: SRPG.SortUtility
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
  public static class SortUtility
  {
    private const char PREFS_KEY_SEPARATOR = ':';
    private static SortUtility.SortRunePrefs s_RunePrefsCache;
    private static bool s_IsCacheReady;

    public static void StableSort<T>(List<T> list, Comparison<T> comparison)
    {
      List<KeyValuePair<int, T>> keyValuePairList = new List<KeyValuePair<int, T>>(list.Count);
      for (int index = 0; index < list.Count; ++index)
        keyValuePairList.Add(new KeyValuePair<int, T>(index, list[index]));
      keyValuePairList.Sort((Comparison<KeyValuePair<int, T>>) ((x, y) =>
      {
        int num = comparison(x.Value, y.Value);
        if (num == 0)
          num = x.Key.CompareTo(y.Key);
        return num;
      }));
      for (int index = 0; index < list.Count; ++index)
        list[index] = keyValuePairList[index].Value;
    }

    public static void Save_RuneSortForCache(SortUtility.SortRunePrefs prefs)
    {
      SortUtility.s_IsCacheReady = false;
      if (prefs.IsParamSortAllOff)
      {
        prefs.SetIsBaseParam(true);
        prefs.SetIsEvoParam(true);
        prefs.SetIsSetParam(true);
      }
      SortUtility.SortPrefs.Save((SortUtility.SortPrefs) prefs);
    }

    public static SortUtility.SortRunePrefs Load_RuneSortFromCache()
    {
      if (SortUtility.s_IsCacheReady)
        return SortUtility.s_RunePrefsCache;
      SortUtility.s_RunePrefsCache = SortUtility.Load_RuneSort();
      SortUtility.s_IsCacheReady = true;
      return SortUtility.s_RunePrefsCache;
    }

    private static SortUtility.SortRunePrefs Load_RuneSort()
    {
      SortUtility.SortRunePrefs sortRunePrefs = (SortUtility.SortRunePrefs) SortUtility.SortRunePrefs.Load("SortRune");
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) MonoSingleton<GameManager>.GetInstanceDirect(), (UnityEngine.Object) null))
      {
        DebugUtility.LogError("GameManager が null です。GameManager が生成されてから使用してください。");
        return sortRunePrefs;
      }
      if (sortRunePrefs.IsPrefabKeyNotFound)
      {
        sortRunePrefs.SetAscending(true);
        sortRunePrefs.SetIsBaseParam(true);
        sortRunePrefs.SetIsEvoParam(true);
        sortRunePrefs.SetIsSetParam(true);
      }
      List<string> all_keys = new List<string>();
      foreach (SortRuneParam sortRuneParam in MonoSingleton<GameManager>.Instance.MasterParam.SortRuneParams)
      {
        foreach (SortRuneConditionParam condition in sortRuneParam.conditions)
        {
          sortRunePrefs.GetValue(condition.parent.iname, condition.cnds_iname);
          all_keys.Add(condition.PrefsKey);
        }
      }
      sortRunePrefs.RemoveKeys((Predicate<SortUtility.SortPrefsData>) (data =>
      {
        string temp_key = data.Key;
        return all_keys.Find((Predicate<string>) (key => key == temp_key)) == null;
      }));
      if (sortRunePrefs.IsDisableSortAll())
        sortRunePrefs.SetValue(0, true);
      return sortRunePrefs;
    }

    public static void SortRune(
      eRuneSortType type,
      bool isAscending,
      bool isBaseParamSort,
      bool isEvoParamSort,
      bool isSetParamSort,
      List<BindRuneData> rune_list)
    {
      List<SortUtility.MultipleSortTempData<BindRuneData>> multipleSortTempDataList = new List<SortUtility.MultipleSortTempData<BindRuneData>>();
      for (int index = 0; index < rune_list.Count; ++index)
      {
        BindRuneData rune1 = rune_list[index];
        if (rune1 != null)
        {
          RuneData rune2 = rune1.Rune;
          if (rune2 != null)
            multipleSortTempDataList.Add(new SortUtility.MultipleSortTempData<BindRuneData>(rune1, rune2.GetSortData(type, isAscending)));
        }
      }
      multipleSortTempDataList.Sort((Comparison<SortUtility.MultipleSortTempData<BindRuneData>>) ((x, y) =>
      {
        for (int index = 0; index < x.sort_count; ++index)
        {
          if (x.sort_val_list[index] != y.sort_val_list[index])
            return x.sort_val_list[index].CompareTo(y.sort_val_list[index]);
        }
        return 0;
      }));
      rune_list.Clear();
      for (int index = 0; index < multipleSortTempDataList.Count; ++index)
      {
        SortUtility.MultipleSortTempData<BindRuneData> multipleSortTempData = multipleSortTempDataList[index];
        rune_list.Add(multipleSortTempData.data);
      }
      if (isAscending)
        return;
      rune_list.Reverse();
    }

    [Serializable]
    public class JSON_SortDataList
    {
      public SortUtility.JSON_SortData[] list;
      public bool is_ascending;
    }

    [Serializable]
    public class JSON_SortDataListRune : SortUtility.JSON_SortDataList
    {
      public bool is_base_param;
      public bool is_evo_param;
      public bool is_set_param;
    }

    [Serializable]
    public class JSON_SortData
    {
      public string key;
      public int value;

      public bool Value
      {
        get => this.value == 1;
        set => this.value = !value ? 0 : 1;
      }
    }

    public class SortTempDiff
    {
      public List<bool> m_ToggleValues = new List<bool>();
      public bool m_IsAscending;
    }

    public class SortTempDiffRune : SortUtility.SortTempDiff
    {
      public bool m_IsBaseParam;
      public bool m_IsEvoParam;
      public bool m_IsSetParam;
    }

    public class SortPrefsData
    {
      private bool m_Value;
      private string m_MajorKey;
      private string m_MinorKey;

      public string Key => SortUtility.SortPrefs.MakeKey(this.m_MajorKey, this.m_MinorKey);

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

      public bool Deserialize(SortUtility.JSON_SortData json)
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

      public void Serialize(ref SortUtility.JSON_SortData json)
      {
        json = new SortUtility.JSON_SortData();
        json.key = this.Key;
        json.Value = this.m_Value;
      }
    }

    public class SortPrefs
    {
      private string m_PrefsKey;
      private List<SortUtility.SortPrefsData> m_SortPrefsDataList = new List<SortUtility.SortPrefsData>();
      private bool m_IsAscending;
      protected bool m_PrefsKeyWasNotFound;

      public SortPrefs(string prefsKey) => this.m_PrefsKey = prefsKey;

      public List<SortUtility.SortPrefsData> SortPrefsDataList => this.m_SortPrefsDataList;

      public bool IsAscending => this.m_IsAscending;

      public bool IsPrefabKeyNotFound => this.m_PrefsKeyWasNotFound;

      public static string MakeKey(string majorKey, string minorKey)
      {
        return majorKey + (object) ':' + minorKey;
      }

      public void SetValue(int index, bool value)
      {
        if (index >= this.m_SortPrefsDataList.Count)
          return;
        this.m_SortPrefsDataList[index].Value = value;
      }

      public void SetAscending(bool value) => this.m_IsAscending = value;

      public void SetValue(string majorKey, string minorKey, bool value)
      {
        SortUtility.SortPrefsData sortPrefsData = this.m_SortPrefsDataList.Find((Predicate<SortUtility.SortPrefsData>) (data => data.MajorKey == majorKey && data.MinorKey == minorKey));
        if (sortPrefsData == null)
          return;
        sortPrefsData.Value = value;
      }

      public void SetValueAll(string majorKey, bool value)
      {
        for (int index = 0; index < this.m_SortPrefsDataList.Count; ++index)
        {
          if (this.m_SortPrefsDataList[index].MajorKey == majorKey)
            this.m_SortPrefsDataList[index].Value = value;
        }
      }

      public void SetValueAll(bool value)
      {
        this.m_SortPrefsDataList.ForEach((Action<SortUtility.SortPrefsData>) (data => data.Value = value));
      }

      public bool GetValue(string majorKey, string minorKey, bool defaultValue = false)
      {
        SortUtility.SortPrefsData sortPrefsData = this.m_SortPrefsDataList.Find((Predicate<SortUtility.SortPrefsData>) (data => data.MajorKey == majorKey && data.MinorKey == minorKey));
        if (sortPrefsData != null)
          return sortPrefsData.Value;
        this.m_SortPrefsDataList.Add(new SortUtility.SortPrefsData()
        {
          MajorKey = majorKey,
          MinorKey = minorKey,
          Value = defaultValue
        });
        return defaultValue;
      }

      public bool IsDisableSortAll()
      {
        return this.m_SortPrefsDataList.Find((Predicate<SortUtility.SortPrefsData>) (data => data.Value)) == null;
      }

      public void RemoveKeys(Predicate<SortUtility.SortPrefsData> predicate)
      {
        this.m_SortPrefsDataList.RemoveAll(predicate);
      }

      public SortUtility.SortPrefsData FindFirstOn()
      {
        for (int index = 0; index < this.m_SortPrefsDataList.Count; ++index)
        {
          if (this.m_SortPrefsDataList[index].Value)
            return this.m_SortPrefsDataList[index];
        }
        return (SortUtility.SortPrefsData) null;
      }

      public virtual void Deserialize(SortUtility.JSON_SortDataList json)
      {
        if (json == null || json.list == null)
          return;
        this.m_IsAscending = json.is_ascending;
        this.m_SortPrefsDataList = new List<SortUtility.SortPrefsData>(json.list.Length);
        for (int index = 0; index < json.list.Length; ++index)
        {
          SortUtility.SortPrefsData sortPrefsData = new SortUtility.SortPrefsData();
          if (sortPrefsData.Deserialize(json.list[index]))
            this.m_SortPrefsDataList.Add(sortPrefsData);
        }
      }

      public virtual void Serialize(ref SortUtility.JSON_SortDataList json)
      {
        if (json == null)
          json = new SortUtility.JSON_SortDataList();
        json.list = new SortUtility.JSON_SortData[this.m_SortPrefsDataList.Count];
        json.is_ascending = this.m_IsAscending;
        for (int index = 0; index < json.list.Length; ++index)
        {
          json.list[index] = new SortUtility.JSON_SortData();
          json.list[index].key = this.m_SortPrefsDataList[index].Key;
          json.list[index].Value = this.m_SortPrefsDataList[index].Value;
        }
      }

      public static void Save(SortUtility.SortPrefs sortPrefs)
      {
        SortUtility.JSON_SortDataList json1 = (SortUtility.JSON_SortDataList) null;
        sortPrefs.Serialize(ref json1);
        string json2 = JsonUtility.ToJson((object) json1);
        PlayerPrefsUtility.SetString(sortPrefs.m_PrefsKey, json2, true);
      }

      public static SortUtility.SortPrefs Load(string prefsKey)
      {
        SortUtility.SortPrefs sortPrefs = new SortUtility.SortPrefs(prefsKey);
        if (PlayerPrefsUtility.HasKey(prefsKey))
        {
          sortPrefs.m_PrefsKeyWasNotFound = false;
          SortUtility.JSON_SortDataList json = JsonUtility.FromJson<SortUtility.JSON_SortDataList>(PlayerPrefsUtility.GetString(prefsKey, string.Empty));
          sortPrefs.Deserialize(json);
        }
        else
          sortPrefs.m_PrefsKeyWasNotFound = true;
        return sortPrefs;
      }

      public virtual SortUtility.SortTempDiff CreateTempDiffData()
      {
        SortUtility.SortTempDiff tempDiffData = new SortUtility.SortTempDiff();
        this.CopyTo(tempDiffData);
        return tempDiffData;
      }

      public void CopyTo(SortUtility.SortTempDiff tempDiffData)
      {
        for (int index = 0; index < this.m_SortPrefsDataList.Count; ++index)
          tempDiffData.m_ToggleValues.Add(this.m_SortPrefsDataList[index].Value);
        tempDiffData.m_IsAscending = this.m_IsAscending;
      }

      public virtual bool IsDiff(SortUtility.SortTempDiff tempDiffData)
      {
        if (tempDiffData.m_ToggleValues.Count != this.m_SortPrefsDataList.Count || tempDiffData.m_IsAscending != this.m_IsAscending)
          return true;
        for (int index = 0; index < this.m_SortPrefsDataList.Count; ++index)
        {
          if (this.m_SortPrefsDataList[index].Value != tempDiffData.m_ToggleValues[index])
            return true;
        }
        return false;
      }
    }

    public class SortRunePrefs : SortUtility.SortPrefs
    {
      private bool m_IsBaseParam;
      private bool m_IsEvoParam;
      private bool m_IsSetParam;

      public SortRunePrefs(string prefsKey)
        : base(prefsKey)
      {
      }

      public bool IsBaseParamSort => this.m_IsBaseParam;

      public bool IsEvoParamSort => this.m_IsEvoParam;

      public bool IsSetParamSort => this.m_IsSetParam;

      public bool IsParamSortAllOff
      {
        get => !this.IsBaseParamSort && !this.IsEvoParamSort && !this.IsSetParamSort;
      }

      public void SetIsBaseParam(bool value) => this.m_IsBaseParam = value;

      public void SetIsEvoParam(bool value) => this.m_IsEvoParam = value;

      public void SetIsSetParam(bool value) => this.m_IsSetParam = value;

      public override void Deserialize(SortUtility.JSON_SortDataList json)
      {
        if (json == null)
          return;
        base.Deserialize(json);
        if (!(json is SortUtility.JSON_SortDataListRune sortDataListRune))
        {
          DebugUtility.LogError("json を JSON_SortDataListRune にキャストできませんでした。ルーンのソートは、SortRunePrefs.Load を呼び出してください。");
        }
        else
        {
          this.m_IsBaseParam = sortDataListRune.is_base_param;
          this.m_IsEvoParam = sortDataListRune.is_evo_param;
          this.m_IsSetParam = sortDataListRune.is_set_param;
        }
      }

      public override void Serialize(ref SortUtility.JSON_SortDataList json)
      {
        json = (SortUtility.JSON_SortDataList) new SortUtility.JSON_SortDataListRune();
        base.Serialize(ref json);
        SortUtility.JSON_SortDataListRune sortDataListRune = json as SortUtility.JSON_SortDataListRune;
        sortDataListRune.is_base_param = this.IsBaseParamSort;
        sortDataListRune.is_evo_param = this.IsEvoParamSort;
        sortDataListRune.is_set_param = this.IsSetParamSort;
      }

      public new static SortUtility.SortPrefs Load(string prefsKey)
      {
        SortUtility.SortRunePrefs sortRunePrefs = new SortUtility.SortRunePrefs(prefsKey);
        if (PlayerPrefsUtility.HasKey(prefsKey))
        {
          sortRunePrefs.m_PrefsKeyWasNotFound = false;
          SortUtility.JSON_SortDataListRune json = JsonUtility.FromJson<SortUtility.JSON_SortDataListRune>(PlayerPrefsUtility.GetString(prefsKey, string.Empty));
          sortRunePrefs.Deserialize((SortUtility.JSON_SortDataList) json);
        }
        else
          sortRunePrefs.m_PrefsKeyWasNotFound = true;
        return (SortUtility.SortPrefs) sortRunePrefs;
      }

      public override SortUtility.SortTempDiff CreateTempDiffData()
      {
        SortUtility.SortTempDiffRune tempDiffData = new SortUtility.SortTempDiffRune();
        this.CopyTo((SortUtility.SortTempDiff) tempDiffData);
        tempDiffData.m_IsBaseParam = this.m_IsBaseParam;
        tempDiffData.m_IsEvoParam = this.m_IsEvoParam;
        tempDiffData.m_IsSetParam = this.m_IsSetParam;
        return (SortUtility.SortTempDiff) tempDiffData;
      }

      public override bool IsDiff(SortUtility.SortTempDiff tempDiffData)
      {
        return !(tempDiffData is SortUtility.SortTempDiffRune tempDiffData1) || tempDiffData1.m_IsBaseParam != this.m_IsBaseParam || tempDiffData1.m_IsEvoParam != this.m_IsEvoParam || tempDiffData1.m_IsSetParam != this.m_IsSetParam || base.IsDiff((SortUtility.SortTempDiff) tempDiffData1);
      }
    }

    public class SortTempData<T> where T : class
    {
      public T data;
      public long sort_val;

      public SortTempData(T _data, long _sort_val)
      {
        this.data = _data;
        this.sort_val = _sort_val;
      }

      public override string ToString() => "val : " + (object) this.sort_val;
    }

    public class MultipleSortTempData<T> where T : class
    {
      public T data;
      public int sort_count;
      public List<long> sort_val_list;

      public MultipleSortTempData(T _data, List<long> _sort_val_list)
      {
        this.data = _data;
        this.sort_count = _sort_val_list.Count;
        this.sort_val_list = _sort_val_list;
      }
    }
  }
}
