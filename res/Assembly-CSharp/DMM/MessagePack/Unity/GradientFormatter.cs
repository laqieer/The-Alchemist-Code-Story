// Decompiled with JetBrains decompiler
// Type: MessagePack.Unity.GradientFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Formatters;
using UnityEngine;

#nullable disable
namespace MessagePack.Unity
{
  public sealed class GradientFormatter : IMessagePackFormatter<Gradient>, IMessagePackFormatter
  {
    public int Serialize(
      ref byte[] bytes,
      int offset,
      Gradient value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedArrayHeaderUnsafe(ref bytes, offset, 3);
      offset += formatterResolver.GetFormatterWithVerify<GradientColorKey[]>().Serialize(ref bytes, offset, value.colorKeys, formatterResolver);
      offset += formatterResolver.GetFormatterWithVerify<GradientAlphaKey[]>().Serialize(ref bytes, offset, value.alphaKeys, formatterResolver);
      offset += formatterResolver.GetFormatterWithVerify<GradientMode>().Serialize(ref bytes, offset, value.mode, formatterResolver);
      return offset - num;
    }

    public Gradient Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (Gradient) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadArrayHeader(bytes, offset, out readSize);
      offset += readSize;
      GradientColorKey[] gradientColorKeyArray = (GradientColorKey[]) null;
      GradientAlphaKey[] gradientAlphaKeyArray = (GradientAlphaKey[]) null;
      GradientMode gradientMode = (GradientMode) 0;
      for (int index = 0; index < num2; ++index)
      {
        switch (index)
        {
          case 0:
            gradientColorKeyArray = formatterResolver.GetFormatterWithVerify<GradientColorKey[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
            break;
          case 1:
            gradientAlphaKeyArray = formatterResolver.GetFormatterWithVerify<GradientAlphaKey[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
            break;
          case 2:
            gradientMode = formatterResolver.GetFormatterWithVerify<GradientMode>().Deserialize(bytes, offset, formatterResolver, out readSize);
            break;
          default:
            readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
            break;
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new Gradient()
      {
        colorKeys = gradientColorKeyArray,
        alphaKeys = gradientAlphaKeyArray,
        mode = gradientMode
      };
    }
  }
}
