// Decompiled with JetBrains decompiler
// Type: MessagePack.Unity.LayerMaskFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Formatters;
using System;
using UnityEngine;

#nullable disable
namespace MessagePack.Unity
{
  public sealed class LayerMaskFormatter : IMessagePackFormatter<LayerMask>, IMessagePackFormatter
  {
    public int Serialize(
      ref byte[] bytes,
      int offset,
      LayerMask value,
      IFormatterResolver formatterResolver)
    {
      int num = offset;
      offset += MessagePackBinary.WriteFixedArrayHeaderUnsafe(ref bytes, offset, 1);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, ((LayerMask) ref value).value);
      return offset - num;
    }

    public LayerMask Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      int num1 = !MessagePackBinary.IsNil(bytes, offset) ? offset : throw new InvalidOperationException("typecode is null, struct not supported");
      int num2 = MessagePackBinary.ReadArrayHeader(bytes, offset, out readSize);
      offset += readSize;
      int num3 = 0;
      for (int index = 0; index < num2; ++index)
      {
        if (index == 0)
          num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
        else
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        offset += readSize;
      }
      readSize = offset - num1;
      LayerMask layerMask = new LayerMask();
      ((LayerMask) ref layerMask).value = num3;
      return layerMask;
    }
  }
}
