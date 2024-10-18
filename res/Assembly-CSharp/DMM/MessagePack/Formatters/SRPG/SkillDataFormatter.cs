// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.SkillDataFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class SkillDataFormatter : IMessagePackFormatter<SkillData>, IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public SkillDataFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "SkillParam",
          0
        },
        {
          "SkillID",
          1
        },
        {
          "Rank",
          2
        },
        {
          "Name",
          3
        },
        {
          "SkillType",
          4
        },
        {
          "Target",
          5
        },
        {
          "Timing",
          6
        },
        {
          "Condition",
          7
        },
        {
          "Cost",
          8
        },
        {
          "LineType",
          9
        },
        {
          "EnableAttackGridHeight",
          10
        },
        {
          "RangeMin",
          11
        },
        {
          "RangeMax",
          12
        },
        {
          "Scope",
          13
        },
        {
          "HpCostRate",
          14
        },
        {
          "CastType",
          15
        },
        {
          "CastSpeed",
          16
        },
        {
          "EffectType",
          17
        },
        {
          "EffectRate",
          18
        },
        {
          "EffectValue",
          19
        },
        {
          "EffectRange",
          20
        },
        {
          "EffectHpMaxRate",
          21
        },
        {
          "EffectGemsMaxRate",
          22
        },
        {
          "EffectCalcType",
          23
        },
        {
          "ElementType",
          24
        },
        {
          "ElementValue",
          25
        },
        {
          "AttackType",
          26
        },
        {
          "AttackDetailType",
          27
        },
        {
          "BackAttackDefenseDownRate",
          28
        },
        {
          "SideAttackDefenseDownRate",
          29
        },
        {
          "ReactionDamageType",
          30
        },
        {
          "DamageAbsorbRate",
          31
        },
        {
          "ControlDamageRate",
          32
        },
        {
          "ControlDamageValue",
          33
        },
        {
          "ControlDamageCalcType",
          34
        },
        {
          "ControlChargeTimeRate",
          35
        },
        {
          "ControlChargeTimeValue",
          36
        },
        {
          "ControlChargeTimeCalcType",
          37
        },
        {
          "ShieldType",
          38
        },
        {
          "ShieldDamageType",
          39
        },
        {
          "ShieldTurn",
          40
        },
        {
          "ShieldValue",
          41
        },
        {
          "UseRate",
          42
        },
        {
          "UseCondition",
          43
        },
        {
          "CheckCount",
          44
        },
        {
          "DuplicateCount",
          45
        },
        {
          "IsCollabo",
          46
        },
        {
          "ReplaceSkillId",
          47
        },
        {
          "TeleportType",
          48
        },
        {
          "TeleportTarget",
          49
        },
        {
          "TeleportHeight",
          50
        },
        {
          "TeleportIsMove",
          51
        },
        {
          "TeleportSkillPos",
          52
        },
        {
          "KnockBackRate",
          53
        },
        {
          "KnockBackVal",
          54
        },
        {
          "KnockBackDir",
          55
        },
        {
          "KnockBackDs",
          56
        },
        {
          "WeatherRate",
          57
        },
        {
          "WeatherId",
          58
        },
        {
          "ElementSpcAtkRate",
          59
        },
        {
          "MaxDamageValue",
          60
        },
        {
          "CutInConceptCardId",
          61
        },
        {
          "JumpSpcAtkRate",
          62
        },
        {
          "OwnerAbiliy",
          63
        },
        {
          "IsDerivedSkill",
          64
        },
        {
          "DeriveParam",
          65
        },
        {
          "IsCreatedByConceptCard",
          66
        },
        {
          "ConceptCard",
          67
        },
        {
          "IsDecreaseEffectOnSubSlot",
          68
        },
        {
          "DecreaseEffectRate",
          69
        },
        {
          "ProtectSkill",
          70
        },
        {
          "ProtectType",
          71
        },
        {
          "ProtectDamageType",
          72
        },
        {
          "ProtectRange",
          73
        },
        {
          "ProtectHeight",
          74
        },
        {
          "ProtectValue",
          75
        },
        {
          "IsForcedTargeting",
          76
        },
        {
          "ForcedTargetingTurn",
          77
        },
        {
          "IsForcedTargetingSkillEffect",
          78
        },
        {
          "IsTargetGridNoUnit",
          79
        },
        {
          "IsTargetValidGrid",
          80
        },
        {
          "IsSkillCountNoLimit",
          81
        },
        {
          "IsTargetTeleport",
          82
        },
        {
          "IsNeedResetDirection",
          83
        },
        {
          "IsShowSkillNamePlate",
          84
        },
        {
          "IsAllowCutIn",
          85
        },
        {
          "IsAdditionalSkill",
          86
        },
        {
          "SkillAdditional",
          87
        },
        {
          "SkillAntiShield",
          88
        },
        {
          "mTargetBuffEffect",
          89
        },
        {
          "mTargetCondEffect",
          90
        },
        {
          "mSelfBuffEffect",
          91
        },
        {
          "mSelfCondEffect",
          92
        },
        {
          "m_BaseSkillIname",
          93
        }
      };
      this.____stringByteKeys = new byte[94][]
      {
        MessagePackBinary.GetEncodedStringBytes("SkillParam"),
        MessagePackBinary.GetEncodedStringBytes("SkillID"),
        MessagePackBinary.GetEncodedStringBytes("Rank"),
        MessagePackBinary.GetEncodedStringBytes("Name"),
        MessagePackBinary.GetEncodedStringBytes("SkillType"),
        MessagePackBinary.GetEncodedStringBytes("Target"),
        MessagePackBinary.GetEncodedStringBytes("Timing"),
        MessagePackBinary.GetEncodedStringBytes("Condition"),
        MessagePackBinary.GetEncodedStringBytes("Cost"),
        MessagePackBinary.GetEncodedStringBytes("LineType"),
        MessagePackBinary.GetEncodedStringBytes("EnableAttackGridHeight"),
        MessagePackBinary.GetEncodedStringBytes("RangeMin"),
        MessagePackBinary.GetEncodedStringBytes("RangeMax"),
        MessagePackBinary.GetEncodedStringBytes("Scope"),
        MessagePackBinary.GetEncodedStringBytes("HpCostRate"),
        MessagePackBinary.GetEncodedStringBytes("CastType"),
        MessagePackBinary.GetEncodedStringBytes("CastSpeed"),
        MessagePackBinary.GetEncodedStringBytes("EffectType"),
        MessagePackBinary.GetEncodedStringBytes("EffectRate"),
        MessagePackBinary.GetEncodedStringBytes("EffectValue"),
        MessagePackBinary.GetEncodedStringBytes("EffectRange"),
        MessagePackBinary.GetEncodedStringBytes("EffectHpMaxRate"),
        MessagePackBinary.GetEncodedStringBytes("EffectGemsMaxRate"),
        MessagePackBinary.GetEncodedStringBytes("EffectCalcType"),
        MessagePackBinary.GetEncodedStringBytes("ElementType"),
        MessagePackBinary.GetEncodedStringBytes("ElementValue"),
        MessagePackBinary.GetEncodedStringBytes("AttackType"),
        MessagePackBinary.GetEncodedStringBytes("AttackDetailType"),
        MessagePackBinary.GetEncodedStringBytes("BackAttackDefenseDownRate"),
        MessagePackBinary.GetEncodedStringBytes("SideAttackDefenseDownRate"),
        MessagePackBinary.GetEncodedStringBytes("ReactionDamageType"),
        MessagePackBinary.GetEncodedStringBytes("DamageAbsorbRate"),
        MessagePackBinary.GetEncodedStringBytes("ControlDamageRate"),
        MessagePackBinary.GetEncodedStringBytes("ControlDamageValue"),
        MessagePackBinary.GetEncodedStringBytes("ControlDamageCalcType"),
        MessagePackBinary.GetEncodedStringBytes("ControlChargeTimeRate"),
        MessagePackBinary.GetEncodedStringBytes("ControlChargeTimeValue"),
        MessagePackBinary.GetEncodedStringBytes("ControlChargeTimeCalcType"),
        MessagePackBinary.GetEncodedStringBytes("ShieldType"),
        MessagePackBinary.GetEncodedStringBytes("ShieldDamageType"),
        MessagePackBinary.GetEncodedStringBytes("ShieldTurn"),
        MessagePackBinary.GetEncodedStringBytes("ShieldValue"),
        MessagePackBinary.GetEncodedStringBytes("UseRate"),
        MessagePackBinary.GetEncodedStringBytes("UseCondition"),
        MessagePackBinary.GetEncodedStringBytes("CheckCount"),
        MessagePackBinary.GetEncodedStringBytes("DuplicateCount"),
        MessagePackBinary.GetEncodedStringBytes("IsCollabo"),
        MessagePackBinary.GetEncodedStringBytes("ReplaceSkillId"),
        MessagePackBinary.GetEncodedStringBytes("TeleportType"),
        MessagePackBinary.GetEncodedStringBytes("TeleportTarget"),
        MessagePackBinary.GetEncodedStringBytes("TeleportHeight"),
        MessagePackBinary.GetEncodedStringBytes("TeleportIsMove"),
        MessagePackBinary.GetEncodedStringBytes("TeleportSkillPos"),
        MessagePackBinary.GetEncodedStringBytes("KnockBackRate"),
        MessagePackBinary.GetEncodedStringBytes("KnockBackVal"),
        MessagePackBinary.GetEncodedStringBytes("KnockBackDir"),
        MessagePackBinary.GetEncodedStringBytes("KnockBackDs"),
        MessagePackBinary.GetEncodedStringBytes("WeatherRate"),
        MessagePackBinary.GetEncodedStringBytes("WeatherId"),
        MessagePackBinary.GetEncodedStringBytes("ElementSpcAtkRate"),
        MessagePackBinary.GetEncodedStringBytes("MaxDamageValue"),
        MessagePackBinary.GetEncodedStringBytes("CutInConceptCardId"),
        MessagePackBinary.GetEncodedStringBytes("JumpSpcAtkRate"),
        MessagePackBinary.GetEncodedStringBytes("OwnerAbiliy"),
        MessagePackBinary.GetEncodedStringBytes("IsDerivedSkill"),
        MessagePackBinary.GetEncodedStringBytes("DeriveParam"),
        MessagePackBinary.GetEncodedStringBytes("IsCreatedByConceptCard"),
        MessagePackBinary.GetEncodedStringBytes("ConceptCard"),
        MessagePackBinary.GetEncodedStringBytes("IsDecreaseEffectOnSubSlot"),
        MessagePackBinary.GetEncodedStringBytes("DecreaseEffectRate"),
        MessagePackBinary.GetEncodedStringBytes("ProtectSkill"),
        MessagePackBinary.GetEncodedStringBytes("ProtectType"),
        MessagePackBinary.GetEncodedStringBytes("ProtectDamageType"),
        MessagePackBinary.GetEncodedStringBytes("ProtectRange"),
        MessagePackBinary.GetEncodedStringBytes("ProtectHeight"),
        MessagePackBinary.GetEncodedStringBytes("ProtectValue"),
        MessagePackBinary.GetEncodedStringBytes("IsForcedTargeting"),
        MessagePackBinary.GetEncodedStringBytes("ForcedTargetingTurn"),
        MessagePackBinary.GetEncodedStringBytes("IsForcedTargetingSkillEffect"),
        MessagePackBinary.GetEncodedStringBytes("IsTargetGridNoUnit"),
        MessagePackBinary.GetEncodedStringBytes("IsTargetValidGrid"),
        MessagePackBinary.GetEncodedStringBytes("IsSkillCountNoLimit"),
        MessagePackBinary.GetEncodedStringBytes("IsTargetTeleport"),
        MessagePackBinary.GetEncodedStringBytes("IsNeedResetDirection"),
        MessagePackBinary.GetEncodedStringBytes("IsShowSkillNamePlate"),
        MessagePackBinary.GetEncodedStringBytes("IsAllowCutIn"),
        MessagePackBinary.GetEncodedStringBytes("IsAdditionalSkill"),
        MessagePackBinary.GetEncodedStringBytes("SkillAdditional"),
        MessagePackBinary.GetEncodedStringBytes("SkillAntiShield"),
        MessagePackBinary.GetEncodedStringBytes("mTargetBuffEffect"),
        MessagePackBinary.GetEncodedStringBytes("mTargetCondEffect"),
        MessagePackBinary.GetEncodedStringBytes("mSelfBuffEffect"),
        MessagePackBinary.GetEncodedStringBytes("mSelfCondEffect"),
        MessagePackBinary.GetEncodedStringBytes("m_BaseSkillIname")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      SkillData value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, 94);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<SkillParam>().Serialize(ref bytes, offset, value.SkillParam, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.SkillID, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Rank);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.Name, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<ESkillType>().Serialize(ref bytes, offset, value.SkillType, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<ESkillTarget>().Serialize(ref bytes, offset, value.Target, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += formatterResolver.GetFormatterWithVerify<ESkillTiming>().Serialize(ref bytes, offset, value.Timing, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += formatterResolver.GetFormatterWithVerify<ESkillCondition>().Serialize(ref bytes, offset, value.Condition, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Cost);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += formatterResolver.GetFormatterWithVerify<ELineType>().Serialize(ref bytes, offset, value.LineType, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.EnableAttackGridHeight);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.RangeMin);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.RangeMax);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Scope);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.HpCostRate);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
      offset += formatterResolver.GetFormatterWithVerify<ECastTypes>().Serialize(ref bytes, offset, value.CastType, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[16]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.CastSpeed, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[17]);
      offset += formatterResolver.GetFormatterWithVerify<SkillEffectTypes>().Serialize(ref bytes, offset, value.EffectType, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[18]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.EffectRate, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[19]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.EffectValue, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[20]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.EffectRange, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[21]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.EffectHpMaxRate, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[22]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.EffectGemsMaxRate, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[23]);
      offset += formatterResolver.GetFormatterWithVerify<SkillParamCalcTypes>().Serialize(ref bytes, offset, value.EffectCalcType, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[24]);
      offset += formatterResolver.GetFormatterWithVerify<EElement>().Serialize(ref bytes, offset, value.ElementType, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[25]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.ElementValue, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[26]);
      offset += formatterResolver.GetFormatterWithVerify<AttackTypes>().Serialize(ref bytes, offset, value.AttackType, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[27]);
      offset += formatterResolver.GetFormatterWithVerify<AttackDetailTypes>().Serialize(ref bytes, offset, value.AttackDetailType, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[28]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.BackAttackDefenseDownRate);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[29]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.SideAttackDefenseDownRate);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[30]);
      offset += formatterResolver.GetFormatterWithVerify<DamageTypes>().Serialize(ref bytes, offset, value.ReactionDamageType, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[31]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.DamageAbsorbRate);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[32]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.ControlDamageRate, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[33]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.ControlDamageValue, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[34]);
      offset += formatterResolver.GetFormatterWithVerify<SkillParamCalcTypes>().Serialize(ref bytes, offset, value.ControlDamageCalcType, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[35]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.ControlChargeTimeRate, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[36]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.ControlChargeTimeValue, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[37]);
      offset += formatterResolver.GetFormatterWithVerify<SkillParamCalcTypes>().Serialize(ref bytes, offset, value.ControlChargeTimeCalcType, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[38]);
      offset += formatterResolver.GetFormatterWithVerify<ShieldTypes>().Serialize(ref bytes, offset, value.ShieldType, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[39]);
      offset += formatterResolver.GetFormatterWithVerify<DamageTypes>().Serialize(ref bytes, offset, value.ShieldDamageType, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[40]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.ShieldTurn, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[41]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.ShieldValue, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[42]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.UseRate, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[43]);
      offset += formatterResolver.GetFormatterWithVerify<SkillLockCondition>().Serialize(ref bytes, offset, value.UseCondition, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[44]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.CheckCount);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[45]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.DuplicateCount);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[46]);
      offset += formatterResolver.GetFormatterWithVerify<OBool>().Serialize(ref bytes, offset, value.IsCollabo, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[47]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.ReplaceSkillId, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[48]);
      offset += formatterResolver.GetFormatterWithVerify<eTeleportType>().Serialize(ref bytes, offset, value.TeleportType, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[49]);
      offset += formatterResolver.GetFormatterWithVerify<ESkillTarget>().Serialize(ref bytes, offset, value.TeleportTarget, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[50]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.TeleportHeight);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[51]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.TeleportIsMove);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[52]);
      offset += formatterResolver.GetFormatterWithVerify<eTeleportSkillPos>().Serialize(ref bytes, offset, value.TeleportSkillPos, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[53]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.KnockBackRate, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[54]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.KnockBackVal, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[55]);
      offset += formatterResolver.GetFormatterWithVerify<eKnockBackDir>().Serialize(ref bytes, offset, value.KnockBackDir, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[56]);
      offset += formatterResolver.GetFormatterWithVerify<eKnockBackDs>().Serialize(ref bytes, offset, value.KnockBackDs, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[57]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.WeatherRate);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[58]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.WeatherId, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[59]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ElementSpcAtkRate);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[60]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.MaxDamageValue);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[61]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.CutInConceptCardId, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[62]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.JumpSpcAtkRate);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[63]);
      offset += formatterResolver.GetFormatterWithVerify<AbilityData>().Serialize(ref bytes, offset, value.OwnerAbiliy, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[64]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsDerivedSkill);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[65]);
      offset += formatterResolver.GetFormatterWithVerify<SkillDeriveParam>().Serialize(ref bytes, offset, value.DeriveParam, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[66]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsCreatedByConceptCard);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[67]);
      offset += formatterResolver.GetFormatterWithVerify<ConceptCardData>().Serialize(ref bytes, offset, value.ConceptCard, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[68]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsDecreaseEffectOnSubSlot);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[69]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.DecreaseEffectRate);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[70]);
      offset += formatterResolver.GetFormatterWithVerify<ProtectSkillParam>().Serialize(ref bytes, offset, value.ProtectSkill, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[71]);
      offset += formatterResolver.GetFormatterWithVerify<ProtectTypes>().Serialize(ref bytes, offset, value.ProtectType, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[72]);
      offset += formatterResolver.GetFormatterWithVerify<DamageTypes>().Serialize(ref bytes, offset, value.ProtectDamageType, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[73]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.ProtectRange, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[74]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.ProtectHeight, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[75]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ProtectValue);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[76]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsForcedTargeting);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[77]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ForcedTargetingTurn);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[78]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsForcedTargetingSkillEffect);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[79]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsTargetGridNoUnit);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[80]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsTargetValidGrid);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[81]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsSkillCountNoLimit);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[82]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsTargetTeleport);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[83]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsNeedResetDirection);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[84]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsShowSkillNamePlate);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[85]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsAllowCutIn);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[86]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsAdditionalSkill);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[87]);
      offset += formatterResolver.GetFormatterWithVerify<SkillAdditionalParam>().Serialize(ref bytes, offset, value.SkillAdditional, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[88]);
      offset += formatterResolver.GetFormatterWithVerify<SkillAntiShieldParam>().Serialize(ref bytes, offset, value.SkillAntiShield, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[89]);
      offset += formatterResolver.GetFormatterWithVerify<BuffEffect>().Serialize(ref bytes, offset, value.mTargetBuffEffect, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[90]);
      offset += formatterResolver.GetFormatterWithVerify<CondEffect>().Serialize(ref bytes, offset, value.mTargetCondEffect, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[91]);
      offset += formatterResolver.GetFormatterWithVerify<BuffEffect>().Serialize(ref bytes, offset, value.mSelfBuffEffect, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[92]);
      offset += formatterResolver.GetFormatterWithVerify<CondEffect>().Serialize(ref bytes, offset, value.mSelfCondEffect, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[93]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.m_BaseSkillIname, formatterResolver);
      return offset - num;
    }

    public SkillData Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (SkillData) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      SkillParam skillParam = (SkillParam) null;
      string str1 = (string) null;
      int num3 = 0;
      string str2 = (string) null;
      ESkillType eskillType = ESkillType.Attack;
      ESkillTarget eskillTarget1 = ESkillTarget.Self;
      ESkillTiming eskillTiming = ESkillTiming.Used;
      ESkillCondition eskillCondition = ESkillCondition.None;
      int num4 = 0;
      ELineType elineType = ELineType.None;
      int num5 = 0;
      int num6 = 0;
      int num7 = 0;
      int num8 = 0;
      int num9 = 0;
      ECastTypes ecastTypes = ECastTypes.Chant;
      OInt oint1 = new OInt();
      SkillEffectTypes skillEffectTypes = SkillEffectTypes.None;
      OInt oint2 = new OInt();
      OInt oint3 = new OInt();
      OInt oint4 = new OInt();
      OInt oint5 = new OInt();
      OInt oint6 = new OInt();
      SkillParamCalcTypes skillParamCalcTypes1 = SkillParamCalcTypes.Add;
      EElement eelement = EElement.None;
      OInt oint7 = new OInt();
      AttackTypes attackTypes = AttackTypes.None;
      AttackDetailTypes attackDetailTypes = AttackDetailTypes.None;
      int num10 = 0;
      int num11 = 0;
      DamageTypes damageTypes1 = DamageTypes.None;
      int num12 = 0;
      OInt oint8 = new OInt();
      OInt oint9 = new OInt();
      SkillParamCalcTypes skillParamCalcTypes2 = SkillParamCalcTypes.Add;
      OInt oint10 = new OInt();
      OInt oint11 = new OInt();
      SkillParamCalcTypes skillParamCalcTypes3 = SkillParamCalcTypes.Add;
      ShieldTypes shieldTypes = ShieldTypes.None;
      DamageTypes damageTypes2 = DamageTypes.None;
      OInt oint12 = new OInt();
      OInt oint13 = new OInt();
      OInt oint14 = new OInt();
      SkillLockCondition skillLockCondition = (SkillLockCondition) null;
      bool flag1 = false;
      int num13 = 0;
      OBool obool = new OBool();
      string str3 = (string) null;
      eTeleportType eTeleportType = eTeleportType.None;
      ESkillTarget eskillTarget2 = ESkillTarget.Self;
      int num14 = 0;
      bool flag2 = false;
      eTeleportSkillPos teleportSkillPos = eTeleportSkillPos.None;
      OInt oint15 = new OInt();
      OInt oint16 = new OInt();
      eKnockBackDir eKnockBackDir = eKnockBackDir.Back;
      eKnockBackDs eKnockBackDs = eKnockBackDs.Target;
      int num15 = 0;
      string str4 = (string) null;
      int num16 = 0;
      int num17 = 0;
      string str5 = (string) null;
      int num18 = 0;
      AbilityData abilityData = (AbilityData) null;
      bool flag3 = false;
      SkillDeriveParam skillDeriveParam = (SkillDeriveParam) null;
      bool flag4 = false;
      ConceptCardData conceptCardData = (ConceptCardData) null;
      bool flag5 = false;
      int num19 = 0;
      ProtectSkillParam protectSkillParam = (ProtectSkillParam) null;
      ProtectTypes protectTypes = ProtectTypes.None;
      DamageTypes damageTypes3 = DamageTypes.None;
      OInt oint17 = new OInt();
      OInt oint18 = new OInt();
      int num20 = 0;
      bool flag6 = false;
      int num21 = 0;
      bool flag7 = false;
      bool flag8 = false;
      bool flag9 = false;
      bool flag10 = false;
      bool flag11 = false;
      bool flag12 = false;
      bool flag13 = false;
      bool flag14 = false;
      bool flag15 = false;
      SkillAdditionalParam skillAdditionalParam = (SkillAdditionalParam) null;
      SkillAntiShieldParam skillAntiShieldParam = (SkillAntiShieldParam) null;
      BuffEffect buffEffect1 = (BuffEffect) null;
      CondEffect condEffect1 = (CondEffect) null;
      BuffEffect buffEffect2 = (BuffEffect) null;
      CondEffect condEffect2 = (CondEffect) null;
      string str6 = (string) null;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num22;
        if (!this.____keyMapping.TryGetValueSafe(key, out num22))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num22)
          {
            case 0:
              skillParam = formatterResolver.GetFormatterWithVerify<SkillParam>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              str1 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 3:
              str2 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              eskillType = formatterResolver.GetFormatterWithVerify<ESkillType>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 5:
              eskillTarget1 = formatterResolver.GetFormatterWithVerify<ESkillTarget>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 6:
              eskillTiming = formatterResolver.GetFormatterWithVerify<ESkillTiming>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 7:
              eskillCondition = formatterResolver.GetFormatterWithVerify<ESkillCondition>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 8:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 9:
              elineType = formatterResolver.GetFormatterWithVerify<ELineType>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 10:
              num5 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 11:
              num6 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 12:
              num7 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 13:
              num8 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 14:
              num9 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 15:
              ecastTypes = formatterResolver.GetFormatterWithVerify<ECastTypes>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 16:
              oint1 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 17:
              skillEffectTypes = formatterResolver.GetFormatterWithVerify<SkillEffectTypes>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 18:
              oint2 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 19:
              oint3 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 20:
              oint4 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 21:
              oint5 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 22:
              oint6 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 23:
              skillParamCalcTypes1 = formatterResolver.GetFormatterWithVerify<SkillParamCalcTypes>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 24:
              eelement = formatterResolver.GetFormatterWithVerify<EElement>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 25:
              oint7 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 26:
              attackTypes = formatterResolver.GetFormatterWithVerify<AttackTypes>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 27:
              attackDetailTypes = formatterResolver.GetFormatterWithVerify<AttackDetailTypes>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 28:
              num10 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 29:
              num11 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 30:
              damageTypes1 = formatterResolver.GetFormatterWithVerify<DamageTypes>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 31:
              num12 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 32:
              oint8 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 33:
              oint9 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 34:
              skillParamCalcTypes2 = formatterResolver.GetFormatterWithVerify<SkillParamCalcTypes>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 35:
              oint10 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 36:
              oint11 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 37:
              skillParamCalcTypes3 = formatterResolver.GetFormatterWithVerify<SkillParamCalcTypes>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 38:
              shieldTypes = formatterResolver.GetFormatterWithVerify<ShieldTypes>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 39:
              damageTypes2 = formatterResolver.GetFormatterWithVerify<DamageTypes>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 40:
              oint12 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 41:
              oint13 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 42:
              oint14 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 43:
              skillLockCondition = formatterResolver.GetFormatterWithVerify<SkillLockCondition>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 44:
              flag1 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 45:
              num13 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 46:
              obool = formatterResolver.GetFormatterWithVerify<OBool>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 47:
              str3 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 48:
              eTeleportType = formatterResolver.GetFormatterWithVerify<eTeleportType>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 49:
              eskillTarget2 = formatterResolver.GetFormatterWithVerify<ESkillTarget>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 50:
              num14 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 51:
              flag2 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 52:
              teleportSkillPos = formatterResolver.GetFormatterWithVerify<eTeleportSkillPos>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 53:
              oint15 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 54:
              oint16 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 55:
              eKnockBackDir = formatterResolver.GetFormatterWithVerify<eKnockBackDir>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 56:
              eKnockBackDs = formatterResolver.GetFormatterWithVerify<eKnockBackDs>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 57:
              num15 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 58:
              str4 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 59:
              num16 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 60:
              num17 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 61:
              str5 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 62:
              num18 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 63:
              abilityData = formatterResolver.GetFormatterWithVerify<AbilityData>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 64:
              flag3 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 65:
              skillDeriveParam = formatterResolver.GetFormatterWithVerify<SkillDeriveParam>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 66:
              flag4 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 67:
              conceptCardData = formatterResolver.GetFormatterWithVerify<ConceptCardData>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 68:
              flag5 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 69:
              num19 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 70:
              protectSkillParam = formatterResolver.GetFormatterWithVerify<ProtectSkillParam>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 71:
              protectTypes = formatterResolver.GetFormatterWithVerify<ProtectTypes>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 72:
              damageTypes3 = formatterResolver.GetFormatterWithVerify<DamageTypes>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 73:
              oint17 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 74:
              oint18 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 75:
              num20 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 76:
              flag6 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 77:
              num21 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 78:
              flag7 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 79:
              flag8 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 80:
              flag9 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 81:
              flag10 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 82:
              flag11 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 83:
              flag12 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 84:
              flag13 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 85:
              flag14 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 86:
              flag15 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 87:
              skillAdditionalParam = formatterResolver.GetFormatterWithVerify<SkillAdditionalParam>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 88:
              skillAntiShieldParam = formatterResolver.GetFormatterWithVerify<SkillAntiShieldParam>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 89:
              buffEffect1 = formatterResolver.GetFormatterWithVerify<BuffEffect>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 90:
              condEffect1 = formatterResolver.GetFormatterWithVerify<CondEffect>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 91:
              buffEffect2 = formatterResolver.GetFormatterWithVerify<BuffEffect>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 92:
              condEffect2 = formatterResolver.GetFormatterWithVerify<CondEffect>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 93:
              str6 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new SkillData()
      {
        UseRate = oint14,
        UseCondition = skillLockCondition,
        CheckCount = flag1,
        IsCollabo = obool,
        ReplaceSkillId = str3,
        mTargetBuffEffect = buffEffect1,
        mTargetCondEffect = condEffect1,
        mSelfBuffEffect = buffEffect2,
        mSelfCondEffect = condEffect2,
        m_BaseSkillIname = str6
      };
    }
  }
}
