// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.JSON_GvGLeagueViewGuildFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class JSON_GvGLeagueViewGuildFormatter : 
    IMessagePackFormatter<JSON_GvGLeagueViewGuild>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public JSON_GvGLeagueViewGuildFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "league",
          0
        },
        {
          "id",
          1
        },
        {
          "name",
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
          "count",
          5
        },
        {
          "max_count",
          6
        },
        {
          "guild_master",
          7
        },
        {
          "created_at",
          8
        }
      };
      this.____stringByteKeys = new byte[9][]
      {
        MessagePackBinary.GetEncodedStringBytes("league"),
        MessagePackBinary.GetEncodedStringBytes("id"),
        MessagePackBinary.GetEncodedStringBytes("name"),
        MessagePackBinary.GetEncodedStringBytes("award_id"),
        MessagePackBinary.GetEncodedStringBytes("level"),
        MessagePackBinary.GetEncodedStringBytes("count"),
        MessagePackBinary.GetEncodedStringBytes("max_count"),
        MessagePackBinary.GetEncodedStringBytes("guild_master"),
        MessagePackBinary.GetEncodedStringBytes("created_at")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      JSON_GvGLeagueViewGuild value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 9);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GvGLeagueData>().Serialize(ref bytes, offset, value.league, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.id);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.name, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.award_id, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.level);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.count);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.max_count);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.guild_master, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += MessagePackBinary.WriteInt64(ref bytes, offset, value.created_at);
      return offset - num;
    }

    public JSON_GvGLeagueViewGuild Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (JSON_GvGLeagueViewGuild) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      JSON_GvGLeagueData jsonGvGleagueData = (JSON_GvGLeagueData) null;
      int num3 = 0;
      string str1 = (string) null;
      string str2 = (string) null;
      int num4 = 0;
      int num5 = 0;
      int num6 = 0;
      string str3 = (string) null;
      long num7 = 0;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num8;
        if (!this.____keyMapping.TryGetValueSafe(key, out num8))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num8)
          {
            case 0:
              jsonGvGleagueData = formatterResolver.GetFormatterWithVerify<JSON_GvGLeagueData>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 2:
              str1 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              str2 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
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
            case 7:
              str3 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 8:
              num7 = MessagePackBinary.ReadInt64(bytes, offset, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      JSON_GvGLeagueViewGuild gleagueViewGuild = new JSON_GvGLeagueViewGuild();
      gleagueViewGuild.league = jsonGvGleagueData;
      gleagueViewGuild.id = num3;
      gleagueViewGuild.name = str1;
      gleagueViewGuild.award_id = str2;
      gleagueViewGuild.level = num4;
      gleagueViewGuild.count = num5;
      gleagueViewGuild.max_count = num6;
      gleagueViewGuild.guild_master = str3;
      gleagueViewGuild.created_at = num7;
      return gleagueViewGuild;
    }
  }
}
