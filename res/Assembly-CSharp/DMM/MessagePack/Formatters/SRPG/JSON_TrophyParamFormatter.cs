// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.JSON_TrophyParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class JSON_TrophyParamFormatter : 
    IMessagePackFormatter<JSON_TrophyParam>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public JSON_TrophyParamFormatter()
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
          "flg_quests",
          3
        },
        {
          "ymd_start",
          4
        },
        {
          "category",
          5
        },
        {
          "disp",
          6
        },
        {
          "type",
          7
        },
        {
          "sval",
          8
        },
        {
          "ival",
          9
        },
        {
          "reward_item1_iname",
          10
        },
        {
          "reward_item2_iname",
          11
        },
        {
          "reward_item3_iname",
          12
        },
        {
          "reward_item1_num",
          13
        },
        {
          "reward_item2_num",
          14
        },
        {
          "reward_item3_num",
          15
        },
        {
          "reward_gold",
          16
        },
        {
          "reward_coin",
          17
        },
        {
          "reward_exp",
          18
        },
        {
          "reward_stamina",
          19
        },
        {
          "reward_artifact1_iname",
          20
        },
        {
          "reward_artifact2_iname",
          21
        },
        {
          "reward_artifact3_iname",
          22
        },
        {
          "reward_artifact1_num",
          23
        },
        {
          "reward_artifact2_num",
          24
        },
        {
          "reward_artifact3_num",
          25
        },
        {
          "parent_iname",
          26
        },
        {
          "help",
          27
        },
        {
          "reward_cc_1_iname",
          28
        },
        {
          "reward_cc_1_num",
          29
        },
        {
          "reward_cc_2_iname",
          30
        },
        {
          "reward_cc_2_num",
          31
        },
        {
          "prio_reward",
          32
        },
        {
          "star_num",
          33
        },
        {
          "is_ended",
          34
        }
      };
      this.____stringByteKeys = new byte[35][]
      {
        MessagePackBinary.GetEncodedStringBytes("iname"),
        MessagePackBinary.GetEncodedStringBytes("name"),
        MessagePackBinary.GetEncodedStringBytes("expr"),
        MessagePackBinary.GetEncodedStringBytes("flg_quests"),
        MessagePackBinary.GetEncodedStringBytes("ymd_start"),
        MessagePackBinary.GetEncodedStringBytes("category"),
        MessagePackBinary.GetEncodedStringBytes("disp"),
        MessagePackBinary.GetEncodedStringBytes("type"),
        MessagePackBinary.GetEncodedStringBytes("sval"),
        MessagePackBinary.GetEncodedStringBytes("ival"),
        MessagePackBinary.GetEncodedStringBytes("reward_item1_iname"),
        MessagePackBinary.GetEncodedStringBytes("reward_item2_iname"),
        MessagePackBinary.GetEncodedStringBytes("reward_item3_iname"),
        MessagePackBinary.GetEncodedStringBytes("reward_item1_num"),
        MessagePackBinary.GetEncodedStringBytes("reward_item2_num"),
        MessagePackBinary.GetEncodedStringBytes("reward_item3_num"),
        MessagePackBinary.GetEncodedStringBytes("reward_gold"),
        MessagePackBinary.GetEncodedStringBytes("reward_coin"),
        MessagePackBinary.GetEncodedStringBytes("reward_exp"),
        MessagePackBinary.GetEncodedStringBytes("reward_stamina"),
        MessagePackBinary.GetEncodedStringBytes("reward_artifact1_iname"),
        MessagePackBinary.GetEncodedStringBytes("reward_artifact2_iname"),
        MessagePackBinary.GetEncodedStringBytes("reward_artifact3_iname"),
        MessagePackBinary.GetEncodedStringBytes("reward_artifact1_num"),
        MessagePackBinary.GetEncodedStringBytes("reward_artifact2_num"),
        MessagePackBinary.GetEncodedStringBytes("reward_artifact3_num"),
        MessagePackBinary.GetEncodedStringBytes("parent_iname"),
        MessagePackBinary.GetEncodedStringBytes("help"),
        MessagePackBinary.GetEncodedStringBytes("reward_cc_1_iname"),
        MessagePackBinary.GetEncodedStringBytes("reward_cc_1_num"),
        MessagePackBinary.GetEncodedStringBytes("reward_cc_2_iname"),
        MessagePackBinary.GetEncodedStringBytes("reward_cc_2_num"),
        MessagePackBinary.GetEncodedStringBytes("prio_reward"),
        MessagePackBinary.GetEncodedStringBytes("star_num"),
        MessagePackBinary.GetEncodedStringBytes("is_ended")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      JSON_TrophyParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, 35);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.name, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.expr, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.flg_quests, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ymd_start);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.category, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.disp);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.type);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.sval, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ival);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.reward_item1_iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.reward_item2_iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.reward_item3_iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.reward_item1_num);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.reward_item2_num);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.reward_item3_num);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[16]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.reward_gold);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[17]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.reward_coin);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[18]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.reward_exp);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[19]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.reward_stamina);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[20]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.reward_artifact1_iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[21]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.reward_artifact2_iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[22]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.reward_artifact3_iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[23]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.reward_artifact1_num);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[24]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.reward_artifact2_num);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[25]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.reward_artifact3_num);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[26]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.parent_iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[27]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.help);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[28]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.reward_cc_1_iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[29]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.reward_cc_1_num);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[30]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.reward_cc_2_iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[31]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.reward_cc_2_num);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[32]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.prio_reward);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[33]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.star_num);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[34]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.is_ended);
      return offset - num;
    }

    public JSON_TrophyParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (JSON_TrophyParam) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      string str1 = (string) null;
      string str2 = (string) null;
      string str3 = (string) null;
      string[] strArray1 = (string[]) null;
      int num3 = 0;
      string str4 = (string) null;
      int num4 = 0;
      int num5 = 0;
      string[] strArray2 = (string[]) null;
      int num6 = 0;
      string str5 = (string) null;
      string str6 = (string) null;
      string str7 = (string) null;
      int num7 = 0;
      int num8 = 0;
      int num9 = 0;
      int num10 = 0;
      int num11 = 0;
      int num12 = 0;
      int num13 = 0;
      string str8 = (string) null;
      string str9 = (string) null;
      string str10 = (string) null;
      int num14 = 0;
      int num15 = 0;
      int num16 = 0;
      string str11 = (string) null;
      int num17 = 0;
      string str12 = (string) null;
      int num18 = 0;
      string str13 = (string) null;
      int num19 = 0;
      int num20 = 0;
      int num21 = 0;
      int num22 = 0;
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
              strArray1 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 5:
              str4 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 6:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 7:
              num5 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 8:
              strArray2 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 9:
              num6 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 10:
              str5 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 11:
              str6 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 12:
              str7 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 13:
              num7 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 14:
              num8 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 15:
              num9 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 16:
              num10 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 17:
              num11 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 18:
              num12 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 19:
              num13 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 20:
              str8 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 21:
              str9 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 22:
              str10 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 23:
              num14 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 24:
              num15 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 25:
              num16 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 26:
              str11 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 27:
              num17 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 28:
              str12 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 29:
              num18 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 30:
              str13 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 31:
              num19 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 32:
              num20 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 33:
              num21 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 34:
              num22 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new JSON_TrophyParam()
      {
        iname = str1,
        name = str2,
        expr = str3,
        flg_quests = strArray1,
        ymd_start = num3,
        category = str4,
        disp = num4,
        type = num5,
        sval = strArray2,
        ival = num6,
        reward_item1_iname = str5,
        reward_item2_iname = str6,
        reward_item3_iname = str7,
        reward_item1_num = num7,
        reward_item2_num = num8,
        reward_item3_num = num9,
        reward_gold = num10,
        reward_coin = num11,
        reward_exp = num12,
        reward_stamina = num13,
        reward_artifact1_iname = str8,
        reward_artifact2_iname = str9,
        reward_artifact3_iname = str10,
        reward_artifact1_num = num14,
        reward_artifact2_num = num15,
        reward_artifact3_num = num16,
        parent_iname = str11,
        help = num17,
        reward_cc_1_iname = str12,
        reward_cc_1_num = num18,
        reward_cc_2_iname = str13,
        reward_cc_2_num = num19,
        prio_reward = num20,
        star_num = num21,
        is_ended = num22
      };
    }
  }
}
