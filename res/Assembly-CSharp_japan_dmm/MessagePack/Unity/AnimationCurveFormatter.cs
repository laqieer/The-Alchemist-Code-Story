// Decompiled with JetBrains decompiler
// Type: MessagePack.Unity.AnimationCurveFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Formatters;
using UnityEngine;

#nullable disable
namespace MessagePack.Unity
{
  public sealed class AnimationCurveFormatter : 
    IMessagePackFormatter<AnimationCurve>,
    IMessagePackFormatter
  {
    public int Serialize(
      ref byte[] bytes,
      int offset,
      AnimationCurve value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedArrayHeaderUnsafe(ref bytes, offset, 3);
      offset += formatterResolver.GetFormatterWithVerify<Keyframe[]>().Serialize(ref bytes, offset, value.keys, formatterResolver);
      offset += formatterResolver.GetFormatterWithVerify<WrapMode>().Serialize(ref bytes, offset, value.postWrapMode, formatterResolver);
      offset += formatterResolver.GetFormatterWithVerify<WrapMode>().Serialize(ref bytes, offset, value.preWrapMode, formatterResolver);
      return offset - num;
    }

    public AnimationCurve Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (AnimationCurve) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadArrayHeader(bytes, offset, out readSize);
      offset += readSize;
      Keyframe[] keyframeArray = (Keyframe[]) null;
      WrapMode wrapMode1 = (WrapMode) 0;
      WrapMode wrapMode2 = (WrapMode) 0;
      for (int index = 0; index < num2; ++index)
      {
        switch (index)
        {
          case 0:
            keyframeArray = formatterResolver.GetFormatterWithVerify<Keyframe[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
            break;
          case 1:
            wrapMode1 = formatterResolver.GetFormatterWithVerify<WrapMode>().Deserialize(bytes, offset, formatterResolver, out readSize);
            break;
          case 2:
            wrapMode2 = formatterResolver.GetFormatterWithVerify<WrapMode>().Deserialize(bytes, offset, formatterResolver, out readSize);
            break;
          default:
            readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
            break;
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new AnimationCurve()
      {
        keys = keyframeArray,
        postWrapMode = wrapMode1,
        preWrapMode = wrapMode2
      };
    }
  }
}
