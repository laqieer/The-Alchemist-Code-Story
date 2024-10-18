// Decompiled with JetBrains decompiler
// Type: MessagePack.Unity.GradientColorKeyFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Formatters;
using System;
using UnityEngine;

#nullable disable
namespace MessagePack.Unity
{
  public sealed class GradientColorKeyFormatter : 
    IMessagePackFormatter<GradientColorKey>,
    IMessagePackFormatter
  {
    public int Serialize(
      ref byte[] bytes,
      int offset,
      GradientColorKey value,
      IFormatterResolver formatterResolver)
    {
      int num = offset;
      offset += MessagePackBinary.WriteFixedArrayHeaderUnsafe(ref bytes, offset, 2);
      offset += formatterResolver.GetFormatterWithVerify<Color>().Serialize(ref bytes, offset, value.color, formatterResolver);
      offset += MessagePackBinary.WriteSingle(ref bytes, offset, value.time);
      return offset - num;
    }

    public GradientColorKey Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      int num1 = !MessagePackBinary.IsNil(bytes, offset) ? offset : throw new InvalidOperationException("typecode is null, struct not supported");
      int num2 = MessagePackBinary.ReadArrayHeader(bytes, offset, out readSize);
      offset += readSize;
      Color color = new Color();
      float num3 = 0.0f;
      for (int index = 0; index < num2; ++index)
      {
        switch (index)
        {
          case 0:
            color = formatterResolver.GetFormatterWithVerify<Color>().Deserialize(bytes, offset, formatterResolver, out readSize);
            break;
          case 1:
            num3 = MessagePackBinary.ReadSingle(bytes, offset, out readSize);
            break;
          default:
            readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
            break;
        }
        offset += readSize;
      }
      readSize = offset - num1;
      GradientColorKey gradientColorKey;
      // ISSUE: explicit constructor call
      ((GradientColorKey) ref gradientColorKey).\u002Ector(color, num3);
      gradientColorKey.color = color;
      gradientColorKey.time = num3;
      return gradientColorKey;
    }
  }
}
