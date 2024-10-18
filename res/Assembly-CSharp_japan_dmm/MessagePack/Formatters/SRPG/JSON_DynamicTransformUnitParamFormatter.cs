// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.JSON_DynamicTransformUnitParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class JSON_DynamicTransformUnitParamFormatter : 
    IMessagePackFormatter<JSON_DynamicTransformUnitParam>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public JSON_DynamicTransformUnitParamFormatter()
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
          "tr_unit_id",
          2
        },
        {
          "turn",
          3
        },
        {
          "upper_to_abid",
          4
        },
        {
          "lower_to_abid",
          5
        },
        {
          "react_to_abid",
          6
        },
        {
          "is_no_wa",
          7
        },
        {
          "is_no_va",
          8
        },
        {
          "is_no_item",
          9
        },
        {
          "ct_eff",
          10
        },
        {
          "ct_dis_ms",
          11
        },
        {
          "ct_app_ms",
          12
        },
        {
          "is_tr_hpf",
          13
        },
        {
          "is_cc_hpf",
          14
        },
        {
          "is_inh_skin",
          15
        }
      };
      this.____stringByteKeys = new byte[16][]
      {
        MessagePackBinary.GetEncodedStringBytes("iname"),
        MessagePackBinary.GetEncodedStringBytes("name"),
        MessagePackBinary.GetEncodedStringBytes("tr_unit_id"),
        MessagePackBinary.GetEncodedStringBytes("turn"),
        MessagePackBinary.GetEncodedStringBytes("upper_to_abid"),
        MessagePackBinary.GetEncodedStringBytes("lower_to_abid"),
        MessagePackBinary.GetEncodedStringBytes("react_to_abid"),
        MessagePackBinary.GetEncodedStringBytes("is_no_wa"),
        MessagePackBinary.GetEncodedStringBytes("is_no_va"),
        MessagePackBinary.GetEncodedStringBytes("is_no_item"),
        MessagePackBinary.GetEncodedStringBytes("ct_eff"),
        MessagePackBinary.GetEncodedStringBytes("ct_dis_ms"),
        MessagePackBinary.GetEncodedStringBytes("ct_app_ms"),
        MessagePackBinary.GetEncodedStringBytes("is_tr_hpf"),
        MessagePackBinary.GetEncodedStringBytes("is_cc_hpf"),
        MessagePackBinary.GetEncodedStringBytes("is_inh_skin")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      JSON_DynamicTransformUnitParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, 16);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.name, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.tr_unit_id, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.turn);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.upper_to_abid, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.lower_to_abid, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.react_to_abid, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.is_no_wa);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.is_no_va);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.is_no_item);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.ct_eff, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ct_dis_ms);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ct_app_ms);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.is_tr_hpf);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.is_cc_hpf);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.is_inh_skin);
      return offset - num;
    }

    public JSON_DynamicTransformUnitParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (JSON_DynamicTransformUnitParam) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      string str1 = (string) null;
      string str2 = (string) null;
      string str3 = (string) null;
      int num3 = 0;
      string str4 = (string) null;
      string str5 = (string) null;
      string str6 = (string) null;
      int num4 = 0;
      int num5 = 0;
      int num6 = 0;
      string str7 = (string) null;
      int num7 = 0;
      int num8 = 0;
      int num9 = 0;
      int num10 = 0;
      int num11 = 0;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num12;
        if (!this.____keyMapping.TryGetValueSafe(key, out num12))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num12)
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
              str4 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 5:
              str5 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 6:
              str6 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 7:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 8:
              num5 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 9:
              num6 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 10:
              str7 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 11:
              num7 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 12:
              num8 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 13:
              num9 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 14:
              num10 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 15:
              num11 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new JSON_DynamicTransformUnitParam()
      {
        iname = str1,
        name = str2,
        tr_unit_id = str3,
        turn = num3,
        upper_to_abid = str4,
        lower_to_abid = str5,
        react_to_abid = str6,
        is_no_wa = num4,
        is_no_va = num5,
        is_no_item = num6,
        ct_eff = str7,
        ct_dis_ms = num7,
        ct_app_ms = num8,
        is_tr_hpf = num9,
        is_cc_hpf = num10,
        is_inh_skin = num11
      };
    }
  }
}
