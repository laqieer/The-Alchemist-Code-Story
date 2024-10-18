// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.JSON_ArtifactParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class JSON_ArtifactParamFormatter : 
    IMessagePackFormatter<JSON_ArtifactParam>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public JSON_ArtifactParamFormatter()
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
          "flavor",
          3
        },
        {
          "spec",
          4
        },
        {
          "asset",
          5
        },
        {
          "voice",
          6
        },
        {
          "icon",
          7
        },
        {
          "type",
          8
        },
        {
          "rini",
          9
        },
        {
          "rmax",
          10
        },
        {
          "kakera",
          11
        },
        {
          "notsmn",
          12
        },
        {
          "maxnum",
          13
        },
        {
          "skills",
          14
        },
        {
          "equip1",
          15
        },
        {
          "equip2",
          16
        },
        {
          "equip3",
          17
        },
        {
          "equip4",
          18
        },
        {
          "equip5",
          19
        },
        {
          "attack1",
          20
        },
        {
          "attack2",
          21
        },
        {
          "attack3",
          22
        },
        {
          "attack4",
          23
        },
        {
          "attack5",
          24
        },
        {
          "abils",
          25
        },
        {
          "abshows",
          26
        },
        {
          "ablvs",
          27
        },
        {
          "abrares",
          28
        },
        {
          "abconds",
          29
        },
        {
          "kc",
          30
        },
        {
          "tc",
          31
        },
        {
          "ac",
          32
        },
        {
          "mc",
          33
        },
        {
          "pp",
          34
        },
        {
          "buy",
          35
        },
        {
          "sell",
          36
        },
        {
          "ecost",
          37
        },
        {
          "tag",
          38
        },
        {
          "eqcond",
          39
        },
        {
          "units",
          40
        },
        {
          "jobs",
          41
        },
        {
          "birth",
          42
        },
        {
          "sex",
          43
        },
        {
          "elem",
          44
        },
        {
          "eqlv",
          45
        },
        {
          "eqrmin",
          46
        },
        {
          "eqrmax",
          47
        },
        {
          "cond_sm",
          48
        },
        {
          "skin_hide",
          49
        },
        {
          "insp_skill",
          50
        },
        {
          "insp_lv_bonus",
          51
        },
        {
          "gallery_view",
          52
        }
      };
      this.____stringByteKeys = new byte[53][]
      {
        MessagePackBinary.GetEncodedStringBytes("iname"),
        MessagePackBinary.GetEncodedStringBytes("name"),
        MessagePackBinary.GetEncodedStringBytes("expr"),
        MessagePackBinary.GetEncodedStringBytes("flavor"),
        MessagePackBinary.GetEncodedStringBytes("spec"),
        MessagePackBinary.GetEncodedStringBytes("asset"),
        MessagePackBinary.GetEncodedStringBytes("voice"),
        MessagePackBinary.GetEncodedStringBytes("icon"),
        MessagePackBinary.GetEncodedStringBytes("type"),
        MessagePackBinary.GetEncodedStringBytes("rini"),
        MessagePackBinary.GetEncodedStringBytes("rmax"),
        MessagePackBinary.GetEncodedStringBytes("kakera"),
        MessagePackBinary.GetEncodedStringBytes("notsmn"),
        MessagePackBinary.GetEncodedStringBytes("maxnum"),
        MessagePackBinary.GetEncodedStringBytes("skills"),
        MessagePackBinary.GetEncodedStringBytes("equip1"),
        MessagePackBinary.GetEncodedStringBytes("equip2"),
        MessagePackBinary.GetEncodedStringBytes("equip3"),
        MessagePackBinary.GetEncodedStringBytes("equip4"),
        MessagePackBinary.GetEncodedStringBytes("equip5"),
        MessagePackBinary.GetEncodedStringBytes("attack1"),
        MessagePackBinary.GetEncodedStringBytes("attack2"),
        MessagePackBinary.GetEncodedStringBytes("attack3"),
        MessagePackBinary.GetEncodedStringBytes("attack4"),
        MessagePackBinary.GetEncodedStringBytes("attack5"),
        MessagePackBinary.GetEncodedStringBytes("abils"),
        MessagePackBinary.GetEncodedStringBytes("abshows"),
        MessagePackBinary.GetEncodedStringBytes("ablvs"),
        MessagePackBinary.GetEncodedStringBytes("abrares"),
        MessagePackBinary.GetEncodedStringBytes("abconds"),
        MessagePackBinary.GetEncodedStringBytes("kc"),
        MessagePackBinary.GetEncodedStringBytes("tc"),
        MessagePackBinary.GetEncodedStringBytes("ac"),
        MessagePackBinary.GetEncodedStringBytes("mc"),
        MessagePackBinary.GetEncodedStringBytes("pp"),
        MessagePackBinary.GetEncodedStringBytes("buy"),
        MessagePackBinary.GetEncodedStringBytes("sell"),
        MessagePackBinary.GetEncodedStringBytes("ecost"),
        MessagePackBinary.GetEncodedStringBytes("tag"),
        MessagePackBinary.GetEncodedStringBytes("eqcond"),
        MessagePackBinary.GetEncodedStringBytes("units"),
        MessagePackBinary.GetEncodedStringBytes("jobs"),
        MessagePackBinary.GetEncodedStringBytes("birth"),
        MessagePackBinary.GetEncodedStringBytes("sex"),
        MessagePackBinary.GetEncodedStringBytes("elem"),
        MessagePackBinary.GetEncodedStringBytes("eqlv"),
        MessagePackBinary.GetEncodedStringBytes("eqrmin"),
        MessagePackBinary.GetEncodedStringBytes("eqrmax"),
        MessagePackBinary.GetEncodedStringBytes("cond_sm"),
        MessagePackBinary.GetEncodedStringBytes("skin_hide"),
        MessagePackBinary.GetEncodedStringBytes("insp_skill"),
        MessagePackBinary.GetEncodedStringBytes("insp_lv_bonus"),
        MessagePackBinary.GetEncodedStringBytes("gallery_view")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      JSON_ArtifactParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, 53);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.name, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.expr, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.flavor, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.spec, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.asset, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.voice, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.icon, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.type);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rini);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rmax);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.kakera, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.notsmn);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.maxnum);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.skills, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.equip1, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[16]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.equip2, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[17]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.equip3, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[18]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.equip4, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[19]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.equip5, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[20]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.attack1, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[21]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.attack2, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[22]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.attack3, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[23]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.attack4, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[24]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.attack5, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[25]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.abils, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[26]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.abshows, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[27]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.ablvs, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[28]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.abrares, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[29]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.abconds, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[30]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.kc);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[31]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.tc);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[32]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ac);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[33]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mc);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[34]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.pp);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[35]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.buy);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[36]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.sell);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[37]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ecost);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[38]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.tag, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[39]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.eqcond, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[40]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.units, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[41]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.jobs, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[42]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.birth, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[43]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.sex);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[44]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.elem);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[45]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.eqlv);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[46]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.eqrmin);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[47]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.eqrmax);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[48]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.cond_sm, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[49]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.skin_hide);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[50]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.insp_skill, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[51]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.insp_lv_bonus);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[52]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.gallery_view);
      return offset - num;
    }

    public JSON_ArtifactParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (JSON_ArtifactParam) null;
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
      string str9 = (string) null;
      int num6 = 0;
      int num7 = 0;
      string[] strArray1 = (string[]) null;
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
      string[] strArray2 = (string[]) null;
      int[] numArray1 = (int[]) null;
      int[] numArray2 = (int[]) null;
      int[] numArray3 = (int[]) null;
      string[] strArray3 = (string[]) null;
      int num8 = 0;
      int num9 = 0;
      int num10 = 0;
      int num11 = 0;
      int num12 = 0;
      int num13 = 0;
      int num14 = 0;
      int num15 = 0;
      string str20 = (string) null;
      string str21 = (string) null;
      string[] strArray4 = (string[]) null;
      string[] strArray5 = (string[]) null;
      string str22 = (string) null;
      int num16 = 0;
      int num17 = 0;
      int num18 = 0;
      int num19 = 0;
      int num20 = 0;
      string str23 = (string) null;
      int num21 = 0;
      string str24 = (string) null;
      int num22 = 0;
      int num23 = 0;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num24;
        if (!this.____keyMapping.TryGetValueSafe(key, out num24))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num24)
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
              str9 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 12:
              num6 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 13:
              num7 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 14:
              strArray1 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 15:
              str10 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 16:
              str11 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 17:
              str12 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 18:
              str13 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 19:
              str14 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 20:
              str15 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 21:
              str16 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 22:
              str17 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 23:
              str18 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 24:
              str19 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 25:
              strArray2 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 26:
              numArray1 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 27:
              numArray2 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 28:
              numArray3 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 29:
              strArray3 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
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
              num11 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 34:
              num12 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 35:
              num13 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 36:
              num14 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 37:
              num15 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 38:
              str20 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 39:
              str21 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 40:
              strArray4 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 41:
              strArray5 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 42:
              str22 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 43:
              num16 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 44:
              num17 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 45:
              num18 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 46:
              num19 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 47:
              num20 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 48:
              str23 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 49:
              num21 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 50:
              str24 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 51:
              num22 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 52:
              num23 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new JSON_ArtifactParam()
      {
        iname = str1,
        name = str2,
        expr = str3,
        flavor = str4,
        spec = str5,
        asset = str6,
        voice = str7,
        icon = str8,
        type = num3,
        rini = num4,
        rmax = num5,
        kakera = str9,
        notsmn = num6,
        maxnum = num7,
        skills = strArray1,
        equip1 = str10,
        equip2 = str11,
        equip3 = str12,
        equip4 = str13,
        equip5 = str14,
        attack1 = str15,
        attack2 = str16,
        attack3 = str17,
        attack4 = str18,
        attack5 = str19,
        abils = strArray2,
        abshows = numArray1,
        ablvs = numArray2,
        abrares = numArray3,
        abconds = strArray3,
        kc = num8,
        tc = num9,
        ac = num10,
        mc = num11,
        pp = num12,
        buy = num13,
        sell = num14,
        ecost = num15,
        tag = str20,
        eqcond = str21,
        units = strArray4,
        jobs = strArray5,
        birth = str22,
        sex = num16,
        elem = num17,
        eqlv = num18,
        eqrmin = num19,
        eqrmax = num20,
        cond_sm = str23,
        skin_hide = num21,
        insp_skill = str24,
        insp_lv_bonus = num22,
        gallery_view = num23
      };
    }
  }
}
