﻿// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.JSON_TobiraConditionParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class JSON_TobiraConditionParamFormatter : 
    IMessagePackFormatter<JSON_TobiraConditionParam>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public JSON_TobiraConditionParamFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "conds_type",
          0
        },
        {
          "conds_iname",
          1
        }
      };
      this.____stringByteKeys = new byte[2][]
      {
        MessagePackBinary.GetEncodedStringBytes("conds_type"),
        MessagePackBinary.GetEncodedStringBytes("conds_iname")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      JSON_TobiraConditionParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 2);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.conds_type);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.conds_iname, formatterResolver);
      return offset - num;
    }

    public JSON_TobiraConditionParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (JSON_TobiraConditionParam) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      int num3 = 0;
      string str = (string) null;
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
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 1:
              str = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new JSON_TobiraConditionParam()
      {
        conds_type = num3,
        conds_iname = str
      };
    }
  }
}