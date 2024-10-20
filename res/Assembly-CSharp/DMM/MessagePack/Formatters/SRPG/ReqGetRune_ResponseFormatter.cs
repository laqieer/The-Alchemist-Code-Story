﻿// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.ReqGetRune_ResponseFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class ReqGetRune_ResponseFormatter : 
    IMessagePackFormatter<ReqGetRune.Response>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public ReqGetRune_ResponseFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "runes",
          0
        },
        {
          "rune_enforce_gauge",
          1
        },
        {
          "rune_storage",
          2
        },
        {
          "rune_storage_used",
          3
        }
      };
      this.____stringByteKeys = new byte[4][]
      {
        MessagePackBinary.GetEncodedStringBytes("runes"),
        MessagePackBinary.GetEncodedStringBytes("rune_enforce_gauge"),
        MessagePackBinary.GetEncodedStringBytes("rune_storage"),
        MessagePackBinary.GetEncodedStringBytes("rune_storage_used")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      ReqGetRune.Response value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 4);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<Json_RuneData[]>().Serialize(ref bytes, offset, value.runes, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<Json_RuneEnforceGaugeData[]>().Serialize(ref bytes, offset, value.rune_enforce_gauge, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rune_storage);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rune_storage_used);
      return offset - num;
    }

    public ReqGetRune.Response Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (ReqGetRune.Response) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      Json_RuneData[] jsonRuneDataArray = (Json_RuneData[]) null;
      Json_RuneEnforceGaugeData[] enforceGaugeDataArray = (Json_RuneEnforceGaugeData[]) null;
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
              jsonRuneDataArray = formatterResolver.GetFormatterWithVerify<Json_RuneData[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              enforceGaugeDataArray = formatterResolver.GetFormatterWithVerify<Json_RuneEnforceGaugeData[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 3:
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
      return new ReqGetRune.Response()
      {
        runes = jsonRuneDataArray,
        rune_enforce_gauge = enforceGaugeDataArray,
        rune_storage = num3,
        rune_storage_used = num4
      };
    }
  }
}