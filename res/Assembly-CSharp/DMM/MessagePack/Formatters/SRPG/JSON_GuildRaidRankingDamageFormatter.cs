// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.JSON_GuildRaidRankingDamageFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class JSON_GuildRaidRankingDamageFormatter : 
    IMessagePackFormatter<JSON_GuildRaidRankingDamage>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public JSON_GuildRaidRankingDamageFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "role",
          0
        },
        {
          "rank",
          1
        },
        {
          "score",
          2
        },
        {
          "name",
          3
        },
        {
          "lv",
          4
        },
        {
          "unit",
          5
        },
        {
          "unit_strength_total",
          6
        },
        {
          "boss_id",
          7
        },
        {
          "round",
          8
        },
        {
          "knock_down_at",
          9
        }
      };
      this.____stringByteKeys = new byte[10][]
      {
        MessagePackBinary.GetEncodedStringBytes("role"),
        MessagePackBinary.GetEncodedStringBytes("rank"),
        MessagePackBinary.GetEncodedStringBytes("score"),
        MessagePackBinary.GetEncodedStringBytes("name"),
        MessagePackBinary.GetEncodedStringBytes("lv"),
        MessagePackBinary.GetEncodedStringBytes("unit"),
        MessagePackBinary.GetEncodedStringBytes("unit_strength_total"),
        MessagePackBinary.GetEncodedStringBytes("boss_id"),
        MessagePackBinary.GetEncodedStringBytes("round"),
        MessagePackBinary.GetEncodedStringBytes("knock_down_at")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      JSON_GuildRaidRankingDamage value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 10);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.role);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rank);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += MessagePackBinary.WriteInt64(ref bytes, offset, value.score);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.name, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.lv);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.unit, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.unit_strength_total);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.boss_id);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.round);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.knock_down_at);
      return offset - num;
    }

    public JSON_GuildRaidRankingDamage Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (JSON_GuildRaidRankingDamage) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      int num3 = 0;
      int num4 = 0;
      long num5 = 0;
      string str1 = (string) null;
      int num6 = 0;
      string str2 = (string) null;
      int num7 = 0;
      int num8 = 0;
      int num9 = 0;
      int num10 = 0;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num11;
        if (!this.____keyMapping.TryGetValueSafe(key, out num11))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num11)
          {
            case 0:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 1:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 2:
              num5 = MessagePackBinary.ReadInt64(bytes, offset, out readSize);
              break;
            case 3:
              str1 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              num6 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 5:
              str2 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 6:
              num7 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 7:
              num8 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 8:
              num9 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 9:
              num10 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new JSON_GuildRaidRankingDamage()
      {
        role = num3,
        rank = num4,
        score = num5,
        name = str1,
        lv = num6,
        unit = str2,
        unit_strength_total = num7,
        boss_id = num8,
        round = num9,
        knock_down_at = num10
      };
    }
  }
}
