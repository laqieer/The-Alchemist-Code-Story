﻿// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.JSON_HighlightParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class JSON_HighlightParamFormatter : 
    IMessagePackFormatter<JSON_HighlightParam>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public JSON_HighlightParamFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "iname",
          0
        },
        {
          "begin_at",
          1
        },
        {
          "end_at",
          2
        },
        {
          "data_end_at",
          3
        },
        {
          "resources",
          4
        },
        {
          "gift",
          5
        }
      };
      this.____stringByteKeys = new byte[6][]
      {
        MessagePackBinary.GetEncodedStringBytes("iname"),
        MessagePackBinary.GetEncodedStringBytes("begin_at"),
        MessagePackBinary.GetEncodedStringBytes("end_at"),
        MessagePackBinary.GetEncodedStringBytes("data_end_at"),
        MessagePackBinary.GetEncodedStringBytes("resources"),
        MessagePackBinary.GetEncodedStringBytes("gift")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      JSON_HighlightParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 6);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.begin_at, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.end_at, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.data_end_at, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_HighlightResource[]>().Serialize(ref bytes, offset, value.resources, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.gift, formatterResolver);
      return offset - num;
    }

    public JSON_HighlightParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (JSON_HighlightParam) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      string str1 = (string) null;
      string str2 = (string) null;
      string str3 = (string) null;
      string str4 = (string) null;
      JSON_HighlightResource[] highlightResourceArray = (JSON_HighlightResource[]) null;
      string str5 = (string) null;
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
              str1 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              str2 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              str3 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              str4 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              highlightResourceArray = formatterResolver.GetFormatterWithVerify<JSON_HighlightResource[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 5:
              str5 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new JSON_HighlightParam()
      {
        iname = str1,
        begin_at = str2,
        end_at = str3,
        data_end_at = str4,
        resources = highlightResourceArray,
        gift = str5
      };
    }
  }
}
