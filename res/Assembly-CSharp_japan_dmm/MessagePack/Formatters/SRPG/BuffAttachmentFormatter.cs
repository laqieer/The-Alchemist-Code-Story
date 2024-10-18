// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.BuffAttachmentFormatter
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
  public sealed class BuffAttachmentFormatter : 
    IMessagePackFormatter<BuffAttachment>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public BuffAttachmentFormatter()
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
          "BuffEffect",
          2
        },
        {
          "Param",
          3
        },
        {
          "BuffType",
          4
        },
        {
          "CalcType",
          5
        },
        {
          "IsCalcLaterCondition",
          6
        },
        {
          "user",
          7
        },
        {
          "CheckTarget",
          8
        },
        {
          "turn",
          9
        },
        {
          "IsPassive",
          10
        },
        {
          "skill",
          11
        },
        {
          "skilltarget",
          12
        },
        {
          "status",
          13
        },
        {
          "IsNegativeValueIsBuff",
          14
        },
        {
          "DuplicateCount",
          15
        },
        {
          "IsCalculated",
          16
        },
        {
          "LinkageID",
          17
        },
        {
          "UpBuffCount",
          18
        },
        {
          "AagTargetLists",
          19
        },
        {
          "ResistStatusBuffList",
          20
        },
        {
          "IsPrevApply",
          21
        }
      };
      this.____stringByteKeys = new byte[22][]
      {
        MessagePackBinary.GetEncodedStringBytes("CheckTiming"),
        MessagePackBinary.GetEncodedStringBytes("UseCondition"),
        MessagePackBinary.GetEncodedStringBytes("BuffEffect"),
        MessagePackBinary.GetEncodedStringBytes("Param"),
        MessagePackBinary.GetEncodedStringBytes("BuffType"),
        MessagePackBinary.GetEncodedStringBytes("CalcType"),
        MessagePackBinary.GetEncodedStringBytes("IsCalcLaterCondition"),
        MessagePackBinary.GetEncodedStringBytes("user"),
        MessagePackBinary.GetEncodedStringBytes("CheckTarget"),
        MessagePackBinary.GetEncodedStringBytes("turn"),
        MessagePackBinary.GetEncodedStringBytes("IsPassive"),
        MessagePackBinary.GetEncodedStringBytes("skill"),
        MessagePackBinary.GetEncodedStringBytes("skilltarget"),
        MessagePackBinary.GetEncodedStringBytes("status"),
        MessagePackBinary.GetEncodedStringBytes("IsNegativeValueIsBuff"),
        MessagePackBinary.GetEncodedStringBytes("DuplicateCount"),
        MessagePackBinary.GetEncodedStringBytes("IsCalculated"),
        MessagePackBinary.GetEncodedStringBytes("LinkageID"),
        MessagePackBinary.GetEncodedStringBytes("UpBuffCount"),
        MessagePackBinary.GetEncodedStringBytes("AagTargetLists"),
        MessagePackBinary.GetEncodedStringBytes("ResistStatusBuffList"),
        MessagePackBinary.GetEncodedStringBytes("IsPrevApply")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      BuffAttachment value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, 22);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<EffectCheckTimings>().Serialize(ref bytes, offset, value.CheckTiming, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<ESkillCondition>().Serialize(ref bytes, offset, value.UseCondition, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<BuffEffect>().Serialize(ref bytes, offset, value.BuffEffect, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<BuffEffectParam>().Serialize(ref bytes, offset, value.Param, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<BuffTypes>().Serialize(ref bytes, offset, value.BuffType, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<SkillParamCalcTypes>().Serialize(ref bytes, offset, value.CalcType, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsCalcLaterCondition);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += formatterResolver.GetFormatterWithVerify<Unit>().Serialize(ref bytes, offset, value.user, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += formatterResolver.GetFormatterWithVerify<Unit>().Serialize(ref bytes, offset, value.CheckTarget, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.turn, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += formatterResolver.GetFormatterWithVerify<OBool>().Serialize(ref bytes, offset, value.IsPassive, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += formatterResolver.GetFormatterWithVerify<SkillData>().Serialize(ref bytes, offset, value.skill, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += formatterResolver.GetFormatterWithVerify<SkillEffectTargets>().Serialize(ref bytes, offset, value.skilltarget, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += formatterResolver.GetFormatterWithVerify<BaseStatus>().Serialize(ref bytes, offset, value.status, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsNegativeValueIsBuff);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.DuplicateCount);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[16]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsCalculated);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[17]);
      offset += MessagePackBinary.WriteUInt32(ref bytes, offset, value.LinkageID);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[18]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.UpBuffCount, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[19]);
      offset += formatterResolver.GetFormatterWithVerify<List<Unit>>().Serialize(ref bytes, offset, value.AagTargetLists, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[20]);
      offset += formatterResolver.GetFormatterWithVerify<List<BuffAttachment.ResistStatusBuff>>().Serialize(ref bytes, offset, value.ResistStatusBuffList, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[21]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsPrevApply);
      return offset - num;
    }

    public BuffAttachment Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (BuffAttachment) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      EffectCheckTimings effectCheckTimings = EffectCheckTimings.ActionStart;
      ESkillCondition eskillCondition = ESkillCondition.None;
      BuffEffect buffEffect = (BuffEffect) null;
      BuffEffectParam buffEffectParam = (BuffEffectParam) null;
      BuffTypes buffTypes = BuffTypes.Buff;
      SkillParamCalcTypes skillParamCalcTypes = SkillParamCalcTypes.Add;
      bool flag1 = false;
      Unit unit1 = (Unit) null;
      Unit unit2 = (Unit) null;
      OInt oint1 = new OInt();
      OBool obool = new OBool();
      SkillData skillData = (SkillData) null;
      SkillEffectTargets skillEffectTargets = SkillEffectTargets.Target;
      BaseStatus baseStatus = (BaseStatus) null;
      bool flag2 = false;
      int num3 = 0;
      bool flag3 = false;
      uint num4 = 0;
      OInt oint2 = new OInt();
      List<Unit> unitList = (List<Unit>) null;
      List<BuffAttachment.ResistStatusBuff> resistStatusBuffList = (List<BuffAttachment.ResistStatusBuff>) null;
      bool flag4 = false;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num5;
        if (!this.____keyMapping.TryGetValueSafe(key, out num5))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num5)
          {
            case 0:
              effectCheckTimings = formatterResolver.GetFormatterWithVerify<EffectCheckTimings>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              eskillCondition = formatterResolver.GetFormatterWithVerify<ESkillCondition>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              buffEffect = formatterResolver.GetFormatterWithVerify<BuffEffect>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              buffEffectParam = formatterResolver.GetFormatterWithVerify<BuffEffectParam>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              buffTypes = formatterResolver.GetFormatterWithVerify<BuffTypes>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 5:
              skillParamCalcTypes = formatterResolver.GetFormatterWithVerify<SkillParamCalcTypes>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 6:
              flag1 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 7:
              unit1 = formatterResolver.GetFormatterWithVerify<Unit>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 8:
              unit2 = formatterResolver.GetFormatterWithVerify<Unit>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 9:
              oint1 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 10:
              obool = formatterResolver.GetFormatterWithVerify<OBool>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 11:
              skillData = formatterResolver.GetFormatterWithVerify<SkillData>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 12:
              skillEffectTargets = formatterResolver.GetFormatterWithVerify<SkillEffectTargets>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 13:
              baseStatus = formatterResolver.GetFormatterWithVerify<BaseStatus>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 14:
              flag2 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 15:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 16:
              flag3 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 17:
              num4 = MessagePackBinary.ReadUInt32(bytes, offset, out readSize);
              break;
            case 18:
              oint2 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 19:
              unitList = formatterResolver.GetFormatterWithVerify<List<Unit>>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 20:
              resistStatusBuffList = formatterResolver.GetFormatterWithVerify<List<BuffAttachment.ResistStatusBuff>>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 21:
              flag4 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new BuffAttachment()
      {
        CheckTiming = effectCheckTimings,
        UseCondition = eskillCondition,
        BuffType = buffTypes,
        CalcType = skillParamCalcTypes,
        user = unit1,
        CheckTarget = unit2,
        turn = oint1,
        IsPassive = obool,
        skill = skillData,
        skilltarget = skillEffectTargets,
        status = baseStatus,
        IsNegativeValueIsBuff = flag2,
        DuplicateCount = num3,
        IsCalculated = flag3,
        LinkageID = num4,
        UpBuffCount = oint2,
        AagTargetLists = unitList,
        ResistStatusBuffList = resistStatusBuffList,
        IsPrevApply = flag4
      };
    }
  }
}
