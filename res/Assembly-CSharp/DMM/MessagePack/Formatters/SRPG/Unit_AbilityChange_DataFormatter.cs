// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.Unit_AbilityChange_DataFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class Unit_AbilityChange_DataFormatter : 
    IMessagePackFormatter<Unit.AbilityChange.Data>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public Unit_AbilityChange_DataFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "mFromAp",
          0
        },
        {
          "mToAp",
          1
        },
        {
          "mTurn",
          2
        },
        {
          "mIsReset",
          3
        },
        {
          "mExp",
          4
        },
        {
          "mIsInfinite",
          5
        }
      };
      this.____stringByteKeys = new byte[6][]
      {
        MessagePackBinary.GetEncodedStringBytes("mFromAp"),
        MessagePackBinary.GetEncodedStringBytes("mToAp"),
        MessagePackBinary.GetEncodedStringBytes("mTurn"),
        MessagePackBinary.GetEncodedStringBytes("mIsReset"),
        MessagePackBinary.GetEncodedStringBytes("mExp"),
        MessagePackBinary.GetEncodedStringBytes("mIsInfinite")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      Unit.AbilityChange.Data value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 6);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<AbilityParam>().Serialize(ref bytes, offset, value.mFromAp, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<AbilityParam>().Serialize(ref bytes, offset, value.mToAp, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mTurn);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.mIsReset);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mExp);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.mIsInfinite);
      return offset - num;
    }

    public Unit.AbilityChange.Data Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (Unit.AbilityChange.Data) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      AbilityParam abilityParam1 = (AbilityParam) null;
      AbilityParam abilityParam2 = (AbilityParam) null;
      int num3 = 0;
      bool flag1 = false;
      int num4 = 0;
      bool flag2 = false;
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
              abilityParam1 = formatterResolver.GetFormatterWithVerify<AbilityParam>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              abilityParam2 = formatterResolver.GetFormatterWithVerify<AbilityParam>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 3:
              flag1 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 4:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 5:
              flag2 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new Unit.AbilityChange.Data()
      {
        mFromAp = abilityParam1,
        mToAp = abilityParam2,
        mTurn = num3,
        mIsReset = flag1,
        mExp = num4,
        mIsInfinite = flag2
      };
    }
  }
}
