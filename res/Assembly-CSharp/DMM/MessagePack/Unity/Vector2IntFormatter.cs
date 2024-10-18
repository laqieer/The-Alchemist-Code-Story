// Decompiled with JetBrains decompiler
// Type: MessagePack.Unity.Vector2IntFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Formatters;
using System;
using UnityEngine;

#nullable disable
namespace MessagePack.Unity
{
  public sealed class Vector2IntFormatter : IMessagePackFormatter<Vector2Int>, IMessagePackFormatter
  {
    public int Serialize(
      ref byte[] bytes,
      int offset,
      Vector2Int value,
      IFormatterResolver formatterResolver)
    {
      int num = offset;
      offset += MessagePackBinary.WriteFixedArrayHeaderUnsafe(ref bytes, offset, 2);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, ((Vector2Int) ref value).x);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, ((Vector2Int) ref value).y);
      return offset - num;
    }

    public Vector2Int Deserialize(
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
      Vector2Int vector2Int;
      // ISSUE: explicit constructor call
      ((Vector2Int) ref vector2Int).\u002Ector(num3, num4);
      ((Vector2Int) ref vector2Int).x = num3;
      ((Vector2Int) ref vector2Int).y = num4;
      return vector2Int;
    }
  }
}
