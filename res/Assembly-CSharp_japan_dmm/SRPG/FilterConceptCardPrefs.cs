// Decompiled with JetBrains decompiler
// Type: SRPG.FilterConceptCardPrefs
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class FilterConceptCardPrefs
  {
    private static FilterConceptCardPrefs cache;
    private static bool is_use_cache;
    private FilterUtility.FilterPrefs prefs_data;

    public bool GetValue(string majorKey, string minorKey)
    {
      return this.prefs_data.GetValue(majorKey, minorKey);
    }

    public void SetValue(string majorKey, string minorKey, bool value)
    {
      this.prefs_data.SetValue(majorKey, minorKey, value);
    }

    public void AllOff(string majorKey) => this.prefs_data.SetValueAll(majorKey, false);

    public bool IsDisableFilterAll() => this.prefs_data.IsDisableFilterAll();

    public bool IsDisableFilterAll(string majorKey) => this.prefs_data.IsDisableFilterAll(majorKey);

    public bool IsEnableFilterAll(string majorKey) => this.prefs_data.IsEnableFilterAll(majorKey);

    public List<bool> CreateFlagList()
    {
      List<bool> flagList = new List<bool>();
      if (this.prefs_data != null)
      {
        for (int index = 0; index < this.prefs_data.FilterPrefsDataList.Count; ++index)
          flagList.Add(this.prefs_data.FilterPrefsDataList[index].Value);
      }
      return flagList;
    }

    public bool IsDiff(List<bool> target)
    {
      if (this.prefs_data.FilterPrefsDataList.Count != target.Count)
        return true;
      int count = this.prefs_data.FilterPrefsDataList.Count;
      for (int index = 0; index < count; ++index)
      {
        if (this.prefs_data.FilterPrefsDataList[index].Value != target[index])
          return true;
      }
      return false;
    }

    public static FilterConceptCardPrefs Load()
    {
      if (FilterConceptCardPrefs.is_use_cache)
        return FilterConceptCardPrefs.cache;
      FilterConceptCardPrefs conceptCardPrefs = new FilterConceptCardPrefs();
      conceptCardPrefs.prefs_data = FilterUtility.Load_ConceptCardFilter();
      FilterConceptCardPrefs.is_use_cache = true;
      FilterConceptCardPrefs.cache = conceptCardPrefs;
      return conceptCardPrefs;
    }

    public static void Save(FilterConceptCardPrefs data)
    {
      FilterConceptCardPrefs.is_use_cache = false;
      FilterUtility.FilterPrefs.Save(data.prefs_data, true);
    }
  }
}
