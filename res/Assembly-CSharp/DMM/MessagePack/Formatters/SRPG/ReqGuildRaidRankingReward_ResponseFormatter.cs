// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.ReqGuildRaidRankingReward_ResponseFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class ReqGuildRaidRankingReward_ResponseFormatter : 
    IMessagePackFormatter<ReqGuildRaidRankingReward.Response>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public ReqGuildRaidRankingReward_ResponseFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "status",
          0
        },
        {
          "period_id",
          1
        },
        {
          "my_guild_info",
          2
        }
      };
      this.____stringByteKeys = new byte[3][]
      {
        MessagePackBinary.GetEncodedStringBytes("status"),
        MessagePackBinary.GetEncodedStringBytes("period_id"),
        MessagePackBinary.GetEncodedStringBytes("my_guild_info")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      ReqGuildRaidRankingReward.Response value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 3);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.status);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.period_id);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GuildRaidGuildRanking>().Serialize(ref bytes, offset, value.my_guild_info, formatterResolver);
      return offset - num;
    }

    public ReqGuildRaidRankingReward.Response Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (ReqGuildRaidRankingReward.Response) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      int num3 = 0;
      int num4 = 0;
      JSON_GuildRaidGuildRanking raidGuildRanking = (JSON_GuildRaidGuildRanking) null;
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
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 1:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 2:
              raidGuildRanking = formatterResolver.GetFormatterWithVerify<JSON_GuildRaidGuildRanking>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new ReqGuildRaidRankingReward.Response()
      {
        status = num3,
        period_id = num4,
        my_guild_info = raidGuildRanking
      };
    }
  }
}
