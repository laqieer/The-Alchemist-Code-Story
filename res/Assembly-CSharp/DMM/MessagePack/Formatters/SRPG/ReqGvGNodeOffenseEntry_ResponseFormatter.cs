// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.ReqGvGNodeOffenseEntry_ResponseFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class ReqGvGNodeOffenseEntry_ResponseFormatter : 
    IMessagePackFormatter<ReqGvGNodeOffenseEntry.Response>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public ReqGvGNodeOffenseEntry_ResponseFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "offense",
          0
        },
        {
          "unavailable_items",
          1
        }
      };
      this.____stringByteKeys = new byte[2][]
      {
        MessagePackBinary.GetEncodedStringBytes("offense"),
        MessagePackBinary.GetEncodedStringBytes("unavailable_items")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      ReqGvGNodeOffenseEntry.Response value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 2);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GvGPartyUnit[]>().Serialize(ref bytes, offset, value.offense, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GvGUsedItems[]>().Serialize(ref bytes, offset, value.unavailable_items, formatterResolver);
      return offset - num;
    }

    public ReqGvGNodeOffenseEntry.Response Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (ReqGvGNodeOffenseEntry.Response) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      JSON_GvGPartyUnit[] jsonGvGpartyUnitArray = (JSON_GvGPartyUnit[]) null;
      JSON_GvGUsedItems[] jsonGvGusedItemsArray = (JSON_GvGUsedItems[]) null;
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
              jsonGvGpartyUnitArray = formatterResolver.GetFormatterWithVerify<JSON_GvGPartyUnit[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              jsonGvGusedItemsArray = formatterResolver.GetFormatterWithVerify<JSON_GvGUsedItems[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new ReqGvGNodeOffenseEntry.Response()
      {
        offense = jsonGvGpartyUnitArray,
        unavailable_items = jsonGvGusedItemsArray
      };
    }
  }
}
