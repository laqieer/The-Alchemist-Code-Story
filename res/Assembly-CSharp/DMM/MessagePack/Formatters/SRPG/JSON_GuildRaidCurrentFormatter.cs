// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.JSON_GuildRaidCurrentFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class JSON_GuildRaidCurrentFormatter : 
    IMessagePackFormatter<JSON_GuildRaidCurrent>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public JSON_GuildRaidCurrentFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "period_id",
          0
        },
        {
          "round",
          1
        },
        {
          "is_finish",
          2
        }
      };
      this.____stringByteKeys = new byte[3][]
      {
        MessagePackBinary.GetEncodedStringBytes("period_id"),
        MessagePackBinary.GetEncodedStringBytes("round"),
        MessagePackBinary.GetEncodedStringBytes("is_finish")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      JSON_GuildRaidCurrent value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 3);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.period_id);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.round);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.is_finish);
      return offset - num;
    }

    public JSON_GuildRaidCurrent Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (JSON_GuildRaidCurrent) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      int num3 = 0;
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
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 1:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 2:
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
      return new JSON_GuildRaidCurrent()
      {
        period_id = num3,
        round = num4,
        is_finish = num5
      };
    }
  }
}
