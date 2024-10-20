﻿// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.RuneParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class RuneParamFormatter : IMessagePackFormatter<RuneParam>, IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public RuneParamFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "ItemParam",
          0
        },
        {
          "RuneSetEff",
          1
        },
        {
          "iname",
          2
        },
        {
          "item_iname",
          3
        },
        {
          "slot_index",
          4
        },
        {
          "seteff_type",
          5
        }
      };
      this.____stringByteKeys = new byte[6][]
      {
        MessagePackBinary.GetEncodedStringBytes("ItemParam"),
        MessagePackBinary.GetEncodedStringBytes("RuneSetEff"),
        MessagePackBinary.GetEncodedStringBytes("iname"),
        MessagePackBinary.GetEncodedStringBytes("item_iname"),
        MessagePackBinary.GetEncodedStringBytes("slot_index"),
        MessagePackBinary.GetEncodedStringBytes("seteff_type")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      RuneParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 6);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<ItemParam>().Serialize(ref bytes, offset, value.ItemParam, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<RuneSetEff>().Serialize(ref bytes, offset, value.RuneSetEff, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.item_iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<RuneSlotIndex>().Serialize(ref bytes, offset, value.slot_index, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.seteff_type);
      return offset - num;
    }

    public RuneParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (RuneParam) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      ItemParam itemParam = (ItemParam) null;
      RuneSetEff runeSetEff = (RuneSetEff) null;
      string str1 = (string) null;
      string str2 = (string) null;
      RuneSlotIndex runeSlotIndex = new RuneSlotIndex();
      int num3 = 0;
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
              itemParam = formatterResolver.GetFormatterWithVerify<ItemParam>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              runeSetEff = formatterResolver.GetFormatterWithVerify<RuneSetEff>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              str1 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              str2 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              runeSlotIndex = formatterResolver.GetFormatterWithVerify<RuneSlotIndex>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 5:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new RuneParam()
      {
        iname = str1,
        item_iname = str2,
        slot_index = runeSlotIndex,
        seteff_type = num3
      };
    }
  }
}