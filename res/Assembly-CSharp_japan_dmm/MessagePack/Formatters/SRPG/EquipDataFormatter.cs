// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.EquipDataFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class EquipDataFormatter : IMessagePackFormatter<EquipData>, IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public EquipDataFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "UniqueID",
          0
        },
        {
          "ItemParam",
          1
        },
        {
          "RarityParam",
          2
        },
        {
          "ItemID",
          3
        },
        {
          "Rank",
          4
        },
        {
          "ItemType",
          5
        },
        {
          "Rarity",
          6
        },
        {
          "Exp",
          7
        },
        {
          "Skill",
          8
        }
      };
      this.____stringByteKeys = new byte[9][]
      {
        MessagePackBinary.GetEncodedStringBytes("UniqueID"),
        MessagePackBinary.GetEncodedStringBytes("ItemParam"),
        MessagePackBinary.GetEncodedStringBytes("RarityParam"),
        MessagePackBinary.GetEncodedStringBytes("ItemID"),
        MessagePackBinary.GetEncodedStringBytes("Rank"),
        MessagePackBinary.GetEncodedStringBytes("ItemType"),
        MessagePackBinary.GetEncodedStringBytes("Rarity"),
        MessagePackBinary.GetEncodedStringBytes("Exp"),
        MessagePackBinary.GetEncodedStringBytes("Skill")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      EquipData value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 9);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += MessagePackBinary.WriteInt64(ref bytes, offset, value.UniqueID);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<ItemParam>().Serialize(ref bytes, offset, value.ItemParam, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<RarityParam>().Serialize(ref bytes, offset, value.RarityParam, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.ItemID, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Rank);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<EItemType>().Serialize(ref bytes, offset, value.ItemType, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Rarity);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Exp);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += formatterResolver.GetFormatterWithVerify<SkillData>().Serialize(ref bytes, offset, value.Skill, formatterResolver);
      return offset - num;
    }

    public EquipData Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (EquipData) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      long num3 = 0;
      ItemParam itemParam = (ItemParam) null;
      RarityParam rarityParam = (RarityParam) null;
      string str = (string) null;
      int num4 = 0;
      EItemType eitemType = EItemType.Used;
      int num5 = 0;
      int num6 = 0;
      SkillData skillData = (SkillData) null;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num7;
        if (!this.____keyMapping.TryGetValueSafe(key, out num7))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num7)
          {
            case 0:
              num3 = MessagePackBinary.ReadInt64(bytes, offset, out readSize);
              break;
            case 1:
              itemParam = formatterResolver.GetFormatterWithVerify<ItemParam>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              rarityParam = formatterResolver.GetFormatterWithVerify<RarityParam>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              str = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 5:
              eitemType = formatterResolver.GetFormatterWithVerify<EItemType>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 6:
              num5 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 7:
              num6 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 8:
              skillData = formatterResolver.GetFormatterWithVerify<SkillData>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new EquipData();
    }
  }
}
