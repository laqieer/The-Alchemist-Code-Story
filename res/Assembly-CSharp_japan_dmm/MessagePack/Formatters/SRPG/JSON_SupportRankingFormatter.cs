﻿// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.JSON_SupportRankingFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class JSON_SupportRankingFormatter : 
    IMessagePackFormatter<JSON_SupportRanking>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public JSON_SupportRankingFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "rank",
          0
        },
        {
          "score",
          1
        },
        {
          "uid",
          2
        },
        {
          "lv",
          3
        },
        {
          "name",
          4
        },
        {
          "selected_award",
          5
        },
        {
          "unit",
          6
        },
        {
          "guild",
          7
        }
      };
      this.____stringByteKeys = new byte[8][]
      {
        MessagePackBinary.GetEncodedStringBytes("rank"),
        MessagePackBinary.GetEncodedStringBytes("score"),
        MessagePackBinary.GetEncodedStringBytes("uid"),
        MessagePackBinary.GetEncodedStringBytes("lv"),
        MessagePackBinary.GetEncodedStringBytes("name"),
        MessagePackBinary.GetEncodedStringBytes("selected_award"),
        MessagePackBinary.GetEncodedStringBytes("unit"),
        MessagePackBinary.GetEncodedStringBytes("guild")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      JSON_SupportRanking value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 8);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rank);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.score);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.uid, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.lv);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.name, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.selected_award, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += formatterResolver.GetFormatterWithVerify<Json_Unit>().Serialize(ref bytes, offset, value.unit, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_SupportRankingGuild>().Serialize(ref bytes, offset, value.guild, formatterResolver);
      return offset - num;
    }

    public JSON_SupportRanking Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (JSON_SupportRanking) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      int num3 = 0;
      int num4 = 0;
      string str1 = (string) null;
      int num5 = 0;
      string str2 = (string) null;
      string str3 = (string) null;
      Json_Unit jsonUnit = (Json_Unit) null;
      JSON_SupportRankingGuild supportRankingGuild = (JSON_SupportRankingGuild) null;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num6;
        if (!this.____keyMapping.TryGetValueSafe(key, out num6))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num6)
          {
            case 0:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 1:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 2:
              str1 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              num5 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 4:
              str2 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 5:
              str3 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 6:
              jsonUnit = formatterResolver.GetFormatterWithVerify<Json_Unit>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 7:
              supportRankingGuild = formatterResolver.GetFormatterWithVerify<JSON_SupportRankingGuild>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new JSON_SupportRanking()
      {
        rank = num3,
        score = num4,
        uid = str1,
        lv = num5,
        name = str2,
        selected_award = str3,
        unit = jsonUnit,
        guild = supportRankingGuild
      };
    }
  }
}
