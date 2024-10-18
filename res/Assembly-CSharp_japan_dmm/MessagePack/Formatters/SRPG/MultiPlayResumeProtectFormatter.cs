﻿// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.MultiPlayResumeProtectFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class MultiPlayResumeProtectFormatter : 
    IMessagePackFormatter<MultiPlayResumeProtect>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public MultiPlayResumeProtectFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "target",
          0
        },
        {
          "value",
          1
        },
        {
          "skillIname",
          2
        }
      };
      this.____stringByteKeys = new byte[3][]
      {
        MessagePackBinary.GetEncodedStringBytes("target"),
        MessagePackBinary.GetEncodedStringBytes("value"),
        MessagePackBinary.GetEncodedStringBytes("skillIname")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      MultiPlayResumeProtect value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 3);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.target);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.value);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.skillIname, formatterResolver);
      return offset - num;
    }

    public MultiPlayResumeProtect Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (MultiPlayResumeProtect) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      int num3 = 0;
      int num4 = 0;
      string str = (string) null;
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
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 2:
              str = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new MultiPlayResumeProtect()
      {
        target = num3,
        value = num4,
        skillIname = str
      };
    }
  }
}
