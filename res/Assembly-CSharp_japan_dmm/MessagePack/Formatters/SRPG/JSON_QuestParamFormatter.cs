// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.JSON_QuestParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class JSON_QuestParamFormatter : 
    IMessagePackFormatter<JSON_QuestParam>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public JSON_QuestParamFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "iname",
          0
        },
        {
          "title",
          1
        },
        {
          "name",
          2
        },
        {
          "expr",
          3
        },
        {
          "cond",
          4
        },
        {
          "mission",
          5
        },
        {
          "tower_mission",
          6
        },
        {
          "world",
          7
        },
        {
          "area",
          8
        },
        {
          "youbi",
          9
        },
        {
          "time",
          10
        },
        {
          "start",
          11
        },
        {
          "end",
          12
        },
        {
          "cond_quests",
          13
        },
        {
          "units",
          14
        },
        {
          "type",
          15
        },
        {
          "subtype",
          16
        },
        {
          "mode",
          17
        },
        {
          "pt",
          18
        },
        {
          "pexp",
          19
        },
        {
          "uexp",
          20
        },
        {
          "gold",
          21
        },
        {
          "mcoin",
          22
        },
        {
          "ctw",
          23
        },
        {
          "ctl",
          24
        },
        {
          "win",
          25
        },
        {
          "lose",
          26
        },
        {
          "lv",
          27
        },
        {
          "multi",
          28
        },
        {
          "multi_dead",
          29
        },
        {
          "map",
          30
        },
        {
          "evst",
          31
        },
        {
          "evw",
          32
        },
        {
          "pnum",
          33
        },
        {
          "unum",
          34
        },
        {
          "swin",
          35
        },
        {
          "aplv",
          36
        },
        {
          "limit",
          37
        },
        {
          "dayreset",
          38
        },
        {
          "hide",
          39
        },
        {
          "replay_limit",
          40
        },
        {
          "key_limit",
          41
        },
        {
          "ticket",
          42
        },
        {
          "not_search",
          43
        },
        {
          "retr",
          44
        },
        {
          "naut",
          45
        },
        {
          "text",
          46
        },
        {
          "nav",
          47
        },
        {
          "ajob",
          48
        },
        {
          "atag",
          49
        },
        {
          "phyb",
          50
        },
        {
          "magb",
          51
        },
        {
          "bgnr",
          52
        },
        {
          "i_lyt",
          53
        },
        {
          "atk_mag",
          54
        },
        {
          "rdy_cnd",
          55
        },
        {
          "notabl",
          56
        },
        {
          "notitm",
          57
        },
        {
          "rdy_cnd_ch",
          58
        },
        {
          "notcon",
          59
        },
        {
          "fix_editor",
          60
        },
        {
          "is_no_start_voice",
          61
        },
        {
          "sprt",
          62
        },
        {
          "thumnail",
          63
        },
        {
          "mskill",
          64
        },
        {
          "vsmovecnt",
          65
        },
        {
          "dmg_up_pl",
          66
        },
        {
          "dmg_up_en",
          67
        },
        {
          "dmg_rt_pl",
          68
        },
        {
          "dmg_rt_en",
          69
        },
        {
          "review",
          70
        },
        {
          "is_unit_chg",
          71
        },
        {
          "extra",
          72
        },
        {
          "is_multileader",
          73
        },
        {
          "me_id",
          74
        },
        {
          "is_wth_no_chg",
          75
        },
        {
          "wth_set_id",
          76
        },
        {
          "fclr_items",
          77
        },
        {
          "party_id",
          78
        },
        {
          "gen_ui_index",
          79
        },
        {
          "reset_item",
          80
        },
        {
          "reset_max",
          81
        },
        {
          "reset_cost",
          82
        },
        {
          "open_unit",
          83
        },
        {
          "is_auto_repeat_quest",
          84
        }
      };
      this.____stringByteKeys = new byte[85][]
      {
        MessagePackBinary.GetEncodedStringBytes("iname"),
        MessagePackBinary.GetEncodedStringBytes("title"),
        MessagePackBinary.GetEncodedStringBytes("name"),
        MessagePackBinary.GetEncodedStringBytes("expr"),
        MessagePackBinary.GetEncodedStringBytes("cond"),
        MessagePackBinary.GetEncodedStringBytes("mission"),
        MessagePackBinary.GetEncodedStringBytes("tower_mission"),
        MessagePackBinary.GetEncodedStringBytes("world"),
        MessagePackBinary.GetEncodedStringBytes("area"),
        MessagePackBinary.GetEncodedStringBytes("youbi"),
        MessagePackBinary.GetEncodedStringBytes("time"),
        MessagePackBinary.GetEncodedStringBytes("start"),
        MessagePackBinary.GetEncodedStringBytes("end"),
        MessagePackBinary.GetEncodedStringBytes("cond_quests"),
        MessagePackBinary.GetEncodedStringBytes("units"),
        MessagePackBinary.GetEncodedStringBytes("type"),
        MessagePackBinary.GetEncodedStringBytes("subtype"),
        MessagePackBinary.GetEncodedStringBytes("mode"),
        MessagePackBinary.GetEncodedStringBytes("pt"),
        MessagePackBinary.GetEncodedStringBytes("pexp"),
        MessagePackBinary.GetEncodedStringBytes("uexp"),
        MessagePackBinary.GetEncodedStringBytes("gold"),
        MessagePackBinary.GetEncodedStringBytes("mcoin"),
        MessagePackBinary.GetEncodedStringBytes("ctw"),
        MessagePackBinary.GetEncodedStringBytes("ctl"),
        MessagePackBinary.GetEncodedStringBytes("win"),
        MessagePackBinary.GetEncodedStringBytes("lose"),
        MessagePackBinary.GetEncodedStringBytes("lv"),
        MessagePackBinary.GetEncodedStringBytes("multi"),
        MessagePackBinary.GetEncodedStringBytes("multi_dead"),
        MessagePackBinary.GetEncodedStringBytes("map"),
        MessagePackBinary.GetEncodedStringBytes("evst"),
        MessagePackBinary.GetEncodedStringBytes("evw"),
        MessagePackBinary.GetEncodedStringBytes("pnum"),
        MessagePackBinary.GetEncodedStringBytes("unum"),
        MessagePackBinary.GetEncodedStringBytes("swin"),
        MessagePackBinary.GetEncodedStringBytes("aplv"),
        MessagePackBinary.GetEncodedStringBytes("limit"),
        MessagePackBinary.GetEncodedStringBytes("dayreset"),
        MessagePackBinary.GetEncodedStringBytes("hide"),
        MessagePackBinary.GetEncodedStringBytes("replay_limit"),
        MessagePackBinary.GetEncodedStringBytes("key_limit"),
        MessagePackBinary.GetEncodedStringBytes("ticket"),
        MessagePackBinary.GetEncodedStringBytes("not_search"),
        MessagePackBinary.GetEncodedStringBytes("retr"),
        MessagePackBinary.GetEncodedStringBytes("naut"),
        MessagePackBinary.GetEncodedStringBytes("text"),
        MessagePackBinary.GetEncodedStringBytes("nav"),
        MessagePackBinary.GetEncodedStringBytes("ajob"),
        MessagePackBinary.GetEncodedStringBytes("atag"),
        MessagePackBinary.GetEncodedStringBytes("phyb"),
        MessagePackBinary.GetEncodedStringBytes("magb"),
        MessagePackBinary.GetEncodedStringBytes("bgnr"),
        MessagePackBinary.GetEncodedStringBytes("i_lyt"),
        MessagePackBinary.GetEncodedStringBytes("atk_mag"),
        MessagePackBinary.GetEncodedStringBytes("rdy_cnd"),
        MessagePackBinary.GetEncodedStringBytes("notabl"),
        MessagePackBinary.GetEncodedStringBytes("notitm"),
        MessagePackBinary.GetEncodedStringBytes("rdy_cnd_ch"),
        MessagePackBinary.GetEncodedStringBytes("notcon"),
        MessagePackBinary.GetEncodedStringBytes("fix_editor"),
        MessagePackBinary.GetEncodedStringBytes("is_no_start_voice"),
        MessagePackBinary.GetEncodedStringBytes("sprt"),
        MessagePackBinary.GetEncodedStringBytes("thumnail"),
        MessagePackBinary.GetEncodedStringBytes("mskill"),
        MessagePackBinary.GetEncodedStringBytes("vsmovecnt"),
        MessagePackBinary.GetEncodedStringBytes("dmg_up_pl"),
        MessagePackBinary.GetEncodedStringBytes("dmg_up_en"),
        MessagePackBinary.GetEncodedStringBytes("dmg_rt_pl"),
        MessagePackBinary.GetEncodedStringBytes("dmg_rt_en"),
        MessagePackBinary.GetEncodedStringBytes("review"),
        MessagePackBinary.GetEncodedStringBytes("is_unit_chg"),
        MessagePackBinary.GetEncodedStringBytes("extra"),
        MessagePackBinary.GetEncodedStringBytes("is_multileader"),
        MessagePackBinary.GetEncodedStringBytes("me_id"),
        MessagePackBinary.GetEncodedStringBytes("is_wth_no_chg"),
        MessagePackBinary.GetEncodedStringBytes("wth_set_id"),
        MessagePackBinary.GetEncodedStringBytes("fclr_items"),
        MessagePackBinary.GetEncodedStringBytes("party_id"),
        MessagePackBinary.GetEncodedStringBytes("gen_ui_index"),
        MessagePackBinary.GetEncodedStringBytes("reset_item"),
        MessagePackBinary.GetEncodedStringBytes("reset_max"),
        MessagePackBinary.GetEncodedStringBytes("reset_cost"),
        MessagePackBinary.GetEncodedStringBytes("open_unit"),
        MessagePackBinary.GetEncodedStringBytes("is_auto_repeat_quest")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      JSON_QuestParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, 85);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.title, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.name, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.expr, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.cond, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.mission, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.tower_mission, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.world, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.area, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.youbi, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.time, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.start, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.end, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.cond_quests, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.units, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.type);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[16]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.subtype);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[17]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mode);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[18]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.pt);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[19]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.pexp);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[20]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.uexp);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[21]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.gold);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[22]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mcoin);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[23]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ctw);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[24]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ctl);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[25]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.win);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[26]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.lose);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[27]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.lv);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[28]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.multi);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[29]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.multi_dead);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[30]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_MapParam[]>().Serialize(ref bytes, offset, value.map, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[31]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.evst, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[32]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.evw, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[33]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.pnum);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[34]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.unum);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[35]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.swin);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[36]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.aplv);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[37]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.limit);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[38]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.dayreset);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[39]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.hide);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[40]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.replay_limit);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[41]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.key_limit);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[42]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.ticket, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[43]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.not_search);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[44]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.retr);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[45]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.naut);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[46]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.text, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[47]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.nav, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[48]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.ajob, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[49]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.atag, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[50]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.phyb);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[51]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.magb);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[52]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.bgnr);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[53]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.i_lyt, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[54]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.atk_mag, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[55]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.rdy_cnd, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[56]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.notabl);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[57]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.notitm);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[58]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.rdy_cnd_ch, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[59]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.notcon);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[60]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.fix_editor);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[61]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.is_no_start_voice);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[62]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.sprt);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[63]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.thumnail, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[64]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.mskill, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[65]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.vsmovecnt);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[66]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.dmg_up_pl);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[67]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.dmg_up_en);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[68]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.dmg_rt_pl);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[69]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.dmg_rt_en);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[70]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.review);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[71]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.is_unit_chg);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[72]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.extra);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[73]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.is_multileader);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[74]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.me_id, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[75]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.is_wth_no_chg);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[76]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.wth_set_id, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[77]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.fclr_items, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[78]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.party_id, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[79]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.gen_ui_index);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[80]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.reset_item, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[81]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.reset_max);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[82]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.reset_cost, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[83]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.open_unit, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[84]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.is_auto_repeat_quest);
      return offset - num;
    }

    public JSON_QuestParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (JSON_QuestParam) null;
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
      string str9 = (string) null;
      string str10 = (string) null;
      string str11 = (string) null;
      string str12 = (string) null;
      string str13 = (string) null;
      string[] strArray1 = (string[]) null;
      string[] strArray2 = (string[]) null;
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
      JSON_MapParam[] jsonMapParamArray = (JSON_MapParam[]) null;
      string str14 = (string) null;
      string str15 = (string) null;
      int num18 = 0;
      int num19 = 0;
      int num20 = 0;
      int num21 = 0;
      int num22 = 0;
      int num23 = 0;
      int num24 = 0;
      int num25 = 0;
      int num26 = 0;
      string str16 = (string) null;
      int num27 = 0;
      int num28 = 0;
      int num29 = 0;
      string str17 = (string) null;
      string str18 = (string) null;
      string str19 = (string) null;
      string str20 = (string) null;
      int num30 = 0;
      int num31 = 0;
      int num32 = 0;
      string str21 = (string) null;
      string str22 = (string) null;
      string str23 = (string) null;
      int num33 = 0;
      int num34 = 0;
      string str24 = (string) null;
      int num35 = 0;
      int num36 = 0;
      int num37 = 0;
      int num38 = 0;
      string str25 = (string) null;
      string str26 = (string) null;
      int num39 = 0;
      int num40 = 0;
      int num41 = 0;
      int num42 = 0;
      int num43 = 0;
      int num44 = 0;
      int num45 = 0;
      int num46 = 0;
      int num47 = 0;
      string str27 = (string) null;
      int num48 = 0;
      string str28 = (string) null;
      string[] strArray3 = (string[]) null;
      string str29 = (string) null;
      int num49 = 0;
      string str30 = (string) null;
      int num50 = 0;
      string str31 = (string) null;
      string str32 = (string) null;
      int num51 = 0;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num52;
        if (!this.____keyMapping.TryGetValueSafe(key, out num52))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num52)
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
              str9 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 9:
              str10 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 10:
              str11 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 11:
              str12 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 12:
              str13 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 13:
              strArray1 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 14:
              strArray2 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 15:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 16:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 17:
              num5 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 18:
              num6 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 19:
              num7 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 20:
              num8 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 21:
              num9 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 22:
              num10 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 23:
              num11 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 24:
              num12 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 25:
              num13 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 26:
              num14 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 27:
              num15 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 28:
              num16 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 29:
              num17 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 30:
              jsonMapParamArray = formatterResolver.GetFormatterWithVerify<JSON_MapParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 31:
              str14 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 32:
              str15 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 33:
              num18 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 34:
              num19 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 35:
              num20 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 36:
              num21 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 37:
              num22 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 38:
              num23 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 39:
              num24 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 40:
              num25 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 41:
              num26 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 42:
              str16 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 43:
              num27 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 44:
              num28 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 45:
              num29 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 46:
              str17 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 47:
              str18 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 48:
              str19 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 49:
              str20 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 50:
              num30 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 51:
              num31 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 52:
              num32 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 53:
              str21 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 54:
              str22 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 55:
              str23 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 56:
              num33 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 57:
              num34 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 58:
              str24 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 59:
              num35 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 60:
              num36 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 61:
              num37 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 62:
              num38 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 63:
              str25 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 64:
              str26 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 65:
              num39 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 66:
              num40 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 67:
              num41 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 68:
              num42 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 69:
              num43 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 70:
              num44 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 71:
              num45 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 72:
              num46 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 73:
              num47 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 74:
              str27 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 75:
              num48 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 76:
              str28 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 77:
              strArray3 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 78:
              str29 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 79:
              num49 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 80:
              str30 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 81:
              num50 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 82:
              str31 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 83:
              str32 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 84:
              num51 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new JSON_QuestParam()
      {
        iname = str1,
        title = str2,
        name = str3,
        expr = str4,
        cond = str5,
        mission = str6,
        tower_mission = str7,
        world = str8,
        area = str9,
        youbi = str10,
        time = str11,
        start = str12,
        end = str13,
        cond_quests = strArray1,
        units = strArray2,
        type = num3,
        subtype = num4,
        mode = num5,
        pt = num6,
        pexp = num7,
        uexp = num8,
        gold = num9,
        mcoin = num10,
        ctw = num11,
        ctl = num12,
        win = num13,
        lose = num14,
        lv = num15,
        multi = num16,
        multi_dead = num17,
        map = jsonMapParamArray,
        evst = str14,
        evw = str15,
        pnum = num18,
        unum = num19,
        swin = num20,
        aplv = num21,
        limit = num22,
        dayreset = num23,
        hide = num24,
        replay_limit = num25,
        key_limit = num26,
        ticket = str16,
        not_search = num27,
        retr = num28,
        naut = num29,
        text = str17,
        nav = str18,
        ajob = str19,
        atag = str20,
        phyb = num30,
        magb = num31,
        bgnr = num32,
        i_lyt = str21,
        atk_mag = str22,
        rdy_cnd = str23,
        notabl = num33,
        notitm = num34,
        rdy_cnd_ch = str24,
        notcon = num35,
        fix_editor = num36,
        is_no_start_voice = num37,
        sprt = num38,
        thumnail = str25,
        mskill = str26,
        vsmovecnt = num39,
        dmg_up_pl = num40,
        dmg_up_en = num41,
        dmg_rt_pl = num42,
        dmg_rt_en = num43,
        review = num44,
        is_unit_chg = num45,
        extra = num46,
        is_multileader = num47,
        me_id = str27,
        is_wth_no_chg = num48,
        wth_set_id = str28,
        fclr_items = strArray3,
        party_id = str29,
        gen_ui_index = num49,
        reset_item = str30,
        reset_max = num50,
        reset_cost = str31,
        open_unit = str32,
        is_auto_repeat_quest = num51
      };
    }
  }
}
