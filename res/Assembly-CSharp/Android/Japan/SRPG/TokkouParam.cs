// Decompiled with JetBrains decompiler
// Type: SRPG.TokkouParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace SRPG
{
  public class TokkouParam
  {
    public List<TokkouValue> values = new List<TokkouValue>();

    public int Count
    {
      get
      {
        return this.values.Count;
      }
    }

    public TokkouValue this[int index]
    {
      get
      {
        return this.values[index];
      }
      set
      {
        this.values[index] = value;
      }
    }

    public TokkouValue SearchTagMax(string tag)
    {
      if (tag == null)
        return (TokkouValue) null;
      TokkouValue tokkouValue = (TokkouValue) null;
      for (int index = 0; index < this.values.Count; ++index)
      {
        if (tag == (string) this.values[index].tag && (tokkouValue == null || (int) tokkouValue.value < (int) this.values[index].value))
          tokkouValue = this.values[index];
      }
      return tokkouValue;
    }

    public TokkouValue Find(OString new_tag)
    {
      return this.Find((string) new_tag);
    }

    public TokkouValue Find(string new_tag)
    {
      if (new_tag == null)
        return (TokkouValue) null;
      return this.values.Find((Predicate<TokkouValue>) (p => (string) p.tag == new_tag));
    }

    public void Add(TokkouParam param)
    {
      if (param == null)
        return;
      for (int index = 0; index < param.Count; ++index)
      {
        TokkouValue tokkouValue1 = this.Find(param[index].tag);
        if (tokkouValue1 != null)
        {
          TokkouValue tokkouValue2 = tokkouValue1;
          tokkouValue2.value = (OShort) ((int) tokkouValue2.value + (int) param[index].value);
        }
        else
          this.values.Add(param[index]);
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

    public void Clear()
    {
      this.values.Clear();
    }

    public void CopyTo(TokkouParam dsc)
    {
      dsc.values = new List<TokkouValue>(this.values.Count);
      for (int index = 0; index < this.values.Count; ++index)
        dsc.Add(this.values[index].Clone());
    }
  }
}
