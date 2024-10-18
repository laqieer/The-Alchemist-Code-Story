// Decompiled with JetBrains decompiler
// Type: SRPG.StatusParam
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
  public class StatusParam
  {
    public static readonly int MAX_STATUS = Enum.GetNames(typeof (StatusTypes)).Length;
    public OInt values_hp = (OInt) 0;
    public OShort[] values = new OShort[StatusParam.MAX_STATUS - 1];
    public static readonly ParamTypes[] ConvertParamTypes = new ParamTypes[14]
    {
      ParamTypes.Hp,
      ParamTypes.Mp,
      ParamTypes.MpIni,
      ParamTypes.Atk,
      ParamTypes.Def,
      ParamTypes.Mag,
      ParamTypes.Mnd,
      ParamTypes.Rec,
      ParamTypes.Dex,
      ParamTypes.Spd,
      ParamTypes.Cri,
      ParamTypes.Luk,
      ParamTypes.Mov,
      ParamTypes.Jmp
    };

    public int Length => StatusParam.MAX_STATUS;

    [IgnoreMember]
    public OInt this[StatusTypes type]
    {
      get => type == StatusTypes.Hp ? this.hp : (OInt) this.values[(int) (type - 1)];
      set
      {
        if (type == StatusTypes.Hp)
          this.hp = value;
        else
          this.values[(int) (type - 1)] = (OShort) value;
      }
    }

    public OInt hp
    {
      get => this.values_hp;
      set => this.values_hp = value;
    }

    public OShort mp
    {
      get => this.values[0];
      set => this.values[0] = value;
    }

    public OShort imp
    {
      get => this.values[1];
      set => this.values[1] = value;
    }

    public OShort atk
    {
      get => this.values[2];
      set => this.values[2] = value;
    }

    public OShort def
    {
      get => this.values[3];
      set => this.values[3] = value;
    }

    public OShort mag
    {
      get => this.values[4];
      set => this.values[4] = value;
    }

    public OShort mnd
    {
      get => this.values[5];
      set => this.values[5] = value;
    }

    public OShort rec
    {
      get => this.values[6];
      set => this.values[6] = value;
    }

    public OShort dex
    {
      get => this.values[7];
      set => this.values[7] = value;
    }

    public OShort spd
    {
      get => this.values[8];
      set => this.values[8] = value;
    }

    public OShort cri
    {
      get => this.values[9];
      set => this.values[9] = value;
    }

    public OShort luk
    {
      get => this.values[10];
      set => this.values[10] = value;
    }

    public OShort mov
    {
      get => this.values[11];
      set => this.values[11] = value;
    }

    public OShort jmp
    {
      get => this.values[12];
      set => this.values[12] = value;
    }

    public int total
    {
      get
      {
        int num = 0;
        for (int index = 0; index < this.values.Length; ++index)
          num += (int) this.values[index];
        return num + (int) this.values_hp;
      }
    }

    public void Clear()
    {
      this.values_hp = (OInt) 0;
      Array.Clear((Array) this.values, 0, this.values.Length);
    }

    public void CopyTo(StatusParam dsc)
    {
      if (dsc == null)
        return;
      for (int index = 0; index < this.values.Length; ++index)
        dsc.values[index] = this.values[index];
      dsc.values_hp = this.values_hp;
    }

    public void Add(StatusParam src)
    {
      if (src == null)
        return;
      for (int index = 0; index < this.values.Length; ++index)
      {
        if ((int) this.values[index] + (int) src.values[index] > (int) BaseStatus.SHORT_PARAM_MAX)
          this.values[index] = (OShort) BaseStatus.SHORT_PARAM_MAX;
        else if ((int) this.values[index] + (int) src.values[index] < (int) BaseStatus.SHORT_PARAM_MIN)
        {
          this.values[index] = (OShort) BaseStatus.SHORT_PARAM_MIN;
        }
        else
        {
          ref OShort local = ref this.values[index];
          local = (OShort) ((int) local + (int) src.values[index]);
        }
      }
      StatusParam statusParam = this;
      statusParam.values_hp = (OInt) ((int) statusParam.values_hp + (int) src.values_hp);
    }

    public void Sub(StatusParam src)
    {
      if (src == null)
        return;
      for (int index = 0; index < this.values.Length; ++index)
      {
        if ((int) this.values[index] - (int) src.values[index] > (int) BaseStatus.SHORT_PARAM_MAX)
          this.values[index] = (OShort) BaseStatus.SHORT_PARAM_MAX;
        else if ((int) this.values[index] - (int) src.values[index] < (int) BaseStatus.SHORT_PARAM_MIN)
        {
          this.values[index] = (OShort) BaseStatus.SHORT_PARAM_MIN;
        }
        else
        {
          ref OShort local = ref this.values[index];
          local = (OShort) ((int) local - (int) src.values[index]);
        }
      }
      StatusParam statusParam = this;
      statusParam.values_hp = (OInt) ((int) statusParam.values_hp - (int) src.values_hp);
    }

    public void AddRate(StatusParam src)
    {
      for (int index = 0; index < this.values.Length; ++index)
      {
        if ((int) this.values[index] + (int) this.values[index] * (int) src.values[index] / 100 > (int) BaseStatus.SHORT_PARAM_MAX)
          this.values[index] = (OShort) BaseStatus.SHORT_PARAM_MAX;
        else if ((int) this.values[index] + (int) this.values[index] * (int) src.values[index] / 100 < (int) BaseStatus.SHORT_PARAM_MIN)
        {
          this.values[index] = (OShort) BaseStatus.SHORT_PARAM_MIN;
        }
        else
        {
          ref OShort local = ref this.values[index];
          local = (OShort) ((int) local + (int) this.values[index] * (int) src.values[index] / 100);
        }
      }
      StatusParam statusParam = this;
      statusParam.values_hp = (OInt) ((int) statusParam.values_hp + (int) this.values_hp * (int) src.values_hp / 100);
    }

    public void SubRateRoundDown(long percent)
    {
      for (int index = 0; index < this.values.Length; ++index)
      {
        int num = GameUtility.CalcSubRateRoundDown((long) (int) this.values[index], percent);
        this.values[index] = (OShort) (short) Mathf.Clamp(num, (int) BaseStatus.SHORT_PARAM_MIN, (int) BaseStatus.SHORT_PARAM_MAX);
      }
      this.values_hp = (OInt) GameUtility.CalcSubRateRoundDown((long) (int) this.values_hp, percent);
    }

    public void ReplceHighest(StatusParam comp)
    {
      for (int index = 0; index < this.values.Length; ++index)
      {
        if ((int) this.values[index] < (int) comp.values[index])
          this.values[index] = comp.values[index];
      }
      if ((int) this.values_hp >= (int) comp.values_hp)
        return;
      this.values_hp = comp.values_hp;
    }

    public void ReplceLowest(StatusParam comp)
    {
      for (int index = 0; index < this.values.Length; ++index)
      {
        if ((int) this.values[index] > (int) comp.values[index])
          this.values[index] = comp.values[index];
      }
      if ((int) this.values_hp <= (int) comp.values_hp)
        return;
      this.values_hp = comp.values_hp;
    }

    public void ChoiceHighest(StatusParam scale, StatusParam base_status)
    {
      for (int index = 0; index < this.values.Length; ++index)
      {
        if ((int) this.values[index] < (int) scale.values[index] * (int) base_status.values[index] / 100)
          this.values[index] = (OShort) 0;
        else
          scale.values[index] = (OShort) 0;
      }
      if ((int) this.values_hp < (int) scale.values_hp * (int) base_status.values_hp / 100)
        this.values_hp = (OInt) 0;
      else
        scale.values_hp = (OInt) 0;
    }

    public void ChoiceLowest(StatusParam scale, StatusParam base_status)
    {
      for (int index = 0; index < this.values.Length; ++index)
      {
        if ((int) this.values[index] > (int) scale.values[index] * (int) base_status.values[index] / 100)
          this.values[index] = (OShort) 0;
        else
          scale.values[index] = (OShort) 0;
      }
      if ((int) this.values_hp > (int) scale.values_hp * (int) base_status.values_hp / 100)
        this.values_hp = (OInt) 0;
      else
        scale.values_hp = (OInt) 0;
    }

    public void ApplyMinVal()
    {
      for (int index = 0; index < this.values.Length; ++index)
      {
        switch (index)
        {
          case 1:
          case 6:
            continue;
          case 8:
            this.values[index] = (OShort) Math.Max((short) this.values[index], (short) 1);
            continue;
          default:
            this.values[index] = (OShort) Math.Max((short) this.values[index], (short) 0);
            continue;
        }
      }
      this.values_hp = (OInt) Math.Max((int) this.values_hp, 1);
    }

    public void AddConvRate(StatusParam scale, StatusParam base_status)
    {
      for (int index = 0; index < this.values.Length; ++index)
      {
        ref OShort local = ref this.values[index];
        local = (OShort) ((int) local + (int) scale.values[index] * (int) base_status.values[index] / 100);
      }
      StatusParam statusParam = this;
      statusParam.values_hp = (OInt) ((int) statusParam.values_hp + (int) scale.values_hp * (int) base_status.values_hp / 100);
    }

    public void SubConvRate(StatusParam scale, StatusParam base_status)
    {
      for (int index = 0; index < this.values.Length; ++index)
      {
        ref OShort local = ref this.values[index];
        local = (OShort) ((int) local - (int) scale.values[index] * (int) base_status.values[index] / 100);
      }
      StatusParam statusParam = this;
      statusParam.values_hp = (OInt) ((int) statusParam.values_hp - (int) scale.values_hp * (int) base_status.values_hp / 100);
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
      StatusParam statusParam = this;
      statusParam.values_hp = (OInt) ((int) statusParam.values_hp * mul_val);
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
      StatusParam statusParam = this;
      statusParam.values_hp = (OInt) ((int) statusParam.values_hp / div_val);
    }

    public void Swap(StatusParam src, bool is_rev)
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
      GameUtility.swap<OInt>(ref this.values_hp, ref src.values_hp);
      if (!is_rev)
        return;
      StatusParam statusParam1 = this;
      statusParam1.values_hp = (OInt) ((int) statusParam1.values_hp * -1);
      StatusParam statusParam2 = src;
      statusParam2.values_hp = (OInt) ((int) statusParam2.values_hp * -1);
    }

    public ParamTypes GetParamTypes(int index) => StatusParam.ConvertParamTypes[index];
  }
}
