// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.SkillParamFormatter
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
  public sealed class SkillParamFormatter : IMessagePackFormatter<SkillParam>, IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public SkillParamFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "ProtectSkill",
          0
        },
        {
          "SkillAdditional",
          1
        },
        {
          "SkillAntiShield",
          2
        },
        {
          "IsCritical",
          3
        },
        {
          "IsJewelAbsorb",
          4
        },
        {
          "IsTargetGridNoUnit",
          5
        },
        {
          "IsTargetValidGrid",
          6
        },
        {
          "IsSkillCountNoLimit",
          7
        },
        {
          "IsTargetTeleport",
          8
        },
        {
          "IsForcedTargetingSkillEffect",
          9
        },
        {
          "IsNeedResetDirection",
          10
        },
        {
          "IsShowSkillNamePlate",
          11
        },
        {
          "IsAllowCutIn",
          12
        },
        {
          "IsAdditionalSkill",
          13
        },
        {
          "iname",
          14
        },
        {
          "name",
          15
        },
        {
          "expr",
          16
        },
        {
          "motion",
          17
        },
        {
          "effect",
          18
        },
        {
          "defend_effect",
          19
        },
        {
          "weapon",
          20
        },
        {
          "tokkou",
          21
        },
        {
          "tk_rate",
          22
        },
        {
          "type",
          23
        },
        {
          "timing",
          24
        },
        {
          "condition",
          25
        },
        {
          "lvcap",
          26
        },
        {
          "cost",
          27
        },
        {
          "count",
          28
        },
        {
          "rate",
          29
        },
        {
          "back_defrate",
          30
        },
        {
          "side_defrate",
          31
        },
        {
          "ignore_defense_rate",
          32
        },
        {
          "line_type",
          33
        },
        {
          "select_range",
          34
        },
        {
          "range_min",
          35
        },
        {
          "range_max",
          36
        },
        {
          "select_scope",
          37
        },
        {
          "scope",
          38
        },
        {
          "effect_height",
          39
        },
        {
          "hp_cost_rate",
          40
        },
        {
          "hp_cost",
          41
        },
        {
          "random_hit_rate",
          42
        },
        {
          "cast_type",
          43
        },
        {
          "cast_speed",
          44
        },
        {
          "target",
          45
        },
        {
          "effect_type",
          46
        },
        {
          "effect_rate",
          47
        },
        {
          "effect_value",
          48
        },
        {
          "effect_range",
          49
        },
        {
          "effect_calc",
          50
        },
        {
          "effect_hprate",
          51
        },
        {
          "effect_mprate",
          52
        },
        {
          "effect_dead_rate",
          53
        },
        {
          "effect_lvrate",
          54
        },
        {
          "absorb_damage_rate",
          55
        },
        {
          "element_type",
          56
        },
        {
          "attack_type",
          57
        },
        {
          "attack_detail",
          58
        },
        {
          "reaction_damage_type",
          59
        },
        {
          "reaction_det_lists",
          60
        },
        {
          "control_damage_rate",
          61
        },
        {
          "control_damage_value",
          62
        },
        {
          "control_damage_calc",
          63
        },
        {
          "control_ct_rate",
          64
        },
        {
          "control_ct_value",
          65
        },
        {
          "control_ct_calc",
          66
        },
        {
          "target_buff_iname",
          67
        },
        {
          "target_cond_iname",
          68
        },
        {
          "self_buff_iname",
          69
        },
        {
          "self_cond_iname",
          70
        },
        {
          "shield_type",
          71
        },
        {
          "shield_damage_type",
          72
        },
        {
          "shield_turn",
          73
        },
        {
          "shield_value",
          74
        },
        {
          "job",
          75
        },
        {
          "ComboNum",
          76
        },
        {
          "ComboDamageRate",
          77
        },
        {
          "JewelDamageType",
          78
        },
        {
          "JewelDamageValue",
          79
        },
        {
          "DuplicateCount",
          80
        },
        {
          "SceneName",
          81
        },
        {
          "SceneNameBigUnit",
          82
        },
        {
          "CollaboMainId",
          83
        },
        {
          "CollaboHeight",
          84
        },
        {
          "KnockBackRate",
          85
        },
        {
          "KnockBackVal",
          86
        },
        {
          "KnockBackDir",
          87
        },
        {
          "KnockBackDs",
          88
        },
        {
          "DamageDispType",
          89
        },
        {
          "TeleportType",
          90
        },
        {
          "TeleportTarget",
          91
        },
        {
          "TeleportHeight",
          92
        },
        {
          "TeleportIsMove",
          93
        },
        {
          "ReplaceTargetIdLists",
          94
        },
        {
          "ReplaceChangeIdLists",
          95
        },
        {
          "AbilityReplaceTargetIdLists",
          96
        },
        {
          "AbilityReplaceChangeIdLists",
          97
        },
        {
          "CollaboVoiceId",
          98
        },
        {
          "CollaboVoicePlayDelayFrame",
          99
        },
        {
          "ReplacedTargetId",
          100
        },
        {
          "TrickId",
          101
        },
        {
          "TrickSetType",
          102
        },
        {
          "BreakObjId",
          103
        },
        {
          "MapEffectDesc",
          104
        },
        {
          "WeatherRate",
          105
        },
        {
          "WeatherId",
          106
        },
        {
          "ElementSpcAtkRate",
          107
        },
        {
          "MaxDamageValue",
          108
        },
        {
          "CutInConceptCardId",
          109
        },
        {
          "JudgeHpVal",
          110
        },
        {
          "JudgeHpCalc",
          111
        },
        {
          "AcFromAbilId",
          112
        },
        {
          "AcToAbilId",
          113
        },
        {
          "AcTurn",
          114
        },
        {
          "EffectHitTargetNumRate",
          115
        },
        {
          "AbsorbAndGive",
          116
        },
        {
          "TargetEx",
          117
        },
        {
          "JumpSpcAtkRate",
          118
        },
        {
          "TeleportSkillPos",
          119
        },
        {
          "DynamicTransformUnitId",
          120
        },
        {
          "SkillMotionId",
          121
        },
        {
          "DependStateSpcEffId",
          122
        },
        {
          "DependStateSpcEffSelfId",
          123
        },
        {
          "ForcedTargetingTurn",
          124
        },
        {
          "ProtectSkillId",
          125
        },
        {
          "SkillAdditionalId",
          126
        },
        {
          "SkillAntiShieldId",
          (int) sbyte.MaxValue
        }
      };
      this.____stringByteKeys = new byte[128][]
      {
        MessagePackBinary.GetEncodedStringBytes("ProtectSkill"),
        MessagePackBinary.GetEncodedStringBytes("SkillAdditional"),
        MessagePackBinary.GetEncodedStringBytes("SkillAntiShield"),
        MessagePackBinary.GetEncodedStringBytes("IsCritical"),
        MessagePackBinary.GetEncodedStringBytes("IsJewelAbsorb"),
        MessagePackBinary.GetEncodedStringBytes("IsTargetGridNoUnit"),
        MessagePackBinary.GetEncodedStringBytes("IsTargetValidGrid"),
        MessagePackBinary.GetEncodedStringBytes("IsSkillCountNoLimit"),
        MessagePackBinary.GetEncodedStringBytes("IsTargetTeleport"),
        MessagePackBinary.GetEncodedStringBytes("IsForcedTargetingSkillEffect"),
        MessagePackBinary.GetEncodedStringBytes("IsNeedResetDirection"),
        MessagePackBinary.GetEncodedStringBytes("IsShowSkillNamePlate"),
        MessagePackBinary.GetEncodedStringBytes("IsAllowCutIn"),
        MessagePackBinary.GetEncodedStringBytes("IsAdditionalSkill"),
        MessagePackBinary.GetEncodedStringBytes("iname"),
        MessagePackBinary.GetEncodedStringBytes("name"),
        MessagePackBinary.GetEncodedStringBytes("expr"),
        MessagePackBinary.GetEncodedStringBytes("motion"),
        MessagePackBinary.GetEncodedStringBytes("effect"),
        MessagePackBinary.GetEncodedStringBytes("defend_effect"),
        MessagePackBinary.GetEncodedStringBytes("weapon"),
        MessagePackBinary.GetEncodedStringBytes("tokkou"),
        MessagePackBinary.GetEncodedStringBytes("tk_rate"),
        MessagePackBinary.GetEncodedStringBytes("type"),
        MessagePackBinary.GetEncodedStringBytes("timing"),
        MessagePackBinary.GetEncodedStringBytes("condition"),
        MessagePackBinary.GetEncodedStringBytes("lvcap"),
        MessagePackBinary.GetEncodedStringBytes("cost"),
        MessagePackBinary.GetEncodedStringBytes("count"),
        MessagePackBinary.GetEncodedStringBytes("rate"),
        MessagePackBinary.GetEncodedStringBytes("back_defrate"),
        MessagePackBinary.GetEncodedStringBytes("side_defrate"),
        MessagePackBinary.GetEncodedStringBytes("ignore_defense_rate"),
        MessagePackBinary.GetEncodedStringBytes("line_type"),
        MessagePackBinary.GetEncodedStringBytes("select_range"),
        MessagePackBinary.GetEncodedStringBytes("range_min"),
        MessagePackBinary.GetEncodedStringBytes("range_max"),
        MessagePackBinary.GetEncodedStringBytes("select_scope"),
        MessagePackBinary.GetEncodedStringBytes("scope"),
        MessagePackBinary.GetEncodedStringBytes("effect_height"),
        MessagePackBinary.GetEncodedStringBytes("hp_cost_rate"),
        MessagePackBinary.GetEncodedStringBytes("hp_cost"),
        MessagePackBinary.GetEncodedStringBytes("random_hit_rate"),
        MessagePackBinary.GetEncodedStringBytes("cast_type"),
        MessagePackBinary.GetEncodedStringBytes("cast_speed"),
        MessagePackBinary.GetEncodedStringBytes("target"),
        MessagePackBinary.GetEncodedStringBytes("effect_type"),
        MessagePackBinary.GetEncodedStringBytes("effect_rate"),
        MessagePackBinary.GetEncodedStringBytes("effect_value"),
        MessagePackBinary.GetEncodedStringBytes("effect_range"),
        MessagePackBinary.GetEncodedStringBytes("effect_calc"),
        MessagePackBinary.GetEncodedStringBytes("effect_hprate"),
        MessagePackBinary.GetEncodedStringBytes("effect_mprate"),
        MessagePackBinary.GetEncodedStringBytes("effect_dead_rate"),
        MessagePackBinary.GetEncodedStringBytes("effect_lvrate"),
        MessagePackBinary.GetEncodedStringBytes("absorb_damage_rate"),
        MessagePackBinary.GetEncodedStringBytes("element_type"),
        MessagePackBinary.GetEncodedStringBytes("attack_type"),
        MessagePackBinary.GetEncodedStringBytes("attack_detail"),
        MessagePackBinary.GetEncodedStringBytes("reaction_damage_type"),
        MessagePackBinary.GetEncodedStringBytes("reaction_det_lists"),
        MessagePackBinary.GetEncodedStringBytes("control_damage_rate"),
        MessagePackBinary.GetEncodedStringBytes("control_damage_value"),
        MessagePackBinary.GetEncodedStringBytes("control_damage_calc"),
        MessagePackBinary.GetEncodedStringBytes("control_ct_rate"),
        MessagePackBinary.GetEncodedStringBytes("control_ct_value"),
        MessagePackBinary.GetEncodedStringBytes("control_ct_calc"),
        MessagePackBinary.GetEncodedStringBytes("target_buff_iname"),
        MessagePackBinary.GetEncodedStringBytes("target_cond_iname"),
        MessagePackBinary.GetEncodedStringBytes("self_buff_iname"),
        MessagePackBinary.GetEncodedStringBytes("self_cond_iname"),
        MessagePackBinary.GetEncodedStringBytes("shield_type"),
        MessagePackBinary.GetEncodedStringBytes("shield_damage_type"),
        MessagePackBinary.GetEncodedStringBytes("shield_turn"),
        MessagePackBinary.GetEncodedStringBytes("shield_value"),
        MessagePackBinary.GetEncodedStringBytes("job"),
        MessagePackBinary.GetEncodedStringBytes("ComboNum"),
        MessagePackBinary.GetEncodedStringBytes("ComboDamageRate"),
        MessagePackBinary.GetEncodedStringBytes("JewelDamageType"),
        MessagePackBinary.GetEncodedStringBytes("JewelDamageValue"),
        MessagePackBinary.GetEncodedStringBytes("DuplicateCount"),
        MessagePackBinary.GetEncodedStringBytes("SceneName"),
        MessagePackBinary.GetEncodedStringBytes("SceneNameBigUnit"),
        MessagePackBinary.GetEncodedStringBytes("CollaboMainId"),
        MessagePackBinary.GetEncodedStringBytes("CollaboHeight"),
        MessagePackBinary.GetEncodedStringBytes("KnockBackRate"),
        MessagePackBinary.GetEncodedStringBytes("KnockBackVal"),
        MessagePackBinary.GetEncodedStringBytes("KnockBackDir"),
        MessagePackBinary.GetEncodedStringBytes("KnockBackDs"),
        MessagePackBinary.GetEncodedStringBytes("DamageDispType"),
        MessagePackBinary.GetEncodedStringBytes("TeleportType"),
        MessagePackBinary.GetEncodedStringBytes("TeleportTarget"),
        MessagePackBinary.GetEncodedStringBytes("TeleportHeight"),
        MessagePackBinary.GetEncodedStringBytes("TeleportIsMove"),
        MessagePackBinary.GetEncodedStringBytes("ReplaceTargetIdLists"),
        MessagePackBinary.GetEncodedStringBytes("ReplaceChangeIdLists"),
        MessagePackBinary.GetEncodedStringBytes("AbilityReplaceTargetIdLists"),
        MessagePackBinary.GetEncodedStringBytes("AbilityReplaceChangeIdLists"),
        MessagePackBinary.GetEncodedStringBytes("CollaboVoiceId"),
        MessagePackBinary.GetEncodedStringBytes("CollaboVoicePlayDelayFrame"),
        MessagePackBinary.GetEncodedStringBytes("ReplacedTargetId"),
        MessagePackBinary.GetEncodedStringBytes("TrickId"),
        MessagePackBinary.GetEncodedStringBytes("TrickSetType"),
        MessagePackBinary.GetEncodedStringBytes("BreakObjId"),
        MessagePackBinary.GetEncodedStringBytes("MapEffectDesc"),
        MessagePackBinary.GetEncodedStringBytes("WeatherRate"),
        MessagePackBinary.GetEncodedStringBytes("WeatherId"),
        MessagePackBinary.GetEncodedStringBytes("ElementSpcAtkRate"),
        MessagePackBinary.GetEncodedStringBytes("MaxDamageValue"),
        MessagePackBinary.GetEncodedStringBytes("CutInConceptCardId"),
        MessagePackBinary.GetEncodedStringBytes("JudgeHpVal"),
        MessagePackBinary.GetEncodedStringBytes("JudgeHpCalc"),
        MessagePackBinary.GetEncodedStringBytes("AcFromAbilId"),
        MessagePackBinary.GetEncodedStringBytes("AcToAbilId"),
        MessagePackBinary.GetEncodedStringBytes("AcTurn"),
        MessagePackBinary.GetEncodedStringBytes("EffectHitTargetNumRate"),
        MessagePackBinary.GetEncodedStringBytes("AbsorbAndGive"),
        MessagePackBinary.GetEncodedStringBytes("TargetEx"),
        MessagePackBinary.GetEncodedStringBytes("JumpSpcAtkRate"),
        MessagePackBinary.GetEncodedStringBytes("TeleportSkillPos"),
        MessagePackBinary.GetEncodedStringBytes("DynamicTransformUnitId"),
        MessagePackBinary.GetEncodedStringBytes("SkillMotionId"),
        MessagePackBinary.GetEncodedStringBytes("DependStateSpcEffId"),
        MessagePackBinary.GetEncodedStringBytes("DependStateSpcEffSelfId"),
        MessagePackBinary.GetEncodedStringBytes("ForcedTargetingTurn"),
        MessagePackBinary.GetEncodedStringBytes("ProtectSkillId"),
        MessagePackBinary.GetEncodedStringBytes("SkillAdditionalId"),
        MessagePackBinary.GetEncodedStringBytes("SkillAntiShieldId")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      SkillParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, 128);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<ProtectSkillParam>().Serialize(ref bytes, offset, value.ProtectSkill, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<SkillAdditionalParam>().Serialize(ref bytes, offset, value.SkillAdditional, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<SkillAntiShieldParam>().Serialize(ref bytes, offset, value.SkillAntiShield, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsCritical);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsJewelAbsorb);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsTargetGridNoUnit);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsTargetValidGrid);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsSkillCountNoLimit);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsTargetTeleport);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsForcedTargetingSkillEffect);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsNeedResetDirection);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsShowSkillNamePlate);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsAllowCutIn);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsAdditionalSkill);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.name, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[16]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.expr, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[17]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.motion, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[18]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.effect, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[19]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.defend_effect, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[20]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.weapon, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[21]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.tokkou, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[22]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.tk_rate);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[23]);
      offset += formatterResolver.GetFormatterWithVerify<ESkillType>().Serialize(ref bytes, offset, value.type, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[24]);
      offset += formatterResolver.GetFormatterWithVerify<ESkillTiming>().Serialize(ref bytes, offset, value.timing, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[25]);
      offset += formatterResolver.GetFormatterWithVerify<ESkillCondition>().Serialize(ref bytes, offset, value.condition, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[26]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.lvcap);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[27]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.cost);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[28]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.count);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[29]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rate);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[30]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.back_defrate);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[31]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.side_defrate);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[32]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ignore_defense_rate);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[33]);
      offset += formatterResolver.GetFormatterWithVerify<ELineType>().Serialize(ref bytes, offset, value.line_type, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[34]);
      offset += formatterResolver.GetFormatterWithVerify<ESelectType>().Serialize(ref bytes, offset, value.select_range, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[35]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.range_min);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[36]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.range_max);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[37]);
      offset += formatterResolver.GetFormatterWithVerify<ESelectType>().Serialize(ref bytes, offset, value.select_scope, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[38]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.scope);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[39]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.effect_height);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[40]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.hp_cost_rate);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[41]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.hp_cost);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[42]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.random_hit_rate);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[43]);
      offset += formatterResolver.GetFormatterWithVerify<ECastTypes>().Serialize(ref bytes, offset, value.cast_type, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[44]);
      offset += formatterResolver.GetFormatterWithVerify<SkillRankUpValueShort>().Serialize(ref bytes, offset, value.cast_speed, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[45]);
      offset += formatterResolver.GetFormatterWithVerify<ESkillTarget>().Serialize(ref bytes, offset, value.target, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[46]);
      offset += formatterResolver.GetFormatterWithVerify<SkillEffectTypes>().Serialize(ref bytes, offset, value.effect_type, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[47]);
      offset += formatterResolver.GetFormatterWithVerify<SkillRankUpValueShort>().Serialize(ref bytes, offset, value.effect_rate, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[48]);
      offset += formatterResolver.GetFormatterWithVerify<SkillRankUpValue>().Serialize(ref bytes, offset, value.effect_value, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[49]);
      offset += formatterResolver.GetFormatterWithVerify<SkillRankUpValueShort>().Serialize(ref bytes, offset, value.effect_range, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[50]);
      offset += formatterResolver.GetFormatterWithVerify<SkillParamCalcTypes>().Serialize(ref bytes, offset, value.effect_calc, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[51]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.effect_hprate);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[52]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.effect_mprate);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[53]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.effect_dead_rate);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[54]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.effect_lvrate);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[55]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.absorb_damage_rate);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[56]);
      offset += formatterResolver.GetFormatterWithVerify<EElement>().Serialize(ref bytes, offset, value.element_type, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[57]);
      offset += formatterResolver.GetFormatterWithVerify<AttackTypes>().Serialize(ref bytes, offset, value.attack_type, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[58]);
      offset += formatterResolver.GetFormatterWithVerify<AttackDetailTypes>().Serialize(ref bytes, offset, value.attack_detail, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[59]);
      offset += formatterResolver.GetFormatterWithVerify<DamageTypes>().Serialize(ref bytes, offset, value.reaction_damage_type, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[60]);
      offset += formatterResolver.GetFormatterWithVerify<List<AttackDetailTypes>>().Serialize(ref bytes, offset, value.reaction_det_lists, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[61]);
      offset += formatterResolver.GetFormatterWithVerify<SkillRankUpValueShort>().Serialize(ref bytes, offset, value.control_damage_rate, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[62]);
      offset += formatterResolver.GetFormatterWithVerify<SkillRankUpValueShort>().Serialize(ref bytes, offset, value.control_damage_value, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[63]);
      offset += formatterResolver.GetFormatterWithVerify<SkillParamCalcTypes>().Serialize(ref bytes, offset, value.control_damage_calc, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[64]);
      offset += formatterResolver.GetFormatterWithVerify<SkillRankUpValueShort>().Serialize(ref bytes, offset, value.control_ct_rate, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[65]);
      offset += formatterResolver.GetFormatterWithVerify<SkillRankUpValueShort>().Serialize(ref bytes, offset, value.control_ct_value, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[66]);
      offset += formatterResolver.GetFormatterWithVerify<SkillParamCalcTypes>().Serialize(ref bytes, offset, value.control_ct_calc, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[67]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.target_buff_iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[68]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.target_cond_iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[69]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.self_buff_iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[70]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.self_cond_iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[71]);
      offset += formatterResolver.GetFormatterWithVerify<ShieldTypes>().Serialize(ref bytes, offset, value.shield_type, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[72]);
      offset += formatterResolver.GetFormatterWithVerify<DamageTypes>().Serialize(ref bytes, offset, value.shield_damage_type, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[73]);
      offset += formatterResolver.GetFormatterWithVerify<SkillRankUpValueShort>().Serialize(ref bytes, offset, value.shield_turn, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[74]);
      offset += formatterResolver.GetFormatterWithVerify<SkillRankUpValue>().Serialize(ref bytes, offset, value.shield_value, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[75]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.job, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[76]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ComboNum);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[77]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ComboDamageRate);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[78]);
      offset += formatterResolver.GetFormatterWithVerify<JewelDamageTypes>().Serialize(ref bytes, offset, value.JewelDamageType, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[79]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.JewelDamageValue);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[80]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.DuplicateCount);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[81]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.SceneName, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[82]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.SceneNameBigUnit, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[83]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.CollaboMainId, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[84]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.CollaboHeight);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[85]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.KnockBackRate);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[86]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.KnockBackVal);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[87]);
      offset += formatterResolver.GetFormatterWithVerify<eKnockBackDir>().Serialize(ref bytes, offset, value.KnockBackDir, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[88]);
      offset += formatterResolver.GetFormatterWithVerify<eKnockBackDs>().Serialize(ref bytes, offset, value.KnockBackDs, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[89]);
      offset += formatterResolver.GetFormatterWithVerify<eDamageDispType>().Serialize(ref bytes, offset, value.DamageDispType, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[90]);
      offset += formatterResolver.GetFormatterWithVerify<eTeleportType>().Serialize(ref bytes, offset, value.TeleportType, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[91]);
      offset += formatterResolver.GetFormatterWithVerify<ESkillTarget>().Serialize(ref bytes, offset, value.TeleportTarget, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[92]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.TeleportHeight);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[93]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.TeleportIsMove);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[94]);
      offset += formatterResolver.GetFormatterWithVerify<List<string>>().Serialize(ref bytes, offset, value.ReplaceTargetIdLists, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[95]);
      offset += formatterResolver.GetFormatterWithVerify<List<string>>().Serialize(ref bytes, offset, value.ReplaceChangeIdLists, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[96]);
      offset += formatterResolver.GetFormatterWithVerify<List<string>>().Serialize(ref bytes, offset, value.AbilityReplaceTargetIdLists, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[97]);
      offset += formatterResolver.GetFormatterWithVerify<List<string>>().Serialize(ref bytes, offset, value.AbilityReplaceChangeIdLists, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[98]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.CollaboVoiceId, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[99]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.CollaboVoicePlayDelayFrame);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[100]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.ReplacedTargetId, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[101]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.TrickId, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[102]);
      offset += formatterResolver.GetFormatterWithVerify<eTrickSetType>().Serialize(ref bytes, offset, value.TrickSetType, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[103]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.BreakObjId, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[104]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.MapEffectDesc, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[105]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.WeatherRate);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[106]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.WeatherId, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[107]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ElementSpcAtkRate);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[108]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.MaxDamageValue);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[109]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.CutInConceptCardId, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[110]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.JudgeHpVal);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[111]);
      offset += formatterResolver.GetFormatterWithVerify<SkillParamCalcTypes>().Serialize(ref bytes, offset, value.JudgeHpCalc, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[112]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.AcFromAbilId, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[113]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.AcToAbilId, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[114]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.AcTurn);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[115]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.EffectHitTargetNumRate);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[116]);
      offset += formatterResolver.GetFormatterWithVerify<eAbsorbAndGive>().Serialize(ref bytes, offset, value.AbsorbAndGive, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[117]);
      offset += formatterResolver.GetFormatterWithVerify<eSkillTargetEx>().Serialize(ref bytes, offset, value.TargetEx, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[118]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.JumpSpcAtkRate);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[119]);
      offset += formatterResolver.GetFormatterWithVerify<eTeleportSkillPos>().Serialize(ref bytes, offset, value.TeleportSkillPos, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[120]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.DynamicTransformUnitId, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[121]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.SkillMotionId, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[122]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.DependStateSpcEffId, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[123]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.DependStateSpcEffSelfId, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[124]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ForcedTargetingTurn);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[125]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.ProtectSkillId, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[126]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.SkillAdditionalId, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[(int) sbyte.MaxValue]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.SkillAntiShieldId, formatterResolver);
      return offset - num;
    }

    public SkillParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (SkillParam) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      ProtectSkillParam protectSkillParam = (ProtectSkillParam) null;
      SkillAdditionalParam skillAdditionalParam = (SkillAdditionalParam) null;
      SkillAntiShieldParam skillAntiShieldParam = (SkillAntiShieldParam) null;
      bool flag1 = false;
      bool flag2 = false;
      bool flag3 = false;
      bool flag4 = false;
      bool flag5 = false;
      bool flag6 = false;
      bool flag7 = false;
      bool flag8 = false;
      bool flag9 = false;
      bool flag10 = false;
      bool flag11 = false;
      string str1 = (string) null;
      string str2 = (string) null;
      string str3 = (string) null;
      string str4 = (string) null;
      string str5 = (string) null;
      string str6 = (string) null;
      string str7 = (string) null;
      string str8 = (string) null;
      int num3 = 0;
      ESkillType eskillType = ESkillType.Attack;
      ESkillTiming eskillTiming = ESkillTiming.Used;
      ESkillCondition eskillCondition = ESkillCondition.None;
      int num4 = 0;
      int num5 = 0;
      int num6 = 0;
      int num7 = 0;
      int num8 = 0;
      int num9 = 0;
      int num10 = 0;
      ELineType elineType = ELineType.None;
      ESelectType eselectType1 = ESelectType.Cross;
      int num11 = 0;
      int num12 = 0;
      ESelectType eselectType2 = ESelectType.Cross;
      int num13 = 0;
      int num14 = 0;
      int num15 = 0;
      int num16 = 0;
      int num17 = 0;
      ECastTypes ecastTypes = ECastTypes.Chant;
      SkillRankUpValueShort rankUpValueShort1 = (SkillRankUpValueShort) null;
      ESkillTarget eskillTarget1 = ESkillTarget.Self;
      SkillEffectTypes skillEffectTypes = SkillEffectTypes.None;
      SkillRankUpValueShort rankUpValueShort2 = (SkillRankUpValueShort) null;
      SkillRankUpValue skillRankUpValue1 = (SkillRankUpValue) null;
      SkillRankUpValueShort rankUpValueShort3 = (SkillRankUpValueShort) null;
      SkillParamCalcTypes skillParamCalcTypes1 = SkillParamCalcTypes.Add;
      int num18 = 0;
      int num19 = 0;
      int num20 = 0;
      int num21 = 0;
      int num22 = 0;
      EElement eelement = EElement.None;
      AttackTypes attackTypes = AttackTypes.None;
      AttackDetailTypes attackDetailTypes = AttackDetailTypes.None;
      DamageTypes damageTypes1 = DamageTypes.None;
      List<AttackDetailTypes> attackDetailTypesList = (List<AttackDetailTypes>) null;
      SkillRankUpValueShort rankUpValueShort4 = (SkillRankUpValueShort) null;
      SkillRankUpValueShort rankUpValueShort5 = (SkillRankUpValueShort) null;
      SkillParamCalcTypes skillParamCalcTypes2 = SkillParamCalcTypes.Add;
      SkillRankUpValueShort rankUpValueShort6 = (SkillRankUpValueShort) null;
      SkillRankUpValueShort rankUpValueShort7 = (SkillRankUpValueShort) null;
      SkillParamCalcTypes skillParamCalcTypes3 = SkillParamCalcTypes.Add;
      string str9 = (string) null;
      string str10 = (string) null;
      string str11 = (string) null;
      string str12 = (string) null;
      ShieldTypes shieldTypes = ShieldTypes.None;
      DamageTypes damageTypes2 = DamageTypes.None;
      SkillRankUpValueShort rankUpValueShort8 = (SkillRankUpValueShort) null;
      SkillRankUpValue skillRankUpValue2 = (SkillRankUpValue) null;
      string str13 = (string) null;
      int num23 = 0;
      int num24 = 0;
      JewelDamageTypes jewelDamageTypes = JewelDamageTypes.None;
      int num25 = 0;
      int num26 = 0;
      string str14 = (string) null;
      string str15 = (string) null;
      string str16 = (string) null;
      int num27 = 0;
      int num28 = 0;
      int num29 = 0;
      eKnockBackDir eKnockBackDir = eKnockBackDir.Back;
      eKnockBackDs eKnockBackDs = eKnockBackDs.Target;
      eDamageDispType eDamageDispType = eDamageDispType.Standard;
      eTeleportType eTeleportType = eTeleportType.None;
      ESkillTarget eskillTarget2 = ESkillTarget.Self;
      int num30 = 0;
      bool flag12 = false;
      List<string> stringList1 = (List<string>) null;
      List<string> stringList2 = (List<string>) null;
      List<string> stringList3 = (List<string>) null;
      List<string> stringList4 = (List<string>) null;
      string str17 = (string) null;
      int num31 = 0;
      string str18 = (string) null;
      string str19 = (string) null;
      eTrickSetType eTrickSetType = eTrickSetType.GridNoUnit;
      string str20 = (string) null;
      string str21 = (string) null;
      int num32 = 0;
      string str22 = (string) null;
      int num33 = 0;
      int num34 = 0;
      string str23 = (string) null;
      int num35 = 0;
      SkillParamCalcTypes skillParamCalcTypes4 = SkillParamCalcTypes.Add;
      string str24 = (string) null;
      string str25 = (string) null;
      int num36 = 0;
      int num37 = 0;
      eAbsorbAndGive eAbsorbAndGive = eAbsorbAndGive.None;
      eSkillTargetEx eSkillTargetEx = eSkillTargetEx.None;
      int num38 = 0;
      eTeleportSkillPos teleportSkillPos = eTeleportSkillPos.None;
      string str26 = (string) null;
      string str27 = (string) null;
      string str28 = (string) null;
      string str29 = (string) null;
      int num39 = 0;
      string str30 = (string) null;
      string str31 = (string) null;
      string str32 = (string) null;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num40;
        if (!this.____keyMapping.TryGetValueSafe(key, out num40))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num40)
          {
            case 0:
              protectSkillParam = formatterResolver.GetFormatterWithVerify<ProtectSkillParam>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              skillAdditionalParam = formatterResolver.GetFormatterWithVerify<SkillAdditionalParam>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              skillAntiShieldParam = formatterResolver.GetFormatterWithVerify<SkillAntiShieldParam>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              flag1 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 4:
              flag2 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 5:
              flag3 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 6:
              flag4 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 7:
              flag5 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 8:
              flag6 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 9:
              flag7 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 10:
              flag8 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 11:
              flag9 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 12:
              flag10 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 13:
              flag11 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 14:
              str1 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 15:
              str2 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 16:
              str3 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 17:
              str4 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 18:
              str5 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 19:
              str6 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 20:
              str7 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 21:
              str8 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 22:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 23:
              eskillType = formatterResolver.GetFormatterWithVerify<ESkillType>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 24:
              eskillTiming = formatterResolver.GetFormatterWithVerify<ESkillTiming>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 25:
              eskillCondition = formatterResolver.GetFormatterWithVerify<ESkillCondition>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 26:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 27:
              num5 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 28:
              num6 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 29:
              num7 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 30:
              num8 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 31:
              num9 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 32:
              num10 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 33:
              elineType = formatterResolver.GetFormatterWithVerify<ELineType>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 34:
              eselectType1 = formatterResolver.GetFormatterWithVerify<ESelectType>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 35:
              num11 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 36:
              num12 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 37:
              eselectType2 = formatterResolver.GetFormatterWithVerify<ESelectType>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 38:
              num13 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 39:
              num14 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 40:
              num15 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 41:
              num16 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 42:
              num17 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 43:
              ecastTypes = formatterResolver.GetFormatterWithVerify<ECastTypes>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 44:
              rankUpValueShort1 = formatterResolver.GetFormatterWithVerify<SkillRankUpValueShort>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 45:
              eskillTarget1 = formatterResolver.GetFormatterWithVerify<ESkillTarget>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 46:
              skillEffectTypes = formatterResolver.GetFormatterWithVerify<SkillEffectTypes>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 47:
              rankUpValueShort2 = formatterResolver.GetFormatterWithVerify<SkillRankUpValueShort>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 48:
              skillRankUpValue1 = formatterResolver.GetFormatterWithVerify<SkillRankUpValue>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 49:
              rankUpValueShort3 = formatterResolver.GetFormatterWithVerify<SkillRankUpValueShort>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 50:
              skillParamCalcTypes1 = formatterResolver.GetFormatterWithVerify<SkillParamCalcTypes>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 51:
              num18 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 52:
              num19 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 53:
              num20 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 54:
              num21 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 55:
              num22 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 56:
              eelement = formatterResolver.GetFormatterWithVerify<EElement>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 57:
              attackTypes = formatterResolver.GetFormatterWithVerify<AttackTypes>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 58:
              attackDetailTypes = formatterResolver.GetFormatterWithVerify<AttackDetailTypes>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 59:
              damageTypes1 = formatterResolver.GetFormatterWithVerify<DamageTypes>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 60:
              attackDetailTypesList = formatterResolver.GetFormatterWithVerify<List<AttackDetailTypes>>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 61:
              rankUpValueShort4 = formatterResolver.GetFormatterWithVerify<SkillRankUpValueShort>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 62:
              rankUpValueShort5 = formatterResolver.GetFormatterWithVerify<SkillRankUpValueShort>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 63:
              skillParamCalcTypes2 = formatterResolver.GetFormatterWithVerify<SkillParamCalcTypes>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 64:
              rankUpValueShort6 = formatterResolver.GetFormatterWithVerify<SkillRankUpValueShort>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 65:
              rankUpValueShort7 = formatterResolver.GetFormatterWithVerify<SkillRankUpValueShort>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 66:
              skillParamCalcTypes3 = formatterResolver.GetFormatterWithVerify<SkillParamCalcTypes>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 67:
              str9 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 68:
              str10 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 69:
              str11 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 70:
              str12 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 71:
              shieldTypes = formatterResolver.GetFormatterWithVerify<ShieldTypes>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 72:
              damageTypes2 = formatterResolver.GetFormatterWithVerify<DamageTypes>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 73:
              rankUpValueShort8 = formatterResolver.GetFormatterWithVerify<SkillRankUpValueShort>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 74:
              skillRankUpValue2 = formatterResolver.GetFormatterWithVerify<SkillRankUpValue>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 75:
              str13 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 76:
              num23 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 77:
              num24 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 78:
              jewelDamageTypes = formatterResolver.GetFormatterWithVerify<JewelDamageTypes>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 79:
              num25 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 80:
              num26 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 81:
              str14 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 82:
              str15 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 83:
              str16 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 84:
              num27 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 85:
              num28 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 86:
              num29 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 87:
              eKnockBackDir = formatterResolver.GetFormatterWithVerify<eKnockBackDir>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 88:
              eKnockBackDs = formatterResolver.GetFormatterWithVerify<eKnockBackDs>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 89:
              eDamageDispType = formatterResolver.GetFormatterWithVerify<eDamageDispType>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 90:
              eTeleportType = formatterResolver.GetFormatterWithVerify<eTeleportType>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 91:
              eskillTarget2 = formatterResolver.GetFormatterWithVerify<ESkillTarget>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 92:
              num30 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 93:
              flag12 = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 94:
              stringList1 = formatterResolver.GetFormatterWithVerify<List<string>>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 95:
              stringList2 = formatterResolver.GetFormatterWithVerify<List<string>>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 96:
              stringList3 = formatterResolver.GetFormatterWithVerify<List<string>>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 97:
              stringList4 = formatterResolver.GetFormatterWithVerify<List<string>>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 98:
              str17 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 99:
              num31 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 100:
              str18 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 101:
              str19 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 102:
              eTrickSetType = formatterResolver.GetFormatterWithVerify<eTrickSetType>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 103:
              str20 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 104:
              str21 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 105:
              num32 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 106:
              str22 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 107:
              num33 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 108:
              num34 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 109:
              str23 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 110:
              num35 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 111:
              skillParamCalcTypes4 = formatterResolver.GetFormatterWithVerify<SkillParamCalcTypes>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 112:
              str24 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 113:
              str25 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 114:
              num36 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 115:
              num37 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 116:
              eAbsorbAndGive = formatterResolver.GetFormatterWithVerify<eAbsorbAndGive>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 117:
              eSkillTargetEx = formatterResolver.GetFormatterWithVerify<eSkillTargetEx>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 118:
              num38 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 119:
              teleportSkillPos = formatterResolver.GetFormatterWithVerify<eTeleportSkillPos>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 120:
              str26 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 121:
              str27 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 122:
              str28 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 123:
              str29 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 124:
              num39 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 125:
              str30 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 126:
              str31 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case (int) sbyte.MaxValue:
              str32 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new SkillParam()
      {
        iname = str1,
        name = str2,
        expr = str3,
        motion = str4,
        effect = str5,
        defend_effect = str6,
        weapon = str7,
        tokkou = str8,
        tk_rate = num3,
        type = eskillType,
        timing = eskillTiming,
        condition = eskillCondition,
        lvcap = num4,
        cost = num5,
        count = num6,
        rate = num7,
        back_defrate = num8,
        side_defrate = num9,
        ignore_defense_rate = num10,
        line_type = elineType,
        select_range = eselectType1,
        range_min = num11,
        range_max = num12,
        select_scope = eselectType2,
        scope = num13,
        effect_height = num14,
        hp_cost_rate = num15,
        hp_cost = num16,
        random_hit_rate = num17,
        cast_type = ecastTypes,
        cast_speed = rankUpValueShort1,
        target = eskillTarget1,
        effect_type = skillEffectTypes,
        effect_rate = rankUpValueShort2,
        effect_value = skillRankUpValue1,
        effect_range = rankUpValueShort3,
        effect_calc = skillParamCalcTypes1,
        effect_hprate = num18,
        effect_mprate = num19,
        effect_dead_rate = num20,
        effect_lvrate = num21,
        absorb_damage_rate = num22,
        element_type = eelement,
        attack_type = attackTypes,
        attack_detail = attackDetailTypes,
        reaction_damage_type = damageTypes1,
        reaction_det_lists = attackDetailTypesList,
        control_damage_rate = rankUpValueShort4,
        control_damage_value = rankUpValueShort5,
        control_damage_calc = skillParamCalcTypes2,
        control_ct_rate = rankUpValueShort6,
        control_ct_value = rankUpValueShort7,
        control_ct_calc = skillParamCalcTypes3,
        target_buff_iname = str9,
        target_cond_iname = str10,
        self_buff_iname = str11,
        self_cond_iname = str12,
        shield_type = shieldTypes,
        shield_damage_type = damageTypes2,
        shield_turn = rankUpValueShort8,
        shield_value = skillRankUpValue2,
        job = str13,
        ComboNum = num23,
        ComboDamageRate = num24,
        JewelDamageType = jewelDamageTypes,
        JewelDamageValue = num25,
        DuplicateCount = num26,
        SceneName = str14,
        SceneNameBigUnit = str15,
        CollaboMainId = str16,
        CollaboHeight = num27,
        KnockBackRate = num28,
        KnockBackVal = num29,
        KnockBackDir = eKnockBackDir,
        KnockBackDs = eKnockBackDs,
        DamageDispType = eDamageDispType,
        TeleportType = eTeleportType,
        TeleportTarget = eskillTarget2,
        TeleportHeight = num30,
        TeleportIsMove = flag12,
        ReplaceTargetIdLists = stringList1,
        ReplaceChangeIdLists = stringList2,
        AbilityReplaceTargetIdLists = stringList3,
        AbilityReplaceChangeIdLists = stringList4,
        CollaboVoiceId = str17,
        CollaboVoicePlayDelayFrame = num31,
        ReplacedTargetId = str18,
        TrickId = str19,
        TrickSetType = eTrickSetType,
        BreakObjId = str20,
        MapEffectDesc = str21,
        WeatherRate = num32,
        WeatherId = str22,
        ElementSpcAtkRate = num33,
        MaxDamageValue = num34,
        CutInConceptCardId = str23,
        JudgeHpVal = num35,
        JudgeHpCalc = skillParamCalcTypes4,
        AcFromAbilId = str24,
        AcToAbilId = str25,
        AcTurn = num36,
        EffectHitTargetNumRate = num37,
        AbsorbAndGive = eAbsorbAndGive,
        TargetEx = eSkillTargetEx,
        JumpSpcAtkRate = num38,
        TeleportSkillPos = teleportSkillPos,
        DynamicTransformUnitId = str26,
        SkillMotionId = str27,
        DependStateSpcEffId = str28,
        DependStateSpcEffSelfId = str29,
        ForcedTargetingTurn = num39,
        ProtectSkillId = str30,
        SkillAdditionalId = str31,
        SkillAntiShieldId = str32
      };
    }
  }
}
