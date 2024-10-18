// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.Json_MyPhotonPlayerBinaryParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class Json_MyPhotonPlayerBinaryParamFormatter : 
    IMessagePackFormatter<Json_MyPhotonPlayerBinaryParam>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public Json_MyPhotonPlayerBinaryParamFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "playerID",
          0
        },
        {
          "playerIndex",
          1
        },
        {
          "playerName",
          2
        },
        {
          "playerLevel",
          3
        },
        {
          "FUID",
          4
        },
        {
          "UID",
          5
        },
        {
          "totalAtk",
          6
        },
        {
          "totalStatus",
          7
        },
        {
          "rankpoint",
          8
        },
        {
          "award",
          9
        },
        {
          "state",
          10
        },
        {
          "rankmatch_score",
          11
        },
        {
          "support_unit",
          12
        },
        {
          "draft_id",
          13
        },
        {
          "units",
          14
        },
        {
          "leaderID",
          15
        }
      };
      this.____stringByteKeys = new byte[16][]
      {
        MessagePackBinary.GetEncodedStringBytes("playerID"),
        MessagePackBinary.GetEncodedStringBytes("playerIndex"),
        MessagePackBinary.GetEncodedStringBytes("playerName"),
        MessagePackBinary.GetEncodedStringBytes("playerLevel"),
        MessagePackBinary.GetEncodedStringBytes("FUID"),
        MessagePackBinary.GetEncodedStringBytes("UID"),
        MessagePackBinary.GetEncodedStringBytes("totalAtk"),
        MessagePackBinary.GetEncodedStringBytes("totalStatus"),
        MessagePackBinary.GetEncodedStringBytes("rankpoint"),
        MessagePackBinary.GetEncodedStringBytes("award"),
        MessagePackBinary.GetEncodedStringBytes("state"),
        MessagePackBinary.GetEncodedStringBytes("rankmatch_score"),
        MessagePackBinary.GetEncodedStringBytes("support_unit"),
        MessagePackBinary.GetEncodedStringBytes("draft_id"),
        MessagePackBinary.GetEncodedStringBytes("units"),
        MessagePackBinary.GetEncodedStringBytes("leaderID")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      Json_MyPhotonPlayerBinaryParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, 16);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.playerID);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.playerIndex);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.playerName, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.playerLevel);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.FUID, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.UID, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.totalAtk);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.totalStatus);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rankpoint);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.award, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.state);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rankmatch_score);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.support_unit, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.draft_id);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += formatterResolver.GetFormatterWithVerify<Json_MyPhotonPlayerBinaryParam.UnitDataElem[]>().Serialize(ref bytes, offset, value.units, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.leaderID);
      return offset - num;
    }

    public Json_MyPhotonPlayerBinaryParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (Json_MyPhotonPlayerBinaryParam) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      int num3 = 0;
      int num4 = 0;
      string str1 = (string) null;
      int num5 = 0;
      string str2 = (string) null;
      string str3 = (string) null;
      int num6 = 0;
      int num7 = 0;
      int num8 = 0;
      string str4 = (string) null;
      int num9 = 0;
      int num10 = 0;
      string str5 = (string) null;
      int num11 = 0;
      Json_MyPhotonPlayerBinaryParam.UnitDataElem[] unitDataElemArray = (Json_MyPhotonPlayerBinaryParam.UnitDataElem[]) null;
      int num12 = 0;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num13;
        if (!this.____keyMapping.TryGetValueSafe(key, out num13))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num13)
          {
            case 0:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 1:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 2:
              str1 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              num5 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 4:
              str2 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 5:
              str3 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 6:
              num6 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 7:
              num7 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 8:
              num8 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 9:
              str4 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 10:
              num9 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 11:
              num10 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 12:
              str5 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 13:
              num11 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 14:
              unitDataElemArray = formatterResolver.GetFormatterWithVerify<Json_MyPhotonPlayerBinaryParam.UnitDataElem[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 15:
              num12 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new Json_MyPhotonPlayerBinaryParam()
      {
        playerID = num3,
        playerIndex = num4,
        playerName = str1,
        playerLevel = num5,
        FUID = str2,
        UID = str3,
        totalAtk = num6,
        totalStatus = num7,
        rankpoint = num8,
        award = str4,
        state = num9,
        rankmatch_score = num10,
        support_unit = str5,
        draft_id = num11,
        units = unitDataElemArray,
        leaderID = num12
      };
    }
  }
}
