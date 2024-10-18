// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.JSON_PlayerGuildFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class JSON_PlayerGuildFormatter : 
    IMessagePackFormatter<JSON_PlayerGuild>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public JSON_PlayerGuildFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "gid",
          0
        },
        {
          "guild_name",
          1
        },
        {
          "role_id",
          2
        },
        {
          "applied_at",
          3
        },
        {
          "joined_at",
          4
        },
        {
          "leaved_at",
          5
        },
        {
          "invest_at",
          6
        }
      };
      this.____stringByteKeys = new byte[7][]
      {
        MessagePackBinary.GetEncodedStringBytes("gid"),
        MessagePackBinary.GetEncodedStringBytes("guild_name"),
        MessagePackBinary.GetEncodedStringBytes("role_id"),
        MessagePackBinary.GetEncodedStringBytes("applied_at"),
        MessagePackBinary.GetEncodedStringBytes("joined_at"),
        MessagePackBinary.GetEncodedStringBytes("leaved_at"),
        MessagePackBinary.GetEncodedStringBytes("invest_at")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      JSON_PlayerGuild value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 7);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.gid);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.guild_name, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.role_id);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += MessagePackBinary.WriteInt64(ref bytes, offset, value.applied_at);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += MessagePackBinary.WriteInt64(ref bytes, offset, value.joined_at);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += MessagePackBinary.WriteInt64(ref bytes, offset, value.leaved_at);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += MessagePackBinary.WriteInt64(ref bytes, offset, value.invest_at);
      return offset - num;
    }

    public JSON_PlayerGuild Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (JSON_PlayerGuild) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      int num3 = 0;
      string str = (string) null;
      int num4 = 0;
      long num5 = 0;
      long num6 = 0;
      long num7 = 0;
      long num8 = 0;
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
              str = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 3:
              num5 = MessagePackBinary.ReadInt64(bytes, offset, out readSize);
              break;
            case 4:
              num6 = MessagePackBinary.ReadInt64(bytes, offset, out readSize);
              break;
            case 5:
              num7 = MessagePackBinary.ReadInt64(bytes, offset, out readSize);
              break;
            case 6:
              num8 = MessagePackBinary.ReadInt64(bytes, offset, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new JSON_PlayerGuild()
      {
        gid = num3,
        guild_name = str,
        role_id = num4,
        applied_at = num5,
        joined_at = num6,
        leaved_at = num7,
        invest_at = num8
      };
    }
  }
}
