// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.JSON_RarityParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class JSON_RarityParamFormatter : 
    IMessagePackFormatter<JSON_RarityParam>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public JSON_RarityParamFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "unitcap",
          0
        },
        {
          "jobcap",
          1
        },
        {
          "awakecap",
          2
        },
        {
          "piece",
          3
        },
        {
          "ch_piece",
          4
        },
        {
          "ch_piece_select",
          5
        },
        {
          "rareup_cost",
          6
        },
        {
          "gain_pp",
          7
        },
        {
          "eq_enhcap",
          8
        },
        {
          "eq_costscale",
          9
        },
        {
          "eq_points",
          10
        },
        {
          "eq_item1",
          11
        },
        {
          "eq_item2",
          12
        },
        {
          "eq_item3",
          13
        },
        {
          "eq_num1",
          14
        },
        {
          "eq_num2",
          15
        },
        {
          "eq_num3",
          16
        },
        {
          "hp",
          17
        },
        {
          "mp",
          18
        },
        {
          "atk",
          19
        },
        {
          "def",
          20
        },
        {
          "mag",
          21
        },
        {
          "mnd",
          22
        },
        {
          "dex",
          23
        },
        {
          "spd",
          24
        },
        {
          "cri",
          25
        },
        {
          "luk",
          26
        },
        {
          "drop",
          27
        },
        {
          "af_lvcap",
          28
        },
        {
          "af_unlock",
          29
        },
        {
          "af_gousei",
          30
        },
        {
          "af_change",
          31
        },
        {
          "af_unlock_cost",
          32
        },
        {
          "af_gousei_cost",
          33
        },
        {
          "af_change_cost",
          34
        },
        {
          "af_upcost",
          35
        },
        {
          "card_lvcap",
          36
        },
        {
          "card_awake_count",
          37
        },
        {
          "ch_piece_coin_num",
          38
        }
      };
      this.____stringByteKeys = new byte[39][]
      {
        MessagePackBinary.GetEncodedStringBytes("unitcap"),
        MessagePackBinary.GetEncodedStringBytes("jobcap"),
        MessagePackBinary.GetEncodedStringBytes("awakecap"),
        MessagePackBinary.GetEncodedStringBytes("piece"),
        MessagePackBinary.GetEncodedStringBytes("ch_piece"),
        MessagePackBinary.GetEncodedStringBytes("ch_piece_select"),
        MessagePackBinary.GetEncodedStringBytes("rareup_cost"),
        MessagePackBinary.GetEncodedStringBytes("gain_pp"),
        MessagePackBinary.GetEncodedStringBytes("eq_enhcap"),
        MessagePackBinary.GetEncodedStringBytes("eq_costscale"),
        MessagePackBinary.GetEncodedStringBytes("eq_points"),
        MessagePackBinary.GetEncodedStringBytes("eq_item1"),
        MessagePackBinary.GetEncodedStringBytes("eq_item2"),
        MessagePackBinary.GetEncodedStringBytes("eq_item3"),
        MessagePackBinary.GetEncodedStringBytes("eq_num1"),
        MessagePackBinary.GetEncodedStringBytes("eq_num2"),
        MessagePackBinary.GetEncodedStringBytes("eq_num3"),
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
        MessagePackBinary.GetEncodedStringBytes("drop"),
        MessagePackBinary.GetEncodedStringBytes("af_lvcap"),
        MessagePackBinary.GetEncodedStringBytes("af_unlock"),
        MessagePackBinary.GetEncodedStringBytes("af_gousei"),
        MessagePackBinary.GetEncodedStringBytes("af_change"),
        MessagePackBinary.GetEncodedStringBytes("af_unlock_cost"),
        MessagePackBinary.GetEncodedStringBytes("af_gousei_cost"),
        MessagePackBinary.GetEncodedStringBytes("af_change_cost"),
        MessagePackBinary.GetEncodedStringBytes("af_upcost"),
        MessagePackBinary.GetEncodedStringBytes("card_lvcap"),
        MessagePackBinary.GetEncodedStringBytes("card_awake_count"),
        MessagePackBinary.GetEncodedStringBytes("ch_piece_coin_num")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      JSON_RarityParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, 39);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.unitcap);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.jobcap);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.awakecap);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.piece);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ch_piece);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ch_piece_select);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rareup_cost);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.gain_pp);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.eq_enhcap);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.eq_costscale);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.eq_points, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.eq_item1, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.eq_item2, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.eq_item3, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.eq_num1, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.eq_num2, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[16]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.eq_num3, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[17]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.hp);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[18]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mp);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[19]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.atk);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[20]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.def);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[21]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mag);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[22]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mnd);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[23]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.dex);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[24]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.spd);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[25]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.cri);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[26]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.luk);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[27]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.drop, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[28]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.af_lvcap);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[29]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.af_unlock);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[30]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.af_gousei);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[31]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.af_change);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[32]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.af_unlock_cost);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[33]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.af_gousei_cost);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[34]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.af_change_cost);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[35]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.af_upcost);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[36]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.card_lvcap);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[37]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.card_awake_count);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[38]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ch_piece_coin_num);
      return offset - num;
    }

    public JSON_RarityParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (JSON_RarityParam) null;
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
      int[] numArray1 = (int[]) null;
      string str1 = (string) null;
      string str2 = (string) null;
      string str3 = (string) null;
      int[] numArray2 = (int[]) null;
      int[] numArray3 = (int[]) null;
      int[] numArray4 = (int[]) null;
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
      string str4 = (string) null;
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
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num34;
        if (!this.____keyMapping.TryGetValueSafe(key, out num34))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num34)
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
              numArray1 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 11:
              str1 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 12:
              str2 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 13:
              str3 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 14:
              numArray2 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 15:
              numArray3 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 16:
              numArray4 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 17:
              num13 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 18:
              num14 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
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
              num20 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 25:
              num21 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 26:
              num22 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 27:
              str4 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 28:
              num23 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 29:
              num24 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 30:
              num25 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 31:
              num26 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 32:
              num27 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 33:
              num28 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 34:
              num29 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 35:
              num30 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 36:
              num31 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 37:
              num32 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 38:
              num33 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new JSON_RarityParam()
      {
        unitcap = num3,
        jobcap = num4,
        awakecap = num5,
        piece = num6,
        ch_piece = num7,
        ch_piece_select = num8,
        rareup_cost = num9,
        gain_pp = num10,
        eq_enhcap = num11,
        eq_costscale = num12,
        eq_points = numArray1,
        eq_item1 = str1,
        eq_item2 = str2,
        eq_item3 = str3,
        eq_num1 = numArray2,
        eq_num2 = numArray3,
        eq_num3 = numArray4,
        hp = num13,
        mp = num14,
        atk = num15,
        def = num16,
        mag = num17,
        mnd = num18,
        dex = num19,
        spd = num20,
        cri = num21,
        luk = num22,
        drop = str4,
        af_lvcap = num23,
        af_unlock = num24,
        af_gousei = num25,
        af_change = num26,
        af_unlock_cost = num27,
        af_gousei_cost = num28,
        af_change_cost = num29,
        af_upcost = num30,
        card_lvcap = num31,
        card_awake_count = num32,
        ch_piece_coin_num = num33
      };
    }
  }
}
