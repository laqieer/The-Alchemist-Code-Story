// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.ReqWorldRaidRanking_ResponseFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class ReqWorldRaidRanking_ResponseFormatter : 
    IMessagePackFormatter<ReqWorldRaidRanking.Response>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public ReqWorldRaidRanking_ResponseFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "worldraid_ranking",
          0
        },
        {
          "my_info",
          1
        },
        {
          "total_page",
          2
        }
      };
      this.____stringByteKeys = new byte[3][]
      {
        MessagePackBinary.GetEncodedStringBytes("worldraid_ranking"),
        MessagePackBinary.GetEncodedStringBytes("my_info"),
        MessagePackBinary.GetEncodedStringBytes("total_page")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      ReqWorldRaidRanking.Response value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 3);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_WorldRaidRankingData[]>().Serialize(ref bytes, offset, value.worldraid_ranking, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_WorldRaidRankingData>().Serialize(ref bytes, offset, value.my_info, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.total_page);
      return offset - num;
    }

    public ReqWorldRaidRanking.Response Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (ReqWorldRaidRanking.Response) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      JSON_WorldRaidRankingData[] worldRaidRankingDataArray = (JSON_WorldRaidRankingData[]) null;
      JSON_WorldRaidRankingData worldRaidRankingData = (JSON_WorldRaidRankingData) null;
      int num3 = 0;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num4;
        if (!this.____keyMapping.TryGetValueSafe(key, out num4))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num4)
          {
            case 0:
              worldRaidRankingDataArray = formatterResolver.GetFormatterWithVerify<JSON_WorldRaidRankingData[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              worldRaidRankingData = formatterResolver.GetFormatterWithVerify<JSON_WorldRaidRankingData>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new ReqWorldRaidRanking.Response()
      {
        worldraid_ranking = worldRaidRankingDataArray,
        my_info = worldRaidRankingData,
        total_page = num3
      };
    }
  }
}
