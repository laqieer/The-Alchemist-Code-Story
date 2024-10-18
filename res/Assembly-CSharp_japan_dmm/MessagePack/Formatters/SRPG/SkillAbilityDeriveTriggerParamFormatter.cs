// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.SkillAbilityDeriveTriggerParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using SRPG;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class SkillAbilityDeriveTriggerParamFormatter : 
    IMessagePackFormatter<SkillAbilityDeriveTriggerParam>,
    IMessagePackFormatter
  {
    public int Serialize(
      ref byte[] bytes,
      int offset,
      SkillAbilityDeriveTriggerParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedArrayHeaderUnsafe(ref bytes, offset, 2);
      offset += formatterResolver.GetFormatterWithVerify<ESkillAbilityDeriveConds>().Serialize(ref bytes, offset, value.m_TriggerType, formatterResolver);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.m_TriggerIname, formatterResolver);
      return offset - num;
    }

    public SkillAbilityDeriveTriggerParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (SkillAbilityDeriveTriggerParam) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadArrayHeader(bytes, offset, out readSize);
      offset += readSize;
      ESkillAbilityDeriveConds triggerType = ESkillAbilityDeriveConds.Unknown;
      string triggerIname = (string) null;
      for (int index = 0; index < num2; ++index)
      {
        switch (index)
        {
          case 0:
            triggerType = formatterResolver.GetFormatterWithVerify<ESkillAbilityDeriveConds>().Deserialize(bytes, offset, formatterResolver, out readSize);
            break;
          case 1:
            triggerIname = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
            break;
          default:
            readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
            break;
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new SkillAbilityDeriveTriggerParam(triggerType, triggerIname)
      {
        m_TriggerType = triggerType,
        m_TriggerIname = triggerIname
      };
    }
  }
}
