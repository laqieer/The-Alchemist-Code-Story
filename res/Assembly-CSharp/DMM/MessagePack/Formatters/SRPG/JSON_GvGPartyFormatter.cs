// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.JSON_GvGPartyFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class JSON_GvGPartyFormatter : 
    IMessagePackFormatter<JSON_GvGParty>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public JSON_GvGPartyFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "id",
          0
        },
        {
          "win_num",
          1
        },
        {
          "beat_num",
          2
        },
        {
          "is_npc",
          3
        },
        {
          "role",
          4
        },
        {
          "name",
          5
        },
        {
          "units",
          6
        },
        {
          "npc_units",
          7
        }
      };
      this.____stringByteKeys = new byte[8][]
      {
        MessagePackBinary.GetEncodedStringBytes("id"),
        MessagePackBinary.GetEncodedStringBytes("win_num"),
        MessagePackBinary.GetEncodedStringBytes("beat_num"),
        MessagePackBinary.GetEncodedStringBytes("is_npc"),
        MessagePackBinary.GetEncodedStringBytes("role"),
        MessagePackBinary.GetEncodedStringBytes("name"),
        MessagePackBinary.GetEncodedStringBytes("units"),
        MessagePackBinary.GetEncodedStringBytes("npc_units")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      JSON_GvGParty value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 8);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.id);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.win_num);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.beat_num);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.is_npc);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.role);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.name, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GvGPartyUnit[]>().Serialize(ref bytes, offset, value.units, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GvGPartyNPC[]>().Serialize(ref bytes, offset, value.npc_units, formatterResolver);
      return offset - num;
    }

    public JSON_GvGParty Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (JSON_GvGParty) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      int num3 = 0;
      int num4 = 0;
      int num5 = 0;
      int num6 = 0;
      int num7 = 0;
      string str = (string) null;
      JSON_GvGPartyUnit[] jsonGvGpartyUnitArray = (JSON_GvGPartyUnit[]) null;
      JSON_GvGPartyNPC[] jsonGvGpartyNpcArray = (JSON_GvGPartyNPC[]) null;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num8;
        if (!this.____keyMapping.TryGetValueSafe(key, out num8))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num8)
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
              str = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 6:
              jsonGvGpartyUnitArray = formatterResolver.GetFormatterWithVerify<JSON_GvGPartyUnit[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 7:
              jsonGvGpartyNpcArray = formatterResolver.GetFormatterWithVerify<JSON_GvGPartyNPC[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new JSON_GvGParty()
      {
        id = num3,
        win_num = num4,
        beat_num = num5,
        is_npc = num6,
        role = num7,
        name = str,
        units = jsonGvGpartyUnitArray,
        npc_units = jsonGvGpartyNpcArray
      };
    }
  }
}
