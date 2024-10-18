// Decompiled with JetBrains decompiler
// Type: MessagePack.Unity.Matrix4x4Formatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Formatters;
using System;
using UnityEngine;

#nullable disable
namespace MessagePack.Unity
{
  public sealed class Matrix4x4Formatter : IMessagePackFormatter<Matrix4x4>, IMessagePackFormatter
  {
    public int Serialize(
      ref byte[] bytes,
      int offset,
      Matrix4x4 value,
      IFormatterResolver formatterResolver)
    {
      int num = offset;
      offset += MessagePackBinary.WriteArrayHeader(ref bytes, offset, 16);
      offset += MessagePackBinary.WriteSingle(ref bytes, offset, value.m00);
      offset += MessagePackBinary.WriteSingle(ref bytes, offset, value.m10);
      offset += MessagePackBinary.WriteSingle(ref bytes, offset, value.m20);
      offset += MessagePackBinary.WriteSingle(ref bytes, offset, value.m30);
      offset += MessagePackBinary.WriteSingle(ref bytes, offset, value.m01);
      offset += MessagePackBinary.WriteSingle(ref bytes, offset, value.m11);
      offset += MessagePackBinary.WriteSingle(ref bytes, offset, value.m21);
      offset += MessagePackBinary.WriteSingle(ref bytes, offset, value.m31);
      offset += MessagePackBinary.WriteSingle(ref bytes, offset, value.m02);
      offset += MessagePackBinary.WriteSingle(ref bytes, offset, value.m12);
      offset += MessagePackBinary.WriteSingle(ref bytes, offset, value.m22);
      offset += MessagePackBinary.WriteSingle(ref bytes, offset, value.m32);
      offset += MessagePackBinary.WriteSingle(ref bytes, offset, value.m03);
      offset += MessagePackBinary.WriteSingle(ref bytes, offset, value.m13);
      offset += MessagePackBinary.WriteSingle(ref bytes, offset, value.m23);
      offset += MessagePackBinary.WriteSingle(ref bytes, offset, value.m33);
      return offset - num;
    }

    public Matrix4x4 Deserialize(
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
      float num7 = 0.0f;
      float num8 = 0.0f;
      float num9 = 0.0f;
      float num10 = 0.0f;
      float num11 = 0.0f;
      float num12 = 0.0f;
      float num13 = 0.0f;
      float num14 = 0.0f;
      float num15 = 0.0f;
      float num16 = 0.0f;
      float num17 = 0.0f;
      float num18 = 0.0f;
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
          case 4:
            num7 = MessagePackBinary.ReadSingle(bytes, offset, out readSize);
            break;
          case 5:
            num8 = MessagePackBinary.ReadSingle(bytes, offset, out readSize);
            break;
          case 6:
            num9 = MessagePackBinary.ReadSingle(bytes, offset, out readSize);
            break;
          case 7:
            num10 = MessagePackBinary.ReadSingle(bytes, offset, out readSize);
            break;
          case 8:
            num11 = MessagePackBinary.ReadSingle(bytes, offset, out readSize);
            break;
          case 9:
            num12 = MessagePackBinary.ReadSingle(bytes, offset, out readSize);
            break;
          case 10:
            num13 = MessagePackBinary.ReadSingle(bytes, offset, out readSize);
            break;
          case 11:
            num14 = MessagePackBinary.ReadSingle(bytes, offset, out readSize);
            break;
          case 12:
            num15 = MessagePackBinary.ReadSingle(bytes, offset, out readSize);
            break;
          case 13:
            num16 = MessagePackBinary.ReadSingle(bytes, offset, out readSize);
            break;
          case 14:
            num17 = MessagePackBinary.ReadSingle(bytes, offset, out readSize);
            break;
          case 15:
            num18 = MessagePackBinary.ReadSingle(bytes, offset, out readSize);
            break;
          default:
            readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
            break;
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new Matrix4x4()
      {
        m00 = num3,
        m10 = num4,
        m20 = num5,
        m30 = num6,
        m01 = num7,
        m11 = num8,
        m21 = num9,
        m31 = num10,
        m02 = num11,
        m12 = num12,
        m22 = num13,
        m32 = num14,
        m03 = num15,
        m13 = num16,
        m23 = num17,
        m33 = num18
      };
    }
  }
}
