// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.RuneBuffDataEvoStateFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class RuneBuffDataEvoStateFormatter : 
    IMessagePackFormatter<RuneBuffDataEvoState>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public RuneBuffDataEvoStateFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "evo_lot",
          0
        },
        {
          "val",
          1
        },
        {
          "slot",
          2
        }
      };
      this.____stringByteKeys = new byte[3][]
      {
        MessagePackBinary.GetEncodedStringBytes("evo_lot"),
        MessagePackBinary.GetEncodedStringBytes("val"),
        MessagePackBinary.GetEncodedStringBytes("slot")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      RuneBuffDataEvoState value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 3);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<RuneLotteryEvoState>().Serialize(ref bytes, offset, value.evo_lot, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.val);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.slot);
      return offset - num;
    }

    public RuneBuffDataEvoState Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (RuneBuffDataEvoState) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      RuneLotteryEvoState runeLotteryEvoState = (RuneLotteryEvoState) null;
      short num3 = 0;
      short num4 = 0;
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
              runeLotteryEvoState = formatterResolver.GetFormatterWithVerify<RuneLotteryEvoState>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              num3 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 2:
              num4 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new RuneBuffDataEvoState()
      {
        evo_lot = runeLotteryEvoState,
        val = num3,
        slot = num4
      };
    }
  }
}
