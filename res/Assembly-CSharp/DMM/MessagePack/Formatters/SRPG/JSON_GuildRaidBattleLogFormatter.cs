﻿// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.JSON_GuildRaidBattleLogFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class JSON_GuildRaidBattleLogFormatter : 
    IMessagePackFormatter<JSON_GuildRaidBattleLog>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public JSON_GuildRaidBattleLogFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "message",
          0
        },
        {
          "report_id",
          1
        },
        {
          "posted_at",
          2
        }
      };
      this.____stringByteKeys = new byte[3][]
      {
        MessagePackBinary.GetEncodedStringBytes("message"),
        MessagePackBinary.GetEncodedStringBytes("report_id"),
        MessagePackBinary.GetEncodedStringBytes("posted_at")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      JSON_GuildRaidBattleLog value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 3);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.message, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.report_id);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.posted_at);
      return offset - num;
    }

    public JSON_GuildRaidBattleLog Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (JSON_GuildRaidBattleLog) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      string str = (string) null;
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
              str = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 2:
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
      return new JSON_GuildRaidBattleLog()
      {
        message = str,
        report_id = num3,
        posted_at = num4
      };
    }
  }
}
