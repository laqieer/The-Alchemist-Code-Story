﻿// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.MultiPlayResumeBuff_ResistStatusFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class MultiPlayResumeBuff_ResistStatusFormatter : 
    IMessagePackFormatter<MultiPlayResumeBuff.ResistStatus>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public MultiPlayResumeBuff_ResistStatusFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "rst",
          0
        },
        {
          "rsv",
          1
        }
      };
      this.____stringByteKeys = new byte[2][]
      {
        MessagePackBinary.GetEncodedStringBytes("rst"),
        MessagePackBinary.GetEncodedStringBytes("rsv")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      MultiPlayResumeBuff.ResistStatus value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 2);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rst);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rsv);
      return offset - num;
    }

    public MultiPlayResumeBuff.ResistStatus Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (MultiPlayResumeBuff.ResistStatus) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      int num3 = 0;
      int num4 = 0;
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
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new MultiPlayResumeBuff.ResistStatus()
      {
        rst = num3,
        rsv = num4
      };
    }
  }
}
