// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.JSON_VersusEnableTimeParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class JSON_VersusEnableTimeParamFormatter : 
    IMessagePackFormatter<JSON_VersusEnableTimeParam>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public JSON_VersusEnableTimeParamFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "id",
          0
        },
        {
          "mode",
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
          "draft_id",
          4
        },
        {
          "draft_type",
          5
        },
        {
          "friend_draft_ids",
          6
        },
        {
          "schedule",
          7
        }
      };
      this.____stringByteKeys = new byte[8][]
      {
        MessagePackBinary.GetEncodedStringBytes("id"),
        MessagePackBinary.GetEncodedStringBytes("mode"),
        MessagePackBinary.GetEncodedStringBytes("begin_at"),
        MessagePackBinary.GetEncodedStringBytes("end_at"),
        MessagePackBinary.GetEncodedStringBytes("draft_id"),
        MessagePackBinary.GetEncodedStringBytes("draft_type"),
        MessagePackBinary.GetEncodedStringBytes("friend_draft_ids"),
        MessagePackBinary.GetEncodedStringBytes("schedule")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      JSON_VersusEnableTimeParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 8);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.id);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mode);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.begin_at, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.end_at, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.draft_id);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.draft_type);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.friend_draft_ids, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_VersusEnableTimeScheduleParam[]>().Serialize(ref bytes, offset, value.schedule, formatterResolver);
      return offset - num;
    }

    public JSON_VersusEnableTimeParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (JSON_VersusEnableTimeParam) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      int num3 = 0;
      int num4 = 0;
      string str1 = (string) null;
      string str2 = (string) null;
      int num5 = 0;
      int num6 = 0;
      int[] numArray = (int[]) null;
      JSON_VersusEnableTimeScheduleParam[] timeScheduleParamArray = (JSON_VersusEnableTimeScheduleParam[]) null;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num7;
        if (!this.____keyMapping.TryGetValueSafe(key, out num7))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num7)
          {
            case 0:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 1:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 2:
              str1 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              str2 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              num5 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 5:
              num6 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 6:
              numArray = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 7:
              timeScheduleParamArray = formatterResolver.GetFormatterWithVerify<JSON_VersusEnableTimeScheduleParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new JSON_VersusEnableTimeParam()
      {
        id = num3,
        mode = num4,
        begin_at = str1,
        end_at = str2,
        draft_id = num5,
        draft_type = num6,
        friend_draft_ids = numArray,
        schedule = timeScheduleParamArray
      };
    }
  }
}
