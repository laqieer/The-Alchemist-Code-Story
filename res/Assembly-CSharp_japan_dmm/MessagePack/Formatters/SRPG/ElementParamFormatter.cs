// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.ElementParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class ElementParamFormatter : 
    IMessagePackFormatter<ElementParam>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public ElementParamFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "fire",
          0
        },
        {
          "water",
          1
        },
        {
          "wind",
          2
        },
        {
          "thunder",
          3
        },
        {
          "shine",
          4
        },
        {
          "dark",
          5
        },
        {
          "values",
          6
        }
      };
      this.____stringByteKeys = new byte[7][]
      {
        MessagePackBinary.GetEncodedStringBytes("fire"),
        MessagePackBinary.GetEncodedStringBytes("water"),
        MessagePackBinary.GetEncodedStringBytes("wind"),
        MessagePackBinary.GetEncodedStringBytes("thunder"),
        MessagePackBinary.GetEncodedStringBytes("shine"),
        MessagePackBinary.GetEncodedStringBytes("dark"),
        MessagePackBinary.GetEncodedStringBytes("values")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      ElementParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 7);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.fire);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.water);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.wind);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.thunder);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.shine);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.dark);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += formatterResolver.GetFormatterWithVerify<short[]>().Serialize(ref bytes, offset, value.values, formatterResolver);
      return offset - num;
    }

    public ElementParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (ElementParam) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      short num3 = 0;
      short num4 = 0;
      short num5 = 0;
      short num6 = 0;
      short num7 = 0;
      short num8 = 0;
      short[] numArray = (short[]) null;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num9;
        if (!this.____keyMapping.TryGetValueSafe(key, out num9))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num9)
          {
            case 0:
              num3 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 1:
              num4 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 2:
              num5 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 3:
              num6 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 4:
              num7 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 5:
              num8 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 6:
              numArray = formatterResolver.GetFormatterWithVerify<short[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new ElementParam()
      {
        fire = num3,
        water = num4,
        wind = num5,
        thunder = num6,
        shine = num7,
        dark = num8,
        values = numArray
      };
    }
  }
}
