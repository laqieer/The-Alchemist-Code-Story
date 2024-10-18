// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.JSON_ProductParamResponseFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class JSON_ProductParamResponseFormatter : 
    IMessagePackFormatter<JSON_ProductParamResponse>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public JSON_ProductParamResponseFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "products",
          0
        },
        {
          "buycoins",
          1
        }
      };
      this.____stringByteKeys = new byte[2][]
      {
        MessagePackBinary.GetEncodedStringBytes("products"),
        MessagePackBinary.GetEncodedStringBytes("buycoins")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      JSON_ProductParamResponse value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 2);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_ProductParam[]>().Serialize(ref bytes, offset, value.products, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_ProductBuyCoinParam[]>().Serialize(ref bytes, offset, value.buycoins, formatterResolver);
      return offset - num;
    }

    public JSON_ProductParamResponse Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (JSON_ProductParamResponse) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      JSON_ProductParam[] jsonProductParamArray = (JSON_ProductParam[]) null;
      JSON_ProductBuyCoinParam[] productBuyCoinParamArray = (JSON_ProductBuyCoinParam[]) null;
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
              jsonProductParamArray = formatterResolver.GetFormatterWithVerify<JSON_ProductParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              productBuyCoinParamArray = formatterResolver.GetFormatterWithVerify<JSON_ProductBuyCoinParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new JSON_ProductParamResponse()
      {
        products = jsonProductParamArray,
        buycoins = productBuyCoinParamArray
      };
    }
  }
}
