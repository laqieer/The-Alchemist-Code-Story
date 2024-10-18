// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.ConceptCardEquipEffectFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class ConceptCardEquipEffectFormatter : 
    IMessagePackFormatter<ConceptCardEquipEffect>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public ConceptCardEquipEffectFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "ConditionsIname",
          0
        },
        {
          "Skin",
          1
        },
        {
          "CardSkill",
          2
        },
        {
          "EquipSkill",
          3
        },
        {
          "Ability",
          4
        },
        {
          "AddCardSkillBuffEffectAwake",
          5
        },
        {
          "AddCardSkillBuffEffectLvMax",
          6
        },
        {
          "IsExistAbilityLvMax",
          7
        },
        {
          "IsDecreaseEffectOnSub",
          8
        }
      };
      this.____stringByteKeys = new byte[9][]
      {
        MessagePackBinary.GetEncodedStringBytes("ConditionsIname"),
        MessagePackBinary.GetEncodedStringBytes("Skin"),
        MessagePackBinary.GetEncodedStringBytes("CardSkill"),
        MessagePackBinary.GetEncodedStringBytes("EquipSkill"),
        MessagePackBinary.GetEncodedStringBytes("Ability"),
        MessagePackBinary.GetEncodedStringBytes("AddCardSkillBuffEffectAwake"),
        MessagePackBinary.GetEncodedStringBytes("AddCardSkillBuffEffectLvMax"),
        MessagePackBinary.GetEncodedStringBytes("IsExistAbilityLvMax"),
        MessagePackBinary.GetEncodedStringBytes("IsDecreaseEffectOnSub")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      ConceptCardEquipEffect value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 9);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.ConditionsIname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.Skin, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<SkillData>().Serialize(ref bytes, offset, value.CardSkill, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<SkillData>().Serialize(ref bytes, offset, value.EquipSkill, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<AbilityData>().Serialize(ref bytes, offset, value.Ability, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<BuffEffect>().Serialize(ref bytes, offset, value.AddCardSkillBuffEffectAwake, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += formatterResolver.GetFormatterWithVerify<BuffEffect>().Serialize(ref bytes, offset, value.AddCardSkillBuffEffectLvMax, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsExistAbilityLvMax);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsDecreaseEffectOnSub);
      return offset - num;
    }

    public ConceptCardEquipEffect Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (ConceptCardEquipEffect) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      string str1 = (string) null;
      string str2 = (string) null;
      SkillData skillData1 = (SkillData) null;
      SkillData skillData2 = (SkillData) null;
      AbilityData abilityData = (AbilityData) null;
      BuffEffect buffEffect1 = (BuffEffect) null;
      BuffEffect buffEffect2 = (BuffEffect) null;
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
              str1 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              str2 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              skillData1 = formatterResolver.GetFormatterWithVerify<SkillData>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              skillData2 = formatterResolver.GetFormatterWithVerify<SkillData>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              abilityData = formatterResolver.GetFormatterWithVerify<AbilityData>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 5:
              buffEffect1 = formatterResolver.GetFormatterWithVerify<BuffEffect>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 6:
              buffEffect2 = formatterResolver.GetFormatterWithVerify<BuffEffect>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 7:
              flag1 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 8:
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
      return new ConceptCardEquipEffect();
    }
  }
}
