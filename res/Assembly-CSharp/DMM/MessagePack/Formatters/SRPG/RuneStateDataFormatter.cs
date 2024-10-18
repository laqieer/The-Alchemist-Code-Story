// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.RuneStateDataFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;
using System.Collections.Generic;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class RuneStateDataFormatter : 
    IMessagePackFormatter<RuneStateData>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public RuneStateDataFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "base_state",
          0
        },
        {
          "evo_state",
          1
        }
      };
      this.____stringByteKeys = new byte[2][]
      {
        MessagePackBinary.GetEncodedStringBytes("base_state"),
        MessagePackBinary.GetEncodedStringBytes("evo_state")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      RuneStateData value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 2);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<RuneBuffDataBaseState>().Serialize(ref bytes, offset, value.base_state, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<List<RuneBuffDataEvoState>>().Serialize(ref bytes, offset, value.evo_state, formatterResolver);
      return offset - num;
    }

    public RuneStateData Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (RuneStateData) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      RuneBuffDataBaseState buffDataBaseState = (RuneBuffDataBaseState) null;
      List<RuneBuffDataEvoState> buffDataEvoStateList = (List<RuneBuffDataEvoState>) null;
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
              buffDataBaseState = formatterResolver.GetFormatterWithVerify<RuneBuffDataBaseState>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              buffDataEvoStateList = formatterResolver.GetFormatterWithVerify<List<RuneBuffDataEvoState>>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new RuneStateData()
      {
        base_state = buffDataBaseState,
        evo_state = buffDataEvoStateList
      };
    }
  }
}
