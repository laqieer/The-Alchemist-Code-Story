// Decompiled with JetBrains decompiler
// Type: MessagePack.Unity.RectIntFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Formatters;
using System;
using UnityEngine;

#nullable disable
namespace MessagePack.Unity
{
  public sealed class RectIntFormatter : IMessagePackFormatter<RectInt>, IMessagePackFormatter
  {
    public int Serialize(
      ref byte[] bytes,
      int offset,
      RectInt value,
      IFormatterResolver formatterResolver)
    {
      int num = offset;
      offset += MessagePackBinary.WriteFixedArrayHeaderUnsafe(ref bytes, offset, 4);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, ((RectInt) ref value).x);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, ((RectInt) ref value).y);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, ((RectInt) ref value).width);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, ((RectInt) ref value).height);
      return offset - num;
    }

    public RectInt Deserialize(
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
      int num6 = 0;
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
          case 3:
            num6 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
            break;
          default:
            readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
            break;
        }
        offset += readSize;
      }
      readSize = offset - num1;
      RectInt rectInt;
      // ISSUE: explicit constructor call
      ((RectInt) ref rectInt).\u002Ector(num3, num4, num5, num6);
      ((RectInt) ref rectInt).x = num3;
      ((RectInt) ref rectInt).y = num4;
      ((RectInt) ref rectInt).width = num5;
      ((RectInt) ref rectInt).height = num6;
      return rectInt;
    }
  }
}
