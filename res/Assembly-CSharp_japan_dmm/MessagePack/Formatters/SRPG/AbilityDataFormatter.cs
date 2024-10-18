// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.AbilityDataFormatter
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
  public sealed class AbilityDataFormatter : 
    IMessagePackFormatter<AbilityData>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public AbilityDataFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "Owner",
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
          "AbilityID",
          3
        },
        {
          "AbilityName",
          4
        },
        {
          "Rank",
          5
        },
        {
          "Exp",
          6
        },
        {
          "AbilityType",
          7
        },
        {
          "SlotType",
          8
        },
        {
          "LearningSkills",
          9
        },
        {
          "Skills",
          10
        },
        {
          "IsDerivedAbility",
          11
        },
        {
          "IsDeriveBaseAbility",
          12
        },
        {
          "DeriveBaseAbility",
          13
        },
        {
          "DeriveParam",
          14
        },
        {
          "DerivedAbility",
          15
        },
        {
          "IsCreatedByConceptCard",
          16
        },
        {
          "IsNoneCategory",
          17
        },
        {
          "IsHideList",
          18
        }
      };
      this.____stringByteKeys = new byte[19][]
      {
        MessagePackBinary.GetEncodedStringBytes("Owner"),
        MessagePackBinary.GetEncodedStringBytes("UniqueID"),
        MessagePackBinary.GetEncodedStringBytes("Param"),
        MessagePackBinary.GetEncodedStringBytes("AbilityID"),
        MessagePackBinary.GetEncodedStringBytes("AbilityName"),
        MessagePackBinary.GetEncodedStringBytes("Rank"),
        MessagePackBinary.GetEncodedStringBytes("Exp"),
        MessagePackBinary.GetEncodedStringBytes("AbilityType"),
        MessagePackBinary.GetEncodedStringBytes("SlotType"),
        MessagePackBinary.GetEncodedStringBytes("LearningSkills"),
        MessagePackBinary.GetEncodedStringBytes("Skills"),
        MessagePackBinary.GetEncodedStringBytes("IsDerivedAbility"),
        MessagePackBinary.GetEncodedStringBytes("IsDeriveBaseAbility"),
        MessagePackBinary.GetEncodedStringBytes("DeriveBaseAbility"),
        MessagePackBinary.GetEncodedStringBytes("DeriveParam"),
        MessagePackBinary.GetEncodedStringBytes("DerivedAbility"),
        MessagePackBinary.GetEncodedStringBytes("IsCreatedByConceptCard"),
        MessagePackBinary.GetEncodedStringBytes("IsNoneCategory"),
        MessagePackBinary.GetEncodedStringBytes("IsHideList")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      AbilityData value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, 19);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<UnitData>().Serialize(ref bytes, offset, value.Owner, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += MessagePackBinary.WriteInt64(ref bytes, offset, value.UniqueID);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<AbilityParam>().Serialize(ref bytes, offset, value.Param, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.AbilityID, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.AbilityName, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Rank);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Exp);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += formatterResolver.GetFormatterWithVerify<EAbilityType>().Serialize(ref bytes, offset, value.AbilityType, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += formatterResolver.GetFormatterWithVerify<EAbilitySlot>().Serialize(ref bytes, offset, value.SlotType, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += formatterResolver.GetFormatterWithVerify<LearningSkill[]>().Serialize(ref bytes, offset, value.LearningSkills, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += formatterResolver.GetFormatterWithVerify<List<SkillData>>().Serialize(ref bytes, offset, value.Skills, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsDerivedAbility);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsDeriveBaseAbility);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += formatterResolver.GetFormatterWithVerify<AbilityData>().Serialize(ref bytes, offset, value.DeriveBaseAbility, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += formatterResolver.GetFormatterWithVerify<AbilityDeriveParam>().Serialize(ref bytes, offset, value.DeriveParam, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
      offset += formatterResolver.GetFormatterWithVerify<AbilityData>().Serialize(ref bytes, offset, value.DerivedAbility, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[16]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsCreatedByConceptCard);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[17]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsNoneCategory);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[18]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsHideList);
      return offset - num;
    }

    public AbilityData Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (AbilityData) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      UnitData unitData = (UnitData) null;
      long num3 = 0;
      AbilityParam abilityParam = (AbilityParam) null;
      string str1 = (string) null;
      string str2 = (string) null;
      int num4 = 0;
      int num5 = 0;
      EAbilityType eabilityType = EAbilityType.Active;
      EAbilitySlot eabilitySlot = EAbilitySlot.Action;
      LearningSkill[] learningSkillArray = (LearningSkill[]) null;
      List<SkillData> skillDataList = (List<SkillData>) null;
      bool flag1 = false;
      bool flag2 = false;
      AbilityData abilityData1 = (AbilityData) null;
      AbilityDeriveParam abilityDeriveParam = (AbilityDeriveParam) null;
      AbilityData abilityData2 = (AbilityData) null;
      bool flag3 = false;
      bool flag4 = false;
      bool flag5 = false;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num6;
        if (!this.____keyMapping.TryGetValueSafe(key, out num6))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num6)
          {
            case 0:
              unitData = formatterResolver.GetFormatterWithVerify<UnitData>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              num3 = MessagePackBinary.ReadInt64(bytes, offset, out readSize);
              break;
            case 2:
              abilityParam = formatterResolver.GetFormatterWithVerify<AbilityParam>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              str1 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              str2 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 5:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 6:
              num5 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 7:
              eabilityType = formatterResolver.GetFormatterWithVerify<EAbilityType>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 8:
              eabilitySlot = formatterResolver.GetFormatterWithVerify<EAbilitySlot>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 9:
              learningSkillArray = formatterResolver.GetFormatterWithVerify<LearningSkill[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 10:
              skillDataList = formatterResolver.GetFormatterWithVerify<List<SkillData>>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 11:
              flag1 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 12:
              flag2 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 13:
              abilityData1 = formatterResolver.GetFormatterWithVerify<AbilityData>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 14:
              abilityDeriveParam = formatterResolver.GetFormatterWithVerify<AbilityDeriveParam>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 15:
              abilityData2 = formatterResolver.GetFormatterWithVerify<AbilityData>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 16:
              flag3 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 17:
              flag4 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 18:
              flag5 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new AbilityData()
      {
        IsNoneCategory = flag4,
        IsHideList = flag5
      };
    }
  }
}
