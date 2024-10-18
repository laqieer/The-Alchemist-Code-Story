// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.JSON_AIParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class JSON_AIParamFormatter : 
    IMessagePackFormatter<JSON_AIParam>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public JSON_AIParamFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "iname",
          0
        },
        {
          "role",
          1
        },
        {
          "prm",
          2
        },
        {
          "prmprio",
          3
        },
        {
          "notprio",
          4
        },
        {
          "best",
          5
        },
        {
          "sneak",
          6
        },
        {
          "notmov",
          7
        },
        {
          "notact",
          8
        },
        {
          "notskl",
          9
        },
        {
          "notavo",
          10
        },
        {
          "notmpd",
          11
        },
        {
          "sos",
          12
        },
        {
          "heal",
          13
        },
        {
          "gems",
          14
        },
        {
          "notsup_hp",
          15
        },
        {
          "notsup_num",
          16
        },
        {
          "skill",
          17
        },
        {
          "csff",
          18
        },
        {
          "skil_prio",
          19
        },
        {
          "buff_prio",
          20
        },
        {
          "buff_self",
          21
        },
        {
          "buff_border",
          22
        },
        {
          "cond_prio",
          23
        },
        {
          "cond_border",
          24
        },
        {
          "safe_border",
          25
        },
        {
          "gosa_border",
          26
        },
        {
          "use_old_sort",
          27
        }
      };
      this.____stringByteKeys = new byte[28][]
      {
        MessagePackBinary.GetEncodedStringBytes("iname"),
        MessagePackBinary.GetEncodedStringBytes("role"),
        MessagePackBinary.GetEncodedStringBytes("prm"),
        MessagePackBinary.GetEncodedStringBytes("prmprio"),
        MessagePackBinary.GetEncodedStringBytes("notprio"),
        MessagePackBinary.GetEncodedStringBytes("best"),
        MessagePackBinary.GetEncodedStringBytes("sneak"),
        MessagePackBinary.GetEncodedStringBytes("notmov"),
        MessagePackBinary.GetEncodedStringBytes("notact"),
        MessagePackBinary.GetEncodedStringBytes("notskl"),
        MessagePackBinary.GetEncodedStringBytes("notavo"),
        MessagePackBinary.GetEncodedStringBytes("notmpd"),
        MessagePackBinary.GetEncodedStringBytes("sos"),
        MessagePackBinary.GetEncodedStringBytes("heal"),
        MessagePackBinary.GetEncodedStringBytes("gems"),
        MessagePackBinary.GetEncodedStringBytes("notsup_hp"),
        MessagePackBinary.GetEncodedStringBytes("notsup_num"),
        MessagePackBinary.GetEncodedStringBytes("skill"),
        MessagePackBinary.GetEncodedStringBytes("csff"),
        MessagePackBinary.GetEncodedStringBytes("skil_prio"),
        MessagePackBinary.GetEncodedStringBytes("buff_prio"),
        MessagePackBinary.GetEncodedStringBytes("buff_self"),
        MessagePackBinary.GetEncodedStringBytes("buff_border"),
        MessagePackBinary.GetEncodedStringBytes("cond_prio"),
        MessagePackBinary.GetEncodedStringBytes("cond_border"),
        MessagePackBinary.GetEncodedStringBytes("safe_border"),
        MessagePackBinary.GetEncodedStringBytes("gosa_border"),
        MessagePackBinary.GetEncodedStringBytes("use_old_sort")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      JSON_AIParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, 28);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.role);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.prm);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.prmprio);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.notprio);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.best);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.sneak);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.notmov);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.notact);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.notskl);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.notavo);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.notmpd);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.sos);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.heal);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.gems);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.notsup_hp);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[16]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.notsup_num);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[17]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.skill, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[18]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.csff);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[19]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.skil_prio, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[20]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.buff_prio, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[21]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.buff_self);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[22]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.buff_border);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[23]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.cond_prio, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[24]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.cond_border);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[25]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.safe_border);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[26]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.gosa_border);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[27]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.use_old_sort);
      return offset - num;
    }

    public JSON_AIParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (JSON_AIParam) null;
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
      int num14 = 0;
      int num15 = 0;
      int num16 = 0;
      int num17 = 0;
      int num18 = 0;
      int[] numArray1 = (int[]) null;
      int num19 = 0;
      int[] numArray2 = (int[]) null;
      int[] numArray3 = (int[]) null;
      int num20 = 0;
      int num21 = 0;
      int[] numArray4 = (int[]) null;
      int num22 = 0;
      int num23 = 0;
      int num24 = 0;
      int num25 = 0;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num26;
        if (!this.____keyMapping.TryGetValueSafe(key, out num26))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num26)
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
              num14 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 13:
              num15 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 14:
              num16 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 15:
              num17 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 16:
              num18 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 17:
              numArray1 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 18:
              num19 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 19:
              numArray2 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 20:
              numArray3 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 21:
              num20 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 22:
              num21 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 23:
              numArray4 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
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
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new JSON_AIParam()
      {
        iname = str,
        role = num3,
        prm = num4,
        prmprio = num5,
        notprio = num6,
        best = num7,
        sneak = num8,
        notmov = num9,
        notact = num10,
        notskl = num11,
        notavo = num12,
        notmpd = num13,
        sos = num14,
        heal = num15,
        gems = num16,
        notsup_hp = num17,
        notsup_num = num18,
        skill = numArray1,
        csff = num19,
        skil_prio = numArray2,
        buff_prio = numArray3,
        buff_self = num20,
        buff_border = num21,
        cond_prio = numArray4,
        cond_border = num22,
        safe_border = num23,
        gosa_border = num24,
        use_old_sort = num25
      };
    }
  }
}
