// Decompiled with JetBrains decompiler
// Type: SRPG.CollaboSkillParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;

namespace SRPG
{
  public class CollaboSkillParam
  {
    private List<CollaboSkillParam.LearnSkill> mLearnSkillLists = new List<CollaboSkillParam.LearnSkill>();
    private string mIname;
    private string mUnitIname;
    private string mAbilityIname;

    public string Iname
    {
      get
      {
        return this.mIname;
      }
    }

    public string UnitIname
    {
      get
      {
        return this.mUnitIname;
      }
    }

    public string AbilityIname
    {
      get
      {
        return this.mAbilityIname;
      }
    }

    public List<CollaboSkillParam.LearnSkill> LearnSkillLists
    {
      get
      {
        return this.mLearnSkillLists;
      }
    }

    public void Deserialize(JSON_CollaboSkillParam json)
    {
      if (json == null)
        return;
      this.mIname = json.iname;
      this.mUnitIname = json.uname;
      this.mAbilityIname = json.abid;
      this.mLearnSkillLists.Clear();
      if (json.lqs == null)
        return;
      foreach (string lq in json.lqs)
        this.mLearnSkillLists.Add(new CollaboSkillParam.LearnSkill(lq));
    }

    public static void UpdateCollaboSkill(List<CollaboSkillParam> csp_lists)
    {
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if ((UnityEngine.Object) instanceDirect == (UnityEngine.Object) null)
        return;
      foreach (CollaboSkillParam cspList1 in csp_lists)
      {
        for (int index = 0; index < cspList1.mLearnSkillLists.Count; ++index)
        {
          CollaboSkillParam.LearnSkill ls = cspList1.mLearnSkillLists[index];
          if (!string.IsNullOrEmpty(ls.QuestIname))
          {
            AbilityParam abilityParam1 = instanceDirect.MasterParam.GetAbilityParam(cspList1.AbilityIname);
            if (abilityParam1 == null)
              DebugUtility.LogError(string.Format("CollaboSkillParam/Deserialize AbilityParam not found. ability_iname={0}", (object) cspList1.mAbilityIname));
            else if (index < abilityParam1.skills.Length)
            {
              ls.SkillIname = abilityParam1.skills[index].iname;
              foreach (CollaboSkillParam cspList2 in csp_lists)
              {
                if (!(cspList2.mUnitIname == cspList1.mUnitIname))
                {
                  AbilityParam abilityParam2 = instanceDirect.MasterParam.GetAbilityParam(cspList2.AbilityIname);
                  if (abilityParam2 != null && new List<LearningSkill>((IEnumerable<LearningSkill>) abilityParam2.skills).Find((Predicate<LearningSkill>) (flgs => flgs.iname == ls.SkillIname)) != null)
                  {
                    ls.PartnerUnitIname = cspList2.UnitIname;
                    break;
                  }
                }
              }
            }
          }
        }
      }
    }

    public static string GetPartnerIname(string unit_iname, string skill_iname)
    {
      if (string.IsNullOrEmpty(unit_iname) || string.IsNullOrEmpty(skill_iname))
        return (string) null;
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if ((UnityEngine.Object) instanceDirect == (UnityEngine.Object) null)
        return (string) null;
      CollaboSkillParam collaboSkillParam = instanceDirect.MasterParam.CollaboSkills.Find((Predicate<CollaboSkillParam>) (fcs => fcs.UnitIname == unit_iname));
      if (collaboSkillParam == null)
      {
        DebugUtility.LogError(string.Format("CollaboSkillParam/GetPartnerIname CollaboSkillParam not found. unit_iname={0}", (object) unit_iname));
        return (string) null;
      }
      CollaboSkillParam.LearnSkill learnSkill = collaboSkillParam.mLearnSkillLists.Find((Predicate<CollaboSkillParam.LearnSkill>) (fls => fls.SkillIname == skill_iname));
      if (learnSkill != null)
        return learnSkill.PartnerUnitIname;
      DebugUtility.LogError(string.Format("CollaboSkillParam/GetPartnerIname LearnSkill not found. skill_iname={0}", (object) skill_iname));
      return (string) null;
    }

    public static List<string> GetLearnSkill(string quest_iname, string unit_iname)
    {
      List<string> stringList = new List<string>();
      if (string.IsNullOrEmpty(quest_iname) || string.IsNullOrEmpty(unit_iname))
        return stringList;
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if ((UnityEngine.Object) instanceDirect == (UnityEngine.Object) null)
        return stringList;
      CollaboSkillParam collaboSkillParam = instanceDirect.MasterParam.CollaboSkills.Find((Predicate<CollaboSkillParam>) (fcs => fcs.UnitIname == unit_iname));
      if (collaboSkillParam == null)
        return stringList;
      foreach (CollaboSkillParam.LearnSkill mLearnSkillList in collaboSkillParam.mLearnSkillLists)
      {
        if (!(mLearnSkillList.QuestIname != quest_iname))
          stringList.Add(mLearnSkillList.SkillIname);
      }
      return stringList;
    }

    public static List<CollaboSkillParam.Pair> GetPairLists()
    {
      List<CollaboSkillParam.Pair> pairList = new List<CollaboSkillParam.Pair>();
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if ((UnityEngine.Object) instanceDirect == (UnityEngine.Object) null)
        return pairList;
      foreach (CollaboSkillParam collaboSkill in instanceDirect.MasterParam.CollaboSkills)
      {
        CollaboSkillParam csp = collaboSkill;
        foreach (CollaboSkillParam.LearnSkill mLearnSkillList in csp.mLearnSkillLists)
        {
          CollaboSkillParam.LearnSkill ls = mLearnSkillList;
          if (pairList.Find((Predicate<CollaboSkillParam.Pair>) (tp =>
          {
            if (tp.UnitParam1.iname == csp.mUnitIname && tp.UnitParam2.iname == ls.PartnerUnitIname)
              return true;
            if (tp.UnitParam1.iname == ls.PartnerUnitIname)
              return tp.UnitParam2.iname == csp.mUnitIname;
            return false;
          })) == null)
            pairList.Add(new CollaboSkillParam.Pair(instanceDirect.MasterParam.GetUnitParam(csp.mUnitIname), instanceDirect.MasterParam.GetUnitParam(ls.PartnerUnitIname)));
        }
      }
      return pairList;
    }

    public static CollaboSkillParam.Pair IsLearnQuest(string quest_id)
    {
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if ((UnityEngine.Object) instanceDirect == (UnityEngine.Object) null)
        return (CollaboSkillParam.Pair) null;
      List<string> stringList = new List<string>();
      foreach (CollaboSkillParam collaboSkill in instanceDirect.MasterParam.CollaboSkills)
      {
        foreach (CollaboSkillParam.LearnSkill mLearnSkillList in collaboSkill.mLearnSkillLists)
        {
          if (mLearnSkillList.QuestIname == quest_id)
            stringList.Add(mLearnSkillList.PartnerUnitIname);
        }
      }
      if (stringList.Count == 2)
        return new CollaboSkillParam.Pair(instanceDirect.MasterParam.GetUnitParam(stringList[1]), instanceDirect.MasterParam.GetUnitParam(stringList[0]));
      return (CollaboSkillParam.Pair) null;
    }

    public class LearnSkill
    {
      public string QuestIname;
      public string SkillIname;
      public string PartnerUnitIname;

      public LearnSkill(string q_iname)
      {
        this.QuestIname = q_iname;
      }
    }

    public class Pair
    {
      public UnitParam UnitParam1;
      public UnitParam UnitParam2;

      public Pair(UnitParam u_param1, UnitParam u_param2)
      {
        this.UnitParam1 = u_param1;
        this.UnitParam2 = u_param2;
      }
    }
  }
}
