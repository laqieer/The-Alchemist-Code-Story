// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.ReqGvGBattleEnemy_ResponseFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class ReqGvGBattleEnemy_ResponseFormatter : 
    IMessagePackFormatter<ReqGvGBattleEnemy.Response>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public ReqGvGBattleEnemy_ResponseFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "defenses",
          0
        },
        {
          "refresh_wait_sec",
          1
        },
        {
          "totalCount",
          2
        },
        {
          "total_beat_defense_num",
          3
        },
        {
          "total_beat_offense_num",
          4
        },
        {
          "totalPage",
          5
        },
        {
          "auto_refresh_wait_sec",
          6
        }
      };
      this.____stringByteKeys = new byte[7][]
      {
        MessagePackBinary.GetEncodedStringBytes("defenses"),
        MessagePackBinary.GetEncodedStringBytes("refresh_wait_sec"),
        MessagePackBinary.GetEncodedStringBytes("totalCount"),
        MessagePackBinary.GetEncodedStringBytes("total_beat_defense_num"),
        MessagePackBinary.GetEncodedStringBytes("total_beat_offense_num"),
        MessagePackBinary.GetEncodedStringBytes("totalPage"),
        MessagePackBinary.GetEncodedStringBytes("auto_refresh_wait_sec")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      ReqGvGBattleEnemy.Response value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 7);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GvGParty[]>().Serialize(ref bytes, offset, value.defenses, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.refresh_wait_sec);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.totalCount);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.total_beat_defense_num);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.total_beat_offense_num);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.totalPage);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.auto_refresh_wait_sec);
      return offset - num;
    }

    public ReqGvGBattleEnemy.Response Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (ReqGvGBattleEnemy.Response) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      JSON_GvGParty[] jsonGvGpartyArray = (JSON_GvGParty[]) null;
      int num3 = 0;
      int num4 = 0;
      int num5 = 0;
      int num6 = 0;
      int num7 = 0;
      int num8 = 0;
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
              jsonGvGpartyArray = formatterResolver.GetFormatterWithVerify<JSON_GvGParty[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
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
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new ReqGvGBattleEnemy.Response()
      {
        defenses = jsonGvGpartyArray,
        refresh_wait_sec = num3,
        totalCount = num4,
        total_beat_defense_num = num5,
        total_beat_offense_num = num6,
        totalPage = num7,
        auto_refresh_wait_sec = num8
      };
    }
  }
}
