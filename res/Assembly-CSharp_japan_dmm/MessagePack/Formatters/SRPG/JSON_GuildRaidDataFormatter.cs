// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.JSON_GuildRaidDataFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class JSON_GuildRaidDataFormatter : 
    IMessagePackFormatter<JSON_GuildRaidData>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public JSON_GuildRaidDataFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "prev",
          0
        },
        {
          "current",
          1
        },
        {
          "bp",
          2
        },
        {
          "bossinfo",
          3
        },
        {
          "can_open_area_id",
          4
        },
        {
          "refresh_wait_sec",
          5
        }
      };
      this.____stringByteKeys = new byte[6][]
      {
        MessagePackBinary.GetEncodedStringBytes("prev"),
        MessagePackBinary.GetEncodedStringBytes("current"),
        MessagePackBinary.GetEncodedStringBytes("bp"),
        MessagePackBinary.GetEncodedStringBytes("bossinfo"),
        MessagePackBinary.GetEncodedStringBytes("can_open_area_id"),
        MessagePackBinary.GetEncodedStringBytes("refresh_wait_sec")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      JSON_GuildRaidData value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 6);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GuildRaidPrev>().Serialize(ref bytes, offset, value.prev, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GuildRaidCurrent>().Serialize(ref bytes, offset, value.current, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GuildRaidBattlePoint>().Serialize(ref bytes, offset, value.bp, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GuildRaidBossInfo>().Serialize(ref bytes, offset, value.bossinfo, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.can_open_area_id);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.refresh_wait_sec);
      return offset - num;
    }

    public JSON_GuildRaidData Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (JSON_GuildRaidData) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      JSON_GuildRaidPrev jsonGuildRaidPrev = (JSON_GuildRaidPrev) null;
      JSON_GuildRaidCurrent guildRaidCurrent = (JSON_GuildRaidCurrent) null;
      JSON_GuildRaidBattlePoint guildRaidBattlePoint = (JSON_GuildRaidBattlePoint) null;
      JSON_GuildRaidBossInfo guildRaidBossInfo = (JSON_GuildRaidBossInfo) null;
      int num3 = 0;
      int num4 = 0;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num5;
        if (!this.____keyMapping.TryGetValueSafe(key, out num5))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num5)
          {
            case 0:
              jsonGuildRaidPrev = formatterResolver.GetFormatterWithVerify<JSON_GuildRaidPrev>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              guildRaidCurrent = formatterResolver.GetFormatterWithVerify<JSON_GuildRaidCurrent>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              guildRaidBattlePoint = formatterResolver.GetFormatterWithVerify<JSON_GuildRaidBattlePoint>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              guildRaidBossInfo = formatterResolver.GetFormatterWithVerify<JSON_GuildRaidBossInfo>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 5:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new JSON_GuildRaidData()
      {
        prev = jsonGuildRaidPrev,
        current = guildRaidCurrent,
        bp = guildRaidBattlePoint,
        bossinfo = guildRaidBossInfo,
        can_open_area_id = num3,
        refresh_wait_sec = num4
      };
    }
  }
}
