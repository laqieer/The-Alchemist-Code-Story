// Decompiled with JetBrains decompiler
// Type: SRPG.RecommendedArtifactParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

namespace SRPG
{
  public class RecommendedArtifactParam
  {
    private string m_UnitIname;
    private string m_JobIname;
    private List<string> m_ArtifactInames;

    public string UnitIname
    {
      get
      {
        return this.m_UnitIname;
      }
    }

    public string JobIname
    {
      get
      {
        return this.m_JobIname;
      }
    }

    public string[] ArtifactInames
    {
      get
      {
        return this.m_ArtifactInames.ToArray();
      }
    }

    public void Deserialize(JSON_RecommendedArtifactParam param)
    {
      this.m_UnitIname = param.unit_iname;
      this.m_JobIname = param.job_iname;
      this.m_ArtifactInames = new List<string>();
      if (!string.IsNullOrEmpty(param.arti1_iname))
        this.m_ArtifactInames.Add(param.arti1_iname);
      if (!string.IsNullOrEmpty(param.arti2_iname))
        this.m_ArtifactInames.Add(param.arti2_iname);
      if (string.IsNullOrEmpty(param.arti3_iname))
        return;
      this.m_ArtifactInames.Add(param.arti3_iname);
    }

    public bool CheckRecommendedForUnit(UnitData unitData)
    {
      return !string.IsNullOrEmpty(this.m_UnitIname) && !(this.m_UnitIname != unitData.UnitParam.iname) && (string.IsNullOrEmpty(this.m_JobIname) || this.m_JobIname == unitData.CurrentJob.Param.iname);
    }
  }
}
