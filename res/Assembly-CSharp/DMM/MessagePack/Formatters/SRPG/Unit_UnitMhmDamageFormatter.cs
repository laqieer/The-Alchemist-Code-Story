﻿// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.Unit_UnitMhmDamageFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class Unit_UnitMhmDamageFormatter : 
    IMessagePackFormatter<Unit.UnitMhmDamage>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public Unit_UnitMhmDamageFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "mType",
          0
        },
        {
          "mDamage",
          1
        }
      };
      this.____stringByteKeys = new byte[2][]
      {
        MessagePackBinary.GetEncodedStringBytes("mType"),
        MessagePackBinary.GetEncodedStringBytes("mDamage")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      Unit.UnitMhmDamage value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 2);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<Unit.eTypeMhmDamage>().Serialize(ref bytes, offset, value.mType, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.mDamage, formatterResolver);
      return offset - num;
    }

    public Unit.UnitMhmDamage Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (Unit.UnitMhmDamage) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      Unit.eTypeMhmDamage eTypeMhmDamage = Unit.eTypeMhmDamage.HP;
      OInt oint = new OInt();
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
              eTypeMhmDamage = formatterResolver.GetFormatterWithVerify<Unit.eTypeMhmDamage>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              oint = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new Unit.UnitMhmDamage()
      {
        mType = eTypeMhmDamage,
        mDamage = oint
      };
    }
  }
}