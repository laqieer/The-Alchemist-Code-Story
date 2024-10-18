// Decompiled with JetBrains decompiler
// Type: SRPG.RecommendedArtifactList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace SRPG
{
  public class RecommendedArtifactList
  {
    private Dictionary<string, List<RecommendedArtifactParam>> mRecommendedArtifacts;

    public void Deserialize(JSON_RecommendedArtifactParam[] json)
    {
      if (json == null || json.Length <= 0)
        return;
      this.mRecommendedArtifacts = new Dictionary<string, List<RecommendedArtifactParam>>();
      for (int index = 0; index < json.Length; ++index)
      {
        RecommendedArtifactParam recommendedArtifactParam = new RecommendedArtifactParam();
        recommendedArtifactParam.Deserialize(json[index]);
        List<RecommendedArtifactParam> recommendedArtifactParamList = (List<RecommendedArtifactParam>) null;
        if (this.mRecommendedArtifacts.TryGetValue(recommendedArtifactParam.UnitIname, out recommendedArtifactParamList))
          recommendedArtifactParamList.Add(recommendedArtifactParam);
        else
          this.mRecommendedArtifacts.Add(recommendedArtifactParam.UnitIname, new List<RecommendedArtifactParam>()
          {
            recommendedArtifactParam
          });
      }
    }

    public List<RecommendedArtifactParam> GetRecommendedArtifacts(UnitData unitData)
    {
      List<RecommendedArtifactParam> recommendedArtifacts = new List<RecommendedArtifactParam>();
      if (this.mRecommendedArtifacts == null)
        return recommendedArtifacts;
      List<RecommendedArtifactParam> source = (List<RecommendedArtifactParam>) null;
      if (this.mRecommendedArtifacts.TryGetValue(unitData.UnitParam.iname, out source))
        recommendedArtifacts = source.Where<RecommendedArtifactParam>((Func<RecommendedArtifactParam, bool>) (param => param.CheckRecommendedForUnit(unitData))).ToList<RecommendedArtifactParam>();
      return recommendedArtifacts;
    }

    public RecommendArtifactParams GetRecommendedArtifacts(
      UnitData unitData,
      MasterParam masterParam)
    {
      List<RecommendedArtifactParam> recommendedArtifacts1 = this.GetRecommendedArtifacts(unitData);
      RecommendArtifactParams recommendedArtifacts2 = new RecommendArtifactParams();
      if (recommendedArtifacts1.Count <= 0)
        return recommendedArtifacts2;
      foreach (RecommendedArtifactParam recommendedArtifactParam in recommendedArtifacts1)
      {
        List<ArtifactParam> artifactParamList = !string.IsNullOrEmpty(recommendedArtifactParam.JobIname) ? recommendedArtifacts2.UnitJobArtifacts : recommendedArtifacts2.UnitArtifacts;
        foreach (string artifactIname in recommendedArtifactParam.ArtifactInames)
        {
          string artifact_iname = artifactIname;
          if (artifactParamList.FindIndex((Predicate<ArtifactParam>) (ap => ap.iname == artifact_iname)) == -1)
          {
            ArtifactParam artifactParam = masterParam.GetArtifactParam(artifact_iname);
            artifactParamList.Add(artifactParam);
          }
        }
      }
      return recommendedArtifacts2;
    }
  }
}
