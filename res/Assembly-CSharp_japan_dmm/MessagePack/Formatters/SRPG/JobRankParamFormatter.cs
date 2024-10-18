// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.JobRankParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class JobRankParamFormatter : 
    IMessagePackFormatter<JobRankParam>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public JobRankParamFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "JobChangeCost",
          0
        },
        {
          "JobChangeItems",
          1
        },
        {
          "JobChangeItemNums",
          2
        },
        {
          "cost",
          3
        },
        {
          "equips",
          4
        },
        {
          "buff_list",
          5
        },
        {
          "learnings",
          6
        }
      };
      this.____stringByteKeys = new byte[7][]
      {
        MessagePackBinary.GetEncodedStringBytes("JobChangeCost"),
        MessagePackBinary.GetEncodedStringBytes("JobChangeItems"),
        MessagePackBinary.GetEncodedStringBytes("JobChangeItemNums"),
        MessagePackBinary.GetEncodedStringBytes("cost"),
        MessagePackBinary.GetEncodedStringBytes("equips"),
        MessagePackBinary.GetEncodedStringBytes("buff_list"),
        MessagePackBinary.GetEncodedStringBytes("learnings")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      JobRankParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 7);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.JobChangeCost);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.JobChangeItems, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.JobChangeItemNums, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.cost);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.equips, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<BuffEffect.BuffValues[]>().Serialize(ref bytes, offset, value.buff_list, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += formatterResolver.GetFormatterWithVerify<OString[]>().Serialize(ref bytes, offset, value.learnings, formatterResolver);
      return offset - num;
    }

    public JobRankParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (JobRankParam) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      int num3 = 0;
      string[] strArray1 = (string[]) null;
      int[] numArray = (int[]) null;
      int num4 = 0;
      string[] strArray2 = (string[]) null;
      BuffEffect.BuffValues[] buffValuesArray = (BuffEffect.BuffValues[]) null;
      OString[] ostringArray = (OString[]) null;
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
              numArray = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 4:
              strArray2 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 5:
              buffValuesArray = formatterResolver.GetFormatterWithVerify<BuffEffect.BuffValues[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 6:
              ostringArray = formatterResolver.GetFormatterWithVerify<OString[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new JobRankParam()
      {
        JobChangeCost = num3,
        JobChangeItems = strArray1,
        JobChangeItemNums = numArray,
        cost = num4,
        equips = strArray2,
        buff_list = buffValuesArray,
        learnings = ostringArray
      };
    }
  }
}
