// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.SkillAbilityDeriveParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using SRPG;
using System.Collections.Generic;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class SkillAbilityDeriveParamFormatter : 
    IMessagePackFormatter<SkillAbilityDeriveParam>,
    IMessagePackFormatter
  {
    public int Serialize(
      ref byte[] bytes,
      int offset,
      SkillAbilityDeriveParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedArrayHeaderUnsafe(ref bytes, offset, 9);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.m_OriginIndex);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.iname, formatterResolver);
      offset += formatterResolver.GetFormatterWithVerify<SkillAbilityDeriveTriggerParam[]>().Serialize(ref bytes, offset, value.deriveTriggers, formatterResolver);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.base_abils, formatterResolver);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.derive_abils, formatterResolver);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.base_skills, formatterResolver);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.derive_skills, formatterResolver);
      offset += formatterResolver.GetFormatterWithVerify<List<SkillDeriveParam>>().Serialize(ref bytes, offset, value.SkillDeriveParams, formatterResolver);
      offset += formatterResolver.GetFormatterWithVerify<List<AbilityDeriveParam>>().Serialize(ref bytes, offset, value.AbilityDeriveParams, formatterResolver);
      return offset - num;
    }

    public SkillAbilityDeriveParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (SkillAbilityDeriveParam) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadArrayHeader(bytes, offset, out readSize);
      offset += readSize;
      List<SkillDeriveParam> skillDeriveParamList = (List<SkillDeriveParam>) null;
      List<AbilityDeriveParam> abilityDeriveParamList = (List<AbilityDeriveParam>) null;
      string str = (string) null;
      SkillAbilityDeriveTriggerParam[] deriveTriggerParamArray = (SkillAbilityDeriveTriggerParam[]) null;
      string[] strArray1 = (string[]) null;
      string[] strArray2 = (string[]) null;
      string[] strArray3 = (string[]) null;
      string[] strArray4 = (string[]) null;
      int index1 = 0;
      for (int index2 = 0; index2 < num2; ++index2)
      {
        switch (index2)
        {
          case 0:
            index1 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
            break;
          case 1:
            str = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
            break;
          case 2:
            deriveTriggerParamArray = formatterResolver.GetFormatterWithVerify<SkillAbilityDeriveTriggerParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
            break;
          case 3:
            strArray1 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
            break;
          case 4:
            strArray2 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
            break;
          case 5:
            strArray3 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
            break;
          case 6:
            strArray4 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
            break;
          case 7:
            skillDeriveParamList = formatterResolver.GetFormatterWithVerify<List<SkillDeriveParam>>().Deserialize(bytes, offset, formatterResolver, out readSize);
            break;
          case 8:
            abilityDeriveParamList = formatterResolver.GetFormatterWithVerify<List<AbilityDeriveParam>>().Deserialize(bytes, offset, formatterResolver, out readSize);
            break;
          default:
            readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
            break;
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new SkillAbilityDeriveParam(index1)
      {
        iname = str,
        deriveTriggers = deriveTriggerParamArray,
        base_abils = strArray1,
        derive_abils = strArray2,
        base_skills = strArray3,
        derive_skills = strArray4,
        m_OriginIndex = index1
      };
    }
  }
}
