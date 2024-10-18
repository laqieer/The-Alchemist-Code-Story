﻿// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.JSON_QuestClearUnlockUnitDataParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class JSON_QuestClearUnlockUnitDataParamFormatter : 
    IMessagePackFormatter<JSON_QuestClearUnlockUnitDataParam>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public JSON_QuestClearUnlockUnitDataParamFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "iname",
          0
        },
        {
          "uid",
          1
        },
        {
          "add",
          2
        },
        {
          "type",
          3
        },
        {
          "new_id",
          4
        },
        {
          "old_id",
          5
        },
        {
          "parent_id",
          6
        },
        {
          "ulv",
          7
        },
        {
          "aid",
          8
        },
        {
          "alv",
          9
        },
        {
          "qids",
          10
        },
        {
          "qcnd",
          11
        }
      };
      this.____stringByteKeys = new byte[12][]
      {
        MessagePackBinary.GetEncodedStringBytes("iname"),
        MessagePackBinary.GetEncodedStringBytes("uid"),
        MessagePackBinary.GetEncodedStringBytes("add"),
        MessagePackBinary.GetEncodedStringBytes("type"),
        MessagePackBinary.GetEncodedStringBytes("new_id"),
        MessagePackBinary.GetEncodedStringBytes("old_id"),
        MessagePackBinary.GetEncodedStringBytes("parent_id"),
        MessagePackBinary.GetEncodedStringBytes("ulv"),
        MessagePackBinary.GetEncodedStringBytes("aid"),
        MessagePackBinary.GetEncodedStringBytes("alv"),
        MessagePackBinary.GetEncodedStringBytes("qids"),
        MessagePackBinary.GetEncodedStringBytes("qcnd")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      JSON_QuestClearUnlockUnitDataParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 12);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.uid, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.add);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.type);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.new_id, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.old_id, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.parent_id, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ulv);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.aid, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.alv);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.qids, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.qcnd);
      return offset - num;
    }

    public JSON_QuestClearUnlockUnitDataParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (JSON_QuestClearUnlockUnitDataParam) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      string str1 = (string) null;
      string str2 = (string) null;
      int num3 = 0;
      int num4 = 0;
      string str3 = (string) null;
      string str4 = (string) null;
      string str5 = (string) null;
      int num5 = 0;
      string str6 = (string) null;
      int num6 = 0;
      string[] strArray = (string[]) null;
      int num7 = 0;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num8;
        if (!this.____keyMapping.TryGetValueSafe(key, out num8))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num8)
          {
            case 0:
              str1 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              str2 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 3:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 4:
              str3 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 5:
              str4 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 6:
              str5 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 7:
              num5 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 8:
              str6 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 9:
              num6 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 10:
              strArray = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 11:
              num7 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new JSON_QuestClearUnlockUnitDataParam()
      {
        iname = str1,
        uid = str2,
        add = num3,
        type = num4,
        new_id = str3,
        old_id = str4,
        parent_id = str5,
        ulv = num5,
        aid = str6,
        alv = num6,
        qids = strArray,
        qcnd = num7
      };
    }
  }
}
