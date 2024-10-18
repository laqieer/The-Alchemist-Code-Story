// Decompiled with JetBrains decompiler
// Type: SRPG.FilterArtifactParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class FilterArtifactParam
  {
    private string mIname;
    private string mTabName;
    private string mName;
    private FilterArtifactParam.eFilterType mFilterType;
    private FilterArtifactParam.Condition[] mConds;

    public string Iname => this.mIname;

    public string TabName => this.mTabName;

    public string Name => this.mName;

    public FilterArtifactParam.eFilterType FilterType => this.mFilterType;

    public List<FilterArtifactParam.Condition> CondList
    {
      get
      {
        return this.mConds != null ? new List<FilterArtifactParam.Condition>((IEnumerable<FilterArtifactParam.Condition>) this.mConds) : new List<FilterArtifactParam.Condition>();
      }
    }

    public void Deserialize(JSON_FilterArtifactParam json)
    {
      if (json == null)
        return;
      this.mIname = json.iname;
      this.mTabName = json.tab_name;
      this.mName = json.name;
      this.mFilterType = (FilterArtifactParam.eFilterType) json.filter_type;
      this.mConds = (FilterArtifactParam.Condition[]) null;
      if (json.cnds == null || json.cnds.Length == 0)
        return;
      this.mConds = new FilterArtifactParam.Condition[json.cnds.Length];
      for (int index = 0; index < json.cnds.Length; ++index)
      {
        this.mConds[index] = new FilterArtifactParam.Condition();
        this.mConds[index].Deserialize(this, json.cnds[index]);
      }
    }

    public static void Deserialize(JSON_FilterArtifactParam[] json, ref FilterArtifactParam[] array)
    {
      if (json == null)
        return;
      array = new FilterArtifactParam[json.Length];
      for (int index = 0; index < json.Length; ++index)
      {
        FilterArtifactParam filterArtifactParam = new FilterArtifactParam();
        filterArtifactParam.Deserialize(json[index]);
        array[index] = filterArtifactParam;
      }
    }

    public static FilterArtifactParam GetParam(string key)
    {
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) MonoSingleton<GameManager>.Instance))
        return (FilterArtifactParam) null;
      List<FilterArtifactParam> filterArtifactParamList = new List<FilterArtifactParam>((IEnumerable<FilterArtifactParam>) MonoSingleton<GameManager>.Instance.MasterParam.FilterArtifactParams);
      if (filterArtifactParamList == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>FilterArtifactParam/GetParam no data!</color>"));
        return (FilterArtifactParam) null;
      }
      FilterArtifactParam filterArtifactParam = filterArtifactParamList.Find((Predicate<FilterArtifactParam>) (d => d.Iname == key));
      if (filterArtifactParam == null)
        DebugUtility.Log(string.Format("<color=yellow>FilterArtifactParam/GetParam valid data not found! iname={0}</color>", (object) key));
      return filterArtifactParam;
    }

    public enum eFilterType
    {
      None,
      Rarity,
      EquipType,
      ArmsType,
    }

    public class Condition
    {
      private FilterArtifactParam mParent;
      private string mCndsName;
      private string mName;
      private int mRarity;
      private ArtifactTypes mEquipType;
      private string[] mArmsType;

      public string CndsName => this.mCndsName;

      public string Name => this.mName;

      public int Rarity => this.mRarity;

      public ArtifactTypes EquipType => this.mEquipType;

      public string[] ArmsType => this.mArmsType;

      public string PrefsKey
      {
        get => FilterUtility.FilterPrefs.MakeKey(this.mParent.Iname, this.mCndsName);
      }

      public void Deserialize(FilterArtifactParam parent, JSON_FilterArtifactParam.Condition json)
      {
        if (json == null)
          return;
        this.mParent = parent;
        this.mCndsName = json.cnds_name;
        this.mName = json.name;
        this.mRarity = json.rarity;
        this.mEquipType = (ArtifactTypes) json.equip_type;
        this.mArmsType = (string[]) null;
        if (json.arms_type == null || json.arms_type.Length == 0)
          return;
        this.mArmsType = new string[json.arms_type.Length];
        Array.Copy((Array) json.arms_type, (Array) this.mArmsType, json.arms_type.Length);
      }
    }
  }
}
