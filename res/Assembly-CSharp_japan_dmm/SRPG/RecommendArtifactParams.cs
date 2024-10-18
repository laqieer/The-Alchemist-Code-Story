// Decompiled with JetBrains decompiler
// Type: SRPG.RecommendArtifactParams
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class RecommendArtifactParams
  {
    private List<ArtifactParam> mUnitJobArtifacts = new List<ArtifactParam>();
    private List<ArtifactParam> mUnitArtifacts = new List<ArtifactParam>();
    private List<ArtifactParam> mAbilityArtifacts = new List<ArtifactParam>();

    public List<ArtifactParam> UnitJobArtifacts => this.mUnitJobArtifacts;

    public List<ArtifactParam> UnitArtifacts => this.mUnitArtifacts;

    public List<ArtifactParam> AbilityArtifacts => this.mAbilityArtifacts;

    public List<ArtifactParam> GetAll()
    {
      List<ArtifactParam> all = new List<ArtifactParam>();
      Dictionary<ArtifactTypes, List<ArtifactParam>> dictionary = new Dictionary<ArtifactTypes, List<ArtifactParam>>();
      dictionary.Add(ArtifactTypes.Arms, new List<ArtifactParam>());
      dictionary.Add(ArtifactTypes.Armor, new List<ArtifactParam>());
      dictionary.Add(ArtifactTypes.Accessory, new List<ArtifactParam>());
      if (this.mUnitJobArtifacts != null)
      {
        for (int index = 0; index < this.mUnitJobArtifacts.Count; ++index)
        {
          ArtifactTypes type = this.mUnitJobArtifacts[index].type;
          if (!dictionary[type].Contains(this.mUnitJobArtifacts[index]))
            dictionary[type].Add(this.mUnitJobArtifacts[index]);
        }
      }
      if (this.mUnitArtifacts != null)
      {
        for (int index = 0; index < this.mUnitArtifacts.Count; ++index)
        {
          ArtifactTypes type = this.mUnitArtifacts[index].type;
          if (!dictionary[type].Contains(this.mUnitArtifacts[index]))
            dictionary[type].Add(this.mUnitArtifacts[index]);
        }
      }
      if (this.mAbilityArtifacts != null)
      {
        for (int index = 0; index < this.mAbilityArtifacts.Count; ++index)
        {
          ArtifactTypes type = this.mAbilityArtifacts[index].type;
          if (!dictionary[type].Contains(this.mAbilityArtifacts[index]))
            dictionary[type].Add(this.mAbilityArtifacts[index]);
        }
      }
      all.AddRange((IEnumerable<ArtifactParam>) dictionary[ArtifactTypes.Arms]);
      all.AddRange((IEnumerable<ArtifactParam>) dictionary[ArtifactTypes.Armor]);
      all.AddRange((IEnumerable<ArtifactParam>) dictionary[ArtifactTypes.Accessory]);
      return all;
    }

    public List<ArtifactData> GetRecommendArtifacts(
      List<ArtifactData> player_artifacts,
      ArtifactTypes type,
      int num)
    {
      List<ArtifactData> recommendArtifacts = new List<ArtifactData>();
      int num1 = num;
      List<ArtifactData> all = player_artifacts.FindAll((Predicate<ArtifactData>) (a => a.ArtifactParam.type == type));
      if (all == null || all.Count <= 0)
        return recommendArtifacts;
      List<ArtifactData> tmp = all.FindAll((Predicate<ArtifactData>) (data => this.mUnitJobArtifacts.Contains(data.ArtifactParam)));
      for (int i = 0; i < tmp.Count && num1 > 0; ++i)
      {
        if (recommendArtifacts.FindIndex((Predicate<ArtifactData>) (r => r.ArtifactParam.iname == tmp[i].ArtifactParam.iname)) < 0)
        {
          recommendArtifacts.Add(tmp[i]);
          --num1;
        }
      }
      if (num1 > 0)
      {
        tmp = all.FindAll((Predicate<ArtifactData>) (data => this.mUnitArtifacts.Contains(data.ArtifactParam)));
        for (int i = 0; i < tmp.Count && num1 > 0; ++i)
        {
          if (recommendArtifacts.FindIndex((Predicate<ArtifactData>) (r => r.ArtifactParam.iname == tmp[i].ArtifactParam.iname)) < 0)
          {
            recommendArtifacts.Add(tmp[i]);
            --num1;
          }
        }
      }
      return recommendArtifacts;
    }

    public List<ArtifactData> GetMasterAbilityArtifacts(
      List<ArtifactData> player_artifacts,
      ArtifactTypes type,
      int num)
    {
      List<ArtifactData> abilityArtifacts = new List<ArtifactData>();
      int num1 = num;
      List<ArtifactData> all = player_artifacts.FindAll((Predicate<ArtifactData>) (a => a.ArtifactParam.type == type));
      if (all == null || all.Count <= 0 || num1 <= 0)
        return abilityArtifacts;
      List<ArtifactData> tmp = all.FindAll((Predicate<ArtifactData>) (data => this.mAbilityArtifacts.Contains(data.ArtifactParam)));
      for (int i = 0; i < tmp.Count && num1 > 0; ++i)
      {
        if (abilityArtifacts.FindIndex((Predicate<ArtifactData>) (r => r.ArtifactParam.iname == tmp[i].ArtifactParam.iname)) < 0)
        {
          abilityArtifacts.Add(tmp[i]);
          --num1;
        }
      }
      return abilityArtifacts;
    }
  }
}
