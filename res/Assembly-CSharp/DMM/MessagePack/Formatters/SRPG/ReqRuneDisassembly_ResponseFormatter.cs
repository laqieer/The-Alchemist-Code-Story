// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.ReqRuneDisassembly_ResponseFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class ReqRuneDisassembly_ResponseFormatter : 
    IMessagePackFormatter<ReqRuneDisassembly.Response>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public ReqRuneDisassembly_ResponseFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "items",
          0
        },
        {
          "player",
          1
        },
        {
          "rewards",
          2
        },
        {
          "rune_ids",
          3
        },
        {
          "result",
          4
        },
        {
          "rune_storage_used",
          5
        }
      };
      this.____stringByteKeys = new byte[6][]
      {
        MessagePackBinary.GetEncodedStringBytes("items"),
        MessagePackBinary.GetEncodedStringBytes("player"),
        MessagePackBinary.GetEncodedStringBytes("rewards"),
        MessagePackBinary.GetEncodedStringBytes("rune_ids"),
        MessagePackBinary.GetEncodedStringBytes("result"),
        MessagePackBinary.GetEncodedStringBytes("rune_storage_used")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      ReqRuneDisassembly.Response value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 6);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<Json_Item[]>().Serialize(ref bytes, offset, value.items, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<Json_PlayerData>().Serialize(ref bytes, offset, value.player, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<ReqRuneDisassembly.Response.Rewards[]>().Serialize(ref bytes, offset, value.rewards, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<long[]>().Serialize(ref bytes, offset, value.rune_ids, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.result, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rune_storage_used);
      return offset - num;
    }

    public ReqRuneDisassembly.Response Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (ReqRuneDisassembly.Response) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      Json_Item[] jsonItemArray = (Json_Item[]) null;
      Json_PlayerData jsonPlayerData = (Json_PlayerData) null;
      ReqRuneDisassembly.Response.Rewards[] rewardsArray = (ReqRuneDisassembly.Response.Rewards[]) null;
      long[] numArray = (long[]) null;
      string str = (string) null;
      int num3 = 0;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num4;
        if (!this.____keyMapping.TryGetValueSafe(key, out num4))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num4)
          {
            case 0:
              jsonItemArray = formatterResolver.GetFormatterWithVerify<Json_Item[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              jsonPlayerData = formatterResolver.GetFormatterWithVerify<Json_PlayerData>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              rewardsArray = formatterResolver.GetFormatterWithVerify<ReqRuneDisassembly.Response.Rewards[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              numArray = formatterResolver.GetFormatterWithVerify<long[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              str = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 5:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new ReqRuneDisassembly.Response()
      {
        items = jsonItemArray,
        player = jsonPlayerData,
        rewards = rewardsArray,
        rune_ids = numArray,
        result = str,
        rune_storage_used = num3
      };
    }
  }
}
