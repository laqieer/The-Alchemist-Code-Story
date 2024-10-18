// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.JSON_MultiTowerFloorParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class JSON_MultiTowerFloorParamFormatter : 
    IMessagePackFormatter<JSON_MultiTowerFloorParam>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public JSON_MultiTowerFloorParamFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "id",
          0
        },
        {
          "title",
          1
        },
        {
          "name",
          2
        },
        {
          "expr",
          3
        },
        {
          "cond",
          4
        },
        {
          "tower_id",
          5
        },
        {
          "cond_floor",
          6
        },
        {
          "reward_id",
          7
        },
        {
          "map",
          8
        },
        {
          "pt",
          9
        },
        {
          "lv",
          10
        },
        {
          "joblv",
          11
        },
        {
          "floor",
          12
        },
        {
          "unitnum",
          13
        },
        {
          "notcon",
          14
        },
        {
          "me_id",
          15
        },
        {
          "is_wth_no_chg",
          16
        },
        {
          "wth_set_id",
          17
        },
        {
          "is_skip",
          18
        },
        {
          "iname",
          19
        },
        {
          "rdy_cnd",
          20
        }
      };
      this.____stringByteKeys = new byte[21][]
      {
        MessagePackBinary.GetEncodedStringBytes("id"),
        MessagePackBinary.GetEncodedStringBytes("title"),
        MessagePackBinary.GetEncodedStringBytes("name"),
        MessagePackBinary.GetEncodedStringBytes("expr"),
        MessagePackBinary.GetEncodedStringBytes("cond"),
        MessagePackBinary.GetEncodedStringBytes("tower_id"),
        MessagePackBinary.GetEncodedStringBytes("cond_floor"),
        MessagePackBinary.GetEncodedStringBytes("reward_id"),
        MessagePackBinary.GetEncodedStringBytes("map"),
        MessagePackBinary.GetEncodedStringBytes("pt"),
        MessagePackBinary.GetEncodedStringBytes("lv"),
        MessagePackBinary.GetEncodedStringBytes("joblv"),
        MessagePackBinary.GetEncodedStringBytes("floor"),
        MessagePackBinary.GetEncodedStringBytes("unitnum"),
        MessagePackBinary.GetEncodedStringBytes("notcon"),
        MessagePackBinary.GetEncodedStringBytes("me_id"),
        MessagePackBinary.GetEncodedStringBytes("is_wth_no_chg"),
        MessagePackBinary.GetEncodedStringBytes("wth_set_id"),
        MessagePackBinary.GetEncodedStringBytes("is_skip"),
        MessagePackBinary.GetEncodedStringBytes("iname"),
        MessagePackBinary.GetEncodedStringBytes("rdy_cnd")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      JSON_MultiTowerFloorParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, 21);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.id);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.title, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.name, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.expr, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.cond, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.tower_id, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.cond_floor);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.reward_id, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_MapParam[]>().Serialize(ref bytes, offset, value.map, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.pt);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.lv);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.joblv);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.floor);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.unitnum);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.notcon);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.me_id, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[16]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.is_wth_no_chg);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[17]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.wth_set_id, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[18]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.is_skip);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[19]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[20]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.rdy_cnd, formatterResolver);
      return offset - num;
    }

    public JSON_MultiTowerFloorParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (JSON_MultiTowerFloorParam) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      int num3 = 0;
      string str1 = (string) null;
      string str2 = (string) null;
      string str3 = (string) null;
      string str4 = (string) null;
      string str5 = (string) null;
      int num4 = 0;
      string str6 = (string) null;
      JSON_MapParam[] jsonMapParamArray = (JSON_MapParam[]) null;
      short num5 = 0;
      short num6 = 0;
      short num7 = 0;
      short num8 = 0;
      short num9 = 0;
      short num10 = 0;
      string str7 = (string) null;
      int num11 = 0;
      string str8 = (string) null;
      int num12 = 0;
      string str9 = (string) null;
      string str10 = (string) null;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num13;
        if (!this.____keyMapping.TryGetValueSafe(key, out num13))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num13)
          {
            case 0:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 1:
              str1 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              str2 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              str3 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              str4 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 5:
              str5 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 6:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 7:
              str6 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 8:
              jsonMapParamArray = formatterResolver.GetFormatterWithVerify<JSON_MapParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 9:
              num5 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 10:
              num6 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 11:
              num7 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 12:
              num8 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 13:
              num9 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 14:
              num10 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 15:
              str7 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 16:
              num11 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 17:
              str8 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 18:
              num12 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 19:
              str9 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 20:
              str10 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new JSON_MultiTowerFloorParam()
      {
        id = num3,
        title = str1,
        name = str2,
        expr = str3,
        cond = str4,
        tower_id = str5,
        cond_floor = num4,
        reward_id = str6,
        map = jsonMapParamArray,
        pt = num5,
        lv = num6,
        joblv = num7,
        floor = num8,
        unitnum = num9,
        notcon = num10,
        me_id = str7,
        is_wth_no_chg = num11,
        wth_set_id = str8,
        is_skip = num12,
        iname = str9,
        rdy_cnd = str10
      };
    }
  }
}
