﻿// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.BuffEffectFormatter
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
  public sealed class BuffEffectFormatter : IMessagePackFormatter<BuffEffect>, IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public BuffEffectFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "param",
          0
        },
        {
          "targets",
          1
        }
      };
      this.____stringByteKeys = new byte[2][]
      {
        MessagePackBinary.GetEncodedStringBytes("param"),
        MessagePackBinary.GetEncodedStringBytes("targets")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      BuffEffect value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 2);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<BuffEffectParam>().Serialize(ref bytes, offset, value.param, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<List<BuffEffect.BuffTarget>>().Serialize(ref bytes, offset, value.targets, formatterResolver);
      return offset - num;
    }

    public BuffEffect Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (BuffEffect) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      BuffEffectParam buffEffectParam = (BuffEffectParam) null;
      List<BuffEffect.BuffTarget> buffTargetList = (List<BuffEffect.BuffTarget>) null;
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
              buffEffectParam = formatterResolver.GetFormatterWithVerify<BuffEffectParam>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              buffTargetList = formatterResolver.GetFormatterWithVerify<List<BuffEffect.BuffTarget>>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new BuffEffect()
      {
        param = buffEffectParam,
        targets = buffTargetList
      };
    }
  }
}
