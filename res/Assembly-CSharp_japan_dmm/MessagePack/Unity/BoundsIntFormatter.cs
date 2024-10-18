// Decompiled with JetBrains decompiler
// Type: MessagePack.Unity.BoundsIntFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Formatters;
using System;
using UnityEngine;

#nullable disable
namespace MessagePack.Unity
{
  public sealed class BoundsIntFormatter : IMessagePackFormatter<BoundsInt>, IMessagePackFormatter
  {
    public int Serialize(
      ref byte[] bytes,
      int offset,
      BoundsInt value,
      IFormatterResolver formatterResolver)
    {
      int num = offset;
      offset += MessagePackBinary.WriteFixedArrayHeaderUnsafe(ref bytes, offset, 2);
      offset += formatterResolver.GetFormatterWithVerify<Vector3Int>().Serialize(ref bytes, offset, ((BoundsInt) ref value).position, formatterResolver);
      offset += formatterResolver.GetFormatterWithVerify<Vector3Int>().Serialize(ref bytes, offset, ((BoundsInt) ref value).size, formatterResolver);
      return offset - num;
    }

    public BoundsInt Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      int num1 = !MessagePackBinary.IsNil(bytes, offset) ? offset : throw new InvalidOperationException("typecode is null, struct not supported");
      int num2 = MessagePackBinary.ReadArrayHeader(bytes, offset, out readSize);
      offset += readSize;
      Vector3Int vector3Int1 = new Vector3Int();
      Vector3Int vector3Int2 = new Vector3Int();
      for (int index = 0; index < num2; ++index)
      {
        switch (index)
        {
          case 0:
            vector3Int1 = formatterResolver.GetFormatterWithVerify<Vector3Int>().Deserialize(bytes, offset, formatterResolver, out readSize);
            break;
          case 1:
            vector3Int2 = formatterResolver.GetFormatterWithVerify<Vector3Int>().Deserialize(bytes, offset, formatterResolver, out readSize);
            break;
          default:
            readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
            break;
        }
        offset += readSize;
      }
      readSize = offset - num1;
      BoundsInt boundsInt;
      // ISSUE: explicit constructor call
      ((BoundsInt) ref boundsInt).\u002Ector(vector3Int1, vector3Int2);
      ((BoundsInt) ref boundsInt).position = vector3Int1;
      ((BoundsInt) ref boundsInt).size = vector3Int2;
      return boundsInt;
    }
  }
}
