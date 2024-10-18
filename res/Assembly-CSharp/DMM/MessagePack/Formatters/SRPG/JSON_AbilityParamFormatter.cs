// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.JSON_AbilityParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class JSON_AbilityParamFormatter : 
    IMessagePackFormatter<JSON_AbilityParam>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public JSON_AbilityParamFormatter()
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
          "icon",
          3
        },
        {
          "type",
          4
        },
        {
          "slot",
          5
        },
        {
          "fix",
          6
        },
        {
          "cap",
          7
        },
        {
          "skl1",
          8
        },
        {
          "skl2",
          9
        },
        {
          "skl3",
          10
        },
        {
          "skl4",
          11
        },
        {
          "skl5",
          12
        },
        {
          "skl6",
          13
        },
        {
          "skl7",
          14
        },
        {
          "skl8",
          15
        },
        {
          "skl9",
          16
        },
        {
          "skl10",
          17
        },
        {
          "lv1",
          18
        },
        {
          "lv2",
          19
        },
        {
          "lv3",
          20
        },
        {
          "lv4",
          21
        },
        {
          "lv5",
          22
        },
        {
          "lv6",
          23
        },
        {
          "lv7",
          24
        },
        {
          "lv8",
          25
        },
        {
          "lv9",
          26
        },
        {
          "lv10",
          27
        },
        {
          "units",
          28
        },
        {
          "units_cnds_type",
          29
        },
        {
          "jobs",
          30
        },
        {
          "jobs_cnds_type",
          31
        },
        {
          "birth",
          32
        },
        {
          "sex",
          33
        },
        {
          "elem",
          34
        },
        {
          "rmin",
          35
        },
        {
          "rmax",
          36
        },
        {
          "type_detail",
          37
        }
      };
      this.____stringByteKeys = new byte[38][]
      {
        MessagePackBinary.GetEncodedStringBytes("iname"),
        MessagePackBinary.GetEncodedStringBytes("name"),
        MessagePackBinary.GetEncodedStringBytes("expr"),
        MessagePackBinary.GetEncodedStringBytes("icon"),
        MessagePackBinary.GetEncodedStringBytes("type"),
        MessagePackBinary.GetEncodedStringBytes("slot"),
        MessagePackBinary.GetEncodedStringBytes("fix"),
        MessagePackBinary.GetEncodedStringBytes("cap"),
        MessagePackBinary.GetEncodedStringBytes("skl1"),
        MessagePackBinary.GetEncodedStringBytes("skl2"),
        MessagePackBinary.GetEncodedStringBytes("skl3"),
        MessagePackBinary.GetEncodedStringBytes("skl4"),
        MessagePackBinary.GetEncodedStringBytes("skl5"),
        MessagePackBinary.GetEncodedStringBytes("skl6"),
        MessagePackBinary.GetEncodedStringBytes("skl7"),
        MessagePackBinary.GetEncodedStringBytes("skl8"),
        MessagePackBinary.GetEncodedStringBytes("skl9"),
        MessagePackBinary.GetEncodedStringBytes("skl10"),
        MessagePackBinary.GetEncodedStringBytes("lv1"),
        MessagePackBinary.GetEncodedStringBytes("lv2"),
        MessagePackBinary.GetEncodedStringBytes("lv3"),
        MessagePackBinary.GetEncodedStringBytes("lv4"),
        MessagePackBinary.GetEncodedStringBytes("lv5"),
        MessagePackBinary.GetEncodedStringBytes("lv6"),
        MessagePackBinary.GetEncodedStringBytes("lv7"),
        MessagePackBinary.GetEncodedStringBytes("lv8"),
        MessagePackBinary.GetEncodedStringBytes("lv9"),
        MessagePackBinary.GetEncodedStringBytes("lv10"),
        MessagePackBinary.GetEncodedStringBytes("units"),
        MessagePackBinary.GetEncodedStringBytes("units_cnds_type"),
        MessagePackBinary.GetEncodedStringBytes("jobs"),
        MessagePackBinary.GetEncodedStringBytes("jobs_cnds_type"),
        MessagePackBinary.GetEncodedStringBytes("birth"),
        MessagePackBinary.GetEncodedStringBytes("sex"),
        MessagePackBinary.GetEncodedStringBytes("elem"),
        MessagePackBinary.GetEncodedStringBytes("rmin"),
        MessagePackBinary.GetEncodedStringBytes("rmax"),
        MessagePackBinary.GetEncodedStringBytes("type_detail")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      JSON_AbilityParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, 38);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.name, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.expr, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.icon, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.type);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.slot);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.fix);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.cap);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.skl1, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.skl2, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.skl3, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.skl4, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.skl5, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.skl6, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.skl7, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.skl8, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[16]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.skl9, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[17]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.skl10, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[18]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.lv1);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[19]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.lv2);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[20]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.lv3);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[21]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.lv4);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[22]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.lv5);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[23]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.lv6);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[24]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.lv7);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[25]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.lv8);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[26]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.lv9);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[27]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.lv10);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[28]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.units, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[29]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.units_cnds_type);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[30]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.jobs, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[31]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.jobs_cnds_type);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[32]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.birth, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[33]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.sex);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[34]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.elem);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[35]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rmin);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[36]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rmax);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[37]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.type_detail);
      return offset - num;
    }

    public JSON_AbilityParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (JSON_AbilityParam) null;
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
      string str5 = (string) null;
      string str6 = (string) null;
      string str7 = (string) null;
      string str8 = (string) null;
      string str9 = (string) null;
      string str10 = (string) null;
      string str11 = (string) null;
      string str12 = (string) null;
      string str13 = (string) null;
      string str14 = (string) null;
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
      string[] strArray1 = (string[]) null;
      int num17 = 0;
      string[] strArray2 = (string[]) null;
      int num18 = 0;
      string str15 = (string) null;
      int num19 = 0;
      int num20 = 0;
      int num21 = 0;
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
              str5 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 9:
              str6 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 10:
              str7 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 11:
              str8 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 12:
              str9 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
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
              num7 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 19:
              num8 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 20:
              num9 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 21:
              num10 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 22:
              num11 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 23:
              num12 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 24:
              num13 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 25:
              num14 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 26:
              num15 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 27:
              num16 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 28:
              strArray1 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 29:
              num17 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 30:
              strArray2 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 31:
              num18 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 32:
              str15 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 33:
              num19 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 34:
              num20 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 35:
              num21 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 36:
              num22 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 37:
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
      return new JSON_AbilityParam()
      {
        iname = str1,
        name = str2,
        expr = str3,
        icon = str4,
        type = num3,
        slot = num4,
        fix = num5,
        cap = num6,
        skl1 = str5,
        skl2 = str6,
        skl3 = str7,
        skl4 = str8,
        skl5 = str9,
        skl6 = str10,
        skl7 = str11,
        skl8 = str12,
        skl9 = str13,
        skl10 = str14,
        lv1 = num7,
        lv2 = num8,
        lv3 = num9,
        lv4 = num10,
        lv5 = num11,
        lv6 = num12,
        lv7 = num13,
        lv8 = num14,
        lv9 = num15,
        lv10 = num16,
        units = strArray1,
        units_cnds_type = num17,
        jobs = strArray2,
        jobs_cnds_type = num18,
        birth = str15,
        sex = num19,
        elem = num20,
        rmin = num21,
        rmax = num22,
        type_detail = num23
      };
    }
  }
}
