// Decompiled with JetBrains decompiler
// Type: SRPG.TokkouParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  public class TokkouParam
  {
    public List<TokkouValue> values = new List<TokkouValue>();

    public int Count => this.values.Count;

    [IgnoreMember]
    public TokkouValue this[int index]
    {
      get => this.values[index];
      set => this.values[index] = value;
    }

    public TokkouValue SearchTagMax(string tag)
    {
      if (tag == null)
        return (TokkouValue) null;
      TokkouValue tokkouValue = (TokkouValue) null;
      for (int index = 0; index < this.values.Count; ++index)
      {
        if (tag == this.values[index].tag && (tokkouValue == null || (int) tokkouValue.value < (int) this.values[index].value))
          tokkouValue = this.values[index];
      }
      return tokkouValue;
    }

    public TokkouValue Find(OString new_tag) => this.Find((string) new_tag);

    public TokkouValue Find(string new_tag)
    {
      return new_tag == null ? (TokkouValue) null : this.values.Find((Predicate<TokkouValue>) (p => p.tag == new_tag));
    }

    public void Add(TokkouParam param)
    {
      if (param == null)
        return;
      for (int index = 0; index < param.Count; ++index)
      {
        TokkouValue tokkouValue = this.Find(param[index].tag);
        if (tokkouValue != null)
          tokkouValue.value += param[index].value;
        else
          this.values.Add(param[index]);
      }
    }

    public void SubRateRoundDown(long percent)
    {
      for (int index = 0; index < this.values.Count; ++index)
      {
        int num = GameUtility.CalcSubRateRoundDown((long) this.values[index].value, percent);
        this.values[index].value = (short) Mathf.Clamp(num, (int) BaseStatus.SHORT_PARAM_MIN, (int) BaseStatus.SHORT_PARAM_MAX);
      }
    }

    public void ReplceHighest(TokkouParam param)
    {
      if (param == null)
        return;
      for (int index = 0; index < param.Count; ++index)
      {
        TokkouValue tokkouValue = this.Find(param[index].tag);
        if (tokkouValue != null)
        {
          if ((int) tokkouValue.value < (int) param[index].value)
            tokkouValue.value = param[index].value;
        }
        else
          this.values.Add(param[index]);
      }
    }

    public void ReplceLowest(TokkouParam param)
    {
      if (param == null)
        return;
      for (int index = 0; index < param.Count; ++index)
      {
        TokkouValue tokkouValue = this.Find(param[index].tag);
        if (tokkouValue != null)
        {
          if ((int) tokkouValue.value > (int) param[index].value)
            tokkouValue.value = param[index].value;
        }
        else
          this.values.Add(param[index]);
      }
    }

    public void Add(TokkouValue value)
    {
      if (value == null)
        return;
      this.values.Add(value);
    }

    public void Clear() => this.values.Clear();

    public void CopyTo(TokkouParam dsc)
    {
      dsc.values = new List<TokkouValue>(this.values.Count);
      for (int index = 0; index < this.values.Count; ++index)
        dsc.Add(this.values[index].Clone());
    }
  }
}
