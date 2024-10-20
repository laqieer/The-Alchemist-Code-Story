﻿// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.JSON_GuildRaidGuildDataFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class JSON_GuildRaidGuildDataFormatter : 
    IMessagePackFormatter<JSON_GuildRaidGuildData>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public JSON_GuildRaidGuildDataFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "gid",
          0
        },
        {
          "name",
          1
        },
        {
          "guild_master",
          2
        },
        {
          "award_id",
          3
        },
        {
          "level",
          4
        },
        {
          "member_count",
          5
        },
        {
          "max_count",
          6
        }
      };
      this.____stringByteKeys = new byte[7][]
      {
        MessagePackBinary.GetEncodedStringBytes("gid"),
        MessagePackBinary.GetEncodedStringBytes("name"),
        MessagePackBinary.GetEncodedStringBytes("guild_master"),
        MessagePackBinary.GetEncodedStringBytes("award_id"),
        MessagePackBinary.GetEncodedStringBytes("level"),
        MessagePackBinary.GetEncodedStringBytes("member_count"),
        MessagePackBinary.GetEncodedStringBytes("max_count")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      JSON_GuildRaidGuildData value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 7);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.gid);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.name, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.guild_master, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.award_id, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.level);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.member_count);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.max_count);
      return offset - num;
    }

    public JSON_GuildRaidGuildData Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (JSON_GuildRaidGuildData) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      int num3 = 0;
      string str1 = (string) null;
      string str2 = (string) null;
      string str3 = (string) null;
      int num4 = 0;
      int num5 = 0;
      int num6 = 0;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num7;
        if (!this.____keyMapping.TryGetValueSafe(key, out num7))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num7)
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
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 5:
              num5 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 6:
              num6 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new JSON_GuildRaidGuildData()
      {
        gid = num3,
        name = str1,
        guild_master = str2,
        award_id = str3,
        level = num4,
        member_count = num5,
        max_count = num6
      };
    }
  }
}