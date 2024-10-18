// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.JSON_UnitRentalParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class JSON_UnitRentalParamFormatter : 
    IMessagePackFormatter<JSON_UnitRentalParam>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public JSON_UnitRentalParamFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "iname",
          0
        },
        {
          "unit",
          1
        },
        {
          "begin_at",
          2
        },
        {
          "end_at",
          3
        },
        {
          "pt_max",
          4
        },
        {
          "ptup_lv",
          5
        },
        {
          "ptup_evol",
          6
        },
        {
          "ptup_awake",
          7
        },
        {
          "ptup_job_lv",
          8
        },
        {
          "ptup_ability_lv",
          9
        },
        {
          "ptup_quest_main",
          10
        },
        {
          "ptup_quest_sub",
          11
        },
        {
          "notification",
          12
        },
        {
          "quest_infos",
          13
        }
      };
      this.____stringByteKeys = new byte[14][]
      {
        MessagePackBinary.GetEncodedStringBytes("iname"),
        MessagePackBinary.GetEncodedStringBytes("unit"),
        MessagePackBinary.GetEncodedStringBytes("begin_at"),
        MessagePackBinary.GetEncodedStringBytes("end_at"),
        MessagePackBinary.GetEncodedStringBytes("pt_max"),
        MessagePackBinary.GetEncodedStringBytes("ptup_lv"),
        MessagePackBinary.GetEncodedStringBytes("ptup_evol"),
        MessagePackBinary.GetEncodedStringBytes("ptup_awake"),
        MessagePackBinary.GetEncodedStringBytes("ptup_job_lv"),
        MessagePackBinary.GetEncodedStringBytes("ptup_ability_lv"),
        MessagePackBinary.GetEncodedStringBytes("ptup_quest_main"),
        MessagePackBinary.GetEncodedStringBytes("ptup_quest_sub"),
        MessagePackBinary.GetEncodedStringBytes("notification"),
        MessagePackBinary.GetEncodedStringBytes("quest_infos")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      JSON_UnitRentalParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 14);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.unit, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.begin_at, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.end_at, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.pt_max);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ptup_lv);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ptup_evol);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ptup_awake);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ptup_job_lv);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ptup_ability_lv);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ptup_quest_main);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ptup_quest_sub);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.notification, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_UnitRentalParam.QuestInfo[]>().Serialize(ref bytes, offset, value.quest_infos, formatterResolver);
      return offset - num;
    }

    public JSON_UnitRentalParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (JSON_UnitRentalParam) null;
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
      string str5 = (string) null;
      JSON_UnitRentalParam.QuestInfo[] questInfoArray = (JSON_UnitRentalParam.QuestInfo[]) null;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num11;
        if (!this.____keyMapping.TryGetValueSafe(key, out num11))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num11)
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
              str5 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 13:
              questInfoArray = formatterResolver.GetFormatterWithVerify<JSON_UnitRentalParam.QuestInfo[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new JSON_UnitRentalParam()
      {
        iname = str1,
        unit = str2,
        begin_at = str3,
        end_at = str4,
        pt_max = num3,
        ptup_lv = num4,
        ptup_evol = num5,
        ptup_awake = num6,
        ptup_job_lv = num7,
        ptup_ability_lv = num8,
        ptup_quest_main = num9,
        ptup_quest_sub = num10,
        notification = str5,
        quest_infos = questInfoArray
      };
    }
  }
}
