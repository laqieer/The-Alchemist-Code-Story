// Decompiled with JetBrains decompiler
// Type: SRPG.ElementParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;
using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  public class ElementParam
  {
    public static readonly int MAX_ELEMENT = Enum.GetNames(typeof (EElement)).Length;
    public short[] values = new short[ElementParam.MAX_ELEMENT];
    public static readonly ParamTypes[] ConvertAssistParamTypes = new ParamTypes[7]
    {
      ParamTypes.None,
      ParamTypes.Assist_Fire,
      ParamTypes.Assist_Water,
      ParamTypes.Assist_Wind,
      ParamTypes.Assist_Thunder,
      ParamTypes.Assist_Shine,
      ParamTypes.Assist_Dark
    };
    public static readonly ParamTypes[] ConvertResistParamTypes = new ParamTypes[7]
    {
      ParamTypes.None,
      ParamTypes.Resist_Fire,
      ParamTypes.Resist_Water,
      ParamTypes.Resist_Wind,
      ParamTypes.Resist_Thunder,
      ParamTypes.Resist_Shine,
      ParamTypes.Resist_Dark
    };

    [IgnoreMember]
    public short this[EElement type]
    {
      get => this.values[(int) type];
      set => this.values[(int) type] = value;
    }

    public short fire
    {
      get => this.values[1];
      set => this.values[1] = value;
    }

    public short water
    {
      get => this.values[2];
      set => this.values[2] = value;
    }

    public short wind
    {
      get => this.values[3];
      set => this.values[3] = value;
    }

    public short thunder
    {
      get => this.values[4];
      set => this.values[4] = value;
    }

    public short shine
    {
      get => this.values[5];
      set => this.values[5] = value;
    }

    public short dark
    {
      get => this.values[6];
      set => this.values[6] = value;
    }

    public void Clear() => Array.Clear((Array) this.values, 0, this.values.Length);

    public void CopyTo(ElementParam dsc)
    {
      if (dsc == null)
        return;
      for (int index = 0; index < this.values.Length; ++index)
        dsc.values[index] = this.values[index];
    }

    public void Add(ElementParam src)
    {
      if (src == null)
        return;
      for (int index = 0; index < this.values.Length; ++index)
      {
        if ((int) this.values[index] + (int) src.values[index] > (int) BaseStatus.SHORT_PARAM_MAX)
          this.values[index] = BaseStatus.SHORT_PARAM_MAX;
        else if ((int) this.values[index] + (int) src.values[index] < (int) BaseStatus.SHORT_PARAM_MIN)
          this.values[index] = BaseStatus.SHORT_PARAM_MIN;
        else
          this.values[index] += src.values[index];
      }
    }

    public void Sub(ElementParam src)
    {
      if (src == null)
        return;
      for (int index = 0; index < this.values.Length; ++index)
      {
        if ((int) this.values[index] - (int) src.values[index] > (int) BaseStatus.SHORT_PARAM_MAX)
          this.values[index] = BaseStatus.SHORT_PARAM_MAX;
        else if ((int) this.values[index] - (int) src.values[index] < (int) BaseStatus.SHORT_PARAM_MIN)
          this.values[index] = BaseStatus.SHORT_PARAM_MIN;
        else
          this.values[index] -= src.values[index];
      }
    }

    public void AddRate(ElementParam src)
    {
      for (int index = 0; index < this.values.Length; ++index)
      {
        if ((int) this.values[index] + (int) this.values[index] * (int) src.values[index] / 100 > (int) BaseStatus.SHORT_PARAM_MAX)
          this.values[index] = BaseStatus.SHORT_PARAM_MAX;
        else if ((int) this.values[index] + (int) this.values[index] * (int) src.values[index] / 100 < (int) BaseStatus.SHORT_PARAM_MIN)
          this.values[index] = BaseStatus.SHORT_PARAM_MIN;
        else
          this.values[index] += (short) ((int) this.values[index] * (int) src.values[index] / 100);
      }
    }

    public void SubRateRoundDown(long percent)
    {
      for (int index = 0; index < this.values.Length; ++index)
      {
        int num = GameUtility.CalcSubRateRoundDown((long) this.values[index], percent);
        this.values[index] = (short) Mathf.Clamp(num, (int) BaseStatus.SHORT_PARAM_MIN, (int) BaseStatus.SHORT_PARAM_MAX);
      }
    }

    public void ReplceHighest(ElementParam comp)
    {
      for (int index = 0; index < this.values.Length; ++index)
      {
        if ((int) this.values[index] < (int) comp.values[index])
          this.values[index] = comp.values[index];
      }
    }

    public void ReplceLowest(ElementParam comp)
    {
      for (int index = 0; index < this.values.Length; ++index)
      {
        if ((int) this.values[index] > (int) comp.values[index])
          this.values[index] = comp.values[index];
      }
    }

    public void ChoiceHighest(ElementParam scale, ElementParam base_status)
    {
      for (int index = 0; index < this.values.Length; ++index)
      {
        if ((int) this.values[index] < (int) scale.values[index] * (int) base_status.values[index] / 100)
          this.values[index] = (short) 0;
        else
          scale.values[index] = (short) 0;
      }
    }

    public void ChoiceLowest(ElementParam scale, ElementParam base_status)
    {
      for (int index = 0; index < this.values.Length; ++index)
      {
        if ((int) this.values[index] > (int) scale.values[index] * (int) base_status.values[index] / 100)
          this.values[index] = (short) 0;
        else
          scale.values[index] = (short) 0;
      }
    }

    public void AddConvRate(ElementParam scale, ElementParam base_status)
    {
      for (int index = 0; index < this.values.Length; ++index)
        this.values[index] += (short) ((int) scale.values[index] * (int) base_status.values[index] / 100);
    }

    public void SubConvRate(ElementParam scale, ElementParam base_status)
    {
      for (int index = 0; index < this.values.Length; ++index)
        this.values[index] -= (short) ((int) scale.values[index] * (int) base_status.values[index] / 100);
    }

    public void Mul(int mul_val)
    {
      if (mul_val == 0)
        return;
      for (int index = 0; index < this.values.Length; ++index)
        this.values[index] = (short) ((int) this.values[index] * mul_val);
    }

    public void Div(int div_val)
    {
      if (div_val == 0)
        return;
      for (int index = 0; index < this.values.Length; ++index)
        this.values[index] = (short) ((int) this.values[index] / div_val);
    }

    public void Swap(ElementParam src, bool is_rev)
    {
      for (int index = 0; index < this.values.Length; ++index)
      {
        GameUtility.swap<short>(ref this.values[index], ref src.values[index]);
        if (is_rev)
        {
          this.values[index] *= (short) -1;
          src.values[index] *= (short) -1;
        }
      }
    }

    public ParamTypes GetAssistParamTypes(int index) => ElementParam.ConvertAssistParamTypes[index];

    public ParamTypes GetResistParamTypes(int index) => ElementParam.ConvertResistParamTypes[index];
  }
}
