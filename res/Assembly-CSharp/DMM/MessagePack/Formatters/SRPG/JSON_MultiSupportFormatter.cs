// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.JSON_MultiSupportFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class JSON_MultiSupportFormatter : 
    IMessagePackFormatter<JSON_MultiSupport>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public JSON_MultiSupportFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "uid",
          0
        },
        {
          "fuid",
          1
        },
        {
          "name",
          2
        },
        {
          "lv",
          3
        },
        {
          "units",
          4
        },
        {
          "guild",
          5
        },
        {
          "isFriend",
          6
        },
        {
          "cost",
          7
        }
      };
      this.____stringByteKeys = new byte[8][]
      {
        MessagePackBinary.GetEncodedStringBytes("uid"),
        MessagePackBinary.GetEncodedStringBytes("fuid"),
        MessagePackBinary.GetEncodedStringBytes("name"),
        MessagePackBinary.GetEncodedStringBytes("lv"),
        MessagePackBinary.GetEncodedStringBytes("units"),
        MessagePackBinary.GetEncodedStringBytes("guild"),
        MessagePackBinary.GetEncodedStringBytes("isFriend"),
        MessagePackBinary.GetEncodedStringBytes("cost")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      JSON_MultiSupport value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 8);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.uid, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.fuid, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.name, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.lv);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<Json_Unit[]>().Serialize(ref bytes, offset, value.units, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_ViewGuild>().Serialize(ref bytes, offset, value.guild, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.isFriend);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.cost);
      return offset - num;
    }

    public JSON_MultiSupport Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (JSON_MultiSupport) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      string str1 = (string) null;
      string str2 = (string) null;
      string str3 = (string) null;
      int num3 = 0;
      Json_Unit[] jsonUnitArray = (Json_Unit[]) null;
      JSON_ViewGuild jsonViewGuild = (JSON_ViewGuild) null;
      int num4 = 0;
      int num5 = 0;
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
              str1 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              str2 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              str3 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 4:
              jsonUnitArray = formatterResolver.GetFormatterWithVerify<Json_Unit[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 5:
              jsonViewGuild = formatterResolver.GetFormatterWithVerify<JSON_ViewGuild>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 6:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 7:
              num5 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new JSON_MultiSupport()
      {
        uid = str1,
        fuid = str2,
        name = str3,
        lv = num3,
        units = jsonUnitArray,
        guild = jsonViewGuild,
        isFriend = num4,
        cost = num5
      };
    }
  }
}
