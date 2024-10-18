// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.JSON_GvGNPCUnitParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class JSON_GvGNPCUnitParamFormatter : 
    IMessagePackFormatter<JSON_GvGNPCUnitParam>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public JSON_GvGNPCUnitParamFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "id",
          0
        },
        {
          "draft_unit_id",
          1
        },
        {
          "weight",
          2
        },
        {
          "unit_iname",
          3
        },
        {
          "rare",
          4
        },
        {
          "awake",
          5
        },
        {
          "lv",
          6
        },
        {
          "select_job_idx",
          7
        },
        {
          "job1_iname",
          8
        },
        {
          "job1_lv",
          9
        },
        {
          "job1_equip",
          10
        },
        {
          "job2_iname",
          11
        },
        {
          "job2_lv",
          12
        },
        {
          "job2_equip",
          13
        },
        {
          "job3_iname",
          14
        },
        {
          "job3_lv",
          15
        },
        {
          "job3_equip",
          16
        },
        {
          "abil1_iname",
          17
        },
        {
          "abil1_lv",
          18
        },
        {
          "abil2_iname",
          19
        },
        {
          "abil2_lv",
          20
        },
        {
          "abil3_iname",
          21
        },
        {
          "abil3_lv",
          22
        },
        {
          "abil4_iname",
          23
        },
        {
          "abil4_lv",
          24
        },
        {
          "abil5_iname",
          25
        },
        {
          "abil5_lv",
          26
        },
        {
          "arti1_iname",
          27
        },
        {
          "arti1_rare",
          28
        },
        {
          "arti1_lv",
          29
        },
        {
          "arti2_iname",
          30
        },
        {
          "arti2_rare",
          31
        },
        {
          "arti2_lv",
          32
        },
        {
          "arti3_iname",
          33
        },
        {
          "arti3_rare",
          34
        },
        {
          "arti3_lv",
          35
        },
        {
          "card_iname",
          36
        },
        {
          "card_lv",
          37
        },
        {
          "card_leaderskill",
          38
        },
        {
          "card2_iname",
          39
        },
        {
          "card2_lv",
          40
        },
        {
          "door1_lv",
          41
        },
        {
          "door2_lv",
          42
        },
        {
          "door3_lv",
          43
        },
        {
          "door4_lv",
          44
        },
        {
          "door5_lv",
          45
        },
        {
          "door6_lv",
          46
        },
        {
          "door7_lv",
          47
        },
        {
          "master_abil",
          48
        },
        {
          "skin",
          49
        },
        {
          "clear_quest_iname",
          50
        }
      };
      this.____stringByteKeys = new byte[51][]
      {
        MessagePackBinary.GetEncodedStringBytes("id"),
        MessagePackBinary.GetEncodedStringBytes("draft_unit_id"),
        MessagePackBinary.GetEncodedStringBytes("weight"),
        MessagePackBinary.GetEncodedStringBytes("unit_iname"),
        MessagePackBinary.GetEncodedStringBytes("rare"),
        MessagePackBinary.GetEncodedStringBytes("awake"),
        MessagePackBinary.GetEncodedStringBytes("lv"),
        MessagePackBinary.GetEncodedStringBytes("select_job_idx"),
        MessagePackBinary.GetEncodedStringBytes("job1_iname"),
        MessagePackBinary.GetEncodedStringBytes("job1_lv"),
        MessagePackBinary.GetEncodedStringBytes("job1_equip"),
        MessagePackBinary.GetEncodedStringBytes("job2_iname"),
        MessagePackBinary.GetEncodedStringBytes("job2_lv"),
        MessagePackBinary.GetEncodedStringBytes("job2_equip"),
        MessagePackBinary.GetEncodedStringBytes("job3_iname"),
        MessagePackBinary.GetEncodedStringBytes("job3_lv"),
        MessagePackBinary.GetEncodedStringBytes("job3_equip"),
        MessagePackBinary.GetEncodedStringBytes("abil1_iname"),
        MessagePackBinary.GetEncodedStringBytes("abil1_lv"),
        MessagePackBinary.GetEncodedStringBytes("abil2_iname"),
        MessagePackBinary.GetEncodedStringBytes("abil2_lv"),
        MessagePackBinary.GetEncodedStringBytes("abil3_iname"),
        MessagePackBinary.GetEncodedStringBytes("abil3_lv"),
        MessagePackBinary.GetEncodedStringBytes("abil4_iname"),
        MessagePackBinary.GetEncodedStringBytes("abil4_lv"),
        MessagePackBinary.GetEncodedStringBytes("abil5_iname"),
        MessagePackBinary.GetEncodedStringBytes("abil5_lv"),
        MessagePackBinary.GetEncodedStringBytes("arti1_iname"),
        MessagePackBinary.GetEncodedStringBytes("arti1_rare"),
        MessagePackBinary.GetEncodedStringBytes("arti1_lv"),
        MessagePackBinary.GetEncodedStringBytes("arti2_iname"),
        MessagePackBinary.GetEncodedStringBytes("arti2_rare"),
        MessagePackBinary.GetEncodedStringBytes("arti2_lv"),
        MessagePackBinary.GetEncodedStringBytes("arti3_iname"),
        MessagePackBinary.GetEncodedStringBytes("arti3_rare"),
        MessagePackBinary.GetEncodedStringBytes("arti3_lv"),
        MessagePackBinary.GetEncodedStringBytes("card_iname"),
        MessagePackBinary.GetEncodedStringBytes("card_lv"),
        MessagePackBinary.GetEncodedStringBytes("card_leaderskill"),
        MessagePackBinary.GetEncodedStringBytes("card2_iname"),
        MessagePackBinary.GetEncodedStringBytes("card2_lv"),
        MessagePackBinary.GetEncodedStringBytes("door1_lv"),
        MessagePackBinary.GetEncodedStringBytes("door2_lv"),
        MessagePackBinary.GetEncodedStringBytes("door3_lv"),
        MessagePackBinary.GetEncodedStringBytes("door4_lv"),
        MessagePackBinary.GetEncodedStringBytes("door5_lv"),
        MessagePackBinary.GetEncodedStringBytes("door6_lv"),
        MessagePackBinary.GetEncodedStringBytes("door7_lv"),
        MessagePackBinary.GetEncodedStringBytes("master_abil"),
        MessagePackBinary.GetEncodedStringBytes("skin"),
        MessagePackBinary.GetEncodedStringBytes("clear_quest_iname")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      JSON_GvGNPCUnitParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, 51);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += MessagePackBinary.WriteInt64(ref bytes, offset, value.id);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += MessagePackBinary.WriteInt64(ref bytes, offset, value.draft_unit_id);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.weight);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.unit_iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rare);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.awake);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.lv);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.select_job_idx);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.job1_iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.job1_lv);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.job1_equip);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.job2_iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.job2_lv);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.job2_equip);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.job3_iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.job3_lv);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[16]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.job3_equip);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[17]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.abil1_iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[18]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.abil1_lv);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[19]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.abil2_iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[20]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.abil2_lv);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[21]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.abil3_iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[22]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.abil3_lv);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[23]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.abil4_iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[24]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.abil4_lv);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[25]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.abil5_iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[26]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.abil5_lv);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[27]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.arti1_iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[28]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.arti1_rare);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[29]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.arti1_lv);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[30]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.arti2_iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[31]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.arti2_rare);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[32]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.arti2_lv);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[33]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.arti3_iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[34]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.arti3_rare);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[35]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.arti3_lv);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[36]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.card_iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[37]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.card_lv);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[38]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.card_leaderskill);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[39]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.card2_iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[40]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.card2_lv);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[41]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.door1_lv);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[42]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.door2_lv);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[43]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.door3_lv);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[44]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.door4_lv);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[45]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.door5_lv);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[46]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.door6_lv);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[47]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.door7_lv);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[48]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.master_abil, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[49]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.skin, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[50]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.clear_quest_iname, formatterResolver);
      return offset - num;
    }

    public JSON_GvGNPCUnitParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (JSON_GvGNPCUnitParam) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      long num3 = 0;
      long num4 = 0;
      int num5 = 0;
      string str1 = (string) null;
      int num6 = 0;
      int num7 = 0;
      int num8 = 0;
      int num9 = 0;
      string str2 = (string) null;
      int num10 = 0;
      int num11 = 0;
      string str3 = (string) null;
      int num12 = 0;
      int num13 = 0;
      string str4 = (string) null;
      int num14 = 0;
      int num15 = 0;
      string str5 = (string) null;
      int num16 = 0;
      string str6 = (string) null;
      int num17 = 0;
      string str7 = (string) null;
      int num18 = 0;
      string str8 = (string) null;
      int num19 = 0;
      string str9 = (string) null;
      int num20 = 0;
      string str10 = (string) null;
      int num21 = 0;
      int num22 = 0;
      string str11 = (string) null;
      int num23 = 0;
      int num24 = 0;
      string str12 = (string) null;
      int num25 = 0;
      int num26 = 0;
      string str13 = (string) null;
      int num27 = 0;
      int num28 = 0;
      string str14 = (string) null;
      int num29 = 0;
      int num30 = 0;
      int num31 = 0;
      int num32 = 0;
      int num33 = 0;
      int num34 = 0;
      int num35 = 0;
      int num36 = 0;
      string str15 = (string) null;
      string str16 = (string) null;
      string[] strArray = (string[]) null;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num37;
        if (!this.____keyMapping.TryGetValueSafe(key, out num37))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num37)
          {
            case 0:
              num3 = MessagePackBinary.ReadInt64(bytes, offset, out readSize);
              break;
            case 1:
              num4 = MessagePackBinary.ReadInt64(bytes, offset, out readSize);
              break;
            case 2:
              num5 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 3:
              str1 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
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
              str2 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 9:
              num10 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 10:
              num11 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 11:
              str3 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 12:
              num12 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 13:
              num13 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 14:
              str4 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 15:
              num14 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 16:
              num15 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 17:
              str5 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 18:
              num16 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 19:
              str6 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 20:
              num17 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 21:
              str7 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 22:
              num18 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 23:
              str8 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 24:
              num19 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 25:
              str9 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 26:
              num20 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 27:
              str10 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 28:
              num21 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 29:
              num22 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 30:
              str11 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 31:
              num23 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 32:
              num24 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 33:
              str12 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 34:
              num25 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 35:
              num26 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 36:
              str13 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 37:
              num27 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 38:
              num28 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 39:
              str14 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 40:
              num29 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 41:
              num30 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 42:
              num31 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 43:
              num32 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 44:
              num33 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 45:
              num34 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 46:
              num35 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 47:
              num36 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 48:
              str15 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 49:
              str16 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 50:
              strArray = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      JSON_GvGNPCUnitParam jsonGvGnpcUnitParam = new JSON_GvGNPCUnitParam();
      jsonGvGnpcUnitParam.id = num3;
      jsonGvGnpcUnitParam.draft_unit_id = num4;
      jsonGvGnpcUnitParam.weight = num5;
      jsonGvGnpcUnitParam.unit_iname = str1;
      jsonGvGnpcUnitParam.rare = num6;
      jsonGvGnpcUnitParam.awake = num7;
      jsonGvGnpcUnitParam.lv = num8;
      jsonGvGnpcUnitParam.select_job_idx = num9;
      jsonGvGnpcUnitParam.job1_iname = str2;
      jsonGvGnpcUnitParam.job1_lv = num10;
      jsonGvGnpcUnitParam.job1_equip = num11;
      jsonGvGnpcUnitParam.job2_iname = str3;
      jsonGvGnpcUnitParam.job2_lv = num12;
      jsonGvGnpcUnitParam.job2_equip = num13;
      jsonGvGnpcUnitParam.job3_iname = str4;
      jsonGvGnpcUnitParam.job3_lv = num14;
      jsonGvGnpcUnitParam.job3_equip = num15;
      jsonGvGnpcUnitParam.abil1_iname = str5;
      jsonGvGnpcUnitParam.abil1_lv = num16;
      jsonGvGnpcUnitParam.abil2_iname = str6;
      jsonGvGnpcUnitParam.abil2_lv = num17;
      jsonGvGnpcUnitParam.abil3_iname = str7;
      jsonGvGnpcUnitParam.abil3_lv = num18;
      jsonGvGnpcUnitParam.abil4_iname = str8;
      jsonGvGnpcUnitParam.abil4_lv = num19;
      jsonGvGnpcUnitParam.abil5_iname = str9;
      jsonGvGnpcUnitParam.abil5_lv = num20;
      jsonGvGnpcUnitParam.arti1_iname = str10;
      jsonGvGnpcUnitParam.arti1_rare = num21;
      jsonGvGnpcUnitParam.arti1_lv = num22;
      jsonGvGnpcUnitParam.arti2_iname = str11;
      jsonGvGnpcUnitParam.arti2_rare = num23;
      jsonGvGnpcUnitParam.arti2_lv = num24;
      jsonGvGnpcUnitParam.arti3_iname = str12;
      jsonGvGnpcUnitParam.arti3_rare = num25;
      jsonGvGnpcUnitParam.arti3_lv = num26;
      jsonGvGnpcUnitParam.card_iname = str13;
      jsonGvGnpcUnitParam.card_lv = num27;
      jsonGvGnpcUnitParam.card_leaderskill = num28;
      jsonGvGnpcUnitParam.card2_iname = str14;
      jsonGvGnpcUnitParam.card2_lv = num29;
      jsonGvGnpcUnitParam.door1_lv = num30;
      jsonGvGnpcUnitParam.door2_lv = num31;
      jsonGvGnpcUnitParam.door3_lv = num32;
      jsonGvGnpcUnitParam.door4_lv = num33;
      jsonGvGnpcUnitParam.door5_lv = num34;
      jsonGvGnpcUnitParam.door6_lv = num35;
      jsonGvGnpcUnitParam.door7_lv = num36;
      jsonGvGnpcUnitParam.master_abil = str15;
      jsonGvGnpcUnitParam.skin = str16;
      jsonGvGnpcUnitParam.clear_quest_iname = strArray;
      return jsonGvGnpcUnitParam;
    }
  }
}
