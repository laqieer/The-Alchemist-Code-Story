// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.JSON_JobParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class JSON_JobParamFormatter : 
    IMessagePackFormatter<JSON_JobParam>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public JSON_JobParamFormatter()
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
          "mdl",
          3
        },
        {
          "ac2d",
          4
        },
        {
          "mdlp",
          5
        },
        {
          "pet",
          6
        },
        {
          "buki",
          7
        },
        {
          "origin",
          8
        },
        {
          "type",
          9
        },
        {
          "role",
          10
        },
        {
          "jmov",
          11
        },
        {
          "jjmp",
          12
        },
        {
          "wepmdl",
          13
        },
        {
          "atkskl",
          14
        },
        {
          "atkfi",
          15
        },
        {
          "atkwa",
          16
        },
        {
          "atkwi",
          17
        },
        {
          "atkth",
          18
        },
        {
          "atksh",
          19
        },
        {
          "atkda",
          20
        },
        {
          "fixabl",
          21
        },
        {
          "artifact",
          22
        },
        {
          "ai",
          23
        },
        {
          "master",
          24
        },
        {
          "me_abl",
          25
        },
        {
          "is_me_rr",
          26
        },
        {
          "desc_ch",
          27
        },
        {
          "desc_ot",
          28
        },
        {
          "hp",
          29
        },
        {
          "mp",
          30
        },
        {
          "atk",
          31
        },
        {
          "def",
          32
        },
        {
          "mag",
          33
        },
        {
          "mnd",
          34
        },
        {
          "dex",
          35
        },
        {
          "spd",
          36
        },
        {
          "cri",
          37
        },
        {
          "luk",
          38
        },
        {
          "avoid",
          39
        },
        {
          "inimp",
          40
        },
        {
          "ranks",
          41
        },
        {
          "unit_image",
          42
        },
        {
          "mov_type",
          43
        },
        {
          "is_riding",
          44
        },
        {
          "no_pass",
          45
        },
        {
          "tags",
          46
        },
        {
          "buff",
          47
        }
      };
      this.____stringByteKeys = new byte[48][]
      {
        MessagePackBinary.GetEncodedStringBytes("iname"),
        MessagePackBinary.GetEncodedStringBytes("name"),
        MessagePackBinary.GetEncodedStringBytes("expr"),
        MessagePackBinary.GetEncodedStringBytes("mdl"),
        MessagePackBinary.GetEncodedStringBytes("ac2d"),
        MessagePackBinary.GetEncodedStringBytes("mdlp"),
        MessagePackBinary.GetEncodedStringBytes("pet"),
        MessagePackBinary.GetEncodedStringBytes("buki"),
        MessagePackBinary.GetEncodedStringBytes("origin"),
        MessagePackBinary.GetEncodedStringBytes("type"),
        MessagePackBinary.GetEncodedStringBytes("role"),
        MessagePackBinary.GetEncodedStringBytes("jmov"),
        MessagePackBinary.GetEncodedStringBytes("jjmp"),
        MessagePackBinary.GetEncodedStringBytes("wepmdl"),
        MessagePackBinary.GetEncodedStringBytes("atkskl"),
        MessagePackBinary.GetEncodedStringBytes("atkfi"),
        MessagePackBinary.GetEncodedStringBytes("atkwa"),
        MessagePackBinary.GetEncodedStringBytes("atkwi"),
        MessagePackBinary.GetEncodedStringBytes("atkth"),
        MessagePackBinary.GetEncodedStringBytes("atksh"),
        MessagePackBinary.GetEncodedStringBytes("atkda"),
        MessagePackBinary.GetEncodedStringBytes("fixabl"),
        MessagePackBinary.GetEncodedStringBytes("artifact"),
        MessagePackBinary.GetEncodedStringBytes("ai"),
        MessagePackBinary.GetEncodedStringBytes("master"),
        MessagePackBinary.GetEncodedStringBytes("me_abl"),
        MessagePackBinary.GetEncodedStringBytes("is_me_rr"),
        MessagePackBinary.GetEncodedStringBytes("desc_ch"),
        MessagePackBinary.GetEncodedStringBytes("desc_ot"),
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
        MessagePackBinary.GetEncodedStringBytes("avoid"),
        MessagePackBinary.GetEncodedStringBytes("inimp"),
        MessagePackBinary.GetEncodedStringBytes("ranks"),
        MessagePackBinary.GetEncodedStringBytes("unit_image"),
        MessagePackBinary.GetEncodedStringBytes("mov_type"),
        MessagePackBinary.GetEncodedStringBytes("is_riding"),
        MessagePackBinary.GetEncodedStringBytes("no_pass"),
        MessagePackBinary.GetEncodedStringBytes("tags"),
        MessagePackBinary.GetEncodedStringBytes("buff")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      JSON_JobParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, 48);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.name, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.expr, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.mdl, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.ac2d, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.mdlp, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.pet, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.buki, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.origin, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.type);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.role);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.jmov);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.jjmp);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.wepmdl, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.atkskl, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.atkfi, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[16]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.atkwa, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[17]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.atkwi, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[18]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.atkth, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[19]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.atksh, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[20]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.atkda, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[21]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.fixabl, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[22]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.artifact, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[23]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.ai, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[24]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.master, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[25]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.me_abl, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[26]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.is_me_rr);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[27]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.desc_ch, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[28]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.desc_ot, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[29]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.hp);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[30]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mp);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[31]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.atk);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[32]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.def);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[33]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mag);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[34]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mnd);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[35]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.dex);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[36]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.spd);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[37]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.cri);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[38]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.luk);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[39]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.avoid);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[40]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.inimp);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[41]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_JobRankParam[]>().Serialize(ref bytes, offset, value.ranks, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[42]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.unit_image, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[43]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mov_type);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[44]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.is_riding);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[45]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.no_pass);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[46]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.tags, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[47]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.buff, formatterResolver);
      return offset - num;
    }

    public JSON_JobParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (JSON_JobParam) null;
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
      int num3 = 0;
      int num4 = 0;
      int num5 = 0;
      int num6 = 0;
      string str10 = (string) null;
      string str11 = (string) null;
      string str12 = (string) null;
      string str13 = (string) null;
      string str14 = (string) null;
      string str15 = (string) null;
      string str16 = (string) null;
      string str17 = (string) null;
      string str18 = (string) null;
      string str19 = (string) null;
      string str20 = (string) null;
      string str21 = (string) null;
      string str22 = (string) null;
      int num7 = 0;
      string str23 = (string) null;
      string str24 = (string) null;
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
      JSON_JobRankParam[] jsonJobRankParamArray = (JSON_JobRankParam[]) null;
      string str25 = (string) null;
      int num20 = 0;
      int num21 = 0;
      int num22 = 0;
      string[] strArray = (string[]) null;
      string str26 = (string) null;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num23;
        if (!this.____keyMapping.TryGetValueSafe(key, out num23))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num23)
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
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 10:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 11:
              num5 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 12:
              num6 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 13:
              str10 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 14:
              str11 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 15:
              str12 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 16:
              str13 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 17:
              str14 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 18:
              str15 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 19:
              str16 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 20:
              str17 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 21:
              str18 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 22:
              str19 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 23:
              str20 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 24:
              str21 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 25:
              str22 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 26:
              num7 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 27:
              str23 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 28:
              str24 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 29:
              num8 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 30:
              num9 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 31:
              num10 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 32:
              num11 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 33:
              num12 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 34:
              num13 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 35:
              num14 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 36:
              num15 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 37:
              num16 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 38:
              num17 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 39:
              num18 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 40:
              num19 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 41:
              jsonJobRankParamArray = formatterResolver.GetFormatterWithVerify<JSON_JobRankParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 42:
              str25 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 43:
              num20 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 44:
              num21 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 45:
              num22 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 46:
              strArray = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 47:
              str26 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new JSON_JobParam()
      {
        iname = str1,
        name = str2,
        expr = str3,
        mdl = str4,
        ac2d = str5,
        mdlp = str6,
        pet = str7,
        buki = str8,
        origin = str9,
        type = num3,
        role = num4,
        jmov = num5,
        jjmp = num6,
        wepmdl = str10,
        atkskl = str11,
        atkfi = str12,
        atkwa = str13,
        atkwi = str14,
        atkth = str15,
        atksh = str16,
        atkda = str17,
        fixabl = str18,
        artifact = str19,
        ai = str20,
        master = str21,
        me_abl = str22,
        is_me_rr = num7,
        desc_ch = str23,
        desc_ot = str24,
        hp = num8,
        mp = num9,
        atk = num10,
        def = num11,
        mag = num12,
        mnd = num13,
        dex = num14,
        spd = num15,
        cri = num16,
        luk = num17,
        avoid = num18,
        inimp = num19,
        ranks = jsonJobRankParamArray,
        unit_image = str25,
        mov_type = num20,
        is_riding = num21,
        no_pass = num22,
        tags = strArray,
        buff = str26
      };
    }
  }
}
