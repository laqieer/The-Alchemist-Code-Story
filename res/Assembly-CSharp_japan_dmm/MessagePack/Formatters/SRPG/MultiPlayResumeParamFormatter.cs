// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.MultiPlayResumeParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class MultiPlayResumeParamFormatter : 
    IMessagePackFormatter<MultiPlayResumeParam>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public MultiPlayResumeParamFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "unit",
          0
        },
        {
          "gimmick",
          1
        },
        {
          "trick",
          2
        },
        {
          "rndseed",
          3
        },
        {
          "dmgrndseed",
          4
        },
        {
          "damageseed",
          5
        },
        {
          "seed",
          6
        },
        {
          "unitcastindex",
          7
        },
        {
          "unitstartcount",
          8
        },
        {
          "treasurecount",
          9
        },
        {
          "versusturn",
          10
        },
        {
          "resumeID",
          11
        },
        {
          "otherresume",
          12
        },
        {
          "scr_ev_trg",
          13
        },
        {
          "ctm",
          14
        },
        {
          "ctt",
          15
        },
        {
          "wti",
          16
        }
      };
      this.____stringByteKeys = new byte[17][]
      {
        MessagePackBinary.GetEncodedStringBytes("unit"),
        MessagePackBinary.GetEncodedStringBytes("gimmick"),
        MessagePackBinary.GetEncodedStringBytes("trick"),
        MessagePackBinary.GetEncodedStringBytes("rndseed"),
        MessagePackBinary.GetEncodedStringBytes("dmgrndseed"),
        MessagePackBinary.GetEncodedStringBytes("damageseed"),
        MessagePackBinary.GetEncodedStringBytes("seed"),
        MessagePackBinary.GetEncodedStringBytes("unitcastindex"),
        MessagePackBinary.GetEncodedStringBytes("unitstartcount"),
        MessagePackBinary.GetEncodedStringBytes("treasurecount"),
        MessagePackBinary.GetEncodedStringBytes("versusturn"),
        MessagePackBinary.GetEncodedStringBytes("resumeID"),
        MessagePackBinary.GetEncodedStringBytes("otherresume"),
        MessagePackBinary.GetEncodedStringBytes("scr_ev_trg"),
        MessagePackBinary.GetEncodedStringBytes("ctm"),
        MessagePackBinary.GetEncodedStringBytes("ctt"),
        MessagePackBinary.GetEncodedStringBytes("wti")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      MultiPlayResumeParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, 17);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<MultiPlayResumeUnitData[]>().Serialize(ref bytes, offset, value.unit, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<MultiPlayGimmickEventParam[]>().Serialize(ref bytes, offset, value.gimmick, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<MultiPlayTrickParam[]>().Serialize(ref bytes, offset, value.trick, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<uint[]>().Serialize(ref bytes, offset, value.rndseed, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<uint[]>().Serialize(ref bytes, offset, value.dmgrndseed, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += MessagePackBinary.WriteUInt32(ref bytes, offset, value.damageseed);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += MessagePackBinary.WriteUInt32(ref bytes, offset, value.seed);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.unitcastindex);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.unitstartcount);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.treasurecount);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += MessagePackBinary.WriteUInt32(ref bytes, offset, value.versusturn);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.resumeID);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.otherresume, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += formatterResolver.GetFormatterWithVerify<bool[]>().Serialize(ref bytes, offset, value.scr_ev_trg, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ctm);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ctt);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[16]);
      offset += formatterResolver.GetFormatterWithVerify<MultiPlayResumeParam.WeatherInfo>().Serialize(ref bytes, offset, value.wti, formatterResolver);
      return offset - num;
    }

    public MultiPlayResumeParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (MultiPlayResumeParam) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      MultiPlayResumeUnitData[] playResumeUnitDataArray = (MultiPlayResumeUnitData[]) null;
      MultiPlayGimmickEventParam[] gimmickEventParamArray = (MultiPlayGimmickEventParam[]) null;
      MultiPlayTrickParam[] multiPlayTrickParamArray = (MultiPlayTrickParam[]) null;
      uint[] numArray1 = (uint[]) null;
      uint[] numArray2 = (uint[]) null;
      uint num3 = 0;
      uint num4 = 0;
      int num5 = 0;
      int num6 = 0;
      int num7 = 0;
      uint num8 = 0;
      int num9 = 0;
      int[] numArray3 = (int[]) null;
      bool[] flagArray = (bool[]) null;
      int num10 = 0;
      int num11 = 0;
      MultiPlayResumeParam.WeatherInfo weatherInfo = (MultiPlayResumeParam.WeatherInfo) null;
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
              playResumeUnitDataArray = formatterResolver.GetFormatterWithVerify<MultiPlayResumeUnitData[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              gimmickEventParamArray = formatterResolver.GetFormatterWithVerify<MultiPlayGimmickEventParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              multiPlayTrickParamArray = formatterResolver.GetFormatterWithVerify<MultiPlayTrickParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              numArray1 = formatterResolver.GetFormatterWithVerify<uint[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              numArray2 = formatterResolver.GetFormatterWithVerify<uint[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 5:
              num3 = MessagePackBinary.ReadUInt32(bytes, offset, out readSize);
              break;
            case 6:
              num4 = MessagePackBinary.ReadUInt32(bytes, offset, out readSize);
              break;
            case 7:
              num5 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 8:
              num6 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 9:
              num7 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 10:
              num8 = MessagePackBinary.ReadUInt32(bytes, offset, out readSize);
              break;
            case 11:
              num9 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 12:
              numArray3 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 13:
              flagArray = formatterResolver.GetFormatterWithVerify<bool[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 14:
              num10 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 15:
              num11 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 16:
              weatherInfo = formatterResolver.GetFormatterWithVerify<MultiPlayResumeParam.WeatherInfo>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new MultiPlayResumeParam()
      {
        unit = playResumeUnitDataArray,
        gimmick = gimmickEventParamArray,
        trick = multiPlayTrickParamArray,
        rndseed = numArray1,
        dmgrndseed = numArray2,
        damageseed = num3,
        seed = num4,
        unitcastindex = num5,
        unitstartcount = num6,
        treasurecount = num7,
        versusturn = num8,
        resumeID = num9,
        otherresume = numArray3,
        scr_ev_trg = flagArray,
        ctm = num10,
        ctt = num11,
        wti = weatherInfo
      };
    }
  }
}
