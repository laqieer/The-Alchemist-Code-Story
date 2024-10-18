// Decompiled with JetBrains decompiler
// Type: SRPG.CondEffect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using MessagePack;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  public class CondEffect
  {
    public CondEffectParam param;
    public OInt turn;
    public OInt rate;
    public OInt value;

    public bool IsCurse => this.param != null && (int) this.param.curse != 0;

    public static CondEffect CreateCondEffect(CondEffectParam param, int rank, int rankcap)
    {
      if (param == null || param.conditions == null || param.conditions.Length == 0)
        return (CondEffect) null;
      CondEffect condEffect = new CondEffect();
      condEffect.param = param;
      condEffect.UpdateCurrentValues(rank, rankcap);
      return condEffect;
    }

    public void UpdateCurrentValues(int rank, int rankcap)
    {
      if (this.param == null || this.param.conditions == null || this.param.conditions.Length == 0)
      {
        this.Clear();
      }
      else
      {
        this.rate = (OInt) this.GetRankValue(rank, rankcap, (int) this.param.rate_ini, (int) this.param.rate_max);
        this.turn = (OInt) this.GetRankValue(rank, rankcap, (int) this.param.turn_ini, (int) this.param.turn_max);
        this.value = (OInt) this.GetRankValue(rank, rankcap, (int) this.param.value_ini, (int) this.param.value_max);
      }
    }

    private int GetRankValue(int rank, int rankcap, int ini, int max)
    {
      int num1 = rankcap - 1;
      int num2 = rank - 1;
      if (ini == max || num2 < 1 || num1 < 1)
        return ini;
      if (num2 >= num1)
        return max;
      int num3 = (max - ini) * 100 / num1;
      return ini + num3 * num2 / 100;
    }

    public bool ContainsCondition(EUnitCondition condition)
    {
      if (this.param == null || this.param.conditions == null)
        return false;
      for (int index = 0; index < this.param.conditions.Length; ++index)
      {
        if (this.param.conditions[index] == condition)
          return true;
      }
      return false;
    }

    private void Clear()
    {
      this.param = (CondEffectParam) null;
      this.rate = (OInt) 0;
      this.turn = (OInt) 0;
      this.value = (OInt) 0;
    }

    public bool CheckEnableCondTarget(Unit target)
    {
      if (this.param == null)
        return false;
      bool flag1 = true;
      if (this.param.sex != ESex.Unknown)
        flag1 &= this.param.sex == target.UnitParam.sex;
      if (this.param.elem != EElement.None)
        flag1 &= this.param.elem == target.Element;
      if (!string.IsNullOrEmpty(this.param.job) && target.Job != null)
        flag1 &= this.param.job == target.Job.Param.origin;
      if (!string.IsNullOrEmpty(this.param.buki))
      {
        if (target.Job != null)
          flag1 &= this.param.buki == target.Job.Param.buki;
        else
          flag1 &= this.param.buki == target.UnitParam.dbuki;
      }
      if (!string.IsNullOrEmpty(this.param.birth))
        flag1 &= this.param.birth == (string) target.UnitParam.birth;
      if (this.param.tags != null)
      {
        List<string> stringList = new List<string>((IEnumerable<string>) this.param.tags);
        bool flag2 = false;
        string[] tags = target.GetTags();
        if (tags != null)
        {
          for (int index = 0; index < tags.Length; ++index)
          {
            if (stringList.Contains(tags[index]))
            {
              flag2 = true;
              break;
            }
          }
        }
        flag1 &= flag2;
      }
      if (!string.IsNullOrEmpty(this.param.un_group))
      {
        UnitGroupParam unitGroup = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetUnitGroup(this.param.un_group);
        if (unitGroup != null)
          flag1 &= unitGroup.IsInGroup(target.UnitParam.iname);
      }
      if (this.param.custom_targets != null)
        flag1 &= BuffEffectParam.CheckCustomTarget(this.param.custom_targets, target, this.param.cond);
      return flag1;
    }
  }
}
