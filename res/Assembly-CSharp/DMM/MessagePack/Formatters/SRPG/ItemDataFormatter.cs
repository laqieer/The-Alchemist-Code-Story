// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.ItemDataFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class ItemDataFormatter : IMessagePackFormatter<ItemData>, IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public ItemDataFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "No",
          0
        },
        {
          "UniqueID",
          1
        },
        {
          "Param",
          2
        },
        {
          "ItemID",
          3
        },
        {
          "Num",
          4
        },
        {
          "NumNonCap",
          5
        },
        {
          "Skill",
          6
        },
        {
          "IsUsed",
          7
        },
        {
          "ItemType",
          8
        },
        {
          "Rarity",
          9
        },
        {
          "RarityParam",
          10
        },
        {
          "HaveCap",
          11
        },
        {
          "InventoryCap",
          12
        },
        {
          "Buy",
          13
        },
        {
          "Sell",
          14
        },
        {
          "Recipe",
          15
        },
        {
          "IsNew",
          16
        },
        {
          "IsNewSkin",
          17
        }
      };
      this.____stringByteKeys = new byte[18][]
      {
        MessagePackBinary.GetEncodedStringBytes("No"),
        MessagePackBinary.GetEncodedStringBytes("UniqueID"),
        MessagePackBinary.GetEncodedStringBytes("Param"),
        MessagePackBinary.GetEncodedStringBytes("ItemID"),
        MessagePackBinary.GetEncodedStringBytes("Num"),
        MessagePackBinary.GetEncodedStringBytes("NumNonCap"),
        MessagePackBinary.GetEncodedStringBytes("Skill"),
        MessagePackBinary.GetEncodedStringBytes("IsUsed"),
        MessagePackBinary.GetEncodedStringBytes("ItemType"),
        MessagePackBinary.GetEncodedStringBytes("Rarity"),
        MessagePackBinary.GetEncodedStringBytes("RarityParam"),
        MessagePackBinary.GetEncodedStringBytes("HaveCap"),
        MessagePackBinary.GetEncodedStringBytes("InventoryCap"),
        MessagePackBinary.GetEncodedStringBytes("Buy"),
        MessagePackBinary.GetEncodedStringBytes("Sell"),
        MessagePackBinary.GetEncodedStringBytes("Recipe"),
        MessagePackBinary.GetEncodedStringBytes("IsNew"),
        MessagePackBinary.GetEncodedStringBytes("IsNewSkin")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      ItemData value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, 18);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.No);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += MessagePackBinary.WriteInt64(ref bytes, offset, value.UniqueID);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<ItemParam>().Serialize(ref bytes, offset, value.Param, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.ItemID, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Num);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.NumNonCap);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += formatterResolver.GetFormatterWithVerify<SkillData>().Serialize(ref bytes, offset, value.Skill, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsUsed);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += formatterResolver.GetFormatterWithVerify<EItemType>().Serialize(ref bytes, offset, value.ItemType, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Rarity);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += formatterResolver.GetFormatterWithVerify<RarityParam>().Serialize(ref bytes, offset, value.RarityParam, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.HaveCap);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.InventoryCap);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Buy);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Sell);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
      offset += formatterResolver.GetFormatterWithVerify<RecipeParam>().Serialize(ref bytes, offset, value.Recipe, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[16]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsNew);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[17]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsNewSkin);
      return offset - num;
    }

    public ItemData Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (ItemData) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      int num3 = 0;
      long num4 = 0;
      ItemParam itemParam = (ItemParam) null;
      string str = (string) null;
      int num5 = 0;
      int num6 = 0;
      SkillData skillData = (SkillData) null;
      bool flag1 = false;
      EItemType eitemType = EItemType.Used;
      int num7 = 0;
      RarityParam rarityParam = (RarityParam) null;
      int num8 = 0;
      int num9 = 0;
      int num10 = 0;
      int num11 = 0;
      RecipeParam recipeParam = (RecipeParam) null;
      bool flag2 = false;
      bool flag3 = false;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num12;
        if (!this.____keyMapping.TryGetValueSafe(key, out num12))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num12)
          {
            case 0:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 1:
              num4 = MessagePackBinary.ReadInt64(bytes, offset, out readSize);
              break;
            case 2:
              itemParam = formatterResolver.GetFormatterWithVerify<ItemParam>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              str = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              num5 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 5:
              num6 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 6:
              skillData = formatterResolver.GetFormatterWithVerify<SkillData>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 7:
              flag1 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 8:
              eitemType = formatterResolver.GetFormatterWithVerify<EItemType>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 9:
              num7 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 10:
              rarityParam = formatterResolver.GetFormatterWithVerify<RarityParam>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 11:
              num8 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 12:
              num9 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 13:
              num10 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 14:
              num11 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 15:
              recipeParam = formatterResolver.GetFormatterWithVerify<RecipeParam>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 16:
              flag2 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 17:
              flag3 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new ItemData()
      {
        IsNew = flag2,
        IsNewSkin = flag3
      };
    }
  }
}
