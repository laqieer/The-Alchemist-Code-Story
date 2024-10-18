// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.JSON_TowerFloorParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class JSON_TowerFloorParamFormatter : 
    IMessagePackFormatter<JSON_TowerFloorParam>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public JSON_TowerFloorParamFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "iname",
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
          "cond_quest",
          6
        },
        {
          "rdy_cnd",
          7
        },
        {
          "reward_id",
          8
        },
        {
          "map",
          9
        },
        {
          "deck",
          10
        },
        {
          "tag",
          11
        },
        {
          "hp_recover_rate",
          12
        },
        {
          "pt",
          13
        },
        {
          "lv",
          14
        },
        {
          "joblv",
          15
        },
        {
          "can_help",
          16
        },
        {
          "floor",
          17
        },
        {
          "is_unit_chg",
          18
        },
        {
          "naut",
          19
        },
        {
          "me_id",
          20
        },
        {
          "is_wth_no_chg",
          21
        },
        {
          "wth_set_id",
          22
        },
        {
          "mission",
          23
        }
      };
      this.____stringByteKeys = new byte[24][]
      {
        MessagePackBinary.GetEncodedStringBytes("iname"),
        MessagePackBinary.GetEncodedStringBytes("title"),
        MessagePackBinary.GetEncodedStringBytes("name"),
        MessagePackBinary.GetEncodedStringBytes("expr"),
        MessagePackBinary.GetEncodedStringBytes("cond"),
        MessagePackBinary.GetEncodedStringBytes("tower_id"),
        MessagePackBinary.GetEncodedStringBytes("cond_quest"),
        MessagePackBinary.GetEncodedStringBytes("rdy_cnd"),
        MessagePackBinary.GetEncodedStringBytes("reward_id"),
        MessagePackBinary.GetEncodedStringBytes("map"),
        MessagePackBinary.GetEncodedStringBytes("deck"),
        MessagePackBinary.GetEncodedStringBytes("tag"),
        MessagePackBinary.GetEncodedStringBytes("hp_recover_rate"),
        MessagePackBinary.GetEncodedStringBytes("pt"),
        MessagePackBinary.GetEncodedStringBytes("lv"),
        MessagePackBinary.GetEncodedStringBytes("joblv"),
        MessagePackBinary.GetEncodedStringBytes("can_help"),
        MessagePackBinary.GetEncodedStringBytes("floor"),
        MessagePackBinary.GetEncodedStringBytes("is_unit_chg"),
        MessagePackBinary.GetEncodedStringBytes("naut"),
        MessagePackBinary.GetEncodedStringBytes("me_id"),
        MessagePackBinary.GetEncodedStringBytes("is_wth_no_chg"),
        MessagePackBinary.GetEncodedStringBytes("wth_set_id"),
        MessagePackBinary.GetEncodedStringBytes("mission")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      JSON_TowerFloorParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, 24);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.iname, formatterResolver);
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
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.cond_quest, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.rdy_cnd, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.reward_id, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_MapParam[]>().Serialize(ref bytes, offset, value.map, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.deck, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += formatterResolver.GetFormatterWithVerify<byte[]>().Serialize(ref bytes, offset, value.tag, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += MessagePackBinary.WriteByte(ref bytes, offset, value.hp_recover_rate);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += MessagePackBinary.WriteByte(ref bytes, offset, value.pt);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += MessagePackBinary.WriteByte(ref bytes, offset, value.lv);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
      offset += MessagePackBinary.WriteByte(ref bytes, offset, value.joblv);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[16]);
      offset += MessagePackBinary.WriteByte(ref bytes, offset, value.can_help);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[17]);
      offset += MessagePackBinary.WriteByte(ref bytes, offset, value.floor);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[18]);
      offset += MessagePackBinary.WriteByte(ref bytes, offset, value.is_unit_chg);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[19]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.naut);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[20]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.me_id, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[21]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.is_wth_no_chg);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[22]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.wth_set_id, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[23]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.mission, formatterResolver);
      return offset - num;
    }

    public JSON_TowerFloorParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (JSON_TowerFloorParam) null;
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
      JSON_MapParam[] jsonMapParamArray = (JSON_MapParam[]) null;
      string str10 = (string) null;
      byte[] numArray = (byte[]) null;
      byte num3 = 0;
      byte num4 = 0;
      byte num5 = 0;
      byte num6 = 0;
      byte num7 = 0;
      byte num8 = 0;
      byte num9 = 0;
      int num10 = 0;
      string str11 = (string) null;
      int num11 = 0;
      string str12 = (string) null;
      string str13 = (string) null;
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
              jsonMapParamArray = formatterResolver.GetFormatterWithVerify<JSON_MapParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 10:
              str10 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 11:
              numArray = formatterResolver.GetFormatterWithVerify<byte[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 12:
              num3 = MessagePackBinary.ReadByte(bytes, offset, out readSize);
              break;
            case 13:
              num4 = MessagePackBinary.ReadByte(bytes, offset, out readSize);
              break;
            case 14:
              num5 = MessagePackBinary.ReadByte(bytes, offset, out readSize);
              break;
            case 15:
              num6 = MessagePackBinary.ReadByte(bytes, offset, out readSize);
              break;
            case 16:
              num7 = MessagePackBinary.ReadByte(bytes, offset, out readSize);
              break;
            case 17:
              num8 = MessagePackBinary.ReadByte(bytes, offset, out readSize);
              break;
            case 18:
              num9 = MessagePackBinary.ReadByte(bytes, offset, out readSize);
              break;
            case 19:
              num10 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 20:
              str11 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 21:
              num11 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 22:
              str12 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 23:
              str13 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new JSON_TowerFloorParam()
      {
        iname = str1,
        title = str2,
        name = str3,
        expr = str4,
        cond = str5,
        tower_id = str6,
        cond_quest = str7,
        rdy_cnd = str8,
        reward_id = str9,
        map = jsonMapParamArray,
        deck = str10,
        tag = numArray,
        hp_recover_rate = num3,
        pt = num4,
        lv = num5,
        joblv = num6,
        can_help = num7,
        floor = num8,
        is_unit_chg = num9,
        naut = num10,
        me_id = str11,
        is_wth_no_chg = num11,
        wth_set_id = str12,
        mission = str13
      };
    }
  }
}
