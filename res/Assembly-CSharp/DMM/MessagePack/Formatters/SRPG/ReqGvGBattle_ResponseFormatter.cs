// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.ReqGvGBattle_ResponseFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class ReqGvGBattle_ResponseFormatter : 
    IMessagePackFormatter<ReqGvGBattle.Response>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public ReqGvGBattle_ResponseFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "offense",
          0
        },
        {
          "win_num",
          1
        },
        {
          "beat_num",
          2
        },
        {
          "seed",
          3
        },
        {
          "used_units",
          4
        },
        {
          "cool_time",
          5
        },
        {
          "used_cards",
          6
        },
        {
          "used_artifacts",
          7
        },
        {
          "used_runes",
          8
        },
        {
          "maxActionNum",
          9
        }
      };
      this.____stringByteKeys = new byte[10][]
      {
        MessagePackBinary.GetEncodedStringBytes("offense"),
        MessagePackBinary.GetEncodedStringBytes("win_num"),
        MessagePackBinary.GetEncodedStringBytes("beat_num"),
        MessagePackBinary.GetEncodedStringBytes("seed"),
        MessagePackBinary.GetEncodedStringBytes("used_units"),
        MessagePackBinary.GetEncodedStringBytes("cool_time"),
        MessagePackBinary.GetEncodedStringBytes("used_cards"),
        MessagePackBinary.GetEncodedStringBytes("used_artifacts"),
        MessagePackBinary.GetEncodedStringBytes("used_runes"),
        MessagePackBinary.GetEncodedStringBytes("maxActionNum")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      ReqGvGBattle.Response value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 10);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GvGPartyUnit[]>().Serialize(ref bytes, offset, value.offense, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.win_num);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.beat_num);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.seed);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GvGUsedUnitData[]>().Serialize(ref bytes, offset, value.used_units, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.cool_time);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.used_cards, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.used_artifacts, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.used_runes, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.maxActionNum);
      return offset - num;
    }

    public ReqGvGBattle.Response Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (ReqGvGBattle.Response) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      JSON_GvGPartyUnit[] jsonGvGpartyUnitArray = (JSON_GvGPartyUnit[]) null;
      int num3 = 0;
      int num4 = 0;
      int num5 = 0;
      JSON_GvGUsedUnitData[] jsonGvGusedUnitDataArray = (JSON_GvGUsedUnitData[]) null;
      int num6 = 0;
      int[] numArray1 = (int[]) null;
      int[] numArray2 = (int[]) null;
      int[] numArray3 = (int[]) null;
      int num7 = 0;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num8;
        if (!this.____keyMapping.TryGetValueSafe(key, out num8))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num8)
          {
            case 0:
              jsonGvGpartyUnitArray = formatterResolver.GetFormatterWithVerify<JSON_GvGPartyUnit[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
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
              jsonGvGusedUnitDataArray = formatterResolver.GetFormatterWithVerify<JSON_GvGUsedUnitData[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 5:
              num6 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 6:
              numArray1 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 7:
              numArray2 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 8:
              numArray3 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 9:
              num7 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new ReqGvGBattle.Response()
      {
        offense = jsonGvGpartyUnitArray,
        win_num = num3,
        beat_num = num4,
        seed = num5,
        used_units = jsonGvGusedUnitDataArray,
        cool_time = num6,
        used_cards = numArray1,
        used_artifacts = numArray2,
        used_runes = numArray3,
        maxActionNum = num7
      };
    }
  }
}
