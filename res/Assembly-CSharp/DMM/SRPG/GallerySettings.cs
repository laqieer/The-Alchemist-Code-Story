// Decompiled with JetBrains decompiler
// Type: SRPG.GallerySettings
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [Serializable]
  public class GallerySettings
  {
    [SerializeField]
    public GallerySettings.SortType sortType;
    [SerializeField]
    public bool isRarityAscending;
    [SerializeField]
    public bool isNameAscending;
    [SerializeField]
    public int[] rareFilters;
    private static readonly int[] AllOnFilter = new int[5]
    {
      0,
      1,
      2,
      3,
      4
    };

    public static GallerySettings CreateDefaultSettings()
    {
      return new GallerySettings()
      {
        sortType = GallerySettings.SortType.Rarity,
        isRarityAscending = true,
        isNameAscending = true,
        rareFilters = new int[0]
      };
    }

    public GallerySettings Clone()
    {
      return new GallerySettings()
      {
        sortType = this.sortType,
        isRarityAscending = this.isRarityAscending,
        isNameAscending = this.isNameAscending,
        rareFilters = ((IEnumerable<int>) this.rareFilters).ToArray<int>()
      };
    }

    public override bool Equals(object obj)
    {
      if (object.ReferenceEquals((object) this, obj))
        return true;
      return obj is GallerySettings other && this.IsSameFilter(other) && this.IsSameSortSetting(other);
    }

    public override int GetHashCode() => base.GetHashCode();

    public bool IsSameSortSetting(GallerySettings other)
    {
      return this.sortType == other.sortType && this.isRarityAscending == other.isRarityAscending && this.isNameAscending == other.isNameAscending;
    }

    public bool IsFilterTotallyOn()
    {
      return GallerySettings.IsSameFilter((IEnumerable<int>) this.rareFilters, (IEnumerable<int>) GallerySettings.AllOnFilter);
    }

    public static bool IsFilterTotallyOn(IEnumerable<int> filter)
    {
      return GallerySettings.IsSameFilter(filter, (IEnumerable<int>) GallerySettings.AllOnFilter);
    }

    public bool IsFilterTotallyOff() => this.rareFilters.Length <= 0;

    public bool IsSameFilter(GallerySettings other)
    {
      return other != null && GallerySettings.IsSameFilter((IEnumerable<int>) this.rareFilters, (IEnumerable<int>) other.rareFilters);
    }

    public bool IsSameFilter(IEnumerable<int> filter)
    {
      return GallerySettings.IsSameFilter((IEnumerable<int>) this.rareFilters, filter);
    }

    public static bool IsSameFilter(IEnumerable<int> fil1, IEnumerable<int> fil2)
    {
      return !object.ReferenceEquals((object) fil1, (object) null) && !object.ReferenceEquals((object) fil2, (object) null) && (object.ReferenceEquals((object) fil1, (object) fil2) || fil1.OrderBy<int, int>((Func<int, int>) (x => x)).SequenceEqual<int>((IEnumerable<int>) fil2.OrderBy<int, int>((Func<int, int>) (x => x))));
    }

    public enum SortType
    {
      Rarity,
      Name,
    }
  }
}
