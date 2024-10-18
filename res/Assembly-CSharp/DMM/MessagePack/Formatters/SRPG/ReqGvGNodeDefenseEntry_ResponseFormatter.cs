// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.ReqGvGNodeDefenseEntry_ResponseFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class ReqGvGNodeDefenseEntry_ResponseFormatter : 
    IMessagePackFormatter<ReqGvGNodeDefenseEntry.Response>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public ReqGvGNodeDefenseEntry_ResponseFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "defenses",
          0
        },
        {
          "used_units",
          1
        },
        {
          "totalPage",
          2
        },
        {
          "total_beat_num",
          3
        },
        {
          "unavailable_items",
          4
        }
      };
      this.____stringByteKeys = new byte[5][]
      {
        MessagePackBinary.GetEncodedStringBytes("defenses"),
        MessagePackBinary.GetEncodedStringBytes("used_units"),
        MessagePackBinary.GetEncodedStringBytes("totalPage"),
        MessagePackBinary.GetEncodedStringBytes("total_beat_num"),
        MessagePackBinary.GetEncodedStringBytes("unavailable_items")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      ReqGvGNodeDefenseEntry.Response value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 5);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GvGParty[]>().Serialize(ref bytes, offset, value.defenses, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GvGUsedUnitData[]>().Serialize(ref bytes, offset, value.used_units, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.totalPage);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.total_beat_num);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GvGUsedItems[]>().Serialize(ref bytes, offset, value.unavailable_items, formatterResolver);
      return offset - num;
    }

    public ReqGvGNodeDefenseEntry.Response Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (ReqGvGNodeDefenseEntry.Response) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      JSON_GvGParty[] jsonGvGpartyArray = (JSON_GvGParty[]) null;
      JSON_GvGUsedUnitData[] jsonGvGusedUnitDataArray = (JSON_GvGUsedUnitData[]) null;
      int num3 = 0;
      int num4 = 0;
      JSON_GvGUsedItems[] jsonGvGusedItemsArray = (JSON_GvGUsedItems[]) null;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num5;
        if (!this.____keyMapping.TryGetValueSafe(key, out num5))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num5)
          {
            case 0:
              jsonGvGpartyArray = formatterResolver.GetFormatterWithVerify<JSON_GvGParty[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              jsonGvGusedUnitDataArray = formatterResolver.GetFormatterWithVerify<JSON_GvGUsedUnitData[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 3:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 4:
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
      return new ReqGvGNodeDefenseEntry.Response()
      {
        defenses = jsonGvGpartyArray,
        used_units = jsonGvGusedUnitDataArray,
        totalPage = num3,
        total_beat_num = num4,
        unavailable_items = jsonGvGusedItemsArray
      };
    }
  }
}
