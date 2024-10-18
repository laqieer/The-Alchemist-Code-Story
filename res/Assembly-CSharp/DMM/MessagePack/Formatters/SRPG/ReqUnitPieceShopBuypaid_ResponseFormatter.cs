// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.ReqUnitPieceShopBuypaid_ResponseFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class ReqUnitPieceShopBuypaid_ResponseFormatter : 
    IMessagePackFormatter<ReqUnitPieceShopBuypaid.Response>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public ReqUnitPieceShopBuypaid_ResponseFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "items",
          0
        },
        {
          "shopitems",
          1
        },
        {
          "trophyprogs",
          2
        },
        {
          "bingoprogs",
          3
        }
      };
      this.____stringByteKeys = new byte[4][]
      {
        MessagePackBinary.GetEncodedStringBytes("items"),
        MessagePackBinary.GetEncodedStringBytes("shopitems"),
        MessagePackBinary.GetEncodedStringBytes("trophyprogs"),
        MessagePackBinary.GetEncodedStringBytes("bingoprogs")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      ReqUnitPieceShopBuypaid.Response value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 4);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<Json_Item[]>().Serialize(ref bytes, offset, value.items, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<Json_UnitPieceShopItem[]>().Serialize(ref bytes, offset, value.shopitems, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_TrophyProgress[]>().Serialize(ref bytes, offset, value.trophyprogs, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_TrophyProgress[]>().Serialize(ref bytes, offset, value.bingoprogs, formatterResolver);
      return offset - num;
    }

    public ReqUnitPieceShopBuypaid.Response Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (ReqUnitPieceShopBuypaid.Response) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      Json_Item[] jsonItemArray = (Json_Item[]) null;
      Json_UnitPieceShopItem[] unitPieceShopItemArray = (Json_UnitPieceShopItem[]) null;
      JSON_TrophyProgress[] jsonTrophyProgressArray1 = (JSON_TrophyProgress[]) null;
      JSON_TrophyProgress[] jsonTrophyProgressArray2 = (JSON_TrophyProgress[]) null;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num3;
        if (!this.____keyMapping.TryGetValueSafe(key, out num3))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num3)
          {
            case 0:
              jsonItemArray = formatterResolver.GetFormatterWithVerify<Json_Item[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              unitPieceShopItemArray = formatterResolver.GetFormatterWithVerify<Json_UnitPieceShopItem[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              jsonTrophyProgressArray1 = formatterResolver.GetFormatterWithVerify<JSON_TrophyProgress[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              jsonTrophyProgressArray2 = formatterResolver.GetFormatterWithVerify<JSON_TrophyProgress[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new ReqUnitPieceShopBuypaid.Response()
      {
        items = jsonItemArray,
        shopitems = unitPieceShopItemArray,
        trophyprogs = jsonTrophyProgressArray1,
        bingoprogs = jsonTrophyProgressArray2
      };
    }
  }
}
