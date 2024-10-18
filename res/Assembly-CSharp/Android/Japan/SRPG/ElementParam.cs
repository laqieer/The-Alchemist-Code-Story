﻿// Decompiled with JetBrains decompiler
// Type: SRPG.ElementParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class ElementParam
  {
    public static readonly int MAX_ELEMENT = Enum.GetNames(typeof (EElement)).Length;
    public static readonly ParamTypes[] ConvertAssistParamTypes = new ParamTypes[7]{ ParamTypes.None, ParamTypes.Assist_Fire, ParamTypes.Assist_Water, ParamTypes.Assist_Wind, ParamTypes.Assist_Thunder, ParamTypes.Assist_Shine, ParamTypes.Assist_Dark };
    public static readonly ParamTypes[] ConvertResistParamTypes = new ParamTypes[7]{ ParamTypes.None, ParamTypes.Resist_Fire, ParamTypes.Resist_Water, ParamTypes.Resist_Wind, ParamTypes.Resist_Thunder, ParamTypes.Resist_Shine, ParamTypes.Resist_Dark };
    public OShort[] values = new OShort[ElementParam.MAX_ELEMENT];

    public OShort this[EElement type]
    {
      get
      {
        return this.values[(int) type];
      }
      set
      {
        this.values[(int) type] = value;
      }
    }

    public OShort fire
    {
      get
      {
        return this.values[1];
      }
      set
      {
        this.values[1] = value;
      }
    }

    public OShort water
    {
      get
      {
        return this.values[2];
      }
      set
      {
        this.values[2] = value;
      }
    }

    public OShort wind
    {
      get
      {
        return this.values[3];
      }
      set
      {
        this.values[3] = value;
      }
    }

    public OShort thunder
    {
      get
      {
        return this.values[4];
      }
      set
      {
        this.values[4] = value;
      }
    }

    public OShort shine
    {
      get
      {
        return this.values[5];
      }
      set
      {
        this.values[5] = value;
      }
    }

    public OShort dark
    {
      get
      {
        return this.values[6];
      }
      set
      {
        this.values[6] = value;
      }
    }

    public void Clear()
    {
      Array.Clear((Array) this.values, 0, this.values.Length);
    }

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
        ref OShort local = ref this.values[index];
        local = (OShort) ((int) local + (int) src.values[index]);
      }
    }

    public void Sub(ElementParam src)
    {
      if (src == null)
        return;
      for (int index = 0; index < this.values.Length; ++index)
      {
        ref OShort local = ref this.values[index];
        local = (OShort) ((int) local - (int) src.values[index]);
      }
    }

    public void AddRate(ElementParam src)
    {
      for (int index = 0; index < this.values.Length; ++index)
      {
        ref OShort local = ref this.values[index];
        local = (OShort) ((int) local + (int) this.values[index] * (int) src.values[index] / 100);
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
          this.values[index] = (OShort) 0;
        else
          scale.values[index] = (OShort) 0;
      }
    }

    public void ChoiceLowest(ElementParam scale, ElementParam base_status)
    {
      for (int index = 0; index < this.values.Length; ++index)
      {
        if ((int) this.values[index] > (int) scale.values[index] * (int) base_status.values[index] / 100)
          this.values[index] = (OShort) 0;
        else
          scale.values[index] = (OShort) 0;
      }
    }

    public void AddConvRate(ElementParam scale, ElementParam base_status)
    {
      for (int index = 0; index < this.values.Length; ++index)
      {
        ref OShort local = ref this.values[index];
        local = (OShort) ((int) local + (int) scale.values[index] * (int) base_status.values[index] / 100);
      }
    }

    public void SubConvRate(ElementParam scale, ElementParam base_status)
    {
      for (int index = 0; index < this.values.Length; ++index)
      {
        ref OShort local = ref this.values[index];
        local = (OShort) ((int) local - (int) scale.values[index] * (int) base_status.values[index] / 100);
      }
    }

    public void Mul(int mul_val)
    {
      if (mul_val == 0)
        return;
      for (int index = 0; index < this.values.Length; ++index)
      {
        ref OShort local = ref this.values[index];
        local = (OShort) ((int) local * mul_val);
      }
    }

    public void Div(int div_val)
    {
      if (div_val == 0)
        return;
      for (int index = 0; index < this.values.Length; ++index)
      {
        ref OShort local = ref this.values[index];
        local = (OShort) ((int) local / div_val);
      }
    }

    public void Swap(ElementParam src, bool is_rev)
    {
      for (int index = 0; index < this.values.Length; ++index)
      {
        GameUtility.swap<OShort>(ref this.values[index], ref src.values[index]);
        if (is_rev)
        {
          ref OShort local1 = ref this.values[index];
          local1 = (OShort) ((int) local1 * -1);
          ref OShort local2 = ref src.values[index];
          local2 = (OShort) ((int) local2 * -1);
        }
      }
    }

    public ParamTypes GetAssistParamTypes(int index)
    {
      return ElementParam.ConvertAssistParamTypes[index];
    }

    public ParamTypes GetResistParamTypes(int index)
    {
      return ElementParam.ConvertResistParamTypes[index];
    }
  }
}
