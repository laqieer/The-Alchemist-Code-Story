// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.JSON_UnitParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class JSON_UnitParamFormatter : 
    IMessagePackFormatter<JSON_UnitParam>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public JSON_UnitParamFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "no",
          0
        },
        {
          "iname",
          1
        },
        {
          "name",
          2
        },
        {
          "ai",
          3
        },
        {
          "mdl",
          4
        },
        {
          "tag",
          5
        },
        {
          "grow",
          6
        },
        {
          "jobsets",
          7
        },
        {
          "piece",
          8
        },
        {
          "sub_piece",
          9
        },
        {
          "birth",
          10
        },
        {
          "birth_id",
          11
        },
        {
          "skill",
          12
        },
        {
          "ability",
          13
        },
        {
          "ma_quest",
          14
        },
        {
          "sw",
          15
        },
        {
          "sh",
          16
        },
        {
          "sd",
          17
        },
        {
          "sex",
          18
        },
        {
          "rare",
          19
        },
        {
          "raremax",
          20
        },
        {
          "type",
          21
        },
        {
          "elem",
          22
        },
        {
          "hero",
          23
        },
        {
          "search",
          24
        },
        {
          "stop",
          25
        },
        {
          "notsmn",
          26
        },
        {
          "available_at",
          27
        },
        {
          "height",
          28
        },
        {
          "weight",
          29
        },
        {
          "hp",
          30
        },
        {
          "mp",
          31
        },
        {
          "atk",
          32
        },
        {
          "def",
          33
        },
        {
          "mag",
          34
        },
        {
          "mnd",
          35
        },
        {
          "dex",
          36
        },
        {
          "spd",
          37
        },
        {
          "cri",
          38
        },
        {
          "luk",
          39
        },
        {
          "apo",
          40
        },
        {
          "apa",
          41
        },
        {
          "ast",
          42
        },
        {
          "asl",
          43
        },
        {
          "ach",
          44
        },
        {
          "asn",
          45
        },
        {
          "abl",
          46
        },
        {
          "ans",
          47
        },
        {
          "anm",
          48
        },
        {
          "ana",
          49
        },
        {
          "azo",
          50
        },
        {
          "ade",
          51
        },
        {
          "akn",
          52
        },
        {
          "rpo",
          53
        },
        {
          "rpa",
          54
        },
        {
          "rst",
          55
        },
        {
          "rsl",
          56
        },
        {
          "rch",
          57
        },
        {
          "rsn",
          58
        },
        {
          "rbl",
          59
        },
        {
          "rns",
          60
        },
        {
          "rnm",
          61
        },
        {
          "rna",
          62
        },
        {
          "rzo",
          63
        },
        {
          "rde",
          64
        },
        {
          "rkn",
          65
        },
        {
          "rdf",
          66
        },
        {
          "rbe",
          67
        },
        {
          "rcs",
          68
        },
        {
          "rcu",
          69
        },
        {
          "rcd",
          70
        },
        {
          "rdo",
          71
        },
        {
          "rra",
          72
        },
        {
          "rsa",
          73
        },
        {
          "raa",
          74
        },
        {
          "rdc",
          75
        },
        {
          "ric",
          76
        },
        {
          "ras",
          77
        },
        {
          "rab",
          78
        },
        {
          "ror",
          79
        },
        {
          "rft",
          80
        },
        {
          "mhp",
          81
        },
        {
          "mmp",
          82
        },
        {
          "matk",
          83
        },
        {
          "mdef",
          84
        },
        {
          "mmag",
          85
        },
        {
          "mmnd",
          86
        },
        {
          "mdex",
          87
        },
        {
          "mspd",
          88
        },
        {
          "mcri",
          89
        },
        {
          "mluk",
          90
        },
        {
          "mapo",
          91
        },
        {
          "mapa",
          92
        },
        {
          "mast",
          93
        },
        {
          "masl",
          94
        },
        {
          "mach",
          95
        },
        {
          "masn",
          96
        },
        {
          "mabl",
          97
        },
        {
          "mans",
          98
        },
        {
          "manm",
          99
        },
        {
          "mana",
          100
        },
        {
          "mazo",
          101
        },
        {
          "made",
          102
        },
        {
          "makn",
          103
        },
        {
          "mrpo",
          104
        },
        {
          "mrpa",
          105
        },
        {
          "mrst",
          106
        },
        {
          "mrsl",
          107
        },
        {
          "mrch",
          108
        },
        {
          "mrsn",
          109
        },
        {
          "mrbl",
          110
        },
        {
          "mrns",
          111
        },
        {
          "mrnm",
          112
        },
        {
          "mrna",
          113
        },
        {
          "mrzo",
          114
        },
        {
          "mrde",
          115
        },
        {
          "mrkn",
          116
        },
        {
          "mrdf",
          117
        },
        {
          "mrbe",
          118
        },
        {
          "mrcs",
          119
        },
        {
          "mrcu",
          120
        },
        {
          "mrcd",
          121
        },
        {
          "mrdo",
          122
        },
        {
          "mrra",
          123
        },
        {
          "mrsa",
          124
        },
        {
          "mraa",
          125
        },
        {
          "mrdc",
          126
        },
        {
          "mric",
          (int) sbyte.MaxValue
        },
        {
          "mras",
          128
        },
        {
          "mrab",
          129
        },
        {
          "mror",
          130
        },
        {
          "mrft",
          131
        },
        {
          "ls1",
          132
        },
        {
          "ls2",
          133
        },
        {
          "ls3",
          134
        },
        {
          "ls4",
          135
        },
        {
          "ls5",
          136
        },
        {
          "ls6",
          137
        },
        {
          "recipe1",
          138
        },
        {
          "recipe2",
          139
        },
        {
          "recipe3",
          140
        },
        {
          "recipe4",
          141
        },
        {
          "recipe5",
          142
        },
        {
          "recipe6",
          143
        },
        {
          "img",
          144
        },
        {
          "vce",
          145
        },
        {
          "dskl",
          146
        },
        {
          "dabi",
          147
        },
        {
          "djob",
          148
        },
        {
          "dbuki",
          149
        },
        {
          "jt",
          150
        },
        {
          "role",
          151
        },
        {
          "mov",
          152
        },
        {
          "jmp",
          153
        },
        {
          "inimp",
          154
        },
        {
          "ma_rarity",
          155
        },
        {
          "ma_lv",
          156
        },
        {
          "skins",
          157
        },
        {
          "jidx",
          158
        },
        {
          "jimgs",
          159
        },
        {
          "jvcs",
          160
        },
        {
          "no_trw",
          161
        },
        {
          "no_kb",
          162
        },
        {
          "no_chg",
          163
        },
        {
          "unlck_t",
          164
        },
        {
          "no_insp",
          165
        },
        {
          "no_recommended",
          166
        },
        {
          "unit_piece_shop_group",
          167
        },
        {
          "no_pass",
          168
        }
      };
      this.____stringByteKeys = new byte[169][]
      {
        MessagePackBinary.GetEncodedStringBytes("no"),
        MessagePackBinary.GetEncodedStringBytes("iname"),
        MessagePackBinary.GetEncodedStringBytes("name"),
        MessagePackBinary.GetEncodedStringBytes("ai"),
        MessagePackBinary.GetEncodedStringBytes("mdl"),
        MessagePackBinary.GetEncodedStringBytes("tag"),
        MessagePackBinary.GetEncodedStringBytes("grow"),
        MessagePackBinary.GetEncodedStringBytes("jobsets"),
        MessagePackBinary.GetEncodedStringBytes("piece"),
        MessagePackBinary.GetEncodedStringBytes("sub_piece"),
        MessagePackBinary.GetEncodedStringBytes("birth"),
        MessagePackBinary.GetEncodedStringBytes("birth_id"),
        MessagePackBinary.GetEncodedStringBytes("skill"),
        MessagePackBinary.GetEncodedStringBytes("ability"),
        MessagePackBinary.GetEncodedStringBytes("ma_quest"),
        MessagePackBinary.GetEncodedStringBytes("sw"),
        MessagePackBinary.GetEncodedStringBytes("sh"),
        MessagePackBinary.GetEncodedStringBytes("sd"),
        MessagePackBinary.GetEncodedStringBytes("sex"),
        MessagePackBinary.GetEncodedStringBytes("rare"),
        MessagePackBinary.GetEncodedStringBytes("raremax"),
        MessagePackBinary.GetEncodedStringBytes("type"),
        MessagePackBinary.GetEncodedStringBytes("elem"),
        MessagePackBinary.GetEncodedStringBytes("hero"),
        MessagePackBinary.GetEncodedStringBytes("search"),
        MessagePackBinary.GetEncodedStringBytes("stop"),
        MessagePackBinary.GetEncodedStringBytes("notsmn"),
        MessagePackBinary.GetEncodedStringBytes("available_at"),
        MessagePackBinary.GetEncodedStringBytes("height"),
        MessagePackBinary.GetEncodedStringBytes("weight"),
        MessagePackBinary.GetEncodedStringBytes("hp"),
        MessagePackBinary.GetEncodedStringBytes("mp"),
        MessagePackBinary.GetEncodedStringBytes("atk"),
        MessagePackBinary.GetEncodedStringBytes("def"),
        MessagePackBinary.GetEncodedStringBytes("mag"),
        MessagePackBinary.GetEncodedStringBytes("mnd"),
        MessagePackBinary.GetEncodedStringBytes("dex"),
        MessagePackBinary.GetEncodedStringBytes("spd"),
        MessagePackBinary.GetEncodedStringBytes("cri"),
        MessagePackBinary.GetEncodedStringBytes("luk"),
        MessagePackBinary.GetEncodedStringBytes("apo"),
        MessagePackBinary.GetEncodedStringBytes("apa"),
        MessagePackBinary.GetEncodedStringBytes("ast"),
        MessagePackBinary.GetEncodedStringBytes("asl"),
        MessagePackBinary.GetEncodedStringBytes("ach"),
        MessagePackBinary.GetEncodedStringBytes("asn"),
        MessagePackBinary.GetEncodedStringBytes("abl"),
        MessagePackBinary.GetEncodedStringBytes("ans"),
        MessagePackBinary.GetEncodedStringBytes("anm"),
        MessagePackBinary.GetEncodedStringBytes("ana"),
        MessagePackBinary.GetEncodedStringBytes("azo"),
        MessagePackBinary.GetEncodedStringBytes("ade"),
        MessagePackBinary.GetEncodedStringBytes("akn"),
        MessagePackBinary.GetEncodedStringBytes("rpo"),
        MessagePackBinary.GetEncodedStringBytes("rpa"),
        MessagePackBinary.GetEncodedStringBytes("rst"),
        MessagePackBinary.GetEncodedStringBytes("rsl"),
        MessagePackBinary.GetEncodedStringBytes("rch"),
        MessagePackBinary.GetEncodedStringBytes("rsn"),
        MessagePackBinary.GetEncodedStringBytes("rbl"),
        MessagePackBinary.GetEncodedStringBytes("rns"),
        MessagePackBinary.GetEncodedStringBytes("rnm"),
        MessagePackBinary.GetEncodedStringBytes("rna"),
        MessagePackBinary.GetEncodedStringBytes("rzo"),
        MessagePackBinary.GetEncodedStringBytes("rde"),
        MessagePackBinary.GetEncodedStringBytes("rkn"),
        MessagePackBinary.GetEncodedStringBytes("rdf"),
        MessagePackBinary.GetEncodedStringBytes("rbe"),
        MessagePackBinary.GetEncodedStringBytes("rcs"),
        MessagePackBinary.GetEncodedStringBytes("rcu"),
        MessagePackBinary.GetEncodedStringBytes("rcd"),
        MessagePackBinary.GetEncodedStringBytes("rdo"),
        MessagePackBinary.GetEncodedStringBytes("rra"),
        MessagePackBinary.GetEncodedStringBytes("rsa"),
        MessagePackBinary.GetEncodedStringBytes("raa"),
        MessagePackBinary.GetEncodedStringBytes("rdc"),
        MessagePackBinary.GetEncodedStringBytes("ric"),
        MessagePackBinary.GetEncodedStringBytes("ras"),
        MessagePackBinary.GetEncodedStringBytes("rab"),
        MessagePackBinary.GetEncodedStringBytes("ror"),
        MessagePackBinary.GetEncodedStringBytes("rft"),
        MessagePackBinary.GetEncodedStringBytes("mhp"),
        MessagePackBinary.GetEncodedStringBytes("mmp"),
        MessagePackBinary.GetEncodedStringBytes("matk"),
        MessagePackBinary.GetEncodedStringBytes("mdef"),
        MessagePackBinary.GetEncodedStringBytes("mmag"),
        MessagePackBinary.GetEncodedStringBytes("mmnd"),
        MessagePackBinary.GetEncodedStringBytes("mdex"),
        MessagePackBinary.GetEncodedStringBytes("mspd"),
        MessagePackBinary.GetEncodedStringBytes("mcri"),
        MessagePackBinary.GetEncodedStringBytes("mluk"),
        MessagePackBinary.GetEncodedStringBytes("mapo"),
        MessagePackBinary.GetEncodedStringBytes("mapa"),
        MessagePackBinary.GetEncodedStringBytes("mast"),
        MessagePackBinary.GetEncodedStringBytes("masl"),
        MessagePackBinary.GetEncodedStringBytes("mach"),
        MessagePackBinary.GetEncodedStringBytes("masn"),
        MessagePackBinary.GetEncodedStringBytes("mabl"),
        MessagePackBinary.GetEncodedStringBytes("mans"),
        MessagePackBinary.GetEncodedStringBytes("manm"),
        MessagePackBinary.GetEncodedStringBytes("mana"),
        MessagePackBinary.GetEncodedStringBytes("mazo"),
        MessagePackBinary.GetEncodedStringBytes("made"),
        MessagePackBinary.GetEncodedStringBytes("makn"),
        MessagePackBinary.GetEncodedStringBytes("mrpo"),
        MessagePackBinary.GetEncodedStringBytes("mrpa"),
        MessagePackBinary.GetEncodedStringBytes("mrst"),
        MessagePackBinary.GetEncodedStringBytes("mrsl"),
        MessagePackBinary.GetEncodedStringBytes("mrch"),
        MessagePackBinary.GetEncodedStringBytes("mrsn"),
        MessagePackBinary.GetEncodedStringBytes("mrbl"),
        MessagePackBinary.GetEncodedStringBytes("mrns"),
        MessagePackBinary.GetEncodedStringBytes("mrnm"),
        MessagePackBinary.GetEncodedStringBytes("mrna"),
        MessagePackBinary.GetEncodedStringBytes("mrzo"),
        MessagePackBinary.GetEncodedStringBytes("mrde"),
        MessagePackBinary.GetEncodedStringBytes("mrkn"),
        MessagePackBinary.GetEncodedStringBytes("mrdf"),
        MessagePackBinary.GetEncodedStringBytes("mrbe"),
        MessagePackBinary.GetEncodedStringBytes("mrcs"),
        MessagePackBinary.GetEncodedStringBytes("mrcu"),
        MessagePackBinary.GetEncodedStringBytes("mrcd"),
        MessagePackBinary.GetEncodedStringBytes("mrdo"),
        MessagePackBinary.GetEncodedStringBytes("mrra"),
        MessagePackBinary.GetEncodedStringBytes("mrsa"),
        MessagePackBinary.GetEncodedStringBytes("mraa"),
        MessagePackBinary.GetEncodedStringBytes("mrdc"),
        MessagePackBinary.GetEncodedStringBytes("mric"),
        MessagePackBinary.GetEncodedStringBytes("mras"),
        MessagePackBinary.GetEncodedStringBytes("mrab"),
        MessagePackBinary.GetEncodedStringBytes("mror"),
        MessagePackBinary.GetEncodedStringBytes("mrft"),
        MessagePackBinary.GetEncodedStringBytes("ls1"),
        MessagePackBinary.GetEncodedStringBytes("ls2"),
        MessagePackBinary.GetEncodedStringBytes("ls3"),
        MessagePackBinary.GetEncodedStringBytes("ls4"),
        MessagePackBinary.GetEncodedStringBytes("ls5"),
        MessagePackBinary.GetEncodedStringBytes("ls6"),
        MessagePackBinary.GetEncodedStringBytes("recipe1"),
        MessagePackBinary.GetEncodedStringBytes("recipe2"),
        MessagePackBinary.GetEncodedStringBytes("recipe3"),
        MessagePackBinary.GetEncodedStringBytes("recipe4"),
        MessagePackBinary.GetEncodedStringBytes("recipe5"),
        MessagePackBinary.GetEncodedStringBytes("recipe6"),
        MessagePackBinary.GetEncodedStringBytes("img"),
        MessagePackBinary.GetEncodedStringBytes("vce"),
        MessagePackBinary.GetEncodedStringBytes("dskl"),
        MessagePackBinary.GetEncodedStringBytes("dabi"),
        MessagePackBinary.GetEncodedStringBytes("djob"),
        MessagePackBinary.GetEncodedStringBytes("dbuki"),
        MessagePackBinary.GetEncodedStringBytes("jt"),
        MessagePackBinary.GetEncodedStringBytes("role"),
        MessagePackBinary.GetEncodedStringBytes("mov"),
        MessagePackBinary.GetEncodedStringBytes("jmp"),
        MessagePackBinary.GetEncodedStringBytes("inimp"),
        MessagePackBinary.GetEncodedStringBytes("ma_rarity"),
        MessagePackBinary.GetEncodedStringBytes("ma_lv"),
        MessagePackBinary.GetEncodedStringBytes("skins"),
        MessagePackBinary.GetEncodedStringBytes("jidx"),
        MessagePackBinary.GetEncodedStringBytes("jimgs"),
        MessagePackBinary.GetEncodedStringBytes("jvcs"),
        MessagePackBinary.GetEncodedStringBytes("no_trw"),
        MessagePackBinary.GetEncodedStringBytes("no_kb"),
        MessagePackBinary.GetEncodedStringBytes("no_chg"),
        MessagePackBinary.GetEncodedStringBytes("unlck_t"),
        MessagePackBinary.GetEncodedStringBytes("no_insp"),
        MessagePackBinary.GetEncodedStringBytes("no_recommended"),
        MessagePackBinary.GetEncodedStringBytes("unit_piece_shop_group"),
        MessagePackBinary.GetEncodedStringBytes("no_pass")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      JSON_UnitParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, 169);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.no);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.name, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.ai, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.mdl, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.tag, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.grow, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.jobsets, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.piece, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.sub_piece, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.birth, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.birth_id);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.skill, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.ability, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.ma_quest, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.sw);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[16]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.sh);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[17]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.sd);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[18]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.sex);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[19]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rare);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[20]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.raremax);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[21]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.type);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[22]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.elem);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[23]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.hero);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[24]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.search);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[25]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.stop);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[26]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.notsmn);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[27]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.available_at, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[28]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.height);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[29]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.weight);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[30]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.hp);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[31]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mp);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[32]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.atk);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[33]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.def);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[34]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mag);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[35]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mnd);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[36]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.dex);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[37]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.spd);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[38]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.cri);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[39]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.luk);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[40]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.apo);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[41]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.apa);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[42]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ast);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[43]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.asl);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[44]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ach);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[45]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.asn);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[46]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.abl);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[47]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ans);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[48]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.anm);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[49]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ana);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[50]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.azo);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[51]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ade);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[52]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.akn);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[53]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rpo);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[54]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rpa);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[55]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rst);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[56]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rsl);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[57]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rch);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[58]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rsn);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[59]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rbl);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[60]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rns);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[61]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rnm);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[62]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rna);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[63]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rzo);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[64]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rde);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[65]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rkn);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[66]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rdf);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[67]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rbe);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[68]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rcs);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[69]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rcu);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[70]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rcd);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[71]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rdo);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[72]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rra);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[73]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rsa);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[74]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.raa);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[75]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rdc);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[76]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ric);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[77]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ras);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[78]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rab);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[79]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ror);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[80]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rft);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[81]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mhp);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[82]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mmp);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[83]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.matk);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[84]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mdef);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[85]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mmag);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[86]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mmnd);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[87]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mdex);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[88]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mspd);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[89]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mcri);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[90]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mluk);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[91]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mapo);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[92]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mapa);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[93]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mast);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[94]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.masl);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[95]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mach);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[96]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.masn);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[97]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mabl);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[98]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mans);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[99]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.manm);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[100]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mana);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[101]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mazo);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[102]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.made);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[103]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.makn);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[104]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mrpo);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[105]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mrpa);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[106]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mrst);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[107]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mrsl);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[108]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mrch);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[109]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mrsn);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[110]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mrbl);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[111]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mrns);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[112]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mrnm);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[113]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mrna);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[114]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mrzo);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[115]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mrde);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[116]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mrkn);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[117]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mrdf);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[118]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mrbe);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[119]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mrcs);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[120]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mrcu);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[121]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mrcd);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[122]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mrdo);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[123]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mrra);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[124]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mrsa);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[125]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mraa);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[126]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mrdc);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[(int) sbyte.MaxValue]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mric);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[128]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mras);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[129]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mrab);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[130]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mror);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[131]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mrft);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[132]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.ls1, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[133]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.ls2, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[134]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.ls3, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[135]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.ls4, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[136]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.ls5, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[137]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.ls6, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[138]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.recipe1, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[139]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.recipe2, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[140]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.recipe3, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[141]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.recipe4, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[142]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.recipe5, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[143]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.recipe6, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[144]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.img, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[145]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.vce, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[146]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.dskl, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[147]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.dabi, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[148]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.djob, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[149]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.dbuki, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[150]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.jt);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[151]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.role);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[152]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mov);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[153]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.jmp);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[154]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.inimp);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[155]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ma_rarity);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[156]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ma_lv);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[157]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.skins, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[158]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.jidx, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[159]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.jimgs, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[160]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.jvcs, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[161]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.no_trw);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[162]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.no_kb);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[163]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.no_chg);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[164]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.unlck_t, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[165]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.no_insp);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[166]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.no_recommended);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[167]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.unit_piece_shop_group, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[168]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.no_pass);
      return offset - num;
    }

    public JSON_UnitParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (JSON_UnitParam) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      int num3 = 0;
      string str1 = (string) null;
      string str2 = (string) null;
      string str3 = (string) null;
      string str4 = (string) null;
      string str5 = (string) null;
      string str6 = (string) null;
      string[] strArray1 = (string[]) null;
      string str7 = (string) null;
      string str8 = (string) null;
      string str9 = (string) null;
      int num4 = 0;
      string str10 = (string) null;
      string str11 = (string) null;
      string str12 = (string) null;
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
      string str13 = (string) null;
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
      int num60 = 0;
      int num61 = 0;
      int num62 = 0;
      int num63 = 0;
      int num64 = 0;
      int num65 = 0;
      int num66 = 0;
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
      string str14 = (string) null;
      string str15 = (string) null;
      string str16 = (string) null;
      string str17 = (string) null;
      string str18 = (string) null;
      string str19 = (string) null;
      string str20 = (string) null;
      string str21 = (string) null;
      string str22 = (string) null;
      string str23 = (string) null;
      string str24 = (string) null;
      string str25 = (string) null;
      string str26 = (string) null;
      string str27 = (string) null;
      string str28 = (string) null;
      string[] strArray2 = (string[]) null;
      string str29 = (string) null;
      string str30 = (string) null;
      int num121 = 0;
      int num122 = 0;
      int num123 = 0;
      int num124 = 0;
      int num125 = 0;
      int num126 = 0;
      int num127 = 0;
      string[] strArray3 = (string[]) null;
      string[] strArray4 = (string[]) null;
      string[] strArray5 = (string[]) null;
      string[] strArray6 = (string[]) null;
      int num128 = 0;
      int num129 = 0;
      int num130 = 0;
      string str31 = (string) null;
      int num131 = 0;
      int num132 = 0;
      string str32 = (string) null;
      int num133 = 0;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num134;
        if (!this.____keyMapping.TryGetValueSafe(key, out num134))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num134)
          {
            case 0:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 1:
              str1 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              str2 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              str3 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              str4 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 5:
              str5 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 6:
              str6 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 7:
              strArray1 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 8:
              str7 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 9:
              str8 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 10:
              str9 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 11:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 12:
              str10 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 13:
              str11 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 14:
              str12 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 15:
              num5 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 16:
              num6 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 17:
              num7 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 18:
              num8 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 19:
              num9 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 20:
              num10 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 21:
              num11 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 22:
              num12 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 23:
              num13 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 24:
              num14 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 25:
              num15 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 26:
              num16 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 27:
              str13 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 28:
              num17 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 29:
              num18 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 30:
              num19 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 31:
              num20 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 32:
              num21 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 33:
              num22 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 34:
              num23 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 35:
              num24 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 36:
              num25 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 37:
              num26 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 38:
              num27 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 39:
              num28 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 40:
              num29 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 41:
              num30 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 42:
              num31 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 43:
              num32 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 44:
              num33 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 45:
              num34 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 46:
              num35 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 47:
              num36 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 48:
              num37 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 49:
              num38 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 50:
              num39 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 51:
              num40 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 52:
              num41 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 53:
              num42 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 54:
              num43 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 55:
              num44 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 56:
              num45 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 57:
              num46 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 58:
              num47 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 59:
              num48 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 60:
              num49 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 61:
              num50 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 62:
              num51 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 63:
              num52 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 64:
              num53 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 65:
              num54 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 66:
              num55 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 67:
              num56 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 68:
              num57 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 69:
              num58 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 70:
              num59 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 71:
              num60 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 72:
              num61 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 73:
              num62 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 74:
              num63 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 75:
              num64 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 76:
              num65 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 77:
              num66 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 78:
              num67 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 79:
              num68 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 80:
              num69 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 81:
              num70 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 82:
              num71 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 83:
              num72 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 84:
              num73 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 85:
              num74 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 86:
              num75 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
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
              num83 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 95:
              num84 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 96:
              num85 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 97:
              num86 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 98:
              num87 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 99:
              num88 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 100:
              num89 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 101:
              num90 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 102:
              num91 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 103:
              num92 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 104:
              num93 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 105:
              num94 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 106:
              num95 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 107:
              num96 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 108:
              num97 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 109:
              num98 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 110:
              num99 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 111:
              num100 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 112:
              num101 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 113:
              num102 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 114:
              num103 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 115:
              num104 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 116:
              num105 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 117:
              num106 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 118:
              num107 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 119:
              num108 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 120:
              num109 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 121:
              num110 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 122:
              num111 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 123:
              num112 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 124:
              num113 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 125:
              num114 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 126:
              num115 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case (int) sbyte.MaxValue:
              num116 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 128:
              num117 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 129:
              num118 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 130:
              num119 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 131:
              num120 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 132:
              str14 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 133:
              str15 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 134:
              str16 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 135:
              str17 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 136:
              str18 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 137:
              str19 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 138:
              str20 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 139:
              str21 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 140:
              str22 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 141:
              str23 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 142:
              str24 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 143:
              str25 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 144:
              str26 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 145:
              str27 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 146:
              str28 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 147:
              strArray2 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 148:
              str29 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 149:
              str30 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 150:
              num121 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 151:
              num122 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 152:
              num123 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 153:
              num124 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 154:
              num125 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 155:
              num126 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 156:
              num127 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 157:
              strArray3 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 158:
              strArray4 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 159:
              strArray5 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 160:
              strArray6 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 161:
              num128 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 162:
              num129 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 163:
              num130 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 164:
              str31 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 165:
              num131 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 166:
              num132 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 167:
              str32 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 168:
              num133 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new JSON_UnitParam()
      {
        no = num3,
        iname = str1,
        name = str2,
        ai = str3,
        mdl = str4,
        tag = str5,
        grow = str6,
        jobsets = strArray1,
        piece = str7,
        sub_piece = str8,
        birth = str9,
        birth_id = num4,
        skill = str10,
        ability = str11,
        ma_quest = str12,
        sw = num5,
        sh = num6,
        sd = num7,
        sex = num8,
        rare = num9,
        raremax = num10,
        type = num11,
        elem = num12,
        hero = num13,
        search = num14,
        stop = num15,
        notsmn = num16,
        available_at = str13,
        height = num17,
        weight = num18,
        hp = num19,
        mp = num20,
        atk = num21,
        def = num22,
        mag = num23,
        mnd = num24,
        dex = num25,
        spd = num26,
        cri = num27,
        luk = num28,
        apo = num29,
        apa = num30,
        ast = num31,
        asl = num32,
        ach = num33,
        asn = num34,
        abl = num35,
        ans = num36,
        anm = num37,
        ana = num38,
        azo = num39,
        ade = num40,
        akn = num41,
        rpo = num42,
        rpa = num43,
        rst = num44,
        rsl = num45,
        rch = num46,
        rsn = num47,
        rbl = num48,
        rns = num49,
        rnm = num50,
        rna = num51,
        rzo = num52,
        rde = num53,
        rkn = num54,
        rdf = num55,
        rbe = num56,
        rcs = num57,
        rcu = num58,
        rcd = num59,
        rdo = num60,
        rra = num61,
        rsa = num62,
        raa = num63,
        rdc = num64,
        ric = num65,
        ras = num66,
        rab = num67,
        ror = num68,
        rft = num69,
        mhp = num70,
        mmp = num71,
        matk = num72,
        mdef = num73,
        mmag = num74,
        mmnd = num75,
        mdex = num76,
        mspd = num77,
        mcri = num78,
        mluk = num79,
        mapo = num80,
        mapa = num81,
        mast = num82,
        masl = num83,
        mach = num84,
        masn = num85,
        mabl = num86,
        mans = num87,
        manm = num88,
        mana = num89,
        mazo = num90,
        made = num91,
        makn = num92,
        mrpo = num93,
        mrpa = num94,
        mrst = num95,
        mrsl = num96,
        mrch = num97,
        mrsn = num98,
        mrbl = num99,
        mrns = num100,
        mrnm = num101,
        mrna = num102,
        mrzo = num103,
        mrde = num104,
        mrkn = num105,
        mrdf = num106,
        mrbe = num107,
        mrcs = num108,
        mrcu = num109,
        mrcd = num110,
        mrdo = num111,
        mrra = num112,
        mrsa = num113,
        mraa = num114,
        mrdc = num115,
        mric = num116,
        mras = num117,
        mrab = num118,
        mror = num119,
        mrft = num120,
        ls1 = str14,
        ls2 = str15,
        ls3 = str16,
        ls4 = str17,
        ls5 = str18,
        ls6 = str19,
        recipe1 = str20,
        recipe2 = str21,
        recipe3 = str22,
        recipe4 = str23,
        recipe5 = str24,
        recipe6 = str25,
        img = str26,
        vce = str27,
        dskl = str28,
        dabi = strArray2,
        djob = str29,
        dbuki = str30,
        jt = num121,
        role = num122,
        mov = num123,
        jmp = num124,
        inimp = num125,
        ma_rarity = num126,
        ma_lv = num127,
        skins = strArray3,
        jidx = strArray4,
        jimgs = strArray5,
        jvcs = strArray6,
        no_trw = num128,
        no_kb = num129,
        no_chg = num130,
        unlck_t = str31,
        no_insp = num131,
        no_recommended = num132,
        unit_piece_shop_group = str32,
        no_pass = num133
      };
    }
  }
}
