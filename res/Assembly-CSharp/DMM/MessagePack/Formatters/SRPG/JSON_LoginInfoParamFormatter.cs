// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.JSON_LoginInfoParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class JSON_LoginInfoParamFormatter : 
    IMessagePackFormatter<JSON_LoginInfoParam>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public JSON_LoginInfoParamFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "iname",
          0
        },
        {
          "path",
          1
        },
        {
          "scene",
          2
        },
        {
          "begin_at",
          3
        },
        {
          "end_at",
          4
        },
        {
          "conditions",
          5
        },
        {
          "conditions_value",
          6
        },
        {
          "priority",
          7
        },
        {
          "draw_count",
          8
        },
        {
          "movie_compel",
          9
        },
        {
          "movie",
          10
        },
        {
          "url",
          11
        }
      };
      this.____stringByteKeys = new byte[12][]
      {
        MessagePackBinary.GetEncodedStringBytes("iname"),
        MessagePackBinary.GetEncodedStringBytes("path"),
        MessagePackBinary.GetEncodedStringBytes("scene"),
        MessagePackBinary.GetEncodedStringBytes("begin_at"),
        MessagePackBinary.GetEncodedStringBytes("end_at"),
        MessagePackBinary.GetEncodedStringBytes("conditions"),
        MessagePackBinary.GetEncodedStringBytes("conditions_value"),
        MessagePackBinary.GetEncodedStringBytes("priority"),
        MessagePackBinary.GetEncodedStringBytes("draw_count"),
        MessagePackBinary.GetEncodedStringBytes("movie_compel"),
        MessagePackBinary.GetEncodedStringBytes("movie"),
        MessagePackBinary.GetEncodedStringBytes("url")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      JSON_LoginInfoParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 12);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.path, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.scene);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.begin_at, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.end_at, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.conditions);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.conditions_value);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.priority);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.draw_count);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.movie_compel);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.movie, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.url, formatterResolver);
      return offset - num;
    }

    public JSON_LoginInfoParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (JSON_LoginInfoParam) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      string str1 = (string) null;
      string str2 = (string) null;
      int num3 = 0;
      string str3 = (string) null;
      string str4 = (string) null;
      int num4 = 0;
      int num5 = 0;
      int num6 = 0;
      int num7 = 0;
      int num8 = 0;
      string str5 = (string) null;
      string str6 = (string) null;
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
              str1 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              str2 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 3:
              str3 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              str4 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 5:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 6:
              num5 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 7:
              num6 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 8:
              num7 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 9:
              num8 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 10:
              str5 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 11:
              str6 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new JSON_LoginInfoParam()
      {
        iname = str1,
        path = str2,
        scene = num3,
        begin_at = str3,
        end_at = str4,
        conditions = num4,
        conditions_value = num5,
        priority = num6,
        draw_count = num7,
        movie_compel = num8,
        movie = str5,
        url = str6
      };
    }
  }
}
