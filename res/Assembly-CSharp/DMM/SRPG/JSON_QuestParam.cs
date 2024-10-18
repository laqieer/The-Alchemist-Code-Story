﻿// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_QuestParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  [Serializable]
  public class JSON_QuestParam
  {
    public string iname;
    public string title;
    public string name;
    public string expr;
    public string cond;
    public string mission;
    public string tower_mission;
    public string world;
    public string area;
    public string youbi;
    public string time;
    public string start;
    public string end;
    public string[] cond_quests;
    public string[] units;
    public int type;
    public int subtype;
    public int mode;
    public int pt;
    public int pexp;
    public int uexp;
    public int gold;
    public int mcoin;
    public int ctw;
    public int ctl;
    public int win;
    public int lose;
    public int lv;
    public int multi;
    public int multi_dead;
    public JSON_MapParam[] map;
    public string evst;
    public string evw;
    public int pnum;
    public int unum;
    public int swin;
    public int aplv;
    public int limit;
    public int dayreset;
    public int hide;
    public int replay_limit;
    public int key_limit;
    public string ticket;
    public int not_search;
    public int retr;
    public int naut;
    public string text;
    public string nav;
    public string ajob;
    public string atag;
    public int phyb;
    public int magb;
    public int bgnr;
    public string i_lyt;
    public string atk_mag;
    public string rdy_cnd;
    public int notabl;
    public int notitm;
    public string rdy_cnd_ch;
    public int notcon;
    public int fix_editor;
    public int is_no_start_voice;
    public int sprt;
    public string thumnail;
    public string mskill;
    public int vsmovecnt;
    public int dmg_up_pl;
    public int dmg_up_en;
    public int dmg_rt_pl;
    public int dmg_rt_en;
    public int review;
    public int is_unit_chg;
    public int extra;
    public int is_multileader;
    public string me_id;
    public int is_wth_no_chg;
    public string wth_set_id;
    public string[] fclr_items;
    public string party_id;
    public int gen_ui_index;
    public string reset_item;
    public int reset_max;
    public string reset_cost;
    public string open_unit;
    public int is_auto_repeat_quest;
  }
}
