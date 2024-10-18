// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.CondAttachmentFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class CondAttachmentFormatter : 
    IMessagePackFormatter<CondAttachment>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public CondAttachmentFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "CheckTiming",
          0
        },
        {
          "UseCondition",
          1
        },
        {
          "Param",
          2
        },
        {
          "CondType",
          3
        },
        {
          "Condition",
          4
        },
        {
          "IsCurse",
          5
        },
        {
          "user",
          6
        },
        {
          "CheckTarget",
          7
        },
        {
          "turn",
          8
        },
        {
          "IsPassive",
          9
        },
        {
          "skill",
          10
        },
        {
          "skilltarget",
          11
        },
        {
          "CondId",
          12
        },
        {
          "LinkageBuff",
          13
        },
        {
          "LinkageID",
          14
        }
      };
      this.____stringByteKeys = new byte[15][]
      {
        MessagePackBinary.GetEncodedStringBytes("CheckTiming"),
        MessagePackBinary.GetEncodedStringBytes("UseCondition"),
        MessagePackBinary.GetEncodedStringBytes("Param"),
        MessagePackBinary.GetEncodedStringBytes("CondType"),
        MessagePackBinary.GetEncodedStringBytes("Condition"),
        MessagePackBinary.GetEncodedStringBytes("IsCurse"),
        MessagePackBinary.GetEncodedStringBytes("user"),
        MessagePackBinary.GetEncodedStringBytes("CheckTarget"),
        MessagePackBinary.GetEncodedStringBytes("turn"),
        MessagePackBinary.GetEncodedStringBytes("IsPassive"),
        MessagePackBinary.GetEncodedStringBytes("skill"),
        MessagePackBinary.GetEncodedStringBytes("skilltarget"),
        MessagePackBinary.GetEncodedStringBytes("CondId"),
        MessagePackBinary.GetEncodedStringBytes("LinkageBuff"),
        MessagePackBinary.GetEncodedStringBytes("LinkageID")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      CondAttachment value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 15);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<EffectCheckTimings>().Serialize(ref bytes, offset, value.CheckTiming, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<ESkillCondition>().Serialize(ref bytes, offset, value.UseCondition, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<CondEffectParam>().Serialize(ref bytes, offset, value.Param, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<ConditionEffectTypes>().Serialize(ref bytes, offset, value.CondType, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<EUnitCondition>().Serialize(ref bytes, offset, value.Condition, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsCurse);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += formatterResolver.GetFormatterWithVerify<Unit>().Serialize(ref bytes, offset, value.user, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += formatterResolver.GetFormatterWithVerify<Unit>().Serialize(ref bytes, offset, value.CheckTarget, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.turn, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += formatterResolver.GetFormatterWithVerify<OBool>().Serialize(ref bytes, offset, value.IsPassive, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += formatterResolver.GetFormatterWithVerify<SkillData>().Serialize(ref bytes, offset, value.skill, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += formatterResolver.GetFormatterWithVerify<SkillEffectTargets>().Serialize(ref bytes, offset, value.skilltarget, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.CondId, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += formatterResolver.GetFormatterWithVerify<BuffEffect>().Serialize(ref bytes, offset, value.LinkageBuff, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += MessagePackBinary.WriteUInt32(ref bytes, offset, value.LinkageID);
      return offset - num;
    }

    public CondAttachment Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (CondAttachment) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      EffectCheckTimings effectCheckTimings = EffectCheckTimings.ActionStart;
      ESkillCondition eskillCondition = ESkillCondition.None;
      CondEffectParam condEffectParam = (CondEffectParam) null;
      ConditionEffectTypes conditionEffectTypes = ConditionEffectTypes.None;
      EUnitCondition eunitCondition = (EUnitCondition) 0;
      bool flag = false;
      Unit unit1 = (Unit) null;
      Unit unit2 = (Unit) null;
      OInt oint = new OInt();
      OBool obool = new OBool();
      SkillData skillData = (SkillData) null;
      SkillEffectTargets skillEffectTargets = SkillEffectTargets.Target;
      string str = (string) null;
      BuffEffect buffEffect = (BuffEffect) null;
      uint num3 = 0;
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
              effectCheckTimings = formatterResolver.GetFormatterWithVerify<EffectCheckTimings>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              eskillCondition = formatterResolver.GetFormatterWithVerify<ESkillCondition>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              condEffectParam = formatterResolver.GetFormatterWithVerify<CondEffectParam>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              conditionEffectTypes = formatterResolver.GetFormatterWithVerify<ConditionEffectTypes>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              eunitCondition = formatterResolver.GetFormatterWithVerify<EUnitCondition>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 5:
              flag = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 6:
              unit1 = formatterResolver.GetFormatterWithVerify<Unit>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 7:
              unit2 = formatterResolver.GetFormatterWithVerify<Unit>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 8:
              oint = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 9:
              obool = formatterResolver.GetFormatterWithVerify<OBool>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 10:
              skillData = formatterResolver.GetFormatterWithVerify<SkillData>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 11:
              skillEffectTargets = formatterResolver.GetFormatterWithVerify<SkillEffectTargets>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 12:
              str = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 13:
              buffEffect = formatterResolver.GetFormatterWithVerify<BuffEffect>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 14:
              num3 = MessagePackBinary.ReadUInt32(bytes, offset, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new CondAttachment()
      {
        CheckTiming = effectCheckTimings,
        UseCondition = eskillCondition,
        CondType = conditionEffectTypes,
        Condition = eunitCondition,
        IsCurse = flag,
        user = unit1,
        CheckTarget = unit2,
        turn = oint,
        IsPassive = obool,
        skill = skillData,
        skilltarget = skillEffectTargets,
        CondId = str,
        LinkageBuff = buffEffect,
        LinkageID = num3
      };
    }
  }
}
