// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.MultiPlayResumeBuffFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;
using System.Collections.Generic;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class MultiPlayResumeBuffFormatter : 
    IMessagePackFormatter<MultiPlayResumeBuff>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public MultiPlayResumeBuffFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "iname",
          0
        },
        {
          "turn",
          1
        },
        {
          "unitindex",
          2
        },
        {
          "checkunit",
          3
        },
        {
          "timing",
          4
        },
        {
          "passive",
          5
        },
        {
          "condition",
          6
        },
        {
          "type",
          7
        },
        {
          "vtp",
          8
        },
        {
          "calc",
          9
        },
        {
          "curse",
          10
        },
        {
          "skilltarget",
          11
        },
        {
          "bc_id",
          12
        },
        {
          "lid",
          13
        },
        {
          "ubc",
          14
        },
        {
          "atl",
          15
        },
        {
          "rsl",
          16
        }
      };
      this.____stringByteKeys = new byte[17][]
      {
        MessagePackBinary.GetEncodedStringBytes("iname"),
        MessagePackBinary.GetEncodedStringBytes("turn"),
        MessagePackBinary.GetEncodedStringBytes("unitindex"),
        MessagePackBinary.GetEncodedStringBytes("checkunit"),
        MessagePackBinary.GetEncodedStringBytes("timing"),
        MessagePackBinary.GetEncodedStringBytes("passive"),
        MessagePackBinary.GetEncodedStringBytes("condition"),
        MessagePackBinary.GetEncodedStringBytes("type"),
        MessagePackBinary.GetEncodedStringBytes("vtp"),
        MessagePackBinary.GetEncodedStringBytes("calc"),
        MessagePackBinary.GetEncodedStringBytes("curse"),
        MessagePackBinary.GetEncodedStringBytes("skilltarget"),
        MessagePackBinary.GetEncodedStringBytes("bc_id"),
        MessagePackBinary.GetEncodedStringBytes("lid"),
        MessagePackBinary.GetEncodedStringBytes("ubc"),
        MessagePackBinary.GetEncodedStringBytes("atl"),
        MessagePackBinary.GetEncodedStringBytes("rsl")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      MultiPlayResumeBuff value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, 17);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.turn);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.unitindex);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.checkunit);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.timing);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.passive);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.condition);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.type);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.vtp);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.calc);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.curse);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.skilltarget);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.bc_id, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += MessagePackBinary.WriteUInt32(ref bytes, offset, value.lid);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ubc);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
      offset += formatterResolver.GetFormatterWithVerify<List<int>>().Serialize(ref bytes, offset, value.atl, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[16]);
      offset += formatterResolver.GetFormatterWithVerify<List<MultiPlayResumeBuff.ResistStatus>>().Serialize(ref bytes, offset, value.rsl, formatterResolver);
      return offset - num;
    }

    public MultiPlayResumeBuff Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (MultiPlayResumeBuff) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      string str1 = (string) null;
      int num3 = 0;
      int num4 = 0;
      int num5 = 0;
      int num6 = 0;
      bool flag = false;
      int num7 = 0;
      int num8 = 0;
      int num9 = 0;
      int num10 = 0;
      int num11 = 0;
      int num12 = 0;
      string str2 = (string) null;
      uint num13 = 0;
      int num14 = 0;
      List<int> intList = (List<int>) null;
      List<MultiPlayResumeBuff.ResistStatus> resistStatusList = (List<MultiPlayResumeBuff.ResistStatus>) null;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num15;
        if (!this.____keyMapping.TryGetValueSafe(key, out num15))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num15)
          {
            case 0:
              str1 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
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
              flag = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 6:
              num7 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 7:
              num8 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 8:
              num9 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 9:
              num10 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 10:
              num11 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 11:
              num12 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 12:
              str2 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 13:
              num13 = MessagePackBinary.ReadUInt32(bytes, offset, out readSize);
              break;
            case 14:
              num14 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 15:
              intList = formatterResolver.GetFormatterWithVerify<List<int>>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 16:
              resistStatusList = formatterResolver.GetFormatterWithVerify<List<MultiPlayResumeBuff.ResistStatus>>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new MultiPlayResumeBuff()
      {
        iname = str1,
        turn = num3,
        unitindex = num4,
        checkunit = num5,
        timing = num6,
        passive = flag,
        condition = num7,
        type = num8,
        vtp = num9,
        calc = num10,
        curse = num11,
        skilltarget = num12,
        bc_id = str2,
        lid = num13,
        ubc = num14,
        atl = intList,
        rsl = resistStatusList
      };
    }
  }
}
