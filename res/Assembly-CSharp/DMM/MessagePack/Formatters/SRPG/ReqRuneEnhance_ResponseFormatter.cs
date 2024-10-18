// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.ReqRuneEnhance_ResponseFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class ReqRuneEnhance_ResponseFormatter : 
    IMessagePackFormatter<ReqRuneEnhance.Response>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public ReqRuneEnhance_ResponseFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "runes",
          0
        },
        {
          "items",
          1
        },
        {
          "player",
          2
        },
        {
          "rune_enforce_gauge",
          3
        },
        {
          "result",
          4
        }
      };
      this.____stringByteKeys = new byte[5][]
      {
        MessagePackBinary.GetEncodedStringBytes("runes"),
        MessagePackBinary.GetEncodedStringBytes("items"),
        MessagePackBinary.GetEncodedStringBytes("player"),
        MessagePackBinary.GetEncodedStringBytes("rune_enforce_gauge"),
        MessagePackBinary.GetEncodedStringBytes("result")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      ReqRuneEnhance.Response value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 5);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<Json_RuneData[]>().Serialize(ref bytes, offset, value.runes, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<Json_Item[]>().Serialize(ref bytes, offset, value.items, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<Json_PlayerData>().Serialize(ref bytes, offset, value.player, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<Json_RuneEnforceGaugeData[]>().Serialize(ref bytes, offset, value.rune_enforce_gauge, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.result);
      return offset - num;
    }

    public ReqRuneEnhance.Response Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (ReqRuneEnhance.Response) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      Json_RuneData[] jsonRuneDataArray = (Json_RuneData[]) null;
      Json_Item[] jsonItemArray = (Json_Item[]) null;
      Json_PlayerData jsonPlayerData = (Json_PlayerData) null;
      Json_RuneEnforceGaugeData[] enforceGaugeDataArray = (Json_RuneEnforceGaugeData[]) null;
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
              jsonRuneDataArray = formatterResolver.GetFormatterWithVerify<Json_RuneData[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              jsonItemArray = formatterResolver.GetFormatterWithVerify<Json_Item[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              jsonPlayerData = formatterResolver.GetFormatterWithVerify<Json_PlayerData>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              enforceGaugeDataArray = formatterResolver.GetFormatterWithVerify<Json_RuneEnforceGaugeData[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
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
      return new ReqRuneEnhance.Response()
      {
        runes = jsonRuneDataArray,
        items = jsonItemArray,
        player = jsonPlayerData,
        rune_enforce_gauge = enforceGaugeDataArray,
        result = num3
      };
    }
  }
}
