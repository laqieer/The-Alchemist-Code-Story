// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.JSON_TowerParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class JSON_TowerParamFormatter : 
    IMessagePackFormatter<JSON_TowerParam>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public JSON_TowerParamFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "iname",
          0
        },
        {
          "name",
          1
        },
        {
          "expr",
          2
        },
        {
          "banr",
          3
        },
        {
          "item",
          4
        },
        {
          "bg",
          5
        },
        {
          "floor_bg_open",
          6
        },
        {
          "floor_bg_close",
          7
        },
        {
          "eventURL",
          8
        },
        {
          "unit_recover_minute",
          9
        },
        {
          "unit_recover_coin",
          10
        },
        {
          "can_unit_recover",
          11
        },
        {
          "is_down",
          12
        },
        {
          "is_view_ranking",
          13
        },
        {
          "unlock_level",
          14
        },
        {
          "unlock_quest",
          15
        },
        {
          "url",
          16
        },
        {
          "floor_reset_coin",
          17
        },
        {
          "score_iname",
          18
        }
      };
      this.____stringByteKeys = new byte[19][]
      {
        MessagePackBinary.GetEncodedStringBytes("iname"),
        MessagePackBinary.GetEncodedStringBytes("name"),
        MessagePackBinary.GetEncodedStringBytes("expr"),
        MessagePackBinary.GetEncodedStringBytes("banr"),
        MessagePackBinary.GetEncodedStringBytes("item"),
        MessagePackBinary.GetEncodedStringBytes("bg"),
        MessagePackBinary.GetEncodedStringBytes("floor_bg_open"),
        MessagePackBinary.GetEncodedStringBytes("floor_bg_close"),
        MessagePackBinary.GetEncodedStringBytes("eventURL"),
        MessagePackBinary.GetEncodedStringBytes("unit_recover_minute"),
        MessagePackBinary.GetEncodedStringBytes("unit_recover_coin"),
        MessagePackBinary.GetEncodedStringBytes("can_unit_recover"),
        MessagePackBinary.GetEncodedStringBytes("is_down"),
        MessagePackBinary.GetEncodedStringBytes("is_view_ranking"),
        MessagePackBinary.GetEncodedStringBytes("unlock_level"),
        MessagePackBinary.GetEncodedStringBytes("unlock_quest"),
        MessagePackBinary.GetEncodedStringBytes("url"),
        MessagePackBinary.GetEncodedStringBytes("floor_reset_coin"),
        MessagePackBinary.GetEncodedStringBytes("score_iname")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      JSON_TowerParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, 19);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.name, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.expr, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.banr, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.item, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.bg, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.floor_bg_open, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.floor_bg_close, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.eventURL, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.unit_recover_minute);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.unit_recover_coin);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += MessagePackBinary.WriteByte(ref bytes, offset, value.can_unit_recover);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += MessagePackBinary.WriteByte(ref bytes, offset, value.is_down);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += MessagePackBinary.WriteByte(ref bytes, offset, value.is_view_ranking);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.unlock_level);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.unlock_quest, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[16]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.url, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[17]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.floor_reset_coin);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[18]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.score_iname, formatterResolver);
      return offset - num;
    }

    public JSON_TowerParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (JSON_TowerParam) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      string str1 = (string) null;
      string str2 = (string) null;
      string str3 = (string) null;
      string str4 = (string) null;
      string str5 = (string) null;
      string str6 = (string) null;
      string str7 = (string) null;
      string str8 = (string) null;
      string str9 = (string) null;
      short num3 = 0;
      short num4 = 0;
      byte num5 = 0;
      byte num6 = 0;
      byte num7 = 0;
      short num8 = 0;
      string str10 = (string) null;
      string str11 = (string) null;
      short num9 = 0;
      string str12 = (string) null;
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
              str1 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              str2 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              str3 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              str4 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              str5 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 5:
              str6 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 6:
              str7 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 7:
              str8 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 8:
              str9 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 9:
              num3 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 10:
              num4 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 11:
              num5 = MessagePackBinary.ReadByte(bytes, offset, out readSize);
              break;
            case 12:
              num6 = MessagePackBinary.ReadByte(bytes, offset, out readSize);
              break;
            case 13:
              num7 = MessagePackBinary.ReadByte(bytes, offset, out readSize);
              break;
            case 14:
              num8 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 15:
              str10 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 16:
              str11 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 17:
              num9 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 18:
              str12 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new JSON_TowerParam()
      {
        iname = str1,
        name = str2,
        expr = str3,
        banr = str4,
        item = str5,
        bg = str6,
        floor_bg_open = str7,
        floor_bg_close = str8,
        eventURL = str9,
        unit_recover_minute = num3,
        unit_recover_coin = num4,
        can_unit_recover = num5,
        is_down = num6,
        is_view_ranking = num7,
        unlock_level = num8,
        unlock_quest = str10,
        url = str11,
        floor_reset_coin = num9,
        score_iname = str12
      };
    }
  }
}
