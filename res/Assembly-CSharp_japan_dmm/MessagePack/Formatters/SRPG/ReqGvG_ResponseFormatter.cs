// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.ReqGvG_ResponseFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class ReqGvG_ResponseFormatter : 
    IMessagePackFormatter<ReqGvG.Response>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public ReqGvG_ResponseFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "nodes",
          0
        },
        {
          "matching_order",
          1
        },
        {
          "guilds",
          2
        },
        {
          "my_guild",
          3
        },
        {
          "used_units",
          4
        },
        {
          "declare_num",
          5
        },
        {
          "refresh_wait_sec",
          6
        },
        {
          "auto_refresh_wait_sec",
          7
        },
        {
          "declare_cool_time",
          8
        },
        {
          "result_daily",
          9
        },
        {
          "result",
          10
        },
        {
          "my_league",
          11
        },
        {
          "used_cards",
          12
        },
        {
          "used_artifacts",
          13
        },
        {
          "used_runes",
          14
        }
      };
      this.____stringByteKeys = new byte[15][]
      {
        MessagePackBinary.GetEncodedStringBytes("nodes"),
        MessagePackBinary.GetEncodedStringBytes("matching_order"),
        MessagePackBinary.GetEncodedStringBytes("guilds"),
        MessagePackBinary.GetEncodedStringBytes("my_guild"),
        MessagePackBinary.GetEncodedStringBytes("used_units"),
        MessagePackBinary.GetEncodedStringBytes("declare_num"),
        MessagePackBinary.GetEncodedStringBytes("refresh_wait_sec"),
        MessagePackBinary.GetEncodedStringBytes("auto_refresh_wait_sec"),
        MessagePackBinary.GetEncodedStringBytes("declare_cool_time"),
        MessagePackBinary.GetEncodedStringBytes("result_daily"),
        MessagePackBinary.GetEncodedStringBytes("result"),
        MessagePackBinary.GetEncodedStringBytes("my_league"),
        MessagePackBinary.GetEncodedStringBytes("used_cards"),
        MessagePackBinary.GetEncodedStringBytes("used_artifacts"),
        MessagePackBinary.GetEncodedStringBytes("used_runes")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      ReqGvG.Response value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 15);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GvGNodeData[]>().Serialize(ref bytes, offset, value.nodes, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.matching_order, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GvGLeagueViewGuild[]>().Serialize(ref bytes, offset, value.guilds, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GvGLeagueViewGuild>().Serialize(ref bytes, offset, value.my_guild, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GvGUsedUnitData[]>().Serialize(ref bytes, offset, value.used_units, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.declare_num);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.refresh_wait_sec);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.auto_refresh_wait_sec);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.declare_cool_time);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GvGResult>().Serialize(ref bytes, offset, value.result_daily, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GvGResult>().Serialize(ref bytes, offset, value.result, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GvGLeagueResult>().Serialize(ref bytes, offset, value.my_league, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.used_cards, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.used_artifacts, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.used_runes, formatterResolver);
      return offset - num;
    }

    public ReqGvG.Response Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (ReqGvG.Response) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      JSON_GvGNodeData[] jsonGvGnodeDataArray = (JSON_GvGNodeData[]) null;
      int[] numArray1 = (int[]) null;
      JSON_GvGLeagueViewGuild[] gleagueViewGuildArray = (JSON_GvGLeagueViewGuild[]) null;
      JSON_GvGLeagueViewGuild gleagueViewGuild = (JSON_GvGLeagueViewGuild) null;
      JSON_GvGUsedUnitData[] jsonGvGusedUnitDataArray = (JSON_GvGUsedUnitData[]) null;
      int num3 = 0;
      int num4 = 0;
      int num5 = 0;
      int num6 = 0;
      JSON_GvGResult jsonGvGresult1 = (JSON_GvGResult) null;
      JSON_GvGResult jsonGvGresult2 = (JSON_GvGResult) null;
      JSON_GvGLeagueResult jsonGvGleagueResult = (JSON_GvGLeagueResult) null;
      int[] numArray2 = (int[]) null;
      int[] numArray3 = (int[]) null;
      int[] numArray4 = (int[]) null;
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
              jsonGvGnodeDataArray = formatterResolver.GetFormatterWithVerify<JSON_GvGNodeData[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              numArray1 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              gleagueViewGuildArray = formatterResolver.GetFormatterWithVerify<JSON_GvGLeagueViewGuild[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              gleagueViewGuild = formatterResolver.GetFormatterWithVerify<JSON_GvGLeagueViewGuild>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              jsonGvGusedUnitDataArray = formatterResolver.GetFormatterWithVerify<JSON_GvGUsedUnitData[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 5:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 6:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 7:
              num5 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 8:
              num6 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 9:
              jsonGvGresult1 = formatterResolver.GetFormatterWithVerify<JSON_GvGResult>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 10:
              jsonGvGresult2 = formatterResolver.GetFormatterWithVerify<JSON_GvGResult>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 11:
              jsonGvGleagueResult = formatterResolver.GetFormatterWithVerify<JSON_GvGLeagueResult>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 12:
              numArray2 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 13:
              numArray3 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 14:
              numArray4 = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new ReqGvG.Response()
      {
        nodes = jsonGvGnodeDataArray,
        matching_order = numArray1,
        guilds = gleagueViewGuildArray,
        my_guild = gleagueViewGuild,
        used_units = jsonGvGusedUnitDataArray,
        declare_num = num3,
        refresh_wait_sec = num4,
        auto_refresh_wait_sec = num5,
        declare_cool_time = num6,
        result_daily = jsonGvGresult1,
        result = jsonGvGresult2,
        my_league = jsonGvGleagueResult,
        used_cards = numArray2,
        used_artifacts = numArray3,
        used_runes = numArray4
      };
    }
  }
}
