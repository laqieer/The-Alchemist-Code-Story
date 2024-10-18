// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.JSON_CondEffectParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class JSON_CondEffectParamFormatter : 
    IMessagePackFormatter<JSON_CondEffectParam>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public JSON_CondEffectParamFormatter()
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
          "elem",
          5
        },
        {
          "cond",
          6
        },
        {
          "type",
          7
        },
        {
          "vini",
          8
        },
        {
          "vmax",
          9
        },
        {
          "rini",
          10
        },
        {
          "rmax",
          11
        },
        {
          "tini",
          12
        },
        {
          "tmax",
          13
        },
        {
          "chktgt",
          14
        },
        {
          "timing",
          15
        },
        {
          "conds",
          16
        },
        {
          "v_poi",
          17
        },
        {
          "v_poifix",
          18
        },
        {
          "v_par",
          19
        },
        {
          "v_blihit",
          20
        },
        {
          "v_bliavo",
          21
        },
        {
          "v_dea",
          22
        },
        {
          "v_beratk",
          23
        },
        {
          "v_berdef",
          24
        },
        {
          "v_fast",
          25
        },
        {
          "v_slow",
          26
        },
        {
          "v_don",
          27
        },
        {
          "v_ahp",
          28
        },
        {
          "v_ahpfix",
          29
        },
        {
          "v_amp",
          30
        },
        {
          "v_ampfix",
          31
        },
        {
          "curse",
          32
        },
        {
          "buffs",
          33
        },
        {
          "is_lb_dupli",
          34
        },
        {
          "is_lb_resist",
          35
        },
        {
          "tag",
          36
        },
        {
          "un_group",
          37
        },
        {
          "custom_targets",
          38
        }
      };
      this.____stringByteKeys = new byte[39][]
      {
        MessagePackBinary.GetEncodedStringBytes("iname"),
        MessagePackBinary.GetEncodedStringBytes("job"),
        MessagePackBinary.GetEncodedStringBytes("buki"),
        MessagePackBinary.GetEncodedStringBytes("birth"),
        MessagePackBinary.GetEncodedStringBytes("sex"),
        MessagePackBinary.GetEncodedStringBytes("elem"),
        MessagePackBinary.GetEncodedStringBytes("cond"),
        MessagePackBinary.GetEncodedStringBytes("type"),
        MessagePackBinary.GetEncodedStringBytes("vini"),
        MessagePackBinary.GetEncodedStringBytes("vmax"),
        MessagePackBinary.GetEncodedStringBytes("rini"),
        MessagePackBinary.GetEncodedStringBytes("rmax"),
        MessagePackBinary.GetEncodedStringBytes("tini"),
        MessagePackBinary.GetEncodedStringBytes("tmax"),
        MessagePackBinary.GetEncodedStringBytes("chktgt"),
        MessagePackBinary.GetEncodedStringBytes("timing"),
        MessagePackBinary.GetEncodedStringBytes("conds"),
        MessagePackBinary.GetEncodedStringBytes("v_poi"),
        MessagePackBinary.GetEncodedStringBytes("v_poifix"),
        MessagePackBinary.GetEncodedStringBytes("v_par"),
        MessagePackBinary.GetEncodedStringBytes("v_blihit"),
        MessagePackBinary.GetEncodedStringBytes("v_bliavo"),
        MessagePackBinary.GetEncodedStringBytes("v_dea"),
        MessagePackBinary.GetEncodedStringBytes("v_beratk"),
        MessagePackBinary.GetEncodedStringBytes("v_berdef"),
        MessagePackBinary.GetEncodedStringBytes("v_fast"),
        MessagePackBinary.GetEncodedStringBytes("v_slow"),
        MessagePackBinary.GetEncodedStringBytes("v_don"),
        MessagePackBinary.GetEncodedStringBytes("v_ahp"),
        MessagePackBinary.GetEncodedStringBytes("v_ahpfix"),
        MessagePackBinary.GetEncodedStringBytes("v_amp"),
        MessagePackBinary.GetEncodedStringBytes("v_ampfix"),
        MessagePackBinary.GetEncodedStringBytes("curse"),
        MessagePackBinary.GetEncodedStringBytes("buffs"),
        MessagePackBinary.GetEncodedStringBytes("is_lb_dupli"),
        MessagePackBinary.GetEncodedStringBytes("is_lb_resist"),
        MessagePackBinary.GetEncodedStringBytes("tag"),
        MessagePackBinary.GetEncodedStringBytes("un_group"),
        MessagePackBinary.GetEncodedStringBytes("custom_targets")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      JSON_CondEffectParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, 39);
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
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.elem);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.cond);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.type);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.vini);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.vmax);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rini);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rmax);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.tini);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.tmax);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.chktgt);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.timing);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[16]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.conds, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[17]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.v_poi);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[18]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.v_poifix);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[19]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.v_par);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[20]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.v_blihit);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[21]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.v_bliavo);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[22]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.v_dea);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[23]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.v_beratk);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[24]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.v_berdef);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[25]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.v_fast);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[26]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.v_slow);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[27]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.v_don);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[28]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.v_ahp);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[29]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.v_ahpfix);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[30]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.v_amp);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[31]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.v_ampfix);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[32]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.curse);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[33]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.buffs, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[34]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.is_lb_dupli);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[35]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.is_lb_resist);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[36]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.tag, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[37]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.un_group, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[38]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.custom_targets, formatterResolver);
      return offset - num;
    }

    public JSON_CondEffectParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (JSON_CondEffectParam) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      string str1 = (string) null;
      string str2 = (string) null;
      string str3 = (string) null;
      string str4 = (string) null;
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
      int[] numArray = (int[]) null;
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
      string[] strArray1 = (string[]) null;
      int num31 = 0;
      int num32 = 0;
      string str5 = (string) null;
      string str6 = (string) null;
      string[] strArray2 = (string[]) null;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num33;
        if (!this.____keyMapping.TryGetValueSafe(key, out num33))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num33)
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
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 6:
              num5 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 7:
              num6 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 8:
              num7 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 9:
              num8 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 10:
              num9 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 11:
              num10 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 12:
              num11 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 13:
              num12 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 14:
              num13 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 15:
              num14 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 16:
              numArray = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
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
              strArray1 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 34:
              num31 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 35:
              num32 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 36:
              str5 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 37:
              str6 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 38:
              strArray2 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new JSON_CondEffectParam()
      {
        iname = str1,
        job = str2,
        buki = str3,
        birth = str4,
        sex = num3,
        elem = num4,
        cond = num5,
        type = num6,
        vini = num7,
        vmax = num8,
        rini = num9,
        rmax = num10,
        tini = num11,
        tmax = num12,
        chktgt = num13,
        timing = num14,
        conds = numArray,
        v_poi = num15,
        v_poifix = num16,
        v_par = num17,
        v_blihit = num18,
        v_bliavo = num19,
        v_dea = num20,
        v_beratk = num21,
        v_berdef = num22,
        v_fast = num23,
        v_slow = num24,
        v_don = num25,
        v_ahp = num26,
        v_ahpfix = num27,
        v_amp = num28,
        v_ampfix = num29,
        curse = num30,
        buffs = strArray1,
        is_lb_dupli = num31,
        is_lb_resist = num32,
        tag = str5,
        un_group = str6,
        custom_targets = strArray2
      };
    }
  }
}
