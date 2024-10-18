// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.JSON_QuestProgressFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class JSON_QuestProgressFormatter : 
    IMessagePackFormatter<JSON_QuestProgress>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public JSON_QuestProgressFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "i",
          0
        },
        {
          "s",
          1
        },
        {
          "e",
          2
        },
        {
          "n",
          3
        },
        {
          "c",
          4
        },
        {
          "t",
          5
        },
        {
          "m",
          6
        },
        {
          "d",
          7
        },
        {
          "b",
          8
        }
      };
      this.____stringByteKeys = new byte[9][]
      {
        MessagePackBinary.GetEncodedStringBytes("i"),
        MessagePackBinary.GetEncodedStringBytes("s"),
        MessagePackBinary.GetEncodedStringBytes("e"),
        MessagePackBinary.GetEncodedStringBytes("n"),
        MessagePackBinary.GetEncodedStringBytes("c"),
        MessagePackBinary.GetEncodedStringBytes("t"),
        MessagePackBinary.GetEncodedStringBytes("m"),
        MessagePackBinary.GetEncodedStringBytes("d"),
        MessagePackBinary.GetEncodedStringBytes("b")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      JSON_QuestProgress value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 9);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.i, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += MessagePackBinary.WriteInt64(ref bytes, offset, value.s);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += MessagePackBinary.WriteInt64(ref bytes, offset, value.e);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += MessagePackBinary.WriteInt64(ref bytes, offset, value.n);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.c);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.t);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.m);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_QuestCount>().Serialize(ref bytes, offset, value.d, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.b);
      return offset - num;
    }

    public JSON_QuestProgress Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (JSON_QuestProgress) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      string str = (string) null;
      long num3 = 0;
      long num4 = 0;
      long num5 = 0;
      int num6 = 0;
      int num7 = 0;
      int num8 = 0;
      JSON_QuestCount jsonQuestCount = (JSON_QuestCount) null;
      int num9 = 0;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num10;
        if (!this.____keyMapping.TryGetValueSafe(key, out num10))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num10)
          {
            case 0:
              str = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              num3 = MessagePackBinary.ReadInt64(bytes, offset, out readSize);
              break;
            case 2:
              num4 = MessagePackBinary.ReadInt64(bytes, offset, out readSize);
              break;
            case 3:
              num5 = MessagePackBinary.ReadInt64(bytes, offset, out readSize);
              break;
            case 4:
              num6 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 5:
              num7 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 6:
              num8 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 7:
              jsonQuestCount = formatterResolver.GetFormatterWithVerify<JSON_QuestCount>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 8:
              num9 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new JSON_QuestProgress()
      {
        i = str,
        s = num3,
        e = num4,
        n = num5,
        c = num6,
        t = num7,
        m = num8,
        d = jsonQuestCount,
        b = num9
      };
    }
  }
}
