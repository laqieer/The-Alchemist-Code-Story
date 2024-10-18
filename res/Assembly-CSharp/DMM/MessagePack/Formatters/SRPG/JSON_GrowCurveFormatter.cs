// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.JSON_GrowCurveFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class JSON_GrowCurveFormatter : 
    IMessagePackFormatter<JSON_GrowCurve>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public JSON_GrowCurveFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "lv",
          0
        },
        {
          "hp",
          1
        },
        {
          "mp",
          2
        },
        {
          "atk",
          3
        },
        {
          "def",
          4
        },
        {
          "mag",
          5
        },
        {
          "mnd",
          6
        },
        {
          "dex",
          7
        },
        {
          "spd",
          8
        },
        {
          "cri",
          9
        },
        {
          "luk",
          10
        },
        {
          "afi",
          11
        },
        {
          "awa",
          12
        },
        {
          "awi",
          13
        },
        {
          "ath",
          14
        },
        {
          "ash",
          15
        },
        {
          "ada",
          16
        },
        {
          "apo",
          17
        },
        {
          "apa",
          18
        },
        {
          "ast",
          19
        },
        {
          "asl",
          20
        },
        {
          "ach",
          21
        },
        {
          "asn",
          22
        },
        {
          "abl",
          23
        },
        {
          "ans",
          24
        },
        {
          "anm",
          25
        },
        {
          "ana",
          26
        },
        {
          "azo",
          27
        },
        {
          "ade",
          28
        },
        {
          "abe",
          29
        },
        {
          "akn",
          30
        },
        {
          "abf",
          31
        },
        {
          "adf",
          32
        },
        {
          "acs",
          33
        },
        {
          "acu",
          34
        },
        {
          "acd",
          35
        },
        {
          "ado",
          36
        },
        {
          "ara",
          37
        },
        {
          "adc",
          38
        },
        {
          "aic",
          39
        },
        {
          "rfi",
          40
        },
        {
          "rwa",
          41
        },
        {
          "rwi",
          42
        },
        {
          "rth",
          43
        },
        {
          "rsh",
          44
        },
        {
          "rda",
          45
        },
        {
          "rpo",
          46
        },
        {
          "rpa",
          47
        },
        {
          "rst",
          48
        },
        {
          "rsl",
          49
        },
        {
          "rch",
          50
        },
        {
          "rsn",
          51
        },
        {
          "rbl",
          52
        },
        {
          "rns",
          53
        },
        {
          "rnm",
          54
        },
        {
          "rna",
          55
        },
        {
          "rzo",
          56
        },
        {
          "rde",
          57
        },
        {
          "rbe",
          58
        },
        {
          "rkn",
          59
        },
        {
          "rbf",
          60
        },
        {
          "rdf",
          61
        },
        {
          "rcs",
          62
        },
        {
          "rcu",
          63
        },
        {
          "rcd",
          64
        },
        {
          "rdo",
          65
        },
        {
          "rra",
          66
        },
        {
          "rdc",
          67
        },
        {
          "ric",
          68
        },
        {
          "val",
          69
        }
      };
      this.____stringByteKeys = new byte[70][]
      {
        MessagePackBinary.GetEncodedStringBytes("lv"),
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
        MessagePackBinary.GetEncodedStringBytes("afi"),
        MessagePackBinary.GetEncodedStringBytes("awa"),
        MessagePackBinary.GetEncodedStringBytes("awi"),
        MessagePackBinary.GetEncodedStringBytes("ath"),
        MessagePackBinary.GetEncodedStringBytes("ash"),
        MessagePackBinary.GetEncodedStringBytes("ada"),
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
        MessagePackBinary.GetEncodedStringBytes("abe"),
        MessagePackBinary.GetEncodedStringBytes("akn"),
        MessagePackBinary.GetEncodedStringBytes("abf"),
        MessagePackBinary.GetEncodedStringBytes("adf"),
        MessagePackBinary.GetEncodedStringBytes("acs"),
        MessagePackBinary.GetEncodedStringBytes("acu"),
        MessagePackBinary.GetEncodedStringBytes("acd"),
        MessagePackBinary.GetEncodedStringBytes("ado"),
        MessagePackBinary.GetEncodedStringBytes("ara"),
        MessagePackBinary.GetEncodedStringBytes("adc"),
        MessagePackBinary.GetEncodedStringBytes("aic"),
        MessagePackBinary.GetEncodedStringBytes("rfi"),
        MessagePackBinary.GetEncodedStringBytes("rwa"),
        MessagePackBinary.GetEncodedStringBytes("rwi"),
        MessagePackBinary.GetEncodedStringBytes("rth"),
        MessagePackBinary.GetEncodedStringBytes("rsh"),
        MessagePackBinary.GetEncodedStringBytes("rda"),
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
        MessagePackBinary.GetEncodedStringBytes("rbe"),
        MessagePackBinary.GetEncodedStringBytes("rkn"),
        MessagePackBinary.GetEncodedStringBytes("rbf"),
        MessagePackBinary.GetEncodedStringBytes("rdf"),
        MessagePackBinary.GetEncodedStringBytes("rcs"),
        MessagePackBinary.GetEncodedStringBytes("rcu"),
        MessagePackBinary.GetEncodedStringBytes("rcd"),
        MessagePackBinary.GetEncodedStringBytes("rdo"),
        MessagePackBinary.GetEncodedStringBytes("rra"),
        MessagePackBinary.GetEncodedStringBytes("rdc"),
        MessagePackBinary.GetEncodedStringBytes("ric"),
        MessagePackBinary.GetEncodedStringBytes("val")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      JSON_GrowCurve value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, 70);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.lv);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.hp);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mp);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.atk);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.def);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mag);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mnd);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.dex);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.spd);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.cri);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.luk);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.afi);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.awa);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.awi);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ath);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ash);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[16]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ada);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[17]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.apo);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[18]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.apa);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[19]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ast);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[20]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.asl);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[21]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ach);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[22]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.asn);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[23]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.abl);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[24]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ans);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[25]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.anm);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[26]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ana);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[27]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.azo);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[28]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ade);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[29]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.abe);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[30]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.akn);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[31]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.abf);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[32]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.adf);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[33]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.acs);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[34]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.acu);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[35]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.acd);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[36]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ado);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[37]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ara);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[38]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.adc);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[39]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.aic);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[40]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rfi);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[41]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rwa);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[42]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rwi);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[43]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rth);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[44]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rsh);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[45]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rda);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[46]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rpo);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[47]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rpa);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[48]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rst);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[49]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rsl);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[50]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rch);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[51]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rsn);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[52]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rbl);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[53]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rns);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[54]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rnm);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[55]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rna);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[56]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rzo);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[57]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rde);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[58]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rbe);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[59]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rkn);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[60]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rbf);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[61]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rdf);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[62]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rcs);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[63]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rcu);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[64]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rcd);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[65]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rdo);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[66]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rra);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[67]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rdc);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[68]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ric);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[69]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.val);
      return offset - num;
    }

    public JSON_GrowCurve Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (JSON_GrowCurve) null;
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
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num73;
        if (!this.____keyMapping.TryGetValueSafe(key, out num73))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num73)
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
              num34 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 32:
              num35 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 33:
              num36 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 34:
              num37 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 35:
              num38 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 36:
              num39 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 37:
              num40 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 38:
              num41 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 39:
              num42 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 40:
              num43 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 41:
              num44 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 42:
              num45 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 43:
              num46 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 44:
              num47 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 45:
              num48 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 46:
              num49 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 47:
              num50 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 48:
              num51 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 49:
              num52 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 50:
              num53 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 51:
              num54 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 52:
              num55 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 53:
              num56 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 54:
              num57 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 55:
              num58 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 56:
              num59 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 57:
              num60 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 58:
              num61 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 59:
              num62 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 60:
              num63 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 61:
              num64 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 62:
              num65 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 63:
              num66 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 64:
              num67 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 65:
              num68 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 66:
              num69 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 67:
              num70 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 68:
              num71 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 69:
              num72 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new JSON_GrowCurve()
      {
        lv = num3,
        hp = num4,
        mp = num5,
        atk = num6,
        def = num7,
        mag = num8,
        mnd = num9,
        dex = num10,
        spd = num11,
        cri = num12,
        luk = num13,
        afi = num14,
        awa = num15,
        awi = num16,
        ath = num17,
        ash = num18,
        ada = num19,
        apo = num20,
        apa = num21,
        ast = num22,
        asl = num23,
        ach = num24,
        asn = num25,
        abl = num26,
        ans = num27,
        anm = num28,
        ana = num29,
        azo = num30,
        ade = num31,
        abe = num32,
        akn = num33,
        abf = num34,
        adf = num35,
        acs = num36,
        acu = num37,
        acd = num38,
        ado = num39,
        ara = num40,
        adc = num41,
        aic = num42,
        rfi = num43,
        rwa = num44,
        rwi = num45,
        rth = num46,
        rsh = num47,
        rda = num48,
        rpo = num49,
        rpa = num50,
        rst = num51,
        rsl = num52,
        rch = num53,
        rsn = num54,
        rbl = num55,
        rns = num56,
        rnm = num57,
        rna = num58,
        rzo = num59,
        rde = num60,
        rbe = num61,
        rkn = num62,
        rbf = num63,
        rdf = num64,
        rcs = num65,
        rcu = num66,
        rcd = num67,
        rdo = num68,
        rra = num69,
        rdc = num70,
        ric = num71,
        val = num72
      };
    }
  }
}
