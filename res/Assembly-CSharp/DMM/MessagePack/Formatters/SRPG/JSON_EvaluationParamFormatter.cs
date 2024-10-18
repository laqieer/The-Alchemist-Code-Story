// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.JSON_EvaluationParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class JSON_EvaluationParamFormatter : 
    IMessagePackFormatter<JSON_EvaluationParam>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public JSON_EvaluationParamFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "iname",
          0
        },
        {
          "val",
          1
        },
        {
          "hp",
          2
        },
        {
          "mp",
          3
        },
        {
          "atk",
          4
        },
        {
          "def",
          5
        },
        {
          "mag",
          6
        },
        {
          "mnd",
          7
        },
        {
          "dex",
          8
        },
        {
          "spd",
          9
        },
        {
          "cri",
          10
        },
        {
          "luk",
          11
        }
      };
      this.____stringByteKeys = new byte[12][]
      {
        MessagePackBinary.GetEncodedStringBytes("iname"),
        MessagePackBinary.GetEncodedStringBytes("val"),
        MessagePackBinary.GetEncodedStringBytes("hp"),
        MessagePackBinary.GetEncodedStringBytes("mp"),
        MessagePackBinary.GetEncodedStringBytes("atk"),
        MessagePackBinary.GetEncodedStringBytes("def"),
        MessagePackBinary.GetEncodedStringBytes("mag"),
        MessagePackBinary.GetEncodedStringBytes("mnd"),
        MessagePackBinary.GetEncodedStringBytes("dex"),
        MessagePackBinary.GetEncodedStringBytes("spd"),
        MessagePackBinary.GetEncodedStringBytes("cri"),
        MessagePackBinary.GetEncodedStringBytes("luk")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      JSON_EvaluationParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 12);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.val);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.hp);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mp);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.atk);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.def);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mag);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mnd);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.dex);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.spd);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.cri);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.luk);
      return offset - num;
    }

    public JSON_EvaluationParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (JSON_EvaluationParam) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      string str = (string) null;
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
      int num13 = 0;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num14;
        if (!this.____keyMapping.TryGetValueSafe(key, out num14))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num14)
          {
            case 0:
              str = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
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
              num7 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 6:
              num8 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 7:
              num9 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 8:
              num10 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 9:
              num11 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 10:
              num12 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 11:
              num13 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new JSON_EvaluationParam()
      {
        iname = str,
        val = num3,
        hp = num4,
        mp = num5,
        atk = num6,
        def = num7,
        mag = num8,
        mnd = num9,
        dex = num10,
        spd = num11,
        cri = num12,
        luk = num13
      };
    }
  }
}
