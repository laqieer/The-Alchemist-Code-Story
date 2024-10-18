// Decompiled with JetBrains decompiler
// Type: SRPG.MapEffectParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class MapEffectParam
  {
    private int mIndex;
    private string mIname;
    private string mName;
    private string mExpr;
    private List<string> mValidSkillLists = new List<string>();
    private static int CurrentIndex;
    private static Dictionary<string, List<JobParam>> mHaveJobDict;

    public int Index => this.mIndex;

    public string Iname => this.mIname;

    public string Name => this.mName;

    public string Expr => this.mExpr;

    public List<string> ValidSkillLists => this.mValidSkillLists;

    public void Deserialize(JSON_MapEffectParam json)
    {
      if (json == null)
        return;
      this.mIndex = ++MapEffectParam.CurrentIndex;
      this.mIname = json.iname;
      this.mName = json.name;
      this.mExpr = json.expr;
      this.mValidSkillLists.Clear();
      if (json.skills == null)
        return;
      foreach (string skill in json.skills)
        this.mValidSkillLists.Add(skill);
    }

    public bool IsValidSkill(string skill)
    {
      return !string.IsNullOrEmpty(skill) && this.mValidSkillLists.Contains(skill);
    }

    public static List<JobParam> GetHaveJobLists(string skill_iname)
    {
      List<JobParam> haveJobLists = new List<JobParam>();
      if (string.IsNullOrEmpty(skill_iname) || MapEffectParam.mHaveJobDict == null || !MapEffectParam.mHaveJobDict.ContainsKey(skill_iname))
        return haveJobLists;
      haveJobLists = MapEffectParam.mHaveJobDict[skill_iname];
      return haveJobLists;
    }

    public static List<MapEffectParam> GetHaveMapEffectLists(string skill_iname)
    {
      List<MapEffectParam> haveMapEffectLists = new List<MapEffectParam>();
      if (string.IsNullOrEmpty(skill_iname))
        return haveMapEffectLists;
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if (!Object.op_Implicit((Object) instanceDirect) || instanceDirect.MapEffectParam == null)
        return haveMapEffectLists;
      foreach (MapEffectParam mapEffectParam in instanceDirect.MapEffectParam)
      {
        if (mapEffectParam.ValidSkillLists.Contains(skill_iname))
          haveMapEffectLists.Add(mapEffectParam);
      }
      return haveMapEffectLists;
    }

    public static bool IsMakeHaveJobLists() => MapEffectParam.mHaveJobDict != null;

    public static void MakeHaveJobLists()
    {
      MapEffectParam.mHaveJobDict = new Dictionary<string, List<JobParam>>();
    }

    public static void AddHaveJob(string skill_iname, JobParam job_param)
    {
      if (MapEffectParam.mHaveJobDict == null)
        MapEffectParam.MakeHaveJobLists();
      if (!MapEffectParam.mHaveJobDict.ContainsKey(skill_iname))
      {
        MapEffectParam.mHaveJobDict.Add(skill_iname, new List<JobParam>((IEnumerable<JobParam>) new JobParam[1]
        {
          job_param
        }));
      }
      else
      {
        if (MapEffectParam.mHaveJobDict[skill_iname].Contains(job_param))
          return;
        MapEffectParam.mHaveJobDict[skill_iname].Add(job_param);
      }
    }
  }
}
