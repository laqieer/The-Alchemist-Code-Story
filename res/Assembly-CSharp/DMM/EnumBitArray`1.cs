// Decompiled with JetBrains decompiler
// Type: EnumBitArray`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;
using System;
using System.Collections;

#nullable disable
public class EnumBitArray<EnumType>
{
  private BitArray mBitArray;

  public EnumBitArray() => this.mBitArray = new BitArray(Enum.GetValues(typeof (EnumType)).Length);

  private int ToIndex(EnumType type)
  {
    int index1 = -1;
    EnumType[] values = (EnumType[]) Enum.GetValues(typeof (EnumType));
    for (int index2 = 0; index2 < values.Length; ++index2)
    {
      if (object.Equals((object) values[index2], (object) type))
      {
        index1 = index2;
        break;
      }
    }
    return index1;
  }

  [IgnoreMember]
  public bool this[EnumType type]
  {
    get => this.Get(type);
    set => this.Set(type, value);
  }

  public bool Get(EnumType type)
  {
    int index = this.ToIndex(type);
    return index >= 0 && this.mBitArray.Get(index);
  }

  public void Set(EnumType type, bool value)
  {
    int index = this.ToIndex(type);
    if (index < 0)
      return;
    this.mBitArray.Set(index, value);
  }

  public void SetAll(bool value) => this.mBitArray.SetAll(value);
}
