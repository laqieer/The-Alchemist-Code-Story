// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.Unit_DropItemFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class Unit_DropItemFormatter : 
    IMessagePackFormatter<Unit.DropItem>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public Unit_DropItemFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "isItem",
          0
        },
        {
          "isConceptCard",
          1
        },
        {
          "Iname",
          2
        },
        {
          "BattleRewardType",
          3
        },
        {
          "itemParam",
          4
        },
        {
          "conceptCardParam",
          5
        },
        {
          "num",
          6
        },
        {
          "is_secret",
          7
        }
      };
      this.____stringByteKeys = new byte[8][]
      {
        MessagePackBinary.GetEncodedStringBytes("isItem"),
        MessagePackBinary.GetEncodedStringBytes("isConceptCard"),
        MessagePackBinary.GetEncodedStringBytes("Iname"),
        MessagePackBinary.GetEncodedStringBytes("BattleRewardType"),
        MessagePackBinary.GetEncodedStringBytes("itemParam"),
        MessagePackBinary.GetEncodedStringBytes("conceptCardParam"),
        MessagePackBinary.GetEncodedStringBytes("num"),
        MessagePackBinary.GetEncodedStringBytes("is_secret")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      Unit.DropItem value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 8);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.isItem);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.isConceptCard);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.Iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<EBattleRewardType>().Serialize(ref bytes, offset, value.BattleRewardType, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<ItemParam>().Serialize(ref bytes, offset, value.itemParam, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<ConceptCardParam>().Serialize(ref bytes, offset, value.conceptCardParam, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.num, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += formatterResolver.GetFormatterWithVerify<OBool>().Serialize(ref bytes, offset, value.is_secret, formatterResolver);
      return offset - num;
    }

    public Unit.DropItem Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (Unit.DropItem) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      bool flag1 = false;
      bool flag2 = false;
      string str = (string) null;
      EBattleRewardType ebattleRewardType = EBattleRewardType.None;
      ItemParam itemParam = (ItemParam) null;
      ConceptCardParam conceptCardParam = (ConceptCardParam) null;
      OInt oint = new OInt();
      OBool obool = new OBool();
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
              flag1 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 1:
              flag2 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 2:
              str = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              ebattleRewardType = formatterResolver.GetFormatterWithVerify<EBattleRewardType>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              itemParam = formatterResolver.GetFormatterWithVerify<ItemParam>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 5:
              conceptCardParam = formatterResolver.GetFormatterWithVerify<ConceptCardParam>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 6:
              oint = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 7:
              obool = formatterResolver.GetFormatterWithVerify<OBool>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new Unit.DropItem()
      {
        itemParam = itemParam,
        conceptCardParam = conceptCardParam,
        num = oint,
        is_secret = obool
      };
    }
  }
}
