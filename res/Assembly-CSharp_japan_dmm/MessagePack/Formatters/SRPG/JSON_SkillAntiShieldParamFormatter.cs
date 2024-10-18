// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.JSON_SkillAntiShieldParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class JSON_SkillAntiShieldParamFormatter : 
    IMessagePackFormatter<JSON_SkillAntiShieldParam>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public JSON_SkillAntiShieldParamFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "iname",
          0
        },
        {
          "is_ignore",
          1
        },
        {
          "ignore_rate_ini",
          2
        },
        {
          "ignore_rate_max",
          3
        },
        {
          "is_destroy",
          4
        },
        {
          "destroy_rate_ini",
          5
        },
        {
          "destroy_rate_max",
          6
        }
      };
      this.____stringByteKeys = new byte[7][]
      {
        MessagePackBinary.GetEncodedStringBytes("iname"),
        MessagePackBinary.GetEncodedStringBytes("is_ignore"),
        MessagePackBinary.GetEncodedStringBytes("ignore_rate_ini"),
        MessagePackBinary.GetEncodedStringBytes("ignore_rate_max"),
        MessagePackBinary.GetEncodedStringBytes("is_destroy"),
        MessagePackBinary.GetEncodedStringBytes("destroy_rate_ini"),
        MessagePackBinary.GetEncodedStringBytes("destroy_rate_max")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      JSON_SkillAntiShieldParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 7);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.is_ignore);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.ignore_rate_ini);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.ignore_rate_max);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.is_destroy);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.destroy_rate_ini);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.destroy_rate_max);
      return offset - num;
    }

    public JSON_SkillAntiShieldParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (JSON_SkillAntiShieldParam) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      string str = (string) null;
      int num3 = 0;
      short num4 = 0;
      short num5 = 0;
      int num6 = 0;
      short num7 = 0;
      short num8 = 0;
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
              str = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 2:
              num4 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 3:
              num5 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 4:
              num6 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 5:
              num7 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 6:
              num8 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new JSON_SkillAntiShieldParam()
      {
        iname = str,
        is_ignore = num3,
        ignore_rate_ini = num4,
        ignore_rate_max = num5,
        is_destroy = num6,
        destroy_rate_ini = num7,
        destroy_rate_max = num8
      };
    }
  }
}
