// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.Json_MyPhotonPlayerBinaryParam_UnitDataElemFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class Json_MyPhotonPlayerBinaryParam_UnitDataElemFormatter : 
    IMessagePackFormatter<Json_MyPhotonPlayerBinaryParam.UnitDataElem>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public Json_MyPhotonPlayerBinaryParam_UnitDataElemFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "slotID",
          0
        },
        {
          "place",
          1
        },
        {
          "unitJson",
          2
        }
      };
      this.____stringByteKeys = new byte[3][]
      {
        MessagePackBinary.GetEncodedStringBytes("slotID"),
        MessagePackBinary.GetEncodedStringBytes("place"),
        MessagePackBinary.GetEncodedStringBytes("unitJson")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      Json_MyPhotonPlayerBinaryParam.UnitDataElem value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 3);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.slotID);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.place);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<Json_Unit>().Serialize(ref bytes, offset, value.unitJson, formatterResolver);
      return offset - num;
    }

    public Json_MyPhotonPlayerBinaryParam.UnitDataElem Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (Json_MyPhotonPlayerBinaryParam.UnitDataElem) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      int num3 = 0;
      int num4 = 0;
      Json_Unit jsonUnit = (Json_Unit) null;
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
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 1:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 2:
              jsonUnit = formatterResolver.GetFormatterWithVerify<Json_Unit>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new Json_MyPhotonPlayerBinaryParam.UnitDataElem()
      {
        slotID = num3,
        place = num4,
        unitJson = jsonUnit
      };
    }
  }
}
