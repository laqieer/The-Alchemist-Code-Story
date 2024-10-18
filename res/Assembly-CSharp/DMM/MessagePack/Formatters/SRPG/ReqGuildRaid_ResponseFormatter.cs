// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.ReqGuildRaid_ResponseFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class ReqGuildRaid_ResponseFormatter : 
    IMessagePackFormatter<ReqGuildRaid.Response>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public ReqGuildRaid_ResponseFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "facilities",
          0
        },
        {
          "prev",
          1
        },
        {
          "current",
          2
        },
        {
          "bp",
          3
        },
        {
          "boss_info",
          4
        },
        {
          "refresh_wait_sec",
          5
        },
        {
          "receive_mail_count",
          6
        },
        {
          "selected_units",
          7
        },
        {
          "forced_deck",
          8
        },
        {
          "ranking",
          9
        },
        {
          "battle_log",
          10
        }
      };
      this.____stringByteKeys = new byte[11][]
      {
        MessagePackBinary.GetEncodedStringBytes("facilities"),
        MessagePackBinary.GetEncodedStringBytes("prev"),
        MessagePackBinary.GetEncodedStringBytes("current"),
        MessagePackBinary.GetEncodedStringBytes("bp"),
        MessagePackBinary.GetEncodedStringBytes("boss_info"),
        MessagePackBinary.GetEncodedStringBytes("refresh_wait_sec"),
        MessagePackBinary.GetEncodedStringBytes("receive_mail_count"),
        MessagePackBinary.GetEncodedStringBytes("selected_units"),
        MessagePackBinary.GetEncodedStringBytes("forced_deck"),
        MessagePackBinary.GetEncodedStringBytes("ranking"),
        MessagePackBinary.GetEncodedStringBytes("battle_log")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      ReqGuildRaid.Response value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 11);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GuildFacilityData[]>().Serialize(ref bytes, offset, value.facilities, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GuildRaidPrev>().Serialize(ref bytes, offset, value.prev, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GuildRaidCurrent>().Serialize(ref bytes, offset, value.current, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GuildRaidBattlePoint>().Serialize(ref bytes, offset, value.bp, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GuildRaidBossInfo>().Serialize(ref bytes, offset, value.boss_info, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.refresh_wait_sec);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.receive_mail_count);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.selected_units, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GuildRaidDeck>().Serialize(ref bytes, offset, value.forced_deck, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ranking);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GuildRaidBattleLog>().Serialize(ref bytes, offset, value.battle_log, formatterResolver);
      return offset - num;
    }

    public ReqGuildRaid.Response Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (ReqGuildRaid.Response) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      JSON_GuildFacilityData[] guildFacilityDataArray = (JSON_GuildFacilityData[]) null;
      JSON_GuildRaidPrev jsonGuildRaidPrev = (JSON_GuildRaidPrev) null;
      JSON_GuildRaidCurrent guildRaidCurrent = (JSON_GuildRaidCurrent) null;
      JSON_GuildRaidBattlePoint guildRaidBattlePoint = (JSON_GuildRaidBattlePoint) null;
      JSON_GuildRaidBossInfo guildRaidBossInfo = (JSON_GuildRaidBossInfo) null;
      int num3 = 0;
      int num4 = 0;
      string[] strArray = (string[]) null;
      JSON_GuildRaidDeck jsonGuildRaidDeck = (JSON_GuildRaidDeck) null;
      int num5 = 0;
      JSON_GuildRaidBattleLog guildRaidBattleLog = (JSON_GuildRaidBattleLog) null;
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
              guildFacilityDataArray = formatterResolver.GetFormatterWithVerify<JSON_GuildFacilityData[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              jsonGuildRaidPrev = formatterResolver.GetFormatterWithVerify<JSON_GuildRaidPrev>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              guildRaidCurrent = formatterResolver.GetFormatterWithVerify<JSON_GuildRaidCurrent>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              guildRaidBattlePoint = formatterResolver.GetFormatterWithVerify<JSON_GuildRaidBattlePoint>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              guildRaidBossInfo = formatterResolver.GetFormatterWithVerify<JSON_GuildRaidBossInfo>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 5:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 6:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 7:
              strArray = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 8:
              jsonGuildRaidDeck = formatterResolver.GetFormatterWithVerify<JSON_GuildRaidDeck>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 9:
              num5 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 10:
              guildRaidBattleLog = formatterResolver.GetFormatterWithVerify<JSON_GuildRaidBattleLog>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new ReqGuildRaid.Response()
      {
        facilities = guildFacilityDataArray,
        prev = jsonGuildRaidPrev,
        current = guildRaidCurrent,
        bp = guildRaidBattlePoint,
        boss_info = guildRaidBossInfo,
        refresh_wait_sec = num3,
        receive_mail_count = num4,
        selected_units = strArray,
        forced_deck = jsonGuildRaidDeck,
        ranking = num5,
        battle_log = guildRaidBattleLog
      };
    }
  }
}
