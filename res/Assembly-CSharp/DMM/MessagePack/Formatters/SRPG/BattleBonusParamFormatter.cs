﻿// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.BattleBonusParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class BattleBonusParamFormatter : 
    IMessagePackFormatter<BattleBonusParam>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public BattleBonusParamFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "values",
          0
        }
      };
      this.____stringByteKeys = new byte[1][]
      {
        MessagePackBinary.GetEncodedStringBytes("values")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      BattleBonusParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 1);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<short[]>().Serialize(ref bytes, offset, value.values, formatterResolver);
      return offset - num;
    }

    public BattleBonusParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (BattleBonusParam) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      short[] numArray = (short[]) null;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num3;
        if (!this.____keyMapping.TryGetValueSafe(key, out num3))
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        else if (num3 == 0)
          numArray = formatterResolver.GetFormatterWithVerify<short[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
        else
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        offset += readSize;
      }
      readSize = offset - num1;
      return new BattleBonusParam() { values = numArray };
    }
  }
}
