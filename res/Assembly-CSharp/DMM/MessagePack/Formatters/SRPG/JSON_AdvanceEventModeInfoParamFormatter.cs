// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.JSON_AdvanceEventModeInfoParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class JSON_AdvanceEventModeInfoParamFormatter : 
    IMessagePackFormatter<JSON_AdvanceEventModeInfoParam>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public JSON_AdvanceEventModeInfoParamFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "star_id",
          0
        },
        {
          "liberation_qno",
          1
        },
        {
          "boss_unit_id",
          2
        },
        {
          "boss_hp",
          3
        },
        {
          "boss_ch_item_id",
          4
        },
        {
          "boss_ch_item_num",
          5
        },
        {
          "boss_reward_id",
          6
        },
        {
          "mode_ui_index",
          7
        },
        {
          "lap_boss_id",
          8
        }
      };
      this.____stringByteKeys = new byte[9][]
      {
        MessagePackBinary.GetEncodedStringBytes("star_id"),
        MessagePackBinary.GetEncodedStringBytes("liberation_qno"),
        MessagePackBinary.GetEncodedStringBytes("boss_unit_id"),
        MessagePackBinary.GetEncodedStringBytes("boss_hp"),
        MessagePackBinary.GetEncodedStringBytes("boss_ch_item_id"),
        MessagePackBinary.GetEncodedStringBytes("boss_ch_item_num"),
        MessagePackBinary.GetEncodedStringBytes("boss_reward_id"),
        MessagePackBinary.GetEncodedStringBytes("mode_ui_index"),
        MessagePackBinary.GetEncodedStringBytes("lap_boss_id")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      JSON_AdvanceEventModeInfoParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 9);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.star_id, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.liberation_qno);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.boss_unit_id, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.boss_hp);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.boss_ch_item_id, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.boss_ch_item_num);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.boss_reward_id, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mode_ui_index);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.lap_boss_id, formatterResolver);
      return offset - num;
    }

    public JSON_AdvanceEventModeInfoParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (JSON_AdvanceEventModeInfoParam) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      string str1 = (string) null;
      int num3 = 0;
      string str2 = (string) null;
      int num4 = 0;
      string str3 = (string) null;
      int num5 = 0;
      string str4 = (string) null;
      int num6 = 0;
      string str5 = (string) null;
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
              str1 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 2:
              str2 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 4:
              str3 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 5:
              num5 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 6:
              str4 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 7:
              num6 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 8:
              str5 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new JSON_AdvanceEventModeInfoParam()
      {
        star_id = str1,
        liberation_qno = num3,
        boss_unit_id = str2,
        boss_hp = num4,
        boss_ch_item_id = str3,
        boss_ch_item_num = num5,
        boss_reward_id = str4,
        mode_ui_index = num6,
        lap_boss_id = str5
      };
    }
  }
}
