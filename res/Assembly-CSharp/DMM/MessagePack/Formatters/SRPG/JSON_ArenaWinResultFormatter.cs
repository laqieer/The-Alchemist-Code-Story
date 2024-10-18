// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.JSON_ArenaWinResultFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class JSON_ArenaWinResultFormatter : 
    IMessagePackFormatter<JSON_ArenaWinResult>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public JSON_ArenaWinResultFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "scale",
          0
        },
        {
          "rank",
          1
        },
        {
          "coin",
          2
        },
        {
          "gold",
          3
        },
        {
          "ac",
          4
        },
        {
          "item1",
          5
        },
        {
          "item2",
          6
        },
        {
          "item3",
          7
        },
        {
          "item4",
          8
        },
        {
          "item5",
          9
        },
        {
          "num1",
          10
        },
        {
          "num2",
          11
        },
        {
          "num3",
          12
        },
        {
          "num4",
          13
        },
        {
          "num5",
          14
        },
        {
          "begin_at",
          15
        },
        {
          "end_at",
          16
        }
      };
      this.____stringByteKeys = new byte[17][]
      {
        MessagePackBinary.GetEncodedStringBytes("scale"),
        MessagePackBinary.GetEncodedStringBytes("rank"),
        MessagePackBinary.GetEncodedStringBytes("coin"),
        MessagePackBinary.GetEncodedStringBytes("gold"),
        MessagePackBinary.GetEncodedStringBytes("ac"),
        MessagePackBinary.GetEncodedStringBytes("item1"),
        MessagePackBinary.GetEncodedStringBytes("item2"),
        MessagePackBinary.GetEncodedStringBytes("item3"),
        MessagePackBinary.GetEncodedStringBytes("item4"),
        MessagePackBinary.GetEncodedStringBytes("item5"),
        MessagePackBinary.GetEncodedStringBytes("num1"),
        MessagePackBinary.GetEncodedStringBytes("num2"),
        MessagePackBinary.GetEncodedStringBytes("num3"),
        MessagePackBinary.GetEncodedStringBytes("num4"),
        MessagePackBinary.GetEncodedStringBytes("num5"),
        MessagePackBinary.GetEncodedStringBytes("begin_at"),
        MessagePackBinary.GetEncodedStringBytes("end_at")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      JSON_ArenaWinResult value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, 17);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.scale, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rank);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.coin);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.gold);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ac);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.item1, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.item2, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.item3, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.item4, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.item5, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.num1);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.num2);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.num3);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.num4);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.num5);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.begin_at, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[16]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.end_at, formatterResolver);
      return offset - num;
    }

    public JSON_ArenaWinResult Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (JSON_ArenaWinResult) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      string str1 = (string) null;
      int num3 = 0;
      int num4 = 0;
      int num5 = 0;
      int num6 = 0;
      string str2 = (string) null;
      string str3 = (string) null;
      string str4 = (string) null;
      string str5 = (string) null;
      string str6 = (string) null;
      int num7 = 0;
      int num8 = 0;
      int num9 = 0;
      int num10 = 0;
      int num11 = 0;
      string str7 = (string) null;
      string str8 = (string) null;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num12;
        if (!this.____keyMapping.TryGetValueSafe(key, out num12))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num12)
          {
            case 0:
              str1 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 2:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 3:
              num5 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 4:
              num6 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 5:
              str2 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 6:
              str3 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 7:
              str4 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 8:
              str5 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 9:
              str6 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 10:
              num7 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 11:
              num8 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 12:
              num9 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 13:
              num10 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 14:
              num11 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 15:
              str7 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 16:
              str8 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      JSON_ArenaWinResult jsonArenaWinResult = new JSON_ArenaWinResult();
      jsonArenaWinResult.scale = str1;
      jsonArenaWinResult.rank = num3;
      jsonArenaWinResult.coin = num4;
      jsonArenaWinResult.gold = num5;
      jsonArenaWinResult.ac = num6;
      jsonArenaWinResult.item1 = str2;
      jsonArenaWinResult.item2 = str3;
      jsonArenaWinResult.item3 = str4;
      jsonArenaWinResult.item4 = str5;
      jsonArenaWinResult.item5 = str6;
      jsonArenaWinResult.num1 = num7;
      jsonArenaWinResult.num2 = num8;
      jsonArenaWinResult.num3 = num9;
      jsonArenaWinResult.num4 = num10;
      jsonArenaWinResult.num5 = num11;
      jsonArenaWinResult.begin_at = str7;
      jsonArenaWinResult.end_at = str8;
      return jsonArenaWinResult;
    }
  }
}
