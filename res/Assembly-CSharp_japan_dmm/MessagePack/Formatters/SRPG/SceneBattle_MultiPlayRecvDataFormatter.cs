// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.SceneBattle_MultiPlayRecvDataFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class SceneBattle_MultiPlayRecvDataFormatter : 
    IMessagePackFormatter<SceneBattle.MultiPlayRecvData>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public SceneBattle_MultiPlayRecvDataFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "qst",
          0
        },
        {
          "sq",
          1
        },
        {
          "h",
          2
        },
        {
          "b",
          3
        },
        {
          "pidx",
          4
        },
        {
          "pid",
          5
        },
        {
          "uid",
          6
        },
        {
          "c",
          7
        },
        {
          "u",
          8
        },
        {
          "s",
          9
        },
        {
          "i",
          10
        },
        {
          "gx",
          11
        },
        {
          "gy",
          12
        },
        {
          "ul",
          13
        },
        {
          "d",
          14
        },
        {
          "x",
          15
        },
        {
          "z",
          16
        },
        {
          "r",
          17
        }
      };
      this.____stringByteKeys = new byte[18][]
      {
        MessagePackBinary.GetEncodedStringBytes("qst"),
        MessagePackBinary.GetEncodedStringBytes("sq"),
        MessagePackBinary.GetEncodedStringBytes("h"),
        MessagePackBinary.GetEncodedStringBytes("b"),
        MessagePackBinary.GetEncodedStringBytes("pidx"),
        MessagePackBinary.GetEncodedStringBytes("pid"),
        MessagePackBinary.GetEncodedStringBytes("uid"),
        MessagePackBinary.GetEncodedStringBytes("c"),
        MessagePackBinary.GetEncodedStringBytes("u"),
        MessagePackBinary.GetEncodedStringBytes("s"),
        MessagePackBinary.GetEncodedStringBytes("i"),
        MessagePackBinary.GetEncodedStringBytes("gx"),
        MessagePackBinary.GetEncodedStringBytes("gy"),
        MessagePackBinary.GetEncodedStringBytes("ul"),
        MessagePackBinary.GetEncodedStringBytes("d"),
        MessagePackBinary.GetEncodedStringBytes("x"),
        MessagePackBinary.GetEncodedStringBytes("z"),
        MessagePackBinary.GetEncodedStringBytes("r")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      SceneBattle.MultiPlayRecvData value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, 18);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.qst, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.sq);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.h);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.b);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.pidx);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.pid);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.uid);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.c, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.u, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.s, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.i, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.gx, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.gy, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.ul, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.d, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
      offset += formatterResolver.GetFormatterWithVerify<float[]>().Serialize(ref bytes, offset, value.x, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[16]);
      offset += formatterResolver.GetFormatterWithVerify<float[]>().Serialize(ref bytes, offset, value.z, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[17]);
      offset += formatterResolver.GetFormatterWithVerify<float[]>().Serialize(ref bytes, offset, value.r, formatterResolver);
      return offset - num;
    }

    public SceneBattle.MultiPlayRecvData Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (SceneBattle.MultiPlayRecvData) null;
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
      int[] numArray1 = (int[]) null;
      int[] numArray2 = (int[]) null;
      string[] strArray1 = (string[]) null;
      string[] strArray2 = (string[]) null;
      int[] numArray3 = (int[]) null;
      int[] numArray4 = (int[]) null;
      int[] numArray5 = (int[]) null;
      int[] numArray6 = (int[]) null;
      float[] numArray7 = (float[]) null;
      float[] numArray8 = (float[]) null;
      float[] numArray9 = (float[]) null;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num9;
        if (!this.____keyMapping.TryGetValueSafe(key, out num9))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num9)
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
              numArray1 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 8:
              numArray2 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 9:
              strArray1 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 10:
              strArray2 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 11:
              numArray3 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 12:
              numArray4 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 13:
              numArray5 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 14:
              numArray6 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 15:
              numArray7 = formatterResolver.GetFormatterWithVerify<float[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 16:
              numArray8 = formatterResolver.GetFormatterWithVerify<float[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 17:
              numArray9 = formatterResolver.GetFormatterWithVerify<float[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new SceneBattle.MultiPlayRecvData()
      {
        qst = str,
        sq = num3,
        h = num4,
        b = num5,
        pidx = num6,
        pid = num7,
        uid = num8,
        c = numArray1,
        u = numArray2,
        s = strArray1,
        i = strArray2,
        gx = numArray3,
        gy = numArray4,
        ul = numArray5,
        d = numArray6,
        x = numArray7,
        z = numArray8,
        r = numArray9
      };
    }
  }
}
