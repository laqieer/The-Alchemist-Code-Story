// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.Json_AutoRepeatQuestDataFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class Json_AutoRepeatQuestDataFormatter : 
    IMessagePackFormatter<Json_AutoRepeatQuestData>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public Json_AutoRepeatQuestDataFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "qid",
          0
        },
        {
          "units",
          1
        },
        {
          "start_time",
          2
        },
        {
          "time_per_lap",
          3
        },
        {
          "lap_num",
          4
        },
        {
          "lap_num_not_box",
          5
        },
        {
          "is_full_box",
          6
        },
        {
          "stop_reason",
          7
        },
        {
          "current_lap_num",
          8
        },
        {
          "gold",
          9
        },
        {
          "drops",
          10
        },
        {
          "auto_repeat_check",
          11
        }
      };
      this.____stringByteKeys = new byte[12][]
      {
        MessagePackBinary.GetEncodedStringBytes("qid"),
        MessagePackBinary.GetEncodedStringBytes("units"),
        MessagePackBinary.GetEncodedStringBytes("start_time"),
        MessagePackBinary.GetEncodedStringBytes("time_per_lap"),
        MessagePackBinary.GetEncodedStringBytes("lap_num"),
        MessagePackBinary.GetEncodedStringBytes("lap_num_not_box"),
        MessagePackBinary.GetEncodedStringBytes("is_full_box"),
        MessagePackBinary.GetEncodedStringBytes("stop_reason"),
        MessagePackBinary.GetEncodedStringBytes("current_lap_num"),
        MessagePackBinary.GetEncodedStringBytes("gold"),
        MessagePackBinary.GetEncodedStringBytes("drops"),
        MessagePackBinary.GetEncodedStringBytes("auto_repeat_check")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      Json_AutoRepeatQuestData value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 12);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.qid, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.units, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.start_time, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.time_per_lap);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.lap_num);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.lap_num_not_box);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.is_full_box);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.stop_reason);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.current_lap_num);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.gold);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += formatterResolver.GetFormatterWithVerify<BattleCore.Json_BtlDrop[][]>().Serialize(ref bytes, offset, value.drops, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.auto_repeat_check);
      return offset - num;
    }

    public Json_AutoRepeatQuestData Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (Json_AutoRepeatQuestData) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      string str1 = (string) null;
      int[] numArray = (int[]) null;
      string str2 = (string) null;
      int num3 = 0;
      int num4 = 0;
      int num5 = 0;
      int num6 = 0;
      int num7 = 0;
      int num8 = 0;
      int num9 = 0;
      BattleCore.Json_BtlDrop[][] jsonBtlDropArray = (BattleCore.Json_BtlDrop[][]) null;
      int num10 = 0;
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
              numArray = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              str2 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
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
              num8 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 9:
              num9 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 10:
              jsonBtlDropArray = formatterResolver.GetFormatterWithVerify<BattleCore.Json_BtlDrop[][]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 11:
              num10 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new Json_AutoRepeatQuestData()
      {
        qid = str1,
        units = numArray,
        start_time = str2,
        time_per_lap = num3,
        lap_num = num4,
        lap_num_not_box = num5,
        is_full_box = num6,
        stop_reason = num7,
        current_lap_num = num8,
        gold = num9,
        drops = jsonBtlDropArray,
        auto_repeat_check = num10
      };
    }
  }
}
