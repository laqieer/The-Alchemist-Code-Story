// Decompiled with JetBrains decompiler
// Type: SRPG.RecommendedArtifactList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;

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
      List<RecommendedArtifactParam> recommendedArtifactParamList = new List<RecommendedArtifactParam>();
      if (this.mRecommendedArtifacts == null)
        return recommendedArtifactParamList;
      List<RecommendedArtifactParam> source = (List<RecommendedArtifactParam>) null;
      if (this.mRecommendedArtifacts.TryGetValue(unitData.UnitParam.iname, out source))
        recommendedArtifactParamList = source.Where<RecommendedArtifactParam>((Func<RecommendedArtifactParam, bool>) (param => param.CheckRecommendedForUnit(unitData))).ToList<RecommendedArtifactParam>();
      return recommendedArtifactParamList;
    }

    public List<ArtifactParam> GetRecommendedArtifacts(UnitData unitData, MasterParam masterParam)
    {
      List<RecommendedArtifactParam> recommendedArtifacts = this.GetRecommendedArtifacts(unitData);
      List<ArtifactParam> artifactParamList = new List<ArtifactParam>();
      if (recommendedArtifacts.Count <= 0)
        return artifactParamList;
      foreach (RecommendedArtifactParam recommendedArtifactParam in recommendedArtifacts)
      {
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
      return artifactParamList;
    }
  }
}
