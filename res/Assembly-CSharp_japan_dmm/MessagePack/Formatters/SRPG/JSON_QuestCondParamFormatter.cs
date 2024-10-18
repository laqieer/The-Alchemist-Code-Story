// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.JSON_QuestCondParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class JSON_QuestCondParamFormatter : 
    IMessagePackFormatter<JSON_QuestCondParam>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public JSON_QuestCondParamFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "iname",
          0
        },
        {
          "plvmax",
          1
        },
        {
          "plvmin",
          2
        },
        {
          "ulvmax",
          3
        },
        {
          "ulvmin",
          4
        },
        {
          "el_none",
          5
        },
        {
          "el_fire",
          6
        },
        {
          "el_watr",
          7
        },
        {
          "el_wind",
          8
        },
        {
          "el_thdr",
          9
        },
        {
          "el_lit",
          10
        },
        {
          "el_drk",
          11
        },
        {
          "job",
          12
        },
        {
          "party_type",
          13
        },
        {
          "unit",
          14
        },
        {
          "sex",
          15
        },
        {
          "rmax",
          16
        },
        {
          "rmin",
          17
        },
        {
          "rmax_ini",
          18
        },
        {
          "rmin_ini",
          19
        },
        {
          "hmax",
          20
        },
        {
          "hmin",
          21
        },
        {
          "wmax",
          22
        },
        {
          "wmin",
          23
        },
        {
          "jobset1",
          24
        },
        {
          "jobset2",
          25
        },
        {
          "jobset3",
          26
        },
        {
          "birth",
          27
        },
        {
          "not_solo",
          28
        }
      };
      this.____stringByteKeys = new byte[29][]
      {
        MessagePackBinary.GetEncodedStringBytes("iname"),
        MessagePackBinary.GetEncodedStringBytes("plvmax"),
        MessagePackBinary.GetEncodedStringBytes("plvmin"),
        MessagePackBinary.GetEncodedStringBytes("ulvmax"),
        MessagePackBinary.GetEncodedStringBytes("ulvmin"),
        MessagePackBinary.GetEncodedStringBytes("el_none"),
        MessagePackBinary.GetEncodedStringBytes("el_fire"),
        MessagePackBinary.GetEncodedStringBytes("el_watr"),
        MessagePackBinary.GetEncodedStringBytes("el_wind"),
        MessagePackBinary.GetEncodedStringBytes("el_thdr"),
        MessagePackBinary.GetEncodedStringBytes("el_lit"),
        MessagePackBinary.GetEncodedStringBytes("el_drk"),
        MessagePackBinary.GetEncodedStringBytes("job"),
        MessagePackBinary.GetEncodedStringBytes("party_type"),
        MessagePackBinary.GetEncodedStringBytes("unit"),
        MessagePackBinary.GetEncodedStringBytes("sex"),
        MessagePackBinary.GetEncodedStringBytes("rmax"),
        MessagePackBinary.GetEncodedStringBytes("rmin"),
        MessagePackBinary.GetEncodedStringBytes("rmax_ini"),
        MessagePackBinary.GetEncodedStringBytes("rmin_ini"),
        MessagePackBinary.GetEncodedStringBytes("hmax"),
        MessagePackBinary.GetEncodedStringBytes("hmin"),
        MessagePackBinary.GetEncodedStringBytes("wmax"),
        MessagePackBinary.GetEncodedStringBytes("wmin"),
        MessagePackBinary.GetEncodedStringBytes("jobset1"),
        MessagePackBinary.GetEncodedStringBytes("jobset2"),
        MessagePackBinary.GetEncodedStringBytes("jobset3"),
        MessagePackBinary.GetEncodedStringBytes("birth"),
        MessagePackBinary.GetEncodedStringBytes("not_solo")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      JSON_QuestCondParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, 29);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.plvmax);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.plvmin);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ulvmax);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ulvmin);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.el_none);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.el_fire);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.el_watr);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.el_wind);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.el_thdr);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.el_lit);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.el_drk);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.job, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.party_type);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.unit, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.sex);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[16]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rmax);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[17]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rmin);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[18]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rmax_ini);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[19]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rmin_ini);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[20]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.hmax);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[21]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.hmin);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[22]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.wmax);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[23]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.wmin);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[24]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.jobset1);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[25]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.jobset2);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[26]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.jobset3);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[27]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.birth, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[28]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.not_solo);
      return offset - num;
    }

    public JSON_QuestCondParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (JSON_QuestCondParam) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      string str = (string) null;
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
      string[] strArray1 = (string[]) null;
      int num14 = 0;
      string[] strArray2 = (string[]) null;
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
      string[] strArray3 = (string[]) null;
      int num27 = 0;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num28;
        if (!this.____keyMapping.TryGetValueSafe(key, out num28))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num28)
          {
            case 0:
              str = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
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
              num11 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 10:
              num12 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 11:
              num13 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 12:
              strArray1 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 13:
              num14 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 14:
              strArray2 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
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
              num26 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 27:
              strArray3 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 28:
              num27 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new JSON_QuestCondParam()
      {
        iname = str,
        plvmax = num3,
        plvmin = num4,
        ulvmax = num5,
        ulvmin = num6,
        el_none = num7,
        el_fire = num8,
        el_watr = num9,
        el_wind = num10,
        el_thdr = num11,
        el_lit = num12,
        el_drk = num13,
        job = strArray1,
        party_type = num14,
        unit = strArray2,
        sex = num15,
        rmax = num16,
        rmin = num17,
        rmax_ini = num18,
        rmin_ini = num19,
        hmax = num20,
        hmin = num21,
        wmax = num22,
        wmin = num23,
        jobset1 = num24,
        jobset2 = num25,
        jobset3 = num26,
        birth = strArray3,
        not_solo = num27
      };
    }
  }
}
