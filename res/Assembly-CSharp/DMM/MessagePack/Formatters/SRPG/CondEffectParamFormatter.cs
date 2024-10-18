// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.CondEffectParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class CondEffectParamFormatter : 
    IMessagePackFormatter<CondEffectParam>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public CondEffectParamFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "IsLinkBuffDupli",
          0
        },
        {
          "IsLinkBuffResist",
          1
        },
        {
          "iname",
          2
        },
        {
          "job",
          3
        },
        {
          "buki",
          4
        },
        {
          "birth",
          5
        },
        {
          "sex",
          6
        },
        {
          "elem",
          7
        },
        {
          "type",
          8
        },
        {
          "cond",
          9
        },
        {
          "value_ini",
          10
        },
        {
          "value_max",
          11
        },
        {
          "rate_ini",
          12
        },
        {
          "rate_max",
          13
        },
        {
          "turn_ini",
          14
        },
        {
          "turn_max",
          15
        },
        {
          "chk_target",
          16
        },
        {
          "chk_timing",
          17
        },
        {
          "conditions",
          18
        },
        {
          "BuffIds",
          19
        },
        {
          "tags",
          20
        },
        {
          "un_group",
          21
        },
        {
          "custom_targets",
          22
        },
        {
          "v_poison_rate",
          23
        },
        {
          "v_poison_fix",
          24
        },
        {
          "v_paralyse_rate",
          25
        },
        {
          "v_blink_hit",
          26
        },
        {
          "v_blink_avo",
          27
        },
        {
          "v_death_count",
          28
        },
        {
          "v_berserk_atk",
          29
        },
        {
          "v_berserk_def",
          30
        },
        {
          "v_fast",
          31
        },
        {
          "v_slow",
          32
        },
        {
          "v_donmov",
          33
        },
        {
          "v_auto_hp_heal",
          34
        },
        {
          "v_auto_hp_heal_fix",
          35
        },
        {
          "v_auto_mp_heal",
          36
        },
        {
          "v_auto_mp_heal_fix",
          37
        },
        {
          "curse",
          38
        }
      };
      this.____stringByteKeys = new byte[39][]
      {
        MessagePackBinary.GetEncodedStringBytes("IsLinkBuffDupli"),
        MessagePackBinary.GetEncodedStringBytes("IsLinkBuffResist"),
        MessagePackBinary.GetEncodedStringBytes("iname"),
        MessagePackBinary.GetEncodedStringBytes("job"),
        MessagePackBinary.GetEncodedStringBytes("buki"),
        MessagePackBinary.GetEncodedStringBytes("birth"),
        MessagePackBinary.GetEncodedStringBytes("sex"),
        MessagePackBinary.GetEncodedStringBytes("elem"),
        MessagePackBinary.GetEncodedStringBytes("type"),
        MessagePackBinary.GetEncodedStringBytes("cond"),
        MessagePackBinary.GetEncodedStringBytes("value_ini"),
        MessagePackBinary.GetEncodedStringBytes("value_max"),
        MessagePackBinary.GetEncodedStringBytes("rate_ini"),
        MessagePackBinary.GetEncodedStringBytes("rate_max"),
        MessagePackBinary.GetEncodedStringBytes("turn_ini"),
        MessagePackBinary.GetEncodedStringBytes("turn_max"),
        MessagePackBinary.GetEncodedStringBytes("chk_target"),
        MessagePackBinary.GetEncodedStringBytes("chk_timing"),
        MessagePackBinary.GetEncodedStringBytes("conditions"),
        MessagePackBinary.GetEncodedStringBytes("BuffIds"),
        MessagePackBinary.GetEncodedStringBytes("tags"),
        MessagePackBinary.GetEncodedStringBytes("un_group"),
        MessagePackBinary.GetEncodedStringBytes("custom_targets"),
        MessagePackBinary.GetEncodedStringBytes("v_poison_rate"),
        MessagePackBinary.GetEncodedStringBytes("v_poison_fix"),
        MessagePackBinary.GetEncodedStringBytes("v_paralyse_rate"),
        MessagePackBinary.GetEncodedStringBytes("v_blink_hit"),
        MessagePackBinary.GetEncodedStringBytes("v_blink_avo"),
        MessagePackBinary.GetEncodedStringBytes("v_death_count"),
        MessagePackBinary.GetEncodedStringBytes("v_berserk_atk"),
        MessagePackBinary.GetEncodedStringBytes("v_berserk_def"),
        MessagePackBinary.GetEncodedStringBytes("v_fast"),
        MessagePackBinary.GetEncodedStringBytes("v_slow"),
        MessagePackBinary.GetEncodedStringBytes("v_donmov"),
        MessagePackBinary.GetEncodedStringBytes("v_auto_hp_heal"),
        MessagePackBinary.GetEncodedStringBytes("v_auto_hp_heal_fix"),
        MessagePackBinary.GetEncodedStringBytes("v_auto_mp_heal"),
        MessagePackBinary.GetEncodedStringBytes("v_auto_mp_heal_fix"),
        MessagePackBinary.GetEncodedStringBytes("curse")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      CondEffectParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, 39);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsLinkBuffDupli);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsLinkBuffResist);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.job, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.buki, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.birth, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += formatterResolver.GetFormatterWithVerify<ESex>().Serialize(ref bytes, offset, value.sex, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += formatterResolver.GetFormatterWithVerify<EElement>().Serialize(ref bytes, offset, value.elem, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += formatterResolver.GetFormatterWithVerify<ConditionEffectTypes>().Serialize(ref bytes, offset, value.type, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += formatterResolver.GetFormatterWithVerify<ESkillCondition>().Serialize(ref bytes, offset, value.cond, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.value_ini, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.value_max, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.rate_ini, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.rate_max, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.turn_ini, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.turn_max, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[16]);
      offset += formatterResolver.GetFormatterWithVerify<EffectCheckTargets>().Serialize(ref bytes, offset, value.chk_target, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[17]);
      offset += formatterResolver.GetFormatterWithVerify<EffectCheckTimings>().Serialize(ref bytes, offset, value.chk_timing, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[18]);
      offset += formatterResolver.GetFormatterWithVerify<EUnitCondition[]>().Serialize(ref bytes, offset, value.conditions, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[19]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.BuffIds, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[20]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.tags, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[21]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.un_group, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[22]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.custom_targets, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[23]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.v_poison_rate, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[24]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.v_poison_fix, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[25]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.v_paralyse_rate, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[26]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.v_blink_hit, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[27]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.v_blink_avo, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[28]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.v_death_count, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[29]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.v_berserk_atk, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[30]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.v_berserk_def, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[31]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.v_fast, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[32]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.v_slow, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[33]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.v_donmov, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[34]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.v_auto_hp_heal, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[35]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.v_auto_hp_heal_fix, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[36]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.v_auto_mp_heal, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[37]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.v_auto_mp_heal_fix, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[38]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.curse, formatterResolver);
      return offset - num;
    }

    public CondEffectParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (CondEffectParam) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      bool flag1 = false;
      bool flag2 = false;
      string str1 = (string) null;
      string str2 = (string) null;
      string str3 = (string) null;
      string str4 = (string) null;
      ESex esex = ESex.Unknown;
      EElement eelement = EElement.None;
      ConditionEffectTypes conditionEffectTypes = ConditionEffectTypes.None;
      ESkillCondition eskillCondition = ESkillCondition.None;
      OInt oint1 = new OInt();
      OInt oint2 = new OInt();
      OInt oint3 = new OInt();
      OInt oint4 = new OInt();
      OInt oint5 = new OInt();
      OInt oint6 = new OInt();
      EffectCheckTargets effectCheckTargets = EffectCheckTargets.Target;
      EffectCheckTimings effectCheckTimings = EffectCheckTimings.ActionStart;
      EUnitCondition[] eunitConditionArray = (EUnitCondition[]) null;
      string[] strArray1 = (string[]) null;
      string[] strArray2 = (string[]) null;
      string str5 = (string) null;
      string[] strArray3 = (string[]) null;
      OInt oint7 = new OInt();
      OInt oint8 = new OInt();
      OInt oint9 = new OInt();
      OInt oint10 = new OInt();
      OInt oint11 = new OInt();
      OInt oint12 = new OInt();
      OInt oint13 = new OInt();
      OInt oint14 = new OInt();
      OInt oint15 = new OInt();
      OInt oint16 = new OInt();
      OInt oint17 = new OInt();
      OInt oint18 = new OInt();
      OInt oint19 = new OInt();
      OInt oint20 = new OInt();
      OInt oint21 = new OInt();
      OInt oint22 = new OInt();
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
              flag1 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 1:
              flag2 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 2:
              str1 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              str2 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              str3 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 5:
              str4 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 6:
              esex = formatterResolver.GetFormatterWithVerify<ESex>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 7:
              eelement = formatterResolver.GetFormatterWithVerify<EElement>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 8:
              conditionEffectTypes = formatterResolver.GetFormatterWithVerify<ConditionEffectTypes>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 9:
              eskillCondition = formatterResolver.GetFormatterWithVerify<ESkillCondition>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 10:
              oint1 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 11:
              oint2 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 12:
              oint3 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 13:
              oint4 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 14:
              oint5 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 15:
              oint6 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 16:
              effectCheckTargets = formatterResolver.GetFormatterWithVerify<EffectCheckTargets>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 17:
              effectCheckTimings = formatterResolver.GetFormatterWithVerify<EffectCheckTimings>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 18:
              eunitConditionArray = formatterResolver.GetFormatterWithVerify<EUnitCondition[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 19:
              strArray1 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 20:
              strArray2 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 21:
              str5 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 22:
              strArray3 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 23:
              oint7 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 24:
              oint8 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 25:
              oint9 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 26:
              oint10 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 27:
              oint11 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 28:
              oint12 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 29:
              oint13 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 30:
              oint14 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 31:
              oint15 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 32:
              oint16 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 33:
              oint17 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 34:
              oint18 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 35:
              oint19 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 36:
              oint20 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 37:
              oint21 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 38:
              oint22 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new CondEffectParam()
      {
        iname = str1,
        job = str2,
        buki = str3,
        birth = str4,
        sex = esex,
        elem = eelement,
        type = conditionEffectTypes,
        cond = eskillCondition,
        value_ini = oint1,
        value_max = oint2,
        rate_ini = oint3,
        rate_max = oint4,
        turn_ini = oint5,
        turn_max = oint6,
        chk_target = effectCheckTargets,
        chk_timing = effectCheckTimings,
        conditions = eunitConditionArray,
        BuffIds = strArray1,
        tags = strArray2,
        un_group = str5,
        custom_targets = strArray3,
        v_poison_rate = oint7,
        v_poison_fix = oint8,
        v_paralyse_rate = oint9,
        v_blink_hit = oint10,
        v_blink_avo = oint11,
        v_death_count = oint12,
        v_berserk_atk = oint13,
        v_berserk_def = oint14,
        v_fast = oint15,
        v_slow = oint16,
        v_donmov = oint17,
        v_auto_hp_heal = oint18,
        v_auto_hp_heal_fix = oint19,
        v_auto_mp_heal = oint20,
        v_auto_mp_heal_fix = oint21,
        curse = oint22
      };
    }
  }
}
