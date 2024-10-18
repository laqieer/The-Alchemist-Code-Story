// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.AbilityParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class AbilityParamFormatter : 
    IMessagePackFormatter<AbilityParam>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public AbilityParamFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "iname",
          0
        },
        {
          "name",
          1
        },
        {
          "expr",
          2
        },
        {
          "icon",
          3
        },
        {
          "type",
          4
        },
        {
          "slot",
          5
        },
        {
          "lvcap",
          6
        },
        {
          "is_fixed",
          7
        },
        {
          "skills",
          8
        },
        {
          "condition_units",
          9
        },
        {
          "units_conditions_type",
          10
        },
        {
          "condition_jobs",
          11
        },
        {
          "jobs_conditions_type",
          12
        },
        {
          "condition_birth",
          13
        },
        {
          "condition_sex",
          14
        },
        {
          "condition_element",
          15
        },
        {
          "condition_raremin",
          16
        },
        {
          "condition_raremax",
          17
        },
        {
          "type_detail",
          18
        }
      };
      this.____stringByteKeys = new byte[19][]
      {
        MessagePackBinary.GetEncodedStringBytes("iname"),
        MessagePackBinary.GetEncodedStringBytes("name"),
        MessagePackBinary.GetEncodedStringBytes("expr"),
        MessagePackBinary.GetEncodedStringBytes("icon"),
        MessagePackBinary.GetEncodedStringBytes("type"),
        MessagePackBinary.GetEncodedStringBytes("slot"),
        MessagePackBinary.GetEncodedStringBytes("lvcap"),
        MessagePackBinary.GetEncodedStringBytes("is_fixed"),
        MessagePackBinary.GetEncodedStringBytes("skills"),
        MessagePackBinary.GetEncodedStringBytes("condition_units"),
        MessagePackBinary.GetEncodedStringBytes("units_conditions_type"),
        MessagePackBinary.GetEncodedStringBytes("condition_jobs"),
        MessagePackBinary.GetEncodedStringBytes("jobs_conditions_type"),
        MessagePackBinary.GetEncodedStringBytes("condition_birth"),
        MessagePackBinary.GetEncodedStringBytes("condition_sex"),
        MessagePackBinary.GetEncodedStringBytes("condition_element"),
        MessagePackBinary.GetEncodedStringBytes("condition_raremin"),
        MessagePackBinary.GetEncodedStringBytes("condition_raremax"),
        MessagePackBinary.GetEncodedStringBytes("type_detail")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      AbilityParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, 19);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.name, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.expr, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.icon, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<EAbilityType>().Serialize(ref bytes, offset, value.type, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<EAbilitySlot>().Serialize(ref bytes, offset, value.slot, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.lvcap, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.is_fixed);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += formatterResolver.GetFormatterWithVerify<LearningSkill[]>().Serialize(ref bytes, offset, value.skills, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.condition_units, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += formatterResolver.GetFormatterWithVerify<EUseConditionsType>().Serialize(ref bytes, offset, value.units_conditions_type, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.condition_jobs, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += formatterResolver.GetFormatterWithVerify<EUseConditionsType>().Serialize(ref bytes, offset, value.jobs_conditions_type, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.condition_birth, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += formatterResolver.GetFormatterWithVerify<ESex>().Serialize(ref bytes, offset, value.condition_sex, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
      offset += formatterResolver.GetFormatterWithVerify<EElement>().Serialize(ref bytes, offset, value.condition_element, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[16]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.condition_raremin, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[17]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.condition_raremax, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[18]);
      offset += formatterResolver.GetFormatterWithVerify<EAbilityTypeDetail>().Serialize(ref bytes, offset, value.type_detail, formatterResolver);
      return offset - num;
    }

    public AbilityParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (AbilityParam) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      string str1 = (string) null;
      string str2 = (string) null;
      string str3 = (string) null;
      string str4 = (string) null;
      EAbilityType eabilityType = EAbilityType.Active;
      EAbilitySlot eabilitySlot = EAbilitySlot.Action;
      OInt oint1 = new OInt();
      bool flag = false;
      LearningSkill[] learningSkillArray = (LearningSkill[]) null;
      string[] strArray1 = (string[]) null;
      EUseConditionsType euseConditionsType1 = EUseConditionsType.Match;
      string[] strArray2 = (string[]) null;
      EUseConditionsType euseConditionsType2 = EUseConditionsType.Match;
      string str5 = (string) null;
      ESex esex = ESex.Unknown;
      EElement eelement = EElement.None;
      OInt oint2 = new OInt();
      OInt oint3 = new OInt();
      EAbilityTypeDetail eabilityTypeDetail = EAbilityTypeDetail.Default;
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
              str1 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              str2 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              str3 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              str4 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              eabilityType = formatterResolver.GetFormatterWithVerify<EAbilityType>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 5:
              eabilitySlot = formatterResolver.GetFormatterWithVerify<EAbilitySlot>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 6:
              oint1 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 7:
              flag = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 8:
              learningSkillArray = formatterResolver.GetFormatterWithVerify<LearningSkill[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 9:
              strArray1 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 10:
              euseConditionsType1 = formatterResolver.GetFormatterWithVerify<EUseConditionsType>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 11:
              strArray2 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 12:
              euseConditionsType2 = formatterResolver.GetFormatterWithVerify<EUseConditionsType>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 13:
              str5 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 14:
              esex = formatterResolver.GetFormatterWithVerify<ESex>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 15:
              eelement = formatterResolver.GetFormatterWithVerify<EElement>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 16:
              oint2 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 17:
              oint3 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 18:
              eabilityTypeDetail = formatterResolver.GetFormatterWithVerify<EAbilityTypeDetail>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new AbilityParam()
      {
        iname = str1,
        name = str2,
        expr = str3,
        icon = str4,
        type = eabilityType,
        slot = eabilitySlot,
        lvcap = oint1,
        is_fixed = flag,
        skills = learningSkillArray,
        condition_units = strArray1,
        units_conditions_type = euseConditionsType1,
        condition_jobs = strArray2,
        jobs_conditions_type = euseConditionsType2,
        condition_birth = str5,
        condition_sex = esex,
        condition_element = eelement,
        condition_raremin = oint2,
        condition_raremax = oint3,
        type_detail = eabilityTypeDetail
      };
    }
  }
}
