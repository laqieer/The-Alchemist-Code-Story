// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.JSON_CombatPowerRankingGuildMemberFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class JSON_CombatPowerRankingGuildMemberFormatter : 
    IMessagePackFormatter<JSON_CombatPowerRankingGuildMember>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public JSON_CombatPowerRankingGuildMemberFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "rank",
          0
        },
        {
          "combat_power",
          1
        },
        {
          "gid",
          2
        },
        {
          "uid",
          3
        },
        {
          "role_id",
          4
        },
        {
          "name",
          5
        },
        {
          "lv",
          6
        },
        {
          "award_id",
          7
        },
        {
          "units",
          8
        },
        {
          "applied_at",
          9
        },
        {
          "joined_at",
          10
        },
        {
          "leave_at",
          11
        },
        {
          "lastlogin",
          12
        }
      };
      this.____stringByteKeys = new byte[13][]
      {
        MessagePackBinary.GetEncodedStringBytes("rank"),
        MessagePackBinary.GetEncodedStringBytes("combat_power"),
        MessagePackBinary.GetEncodedStringBytes("gid"),
        MessagePackBinary.GetEncodedStringBytes("uid"),
        MessagePackBinary.GetEncodedStringBytes("role_id"),
        MessagePackBinary.GetEncodedStringBytes("name"),
        MessagePackBinary.GetEncodedStringBytes("lv"),
        MessagePackBinary.GetEncodedStringBytes("award_id"),
        MessagePackBinary.GetEncodedStringBytes("units"),
        MessagePackBinary.GetEncodedStringBytes("applied_at"),
        MessagePackBinary.GetEncodedStringBytes("joined_at"),
        MessagePackBinary.GetEncodedStringBytes("leave_at"),
        MessagePackBinary.GetEncodedStringBytes("lastlogin")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      JSON_CombatPowerRankingGuildMember value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 13);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rank);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += MessagePackBinary.WriteInt64(ref bytes, offset, value.combat_power);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += MessagePackBinary.WriteInt64(ref bytes, offset, value.gid);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.uid, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.role_id);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.name, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.lv);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.award_id, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += formatterResolver.GetFormatterWithVerify<Json_Unit>().Serialize(ref bytes, offset, value.units, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += MessagePackBinary.WriteInt64(ref bytes, offset, value.applied_at);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += MessagePackBinary.WriteInt64(ref bytes, offset, value.joined_at);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += MessagePackBinary.WriteInt64(ref bytes, offset, value.leave_at);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += MessagePackBinary.WriteInt64(ref bytes, offset, value.lastlogin);
      return offset - num;
    }

    public JSON_CombatPowerRankingGuildMember Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (JSON_CombatPowerRankingGuildMember) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      int num3 = 0;
      long num4 = 0;
      long num5 = 0;
      string str1 = (string) null;
      int num6 = 0;
      string str2 = (string) null;
      int num7 = 0;
      string str3 = (string) null;
      Json_Unit jsonUnit = (Json_Unit) null;
      long num8 = 0;
      long num9 = 0;
      long num10 = 0;
      long num11 = 0;
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
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 1:
              num4 = MessagePackBinary.ReadInt64(bytes, offset, out readSize);
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
              str3 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 8:
              jsonUnit = formatterResolver.GetFormatterWithVerify<Json_Unit>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 9:
              num8 = MessagePackBinary.ReadInt64(bytes, offset, out readSize);
              break;
            case 10:
              num9 = MessagePackBinary.ReadInt64(bytes, offset, out readSize);
              break;
            case 11:
              num10 = MessagePackBinary.ReadInt64(bytes, offset, out readSize);
              break;
            case 12:
              num11 = MessagePackBinary.ReadInt64(bytes, offset, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      JSON_CombatPowerRankingGuildMember rankingGuildMember = new JSON_CombatPowerRankingGuildMember();
      rankingGuildMember.rank = num3;
      rankingGuildMember.combat_power = num4;
      rankingGuildMember.gid = num5;
      rankingGuildMember.uid = str1;
      rankingGuildMember.role_id = num6;
      rankingGuildMember.name = str2;
      rankingGuildMember.lv = num7;
      rankingGuildMember.award_id = str3;
      rankingGuildMember.units = jsonUnit;
      rankingGuildMember.applied_at = num8;
      rankingGuildMember.joined_at = num9;
      rankingGuildMember.leave_at = num10;
      rankingGuildMember.lastlogin = num11;
      return rankingGuildMember;
    }
  }
}
