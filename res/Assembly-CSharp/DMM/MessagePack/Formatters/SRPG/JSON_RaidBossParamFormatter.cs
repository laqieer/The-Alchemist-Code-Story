// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.JSON_RaidBossParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class JSON_RaidBossParamFormatter : 
    IMessagePackFormatter<JSON_RaidBossParam>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public JSON_RaidBossParamFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "id",
          0
        },
        {
          "stamp_index",
          1
        },
        {
          "period_id",
          2
        },
        {
          "weight",
          3
        },
        {
          "name",
          4
        },
        {
          "hp",
          5
        },
        {
          "unit_iname",
          6
        },
        {
          "quest_iname",
          7
        },
        {
          "time_limit",
          8
        },
        {
          "battle_reward_id",
          9
        },
        {
          "beat_reward_id",
          10
        },
        {
          "damage_ratio_reward_id",
          11
        },
        {
          "damage_amount_reward_id",
          12
        },
        {
          "buff_id",
          13
        },
        {
          "mob_buff_id",
          14
        },
        {
          "is_boss",
          15
        }
      };
      this.____stringByteKeys = new byte[16][]
      {
        MessagePackBinary.GetEncodedStringBytes("id"),
        MessagePackBinary.GetEncodedStringBytes("stamp_index"),
        MessagePackBinary.GetEncodedStringBytes("period_id"),
        MessagePackBinary.GetEncodedStringBytes("weight"),
        MessagePackBinary.GetEncodedStringBytes("name"),
        MessagePackBinary.GetEncodedStringBytes("hp"),
        MessagePackBinary.GetEncodedStringBytes("unit_iname"),
        MessagePackBinary.GetEncodedStringBytes("quest_iname"),
        MessagePackBinary.GetEncodedStringBytes("time_limit"),
        MessagePackBinary.GetEncodedStringBytes("battle_reward_id"),
        MessagePackBinary.GetEncodedStringBytes("beat_reward_id"),
        MessagePackBinary.GetEncodedStringBytes("damage_ratio_reward_id"),
        MessagePackBinary.GetEncodedStringBytes("damage_amount_reward_id"),
        MessagePackBinary.GetEncodedStringBytes("buff_id"),
        MessagePackBinary.GetEncodedStringBytes("mob_buff_id"),
        MessagePackBinary.GetEncodedStringBytes("is_boss")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      JSON_RaidBossParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, 16);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.id);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.stamp_index);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.period_id);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.weight);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.name, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.hp);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.unit_iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.quest_iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.time_limit, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.battle_reward_id);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.beat_reward_id);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.damage_ratio_reward_id);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.damage_amount_reward_id);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.buff_id, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.mob_buff_id, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.is_boss);
      return offset - num;
    }

    public JSON_RaidBossParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (JSON_RaidBossParam) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      int num3 = 0;
      int num4 = 0;
      int num5 = 0;
      int num6 = 0;
      string str1 = (string) null;
      int num7 = 0;
      string str2 = (string) null;
      string str3 = (string) null;
      string str4 = (string) null;
      int num8 = 0;
      int num9 = 0;
      int num10 = 0;
      int num11 = 0;
      string str5 = (string) null;
      string str6 = (string) null;
      int num12 = 0;
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
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 2:
              num5 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 3:
              num6 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 4:
              str1 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 5:
              num7 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 6:
              str2 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 7:
              str3 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 8:
              str4 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 9:
              num8 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 10:
              num9 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 11:
              num10 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 12:
              num11 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 13:
              str5 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 14:
              str6 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 15:
              num12 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new JSON_RaidBossParam()
      {
        id = num3,
        stamp_index = num4,
        period_id = num5,
        weight = num6,
        name = str1,
        hp = num7,
        unit_iname = str2,
        quest_iname = str3,
        time_limit = str4,
        battle_reward_id = num8,
        beat_reward_id = num9,
        damage_ratio_reward_id = num10,
        damage_amount_reward_id = num11,
        buff_id = str5,
        mob_buff_id = str6,
        is_boss = num12
      };
    }
  }
}
