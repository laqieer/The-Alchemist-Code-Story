// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.JSON_BuffEffectParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class JSON_BuffEffectParamFormatter : 
    IMessagePackFormatter<JSON_BuffEffectParam>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public JSON_BuffEffectParamFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "iname",
          0
        },
        {
          "job",
          1
        },
        {
          "buki",
          2
        },
        {
          "birth",
          3
        },
        {
          "sex",
          4
        },
        {
          "un_group",
          5
        },
        {
          "elem",
          6
        },
        {
          "cond",
          7
        },
        {
          "rate",
          8
        },
        {
          "turn",
          9
        },
        {
          "chktgt",
          10
        },
        {
          "timing",
          11
        },
        {
          "up_timing",
          12
        },
        {
          "app_type",
          13
        },
        {
          "app_mct",
          14
        },
        {
          "eff_range",
          15
        },
        {
          "is_up_rep",
          16
        },
        {
          "is_no_dis",
          17
        },
        {
          "is_no_bt",
          18
        },
        {
          "avoid_type",
          19
        },
        {
          "is_up_rep_usc",
          20
        },
        {
          "type1",
          21
        },
        {
          "type2",
          22
        },
        {
          "type3",
          23
        },
        {
          "type4",
          24
        },
        {
          "type5",
          25
        },
        {
          "type6",
          26
        },
        {
          "type7",
          27
        },
        {
          "type8",
          28
        },
        {
          "type9",
          29
        },
        {
          "type10",
          30
        },
        {
          "type11",
          31
        },
        {
          "vini1",
          32
        },
        {
          "vini2",
          33
        },
        {
          "vini3",
          34
        },
        {
          "vini4",
          35
        },
        {
          "vini5",
          36
        },
        {
          "vini6",
          37
        },
        {
          "vini7",
          38
        },
        {
          "vini8",
          39
        },
        {
          "vini9",
          40
        },
        {
          "vini10",
          41
        },
        {
          "vini11",
          42
        },
        {
          "vmax1",
          43
        },
        {
          "vmax2",
          44
        },
        {
          "vmax3",
          45
        },
        {
          "vmax4",
          46
        },
        {
          "vmax5",
          47
        },
        {
          "vmax6",
          48
        },
        {
          "vmax7",
          49
        },
        {
          "vmax8",
          50
        },
        {
          "vmax9",
          51
        },
        {
          "vmax10",
          52
        },
        {
          "vmax11",
          53
        },
        {
          "calc1",
          54
        },
        {
          "calc2",
          55
        },
        {
          "calc3",
          56
        },
        {
          "calc4",
          57
        },
        {
          "calc5",
          58
        },
        {
          "calc6",
          59
        },
        {
          "calc7",
          60
        },
        {
          "calc8",
          61
        },
        {
          "calc9",
          62
        },
        {
          "calc10",
          63
        },
        {
          "calc11",
          64
        },
        {
          "vone1",
          65
        },
        {
          "vone2",
          66
        },
        {
          "vone3",
          67
        },
        {
          "vone4",
          68
        },
        {
          "vone5",
          69
        },
        {
          "vone6",
          70
        },
        {
          "vone7",
          71
        },
        {
          "vone8",
          72
        },
        {
          "vone9",
          73
        },
        {
          "vone10",
          74
        },
        {
          "vone11",
          75
        },
        {
          "tktag1",
          76
        },
        {
          "tktag2",
          77
        },
        {
          "tktag3",
          78
        },
        {
          "tktag4",
          79
        },
        {
          "tktag5",
          80
        },
        {
          "tktag6",
          81
        },
        {
          "tktag7",
          82
        },
        {
          "tktag8",
          83
        },
        {
          "tktag9",
          84
        },
        {
          "tktag10",
          85
        },
        {
          "tktag11",
          86
        },
        {
          "custom_targets",
          87
        },
        {
          "tag",
          88
        }
      };
      this.____stringByteKeys = new byte[89][]
      {
        MessagePackBinary.GetEncodedStringBytes("iname"),
        MessagePackBinary.GetEncodedStringBytes("job"),
        MessagePackBinary.GetEncodedStringBytes("buki"),
        MessagePackBinary.GetEncodedStringBytes("birth"),
        MessagePackBinary.GetEncodedStringBytes("sex"),
        MessagePackBinary.GetEncodedStringBytes("un_group"),
        MessagePackBinary.GetEncodedStringBytes("elem"),
        MessagePackBinary.GetEncodedStringBytes("cond"),
        MessagePackBinary.GetEncodedStringBytes("rate"),
        MessagePackBinary.GetEncodedStringBytes("turn"),
        MessagePackBinary.GetEncodedStringBytes("chktgt"),
        MessagePackBinary.GetEncodedStringBytes("timing"),
        MessagePackBinary.GetEncodedStringBytes("up_timing"),
        MessagePackBinary.GetEncodedStringBytes("app_type"),
        MessagePackBinary.GetEncodedStringBytes("app_mct"),
        MessagePackBinary.GetEncodedStringBytes("eff_range"),
        MessagePackBinary.GetEncodedStringBytes("is_up_rep"),
        MessagePackBinary.GetEncodedStringBytes("is_no_dis"),
        MessagePackBinary.GetEncodedStringBytes("is_no_bt"),
        MessagePackBinary.GetEncodedStringBytes("avoid_type"),
        MessagePackBinary.GetEncodedStringBytes("is_up_rep_usc"),
        MessagePackBinary.GetEncodedStringBytes("type1"),
        MessagePackBinary.GetEncodedStringBytes("type2"),
        MessagePackBinary.GetEncodedStringBytes("type3"),
        MessagePackBinary.GetEncodedStringBytes("type4"),
        MessagePackBinary.GetEncodedStringBytes("type5"),
        MessagePackBinary.GetEncodedStringBytes("type6"),
        MessagePackBinary.GetEncodedStringBytes("type7"),
        MessagePackBinary.GetEncodedStringBytes("type8"),
        MessagePackBinary.GetEncodedStringBytes("type9"),
        MessagePackBinary.GetEncodedStringBytes("type10"),
        MessagePackBinary.GetEncodedStringBytes("type11"),
        MessagePackBinary.GetEncodedStringBytes("vini1"),
        MessagePackBinary.GetEncodedStringBytes("vini2"),
        MessagePackBinary.GetEncodedStringBytes("vini3"),
        MessagePackBinary.GetEncodedStringBytes("vini4"),
        MessagePackBinary.GetEncodedStringBytes("vini5"),
        MessagePackBinary.GetEncodedStringBytes("vini6"),
        MessagePackBinary.GetEncodedStringBytes("vini7"),
        MessagePackBinary.GetEncodedStringBytes("vini8"),
        MessagePackBinary.GetEncodedStringBytes("vini9"),
        MessagePackBinary.GetEncodedStringBytes("vini10"),
        MessagePackBinary.GetEncodedStringBytes("vini11"),
        MessagePackBinary.GetEncodedStringBytes("vmax1"),
        MessagePackBinary.GetEncodedStringBytes("vmax2"),
        MessagePackBinary.GetEncodedStringBytes("vmax3"),
        MessagePackBinary.GetEncodedStringBytes("vmax4"),
        MessagePackBinary.GetEncodedStringBytes("vmax5"),
        MessagePackBinary.GetEncodedStringBytes("vmax6"),
        MessagePackBinary.GetEncodedStringBytes("vmax7"),
        MessagePackBinary.GetEncodedStringBytes("vmax8"),
        MessagePackBinary.GetEncodedStringBytes("vmax9"),
        MessagePackBinary.GetEncodedStringBytes("vmax10"),
        MessagePackBinary.GetEncodedStringBytes("vmax11"),
        MessagePackBinary.GetEncodedStringBytes("calc1"),
        MessagePackBinary.GetEncodedStringBytes("calc2"),
        MessagePackBinary.GetEncodedStringBytes("calc3"),
        MessagePackBinary.GetEncodedStringBytes("calc4"),
        MessagePackBinary.GetEncodedStringBytes("calc5"),
        MessagePackBinary.GetEncodedStringBytes("calc6"),
        MessagePackBinary.GetEncodedStringBytes("calc7"),
        MessagePackBinary.GetEncodedStringBytes("calc8"),
        MessagePackBinary.GetEncodedStringBytes("calc9"),
        MessagePackBinary.GetEncodedStringBytes("calc10"),
        MessagePackBinary.GetEncodedStringBytes("calc11"),
        MessagePackBinary.GetEncodedStringBytes("vone1"),
        MessagePackBinary.GetEncodedStringBytes("vone2"),
        MessagePackBinary.GetEncodedStringBytes("vone3"),
        MessagePackBinary.GetEncodedStringBytes("vone4"),
        MessagePackBinary.GetEncodedStringBytes("vone5"),
        MessagePackBinary.GetEncodedStringBytes("vone6"),
        MessagePackBinary.GetEncodedStringBytes("vone7"),
        MessagePackBinary.GetEncodedStringBytes("vone8"),
        MessagePackBinary.GetEncodedStringBytes("vone9"),
        MessagePackBinary.GetEncodedStringBytes("vone10"),
        MessagePackBinary.GetEncodedStringBytes("vone11"),
        MessagePackBinary.GetEncodedStringBytes("tktag1"),
        MessagePackBinary.GetEncodedStringBytes("tktag2"),
        MessagePackBinary.GetEncodedStringBytes("tktag3"),
        MessagePackBinary.GetEncodedStringBytes("tktag4"),
        MessagePackBinary.GetEncodedStringBytes("tktag5"),
        MessagePackBinary.GetEncodedStringBytes("tktag6"),
        MessagePackBinary.GetEncodedStringBytes("tktag7"),
        MessagePackBinary.GetEncodedStringBytes("tktag8"),
        MessagePackBinary.GetEncodedStringBytes("tktag9"),
        MessagePackBinary.GetEncodedStringBytes("tktag10"),
        MessagePackBinary.GetEncodedStringBytes("tktag11"),
        MessagePackBinary.GetEncodedStringBytes("custom_targets"),
        MessagePackBinary.GetEncodedStringBytes("tag")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      JSON_BuffEffectParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, 89);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.job, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.buki, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.birth, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.sex);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.un_group, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.elem);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.cond);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rate);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.turn);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.chktgt);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.timing);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.up_timing);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.app_type);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.app_mct);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.eff_range);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[16]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.is_up_rep);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[17]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.is_no_dis);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[18]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.is_no_bt);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[19]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.avoid_type);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[20]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.is_up_rep_usc);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[21]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.type1);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[22]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.type2);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[23]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.type3);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[24]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.type4);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[25]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.type5);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[26]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.type6);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[27]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.type7);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[28]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.type8);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[29]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.type9);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[30]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.type10);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[31]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.type11);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[32]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.vini1);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[33]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.vini2);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[34]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.vini3);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[35]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.vini4);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[36]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.vini5);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[37]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.vini6);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[38]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.vini7);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[39]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.vini8);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[40]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.vini9);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[41]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.vini10);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[42]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.vini11);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[43]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.vmax1);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[44]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.vmax2);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[45]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.vmax3);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[46]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.vmax4);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[47]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.vmax5);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[48]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.vmax6);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[49]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.vmax7);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[50]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.vmax8);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[51]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.vmax9);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[52]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.vmax10);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[53]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.vmax11);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[54]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.calc1);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[55]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.calc2);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[56]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.calc3);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[57]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.calc4);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[58]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.calc5);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[59]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.calc6);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[60]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.calc7);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[61]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.calc8);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[62]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.calc9);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[63]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.calc10);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[64]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.calc11);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[65]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.vone1);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[66]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.vone2);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[67]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.vone3);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[68]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.vone4);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[69]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.vone5);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[70]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.vone6);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[71]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.vone7);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[72]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.vone8);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[73]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.vone9);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[74]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.vone10);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[75]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.vone11);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[76]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.tktag1, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[77]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.tktag2, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[78]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.tktag3, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[79]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.tktag4, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[80]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.tktag5, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[81]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.tktag6, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[82]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.tktag7, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[83]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.tktag8, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[84]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.tktag9, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[85]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.tktag10, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[86]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.tktag11, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[87]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.custom_targets, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[88]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.tag, formatterResolver);
      return offset - num;
    }

    public JSON_BuffEffectParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (JSON_BuffEffectParam) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      string str1 = (string) null;
      string str2 = (string) null;
      string str3 = (string) null;
      string str4 = (string) null;
      int num3 = 0;
      string str5 = (string) null;
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
      short num63 = 0;
      short num64 = 0;
      short num65 = 0;
      short num66 = 0;
      short num67 = 0;
      short num68 = 0;
      short num69 = 0;
      short num70 = 0;
      short num71 = 0;
      short num72 = 0;
      short num73 = 0;
      string str6 = (string) null;
      string str7 = (string) null;
      string str8 = (string) null;
      string str9 = (string) null;
      string str10 = (string) null;
      string str11 = (string) null;
      string str12 = (string) null;
      string str13 = (string) null;
      string str14 = (string) null;
      string str15 = (string) null;
      string str16 = (string) null;
      string[] strArray = (string[]) null;
      string str17 = (string) null;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num74;
        if (!this.____keyMapping.TryGetValueSafe(key, out num74))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num74)
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
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 5:
              str5 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 6:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 7:
              num5 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 8:
              num6 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 9:
              num7 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 10:
              num8 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 11:
              num9 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 12:
              num10 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 13:
              num11 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 14:
              num12 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 15:
              num13 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 16:
              num14 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 17:
              num15 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 18:
              num16 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 19:
              num17 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 20:
              num18 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 21:
              num19 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 22:
              num20 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 23:
              num21 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 24:
              num22 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 25:
              num23 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 26:
              num24 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 27:
              num25 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 28:
              num26 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 29:
              num27 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 30:
              num28 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 31:
              num29 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 32:
              num30 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 33:
              num31 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 34:
              num32 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 35:
              num33 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 36:
              num34 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 37:
              num35 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
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
              num40 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 43:
              num41 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 44:
              num42 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 45:
              num43 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 46:
              num44 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 47:
              num45 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 48:
              num46 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 49:
              num47 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 50:
              num48 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 51:
              num49 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 52:
              num50 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 53:
              num51 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 54:
              num52 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 55:
              num53 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 56:
              num54 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 57:
              num55 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 58:
              num56 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 59:
              num57 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 60:
              num58 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 61:
              num59 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 62:
              num60 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 63:
              num61 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 64:
              num62 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 65:
              num63 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 66:
              num64 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 67:
              num65 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 68:
              num66 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 69:
              num67 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 70:
              num68 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 71:
              num69 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 72:
              num70 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 73:
              num71 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 74:
              num72 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 75:
              num73 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 76:
              str6 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 77:
              str7 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 78:
              str8 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 79:
              str9 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 80:
              str10 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 81:
              str11 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 82:
              str12 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 83:
              str13 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 84:
              str14 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 85:
              str15 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 86:
              str16 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 87:
              strArray = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 88:
              str17 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new JSON_BuffEffectParam()
      {
        iname = str1,
        job = str2,
        buki = str3,
        birth = str4,
        sex = num3,
        un_group = str5,
        elem = num4,
        cond = num5,
        rate = num6,
        turn = num7,
        chktgt = num8,
        timing = num9,
        up_timing = num10,
        app_type = num11,
        app_mct = num12,
        eff_range = num13,
        is_up_rep = num14,
        is_no_dis = num15,
        is_no_bt = num16,
        avoid_type = num17,
        is_up_rep_usc = num18,
        type1 = num19,
        type2 = num20,
        type3 = num21,
        type4 = num22,
        type5 = num23,
        type6 = num24,
        type7 = num25,
        type8 = num26,
        type9 = num27,
        type10 = num28,
        type11 = num29,
        vini1 = num30,
        vini2 = num31,
        vini3 = num32,
        vini4 = num33,
        vini5 = num34,
        vini6 = num35,
        vini7 = num36,
        vini8 = num37,
        vini9 = num38,
        vini10 = num39,
        vini11 = num40,
        vmax1 = num41,
        vmax2 = num42,
        vmax3 = num43,
        vmax4 = num44,
        vmax5 = num45,
        vmax6 = num46,
        vmax7 = num47,
        vmax8 = num48,
        vmax9 = num49,
        vmax10 = num50,
        vmax11 = num51,
        calc1 = num52,
        calc2 = num53,
        calc3 = num54,
        calc4 = num55,
        calc5 = num56,
        calc6 = num57,
        calc7 = num58,
        calc8 = num59,
        calc9 = num60,
        calc10 = num61,
        calc11 = num62,
        vone1 = num63,
        vone2 = num64,
        vone3 = num65,
        vone4 = num66,
        vone5 = num67,
        vone6 = num68,
        vone7 = num69,
        vone8 = num70,
        vone9 = num71,
        vone10 = num72,
        vone11 = num73,
        tktag1 = str6,
        tktag2 = str7,
        tktag3 = str8,
        tktag4 = str9,
        tktag5 = str10,
        tktag6 = str11,
        tktag7 = str12,
        tktag8 = str13,
        tktag9 = str14,
        tktag10 = str15,
        tktag11 = str16,
        custom_targets = strArray,
        tag = str17
      };
    }
  }
}
