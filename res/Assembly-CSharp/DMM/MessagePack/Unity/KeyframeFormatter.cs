// Decompiled with JetBrains decompiler
// Type: MessagePack.Unity.KeyframeFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Formatters;
using System;
using UnityEngine;

#nullable disable
namespace MessagePack.Unity
{
  public sealed class KeyframeFormatter : IMessagePackFormatter<Keyframe>, IMessagePackFormatter
  {
    public int Serialize(
      ref byte[] bytes,
      int offset,
      Keyframe value,
      IFormatterResolver formatterResolver)
    {
      int num = offset;
      offset += MessagePackBinary.WriteFixedArrayHeaderUnsafe(ref bytes, offset, 4);
      offset += MessagePackBinary.WriteSingle(ref bytes, offset, ((Keyframe) ref value).time);
      offset += MessagePackBinary.WriteSingle(ref bytes, offset, ((Keyframe) ref value).value);
      offset += MessagePackBinary.WriteSingle(ref bytes, offset, ((Keyframe) ref value).inTangent);
      offset += MessagePackBinary.WriteSingle(ref bytes, offset, ((Keyframe) ref value).outTangent);
      return offset - num;
    }

    public Keyframe Deserialize(
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
      float num6 = 0.0f;
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
          case 3:
            num6 = MessagePackBinary.ReadSingle(bytes, offset, out readSize);
            break;
          default:
            readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
            break;
        }
        offset += readSize;
      }
      readSize = offset - num1;
      Keyframe keyframe;
      // ISSUE: explicit constructor call
      ((Keyframe) ref keyframe).\u002Ector(num3, num4, num5, num6);
      ((Keyframe) ref keyframe).time = num3;
      ((Keyframe) ref keyframe).value = num4;
      ((Keyframe) ref keyframe).inTangent = num5;
      ((Keyframe) ref keyframe).outTangent = num6;
      return keyframe;
    }
  }
}
