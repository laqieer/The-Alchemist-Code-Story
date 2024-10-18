// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.VersusDraftList_VersusDraftMessageDataFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class VersusDraftList_VersusDraftMessageDataFormatter : 
    IMessagePackFormatter<VersusDraftList.VersusDraftMessageData>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public VersusDraftList_VersusDraftMessageDataFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "uidx0",
          0
        },
        {
          "uidx1",
          1
        },
        {
          "uidx2",
          2
        },
        {
          "qst",
          3
        },
        {
          "sq",
          4
        },
        {
          "h",
          5
        },
        {
          "b",
          6
        },
        {
          "pidx",
          7
        },
        {
          "pid",
          8
        },
        {
          "uid",
          9
        },
        {
          "c",
          10
        },
        {
          "u",
          11
        },
        {
          "s",
          12
        },
        {
          "i",
          13
        },
        {
          "gx",
          14
        },
        {
          "gy",
          15
        },
        {
          "ul",
          16
        },
        {
          "d",
          17
        },
        {
          "x",
          18
        },
        {
          "z",
          19
        },
        {
          "r",
          20
        }
      };
      this.____stringByteKeys = new byte[21][]
      {
        MessagePackBinary.GetEncodedStringBytes("uidx0"),
        MessagePackBinary.GetEncodedStringBytes("uidx1"),
        MessagePackBinary.GetEncodedStringBytes("uidx2"),
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
      VersusDraftList.VersusDraftMessageData value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, 21);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.uidx0);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.uidx1);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.uidx2);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.qst, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.sq);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.h);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.b);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.pidx);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.pid);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.uid);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.c, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.u, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.s, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.i, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.gx, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.gy, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[16]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.ul, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[17]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.d, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[18]);
      offset += formatterResolver.GetFormatterWithVerify<float[]>().Serialize(ref bytes, offset, value.x, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[19]);
      offset += formatterResolver.GetFormatterWithVerify<float[]>().Serialize(ref bytes, offset, value.z, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[20]);
      offset += formatterResolver.GetFormatterWithVerify<float[]>().Serialize(ref bytes, offset, value.r, formatterResolver);
      return offset - num;
    }

    public VersusDraftList.VersusDraftMessageData Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (VersusDraftList.VersusDraftMessageData) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      int num3 = 0;
      int num4 = 0;
      int num5 = 0;
      string str = (string) null;
      int num6 = 0;
      int num7 = 0;
      int num8 = 0;
      int num9 = 0;
      int num10 = 0;
      int num11 = 0;
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
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 1:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 2:
              num5 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 3:
              str = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
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
              numArray1 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 11:
              numArray2 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 12:
              strArray1 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 13:
              strArray2 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 14:
              numArray3 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 15:
              numArray4 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 16:
              numArray5 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 17:
              numArray6 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 18:
              numArray7 = formatterResolver.GetFormatterWithVerify<float[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 19:
              numArray8 = formatterResolver.GetFormatterWithVerify<float[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 20:
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
      VersusDraftList.VersusDraftMessageData draftMessageData = new VersusDraftList.VersusDraftMessageData();
      draftMessageData.uidx0 = num3;
      draftMessageData.uidx1 = num4;
      draftMessageData.uidx2 = num5;
      draftMessageData.qst = str;
      draftMessageData.sq = num6;
      draftMessageData.h = num7;
      draftMessageData.b = num8;
      draftMessageData.pidx = num9;
      draftMessageData.pid = num10;
      draftMessageData.uid = num11;
      draftMessageData.c = numArray1;
      draftMessageData.u = numArray2;
      draftMessageData.s = strArray1;
      draftMessageData.i = strArray2;
      draftMessageData.gx = numArray3;
      draftMessageData.gy = numArray4;
      draftMessageData.ul = numArray5;
      draftMessageData.d = numArray6;
      draftMessageData.x = numArray7;
      draftMessageData.z = numArray8;
      draftMessageData.r = numArray9;
      return draftMessageData;
    }
  }
}
