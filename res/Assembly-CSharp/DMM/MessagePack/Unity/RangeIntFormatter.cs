// Decompiled with JetBrains decompiler
// Type: MessagePack.Unity.RangeIntFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Formatters;
using System;
using UnityEngine;

#nullable disable
namespace MessagePack.Unity
{
  public sealed class RangeIntFormatter : IMessagePackFormatter<RangeInt>, IMessagePackFormatter
  {
    public int Serialize(
      ref byte[] bytes,
      int offset,
      RangeInt value,
      IFormatterResolver formatterResolver)
    {
      int num = offset;
      offset += MessagePackBinary.WriteFixedArrayHeaderUnsafe(ref bytes, offset, 2);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.start);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.length);
      return offset - num;
    }

    public RangeInt Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      int num1 = !MessagePackBinary.IsNil(bytes, offset) ? offset : throw new InvalidOperationException("typecode is null, struct not supported");
      int num2 = MessagePackBinary.ReadArrayHeader(bytes, offset, out readSize);
      offset += readSize;
      int num3 = 0;
      int num4 = 0;
      for (int index = 0; index < num2; ++index)
      {
        switch (index)
        {
          case 0:
            num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
            break;
          case 1:
            num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
            break;
          default:
            readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
            break;
        }
        offset += readSize;
      }
      readSize = offset - num1;
      RangeInt rangeInt;
      // ISSUE: explicit constructor call
      ((RangeInt) ref rangeInt).\u002Ector(num3, num4);
      rangeInt.start = num3;
      rangeInt.length = num4;
      return rangeInt;
    }
  }
}
