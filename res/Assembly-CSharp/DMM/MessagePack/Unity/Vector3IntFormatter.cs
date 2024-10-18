// Decompiled with JetBrains decompiler
// Type: MessagePack.Unity.Vector3IntFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Formatters;
using System;
using UnityEngine;

#nullable disable
namespace MessagePack.Unity
{
  public sealed class Vector3IntFormatter : IMessagePackFormatter<Vector3Int>, IMessagePackFormatter
  {
    public int Serialize(
      ref byte[] bytes,
      int offset,
      Vector3Int value,
      IFormatterResolver formatterResolver)
    {
      int num = offset;
      offset += MessagePackBinary.WriteFixedArrayHeaderUnsafe(ref bytes, offset, 3);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, ((Vector3Int) ref value).x);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, ((Vector3Int) ref value).y);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, ((Vector3Int) ref value).z);
      return offset - num;
    }

    public Vector3Int Deserialize(
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
      int num5 = 0;
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
          case 2:
            num5 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
            break;
          default:
            readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
            break;
        }
        offset += readSize;
      }
      readSize = offset - num1;
      Vector3Int vector3Int;
      // ISSUE: explicit constructor call
      ((Vector3Int) ref vector3Int).\u002Ector(num3, num4, num5);
      ((Vector3Int) ref vector3Int).x = num3;
      ((Vector3Int) ref vector3Int).y = num4;
      ((Vector3Int) ref vector3Int).z = num5;
      return vector3Int;
    }
  }
}
