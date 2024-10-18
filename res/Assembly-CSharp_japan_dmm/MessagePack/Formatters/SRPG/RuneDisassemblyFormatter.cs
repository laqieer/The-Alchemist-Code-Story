// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.RuneDisassemblyFormatter
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
  public sealed class RuneDisassemblyFormatter : 
    IMessagePackFormatter<RuneDisassembly>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public RuneDisassemblyFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "success",
          0
        },
        {
          "great",
          1
        },
        {
          "super",
          2
        }
      };
      this.____stringByteKeys = new byte[3][]
      {
        MessagePackBinary.GetEncodedStringBytes("success"),
        MessagePackBinary.GetEncodedStringBytes("great"),
        MessagePackBinary.GetEncodedStringBytes("super")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      RuneDisassembly value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 3);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<List<RuneDisassembly.Materials>>().Serialize(ref bytes, offset, value.success, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<List<RuneDisassembly.Materials>>().Serialize(ref bytes, offset, value.great, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<List<RuneDisassembly.Materials>>().Serialize(ref bytes, offset, value.super, formatterResolver);
      return offset - num;
    }

    public RuneDisassembly Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (RuneDisassembly) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      List<RuneDisassembly.Materials> materialsList1 = (List<RuneDisassembly.Materials>) null;
      List<RuneDisassembly.Materials> materialsList2 = (List<RuneDisassembly.Materials>) null;
      List<RuneDisassembly.Materials> materialsList3 = (List<RuneDisassembly.Materials>) null;
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
              materialsList1 = formatterResolver.GetFormatterWithVerify<List<RuneDisassembly.Materials>>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              materialsList2 = formatterResolver.GetFormatterWithVerify<List<RuneDisassembly.Materials>>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              materialsList3 = formatterResolver.GetFormatterWithVerify<List<RuneDisassembly.Materials>>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new RuneDisassembly()
      {
        success = materialsList1,
        great = materialsList2,
        super = materialsList3
      };
    }
  }
}
