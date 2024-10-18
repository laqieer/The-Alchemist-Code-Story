// Decompiled with JetBrains decompiler
// Type: SRPG.AbilityConditions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using System.Text;

#nullable disable
namespace SRPG
{
  public class AbilityConditions
  {
    public AbilityParam m_AbilityParam;
    public List<UnitParam> m_CondsUnits = new List<UnitParam>();
    public List<JobParam> m_CondsJobs = new List<JobParam>();
    private string m_CondsBirth;
    private ESex m_CondsSex;
    private EElement m_CondsElem;

    private bool enableCondsUnits => this.m_CondsUnits.Count > 0;

    private bool enableCondsJobs => this.m_CondsJobs.Count > 0;

    private bool enableCondsBirth => !string.IsNullOrEmpty(this.m_CondsBirth);

    private bool enableCondsSex => this.m_CondsSex != ESex.Unknown;

    private bool enableCondsElem => this.m_CondsElem != EElement.None;

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
      return LocalizedText.Get(string.Format("sys.SEX_{0}", (object) (int) sex));
    }

    private static string ElementToString(EElement element)
    {
      return LocalizedText.Get(string.Format("sys.ABILITY_CONDS_ELEMENT_{0}", (object) (int) element));
    }

    private static string InternalMakeConditionsText(params string[] arg)
    {
      return LocalizedText.Get("sys.ABILITY_CONDS_TEXT_FORMAT", (object) string.Join(string.Empty, arg));
    }

    public string MakeConditionsText()
    {
      StringBuilder stringBuilder = new StringBuilder();
      string empty = string.Empty;
      if (this.enableCondsSex)
      {
        string str = AbilityConditions.SexToString(this.m_CondsSex);
        empty += LocalizedText.Get("sys.ABILITY_CONDS_SEX", (object) str);
      }
      if (this.enableCondsElem)
      {
        string str = AbilityConditions.ElementToString(this.m_CondsElem);
        empty += LocalizedText.Get("sys.ABILITY_CONDS_ELEMENT", (object) str);
      }
      if (this.enableCondsBirth)
        empty += LocalizedText.Get("sys.ABILITY_CONDS_BIRTH", (object) this.m_CondsBirth);
      if (!this.enableCondsSex && !this.enableCondsElem && !this.enableCondsBirth && !this.enableCondsUnits && !this.enableCondsJobs)
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
                string str = AbilityConditions.InternalMakeConditionsText(LocalizedText.Get("sys.ABILITY_CONDS_UNIT", (object) condsUnit.name), LocalizedText.Get("sys.ABILITY_CONDS_JOB", (object) condsJob.name));
                stringBuilder.Append(str);
                stringBuilder.Append("\n");
              }
            }
          }
          else
          {
            foreach (UnitParam condsUnit in this.m_CondsUnits)
            {
              string str = AbilityConditions.InternalMakeConditionsText(LocalizedText.Get("sys.ABILITY_CONDS_UNIT", (object) condsUnit.name));
              stringBuilder.Append(str);
              stringBuilder.Append("\n");
            }
          }
        }
        else if (this.enableCondsJobs)
        {
          foreach (JobParam condsJob in this.m_CondsJobs)
          {
            string str = AbilityConditions.InternalMakeConditionsText(LocalizedText.Get("sys.ABILITY_CONDS_JOB", (object) condsJob.name));
            stringBuilder.Append(str);
            stringBuilder.Append("\n");
          }
        }
        if ((this.enableCondsUnits || this.enableCondsJobs) && !string.IsNullOrEmpty(empty))
          stringBuilder.Append(LocalizedText.Get("sys.ABILITY_CONDS_AND"));
        stringBuilder.Append(LocalizedText.Get("sys.ABILITY_CONDS_END", (object) empty));
      }
      return stringBuilder.ToString();
    }
  }
}
