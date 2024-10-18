// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.MultiPlayResumeUnitDataFormatter
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
  public sealed class MultiPlayResumeUnitDataFormatter : 
    IMessagePackFormatter<MultiPlayResumeUnitData>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public MultiPlayResumeUnitDataFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "name",
          0
        },
        {
          "hp",
          1
        },
        {
          "chp",
          2
        },
        {
          "gem",
          3
        },
        {
          "dir",
          4
        },
        {
          "x",
          5
        },
        {
          "y",
          6
        },
        {
          "target",
          7
        },
        {
          "ragetarget",
          8
        },
        {
          "castskill",
          9
        },
        {
          "chargetime",
          10
        },
        {
          "casttime",
          11
        },
        {
          "castgrid",
          12
        },
        {
          "casttarget",
          13
        },
        {
          "castindex",
          14
        },
        {
          "grid_w",
          15
        },
        {
          "grid_h",
          16
        },
        {
          "isDead",
          17
        },
        {
          "deathcnt",
          18
        },
        {
          "autojewel",
          19
        },
        {
          "waitturn",
          20
        },
        {
          "moveturn",
          21
        },
        {
          "actcnt",
          22
        },
        {
          "turncnt",
          23
        },
        {
          "trgcnt",
          24
        },
        {
          "killcnt",
          25
        },
        {
          "etr",
          26
        },
        {
          "aiindex",
          27
        },
        {
          "aiturn",
          28
        },
        {
          "aipatrol",
          29
        },
        {
          "search",
          30
        },
        {
          "entry",
          31
        },
        {
          "to_dying",
          32
        },
        {
          "paralyse",
          33
        },
        {
          "flag",
          34
        },
        {
          "ctx",
          35
        },
        {
          "cty",
          36
        },
        {
          "boi",
          37
        },
        {
          "boc",
          38
        },
        {
          "own",
          39
        },
        {
          "ist",
          40
        },
        {
          "isd",
          41
        },
        {
          "did",
          42
        },
        {
          "dfu",
          43
        },
        {
          "drt",
          44
        },
        {
          "okd",
          45
        },
        {
          "buff",
          46
        },
        {
          "cond",
          47
        },
        {
          "shields",
          48
        },
        {
          "hpis",
          49
        },
        {
          "mhm_dmgs",
          50
        },
        {
          "tfl",
          51
        },
        {
          "ffl",
          52
        },
        {
          "abilchgs",
          53
        },
        {
          "addedabils",
          54
        },
        {
          "protects",
          55
        },
        {
          "guards",
          56
        },
        {
          "skillname",
          57
        },
        {
          "skillcnt",
          58
        }
      };
      this.____stringByteKeys = new byte[59][]
      {
        MessagePackBinary.GetEncodedStringBytes("name"),
        MessagePackBinary.GetEncodedStringBytes("hp"),
        MessagePackBinary.GetEncodedStringBytes("chp"),
        MessagePackBinary.GetEncodedStringBytes("gem"),
        MessagePackBinary.GetEncodedStringBytes("dir"),
        MessagePackBinary.GetEncodedStringBytes("x"),
        MessagePackBinary.GetEncodedStringBytes("y"),
        MessagePackBinary.GetEncodedStringBytes("target"),
        MessagePackBinary.GetEncodedStringBytes("ragetarget"),
        MessagePackBinary.GetEncodedStringBytes("castskill"),
        MessagePackBinary.GetEncodedStringBytes("chargetime"),
        MessagePackBinary.GetEncodedStringBytes("casttime"),
        MessagePackBinary.GetEncodedStringBytes("castgrid"),
        MessagePackBinary.GetEncodedStringBytes("casttarget"),
        MessagePackBinary.GetEncodedStringBytes("castindex"),
        MessagePackBinary.GetEncodedStringBytes("grid_w"),
        MessagePackBinary.GetEncodedStringBytes("grid_h"),
        MessagePackBinary.GetEncodedStringBytes("isDead"),
        MessagePackBinary.GetEncodedStringBytes("deathcnt"),
        MessagePackBinary.GetEncodedStringBytes("autojewel"),
        MessagePackBinary.GetEncodedStringBytes("waitturn"),
        MessagePackBinary.GetEncodedStringBytes("moveturn"),
        MessagePackBinary.GetEncodedStringBytes("actcnt"),
        MessagePackBinary.GetEncodedStringBytes("turncnt"),
        MessagePackBinary.GetEncodedStringBytes("trgcnt"),
        MessagePackBinary.GetEncodedStringBytes("killcnt"),
        MessagePackBinary.GetEncodedStringBytes("etr"),
        MessagePackBinary.GetEncodedStringBytes("aiindex"),
        MessagePackBinary.GetEncodedStringBytes("aiturn"),
        MessagePackBinary.GetEncodedStringBytes("aipatrol"),
        MessagePackBinary.GetEncodedStringBytes("search"),
        MessagePackBinary.GetEncodedStringBytes("entry"),
        MessagePackBinary.GetEncodedStringBytes("to_dying"),
        MessagePackBinary.GetEncodedStringBytes("paralyse"),
        MessagePackBinary.GetEncodedStringBytes("flag"),
        MessagePackBinary.GetEncodedStringBytes("ctx"),
        MessagePackBinary.GetEncodedStringBytes("cty"),
        MessagePackBinary.GetEncodedStringBytes("boi"),
        MessagePackBinary.GetEncodedStringBytes("boc"),
        MessagePackBinary.GetEncodedStringBytes("own"),
        MessagePackBinary.GetEncodedStringBytes("ist"),
        MessagePackBinary.GetEncodedStringBytes("isd"),
        MessagePackBinary.GetEncodedStringBytes("did"),
        MessagePackBinary.GetEncodedStringBytes("dfu"),
        MessagePackBinary.GetEncodedStringBytes("drt"),
        MessagePackBinary.GetEncodedStringBytes("okd"),
        MessagePackBinary.GetEncodedStringBytes("buff"),
        MessagePackBinary.GetEncodedStringBytes("cond"),
        MessagePackBinary.GetEncodedStringBytes("shields"),
        MessagePackBinary.GetEncodedStringBytes("hpis"),
        MessagePackBinary.GetEncodedStringBytes("mhm_dmgs"),
        MessagePackBinary.GetEncodedStringBytes("tfl"),
        MessagePackBinary.GetEncodedStringBytes("ffl"),
        MessagePackBinary.GetEncodedStringBytes("abilchgs"),
        MessagePackBinary.GetEncodedStringBytes("addedabils"),
        MessagePackBinary.GetEncodedStringBytes("protects"),
        MessagePackBinary.GetEncodedStringBytes("guards"),
        MessagePackBinary.GetEncodedStringBytes("skillname"),
        MessagePackBinary.GetEncodedStringBytes("skillcnt")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      MultiPlayResumeUnitData value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, 59);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.name, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.hp);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.chp);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.gem);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.dir);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.x);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.y);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.target);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ragetarget);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.castskill, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.chargetime);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.casttime);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.castgrid, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.casttarget);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.castindex);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.grid_w);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[16]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.grid_h);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[17]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.isDead);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[18]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.deathcnt);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[19]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.autojewel);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[20]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.waitturn);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[21]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.moveturn);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[22]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.actcnt);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[23]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.turncnt);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[24]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.trgcnt);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[25]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.killcnt);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[26]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.etr, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[27]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.aiindex);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[28]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.aiturn);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[29]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.aipatrol);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[30]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.search);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[31]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.entry);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[32]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.to_dying);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[33]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.paralyse);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[34]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.flag);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[35]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ctx);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[36]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.cty);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[37]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.boi, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[38]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.boc);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[39]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.own);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[40]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ist);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[41]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.isd);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[42]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.did, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[43]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.dfu);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[44]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.drt);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[45]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.okd);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[46]);
      offset += formatterResolver.GetFormatterWithVerify<MultiPlayResumeBuff[]>().Serialize(ref bytes, offset, value.buff, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[47]);
      offset += formatterResolver.GetFormatterWithVerify<MultiPlayResumeBuff[]>().Serialize(ref bytes, offset, value.cond, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[48]);
      offset += formatterResolver.GetFormatterWithVerify<MultiPlayResumeShield[]>().Serialize(ref bytes, offset, value.shields, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[49]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.hpis, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[50]);
      offset += formatterResolver.GetFormatterWithVerify<MultiPlayResumeMhmDmg[]>().Serialize(ref bytes, offset, value.mhm_dmgs, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[51]);
      offset += formatterResolver.GetFormatterWithVerify<MultiPlayResumeFtgt[]>().Serialize(ref bytes, offset, value.tfl, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[52]);
      offset += formatterResolver.GetFormatterWithVerify<MultiPlayResumeFtgt[]>().Serialize(ref bytes, offset, value.ffl, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[53]);
      offset += formatterResolver.GetFormatterWithVerify<MultiPlayResumeAbilChg[]>().Serialize(ref bytes, offset, value.abilchgs, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[54]);
      offset += formatterResolver.GetFormatterWithVerify<MultiPlayResumeAddedAbil[]>().Serialize(ref bytes, offset, value.addedabils, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[55]);
      offset += formatterResolver.GetFormatterWithVerify<List<MultiPlayResumeProtect>>().Serialize(ref bytes, offset, value.protects, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[56]);
      offset += formatterResolver.GetFormatterWithVerify<List<MultiPlayResumeProtect>>().Serialize(ref bytes, offset, value.guards, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[57]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.skillname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[58]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.skillcnt, formatterResolver);
      return offset - num;
    }

    public MultiPlayResumeUnitData Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (MultiPlayResumeUnitData) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      string str1 = (string) null;
      int num3 = 0;
      int num4 = 0;
      int num5 = 0;
      int num6 = 0;
      int num7 = 0;
      int num8 = 0;
      int num9 = 0;
      int num10 = 0;
      string str2 = (string) null;
      int num11 = 0;
      int num12 = 0;
      int[] numArray1 = (int[]) null;
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
      int[] numArray2 = (int[]) null;
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
      string str3 = (string) null;
      int num36 = 0;
      int num37 = 0;
      int num38 = 0;
      int num39 = 0;
      string str4 = (string) null;
      int num40 = 0;
      int num41 = 0;
      int num42 = 0;
      MultiPlayResumeBuff[] multiPlayResumeBuffArray1 = (MultiPlayResumeBuff[]) null;
      MultiPlayResumeBuff[] multiPlayResumeBuffArray2 = (MultiPlayResumeBuff[]) null;
      MultiPlayResumeShield[] playResumeShieldArray = (MultiPlayResumeShield[]) null;
      string[] strArray1 = (string[]) null;
      MultiPlayResumeMhmDmg[] playResumeMhmDmgArray = (MultiPlayResumeMhmDmg[]) null;
      MultiPlayResumeFtgt[] multiPlayResumeFtgtArray1 = (MultiPlayResumeFtgt[]) null;
      MultiPlayResumeFtgt[] multiPlayResumeFtgtArray2 = (MultiPlayResumeFtgt[]) null;
      MultiPlayResumeAbilChg[] playResumeAbilChgArray = (MultiPlayResumeAbilChg[]) null;
      MultiPlayResumeAddedAbil[] playResumeAddedAbilArray = (MultiPlayResumeAddedAbil[]) null;
      List<MultiPlayResumeProtect> playResumeProtectList1 = (List<MultiPlayResumeProtect>) null;
      List<MultiPlayResumeProtect> playResumeProtectList2 = (List<MultiPlayResumeProtect>) null;
      string[] strArray2 = (string[]) null;
      int[] numArray3 = (int[]) null;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num43;
        if (!this.____keyMapping.TryGetValueSafe(key, out num43))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num43)
          {
            case 0:
              str1 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 2:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 3:
              num5 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 4:
              num6 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 5:
              num7 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 6:
              num8 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 7:
              num9 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 8:
              num10 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 9:
              str2 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 10:
              num11 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 11:
              num12 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 12:
              numArray1 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 13:
              num13 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 14:
              num14 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 15:
              num15 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 16:
              num16 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 17:
              num17 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 18:
              num18 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 19:
              num19 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 20:
              num20 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 21:
              num21 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 22:
              num22 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 23:
              num23 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 24:
              num24 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 25:
              num25 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 26:
              numArray2 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 27:
              num26 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 28:
              num27 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 29:
              num28 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 30:
              num29 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 31:
              num30 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 32:
              num31 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 33:
              num32 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 34:
              num33 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 35:
              num34 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 36:
              num35 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 37:
              str3 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 38:
              num36 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 39:
              num37 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 40:
              num38 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 41:
              num39 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 42:
              str4 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 43:
              num40 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 44:
              num41 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 45:
              num42 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 46:
              multiPlayResumeBuffArray1 = formatterResolver.GetFormatterWithVerify<MultiPlayResumeBuff[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 47:
              multiPlayResumeBuffArray2 = formatterResolver.GetFormatterWithVerify<MultiPlayResumeBuff[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 48:
              playResumeShieldArray = formatterResolver.GetFormatterWithVerify<MultiPlayResumeShield[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 49:
              strArray1 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 50:
              playResumeMhmDmgArray = formatterResolver.GetFormatterWithVerify<MultiPlayResumeMhmDmg[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 51:
              multiPlayResumeFtgtArray1 = formatterResolver.GetFormatterWithVerify<MultiPlayResumeFtgt[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 52:
              multiPlayResumeFtgtArray2 = formatterResolver.GetFormatterWithVerify<MultiPlayResumeFtgt[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 53:
              playResumeAbilChgArray = formatterResolver.GetFormatterWithVerify<MultiPlayResumeAbilChg[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 54:
              playResumeAddedAbilArray = formatterResolver.GetFormatterWithVerify<MultiPlayResumeAddedAbil[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 55:
              playResumeProtectList1 = formatterResolver.GetFormatterWithVerify<List<MultiPlayResumeProtect>>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 56:
              playResumeProtectList2 = formatterResolver.GetFormatterWithVerify<List<MultiPlayResumeProtect>>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 57:
              strArray2 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 58:
              numArray3 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new MultiPlayResumeUnitData()
      {
        name = str1,
        hp = num3,
        chp = num4,
        gem = num5,
        dir = num6,
        x = num7,
        y = num8,
        target = num9,
        ragetarget = num10,
        castskill = str2,
        chargetime = num11,
        casttime = num12,
        castgrid = numArray1,
        casttarget = num13,
        castindex = num14,
        grid_w = num15,
        grid_h = num16,
        isDead = num17,
        deathcnt = num18,
        autojewel = num19,
        waitturn = num20,
        moveturn = num21,
        actcnt = num22,
        turncnt = num23,
        trgcnt = num24,
        killcnt = num25,
        etr = numArray2,
        aiindex = num26,
        aiturn = num27,
        aipatrol = num28,
        search = num29,
        entry = num30,
        to_dying = num31,
        paralyse = num32,
        flag = num33,
        ctx = num34,
        cty = num35,
        boi = str3,
        boc = num36,
        own = num37,
        ist = num38,
        isd = num39,
        did = str4,
        dfu = num40,
        drt = num41,
        okd = num42,
        buff = multiPlayResumeBuffArray1,
        cond = multiPlayResumeBuffArray2,
        shields = playResumeShieldArray,
        hpis = strArray1,
        mhm_dmgs = playResumeMhmDmgArray,
        tfl = multiPlayResumeFtgtArray1,
        ffl = multiPlayResumeFtgtArray2,
        abilchgs = playResumeAbilChgArray,
        addedabils = playResumeAddedAbilArray,
        protects = playResumeProtectList1,
        guards = playResumeProtectList2,
        skillname = strArray2,
        skillcnt = numArray3
      };
    }
  }
}
