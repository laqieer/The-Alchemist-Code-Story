﻿// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.Json_HikkoshiFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class Json_HikkoshiFormatter : 
    IMessagePackFormatter<Json_Hikkoshi>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public Json_HikkoshiFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "id",
          0
        },
        {
          "expired_at",
          1
        }
      };
      this.____stringByteKeys = new byte[2][]
      {
        MessagePackBinary.GetEncodedStringBytes("id"),
        MessagePackBinary.GetEncodedStringBytes("expired_at")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      Json_Hikkoshi value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 2);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.id, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += MessagePackBinary.WriteInt64(ref bytes, offset, value.expired_at);
      return offset - num;
    }

    public Json_Hikkoshi Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (Json_Hikkoshi) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      string str = (string) null;
      long num3 = 0;
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
              str = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              num3 = MessagePackBinary.ReadInt64(bytes, offset, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new Json_Hikkoshi()
      {
        id = str,
        expired_at = num3
      };
    }
  }
}
