// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.ReqGuildAttend_ResponseFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class ReqGuildAttend_ResponseFormatter : 
    IMessagePackFormatter<ReqGuildAttend.Response>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public ReqGuildAttend_ResponseFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "yesterday_attendance",
          0
        },
        {
          "attend_guild_member_uids",
          1
        },
        {
          "status",
          2
        },
        {
          "rewards",
          3
        },
        {
          "player",
          4
        },
        {
          "items",
          5
        },
        {
          "cards",
          6
        },
        {
          "runes",
          7
        },
        {
          "rune_storage_used",
          8
        },
        {
          "guild_trophies",
          9
        }
      };
      this.____stringByteKeys = new byte[10][]
      {
        MessagePackBinary.GetEncodedStringBytes("yesterday_attendance"),
        MessagePackBinary.GetEncodedStringBytes("attend_guild_member_uids"),
        MessagePackBinary.GetEncodedStringBytes("status"),
        MessagePackBinary.GetEncodedStringBytes("rewards"),
        MessagePackBinary.GetEncodedStringBytes("player"),
        MessagePackBinary.GetEncodedStringBytes("items"),
        MessagePackBinary.GetEncodedStringBytes("cards"),
        MessagePackBinary.GetEncodedStringBytes("runes"),
        MessagePackBinary.GetEncodedStringBytes("rune_storage_used"),
        MessagePackBinary.GetEncodedStringBytes("guild_trophies")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      ReqGuildAttend.Response value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 10);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.yesterday_attendance);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.attend_guild_member_uids, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.status);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GuildAttendReward[]>().Serialize(ref bytes, offset, value.rewards, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<Json_PlayerData>().Serialize(ref bytes, offset, value.player, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<Json_Item[]>().Serialize(ref bytes, offset, value.items, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_ConceptCard[]>().Serialize(ref bytes, offset, value.cards, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += formatterResolver.GetFormatterWithVerify<Json_RuneData[]>().Serialize(ref bytes, offset, value.runes, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rune_storage_used);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_TrophyProgress[]>().Serialize(ref bytes, offset, value.guild_trophies, formatterResolver);
      return offset - num;
    }

    public ReqGuildAttend.Response Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (ReqGuildAttend.Response) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      int num3 = 0;
      string[] strArray = (string[]) null;
      int num4 = 0;
      JSON_GuildAttendReward[] guildAttendRewardArray = (JSON_GuildAttendReward[]) null;
      Json_PlayerData jsonPlayerData = (Json_PlayerData) null;
      Json_Item[] jsonItemArray = (Json_Item[]) null;
      JSON_ConceptCard[] jsonConceptCardArray = (JSON_ConceptCard[]) null;
      Json_RuneData[] jsonRuneDataArray = (Json_RuneData[]) null;
      int num5 = 0;
      JSON_TrophyProgress[] jsonTrophyProgressArray = (JSON_TrophyProgress[]) null;
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
              strArray = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 3:
              guildAttendRewardArray = formatterResolver.GetFormatterWithVerify<JSON_GuildAttendReward[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              jsonPlayerData = formatterResolver.GetFormatterWithVerify<Json_PlayerData>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 5:
              jsonItemArray = formatterResolver.GetFormatterWithVerify<Json_Item[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 6:
              jsonConceptCardArray = formatterResolver.GetFormatterWithVerify<JSON_ConceptCard[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 7:
              jsonRuneDataArray = formatterResolver.GetFormatterWithVerify<Json_RuneData[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 8:
              num5 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 9:
              jsonTrophyProgressArray = formatterResolver.GetFormatterWithVerify<JSON_TrophyProgress[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new ReqGuildAttend.Response()
      {
        yesterday_attendance = num3,
        attend_guild_member_uids = strArray,
        status = num4,
        rewards = guildAttendRewardArray,
        player = jsonPlayerData,
        items = jsonItemArray,
        cards = jsonConceptCardArray,
        runes = jsonRuneDataArray,
        rune_storage_used = num5,
        guild_trophies = jsonTrophyProgressArray
      };
    }
  }
}
