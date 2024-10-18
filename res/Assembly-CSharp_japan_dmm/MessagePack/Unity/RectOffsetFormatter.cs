// Decompiled with JetBrains decompiler
// Type: MessagePack.Unity.RectOffsetFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Formatters;
using UnityEngine;

#nullable disable
namespace MessagePack.Unity
{
  public sealed class RectOffsetFormatter : IMessagePackFormatter<RectOffset>, IMessagePackFormatter
  {
    public int Serialize(
      ref byte[] bytes,
      int offset,
      RectOffset value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedArrayHeaderUnsafe(ref bytes, offset, 4);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.left);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.right);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.top);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.bottom);
      return offset - num;
    }

    public RectOffset Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (RectOffset) null;
      }
      int num1 = offset;
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
      return new RectOffset()
      {
        left = num3,
        right = num4,
        top = num5,
        bottom = num6
      };
    }
  }
}
