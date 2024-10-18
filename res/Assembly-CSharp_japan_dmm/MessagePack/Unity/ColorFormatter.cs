// Decompiled with JetBrains decompiler
// Type: MessagePack.Unity.ColorFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Formatters;
using System;
using UnityEngine;

#nullable disable
namespace MessagePack.Unity
{
  public sealed class ColorFormatter : IMessagePackFormatter<Color>, IMessagePackFormatter
  {
    public int Serialize(
      ref byte[] bytes,
      int offset,
      Color value,
      IFormatterResolver formatterResolver)
    {
      int num = offset;
      offset += MessagePackBinary.WriteFixedArrayHeaderUnsafe(ref bytes, offset, 4);
      offset += MessagePackBinary.WriteSingle(ref bytes, offset, value.r);
      offset += MessagePackBinary.WriteSingle(ref bytes, offset, value.g);
      offset += MessagePackBinary.WriteSingle(ref bytes, offset, value.b);
      offset += MessagePackBinary.WriteSingle(ref bytes, offset, value.a);
      return offset - num;
    }

    public Color Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      int num1 = !MessagePackBinary.IsNil(bytes, offset) ? offset : throw new InvalidOperationException("typecode is null, struct not supported");
      int num2 = MessagePackBinary.ReadArrayHeader(bytes, offset, out readSize);
      offset += readSize;
      float num3 = 0.0f;
      float num4 = 0.0f;
      float num5 = 0.0f;
      float num6 = 0.0f;
      for (int index = 0; index < num2; ++index)
      {
        switch (index)
        {
          case 0:
            num3 = MessagePackBinary.ReadSingle(bytes, offset, out readSize);
            break;
          case 1:
            num4 = MessagePackBinary.ReadSingle(bytes, offset, out readSize);
            break;
          case 2:
            num5 = MessagePackBinary.ReadSingle(bytes, offset, out readSize);
            break;
          case 3:
            num6 = MessagePackBinary.ReadSingle(bytes, offset, out readSize);
            break;
          default:
            readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
            break;
        }
        offset += readSize;
      }
      readSize = offset - num1;
      Color color;
      // ISSUE: explicit constructor call
      ((Color) ref color).\u002Ector(num3, num4, num5, num6);
      return color;
    }
  }
}
