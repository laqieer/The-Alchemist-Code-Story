﻿// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.JSON_InnerObjectiveFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class JSON_InnerObjectiveFormatter : 
    IMessagePackFormatter<JSON_InnerObjective>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public JSON_InnerObjectiveFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "IsTakeoverProgress",
          0
        },
        {
          "type",
          1
        },
        {
          "val",
          2
        },
        {
          "item",
          3
        },
        {
          "num",
          4
        },
        {
          "item_type",
          5
        },
        {
          "is_takeover_progress",
          6
        }
      };
      this.____stringByteKeys = new byte[7][]
      {
        MessagePackBinary.GetEncodedStringBytes("IsTakeoverProgress"),
        MessagePackBinary.GetEncodedStringBytes("type"),
        MessagePackBinary.GetEncodedStringBytes("val"),
        MessagePackBinary.GetEncodedStringBytes("item"),
        MessagePackBinary.GetEncodedStringBytes("num"),
        MessagePackBinary.GetEncodedStringBytes("item_type"),
        MessagePackBinary.GetEncodedStringBytes("is_takeover_progress")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      JSON_InnerObjective value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 7);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsTakeoverProgress);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.type);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.val, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.item, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.num);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.item_type);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.is_takeover_progress);
      return offset - num;
    }

    public JSON_InnerObjective Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (JSON_InnerObjective) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      bool flag = false;
      int num3 = 0;
      string str1 = (string) null;
      string str2 = (string) null;
      int num4 = 0;
      int num5 = 0;
      int num6 = 0;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num7;
        if (!this.____keyMapping.TryGetValueSafe(key, out num7))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num7)
          {
            case 0:
              flag = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 1:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 2:
              str1 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              str2 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 5:
              num5 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 6:
              num6 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new JSON_InnerObjective()
      {
        type = num3,
        val = str1,
        item = str2,
        num = num4,
        item_type = num5,
        is_takeover_progress = num6
      };
    }
  }
}