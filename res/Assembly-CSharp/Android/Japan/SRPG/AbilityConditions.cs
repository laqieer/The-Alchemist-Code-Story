// Decompiled with JetBrains decompiler
// Type: SRPG.AbilityConditions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using System.Text;

namespace SRPG
{
  public class AbilityConditions
  {
    public List<UnitParam> m_CondsUnits = new List<UnitParam>();
    public List<JobParam> m_CondsJobs = new List<JobParam>();
    public AbilityParam m_AbilityParam;
    private string m_CondsBirth;
    private ESex m_CondsSex;
    private EElement m_CondsElem;

    private bool enableCondsUnits
    {
      get
      {
        return this.m_CondsUnits.Count > 0;
      }
    }

    private bool enableCondsJobs
    {
      get
      {
        return this.m_CondsJobs.Count > 0;
      }
    }

    private bool enableCondsBirth
    {
      get
      {
        return !string.IsNullOrEmpty(this.m_CondsBirth);
      }
    }

    private bool enableCondsSex
    {
      get
      {
        return this.m_CondsSex != ESex.Unknown;
      }
    }

    private bool enableCondsElem
    {
      get
      {
        return this.m_CondsElem != EElement.None;
      }
    }

    public void Setup(AbilityParam abil, MasterParam master)
    {
      this.m_AbilityParam = abil;
      this.m_CondsUnits = this.m_AbilityParam.FindConditionUnitParams(master);
      this.m_CondsJobs = this.m_AbilityParam.FindConditionJobParams(master);
      this.m_CondsBirth = this.m_AbilityParam.condition_birth;
      this.m_CondsSex = this.m_AbilityParam.condition_sex;
      this.m_CondsElem = this.m_AbilityParam.condition_element;
    }

    private static string SexToString(ESex sex)
    {
      return LocalizedText.Get(string.Format("sys.SEX_{0}", (object) sex));
    }

    private static string ElementToString(EElement element)
    {
      return LocalizedText.Get(string.Format("sys.ABILITY_CONDS_ELEMENT_{0}", (object) element));
    }

    private static string InternalMakeConditionsText(params string[] arg)
    {
      return LocalizedText.Get("sys.ABILITY_CONDS_TEXT_FORMAT", new object[1]{ (object) string.Join(string.Empty, arg) });
    }

    public string MakeConditionsText()
    {
      StringBuilder stringBuilder = new StringBuilder();
      string empty = string.Empty;
      if (this.enableCondsSex)
      {
        string str = AbilityConditions.SexToString(this.m_CondsSex);
        empty += LocalizedText.Get("sys.ABILITY_CONDS_SEX", new object[1]
        {
          (object) str
        });
      }
      if (this.enableCondsElem)
      {
        string str = AbilityConditions.ElementToString(this.m_CondsElem);
        empty += LocalizedText.Get("sys.ABILITY_CONDS_ELEMENT", new object[1]
        {
          (object) str
        });
      }
      if (this.enableCondsBirth)
        empty += LocalizedText.Get("sys.ABILITY_CONDS_BIRTH", new object[1]
        {
          (object) this.m_CondsBirth
        });
      if (!this.enableCondsSex && !this.enableCondsElem && (!this.enableCondsBirth && !this.enableCondsUnits) && !this.enableCondsJobs)
      {
        string str = LocalizedText.Get("sys.ABILITY_CONDS_NO_CONDS");
        stringBuilder.Append(str);
        stringBuilder.Append("\n");
      }
      else
      {
        if (this.enableCondsUnits)
        {
          if (this.enableCondsJobs)
          {
            foreach (UnitParam condsUnit in this.m_CondsUnits)
            {
              foreach (JobParam condsJob in this.m_CondsJobs)
              {
                string str = AbilityConditions.InternalMakeConditionsText(LocalizedText.Get("sys.ABILITY_CONDS_UNIT", new object[1]{ (object) condsUnit.name }), LocalizedText.Get("sys.ABILITY_CONDS_JOB", new object[1]{ (object) condsJob.name }));
                stringBuilder.Append(str);
                stringBuilder.Append("\n");
              }
            }
          }
          else
          {
            foreach (UnitParam condsUnit in this.m_CondsUnits)
            {
              string str = AbilityConditions.InternalMakeConditionsText(LocalizedText.Get("sys.ABILITY_CONDS_UNIT", new object[1]{ (object) condsUnit.name }));
              stringBuilder.Append(str);
              stringBuilder.Append("\n");
            }
          }
        }
        else if (this.enableCondsJobs)
        {
          foreach (JobParam condsJob in this.m_CondsJobs)
          {
            string str = AbilityConditions.InternalMakeConditionsText(LocalizedText.Get("sys.ABILITY_CONDS_JOB", new object[1]{ (object) condsJob.name }));
            stringBuilder.Append(str);
            stringBuilder.Append("\n");
          }
        }
        if ((this.enableCondsUnits || this.enableCondsJobs) && !string.IsNullOrEmpty(empty))
          stringBuilder.Append(LocalizedText.Get("sys.ABILITY_CONDS_AND"));
        stringBuilder.Append(LocalizedText.Get("sys.ABILITY_CONDS_END", new object[1]
        {
          (object) empty
        }));
      }
      return stringBuilder.ToString();
    }
  }
}
