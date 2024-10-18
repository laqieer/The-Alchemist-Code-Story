// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.JSON_SupportRankingUserFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class JSON_SupportRankingUserFormatter : 
    IMessagePackFormatter<JSON_SupportRankingUser>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public JSON_SupportRankingUserFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "my_info",
          0
        },
        {
          "ranking",
          1
        }
      };
      this.____stringByteKeys = new byte[2][]
      {
        MessagePackBinary.GetEncodedStringBytes("my_info"),
        MessagePackBinary.GetEncodedStringBytes("ranking")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      JSON_SupportRankingUser value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 2);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_SupportMyInfo>().Serialize(ref bytes, offset, value.my_info, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_SupportRanking[]>().Serialize(ref bytes, offset, value.ranking, formatterResolver);
      return offset - num;
    }

    public JSON_SupportRankingUser Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (JSON_SupportRankingUser) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      JSON_SupportMyInfo jsonSupportMyInfo = (JSON_SupportMyInfo) null;
      JSON_SupportRanking[] jsonSupportRankingArray = (JSON_SupportRanking[]) null;
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
              jsonSupportMyInfo = formatterResolver.GetFormatterWithVerify<JSON_SupportMyInfo>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              jsonSupportRankingArray = formatterResolver.GetFormatterWithVerify<JSON_SupportRanking[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new JSON_SupportRankingUser()
      {
        my_info = jsonSupportMyInfo,
        ranking = jsonSupportRankingArray
      };
    }
  }
}
