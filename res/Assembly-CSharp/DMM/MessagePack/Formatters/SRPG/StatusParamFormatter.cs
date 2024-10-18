// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.StatusParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class StatusParamFormatter : 
    IMessagePackFormatter<StatusParam>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public StatusParamFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "Length",
          0
        },
        {
          "hp",
          1
        },
        {
          "mp",
          2
        },
        {
          "imp",
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
          "rec",
          8
        },
        {
          "dex",
          9
        },
        {
          "spd",
          10
        },
        {
          "cri",
          11
        },
        {
          "luk",
          12
        },
        {
          "mov",
          13
        },
        {
          "jmp",
          14
        },
        {
          "total",
          15
        },
        {
          "values_hp",
          16
        },
        {
          "values",
          17
        }
      };
      this.____stringByteKeys = new byte[18][]
      {
        MessagePackBinary.GetEncodedStringBytes("Length"),
        MessagePackBinary.GetEncodedStringBytes("hp"),
        MessagePackBinary.GetEncodedStringBytes("mp"),
        MessagePackBinary.GetEncodedStringBytes("imp"),
        MessagePackBinary.GetEncodedStringBytes("atk"),
        MessagePackBinary.GetEncodedStringBytes("def"),
        MessagePackBinary.GetEncodedStringBytes("mag"),
        MessagePackBinary.GetEncodedStringBytes("mnd"),
        MessagePackBinary.GetEncodedStringBytes("rec"),
        MessagePackBinary.GetEncodedStringBytes("dex"),
        MessagePackBinary.GetEncodedStringBytes("spd"),
        MessagePackBinary.GetEncodedStringBytes("cri"),
        MessagePackBinary.GetEncodedStringBytes("luk"),
        MessagePackBinary.GetEncodedStringBytes("mov"),
        MessagePackBinary.GetEncodedStringBytes("jmp"),
        MessagePackBinary.GetEncodedStringBytes("total"),
        MessagePackBinary.GetEncodedStringBytes("values_hp"),
        MessagePackBinary.GetEncodedStringBytes("values")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      StatusParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, 18);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Length);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.hp, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<OShort>().Serialize(ref bytes, offset, value.mp, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<OShort>().Serialize(ref bytes, offset, value.imp, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<OShort>().Serialize(ref bytes, offset, value.atk, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<OShort>().Serialize(ref bytes, offset, value.def, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += formatterResolver.GetFormatterWithVerify<OShort>().Serialize(ref bytes, offset, value.mag, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += formatterResolver.GetFormatterWithVerify<OShort>().Serialize(ref bytes, offset, value.mnd, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += formatterResolver.GetFormatterWithVerify<OShort>().Serialize(ref bytes, offset, value.rec, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += formatterResolver.GetFormatterWithVerify<OShort>().Serialize(ref bytes, offset, value.dex, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += formatterResolver.GetFormatterWithVerify<OShort>().Serialize(ref bytes, offset, value.spd, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += formatterResolver.GetFormatterWithVerify<OShort>().Serialize(ref bytes, offset, value.cri, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += formatterResolver.GetFormatterWithVerify<OShort>().Serialize(ref bytes, offset, value.luk, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += formatterResolver.GetFormatterWithVerify<OShort>().Serialize(ref bytes, offset, value.mov, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += formatterResolver.GetFormatterWithVerify<OShort>().Serialize(ref bytes, offset, value.jmp, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.total);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[16]);
      offset += formatterResolver.GetFormatterWithVerify<OInt>().Serialize(ref bytes, offset, value.values_hp, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[17]);
      offset += formatterResolver.GetFormatterWithVerify<OShort[]>().Serialize(ref bytes, offset, value.values, formatterResolver);
      return offset - num;
    }

    public StatusParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (StatusParam) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      int num3 = 0;
      OInt oint1 = new OInt();
      OShort oshort1 = new OShort();
      OShort oshort2 = new OShort();
      OShort oshort3 = new OShort();
      OShort oshort4 = new OShort();
      OShort oshort5 = new OShort();
      OShort oshort6 = new OShort();
      OShort oshort7 = new OShort();
      OShort oshort8 = new OShort();
      OShort oshort9 = new OShort();
      OShort oshort10 = new OShort();
      OShort oshort11 = new OShort();
      OShort oshort12 = new OShort();
      OShort oshort13 = new OShort();
      int num4 = 0;
      OInt oint2 = new OInt();
      OShort[] oshortArray = (OShort[]) null;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num5;
        if (!this.____keyMapping.TryGetValueSafe(key, out num5))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num5)
          {
            case 0:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 1:
              oint1 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              oshort1 = formatterResolver.GetFormatterWithVerify<OShort>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              oshort2 = formatterResolver.GetFormatterWithVerify<OShort>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              oshort3 = formatterResolver.GetFormatterWithVerify<OShort>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 5:
              oshort4 = formatterResolver.GetFormatterWithVerify<OShort>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 6:
              oshort5 = formatterResolver.GetFormatterWithVerify<OShort>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 7:
              oshort6 = formatterResolver.GetFormatterWithVerify<OShort>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 8:
              oshort7 = formatterResolver.GetFormatterWithVerify<OShort>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 9:
              oshort8 = formatterResolver.GetFormatterWithVerify<OShort>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 10:
              oshort9 = formatterResolver.GetFormatterWithVerify<OShort>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 11:
              oshort10 = formatterResolver.GetFormatterWithVerify<OShort>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 12:
              oshort11 = formatterResolver.GetFormatterWithVerify<OShort>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 13:
              oshort12 = formatterResolver.GetFormatterWithVerify<OShort>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 14:
              oshort13 = formatterResolver.GetFormatterWithVerify<OShort>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 15:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 16:
              oint2 = formatterResolver.GetFormatterWithVerify<OInt>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 17:
              oshortArray = formatterResolver.GetFormatterWithVerify<OShort[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new StatusParam()
      {
        hp = oint1,
        mp = oshort1,
        imp = oshort2,
        atk = oshort3,
        def = oshort4,
        mag = oshort5,
        mnd = oshort6,
        rec = oshort7,
        dex = oshort8,
        spd = oshort9,
        cri = oshort10,
        luk = oshort11,
        mov = oshort12,
        jmp = oshort13,
        values_hp = oint2,
        values = oshortArray
      };
    }
  }
}
