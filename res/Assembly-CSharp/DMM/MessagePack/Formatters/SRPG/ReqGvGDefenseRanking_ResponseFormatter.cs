// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.ReqGvGDefenseRanking_ResponseFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class ReqGvGDefenseRanking_ResponseFormatter : 
    IMessagePackFormatter<ReqGvGDefenseRanking.Response>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public ReqGvGDefenseRanking_ResponseFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "ranking_defense_num",
          0
        },
        {
          "totalPage",
          1
        }
      };
      this.____stringByteKeys = new byte[2][]
      {
        MessagePackBinary.GetEncodedStringBytes("ranking_defense_num"),
        MessagePackBinary.GetEncodedStringBytes("totalPage")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      ReqGvGDefenseRanking.Response value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 2);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GvGScoreRanking[]>().Serialize(ref bytes, offset, value.ranking_defense_num, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.totalPage);
      return offset - num;
    }

    public ReqGvGDefenseRanking.Response Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (ReqGvGDefenseRanking.Response) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      JSON_GvGScoreRanking[] jsonGvGscoreRankingArray = (JSON_GvGScoreRanking[]) null;
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
              jsonGvGscoreRankingArray = formatterResolver.GetFormatterWithVerify<JSON_GvGScoreRanking[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
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
      return new ReqGvGDefenseRanking.Response()
      {
        ranking_defense_num = jsonGvGscoreRankingArray,
        totalPage = num3
      };
    }
  }
}
