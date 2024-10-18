// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.JSON_FixParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class JSON_FixParamFormatter : 
    IMessagePackFormatter<JSON_FixParam>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public JSON_FixParamFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "mincri",
          0
        },
        {
          "maxcri",
          1
        },
        {
          "mulcri",
          2
        },
        {
          "divcri",
          3
        },
        {
          "mulluk",
          4
        },
        {
          "divluk",
          5
        },
        {
          "back",
          6
        },
        {
          "chmap_heal",
          7
        },
        {
          "hatk",
          8
        },
        {
          "hdef",
          9
        },
        {
          "hcri",
          10
        },
        {
          "datk",
          11
        },
        {
          "ddef",
          12
        },
        {
          "dcri",
          13
        },
        {
          "paralyse",
          14
        },
        {
          "poi_rate",
          15
        },
        {
          "bli_hit",
          16
        },
        {
          "bli_avo",
          17
        },
        {
          "ber_atk",
          18
        },
        {
          "ber_def",
          19
        },
        {
          "tk_rate",
          20
        },
        {
          "abilupcoin",
          21
        },
        {
          "abilupmax",
          22
        },
        {
          "abiluprec",
          23
        },
        {
          "abilupsec",
          24
        },
        {
          "stmncoin",
          25
        },
        {
          "stmnrec",
          26
        },
        {
          "stmnsec",
          27
        },
        {
          "stmnadd",
          28
        },
        {
          "stmnadd2",
          29
        },
        {
          "stmncap",
          30
        },
        {
          "stmncost",
          31
        },
        {
          "cavemax",
          32
        },
        {
          "caverec",
          33
        },
        {
          "cavesec",
          34
        },
        {
          "caveadd",
          35
        },
        {
          "cavecap",
          36
        },
        {
          "cavecost",
          37
        },
        {
          "arenamax",
          38
        },
        {
          "arenasec",
          39
        },
        {
          "arenatcost",
          40
        },
        {
          "arenaccost",
          41
        },
        {
          "arenamedal",
          42
        },
        {
          "arenacoin",
          43
        },
        {
          "tourmax",
          44
        },
        {
          "multimax",
          45
        },
        {
          "awakerate",
          46
        },
        {
          "na_gems",
          47
        },
        {
          "sa_gems",
          48
        },
        {
          "ba_gems",
          49
        },
        {
          "wa_gems",
          50
        },
        {
          "ca_gems",
          51
        },
        {
          "ki_gems",
          52
        },
        {
          "di_gems_floor",
          53
        },
        {
          "di_gems_max",
          54
        },
        {
          "elem_up",
          55
        },
        {
          "elem_down",
          56
        },
        {
          "gems_gain",
          57
        },
        {
          "gems_buff",
          58
        },
        {
          "gems_buff_turn",
          59
        },
        {
          "shop_update_time",
          60
        },
        {
          "continue_cost",
          61
        },
        {
          "continue_cost_multi",
          62
        },
        {
          "continue_cost_multitower",
          63
        },
        {
          "avoid_rate",
          64
        },
        {
          "avoid_scale",
          65
        },
        {
          "avoid_rate_max",
          66
        },
        {
          "products",
          67
        },
        {
          "vip_product",
          68
        },
        {
          "vip_product_au",
          69
        },
        {
          "vip_date",
          70
        },
        {
          "premium_product",
          71
        },
        {
          "premium_product_au",
          72
        },
        {
          "ggmax",
          73
        },
        {
          "ggsec",
          74
        },
        {
          "cgsec",
          75
        },
        {
          "buygoldcost",
          76
        },
        {
          "buygold",
          77
        },
        {
          "sp_cost",
          78
        },
        {
          "ct_poi",
          79
        },
        {
          "ct_par",
          80
        },
        {
          "ct_stu",
          81
        },
        {
          "ct_sle",
          82
        },
        {
          "st_cha",
          83
        },
        {
          "ct_sto",
          84
        },
        {
          "ct_bli",
          85
        },
        {
          "ct_dsk",
          86
        },
        {
          "ct_dmo",
          87
        },
        {
          "ct_dat",
          88
        },
        {
          "ct_zom",
          89
        },
        {
          "ct_dea",
          90
        },
        {
          "ct_dkn",
          91
        },
        {
          "ct_dbu",
          92
        },
        {
          "ct_ddb",
          93
        },
        {
          "ct_ber",
          94
        },
        {
          "ct_stop",
          95
        },
        {
          "ct_fast",
          96
        },
        {
          "ct_slow",
          97
        },
        {
          "ct_ahe",
          98
        },
        {
          "ct_don",
          99
        },
        {
          "ct_rag",
          100
        },
        {
          "ct_gsl",
          101
        },
        {
          "ct_aje",
          102
        },
        {
          "ct_dhe",
          103
        },
        {
          "ct_dsa",
          104
        },
        {
          "ct_daa",
          105
        },
        {
          "ct_ddc",
          106
        },
        {
          "ct_dic",
          107
        },
        {
          "ct_esa",
          108
        },
        {
          "ct_das",
          109
        },
        {
          "ct_dab",
          110
        },
        {
          "ct_dor",
          111
        },
        {
          "ct_dft",
          112
        },
        {
          "yuragi",
          113
        },
        {
          "ct_max",
          114
        },
        {
          "ct_wait",
          115
        },
        {
          "ct_mov",
          116
        },
        {
          "ct_act",
          117
        },
        {
          "hit_side",
          118
        },
        {
          "hit_back",
          119
        },
        {
          "ahhp_rate",
          120
        },
        {
          "ahmp_rate",
          121
        },
        {
          "gshp_rate",
          122
        },
        {
          "gsmp_rate",
          123
        },
        {
          "dy_rate",
          124
        },
        {
          "zsup_rate",
          125
        },
        {
          "beginner_days",
          126
        },
        {
          "afcap",
          (int) sbyte.MaxValue
        },
        {
          "cmn_pi_fire",
          128
        },
        {
          "cmn_pi_water",
          129
        },
        {
          "cmn_pi_thunder",
          130
        },
        {
          "cmn_pi_wind",
          131
        },
        {
          "cmn_pi_shine",
          132
        },
        {
          "cmn_pi_dark",
          133
        },
        {
          "cmn_pi_all",
          134
        },
        {
          "ptnum_nml",
          135
        },
        {
          "ptnum_evnt",
          136
        },
        {
          "ptnum_mlt",
          137
        },
        {
          "ptnum_aatk",
          138
        },
        {
          "ptnum_adef",
          139
        },
        {
          "ptnum_chq",
          140
        },
        {
          "ptnum_tow",
          141
        },
        {
          "ptnum_vs",
          142
        },
        {
          "ptnum_mt",
          143
        },
        {
          "ptnum_ordeal",
          144
        },
        {
          "ptnum_raid",
          145
        },
        {
          "ptnum_guild_raid",
          146
        },
        {
          "ptnum_extra",
          147
        },
        {
          "ptnum_gvg",
          148
        },
        {
          "ptnum_wr",
          149
        },
        {
          "notsus",
          150
        },
        {
          "sus_int",
          151
        },
        {
          "jobms",
          152
        },
        {
          "death_count",
          153
        },
        {
          "fast_val",
          154
        },
        {
          "slow_val",
          155
        },
        {
          "equip_artifact_slot_unlock",
          156
        },
        {
          "kb_gh",
          157
        },
        {
          "th_gh",
          158
        },
        {
          "art_rare_pi",
          159
        },
        {
          "art_cmn_pi",
          160
        },
        {
          "soul_rare",
          161
        },
        {
          "equ_rare_pi",
          162
        },
        {
          "equ_rare_pi_use",
          163
        },
        {
          "equ_rare_cost",
          164
        },
        {
          "aud_max",
          165
        },
        {
          "equip_cmn",
          166
        },
        {
          "ab_rankup_max",
          167
        },
        {
          "ab_rankup_addmax",
          168
        },
        {
          "ab_coin_convert",
          169
        },
        {
          "firstfriend_max",
          170
        },
        {
          "firstfriend_coin",
          171
        },
        {
          "cmb_rate",
          172
        },
        {
          "weak_up",
          173
        },
        {
          "resist_dw",
          174
        },
        {
          "ordeal_ct",
          175
        },
        {
          "esa_assist",
          176
        },
        {
          "esa_resist",
          177
        },
        {
          "card_sell_mul",
          178
        },
        {
          "card_sell_coin_iname",
          179
        },
        {
          "card_sell_coin_mul_level",
          180
        },
        {
          "card_sell_coin_mul_plus",
          181
        },
        {
          "card_exp_mul",
          182
        },
        {
          "card_max",
          183
        },
        {
          "card_trust_max",
          184
        },
        {
          "card_trust_en_bonus",
          185
        },
        {
          "card_trust_qe_bonus",
          186
        },
        {
          "card_trust_lottery_rate",
          187
        },
        {
          "card_awake_unlock_lvcap",
          188
        },
        {
          "tobira_lv_cap",
          189
        },
        {
          "tobira_unit_lv_cap",
          190
        },
        {
          "tobira_unlock_elem",
          191
        },
        {
          "tobira_unlock_birth",
          192
        },
        {
          "ini_rec",
          193
        },
        {
          "ct_mdh",
          194
        },
        {
          "ct_mdm",
          195
        },
        {
          "guerrilla_val",
          196
        },
        {
          "draft_select_sec",
          197
        },
        {
          "draft_organize_sec",
          198
        },
        {
          "draft_place_sec",
          199
        },
        {
          "guild_create_cost",
          200
        },
        {
          "guild_rename_cost",
          201
        },
        {
          "guild_emblem_cost",
          202
        },
        {
          "guild_invest_limit",
          203
        },
        {
          "guild_member_max",
          204
        },
        {
          "guild_submaster_max",
          205
        },
        {
          "guild_entry_cooltime",
          206
        },
        {
          "guild_invest_cooltime",
          207
        },
        {
          "convert_rate_piece_element",
          208
        },
        {
          "convert_rate_piece_common",
          209
        },
        {
          "raid_effective_time",
          210
        },
        {
          "mt_skip_cost",
          211
        },
        {
          "multi_room_comment_max",
          212
        },
        {
          "multi_invite_comment_max",
          213
        },
        {
          "insp_skill_lvup_rate",
          214
        },
        {
          "insp_skill_slot_max",
          215
        },
        {
          "ch_piece_coin_iname",
          216
        },
        {
          "quest_reset_cost",
          217
        },
        {
          "ini_auto_repeat_box",
          218
        },
        {
          "auto_repeat_max",
          219
        },
        {
          "auto_repeat_cooltime",
          220
        },
        {
          "conceptcard_slot2_unlock_tobira",
          221
        },
        {
          "conceptcard_slot2_dec_rate",
          222
        },
        {
          "rune_enh_next_num",
          223
        },
        {
          "rune_evo_num",
          224
        },
        {
          "rune_storage_init",
          225
        },
        {
          "rune_storage_expansion",
          226
        },
        {
          "rune_storage_max",
          227
        },
        {
          "rune_storage_coin_cost",
          228
        },
        {
          "story_ex_total_limit",
          229
        },
        {
          "story_ex_total_limit_reset_num",
          230
        },
        {
          "story_ex_total_limit_reset_cost",
          231
        },
        {
          "wr_dmg_drop_max",
          232
        }
      };
      this.____stringByteKeys = new byte[233][]
      {
        MessagePackBinary.GetEncodedStringBytes("mincri"),
        MessagePackBinary.GetEncodedStringBytes("maxcri"),
        MessagePackBinary.GetEncodedStringBytes("mulcri"),
        MessagePackBinary.GetEncodedStringBytes("divcri"),
        MessagePackBinary.GetEncodedStringBytes("mulluk"),
        MessagePackBinary.GetEncodedStringBytes("divluk"),
        MessagePackBinary.GetEncodedStringBytes("back"),
        MessagePackBinary.GetEncodedStringBytes("chmap_heal"),
        MessagePackBinary.GetEncodedStringBytes("hatk"),
        MessagePackBinary.GetEncodedStringBytes("hdef"),
        MessagePackBinary.GetEncodedStringBytes("hcri"),
        MessagePackBinary.GetEncodedStringBytes("datk"),
        MessagePackBinary.GetEncodedStringBytes("ddef"),
        MessagePackBinary.GetEncodedStringBytes("dcri"),
        MessagePackBinary.GetEncodedStringBytes("paralyse"),
        MessagePackBinary.GetEncodedStringBytes("poi_rate"),
        MessagePackBinary.GetEncodedStringBytes("bli_hit"),
        MessagePackBinary.GetEncodedStringBytes("bli_avo"),
        MessagePackBinary.GetEncodedStringBytes("ber_atk"),
        MessagePackBinary.GetEncodedStringBytes("ber_def"),
        MessagePackBinary.GetEncodedStringBytes("tk_rate"),
        MessagePackBinary.GetEncodedStringBytes("abilupcoin"),
        MessagePackBinary.GetEncodedStringBytes("abilupmax"),
        MessagePackBinary.GetEncodedStringBytes("abiluprec"),
        MessagePackBinary.GetEncodedStringBytes("abilupsec"),
        MessagePackBinary.GetEncodedStringBytes("stmncoin"),
        MessagePackBinary.GetEncodedStringBytes("stmnrec"),
        MessagePackBinary.GetEncodedStringBytes("stmnsec"),
        MessagePackBinary.GetEncodedStringBytes("stmnadd"),
        MessagePackBinary.GetEncodedStringBytes("stmnadd2"),
        MessagePackBinary.GetEncodedStringBytes("stmncap"),
        MessagePackBinary.GetEncodedStringBytes("stmncost"),
        MessagePackBinary.GetEncodedStringBytes("cavemax"),
        MessagePackBinary.GetEncodedStringBytes("caverec"),
        MessagePackBinary.GetEncodedStringBytes("cavesec"),
        MessagePackBinary.GetEncodedStringBytes("caveadd"),
        MessagePackBinary.GetEncodedStringBytes("cavecap"),
        MessagePackBinary.GetEncodedStringBytes("cavecost"),
        MessagePackBinary.GetEncodedStringBytes("arenamax"),
        MessagePackBinary.GetEncodedStringBytes("arenasec"),
        MessagePackBinary.GetEncodedStringBytes("arenatcost"),
        MessagePackBinary.GetEncodedStringBytes("arenaccost"),
        MessagePackBinary.GetEncodedStringBytes("arenamedal"),
        MessagePackBinary.GetEncodedStringBytes("arenacoin"),
        MessagePackBinary.GetEncodedStringBytes("tourmax"),
        MessagePackBinary.GetEncodedStringBytes("multimax"),
        MessagePackBinary.GetEncodedStringBytes("awakerate"),
        MessagePackBinary.GetEncodedStringBytes("na_gems"),
        MessagePackBinary.GetEncodedStringBytes("sa_gems"),
        MessagePackBinary.GetEncodedStringBytes("ba_gems"),
        MessagePackBinary.GetEncodedStringBytes("wa_gems"),
        MessagePackBinary.GetEncodedStringBytes("ca_gems"),
        MessagePackBinary.GetEncodedStringBytes("ki_gems"),
        MessagePackBinary.GetEncodedStringBytes("di_gems_floor"),
        MessagePackBinary.GetEncodedStringBytes("di_gems_max"),
        MessagePackBinary.GetEncodedStringBytes("elem_up"),
        MessagePackBinary.GetEncodedStringBytes("elem_down"),
        MessagePackBinary.GetEncodedStringBytes("gems_gain"),
        MessagePackBinary.GetEncodedStringBytes("gems_buff"),
        MessagePackBinary.GetEncodedStringBytes("gems_buff_turn"),
        MessagePackBinary.GetEncodedStringBytes("shop_update_time"),
        MessagePackBinary.GetEncodedStringBytes("continue_cost"),
        MessagePackBinary.GetEncodedStringBytes("continue_cost_multi"),
        MessagePackBinary.GetEncodedStringBytes("continue_cost_multitower"),
        MessagePackBinary.GetEncodedStringBytes("avoid_rate"),
        MessagePackBinary.GetEncodedStringBytes("avoid_scale"),
        MessagePackBinary.GetEncodedStringBytes("avoid_rate_max"),
        MessagePackBinary.GetEncodedStringBytes("products"),
        MessagePackBinary.GetEncodedStringBytes("vip_product"),
        MessagePackBinary.GetEncodedStringBytes("vip_product_au"),
        MessagePackBinary.GetEncodedStringBytes("vip_date"),
        MessagePackBinary.GetEncodedStringBytes("premium_product"),
        MessagePackBinary.GetEncodedStringBytes("premium_product_au"),
        MessagePackBinary.GetEncodedStringBytes("ggmax"),
        MessagePackBinary.GetEncodedStringBytes("ggsec"),
        MessagePackBinary.GetEncodedStringBytes("cgsec"),
        MessagePackBinary.GetEncodedStringBytes("buygoldcost"),
        MessagePackBinary.GetEncodedStringBytes("buygold"),
        MessagePackBinary.GetEncodedStringBytes("sp_cost"),
        MessagePackBinary.GetEncodedStringBytes("ct_poi"),
        MessagePackBinary.GetEncodedStringBytes("ct_par"),
        MessagePackBinary.GetEncodedStringBytes("ct_stu"),
        MessagePackBinary.GetEncodedStringBytes("ct_sle"),
        MessagePackBinary.GetEncodedStringBytes("st_cha"),
        MessagePackBinary.GetEncodedStringBytes("ct_sto"),
        MessagePackBinary.GetEncodedStringBytes("ct_bli"),
        MessagePackBinary.GetEncodedStringBytes("ct_dsk"),
        MessagePackBinary.GetEncodedStringBytes("ct_dmo"),
        MessagePackBinary.GetEncodedStringBytes("ct_dat"),
        MessagePackBinary.GetEncodedStringBytes("ct_zom"),
        MessagePackBinary.GetEncodedStringBytes("ct_dea"),
        MessagePackBinary.GetEncodedStringBytes("ct_dkn"),
        MessagePackBinary.GetEncodedStringBytes("ct_dbu"),
        MessagePackBinary.GetEncodedStringBytes("ct_ddb"),
        MessagePackBinary.GetEncodedStringBytes("ct_ber"),
        MessagePackBinary.GetEncodedStringBytes("ct_stop"),
        MessagePackBinary.GetEncodedStringBytes("ct_fast"),
        MessagePackBinary.GetEncodedStringBytes("ct_slow"),
        MessagePackBinary.GetEncodedStringBytes("ct_ahe"),
        MessagePackBinary.GetEncodedStringBytes("ct_don"),
        MessagePackBinary.GetEncodedStringBytes("ct_rag"),
        MessagePackBinary.GetEncodedStringBytes("ct_gsl"),
        MessagePackBinary.GetEncodedStringBytes("ct_aje"),
        MessagePackBinary.GetEncodedStringBytes("ct_dhe"),
        MessagePackBinary.GetEncodedStringBytes("ct_dsa"),
        MessagePackBinary.GetEncodedStringBytes("ct_daa"),
        MessagePackBinary.GetEncodedStringBytes("ct_ddc"),
        MessagePackBinary.GetEncodedStringBytes("ct_dic"),
        MessagePackBinary.GetEncodedStringBytes("ct_esa"),
        MessagePackBinary.GetEncodedStringBytes("ct_das"),
        MessagePackBinary.GetEncodedStringBytes("ct_dab"),
        MessagePackBinary.GetEncodedStringBytes("ct_dor"),
        MessagePackBinary.GetEncodedStringBytes("ct_dft"),
        MessagePackBinary.GetEncodedStringBytes("yuragi"),
        MessagePackBinary.GetEncodedStringBytes("ct_max"),
        MessagePackBinary.GetEncodedStringBytes("ct_wait"),
        MessagePackBinary.GetEncodedStringBytes("ct_mov"),
        MessagePackBinary.GetEncodedStringBytes("ct_act"),
        MessagePackBinary.GetEncodedStringBytes("hit_side"),
        MessagePackBinary.GetEncodedStringBytes("hit_back"),
        MessagePackBinary.GetEncodedStringBytes("ahhp_rate"),
        MessagePackBinary.GetEncodedStringBytes("ahmp_rate"),
        MessagePackBinary.GetEncodedStringBytes("gshp_rate"),
        MessagePackBinary.GetEncodedStringBytes("gsmp_rate"),
        MessagePackBinary.GetEncodedStringBytes("dy_rate"),
        MessagePackBinary.GetEncodedStringBytes("zsup_rate"),
        MessagePackBinary.GetEncodedStringBytes("beginner_days"),
        MessagePackBinary.GetEncodedStringBytes("afcap"),
        MessagePackBinary.GetEncodedStringBytes("cmn_pi_fire"),
        MessagePackBinary.GetEncodedStringBytes("cmn_pi_water"),
        MessagePackBinary.GetEncodedStringBytes("cmn_pi_thunder"),
        MessagePackBinary.GetEncodedStringBytes("cmn_pi_wind"),
        MessagePackBinary.GetEncodedStringBytes("cmn_pi_shine"),
        MessagePackBinary.GetEncodedStringBytes("cmn_pi_dark"),
        MessagePackBinary.GetEncodedStringBytes("cmn_pi_all"),
        MessagePackBinary.GetEncodedStringBytes("ptnum_nml"),
        MessagePackBinary.GetEncodedStringBytes("ptnum_evnt"),
        MessagePackBinary.GetEncodedStringBytes("ptnum_mlt"),
        MessagePackBinary.GetEncodedStringBytes("ptnum_aatk"),
        MessagePackBinary.GetEncodedStringBytes("ptnum_adef"),
        MessagePackBinary.GetEncodedStringBytes("ptnum_chq"),
        MessagePackBinary.GetEncodedStringBytes("ptnum_tow"),
        MessagePackBinary.GetEncodedStringBytes("ptnum_vs"),
        MessagePackBinary.GetEncodedStringBytes("ptnum_mt"),
        MessagePackBinary.GetEncodedStringBytes("ptnum_ordeal"),
        MessagePackBinary.GetEncodedStringBytes("ptnum_raid"),
        MessagePackBinary.GetEncodedStringBytes("ptnum_guild_raid"),
        MessagePackBinary.GetEncodedStringBytes("ptnum_extra"),
        MessagePackBinary.GetEncodedStringBytes("ptnum_gvg"),
        MessagePackBinary.GetEncodedStringBytes("ptnum_wr"),
        MessagePackBinary.GetEncodedStringBytes("notsus"),
        MessagePackBinary.GetEncodedStringBytes("sus_int"),
        MessagePackBinary.GetEncodedStringBytes("jobms"),
        MessagePackBinary.GetEncodedStringBytes("death_count"),
        MessagePackBinary.GetEncodedStringBytes("fast_val"),
        MessagePackBinary.GetEncodedStringBytes("slow_val"),
        MessagePackBinary.GetEncodedStringBytes("equip_artifact_slot_unlock"),
        MessagePackBinary.GetEncodedStringBytes("kb_gh"),
        MessagePackBinary.GetEncodedStringBytes("th_gh"),
        MessagePackBinary.GetEncodedStringBytes("art_rare_pi"),
        MessagePackBinary.GetEncodedStringBytes("art_cmn_pi"),
        MessagePackBinary.GetEncodedStringBytes("soul_rare"),
        MessagePackBinary.GetEncodedStringBytes("equ_rare_pi"),
        MessagePackBinary.GetEncodedStringBytes("equ_rare_pi_use"),
        MessagePackBinary.GetEncodedStringBytes("equ_rare_cost"),
        MessagePackBinary.GetEncodedStringBytes("aud_max"),
        MessagePackBinary.GetEncodedStringBytes("equip_cmn"),
        MessagePackBinary.GetEncodedStringBytes("ab_rankup_max"),
        MessagePackBinary.GetEncodedStringBytes("ab_rankup_addmax"),
        MessagePackBinary.GetEncodedStringBytes("ab_coin_convert"),
        MessagePackBinary.GetEncodedStringBytes("firstfriend_max"),
        MessagePackBinary.GetEncodedStringBytes("firstfriend_coin"),
        MessagePackBinary.GetEncodedStringBytes("cmb_rate"),
        MessagePackBinary.GetEncodedStringBytes("weak_up"),
        MessagePackBinary.GetEncodedStringBytes("resist_dw"),
        MessagePackBinary.GetEncodedStringBytes("ordeal_ct"),
        MessagePackBinary.GetEncodedStringBytes("esa_assist"),
        MessagePackBinary.GetEncodedStringBytes("esa_resist"),
        MessagePackBinary.GetEncodedStringBytes("card_sell_mul"),
        MessagePackBinary.GetEncodedStringBytes("card_sell_coin_iname"),
        MessagePackBinary.GetEncodedStringBytes("card_sell_coin_mul_level"),
        MessagePackBinary.GetEncodedStringBytes("card_sell_coin_mul_plus"),
        MessagePackBinary.GetEncodedStringBytes("card_exp_mul"),
        MessagePackBinary.GetEncodedStringBytes("card_max"),
        MessagePackBinary.GetEncodedStringBytes("card_trust_max"),
        MessagePackBinary.GetEncodedStringBytes("card_trust_en_bonus"),
        MessagePackBinary.GetEncodedStringBytes("card_trust_qe_bonus"),
        MessagePackBinary.GetEncodedStringBytes("card_trust_lottery_rate"),
        MessagePackBinary.GetEncodedStringBytes("card_awake_unlock_lvcap"),
        MessagePackBinary.GetEncodedStringBytes("tobira_lv_cap"),
        MessagePackBinary.GetEncodedStringBytes("tobira_unit_lv_cap"),
        MessagePackBinary.GetEncodedStringBytes("tobira_unlock_elem"),
        MessagePackBinary.GetEncodedStringBytes("tobira_unlock_birth"),
        MessagePackBinary.GetEncodedStringBytes("ini_rec"),
        MessagePackBinary.GetEncodedStringBytes("ct_mdh"),
        MessagePackBinary.GetEncodedStringBytes("ct_mdm"),
        MessagePackBinary.GetEncodedStringBytes("guerrilla_val"),
        MessagePackBinary.GetEncodedStringBytes("draft_select_sec"),
        MessagePackBinary.GetEncodedStringBytes("draft_organize_sec"),
        MessagePackBinary.GetEncodedStringBytes("draft_place_sec"),
        MessagePackBinary.GetEncodedStringBytes("guild_create_cost"),
        MessagePackBinary.GetEncodedStringBytes("guild_rename_cost"),
        MessagePackBinary.GetEncodedStringBytes("guild_emblem_cost"),
        MessagePackBinary.GetEncodedStringBytes("guild_invest_limit"),
        MessagePackBinary.GetEncodedStringBytes("guild_member_max"),
        MessagePackBinary.GetEncodedStringBytes("guild_submaster_max"),
        MessagePackBinary.GetEncodedStringBytes("guild_entry_cooltime"),
        MessagePackBinary.GetEncodedStringBytes("guild_invest_cooltime"),
        MessagePackBinary.GetEncodedStringBytes("convert_rate_piece_element"),
        MessagePackBinary.GetEncodedStringBytes("convert_rate_piece_common"),
        MessagePackBinary.GetEncodedStringBytes("raid_effective_time"),
        MessagePackBinary.GetEncodedStringBytes("mt_skip_cost"),
        MessagePackBinary.GetEncodedStringBytes("multi_room_comment_max"),
        MessagePackBinary.GetEncodedStringBytes("multi_invite_comment_max"),
        MessagePackBinary.GetEncodedStringBytes("insp_skill_lvup_rate"),
        MessagePackBinary.GetEncodedStringBytes("insp_skill_slot_max"),
        MessagePackBinary.GetEncodedStringBytes("ch_piece_coin_iname"),
        MessagePackBinary.GetEncodedStringBytes("quest_reset_cost"),
        MessagePackBinary.GetEncodedStringBytes("ini_auto_repeat_box"),
        MessagePackBinary.GetEncodedStringBytes("auto_repeat_max"),
        MessagePackBinary.GetEncodedStringBytes("auto_repeat_cooltime"),
        MessagePackBinary.GetEncodedStringBytes("conceptcard_slot2_unlock_tobira"),
        MessagePackBinary.GetEncodedStringBytes("conceptcard_slot2_dec_rate"),
        MessagePackBinary.GetEncodedStringBytes("rune_enh_next_num"),
        MessagePackBinary.GetEncodedStringBytes("rune_evo_num"),
        MessagePackBinary.GetEncodedStringBytes("rune_storage_init"),
        MessagePackBinary.GetEncodedStringBytes("rune_storage_expansion"),
        MessagePackBinary.GetEncodedStringBytes("rune_storage_max"),
        MessagePackBinary.GetEncodedStringBytes("rune_storage_coin_cost"),
        MessagePackBinary.GetEncodedStringBytes("story_ex_total_limit"),
        MessagePackBinary.GetEncodedStringBytes("story_ex_total_limit_reset_num"),
        MessagePackBinary.GetEncodedStringBytes("story_ex_total_limit_reset_cost"),
        MessagePackBinary.GetEncodedStringBytes("wr_dmg_drop_max")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      JSON_FixParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, 233);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mincri);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.maxcri);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mulcri);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.divcri);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mulluk);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.divluk);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.back);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.chmap_heal);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.hatk);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.hdef);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.hcri);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.datk);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ddef);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.dcri);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.paralyse);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.poi_rate);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[16]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.bli_hit);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[17]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.bli_avo);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[18]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ber_atk);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[19]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ber_def);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[20]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.tk_rate);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[21]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.abilupcoin);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[22]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.abilupmax);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[23]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.abiluprec);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[24]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.abilupsec);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[25]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.stmncoin);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[26]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.stmnrec);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[27]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.stmnsec);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[28]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.stmnadd);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[29]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.stmnadd2);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[30]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.stmncap);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[31]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.stmncost, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[32]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.cavemax);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[33]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.caverec);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[34]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.cavesec);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[35]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.caveadd);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[36]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.cavecap);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[37]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.cavecost, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[38]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.arenamax);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[39]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.arenasec);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[40]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.arenatcost, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[41]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.arenaccost);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[42]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.arenamedal);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[43]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.arenacoin);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[44]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.tourmax);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[45]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.multimax);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[46]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.awakerate);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[47]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.na_gems);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[48]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.sa_gems);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[49]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ba_gems);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[50]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.wa_gems);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[51]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ca_gems);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[52]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ki_gems);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[53]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.di_gems_floor);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[54]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.di_gems_max);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[55]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.elem_up);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[56]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.elem_down);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[57]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.gems_gain);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[58]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.gems_buff);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[59]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.gems_buff_turn);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[60]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.shop_update_time, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[61]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.continue_cost);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[62]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.continue_cost_multi);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[63]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.continue_cost_multitower);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[64]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.avoid_rate);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[65]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.avoid_scale);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[66]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.avoid_rate_max);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[67]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.products, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[68]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.vip_product, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[69]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.vip_product_au, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[70]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.vip_date);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[71]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.premium_product, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[72]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.premium_product_au, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[73]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ggmax);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[74]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ggsec);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[75]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.cgsec);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[76]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.buygoldcost);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[77]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.buygold);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[78]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.sp_cost);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[79]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ct_poi);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[80]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ct_par);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[81]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ct_stu);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[82]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ct_sle);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[83]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.st_cha);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[84]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ct_sto);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[85]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ct_bli);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[86]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ct_dsk);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[87]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ct_dmo);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[88]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ct_dat);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[89]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ct_zom);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[90]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ct_dea);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[91]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ct_dkn);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[92]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ct_dbu);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[93]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ct_ddb);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[94]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ct_ber);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[95]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ct_stop);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[96]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ct_fast);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[97]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ct_slow);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[98]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ct_ahe);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[99]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ct_don);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[100]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ct_rag);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[101]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ct_gsl);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[102]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ct_aje);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[103]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ct_dhe);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[104]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ct_dsa);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[105]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ct_daa);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[106]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ct_ddc);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[107]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ct_dic);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[108]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ct_esa);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[109]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ct_das);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[110]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ct_dab);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[111]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ct_dor);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[112]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ct_dft);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[113]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.yuragi);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[114]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ct_max);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[115]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ct_wait);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[116]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ct_mov);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[117]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ct_act);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[118]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.hit_side);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[119]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.hit_back);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[120]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ahhp_rate);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[121]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ahmp_rate);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[122]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.gshp_rate);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[123]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.gsmp_rate);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[124]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.dy_rate);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[125]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.zsup_rate);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[126]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.beginner_days);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[(int) sbyte.MaxValue]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.afcap);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[128]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.cmn_pi_fire, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[129]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.cmn_pi_water, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[130]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.cmn_pi_thunder, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[131]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.cmn_pi_wind, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[132]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.cmn_pi_shine, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[133]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.cmn_pi_dark, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[134]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.cmn_pi_all, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[135]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ptnum_nml);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[136]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ptnum_evnt);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[137]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ptnum_mlt);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[138]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ptnum_aatk);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[139]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ptnum_adef);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[140]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ptnum_chq);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[141]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ptnum_tow);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[142]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ptnum_vs);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[143]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ptnum_mt);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[144]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ptnum_ordeal);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[145]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ptnum_raid);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[146]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ptnum_guild_raid);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[147]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ptnum_extra);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[148]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ptnum_gvg);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[149]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ptnum_wr);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[150]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.notsus);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[151]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.sus_int);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[152]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.jobms);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[153]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.death_count);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[154]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.fast_val);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[155]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.slow_val);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[156]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.equip_artifact_slot_unlock, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[157]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.kb_gh);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[158]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.th_gh);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[159]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.art_rare_pi, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[160]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.art_cmn_pi, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[161]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.soul_rare, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[162]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.equ_rare_pi, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[163]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.equ_rare_pi_use, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[164]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.equ_rare_cost, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[165]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.aud_max);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[166]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.equip_cmn, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[167]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ab_rankup_max);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[168]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ab_rankup_addmax);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[169]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ab_coin_convert);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[170]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.firstfriend_max);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[171]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.firstfriend_coin);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[172]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.cmb_rate);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[173]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.weak_up);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[174]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.resist_dw);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[175]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ordeal_ct);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[176]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.esa_assist);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[177]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.esa_resist);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[178]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.card_sell_mul);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[179]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.card_sell_coin_iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[180]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.card_sell_coin_mul_level);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[181]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.card_sell_coin_mul_plus);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[182]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.card_exp_mul);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[183]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.card_max);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[184]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.card_trust_max);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[185]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.card_trust_en_bonus);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[186]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.card_trust_qe_bonus);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[187]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.card_trust_lottery_rate);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[188]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.card_awake_unlock_lvcap);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[189]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.tobira_lv_cap);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[190]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.tobira_unit_lv_cap);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[191]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.tobira_unlock_elem, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[192]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.tobira_unlock_birth, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[193]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ini_rec);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[194]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ct_mdh);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[195]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ct_mdm);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[196]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.guerrilla_val);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[197]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.draft_select_sec);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[198]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.draft_organize_sec);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[199]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.draft_place_sec);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[200]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.guild_create_cost);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[201]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.guild_rename_cost);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[202]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.guild_emblem_cost);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[203]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.guild_invest_limit);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[204]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.guild_member_max);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[205]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.guild_submaster_max);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[206]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.guild_entry_cooltime);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[207]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.guild_invest_cooltime);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[208]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.convert_rate_piece_element);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[209]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.convert_rate_piece_common);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[210]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.raid_effective_time, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[211]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mt_skip_cost);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[212]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.multi_room_comment_max);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[213]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.multi_invite_comment_max);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[214]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.insp_skill_lvup_rate);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[215]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.insp_skill_slot_max);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[216]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.ch_piece_coin_iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[217]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.quest_reset_cost, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[218]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ini_auto_repeat_box);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[219]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.auto_repeat_max);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[220]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.auto_repeat_cooltime);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[221]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.conceptcard_slot2_unlock_tobira);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[222]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.conceptcard_slot2_dec_rate, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[223]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rune_enh_next_num);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[224]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rune_evo_num);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[225]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rune_storage_init);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[226]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rune_storage_expansion);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[227]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rune_storage_max);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[228]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rune_storage_coin_cost);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[229]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.story_ex_total_limit);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[230]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.story_ex_total_limit_reset_num);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[231]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.story_ex_total_limit_reset_cost, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[232]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.wr_dmg_drop_max);
      return offset - num;
    }

    public JSON_FixParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (JSON_FixParam) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
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
      int num32 = 0;
      int num33 = 0;
      int[] numArray1 = (int[]) null;
      int num34 = 0;
      int num35 = 0;
      int num36 = 0;
      int num37 = 0;
      int num38 = 0;
      int[] numArray2 = (int[]) null;
      int num39 = 0;
      int num40 = 0;
      int[] numArray3 = (int[]) null;
      int num41 = 0;
      int num42 = 0;
      int num43 = 0;
      int num44 = 0;
      int num45 = 0;
      int num46 = 0;
      int num47 = 0;
      int num48 = 0;
      int num49 = 0;
      int num50 = 0;
      int num51 = 0;
      int num52 = 0;
      int num53 = 0;
      int num54 = 0;
      int num55 = 0;
      int num56 = 0;
      int num57 = 0;
      int num58 = 0;
      int num59 = 0;
      int[] numArray4 = (int[]) null;
      int num60 = 0;
      int num61 = 0;
      int num62 = 0;
      int num63 = 0;
      int num64 = 0;
      int num65 = 0;
      string[] strArray1 = (string[]) null;
      string str1 = (string) null;
      string str2 = (string) null;
      int num66 = 0;
      string str3 = (string) null;
      string str4 = (string) null;
      int num67 = 0;
      int num68 = 0;
      int num69 = 0;
      int num70 = 0;
      int num71 = 0;
      int num72 = 0;
      int num73 = 0;
      int num74 = 0;
      int num75 = 0;
      int num76 = 0;
      int num77 = 0;
      int num78 = 0;
      int num79 = 0;
      int num80 = 0;
      int num81 = 0;
      int num82 = 0;
      int num83 = 0;
      int num84 = 0;
      int num85 = 0;
      int num86 = 0;
      int num87 = 0;
      int num88 = 0;
      int num89 = 0;
      int num90 = 0;
      int num91 = 0;
      int num92 = 0;
      int num93 = 0;
      int num94 = 0;
      int num95 = 0;
      int num96 = 0;
      int num97 = 0;
      int num98 = 0;
      int num99 = 0;
      int num100 = 0;
      int num101 = 0;
      int num102 = 0;
      int num103 = 0;
      int num104 = 0;
      int num105 = 0;
      int num106 = 0;
      int num107 = 0;
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
      int num119 = 0;
      int num120 = 0;
      int num121 = 0;
      string str5 = (string) null;
      string str6 = (string) null;
      string str7 = (string) null;
      string str8 = (string) null;
      string str9 = (string) null;
      string str10 = (string) null;
      string str11 = (string) null;
      int num122 = 0;
      int num123 = 0;
      int num124 = 0;
      int num125 = 0;
      int num126 = 0;
      int num127 = 0;
      int num128 = 0;
      int num129 = 0;
      int num130 = 0;
      int num131 = 0;
      int num132 = 0;
      int num133 = 0;
      int num134 = 0;
      int num135 = 0;
      int num136 = 0;
      int num137 = 0;
      int num138 = 0;
      int num139 = 0;
      int num140 = 0;
      int num141 = 0;
      int num142 = 0;
      int[] numArray5 = (int[]) null;
      int num143 = 0;
      int num144 = 0;
      string[] strArray2 = (string[]) null;
      string str12 = (string) null;
      string[] strArray3 = (string[]) null;
      string[] strArray4 = (string[]) null;
      int[] numArray6 = (int[]) null;
      int[] numArray7 = (int[]) null;
      int num145 = 0;
      string[] strArray5 = (string[]) null;
      int num146 = 0;
      int num147 = 0;
      int num148 = 0;
      int num149 = 0;
      int num150 = 0;
      int num151 = 0;
      int num152 = 0;
      int num153 = 0;
      int num154 = 0;
      int num155 = 0;
      int num156 = 0;
      int num157 = 0;
      string str13 = (string) null;
      int num158 = 0;
      int num159 = 0;
      int num160 = 0;
      int num161 = 0;
      int num162 = 0;
      int num163 = 0;
      int num164 = 0;
      int num165 = 0;
      int num166 = 0;
      int num167 = 0;
      int num168 = 0;
      string[] strArray6 = (string[]) null;
      string[] strArray7 = (string[]) null;
      int num169 = 0;
      int num170 = 0;
      int num171 = 0;
      int num172 = 0;
      int num173 = 0;
      int num174 = 0;
      int num175 = 0;
      int num176 = 0;
      int num177 = 0;
      int num178 = 0;
      int num179 = 0;
      int num180 = 0;
      int num181 = 0;
      int num182 = 0;
      int num183 = 0;
      int num184 = 0;
      int num185 = 0;
      string str14 = (string) null;
      int num186 = 0;
      int num187 = 0;
      int num188 = 0;
      int num189 = 0;
      int num190 = 0;
      string str15 = (string) null;
      int[] numArray8 = (int[]) null;
      int num191 = 0;
      int num192 = 0;
      int num193 = 0;
      int num194 = 0;
      int[] numArray9 = (int[]) null;
      int num195 = 0;
      int num196 = 0;
      int num197 = 0;
      int num198 = 0;
      int num199 = 0;
      int num200 = 0;
      int num201 = 0;
      int num202 = 0;
      string str16 = (string) null;
      int num203 = 0;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num204;
        if (!this.____keyMapping.TryGetValueSafe(key, out num204))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num204)
          {
            case 0:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 1:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 2:
              num5 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 3:
              num6 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 4:
              num7 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 5:
              num8 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 6:
              num9 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 7:
              num10 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 8:
              num11 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 9:
              num12 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 10:
              num13 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 11:
              num14 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 12:
              num15 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 13:
              num16 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 14:
              num17 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 15:
              num18 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 16:
              num19 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 17:
              num20 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 18:
              num21 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 19:
              num22 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 20:
              num23 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 21:
              num24 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 22:
              num25 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 23:
              num26 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 24:
              num27 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 25:
              num28 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 26:
              num29 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 27:
              num30 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 28:
              num31 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 29:
              num32 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 30:
              num33 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 31:
              numArray1 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 32:
              num34 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 33:
              num35 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 34:
              num36 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 35:
              num37 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 36:
              num38 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 37:
              numArray2 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 38:
              num39 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 39:
              num40 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 40:
              numArray3 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 41:
              num41 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 42:
              num42 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 43:
              num43 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 44:
              num44 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 45:
              num45 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 46:
              num46 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 47:
              num47 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 48:
              num48 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 49:
              num49 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 50:
              num50 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 51:
              num51 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 52:
              num52 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 53:
              num53 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 54:
              num54 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 55:
              num55 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 56:
              num56 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 57:
              num57 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 58:
              num58 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 59:
              num59 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 60:
              numArray4 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 61:
              num60 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 62:
              num61 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 63:
              num62 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 64:
              num63 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 65:
              num64 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 66:
              num65 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 67:
              strArray1 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 68:
              str1 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 69:
              str2 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 70:
              num66 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 71:
              str3 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 72:
              str4 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 73:
              num67 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 74:
              num68 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 75:
              num69 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 76:
              num70 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 77:
              num71 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 78:
              num72 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 79:
              num73 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 80:
              num74 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 81:
              num75 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 82:
              num76 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 83:
              num77 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 84:
              num78 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 85:
              num79 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 86:
              num80 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 87:
              num81 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 88:
              num82 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 89:
              num83 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 90:
              num84 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 91:
              num85 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 92:
              num86 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 93:
              num87 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 94:
              num88 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 95:
              num89 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 96:
              num90 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 97:
              num91 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 98:
              num92 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 99:
              num93 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 100:
              num94 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 101:
              num95 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 102:
              num96 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 103:
              num97 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 104:
              num98 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 105:
              num99 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 106:
              num100 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 107:
              num101 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 108:
              num102 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 109:
              num103 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 110:
              num104 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 111:
              num105 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 112:
              num106 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 113:
              num107 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 114:
              num108 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 115:
              num109 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 116:
              num110 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 117:
              num111 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 118:
              num112 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 119:
              num113 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 120:
              num114 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 121:
              num115 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 122:
              num116 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 123:
              num117 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 124:
              num118 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 125:
              num119 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 126:
              num120 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case (int) sbyte.MaxValue:
              num121 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 128:
              str5 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 129:
              str6 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 130:
              str7 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 131:
              str8 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 132:
              str9 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 133:
              str10 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 134:
              str11 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 135:
              num122 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 136:
              num123 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 137:
              num124 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 138:
              num125 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 139:
              num126 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 140:
              num127 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 141:
              num128 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 142:
              num129 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 143:
              num130 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 144:
              num131 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 145:
              num132 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 146:
              num133 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 147:
              num134 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 148:
              num135 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 149:
              num136 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 150:
              num137 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 151:
              num138 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 152:
              num139 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 153:
              num140 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 154:
              num141 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 155:
              num142 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 156:
              numArray5 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 157:
              num143 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 158:
              num144 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 159:
              strArray2 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 160:
              str12 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 161:
              strArray3 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 162:
              strArray4 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 163:
              numArray6 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 164:
              numArray7 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 165:
              num145 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 166:
              strArray5 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 167:
              num146 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 168:
              num147 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 169:
              num148 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 170:
              num149 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 171:
              num150 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 172:
              num151 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 173:
              num152 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 174:
              num153 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 175:
              num154 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 176:
              num155 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 177:
              num156 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 178:
              num157 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 179:
              str13 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 180:
              num158 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 181:
              num159 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 182:
              num160 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 183:
              num161 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 184:
              num162 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 185:
              num163 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 186:
              num164 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 187:
              num165 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 188:
              num166 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 189:
              num167 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 190:
              num168 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 191:
              strArray6 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 192:
              strArray7 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 193:
              num169 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 194:
              num170 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 195:
              num171 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 196:
              num172 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 197:
              num173 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 198:
              num174 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 199:
              num175 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 200:
              num176 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 201:
              num177 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 202:
              num178 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 203:
              num179 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 204:
              num180 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 205:
              num181 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 206:
              num182 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 207:
              num183 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 208:
              num184 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 209:
              num185 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 210:
              str14 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 211:
              num186 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 212:
              num187 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 213:
              num188 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 214:
              num189 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 215:
              num190 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 216:
              str15 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 217:
              numArray8 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 218:
              num191 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 219:
              num192 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 220:
              num193 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 221:
              num194 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 222:
              numArray9 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 223:
              num195 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 224:
              num196 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 225:
              num197 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 226:
              num198 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 227:
              num199 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 228:
              num200 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 229:
              num201 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 230:
              num202 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 231:
              str16 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 232:
              num203 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new JSON_FixParam()
      {
        mincri = num3,
        maxcri = num4,
        mulcri = num5,
        divcri = num6,
        mulluk = num7,
        divluk = num8,
        back = num9,
        chmap_heal = num10,
        hatk = num11,
        hdef = num12,
        hcri = num13,
        datk = num14,
        ddef = num15,
        dcri = num16,
        paralyse = num17,
        poi_rate = num18,
        bli_hit = num19,
        bli_avo = num20,
        ber_atk = num21,
        ber_def = num22,
        tk_rate = num23,
        abilupcoin = num24,
        abilupmax = num25,
        abiluprec = num26,
        abilupsec = num27,
        stmncoin = num28,
        stmnrec = num29,
        stmnsec = num30,
        stmnadd = num31,
        stmnadd2 = num32,
        stmncap = num33,
        stmncost = numArray1,
        cavemax = num34,
        caverec = num35,
        cavesec = num36,
        caveadd = num37,
        cavecap = num38,
        cavecost = numArray2,
        arenamax = num39,
        arenasec = num40,
        arenatcost = numArray3,
        arenaccost = num41,
        arenamedal = num42,
        arenacoin = num43,
        tourmax = num44,
        multimax = num45,
        awakerate = num46,
        na_gems = num47,
        sa_gems = num48,
        ba_gems = num49,
        wa_gems = num50,
        ca_gems = num51,
        ki_gems = num52,
        di_gems_floor = num53,
        di_gems_max = num54,
        elem_up = num55,
        elem_down = num56,
        gems_gain = num57,
        gems_buff = num58,
        gems_buff_turn = num59,
        shop_update_time = numArray4,
        continue_cost = num60,
        continue_cost_multi = num61,
        continue_cost_multitower = num62,
        avoid_rate = num63,
        avoid_scale = num64,
        avoid_rate_max = num65,
        products = strArray1,
        vip_product = str1,
        vip_product_au = str2,
        vip_date = num66,
        premium_product = str3,
        premium_product_au = str4,
        ggmax = num67,
        ggsec = num68,
        cgsec = num69,
        buygoldcost = num70,
        buygold = num71,
        sp_cost = num72,
        ct_poi = num73,
        ct_par = num74,
        ct_stu = num75,
        ct_sle = num76,
        st_cha = num77,
        ct_sto = num78,
        ct_bli = num79,
        ct_dsk = num80,
        ct_dmo = num81,
        ct_dat = num82,
        ct_zom = num83,
        ct_dea = num84,
        ct_dkn = num85,
        ct_dbu = num86,
        ct_ddb = num87,
        ct_ber = num88,
        ct_stop = num89,
        ct_fast = num90,
        ct_slow = num91,
        ct_ahe = num92,
        ct_don = num93,
        ct_rag = num94,
        ct_gsl = num95,
        ct_aje = num96,
        ct_dhe = num97,
        ct_dsa = num98,
        ct_daa = num99,
        ct_ddc = num100,
        ct_dic = num101,
        ct_esa = num102,
        ct_das = num103,
        ct_dab = num104,
        ct_dor = num105,
        ct_dft = num106,
        yuragi = num107,
        ct_max = num108,
        ct_wait = num109,
        ct_mov = num110,
        ct_act = num111,
        hit_side = num112,
        hit_back = num113,
        ahhp_rate = num114,
        ahmp_rate = num115,
        gshp_rate = num116,
        gsmp_rate = num117,
        dy_rate = num118,
        zsup_rate = num119,
        beginner_days = num120,
        afcap = num121,
        cmn_pi_fire = str5,
        cmn_pi_water = str6,
        cmn_pi_thunder = str7,
        cmn_pi_wind = str8,
        cmn_pi_shine = str9,
        cmn_pi_dark = str10,
        cmn_pi_all = str11,
        ptnum_nml = num122,
        ptnum_evnt = num123,
        ptnum_mlt = num124,
        ptnum_aatk = num125,
        ptnum_adef = num126,
        ptnum_chq = num127,
        ptnum_tow = num128,
        ptnum_vs = num129,
        ptnum_mt = num130,
        ptnum_ordeal = num131,
        ptnum_raid = num132,
        ptnum_guild_raid = num133,
        ptnum_extra = num134,
        ptnum_gvg = num135,
        ptnum_wr = num136,
        notsus = num137,
        sus_int = num138,
        jobms = num139,
        death_count = num140,
        fast_val = num141,
        slow_val = num142,
        equip_artifact_slot_unlock = numArray5,
        kb_gh = num143,
        th_gh = num144,
        art_rare_pi = strArray2,
        art_cmn_pi = str12,
        soul_rare = strArray3,
        equ_rare_pi = strArray4,
        equ_rare_pi_use = numArray6,
        equ_rare_cost = numArray7,
        aud_max = num145,
        equip_cmn = strArray5,
        ab_rankup_max = num146,
        ab_rankup_addmax = num147,
        ab_coin_convert = num148,
        firstfriend_max = num149,
        firstfriend_coin = num150,
        cmb_rate = num151,
        weak_up = num152,
        resist_dw = num153,
        ordeal_ct = num154,
        esa_assist = num155,
        esa_resist = num156,
        card_sell_mul = num157,
        card_sell_coin_iname = str13,
        card_sell_coin_mul_level = num158,
        card_sell_coin_mul_plus = num159,
        card_exp_mul = num160,
        card_max = num161,
        card_trust_max = num162,
        card_trust_en_bonus = num163,
        card_trust_qe_bonus = num164,
        card_trust_lottery_rate = num165,
        card_awake_unlock_lvcap = num166,
        tobira_lv_cap = num167,
        tobira_unit_lv_cap = num168,
        tobira_unlock_elem = strArray6,
        tobira_unlock_birth = strArray7,
        ini_rec = num169,
        ct_mdh = num170,
        ct_mdm = num171,
        guerrilla_val = num172,
        draft_select_sec = num173,
        draft_organize_sec = num174,
        draft_place_sec = num175,
        guild_create_cost = num176,
        guild_rename_cost = num177,
        guild_emblem_cost = num178,
        guild_invest_limit = num179,
        guild_member_max = num180,
        guild_submaster_max = num181,
        guild_entry_cooltime = num182,
        guild_invest_cooltime = num183,
        convert_rate_piece_element = num184,
        convert_rate_piece_common = num185,
        raid_effective_time = str14,
        mt_skip_cost = num186,
        multi_room_comment_max = num187,
        multi_invite_comment_max = num188,
        insp_skill_lvup_rate = num189,
        insp_skill_slot_max = num190,
        ch_piece_coin_iname = str15,
        quest_reset_cost = numArray8,
        ini_auto_repeat_box = num191,
        auto_repeat_max = num192,
        auto_repeat_cooltime = num193,
        conceptcard_slot2_unlock_tobira = num194,
        conceptcard_slot2_dec_rate = numArray9,
        rune_enh_next_num = num195,
        rune_evo_num = num196,
        rune_storage_init = num197,
        rune_storage_expansion = num198,
        rune_storage_max = num199,
        rune_storage_coin_cost = num200,
        story_ex_total_limit = num201,
        story_ex_total_limit_reset_num = num202,
        story_ex_total_limit_reset_cost = str16,
        wr_dmg_drop_max = num203
      };
    }
  }
}
