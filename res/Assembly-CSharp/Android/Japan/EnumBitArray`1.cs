// Decompiled with JetBrains decompiler
// Type: EnumBitArray`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;

internal class EnumBitArray<EnumType>
{
  private BitArray m_BitArray;

  public EnumBitArray()
  {
    this.m_BitArray = new BitArray(Enum.GetValues(typeof (EnumType)).Length);
  }

  public void Set(EnumType type, bool value)
  {
    int index = this.ToIndex(type);
    if (index == -1)
      return;
    this.m_BitArray.Set(index, value);
  }

  public bool Get(EnumType type)
  {
    int index = this.ToIndex(type);
    if (index != -1)
      return this.m_BitArray.Get(index);
    return false;
  }

  public void SetAll(bool value)
  {
    this.m_BitArray.SetAll(value);
  }

  private int ToIndex(EnumType type)
  {
    EnumType[] values = (EnumType[]) Enum.GetValues(typeof (EnumType));
    int num = -1;
    for (int index = 0; index < values.Length; ++index)
    {
      if (object.Equals((object) values[index], (object) type))
      {
        num = index;
        break;
      }
    }
    return num;
  }
}
