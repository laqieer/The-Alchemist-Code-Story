// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.MapBreakObjFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class MapBreakObjFormatter : 
    IMessagePackFormatter<MapBreakObj>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public MapBreakObjFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "clash_type",
          0
        },
        {
          "ai_type",
          1
        },
        {
          "side_type",
          2
        },
        {
          "ray_type",
          3
        },
        {
          "is_ui",
          4
        },
        {
          "max_hp",
          5
        },
        {
          "rest_hps",
          6
        }
      };
      this.____stringByteKeys = new byte[7][]
      {
        MessagePackBinary.GetEncodedStringBytes("clash_type"),
        MessagePackBinary.GetEncodedStringBytes("ai_type"),
        MessagePackBinary.GetEncodedStringBytes("side_type"),
        MessagePackBinary.GetEncodedStringBytes("ray_type"),
        MessagePackBinary.GetEncodedStringBytes("is_ui"),
        MessagePackBinary.GetEncodedStringBytes("max_hp"),
        MessagePackBinary.GetEncodedStringBytes("rest_hps")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      MapBreakObj value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 7);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.clash_type);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ai_type);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.side_type);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ray_type);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.is_ui);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.max_hp);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.rest_hps, formatterResolver);
      return offset - num;
    }

    public MapBreakObj Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (MapBreakObj) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      int num3 = 0;
      int num4 = 0;
      int num5 = 0;
      int num6 = 0;
      int num7 = 0;
      int num8 = 0;
      int[] numArray = (int[]) null;
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
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 1:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 2:
              num5 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 3:
              num6 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 4:
              num7 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 5:
              num8 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 6:
              numArray = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new MapBreakObj()
      {
        clash_type = num3,
        ai_type = num4,
        side_type = num5,
        ray_type = num6,
        is_ui = num7,
        max_hp = num8,
        rest_hps = numArray
      };
    }
  }
}
