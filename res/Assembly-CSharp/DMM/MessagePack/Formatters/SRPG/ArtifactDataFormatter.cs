// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.ArtifactDataFormatter
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
  public sealed class ArtifactDataFormatter : 
    IMessagePackFormatter<ArtifactData>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public ArtifactDataFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "Exp",
          0
        },
        {
          "UniqueID",
          1
        },
        {
          "ArtifactParam",
          2
        },
        {
          "RarityParam",
          3
        },
        {
          "Rarity",
          4
        },
        {
          "RarityCap",
          5
        },
        {
          "Lv",
          6
        },
        {
          "LvCap",
          7
        },
        {
          "Kakera",
          8
        },
        {
          "KakeraData",
          9
        },
        {
          "RarityKakera",
          10
        },
        {
          "RarityKakeraData",
          11
        },
        {
          "CommonKakera",
          12
        },
        {
          "CommonKakeraData",
          13
        },
        {
          "IsFavorite",
          14
        },
        {
          "EquipSkill",
          15
        },
        {
          "BattleEffectSkill",
          16
        },
        {
          "LearningAbilities",
          17
        },
        {
          "InspirationSkillSlotNum",
          18
        },
        {
          "InspirationSkillList",
          19
        }
      };
      this.____stringByteKeys = new byte[20][]
      {
        MessagePackBinary.GetEncodedStringBytes("Exp"),
        MessagePackBinary.GetEncodedStringBytes("UniqueID"),
        MessagePackBinary.GetEncodedStringBytes("ArtifactParam"),
        MessagePackBinary.GetEncodedStringBytes("RarityParam"),
        MessagePackBinary.GetEncodedStringBytes("Rarity"),
        MessagePackBinary.GetEncodedStringBytes("RarityCap"),
        MessagePackBinary.GetEncodedStringBytes("Lv"),
        MessagePackBinary.GetEncodedStringBytes("LvCap"),
        MessagePackBinary.GetEncodedStringBytes("Kakera"),
        MessagePackBinary.GetEncodedStringBytes("KakeraData"),
        MessagePackBinary.GetEncodedStringBytes("RarityKakera"),
        MessagePackBinary.GetEncodedStringBytes("RarityKakeraData"),
        MessagePackBinary.GetEncodedStringBytes("CommonKakera"),
        MessagePackBinary.GetEncodedStringBytes("CommonKakeraData"),
        MessagePackBinary.GetEncodedStringBytes("IsFavorite"),
        MessagePackBinary.GetEncodedStringBytes("EquipSkill"),
        MessagePackBinary.GetEncodedStringBytes("BattleEffectSkill"),
        MessagePackBinary.GetEncodedStringBytes("LearningAbilities"),
        MessagePackBinary.GetEncodedStringBytes("InspirationSkillSlotNum"),
        MessagePackBinary.GetEncodedStringBytes("InspirationSkillList")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      ArtifactData value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, 20);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Exp);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<OLong>().Serialize(ref bytes, offset, value.UniqueID, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<ArtifactParam>().Serialize(ref bytes, offset, value.ArtifactParam, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<RarityParam>().Serialize(ref bytes, offset, value.RarityParam, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.Rarity, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.RarityCap, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.Lv, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.LvCap, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += formatterResolver.GetFormatterWithVerify<ItemParam>().Serialize(ref bytes, offset, value.Kakera, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += formatterResolver.GetFormatterWithVerify<ItemData>().Serialize(ref bytes, offset, value.KakeraData, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += formatterResolver.GetFormatterWithVerify<ItemParam>().Serialize(ref bytes, offset, value.RarityKakera, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += formatterResolver.GetFormatterWithVerify<ItemData>().Serialize(ref bytes, offset, value.RarityKakeraData, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += formatterResolver.GetFormatterWithVerify<ItemParam>().Serialize(ref bytes, offset, value.CommonKakera, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += formatterResolver.GetFormatterWithVerify<ItemData>().Serialize(ref bytes, offset, value.CommonKakeraData, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsFavorite);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
      offset += formatterResolver.GetFormatterWithVerify<SkillData>().Serialize(ref bytes, offset, value.EquipSkill, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[16]);
      offset += formatterResolver.GetFormatterWithVerify<SkillData>().Serialize(ref bytes, offset, value.BattleEffectSkill, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[17]);
      offset += formatterResolver.GetFormatterWithVerify<List<AbilityData>>().Serialize(ref bytes, offset, value.LearningAbilities, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[18]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.InspirationSkillSlotNum, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[19]);
      offset += formatterResolver.GetFormatterWithVerify<List<InspirationSkillData>>().Serialize(ref bytes, offset, value.InspirationSkillList, formatterResolver);
      return offset - num;
    }

    public ArtifactData Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (ArtifactData) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      int num3 = 0;
      OLong olong = new OLong();
      ArtifactParam artifactParam = (ArtifactParam) null;
      RarityParam rarityParam = (RarityParam) null;
      OInt oint1 = new OInt();
      OInt oint2 = new OInt();
      OInt oint3 = new OInt();
      OInt oint4 = new OInt();
      ItemParam itemParam1 = (ItemParam) null;
      ItemData itemData1 = (ItemData) null;
      ItemParam itemParam2 = (ItemParam) null;
      ItemData itemData2 = (ItemData) null;
      ItemParam itemParam3 = (ItemParam) null;
      ItemData itemData3 = (ItemData) null;
      bool flag = false;
      SkillData skillData1 = (SkillData) null;
      SkillData skillData2 = (SkillData) null;
      List<AbilityData> abilityDataList = (List<AbilityData>) null;
      OInt oint5 = new OInt();
      List<InspirationSkillData> inspirationSkillDataList = (List<InspirationSkillData>) null;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num4;
        if (!this.____keyMapping.TryGetValueSafe(key, out num4))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num4)
          {
            case 0:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 1:
              olong = formatterResolver.GetFormatterWithVerify<OLong>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              artifactParam = formatterResolver.GetFormatterWithVerify<ArtifactParam>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              rarityParam = formatterResolver.GetFormatterWithVerify<RarityParam>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              oint1 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 5:
              oint2 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 6:
              oint3 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 7:
              oint4 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 8:
              itemParam1 = formatterResolver.GetFormatterWithVerify<ItemParam>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 9:
              itemData1 = formatterResolver.GetFormatterWithVerify<ItemData>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 10:
              itemParam2 = formatterResolver.GetFormatterWithVerify<ItemParam>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 11:
              itemData2 = formatterResolver.GetFormatterWithVerify<ItemData>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 12:
              itemParam3 = formatterResolver.GetFormatterWithVerify<ItemParam>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 13:
              itemData3 = formatterResolver.GetFormatterWithVerify<ItemData>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 14:
              flag = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 15:
              skillData1 = formatterResolver.GetFormatterWithVerify<SkillData>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 16:
              skillData2 = formatterResolver.GetFormatterWithVerify<SkillData>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 17:
              abilityDataList = formatterResolver.GetFormatterWithVerify<List<AbilityData>>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 18:
              oint5 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 19:
              inspirationSkillDataList = formatterResolver.GetFormatterWithVerify<List<InspirationSkillData>>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new ArtifactData() { IsFavorite = flag };
    }
  }
}
