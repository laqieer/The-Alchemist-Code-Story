// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.Unit_UnitStealFormatter
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
  public sealed class Unit_UnitStealFormatter : 
    IMessagePackFormatter<Unit.UnitSteal>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public Unit_UnitStealFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "items",
          0
        },
        {
          "gold",
          1
        },
        {
          "is_item_steeled",
          2
        },
        {
          "is_gold_steeled",
          3
        }
      };
      this.____stringByteKeys = new byte[4][]
      {
        MessagePackBinary.GetEncodedStringBytes("items"),
        MessagePackBinary.GetEncodedStringBytes("gold"),
        MessagePackBinary.GetEncodedStringBytes("is_item_steeled"),
        MessagePackBinary.GetEncodedStringBytes("is_gold_steeled")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      Unit.UnitSteal value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 4);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<List<Unit.DropItem>>().Serialize(ref bytes, offset, value.items, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.gold, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.is_item_steeled);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.is_gold_steeled);
      return offset - num;
    }

    public Unit.UnitSteal Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (Unit.UnitSteal) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      List<Unit.DropItem> dropItemList = (List<Unit.DropItem>) null;
      OInt oint = new OInt();
      bool flag1 = false;
      bool flag2 = false;
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
              dropItemList = formatterResolver.GetFormatterWithVerify<List<Unit.DropItem>>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              oint = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              flag1 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 3:
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
      return new Unit.UnitSteal()
      {
        items = dropItemList,
        gold = oint,
        is_item_steeled = flag1,
        is_gold_steeled = flag2
      };
    }
  }
}
