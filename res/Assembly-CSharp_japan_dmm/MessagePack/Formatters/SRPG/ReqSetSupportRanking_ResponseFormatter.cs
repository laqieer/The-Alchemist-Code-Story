// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.ReqSetSupportRanking_ResponseFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class ReqSetSupportRanking_ResponseFormatter : 
    IMessagePackFormatter<ReqSetSupportRanking.Response>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public ReqSetSupportRanking_ResponseFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "date",
          0
        },
        {
          "user_ranking",
          1
        },
        {
          "unit_ranking",
          2
        }
      };
      this.____stringByteKeys = new byte[3][]
      {
        MessagePackBinary.GetEncodedStringBytes("date"),
        MessagePackBinary.GetEncodedStringBytes("user_ranking"),
        MessagePackBinary.GetEncodedStringBytes("unit_ranking")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      ReqSetSupportRanking.Response value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 3);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.date, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_SupportRankingUser>().Serialize(ref bytes, offset, value.user_ranking, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_SupportRankingUnit>().Serialize(ref bytes, offset, value.unit_ranking, formatterResolver);
      return offset - num;
    }

    public ReqSetSupportRanking.Response Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (ReqSetSupportRanking.Response) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      string str = (string) null;
      JSON_SupportRankingUser supportRankingUser = (JSON_SupportRankingUser) null;
      JSON_SupportRankingUnit supportRankingUnit = (JSON_SupportRankingUnit) null;
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
              str = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              supportRankingUser = formatterResolver.GetFormatterWithVerify<JSON_SupportRankingUser>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              supportRankingUnit = formatterResolver.GetFormatterWithVerify<JSON_SupportRankingUnit>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new ReqSetSupportRanking.Response()
      {
        date = str,
        user_ranking = supportRankingUser,
        unit_ranking = supportRankingUnit
      };
    }
  }
}
