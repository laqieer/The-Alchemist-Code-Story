// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.BuffEffectParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class BuffEffectParamFormatter : 
    IMessagePackFormatter<BuffEffectParam>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public BuffEffectParamFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "IsUpReplenish",
          0
        },
        {
          "IsUpReplenishUseSkillCtr",
          1
        },
        {
          "IsNoDisabled",
          2
        },
        {
          "IsNoBuffTurn",
          3
        },
        {
          "IsAvoidMissType",
          4
        },
        {
          "IsAvoidPerfectType",
          5
        },
        {
          "IsAvoidAllType",
          6
        },
        {
          "iname",
          7
        },
        {
          "job",
          8
        },
        {
          "buki",
          9
        },
        {
          "birth",
          10
        },
        {
          "sex",
          11
        },
        {
          "un_group",
          12
        },
        {
          "elem",
          13
        },
        {
          "cond",
          14
        },
        {
          "rate",
          15
        },
        {
          "turn",
          16
        },
        {
          "chk_target",
          17
        },
        {
          "chk_timing",
          18
        },
        {
          "mIsUpBuff",
          19
        },
        {
          "mUpTiming",
          20
        },
        {
          "mAppType",
          21
        },
        {
          "mAppMct",
          22
        },
        {
          "mEffRange",
          23
        },
        {
          "mFlags",
          24
        },
        {
          "custom_targets",
          25
        },
        {
          "tags",
          26
        },
        {
          "buffs",
          27
        }
      };
      this.____stringByteKeys = new byte[28][]
      {
        MessagePackBinary.GetEncodedStringBytes("IsUpReplenish"),
        MessagePackBinary.GetEncodedStringBytes("IsUpReplenishUseSkillCtr"),
        MessagePackBinary.GetEncodedStringBytes("IsNoDisabled"),
        MessagePackBinary.GetEncodedStringBytes("IsNoBuffTurn"),
        MessagePackBinary.GetEncodedStringBytes("IsAvoidMissType"),
        MessagePackBinary.GetEncodedStringBytes("IsAvoidPerfectType"),
        MessagePackBinary.GetEncodedStringBytes("IsAvoidAllType"),
        MessagePackBinary.GetEncodedStringBytes("iname"),
        MessagePackBinary.GetEncodedStringBytes("job"),
        MessagePackBinary.GetEncodedStringBytes("buki"),
        MessagePackBinary.GetEncodedStringBytes("birth"),
        MessagePackBinary.GetEncodedStringBytes("sex"),
        MessagePackBinary.GetEncodedStringBytes("un_group"),
        MessagePackBinary.GetEncodedStringBytes("elem"),
        MessagePackBinary.GetEncodedStringBytes("cond"),
        MessagePackBinary.GetEncodedStringBytes("rate"),
        MessagePackBinary.GetEncodedStringBytes("turn"),
        MessagePackBinary.GetEncodedStringBytes("chk_target"),
        MessagePackBinary.GetEncodedStringBytes("chk_timing"),
        MessagePackBinary.GetEncodedStringBytes("mIsUpBuff"),
        MessagePackBinary.GetEncodedStringBytes("mUpTiming"),
        MessagePackBinary.GetEncodedStringBytes("mAppType"),
        MessagePackBinary.GetEncodedStringBytes("mAppMct"),
        MessagePackBinary.GetEncodedStringBytes("mEffRange"),
        MessagePackBinary.GetEncodedStringBytes("mFlags"),
        MessagePackBinary.GetEncodedStringBytes("custom_targets"),
        MessagePackBinary.GetEncodedStringBytes("tags"),
        MessagePackBinary.GetEncodedStringBytes("buffs")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      BuffEffectParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, 28);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsUpReplenish);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsUpReplenishUseSkillCtr);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsNoDisabled);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsNoBuffTurn);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsAvoidMissType);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsAvoidPerfectType);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsAvoidAllType);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.job, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.buki, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.birth, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += formatterResolver.GetFormatterWithVerify<ESex>().Serialize(ref bytes, offset, value.sex, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.un_group, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.elem);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += formatterResolver.GetFormatterWithVerify<ESkillCondition>().Serialize(ref bytes, offset, value.cond, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.rate, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[16]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.turn, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[17]);
      offset += formatterResolver.GetFormatterWithVerify<EffectCheckTargets>().Serialize(ref bytes, offset, value.chk_target, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[18]);
      offset += formatterResolver.GetFormatterWithVerify<EffectCheckTimings>().Serialize(ref bytes, offset, value.chk_timing, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[19]);
      offset += formatterResolver.GetFormatterWithVerify<OBool>().Serialize(ref bytes, offset, value.mIsUpBuff, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[20]);
      offset += formatterResolver.GetFormatterWithVerify<EffectCheckTimings>().Serialize(ref bytes, offset, value.mUpTiming, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[21]);
      offset += formatterResolver.GetFormatterWithVerify<EAppType>().Serialize(ref bytes, offset, value.mAppType, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[22]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mAppMct);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[23]);
      offset += formatterResolver.GetFormatterWithVerify<EEffRange>().Serialize(ref bytes, offset, value.mEffRange, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[24]);
      offset += formatterResolver.GetFormatterWithVerify<BuffFlags>().Serialize(ref bytes, offset, value.mFlags, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[25]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.custom_targets, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[26]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.tags, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[27]);
      offset += formatterResolver.GetFormatterWithVerify<BuffEffectParam.Buff[]>().Serialize(ref bytes, offset, value.buffs, formatterResolver);
      return offset - num;
    }

    public BuffEffectParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (BuffEffectParam) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      bool flag1 = false;
      bool flag2 = false;
      bool flag3 = false;
      bool flag4 = false;
      bool flag5 = false;
      bool flag6 = false;
      bool flag7 = false;
      string str1 = (string) null;
      string str2 = (string) null;
      string str3 = (string) null;
      string str4 = (string) null;
      ESex esex = ESex.Unknown;
      string str5 = (string) null;
      int num3 = 0;
      ESkillCondition eskillCondition = ESkillCondition.None;
      OInt oint1 = new OInt();
      OInt oint2 = new OInt();
      EffectCheckTargets effectCheckTargets = EffectCheckTargets.Target;
      EffectCheckTimings effectCheckTimings1 = EffectCheckTimings.ActionStart;
      OBool obool = new OBool();
      EffectCheckTimings effectCheckTimings2 = EffectCheckTimings.ActionStart;
      EAppType eappType = EAppType.Standard;
      int num4 = 0;
      EEffRange eeffRange = EEffRange.None;
      BuffFlags buffFlags = (BuffFlags) 0;
      string[] strArray1 = (string[]) null;
      string[] strArray2 = (string[]) null;
      BuffEffectParam.Buff[] buffArray = (BuffEffectParam.Buff[]) null;
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
              flag1 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 1:
              flag2 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 2:
              flag3 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 3:
              flag4 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 4:
              flag5 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 5:
              flag6 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 6:
              flag7 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 7:
              str1 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 8:
              str2 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 9:
              str3 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 10:
              str4 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 11:
              esex = formatterResolver.GetFormatterWithVerify<ESex>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 12:
              str5 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 13:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 14:
              eskillCondition = formatterResolver.GetFormatterWithVerify<ESkillCondition>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 15:
              oint1 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 16:
              oint2 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 17:
              effectCheckTargets = formatterResolver.GetFormatterWithVerify<EffectCheckTargets>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 18:
              effectCheckTimings1 = formatterResolver.GetFormatterWithVerify<EffectCheckTimings>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 19:
              obool = formatterResolver.GetFormatterWithVerify<OBool>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 20:
              effectCheckTimings2 = formatterResolver.GetFormatterWithVerify<EffectCheckTimings>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 21:
              eappType = formatterResolver.GetFormatterWithVerify<EAppType>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 22:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 23:
              eeffRange = formatterResolver.GetFormatterWithVerify<EEffRange>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 24:
              buffFlags = formatterResolver.GetFormatterWithVerify<BuffFlags>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 25:
              strArray1 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 26:
              strArray2 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 27:
              buffArray = formatterResolver.GetFormatterWithVerify<BuffEffectParam.Buff[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new BuffEffectParam()
      {
        iname = str1,
        job = str2,
        buki = str3,
        birth = str4,
        sex = esex,
        un_group = str5,
        elem = num3,
        cond = eskillCondition,
        rate = oint1,
        turn = oint2,
        chk_target = effectCheckTargets,
        chk_timing = effectCheckTimings1,
        mIsUpBuff = obool,
        mUpTiming = effectCheckTimings2,
        mAppType = eappType,
        mAppMct = num4,
        mEffRange = eeffRange,
        mFlags = buffFlags,
        custom_targets = strArray1,
        tags = strArray2,
        buffs = buffArray
      };
    }
  }
}
