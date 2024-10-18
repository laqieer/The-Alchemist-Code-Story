// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.RuneMaterialFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class RuneMaterialFormatter : 
    IMessagePackFormatter<RuneMaterial>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public RuneMaterialFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "rarity",
          0
        },
        {
          "enhance_probability",
          1
        },
        {
          "enh_cost",
          2
        },
        {
          "evo_cost",
          3
        },
        {
          "disassembly_zeny",
          4
        },
        {
          "disassembly",
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
        MessagePackBinary.GetEncodedStringBytes("enhance_probability"),
        MessagePackBinary.GetEncodedStringBytes("enh_cost"),
        MessagePackBinary.GetEncodedStringBytes("evo_cost"),
        MessagePackBinary.GetEncodedStringBytes("disassembly_zeny"),
        MessagePackBinary.GetEncodedStringBytes("disassembly"),
        MessagePackBinary.GetEncodedStringBytes("magnification"),
        MessagePackBinary.GetEncodedStringBytes("reset_base_param_costs"),
        MessagePackBinary.GetEncodedStringBytes("reset_evo_status_costs"),
        MessagePackBinary.GetEncodedStringBytes("param_enh_evo_costs")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      RuneMaterial value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 10);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.rarity);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<short[]>().Serialize(ref bytes, offset, value.enhance_probability, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<RuneCost[]>().Serialize(ref bytes, offset, value.enh_cost, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<RuneCost[]>().Serialize(ref bytes, offset, value.evo_cost, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.disassembly_zeny);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<RuneDisassembly>().Serialize(ref bytes, offset, value.disassembly, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.magnification, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += formatterResolver.GetFormatterWithVerify<RuneCost[]>().Serialize(ref bytes, offset, value.reset_base_param_costs, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += formatterResolver.GetFormatterWithVerify<RuneCost[]>().Serialize(ref bytes, offset, value.reset_evo_status_costs, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += formatterResolver.GetFormatterWithVerify<RuneCost[]>().Serialize(ref bytes, offset, value.param_enh_evo_costs, formatterResolver);
      return offset - num;
    }

    public RuneMaterial Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (RuneMaterial) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      short num3 = 0;
      short[] numArray1 = (short[]) null;
      RuneCost[] runeCostArray1 = (RuneCost[]) null;
      RuneCost[] runeCostArray2 = (RuneCost[]) null;
      int num4 = 0;
      RuneDisassembly runeDisassembly = (RuneDisassembly) null;
      int[] numArray2 = (int[]) null;
      RuneCost[] runeCostArray3 = (RuneCost[]) null;
      RuneCost[] runeCostArray4 = (RuneCost[]) null;
      RuneCost[] runeCostArray5 = (RuneCost[]) null;
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
              num3 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 1:
              numArray1 = formatterResolver.GetFormatterWithVerify<short[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              runeCostArray1 = formatterResolver.GetFormatterWithVerify<RuneCost[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              runeCostArray2 = formatterResolver.GetFormatterWithVerify<RuneCost[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 5:
              runeDisassembly = formatterResolver.GetFormatterWithVerify<RuneDisassembly>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 6:
              numArray2 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 7:
              runeCostArray3 = formatterResolver.GetFormatterWithVerify<RuneCost[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 8:
              runeCostArray4 = formatterResolver.GetFormatterWithVerify<RuneCost[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 9:
              runeCostArray5 = formatterResolver.GetFormatterWithVerify<RuneCost[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new RuneMaterial()
      {
        rarity = num3,
        enhance_probability = numArray1,
        enh_cost = runeCostArray1,
        evo_cost = runeCostArray2,
        disassembly_zeny = num4,
        disassembly = runeDisassembly,
        magnification = numArray2,
        reset_base_param_costs = runeCostArray3,
        reset_evo_status_costs = runeCostArray4,
        param_enh_evo_costs = runeCostArray5
      };
    }
  }
}
