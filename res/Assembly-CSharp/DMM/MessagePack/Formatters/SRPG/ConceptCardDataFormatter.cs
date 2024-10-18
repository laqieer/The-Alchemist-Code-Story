// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.ConceptCardDataFormatter
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
  public sealed class ConceptCardDataFormatter : 
    IMessagePackFormatter<ConceptCardData>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public ConceptCardDataFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "UniqueID",
          0
        },
        {
          "Rarity",
          1
        },
        {
          "Lv",
          2
        },
        {
          "Exp",
          3
        },
        {
          "Trust",
          4
        },
        {
          "Favorite",
          5
        },
        {
          "TrustBonus",
          6
        },
        {
          "TrustBonusCount",
          7
        },
        {
          "Param",
          8
        },
        {
          "EquipEffects",
          9
        },
        {
          "CurrentLvCap",
          10
        },
        {
          "LvCap",
          11
        },
        {
          "AwakeLevel",
          12
        },
        {
          "IsEnableAwake",
          13
        },
        {
          "AwakeCountCap",
          14
        },
        {
          "AwakeLevelCap",
          15
        },
        {
          "LeaderSkill",
          16
        },
        {
          "EquipedSlotIndex",
          17
        },
        {
          "AwakeCount",
          18
        },
        {
          "IsEquipedMainSlot",
          19
        },
        {
          "IsEquipedSubSlot",
          20
        },
        {
          "CurrentDecreaseEffectRate",
          21
        },
        {
          "SellGold",
          22
        },
        {
          "SellCoinItemNum",
          23
        },
        {
          "MixExp",
          24
        }
      };
      this.____stringByteKeys = new byte[25][]
      {
        MessagePackBinary.GetEncodedStringBytes("UniqueID"),
        MessagePackBinary.GetEncodedStringBytes("Rarity"),
        MessagePackBinary.GetEncodedStringBytes("Lv"),
        MessagePackBinary.GetEncodedStringBytes("Exp"),
        MessagePackBinary.GetEncodedStringBytes("Trust"),
        MessagePackBinary.GetEncodedStringBytes("Favorite"),
        MessagePackBinary.GetEncodedStringBytes("TrustBonus"),
        MessagePackBinary.GetEncodedStringBytes("TrustBonusCount"),
        MessagePackBinary.GetEncodedStringBytes("Param"),
        MessagePackBinary.GetEncodedStringBytes("EquipEffects"),
        MessagePackBinary.GetEncodedStringBytes("CurrentLvCap"),
        MessagePackBinary.GetEncodedStringBytes("LvCap"),
        MessagePackBinary.GetEncodedStringBytes("AwakeLevel"),
        MessagePackBinary.GetEncodedStringBytes("IsEnableAwake"),
        MessagePackBinary.GetEncodedStringBytes("AwakeCountCap"),
        MessagePackBinary.GetEncodedStringBytes("AwakeLevelCap"),
        MessagePackBinary.GetEncodedStringBytes("LeaderSkill"),
        MessagePackBinary.GetEncodedStringBytes("EquipedSlotIndex"),
        MessagePackBinary.GetEncodedStringBytes("AwakeCount"),
        MessagePackBinary.GetEncodedStringBytes("IsEquipedMainSlot"),
        MessagePackBinary.GetEncodedStringBytes("IsEquipedSubSlot"),
        MessagePackBinary.GetEncodedStringBytes("CurrentDecreaseEffectRate"),
        MessagePackBinary.GetEncodedStringBytes("SellGold"),
        MessagePackBinary.GetEncodedStringBytes("SellCoinItemNum"),
        MessagePackBinary.GetEncodedStringBytes("MixExp")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      ConceptCardData value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, 25);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<OLong>().Serialize(ref bytes, offset, value.UniqueID, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.Rarity, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.Lv, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.Exp, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.Trust, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.Favorite);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.TrustBonus);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.TrustBonusCount);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += formatterResolver.GetFormatterWithVerify<ConceptCardParam>().Serialize(ref bytes, offset, value.Param, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += formatterResolver.GetFormatterWithVerify<List<ConceptCardEquipEffect>>().Serialize(ref bytes, offset, value.EquipEffects, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.CurrentLvCap, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.LvCap, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.AwakeLevel, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsEnableAwake);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.AwakeCountCap);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.AwakeLevelCap);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[16]);
      offset += formatterResolver.GetFormatterWithVerify<SkillData>().Serialize(ref bytes, offset, value.LeaderSkill, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[17]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.EquipedSlotIndex);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[18]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.AwakeCount, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[19]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsEquipedMainSlot);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[20]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsEquipedSubSlot);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[21]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.CurrentDecreaseEffectRate);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[22]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.SellGold);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[23]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.SellCoinItemNum);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[24]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.MixExp);
      return offset - num;
    }

    public ConceptCardData Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (ConceptCardData) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      OLong olong = new OLong();
      OInt oint1 = new OInt();
      OInt oint2 = new OInt();
      OInt oint3 = new OInt();
      OInt oint4 = new OInt();
      bool flag1 = false;
      int num3 = 0;
      int num4 = 0;
      ConceptCardParam conceptCardParam = (ConceptCardParam) null;
      List<ConceptCardEquipEffect> conceptCardEquipEffectList = (List<ConceptCardEquipEffect>) null;
      OInt oint5 = new OInt();
      OInt oint6 = new OInt();
      OInt oint7 = new OInt();
      bool flag2 = false;
      int num5 = 0;
      int num6 = 0;
      SkillData skillData = (SkillData) null;
      int num7 = 0;
      OInt oint8 = new OInt();
      bool flag3 = false;
      bool flag4 = false;
      int num8 = 0;
      int num9 = 0;
      int num10 = 0;
      int num11 = 0;
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
              olong = formatterResolver.GetFormatterWithVerify<OLong>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              oint1 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              oint2 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              oint3 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              oint4 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 5:
              flag1 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 6:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 7:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 8:
              conceptCardParam = formatterResolver.GetFormatterWithVerify<ConceptCardParam>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 9:
              conceptCardEquipEffectList = formatterResolver.GetFormatterWithVerify<List<ConceptCardEquipEffect>>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 10:
              oint5 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 11:
              oint6 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 12:
              oint7 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 13:
              flag2 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 14:
              num5 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 15:
              num6 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 16:
              skillData = formatterResolver.GetFormatterWithVerify<SkillData>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 17:
              num7 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 18:
              oint8 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 19:
              flag3 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 20:
              flag4 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 21:
              num8 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 22:
              num9 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 23:
              num10 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 24:
              num11 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new ConceptCardData();
    }
  }
}
