// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.ReqGuildRaidInfo_ResponseFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class ReqGuildRaidInfo_ResponseFormatter : 
    IMessagePackFormatter<ReqGuildRaidInfo.Response>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public ReqGuildRaidInfo_ResponseFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "boss_info",
          0
        },
        {
          "players_challenging",
          1
        }
      };
      this.____stringByteKeys = new byte[2][]
      {
        MessagePackBinary.GetEncodedStringBytes("boss_info"),
        MessagePackBinary.GetEncodedStringBytes("players_challenging")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      ReqGuildRaidInfo.Response value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 2);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GuildRaidBossInfo>().Serialize(ref bytes, offset, value.boss_info, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GuildRaidChallengingPlayer[]>().Serialize(ref bytes, offset, value.players_challenging, formatterResolver);
      return offset - num;
    }

    public ReqGuildRaidInfo.Response Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (ReqGuildRaidInfo.Response) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      JSON_GuildRaidBossInfo guildRaidBossInfo = (JSON_GuildRaidBossInfo) null;
      JSON_GuildRaidChallengingPlayer[] challengingPlayerArray = (JSON_GuildRaidChallengingPlayer[]) null;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num3;
        if (!this.____keyMapping.TryGetValueSafe(key, out num3))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num3)
          {
            case 0:
              guildRaidBossInfo = formatterResolver.GetFormatterWithVerify<JSON_GuildRaidBossInfo>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              challengingPlayerArray = formatterResolver.GetFormatterWithVerify<JSON_GuildRaidChallengingPlayer[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new ReqGuildRaidInfo.Response()
      {
        boss_info = guildRaidBossInfo,
        players_challenging = challengingPlayerArray
      };
    }
  }
}
