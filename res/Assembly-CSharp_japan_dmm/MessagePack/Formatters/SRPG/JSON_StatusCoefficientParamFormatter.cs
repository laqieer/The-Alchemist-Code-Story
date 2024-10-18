// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.JSON_StatusCoefficientParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class JSON_StatusCoefficientParamFormatter : 
    IMessagePackFormatter<JSON_StatusCoefficientParam>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public JSON_StatusCoefficientParamFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "hp",
          0
        },
        {
          "atk",
          1
        },
        {
          "def",
          2
        },
        {
          "matk",
          3
        },
        {
          "mdef",
          4
        },
        {
          "dex",
          5
        },
        {
          "spd",
          6
        },
        {
          "cri",
          7
        },
        {
          "luck",
          8
        },
        {
          "cmb",
          9
        },
        {
          "move",
          10
        },
        {
          "jmp",
          11
        }
      };
      this.____stringByteKeys = new byte[12][]
      {
        MessagePackBinary.GetEncodedStringBytes("hp"),
        MessagePackBinary.GetEncodedStringBytes("atk"),
        MessagePackBinary.GetEncodedStringBytes("def"),
        MessagePackBinary.GetEncodedStringBytes("matk"),
        MessagePackBinary.GetEncodedStringBytes("mdef"),
        MessagePackBinary.GetEncodedStringBytes("dex"),
        MessagePackBinary.GetEncodedStringBytes("spd"),
        MessagePackBinary.GetEncodedStringBytes("cri"),
        MessagePackBinary.GetEncodedStringBytes("luck"),
        MessagePackBinary.GetEncodedStringBytes("cmb"),
        MessagePackBinary.GetEncodedStringBytes("move"),
        MessagePackBinary.GetEncodedStringBytes("jmp")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      JSON_StatusCoefficientParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 12);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += MessagePackBinary.WriteSingle(ref bytes, offset, value.hp);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += MessagePackBinary.WriteSingle(ref bytes, offset, value.atk);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += MessagePackBinary.WriteSingle(ref bytes, offset, value.def);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += MessagePackBinary.WriteSingle(ref bytes, offset, value.matk);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += MessagePackBinary.WriteSingle(ref bytes, offset, value.mdef);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += MessagePackBinary.WriteSingle(ref bytes, offset, value.dex);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += MessagePackBinary.WriteSingle(ref bytes, offset, value.spd);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += MessagePackBinary.WriteSingle(ref bytes, offset, value.cri);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += MessagePackBinary.WriteSingle(ref bytes, offset, value.luck);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += MessagePackBinary.WriteSingle(ref bytes, offset, value.cmb);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += MessagePackBinary.WriteSingle(ref bytes, offset, value.move);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += MessagePackBinary.WriteSingle(ref bytes, offset, value.jmp);
      return offset - num;
    }

    public JSON_StatusCoefficientParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (JSON_StatusCoefficientParam) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      float num3 = 0.0f;
      float num4 = 0.0f;
      float num5 = 0.0f;
      float num6 = 0.0f;
      float num7 = 0.0f;
      float num8 = 0.0f;
      float num9 = 0.0f;
      float num10 = 0.0f;
      float num11 = 0.0f;
      float num12 = 0.0f;
      float num13 = 0.0f;
      float num14 = 0.0f;
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
              num3 = MessagePackBinary.ReadSingle(bytes, offset, out readSize);
              break;
            case 1:
              num4 = MessagePackBinary.ReadSingle(bytes, offset, out readSize);
              break;
            case 2:
              num5 = MessagePackBinary.ReadSingle(bytes, offset, out readSize);
              break;
            case 3:
              num6 = MessagePackBinary.ReadSingle(bytes, offset, out readSize);
              break;
            case 4:
              num7 = MessagePackBinary.ReadSingle(bytes, offset, out readSize);
              break;
            case 5:
              num8 = MessagePackBinary.ReadSingle(bytes, offset, out readSize);
              break;
            case 6:
              num9 = MessagePackBinary.ReadSingle(bytes, offset, out readSize);
              break;
            case 7:
              num10 = MessagePackBinary.ReadSingle(bytes, offset, out readSize);
              break;
            case 8:
              num11 = MessagePackBinary.ReadSingle(bytes, offset, out readSize);
              break;
            case 9:
              num12 = MessagePackBinary.ReadSingle(bytes, offset, out readSize);
              break;
            case 10:
              num13 = MessagePackBinary.ReadSingle(bytes, offset, out readSize);
              break;
            case 11:
              num14 = MessagePackBinary.ReadSingle(bytes, offset, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new JSON_StatusCoefficientParam()
      {
        hp = num3,
        atk = num4,
        def = num5,
        matk = num6,
        mdef = num7,
        dex = num8,
        spd = num9,
        cri = num10,
        luck = num11,
        cmb = num12,
        move = num13,
        jmp = num14
      };
    }
  }
}
