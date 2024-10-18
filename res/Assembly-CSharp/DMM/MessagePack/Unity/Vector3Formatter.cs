// Decompiled with JetBrains decompiler
// Type: MessagePack.Unity.Vector3Formatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Formatters;
using System;
using UnityEngine;

#nullable disable
namespace MessagePack.Unity
{
  public sealed class Vector3Formatter : IMessagePackFormatter<Vector3>, IMessagePackFormatter
  {
    public int Serialize(
      ref byte[] bytes,
      int offset,
      Vector3 value,
      IFormatterResolver formatterResolver)
    {
      int num = offset;
      offset += MessagePackBinary.WriteFixedArrayHeaderUnsafe(ref bytes, offset, 3);
      offset += MessagePackBinary.WriteSingle(ref bytes, offset, value.x);
      offset += MessagePackBinary.WriteSingle(ref bytes, offset, value.y);
      offset += MessagePackBinary.WriteSingle(ref bytes, offset, value.z);
      return offset - num;
    }

    public Vector3 Deserialize(
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
          default:
            readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
            break;
        }
        offset += readSize;
      }
      readSize = offset - num1;
      Vector3 vector3;
      // ISSUE: explicit constructor call
      ((Vector3) ref vector3).\u002Ector(num3, num4, num5);
      return vector3;
    }
  }
}
