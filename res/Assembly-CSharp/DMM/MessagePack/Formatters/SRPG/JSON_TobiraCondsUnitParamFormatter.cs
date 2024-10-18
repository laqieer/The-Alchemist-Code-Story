// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.JSON_TobiraCondsUnitParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class JSON_TobiraCondsUnitParamFormatter : 
    IMessagePackFormatter<JSON_TobiraCondsUnitParam>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public JSON_TobiraCondsUnitParamFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "id",
          0
        },
        {
          "unit_iname",
          1
        },
        {
          "lv",
          2
        },
        {
          "awake_lv",
          3
        },
        {
          "jobs",
          4
        },
        {
          "category",
          5
        },
        {
          "tobira_lv",
          6
        }
      };
      this.____stringByteKeys = new byte[7][]
      {
        MessagePackBinary.GetEncodedStringBytes("id"),
        MessagePackBinary.GetEncodedStringBytes("unit_iname"),
        MessagePackBinary.GetEncodedStringBytes("lv"),
        MessagePackBinary.GetEncodedStringBytes("awake_lv"),
        MessagePackBinary.GetEncodedStringBytes("jobs"),
        MessagePackBinary.GetEncodedStringBytes("category"),
        MessagePackBinary.GetEncodedStringBytes("tobira_lv")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      JSON_TobiraCondsUnitParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 7);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.id, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.unit_iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.lv);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.awake_lv);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_TobiraCondsUnitParam.JobCond[]>().Serialize(ref bytes, offset, value.jobs, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.category);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.tobira_lv);
      return offset - num;
    }

    public JSON_TobiraCondsUnitParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (JSON_TobiraCondsUnitParam) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      string str1 = (string) null;
      string str2 = (string) null;
      int num3 = 0;
      int num4 = 0;
      JSON_TobiraCondsUnitParam.JobCond[] jobCondArray = (JSON_TobiraCondsUnitParam.JobCond[]) null;
      int num5 = 0;
      int num6 = 0;
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
              str2 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 3:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 4:
              jobCondArray = formatterResolver.GetFormatterWithVerify<JSON_TobiraCondsUnitParam.JobCond[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 5:
              num5 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 6:
              num6 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new JSON_TobiraCondsUnitParam()
      {
        id = str1,
        unit_iname = str2,
        lv = num3,
        awake_lv = num4,
        jobs = jobCondArray,
        category = num5,
        tobira_lv = num6
      };
    }
  }
}
