// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.JSON_GvGPeriodParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class JSON_GvGPeriodParamFormatter : 
    IMessagePackFormatter<JSON_GvGPeriodParam>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public JSON_GvGPeriodParamFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "id",
          0
        },
        {
          "prepare_start_at",
          1
        },
        {
          "begin_at",
          2
        },
        {
          "end_at",
          3
        },
        {
          "exit_at",
          4
        },
        {
          "declaration_start_time",
          5
        },
        {
          "declaration_end_time",
          6
        },
        {
          "declaration_cool_minites",
          7
        },
        {
          "battle_start_time",
          8
        },
        {
          "battle_end_time",
          9
        },
        {
          "battle_cool_start_time",
          10
        },
        {
          "battle_cool_end_time",
          11
        },
        {
          "declared_cool_minutes",
          12
        },
        {
          "battle_cool_seconds",
          13
        },
        {
          "declare_num",
          14
        },
        {
          "map_idx",
          15
        },
        {
          "matching_count_min",
          16
        },
        {
          "matching_count_max",
          17
        },
        {
          "first_occupy_node_num",
          18
        },
        {
          "defense_unit_min",
          19
        },
        {
          "url",
          20
        },
        {
          "rule_cycle",
          21
        },
        {
          "url_title",
          22
        }
      };
      this.____stringByteKeys = new byte[23][]
      {
        MessagePackBinary.GetEncodedStringBytes("id"),
        MessagePackBinary.GetEncodedStringBytes("prepare_start_at"),
        MessagePackBinary.GetEncodedStringBytes("begin_at"),
        MessagePackBinary.GetEncodedStringBytes("end_at"),
        MessagePackBinary.GetEncodedStringBytes("exit_at"),
        MessagePackBinary.GetEncodedStringBytes("declaration_start_time"),
        MessagePackBinary.GetEncodedStringBytes("declaration_end_time"),
        MessagePackBinary.GetEncodedStringBytes("declaration_cool_minites"),
        MessagePackBinary.GetEncodedStringBytes("battle_start_time"),
        MessagePackBinary.GetEncodedStringBytes("battle_end_time"),
        MessagePackBinary.GetEncodedStringBytes("battle_cool_start_time"),
        MessagePackBinary.GetEncodedStringBytes("battle_cool_end_time"),
        MessagePackBinary.GetEncodedStringBytes("declared_cool_minutes"),
        MessagePackBinary.GetEncodedStringBytes("battle_cool_seconds"),
        MessagePackBinary.GetEncodedStringBytes("declare_num"),
        MessagePackBinary.GetEncodedStringBytes("map_idx"),
        MessagePackBinary.GetEncodedStringBytes("matching_count_min"),
        MessagePackBinary.GetEncodedStringBytes("matching_count_max"),
        MessagePackBinary.GetEncodedStringBytes("first_occupy_node_num"),
        MessagePackBinary.GetEncodedStringBytes("defense_unit_min"),
        MessagePackBinary.GetEncodedStringBytes("url"),
        MessagePackBinary.GetEncodedStringBytes("rule_cycle"),
        MessagePackBinary.GetEncodedStringBytes("url_title")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      JSON_GvGPeriodParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, 23);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.id);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.prepare_start_at, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.begin_at, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.end_at, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.exit_at, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.declaration_start_time, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.declaration_end_time, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.declaration_cool_minites);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.battle_start_time, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.battle_end_time, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.battle_cool_start_time, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.battle_cool_end_time, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.declared_cool_minutes);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.battle_cool_seconds);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.declare_num);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.map_idx);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[16]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.matching_count_min);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[17]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.matching_count_max);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[18]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.first_occupy_node_num);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[19]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.defense_unit_min);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[20]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.url, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[21]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.rule_cycle, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[22]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.url_title, formatterResolver);
      return offset - num;
    }

    public JSON_GvGPeriodParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (JSON_GvGPeriodParam) null;
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
      string str6 = (string) null;
      int num4 = 0;
      string str7 = (string) null;
      string str8 = (string) null;
      string str9 = (string) null;
      string str10 = (string) null;
      int num5 = 0;
      int num6 = 0;
      int num7 = 0;
      int num8 = 0;
      int num9 = 0;
      int num10 = 0;
      int num11 = 0;
      int num12 = 0;
      string str11 = (string) null;
      string[] strArray = (string[]) null;
      string str12 = (string) null;
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
              str6 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 7:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 8:
              str7 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 9:
              str8 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 10:
              str9 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 11:
              str10 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 12:
              num5 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 13:
              num6 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 14:
              num7 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 15:
              num8 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 16:
              num9 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 17:
              num10 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 18:
              num11 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 19:
              num12 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 20:
              str11 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 21:
              strArray = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 22:
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
      return new JSON_GvGPeriodParam()
      {
        id = num3,
        prepare_start_at = str1,
        begin_at = str2,
        end_at = str3,
        exit_at = str4,
        declaration_start_time = str5,
        declaration_end_time = str6,
        declaration_cool_minites = num4,
        battle_start_time = str7,
        battle_end_time = str8,
        battle_cool_start_time = str9,
        battle_cool_end_time = str10,
        declared_cool_minutes = num5,
        battle_cool_seconds = num6,
        declare_num = num7,
        map_idx = num8,
        matching_count_min = num9,
        matching_count_max = num10,
        first_occupy_node_num = num11,
        defense_unit_min = num12,
        url = str11,
        rule_cycle = strArray,
        url_title = str12
      };
    }
  }
}
