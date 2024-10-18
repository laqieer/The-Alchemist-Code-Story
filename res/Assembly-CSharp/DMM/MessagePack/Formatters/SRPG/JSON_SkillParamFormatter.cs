// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.JSON_SkillParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class JSON_SkillParamFormatter : 
    IMessagePackFormatter<JSON_SkillParam>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public JSON_SkillParamFormatter()
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
          "motnm",
          3
        },
        {
          "effnm",
          4
        },
        {
          "effdef",
          5
        },
        {
          "weapon",
          6
        },
        {
          "tktag",
          7
        },
        {
          "tkrate",
          8
        },
        {
          "cutin",
          9
        },
        {
          "isbtl",
          10
        },
        {
          "cost",
          11
        },
        {
          "count",
          12
        },
        {
          "cap",
          13
        },
        {
          "rate",
          14
        },
        {
          "bdb",
          15
        },
        {
          "sdb",
          16
        },
        {
          "idr",
          17
        },
        {
          "line",
          18
        },
        {
          "sran",
          19
        },
        {
          "rangemin",
          20
        },
        {
          "range",
          21
        },
        {
          "ssco",
          22
        },
        {
          "scope",
          23
        },
        {
          "eff_h",
          24
        },
        {
          "chran",
          25
        },
        {
          "pierce",
          26
        },
        {
          "sonoba",
          27
        },
        {
          "hbonus",
          28
        },
        {
          "ctbreak",
          29
        },
        {
          "mpatk",
          30
        },
        {
          "fhit",
          31
        },
        {
          "suicide",
          32
        },
        {
          "rhit",
          33
        },
        {
          "hp_cost_rate",
          34
        },
        {
          "hp_cost",
          35
        },
        {
          "ct_type",
          36
        },
        {
          "ct_spd_ini",
          37
        },
        {
          "ct_spd_max",
          38
        },
        {
          "utgt",
          39
        },
        {
          "type",
          40
        },
        {
          "timing",
          41
        },
        {
          "cond",
          42
        },
        {
          "target",
          43
        },
        {
          "eff_type",
          44
        },
        {
          "atk_type",
          45
        },
        {
          "atk_det",
          46
        },
        {
          "ehpa",
          47
        },
        {
          "elem",
          48
        },
        {
          "elem_ini",
          49
        },
        {
          "elem_max",
          50
        },
        {
          "eff_rate_ini",
          51
        },
        {
          "eff_rate_max",
          52
        },
        {
          "eff_val_ini",
          53
        },
        {
          "eff_val_max",
          54
        },
        {
          "eff_calc",
          55
        },
        {
          "eff_range_ini",
          56
        },
        {
          "eff_range_max",
          57
        },
        {
          "eff_hprate",
          58
        },
        {
          "eff_mprate",
          59
        },
        {
          "eff_durate",
          60
        },
        {
          "eff_lvrate",
          61
        },
        {
          "abs_d_rate",
          62
        },
        {
          "react_d_type",
          63
        },
        {
          "react_dets",
          64
        },
        {
          "ctrl_d_rate_ini",
          65
        },
        {
          "ctrl_d_rate_max",
          66
        },
        {
          "ctrl_d_ini",
          67
        },
        {
          "ctrl_d_max",
          68
        },
        {
          "ctrl_d_calc",
          69
        },
        {
          "ct_rate_ini",
          70
        },
        {
          "ct_rate_max",
          71
        },
        {
          "ct_val_ini",
          72
        },
        {
          "ct_val_max",
          73
        },
        {
          "ct_calc",
          74
        },
        {
          "t_buff",
          75
        },
        {
          "t_cond",
          76
        },
        {
          "s_buff",
          77
        },
        {
          "s_cond",
          78
        },
        {
          "shield_type",
          79
        },
        {
          "shield_d_type",
          80
        },
        {
          "shield_turn_ini",
          81
        },
        {
          "shield_turn_max",
          82
        },
        {
          "shield_ini",
          83
        },
        {
          "shield_max",
          84
        },
        {
          "shield_reset",
          85
        },
        {
          "job",
          86
        },
        {
          "combo_num",
          87
        },
        {
          "combo_rate",
          88
        },
        {
          "is_cri",
          89
        },
        {
          "jdtype",
          90
        },
        {
          "jdv",
          91
        },
        {
          "jdabs",
          92
        },
        {
          "dupli",
          93
        },
        {
          "scn",
          94
        },
        {
          "scn_bu",
          95
        },
        {
          "cs_main_id",
          96
        },
        {
          "cs_height",
          97
        },
        {
          "kb_rate",
          98
        },
        {
          "kb_val",
          99
        },
        {
          "kb_dir",
          100
        },
        {
          "kb_ds",
          101
        },
        {
          "dmg_dt",
          102
        },
        {
          "rp_tgt_ids",
          103
        },
        {
          "rp_chg_ids",
          104
        },
        {
          "ab_rp_tgt_ids",
          105
        },
        {
          "ab_rp_chg_ids",
          106
        },
        {
          "cs_voice",
          107
        },
        {
          "cs_vp_df",
          108
        },
        {
          "tl_type",
          109
        },
        {
          "tl_target",
          110
        },
        {
          "tl_height",
          111
        },
        {
          "tl_is_mov",
          112
        },
        {
          "sub_actuate",
          113
        },
        {
          "is_fixed",
          114
        },
        {
          "tr_id",
          115
        },
        {
          "tr_set",
          116
        },
        {
          "f_ulock",
          117
        },
        {
          "ad_react",
          118
        },
        {
          "ig_elem",
          119
        },
        {
          "bo_id",
          120
        },
        {
          "me_desc",
          121
        },
        {
          "wth_rate",
          122
        },
        {
          "wth_id",
          123
        },
        {
          "elem_tk",
          124
        },
        {
          "is_pre_apply",
          125
        },
        {
          "max_dmg",
          126
        },
        {
          "ci_cc_id",
          (int) sbyte.MaxValue
        },
        {
          "jhp_val",
          128
        },
        {
          "jhp_calc",
          129
        },
        {
          "jhp_over",
          130
        },
        {
          "is_mhm_dmg",
          131
        },
        {
          "ac_fr_ab_id",
          132
        },
        {
          "ac_to_ab_id",
          133
        },
        {
          "ac_turn",
          134
        },
        {
          "ac_is_self",
          135
        },
        {
          "ac_is_reset",
          136
        },
        {
          "eff_htnrate",
          137
        },
        {
          "is_htndiv",
          138
        },
        {
          "aag",
          139
        },
        {
          "target_ex",
          140
        },
        {
          "jmp_tk",
          141
        },
        {
          "is_no_ccc",
          142
        },
        {
          "jmpbreak",
          143
        },
        {
          "tsk_pos",
          144
        },
        {
          "dtu_id",
          145
        },
        {
          "sm_id",
          146
        },
        {
          "is_ob_react",
          147
        },
        {
          "dsse_id",
          148
        },
        {
          "dsse_self_id",
          149
        },
        {
          "is_no_ujb",
          150
        },
        {
          "is_ai_noautotiming",
          151
        },
        {
          "is_mp_use_after",
          152
        },
        {
          "ft_turn",
          153
        },
        {
          "protect_iname",
          154
        },
        {
          "protect_ignore",
          155
        },
        {
          "sa_iname",
          156
        },
        {
          "sas_iname",
          157
        }
      };
      this.____stringByteKeys = new byte[158][]
      {
        MessagePackBinary.GetEncodedStringBytes("iname"),
        MessagePackBinary.GetEncodedStringBytes("name"),
        MessagePackBinary.GetEncodedStringBytes("expr"),
        MessagePackBinary.GetEncodedStringBytes("motnm"),
        MessagePackBinary.GetEncodedStringBytes("effnm"),
        MessagePackBinary.GetEncodedStringBytes("effdef"),
        MessagePackBinary.GetEncodedStringBytes("weapon"),
        MessagePackBinary.GetEncodedStringBytes("tktag"),
        MessagePackBinary.GetEncodedStringBytes("tkrate"),
        MessagePackBinary.GetEncodedStringBytes("cutin"),
        MessagePackBinary.GetEncodedStringBytes("isbtl"),
        MessagePackBinary.GetEncodedStringBytes("cost"),
        MessagePackBinary.GetEncodedStringBytes("count"),
        MessagePackBinary.GetEncodedStringBytes("cap"),
        MessagePackBinary.GetEncodedStringBytes("rate"),
        MessagePackBinary.GetEncodedStringBytes("bdb"),
        MessagePackBinary.GetEncodedStringBytes("sdb"),
        MessagePackBinary.GetEncodedStringBytes("idr"),
        MessagePackBinary.GetEncodedStringBytes("line"),
        MessagePackBinary.GetEncodedStringBytes("sran"),
        MessagePackBinary.GetEncodedStringBytes("rangemin"),
        MessagePackBinary.GetEncodedStringBytes("range"),
        MessagePackBinary.GetEncodedStringBytes("ssco"),
        MessagePackBinary.GetEncodedStringBytes("scope"),
        MessagePackBinary.GetEncodedStringBytes("eff_h"),
        MessagePackBinary.GetEncodedStringBytes("chran"),
        MessagePackBinary.GetEncodedStringBytes("pierce"),
        MessagePackBinary.GetEncodedStringBytes("sonoba"),
        MessagePackBinary.GetEncodedStringBytes("hbonus"),
        MessagePackBinary.GetEncodedStringBytes("ctbreak"),
        MessagePackBinary.GetEncodedStringBytes("mpatk"),
        MessagePackBinary.GetEncodedStringBytes("fhit"),
        MessagePackBinary.GetEncodedStringBytes("suicide"),
        MessagePackBinary.GetEncodedStringBytes("rhit"),
        MessagePackBinary.GetEncodedStringBytes("hp_cost_rate"),
        MessagePackBinary.GetEncodedStringBytes("hp_cost"),
        MessagePackBinary.GetEncodedStringBytes("ct_type"),
        MessagePackBinary.GetEncodedStringBytes("ct_spd_ini"),
        MessagePackBinary.GetEncodedStringBytes("ct_spd_max"),
        MessagePackBinary.GetEncodedStringBytes("utgt"),
        MessagePackBinary.GetEncodedStringBytes("type"),
        MessagePackBinary.GetEncodedStringBytes("timing"),
        MessagePackBinary.GetEncodedStringBytes("cond"),
        MessagePackBinary.GetEncodedStringBytes("target"),
        MessagePackBinary.GetEncodedStringBytes("eff_type"),
        MessagePackBinary.GetEncodedStringBytes("atk_type"),
        MessagePackBinary.GetEncodedStringBytes("atk_det"),
        MessagePackBinary.GetEncodedStringBytes("ehpa"),
        MessagePackBinary.GetEncodedStringBytes("elem"),
        MessagePackBinary.GetEncodedStringBytes("elem_ini"),
        MessagePackBinary.GetEncodedStringBytes("elem_max"),
        MessagePackBinary.GetEncodedStringBytes("eff_rate_ini"),
        MessagePackBinary.GetEncodedStringBytes("eff_rate_max"),
        MessagePackBinary.GetEncodedStringBytes("eff_val_ini"),
        MessagePackBinary.GetEncodedStringBytes("eff_val_max"),
        MessagePackBinary.GetEncodedStringBytes("eff_calc"),
        MessagePackBinary.GetEncodedStringBytes("eff_range_ini"),
        MessagePackBinary.GetEncodedStringBytes("eff_range_max"),
        MessagePackBinary.GetEncodedStringBytes("eff_hprate"),
        MessagePackBinary.GetEncodedStringBytes("eff_mprate"),
        MessagePackBinary.GetEncodedStringBytes("eff_durate"),
        MessagePackBinary.GetEncodedStringBytes("eff_lvrate"),
        MessagePackBinary.GetEncodedStringBytes("abs_d_rate"),
        MessagePackBinary.GetEncodedStringBytes("react_d_type"),
        MessagePackBinary.GetEncodedStringBytes("react_dets"),
        MessagePackBinary.GetEncodedStringBytes("ctrl_d_rate_ini"),
        MessagePackBinary.GetEncodedStringBytes("ctrl_d_rate_max"),
        MessagePackBinary.GetEncodedStringBytes("ctrl_d_ini"),
        MessagePackBinary.GetEncodedStringBytes("ctrl_d_max"),
        MessagePackBinary.GetEncodedStringBytes("ctrl_d_calc"),
        MessagePackBinary.GetEncodedStringBytes("ct_rate_ini"),
        MessagePackBinary.GetEncodedStringBytes("ct_rate_max"),
        MessagePackBinary.GetEncodedStringBytes("ct_val_ini"),
        MessagePackBinary.GetEncodedStringBytes("ct_val_max"),
        MessagePackBinary.GetEncodedStringBytes("ct_calc"),
        MessagePackBinary.GetEncodedStringBytes("t_buff"),
        MessagePackBinary.GetEncodedStringBytes("t_cond"),
        MessagePackBinary.GetEncodedStringBytes("s_buff"),
        MessagePackBinary.GetEncodedStringBytes("s_cond"),
        MessagePackBinary.GetEncodedStringBytes("shield_type"),
        MessagePackBinary.GetEncodedStringBytes("shield_d_type"),
        MessagePackBinary.GetEncodedStringBytes("shield_turn_ini"),
        MessagePackBinary.GetEncodedStringBytes("shield_turn_max"),
        MessagePackBinary.GetEncodedStringBytes("shield_ini"),
        MessagePackBinary.GetEncodedStringBytes("shield_max"),
        MessagePackBinary.GetEncodedStringBytes("shield_reset"),
        MessagePackBinary.GetEncodedStringBytes("job"),
        MessagePackBinary.GetEncodedStringBytes("combo_num"),
        MessagePackBinary.GetEncodedStringBytes("combo_rate"),
        MessagePackBinary.GetEncodedStringBytes("is_cri"),
        MessagePackBinary.GetEncodedStringBytes("jdtype"),
        MessagePackBinary.GetEncodedStringBytes("jdv"),
        MessagePackBinary.GetEncodedStringBytes("jdabs"),
        MessagePackBinary.GetEncodedStringBytes("dupli"),
        MessagePackBinary.GetEncodedStringBytes("scn"),
        MessagePackBinary.GetEncodedStringBytes("scn_bu"),
        MessagePackBinary.GetEncodedStringBytes("cs_main_id"),
        MessagePackBinary.GetEncodedStringBytes("cs_height"),
        MessagePackBinary.GetEncodedStringBytes("kb_rate"),
        MessagePackBinary.GetEncodedStringBytes("kb_val"),
        MessagePackBinary.GetEncodedStringBytes("kb_dir"),
        MessagePackBinary.GetEncodedStringBytes("kb_ds"),
        MessagePackBinary.GetEncodedStringBytes("dmg_dt"),
        MessagePackBinary.GetEncodedStringBytes("rp_tgt_ids"),
        MessagePackBinary.GetEncodedStringBytes("rp_chg_ids"),
        MessagePackBinary.GetEncodedStringBytes("ab_rp_tgt_ids"),
        MessagePackBinary.GetEncodedStringBytes("ab_rp_chg_ids"),
        MessagePackBinary.GetEncodedStringBytes("cs_voice"),
        MessagePackBinary.GetEncodedStringBytes("cs_vp_df"),
        MessagePackBinary.GetEncodedStringBytes("tl_type"),
        MessagePackBinary.GetEncodedStringBytes("tl_target"),
        MessagePackBinary.GetEncodedStringBytes("tl_height"),
        MessagePackBinary.GetEncodedStringBytes("tl_is_mov"),
        MessagePackBinary.GetEncodedStringBytes("sub_actuate"),
        MessagePackBinary.GetEncodedStringBytes("is_fixed"),
        MessagePackBinary.GetEncodedStringBytes("tr_id"),
        MessagePackBinary.GetEncodedStringBytes("tr_set"),
        MessagePackBinary.GetEncodedStringBytes("f_ulock"),
        MessagePackBinary.GetEncodedStringBytes("ad_react"),
        MessagePackBinary.GetEncodedStringBytes("ig_elem"),
        MessagePackBinary.GetEncodedStringBytes("bo_id"),
        MessagePackBinary.GetEncodedStringBytes("me_desc"),
        MessagePackBinary.GetEncodedStringBytes("wth_rate"),
        MessagePackBinary.GetEncodedStringBytes("wth_id"),
        MessagePackBinary.GetEncodedStringBytes("elem_tk"),
        MessagePackBinary.GetEncodedStringBytes("is_pre_apply"),
        MessagePackBinary.GetEncodedStringBytes("max_dmg"),
        MessagePackBinary.GetEncodedStringBytes("ci_cc_id"),
        MessagePackBinary.GetEncodedStringBytes("jhp_val"),
        MessagePackBinary.GetEncodedStringBytes("jhp_calc"),
        MessagePackBinary.GetEncodedStringBytes("jhp_over"),
        MessagePackBinary.GetEncodedStringBytes("is_mhm_dmg"),
        MessagePackBinary.GetEncodedStringBytes("ac_fr_ab_id"),
        MessagePackBinary.GetEncodedStringBytes("ac_to_ab_id"),
        MessagePackBinary.GetEncodedStringBytes("ac_turn"),
        MessagePackBinary.GetEncodedStringBytes("ac_is_self"),
        MessagePackBinary.GetEncodedStringBytes("ac_is_reset"),
        MessagePackBinary.GetEncodedStringBytes("eff_htnrate"),
        MessagePackBinary.GetEncodedStringBytes("is_htndiv"),
        MessagePackBinary.GetEncodedStringBytes("aag"),
        MessagePackBinary.GetEncodedStringBytes("target_ex"),
        MessagePackBinary.GetEncodedStringBytes("jmp_tk"),
        MessagePackBinary.GetEncodedStringBytes("is_no_ccc"),
        MessagePackBinary.GetEncodedStringBytes("jmpbreak"),
        MessagePackBinary.GetEncodedStringBytes("tsk_pos"),
        MessagePackBinary.GetEncodedStringBytes("dtu_id"),
        MessagePackBinary.GetEncodedStringBytes("sm_id"),
        MessagePackBinary.GetEncodedStringBytes("is_ob_react"),
        MessagePackBinary.GetEncodedStringBytes("dsse_id"),
        MessagePackBinary.GetEncodedStringBytes("dsse_self_id"),
        MessagePackBinary.GetEncodedStringBytes("is_no_ujb"),
        MessagePackBinary.GetEncodedStringBytes("is_ai_noautotiming"),
        MessagePackBinary.GetEncodedStringBytes("is_mp_use_after"),
        MessagePackBinary.GetEncodedStringBytes("ft_turn"),
        MessagePackBinary.GetEncodedStringBytes("protect_iname"),
        MessagePackBinary.GetEncodedStringBytes("protect_ignore"),
        MessagePackBinary.GetEncodedStringBytes("sa_iname"),
        MessagePackBinary.GetEncodedStringBytes("sas_iname")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      JSON_SkillParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, 158);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.name, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.expr, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.motnm, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.effnm, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.effdef, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.weapon, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.tktag, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.tkrate);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.cutin);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.isbtl);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.cost);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.count);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.cap);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rate);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.bdb);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[16]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.sdb);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[17]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.idr);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[18]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.line);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[19]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.sran);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[20]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rangemin);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[21]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.range);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[22]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ssco);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[23]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.scope);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[24]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.eff_h);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[25]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.chran);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[26]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.pierce);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[27]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.sonoba);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[28]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.hbonus);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[29]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ctbreak);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[30]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mpatk);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[31]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.fhit);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[32]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.suicide);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[33]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rhit);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[34]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.hp_cost_rate);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[35]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.hp_cost);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[36]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ct_type);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[37]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.ct_spd_ini);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[38]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.ct_spd_max);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[39]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.utgt);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[40]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.type);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[41]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.timing);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[42]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.cond);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[43]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.target);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[44]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.eff_type);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[45]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.atk_type);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[46]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.atk_det);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[47]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ehpa);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[48]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.elem);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[49]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.elem_ini);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[50]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.elem_max);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[51]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.eff_rate_ini);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[52]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.eff_rate_max);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[53]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.eff_val_ini);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[54]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.eff_val_max);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[55]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.eff_calc);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[56]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.eff_range_ini);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[57]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.eff_range_max);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[58]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.eff_hprate);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[59]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.eff_mprate);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[60]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.eff_durate);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[61]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.eff_lvrate);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[62]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.abs_d_rate);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[63]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.react_d_type);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[64]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.react_dets, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[65]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.ctrl_d_rate_ini);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[66]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.ctrl_d_rate_max);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[67]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.ctrl_d_ini);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[68]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.ctrl_d_max);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[69]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ctrl_d_calc);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[70]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.ct_rate_ini);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[71]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.ct_rate_max);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[72]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.ct_val_ini);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[73]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.ct_val_max);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[74]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ct_calc);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[75]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.t_buff, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[76]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.t_cond, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[77]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.s_buff, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[78]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.s_cond, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[79]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.shield_type);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[80]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.shield_d_type);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[81]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.shield_turn_ini);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[82]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.shield_turn_max);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[83]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.shield_ini);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[84]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.shield_max);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[85]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.shield_reset);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[86]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.job, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[87]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.combo_num);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[88]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.combo_rate);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[89]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.is_cri);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[90]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.jdtype);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[91]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.jdv);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[92]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.jdabs);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[93]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.dupli);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[94]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.scn, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[95]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.scn_bu, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[96]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.cs_main_id, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[97]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.cs_height);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[98]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.kb_rate);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[99]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.kb_val);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[100]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.kb_dir);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[101]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.kb_ds);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[102]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.dmg_dt);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[103]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.rp_tgt_ids, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[104]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.rp_chg_ids, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[105]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.ab_rp_tgt_ids, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[106]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.ab_rp_chg_ids, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[107]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.cs_voice, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[108]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.cs_vp_df);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[109]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.tl_type);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[110]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.tl_target);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[111]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.tl_height);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[112]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.tl_is_mov);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[113]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.sub_actuate);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[114]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.is_fixed);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[115]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.tr_id, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[116]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.tr_set);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[117]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.f_ulock);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[118]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ad_react);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[119]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ig_elem);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[120]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.bo_id, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[121]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.me_desc, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[122]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.wth_rate);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[123]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.wth_id, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[124]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.elem_tk);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[125]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.is_pre_apply);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[126]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.max_dmg);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[(int) sbyte.MaxValue]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.ci_cc_id, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[128]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.jhp_val);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[129]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.jhp_calc);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[130]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.jhp_over);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[131]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.is_mhm_dmg);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[132]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.ac_fr_ab_id, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[133]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.ac_to_ab_id, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[134]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ac_turn);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[135]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ac_is_self);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[136]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ac_is_reset);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[137]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.eff_htnrate);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[138]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.is_htndiv);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[139]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.aag);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[140]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.target_ex);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[141]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.jmp_tk);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[142]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.is_no_ccc);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[143]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.jmpbreak);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[144]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.tsk_pos);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[145]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.dtu_id, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[146]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.sm_id, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[147]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.is_ob_react);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[148]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.dsse_id, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[149]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.dsse_self_id, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[150]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.is_no_ujb);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[151]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.is_ai_noautotiming);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[152]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.is_mp_use_after);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[153]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ft_turn);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[154]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.protect_iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[155]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.protect_ignore);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[156]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.sa_iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[157]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.sas_iname, formatterResolver);
      return offset - num;
    }

    public JSON_SkillParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (JSON_SkillParam) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      string str1 = (string) null;
      string str2 = (string) null;
      string str3 = (string) null;
      string str4 = (string) null;
      string str5 = (string) null;
      string str6 = (string) null;
      string str7 = (string) null;
      string str8 = (string) null;
      int num3 = 0;
      int num4 = 0;
      int num5 = 0;
      int num6 = 0;
      int num7 = 0;
      int num8 = 0;
      int num9 = 0;
      int num10 = 0;
      int num11 = 0;
      int num12 = 0;
      int num13 = 0;
      int num14 = 0;
      int num15 = 0;
      int num16 = 0;
      int num17 = 0;
      int num18 = 0;
      int num19 = 0;
      int num20 = 0;
      int num21 = 0;
      int num22 = 0;
      int num23 = 0;
      int num24 = 0;
      int num25 = 0;
      int num26 = 0;
      int num27 = 0;
      int num28 = 0;
      int num29 = 0;
      int num30 = 0;
      int num31 = 0;
      short num32 = 0;
      short num33 = 0;
      int num34 = 0;
      int num35 = 0;
      int num36 = 0;
      int num37 = 0;
      int num38 = 0;
      int num39 = 0;
      int num40 = 0;
      int num41 = 0;
      int num42 = 0;
      int num43 = 0;
      int num44 = 0;
      int num45 = 0;
      short num46 = 0;
      short num47 = 0;
      int num48 = 0;
      int num49 = 0;
      int num50 = 0;
      short num51 = 0;
      short num52 = 0;
      int num53 = 0;
      int num54 = 0;
      int num55 = 0;
      int num56 = 0;
      int num57 = 0;
      int num58 = 0;
      int[] numArray = (int[]) null;
      short num59 = 0;
      short num60 = 0;
      short num61 = 0;
      short num62 = 0;
      int num63 = 0;
      short num64 = 0;
      short num65 = 0;
      short num66 = 0;
      short num67 = 0;
      int num68 = 0;
      string str9 = (string) null;
      string str10 = (string) null;
      string str11 = (string) null;
      string str12 = (string) null;
      int num69 = 0;
      int num70 = 0;
      short num71 = 0;
      short num72 = 0;
      int num73 = 0;
      int num74 = 0;
      int num75 = 0;
      string str13 = (string) null;
      int num76 = 0;
      int num77 = 0;
      int num78 = 0;
      int num79 = 0;
      int num80 = 0;
      int num81 = 0;
      int num82 = 0;
      string str14 = (string) null;
      string str15 = (string) null;
      string str16 = (string) null;
      int num83 = 0;
      int num84 = 0;
      int num85 = 0;
      int num86 = 0;
      int num87 = 0;
      int num88 = 0;
      string[] strArray1 = (string[]) null;
      string[] strArray2 = (string[]) null;
      string[] strArray3 = (string[]) null;
      string[] strArray4 = (string[]) null;
      string str17 = (string) null;
      int num89 = 0;
      int num90 = 0;
      int num91 = 0;
      int num92 = 0;
      int num93 = 0;
      int num94 = 0;
      int num95 = 0;
      string str18 = (string) null;
      int num96 = 0;
      int num97 = 0;
      int num98 = 0;
      int num99 = 0;
      string str19 = (string) null;
      string str20 = (string) null;
      int num100 = 0;
      string str21 = (string) null;
      int num101 = 0;
      int num102 = 0;
      int num103 = 0;
      string str22 = (string) null;
      int num104 = 0;
      int num105 = 0;
      int num106 = 0;
      int num107 = 0;
      string str23 = (string) null;
      string str24 = (string) null;
      int num108 = 0;
      int num109 = 0;
      int num110 = 0;
      int num111 = 0;
      int num112 = 0;
      int num113 = 0;
      int num114 = 0;
      int num115 = 0;
      int num116 = 0;
      int num117 = 0;
      int num118 = 0;
      string str25 = (string) null;
      string str26 = (string) null;
      int num119 = 0;
      string str27 = (string) null;
      string str28 = (string) null;
      int num120 = 0;
      int num121 = 0;
      int num122 = 0;
      int num123 = 0;
      string str29 = (string) null;
      int num124 = 0;
      string str30 = (string) null;
      string str31 = (string) null;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num125;
        if (!this.____keyMapping.TryGetValueSafe(key, out num125))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num125)
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
              str5 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 5:
              str6 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 6:
              str7 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 7:
              str8 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 8:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 9:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
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
              num10 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 16:
              num11 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 17:
              num12 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 18:
              num13 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 19:
              num14 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 20:
              num15 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 21:
              num16 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 22:
              num17 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 23:
              num18 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 24:
              num19 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 25:
              num20 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 26:
              num21 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 27:
              num22 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 28:
              num23 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 29:
              num24 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 30:
              num25 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 31:
              num26 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 32:
              num27 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 33:
              num28 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 34:
              num29 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 35:
              num30 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 36:
              num31 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 37:
              num32 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 38:
              num33 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 39:
              num34 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 40:
              num35 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 41:
              num36 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 42:
              num37 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 43:
              num38 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 44:
              num39 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 45:
              num40 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 46:
              num41 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 47:
              num42 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 48:
              num43 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 49:
              num44 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 50:
              num45 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 51:
              num46 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 52:
              num47 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 53:
              num48 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 54:
              num49 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 55:
              num50 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 56:
              num51 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 57:
              num52 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 58:
              num53 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 59:
              num54 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 60:
              num55 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 61:
              num56 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 62:
              num57 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 63:
              num58 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 64:
              numArray = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 65:
              num59 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 66:
              num60 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 67:
              num61 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 68:
              num62 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 69:
              num63 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 70:
              num64 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 71:
              num65 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 72:
              num66 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 73:
              num67 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 74:
              num68 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 75:
              str9 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 76:
              str10 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 77:
              str11 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 78:
              str12 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 79:
              num69 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 80:
              num70 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 81:
              num71 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 82:
              num72 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 83:
              num73 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 84:
              num74 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 85:
              num75 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 86:
              str13 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 87:
              num76 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 88:
              num77 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 89:
              num78 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 90:
              num79 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 91:
              num80 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 92:
              num81 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 93:
              num82 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 94:
              str14 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 95:
              str15 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 96:
              str16 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 97:
              num83 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 98:
              num84 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 99:
              num85 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 100:
              num86 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 101:
              num87 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 102:
              num88 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 103:
              strArray1 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 104:
              strArray2 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 105:
              strArray3 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 106:
              strArray4 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 107:
              str17 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 108:
              num89 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 109:
              num90 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 110:
              num91 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 111:
              num92 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 112:
              num93 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 113:
              num94 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 114:
              num95 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 115:
              str18 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 116:
              num96 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 117:
              num97 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 118:
              num98 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 119:
              num99 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 120:
              str19 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 121:
              str20 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 122:
              num100 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 123:
              str21 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 124:
              num101 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 125:
              num102 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 126:
              num103 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case (int) sbyte.MaxValue:
              str22 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 128:
              num104 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 129:
              num105 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 130:
              num106 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 131:
              num107 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 132:
              str23 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 133:
              str24 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 134:
              num108 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 135:
              num109 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 136:
              num110 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 137:
              num111 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 138:
              num112 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 139:
              num113 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 140:
              num114 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 141:
              num115 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 142:
              num116 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 143:
              num117 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 144:
              num118 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 145:
              str25 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 146:
              str26 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 147:
              num119 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 148:
              str27 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 149:
              str28 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 150:
              num120 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 151:
              num121 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 152:
              num122 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 153:
              num123 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 154:
              str29 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 155:
              num124 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 156:
              str30 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 157:
              str31 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new JSON_SkillParam()
      {
        iname = str1,
        name = str2,
        expr = str3,
        motnm = str4,
        effnm = str5,
        effdef = str6,
        weapon = str7,
        tktag = str8,
        tkrate = num3,
        cutin = num4,
        isbtl = num5,
        cost = num6,
        count = num7,
        cap = num8,
        rate = num9,
        bdb = num10,
        sdb = num11,
        idr = num12,
        line = num13,
        sran = num14,
        rangemin = num15,
        range = num16,
        ssco = num17,
        scope = num18,
        eff_h = num19,
        chran = num20,
        pierce = num21,
        sonoba = num22,
        hbonus = num23,
        ctbreak = num24,
        mpatk = num25,
        fhit = num26,
        suicide = num27,
        rhit = num28,
        hp_cost_rate = num29,
        hp_cost = num30,
        ct_type = num31,
        ct_spd_ini = num32,
        ct_spd_max = num33,
        utgt = num34,
        type = num35,
        timing = num36,
        cond = num37,
        target = num38,
        eff_type = num39,
        atk_type = num40,
        atk_det = num41,
        ehpa = num42,
        elem = num43,
        elem_ini = num44,
        elem_max = num45,
        eff_rate_ini = num46,
        eff_rate_max = num47,
        eff_val_ini = num48,
        eff_val_max = num49,
        eff_calc = num50,
        eff_range_ini = num51,
        eff_range_max = num52,
        eff_hprate = num53,
        eff_mprate = num54,
        eff_durate = num55,
        eff_lvrate = num56,
        abs_d_rate = num57,
        react_d_type = num58,
        react_dets = numArray,
        ctrl_d_rate_ini = num59,
        ctrl_d_rate_max = num60,
        ctrl_d_ini = num61,
        ctrl_d_max = num62,
        ctrl_d_calc = num63,
        ct_rate_ini = num64,
        ct_rate_max = num65,
        ct_val_ini = num66,
        ct_val_max = num67,
        ct_calc = num68,
        t_buff = str9,
        t_cond = str10,
        s_buff = str11,
        s_cond = str12,
        shield_type = num69,
        shield_d_type = num70,
        shield_turn_ini = num71,
        shield_turn_max = num72,
        shield_ini = num73,
        shield_max = num74,
        shield_reset = num75,
        job = str13,
        combo_num = num76,
        combo_rate = num77,
        is_cri = num78,
        jdtype = num79,
        jdv = num80,
        jdabs = num81,
        dupli = num82,
        scn = str14,
        scn_bu = str15,
        cs_main_id = str16,
        cs_height = num83,
        kb_rate = num84,
        kb_val = num85,
        kb_dir = num86,
        kb_ds = num87,
        dmg_dt = num88,
        rp_tgt_ids = strArray1,
        rp_chg_ids = strArray2,
        ab_rp_tgt_ids = strArray3,
        ab_rp_chg_ids = strArray4,
        cs_voice = str17,
        cs_vp_df = num89,
        tl_type = num90,
        tl_target = num91,
        tl_height = num92,
        tl_is_mov = num93,
        sub_actuate = num94,
        is_fixed = num95,
        tr_id = str18,
        tr_set = num96,
        f_ulock = num97,
        ad_react = num98,
        ig_elem = num99,
        bo_id = str19,
        me_desc = str20,
        wth_rate = num100,
        wth_id = str21,
        elem_tk = num101,
        is_pre_apply = num102,
        max_dmg = num103,
        ci_cc_id = str22,
        jhp_val = num104,
        jhp_calc = num105,
        jhp_over = num106,
        is_mhm_dmg = num107,
        ac_fr_ab_id = str23,
        ac_to_ab_id = str24,
        ac_turn = num108,
        ac_is_self = num109,
        ac_is_reset = num110,
        eff_htnrate = num111,
        is_htndiv = num112,
        aag = num113,
        target_ex = num114,
        jmp_tk = num115,
        is_no_ccc = num116,
        jmpbreak = num117,
        tsk_pos = num118,
        dtu_id = str25,
        sm_id = str26,
        is_ob_react = num119,
        dsse_id = str27,
        dsse_self_id = str28,
        is_no_ujb = num120,
        is_ai_noautotiming = num121,
        is_mp_use_after = num122,
        ft_turn = num123,
        protect_iname = str29,
        protect_ignore = num124,
        sa_iname = str30,
        sas_iname = str31
      };
    }
  }
}
