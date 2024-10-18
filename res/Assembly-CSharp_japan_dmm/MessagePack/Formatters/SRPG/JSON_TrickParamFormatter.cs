// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.JSON_TrickParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class JSON_TrickParamFormatter : 
    IMessagePackFormatter<JSON_TrickParam>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public JSON_TrickParamFormatter()
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
          "dmg_type",
          3
        },
        {
          "dmg_val",
          4
        },
        {
          "calc",
          5
        },
        {
          "elem",
          6
        },
        {
          "atk_det",
          7
        },
        {
          "buff",
          8
        },
        {
          "cond",
          9
        },
        {
          "kb_rate",
          10
        },
        {
          "kb_val",
          11
        },
        {
          "target",
          12
        },
        {
          "visual",
          13
        },
        {
          "count",
          14
        },
        {
          "clock",
          15
        },
        {
          "is_no_ow",
          16
        },
        {
          "marker",
          17
        },
        {
          "effect",
          18
        },
        {
          "eff_target",
          19
        },
        {
          "eff_shape",
          20
        },
        {
          "eff_scope",
          21
        },
        {
          "eff_height",
          22
        },
        {
          "ig_mt_num",
          23
        },
        {
          "ig_mts",
          24
        },
        {
          "is_rein",
          25
        }
      };
      this.____stringByteKeys = new byte[26][]
      {
        MessagePackBinary.GetEncodedStringBytes("iname"),
        MessagePackBinary.GetEncodedStringBytes("name"),
        MessagePackBinary.GetEncodedStringBytes("expr"),
        MessagePackBinary.GetEncodedStringBytes("dmg_type"),
        MessagePackBinary.GetEncodedStringBytes("dmg_val"),
        MessagePackBinary.GetEncodedStringBytes("calc"),
        MessagePackBinary.GetEncodedStringBytes("elem"),
        MessagePackBinary.GetEncodedStringBytes("atk_det"),
        MessagePackBinary.GetEncodedStringBytes("buff"),
        MessagePackBinary.GetEncodedStringBytes("cond"),
        MessagePackBinary.GetEncodedStringBytes("kb_rate"),
        MessagePackBinary.GetEncodedStringBytes("kb_val"),
        MessagePackBinary.GetEncodedStringBytes("target"),
        MessagePackBinary.GetEncodedStringBytes("visual"),
        MessagePackBinary.GetEncodedStringBytes("count"),
        MessagePackBinary.GetEncodedStringBytes("clock"),
        MessagePackBinary.GetEncodedStringBytes("is_no_ow"),
        MessagePackBinary.GetEncodedStringBytes("marker"),
        MessagePackBinary.GetEncodedStringBytes("effect"),
        MessagePackBinary.GetEncodedStringBytes("eff_target"),
        MessagePackBinary.GetEncodedStringBytes("eff_shape"),
        MessagePackBinary.GetEncodedStringBytes("eff_scope"),
        MessagePackBinary.GetEncodedStringBytes("eff_height"),
        MessagePackBinary.GetEncodedStringBytes("ig_mt_num"),
        MessagePackBinary.GetEncodedStringBytes("ig_mts"),
        MessagePackBinary.GetEncodedStringBytes("is_rein")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      JSON_TrickParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, 26);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.name, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.expr, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.dmg_type);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.dmg_val);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.calc);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.elem);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.atk_det);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.buff, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.cond, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.kb_rate);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.kb_val);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.target);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.visual);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.count);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.clock);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[16]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.is_no_ow);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[17]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.marker, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[18]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.effect, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[19]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.eff_target);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[20]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.eff_shape);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[21]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.eff_scope);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[22]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.eff_height);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[23]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ig_mt_num);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[24]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.ig_mts, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[25]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.is_rein);
      return offset - num;
    }

    public JSON_TrickParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (JSON_TrickParam) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      string str1 = (string) null;
      string str2 = (string) null;
      string str3 = (string) null;
      int num3 = 0;
      int num4 = 0;
      int num5 = 0;
      int num6 = 0;
      int num7 = 0;
      string str4 = (string) null;
      string str5 = (string) null;
      int num8 = 0;
      int num9 = 0;
      int num10 = 0;
      int num11 = 0;
      int num12 = 0;
      int num13 = 0;
      int num14 = 0;
      string str6 = (string) null;
      string str7 = (string) null;
      int num15 = 0;
      int num16 = 0;
      int num17 = 0;
      int num18 = 0;
      int num19 = 0;
      int[] numArray = (int[]) null;
      int num20 = 0;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num21;
        if (!this.____keyMapping.TryGetValueSafe(key, out num21))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num21)
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
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 4:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 5:
              num5 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 6:
              num6 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 7:
              num7 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 8:
              str4 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 9:
              str5 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
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
              str6 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 18:
              str7 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 19:
              num15 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 20:
              num16 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 21:
              num17 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 22:
              num18 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 23:
              num19 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 24:
              numArray = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 25:
              num20 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new JSON_TrickParam()
      {
        iname = str1,
        name = str2,
        expr = str3,
        dmg_type = num3,
        dmg_val = num4,
        calc = num5,
        elem = num6,
        atk_det = num7,
        buff = str4,
        cond = str5,
        kb_rate = num8,
        kb_val = num9,
        target = num10,
        visual = num11,
        count = num12,
        clock = num13,
        is_no_ow = num14,
        marker = str6,
        effect = str7,
        eff_target = num15,
        eff_shape = num16,
        eff_scope = num17,
        eff_height = num18,
        ig_mt_num = num19,
        ig_mts = numArray,
        is_rein = num20
      };
    }
  }
}
