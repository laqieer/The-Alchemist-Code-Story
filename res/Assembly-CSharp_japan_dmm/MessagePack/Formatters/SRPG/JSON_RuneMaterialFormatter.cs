﻿// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.JSON_RuneMaterialFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class JSON_RuneMaterialFormatter : 
    IMessagePackFormatter<JSON_RuneMaterial>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public JSON_RuneMaterialFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "rarity",
          0
        },
        {
          "enh_cost",
          1
        },
        {
          "evo_cost",
          2
        },
        {
          "disassembly_zeny",
          3
        },
        {
          "disassembly",
          4
        },
        {
          "enhance_probability",
          5
        },
        {
          "magnification",
          6
        },
        {
          "reset_base_param_costs",
          7
        },
        {
          "reset_evo_status_costs",
          8
        },
        {
          "param_enh_evo_costs",
          9
        }
      };
      this.____stringByteKeys = new byte[10][]
      {
        MessagePackBinary.GetEncodedStringBytes("rarity"),
        MessagePackBinary.GetEncodedStringBytes("enh_cost"),
        MessagePackBinary.GetEncodedStringBytes("evo_cost"),
        MessagePackBinary.GetEncodedStringBytes("disassembly_zeny"),
        MessagePackBinary.GetEncodedStringBytes("disassembly"),
        MessagePackBinary.GetEncodedStringBytes("enhance_probability"),
        MessagePackBinary.GetEncodedStringBytes("magnification"),
        MessagePackBinary.GetEncodedStringBytes("reset_base_param_costs"),
        MessagePackBinary.GetEncodedStringBytes("reset_evo_status_costs"),
        MessagePackBinary.GetEncodedStringBytes("param_enh_evo_costs")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      JSON_RuneMaterial value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 10);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rarity);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.enh_cost, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.evo_cost, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.disassembly_zeny);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_RuneDisassembly[]>().Serialize(ref bytes, offset, value.disassembly, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.enhance_probability, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.magnification, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.reset_base_param_costs, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.reset_evo_status_costs, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.param_enh_evo_costs, formatterResolver);
      return offset - num;
    }

    public JSON_RuneMaterial Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (JSON_RuneMaterial) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      int num3 = 0;
      string[] strArray1 = (string[]) null;
      string[] strArray2 = (string[]) null;
      int num4 = 0;
      JSON_RuneDisassembly[] jsonRuneDisassemblyArray = (JSON_RuneDisassembly[]) null;
      int[] numArray1 = (int[]) null;
      int[] numArray2 = (int[]) null;
      string[] strArray3 = (string[]) null;
      string[] strArray4 = (string[]) null;
      string[] strArray5 = (string[]) null;
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
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 1:
              strArray1 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              strArray2 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 4:
              jsonRuneDisassemblyArray = formatterResolver.GetFormatterWithVerify<JSON_RuneDisassembly[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 5:
              numArray1 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 6:
              numArray2 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 7:
              strArray3 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 8:
              strArray4 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 9:
              strArray5 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new JSON_RuneMaterial()
      {
        rarity = num3,
        enh_cost = strArray1,
        evo_cost = strArray2,
        disassembly_zeny = num4,
        disassembly = jsonRuneDisassemblyArray,
        enhance_probability = numArray1,
        magnification = numArray2,
        reset_base_param_costs = strArray3,
        reset_evo_status_costs = strArray4,
        param_enh_evo_costs = strArray5
      };
    }
  }
}
