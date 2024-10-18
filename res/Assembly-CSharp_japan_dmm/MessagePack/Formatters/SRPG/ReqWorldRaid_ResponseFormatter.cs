// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.ReqWorldRaid_ResponseFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class ReqWorldRaid_ResponseFormatter : 
    IMessagePackFormatter<ReqWorldRaid.Response>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public ReqWorldRaid_ResponseFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "bosses",
          0
        },
        {
          "total_challenge",
          1
        },
        {
          "logs",
          2
        },
        {
          "refresh_wait_sec",
          3
        },
        {
          "auto_refresh_wait_sec",
          4
        }
      };
      this.____stringByteKeys = new byte[5][]
      {
        MessagePackBinary.GetEncodedStringBytes("bosses"),
        MessagePackBinary.GetEncodedStringBytes("total_challenge"),
        MessagePackBinary.GetEncodedStringBytes("logs"),
        MessagePackBinary.GetEncodedStringBytes("refresh_wait_sec"),
        MessagePackBinary.GetEncodedStringBytes("auto_refresh_wait_sec")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      ReqWorldRaid.Response value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 5);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_WorldRaidBossChallengedData[]>().Serialize(ref bytes, offset, value.bosses, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.total_challenge);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_WorldRaidLogData[]>().Serialize(ref bytes, offset, value.logs, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.refresh_wait_sec);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.auto_refresh_wait_sec);
      return offset - num;
    }

    public ReqWorldRaid.Response Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (ReqWorldRaid.Response) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      JSON_WorldRaidBossChallengedData[] bossChallengedDataArray = (JSON_WorldRaidBossChallengedData[]) null;
      int num3 = 0;
      JSON_WorldRaidLogData[] worldRaidLogDataArray = (JSON_WorldRaidLogData[]) null;
      int num4 = 0;
      int num5 = 0;
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
              bossChallengedDataArray = formatterResolver.GetFormatterWithVerify<JSON_WorldRaidBossChallengedData[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 2:
              worldRaidLogDataArray = formatterResolver.GetFormatterWithVerify<JSON_WorldRaidLogData[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 4:
              num5 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new ReqWorldRaid.Response()
      {
        bosses = bossChallengedDataArray,
        total_challenge = num3,
        logs = worldRaidLogDataArray,
        refresh_wait_sec = num4,
        auto_refresh_wait_sec = num5
      };
    }
  }
}
