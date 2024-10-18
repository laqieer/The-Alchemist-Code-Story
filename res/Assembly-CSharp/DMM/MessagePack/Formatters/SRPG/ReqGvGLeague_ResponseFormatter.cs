// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.ReqGvGLeague_ResponseFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class ReqGvGLeague_ResponseFormatter : 
    IMessagePackFormatter<ReqGvGLeague.Response>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public ReqGvGLeague_ResponseFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "guilds",
          0
        },
        {
          "total_count",
          1
        },
        {
          "my_league",
          2
        }
      };
      this.____stringByteKeys = new byte[3][]
      {
        MessagePackBinary.GetEncodedStringBytes("guilds"),
        MessagePackBinary.GetEncodedStringBytes("total_count"),
        MessagePackBinary.GetEncodedStringBytes("my_league")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      ReqGvGLeague.Response value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 3);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GvGLeagueViewGuild[]>().Serialize(ref bytes, offset, value.guilds, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.total_count);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GvGLeagueData>().Serialize(ref bytes, offset, value.my_league, formatterResolver);
      return offset - num;
    }

    public ReqGvGLeague.Response Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (ReqGvGLeague.Response) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      JSON_GvGLeagueViewGuild[] gleagueViewGuildArray = (JSON_GvGLeagueViewGuild[]) null;
      int num3 = 0;
      JSON_GvGLeagueData jsonGvGleagueData = (JSON_GvGLeagueData) null;
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
              gleagueViewGuildArray = formatterResolver.GetFormatterWithVerify<JSON_GvGLeagueViewGuild[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 2:
              jsonGvGleagueData = formatterResolver.GetFormatterWithVerify<JSON_GvGLeagueData>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new ReqGvGLeague.Response()
      {
        guilds = gleagueViewGuildArray,
        total_count = num3,
        my_league = jsonGvGleagueData
      };
    }
  }
}
