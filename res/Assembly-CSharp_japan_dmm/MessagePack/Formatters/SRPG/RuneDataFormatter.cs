// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.RuneDataFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class RuneDataFormatter : IMessagePackFormatter<RuneData>, IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public RuneDataFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "mUniqueID",
          0
        },
        {
          "iname",
          1
        },
        {
          "unit_id",
          2
        },
        {
          "enforce",
          3
        },
        {
          "evo",
          4
        },
        {
          "state",
          5
        }
      };
      this.____stringByteKeys = new byte[6][]
      {
        MessagePackBinary.GetEncodedStringBytes("mUniqueID"),
        MessagePackBinary.GetEncodedStringBytes("iname"),
        MessagePackBinary.GetEncodedStringBytes("unit_id"),
        MessagePackBinary.GetEncodedStringBytes("enforce"),
        MessagePackBinary.GetEncodedStringBytes("evo"),
        MessagePackBinary.GetEncodedStringBytes("state")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      RuneData value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 6);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<OLong>().Serialize(ref bytes, offset, value.mUniqueID, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.unit_id);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += MessagePackBinary.WriteByte(ref bytes, offset, value.enforce);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += MessagePackBinary.WriteByte(ref bytes, offset, value.evo);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<RuneStateData>().Serialize(ref bytes, offset, value.state, formatterResolver);
      return offset - num;
    }

    public RuneData Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (RuneData) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      OLong olong = new OLong();
      string str = (string) null;
      int num3 = 0;
      byte num4 = 0;
      byte num5 = 0;
      RuneStateData runeStateData = (RuneStateData) null;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num6;
        if (!this.____keyMapping.TryGetValueSafe(key, out num6))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num6)
          {
            case 0:
              olong = formatterResolver.GetFormatterWithVerify<OLong>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              str = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 3:
              num4 = MessagePackBinary.ReadByte(bytes, offset, out readSize);
              break;
            case 4:
              num5 = MessagePackBinary.ReadByte(bytes, offset, out readSize);
              break;
            case 5:
              runeStateData = formatterResolver.GetFormatterWithVerify<RuneStateData>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new RuneData()
      {
        mUniqueID = olong,
        iname = str,
        unit_id = num3,
        enforce = num4,
        evo = num5,
        state = runeStateData
      };
    }
  }
}
