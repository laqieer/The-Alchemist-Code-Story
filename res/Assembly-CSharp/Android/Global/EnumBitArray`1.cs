﻿// Decompiled with JetBrains decompiler
// Type: EnumBitArray`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
