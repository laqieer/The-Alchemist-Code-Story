// Decompiled with JetBrains decompiler
// Type: MessagePack.Unity.Color32Formatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Formatters;
using System;
using UnityEngine;

#nullable disable
namespace MessagePack.Unity
{
  public sealed class Color32Formatter : IMessagePackFormatter<Color32>, IMessagePackFormatter
  {
    public int Serialize(
      ref byte[] bytes,
      int offset,
      Color32 value,
      IFormatterResolver formatterResolver)
    {
      int num = offset;
      offset += MessagePackBinary.WriteFixedArrayHeaderUnsafe(ref bytes, offset, 4);
      offset += MessagePackBinary.WriteByte(ref bytes, offset, value.r);
      offset += MessagePackBinary.WriteByte(ref bytes, offset, value.g);
      offset += MessagePackBinary.WriteByte(ref bytes, offset, value.b);
      offset += MessagePackBinary.WriteByte(ref bytes, offset, value.a);
      return offset - num;
    }

    public Color32 Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      int num1 = !MessagePackBinary.IsNil(bytes, offset) ? offset : throw new InvalidOperationException("typecode is null, struct not supported");
      int num2 = MessagePackBinary.ReadArrayHeader(bytes, offset, out readSize);
      offset += readSize;
      byte num3 = 0;
      byte num4 = 0;
      byte num5 = 0;
      byte num6 = 0;
      for (int index = 0; index < num2; ++index)
      {
        switch (index)
        {
          case 0:
            num3 = MessagePackBinary.ReadByte(bytes, offset, out readSize);
            break;
          case 1:
            num4 = MessagePackBinary.ReadByte(bytes, offset, out readSize);
            break;
          case 2:
            num5 = MessagePackBinary.ReadByte(bytes, offset, out readSize);
            break;
          case 3:
            num6 = MessagePackBinary.ReadByte(bytes, offset, out readSize);
            break;
          default:
            readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
            break;
        }
        offset += readSize;
      }
      readSize = offset - num1;
      Color32 color32;
      // ISSUE: explicit constructor call
      ((Color32) ref color32).\u002Ector(num3, num4, num5, num6);
      color32.r = num3;
      color32.g = num4;
      color32.b = num5;
      color32.a = num6;
      return color32;
    }
  }
}
