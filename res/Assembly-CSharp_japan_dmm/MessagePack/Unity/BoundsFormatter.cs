// Decompiled with JetBrains decompiler
// Type: MessagePack.Unity.BoundsFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Formatters;
using System;
using UnityEngine;

#nullable disable
namespace MessagePack.Unity
{
  public sealed class BoundsFormatter : IMessagePackFormatter<Bounds>, IMessagePackFormatter
  {
    public int Serialize(
      ref byte[] bytes,
      int offset,
      Bounds value,
      IFormatterResolver formatterResolver)
    {
      int num = offset;
      offset += MessagePackBinary.WriteFixedArrayHeaderUnsafe(ref bytes, offset, 2);
      offset += formatterResolver.GetFormatterWithVerify<Vector3>().Serialize(ref bytes, offset, ((Bounds) ref value).center, formatterResolver);
      offset += formatterResolver.GetFormatterWithVerify<Vector3>().Serialize(ref bytes, offset, ((Bounds) ref value).size, formatterResolver);
      return offset - num;
    }

    public Bounds Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      int num1 = !MessagePackBinary.IsNil(bytes, offset) ? offset : throw new InvalidOperationException("typecode is null, struct not supported");
      int num2 = MessagePackBinary.ReadArrayHeader(bytes, offset, out readSize);
      offset += readSize;
      Vector3 vector3_1 = new Vector3();
      Vector3 vector3_2 = new Vector3();
      for (int index = 0; index < num2; ++index)
      {
        switch (index)
        {
          case 0:
            vector3_1 = formatterResolver.GetFormatterWithVerify<Vector3>().Deserialize(bytes, offset, formatterResolver, out readSize);
            break;
          case 1:
            vector3_2 = formatterResolver.GetFormatterWithVerify<Vector3>().Deserialize(bytes, offset, formatterResolver, out readSize);
            break;
          default:
            readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
            break;
        }
        offset += readSize;
      }
      readSize = offset - num1;
      Bounds bounds;
      // ISSUE: explicit constructor call
      ((Bounds) ref bounds).\u002Ector(vector3_1, vector3_2);
      return bounds;
    }
  }
}
