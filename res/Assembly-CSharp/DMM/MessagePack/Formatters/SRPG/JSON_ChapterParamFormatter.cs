// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.JSON_ChapterParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class JSON_ChapterParamFormatter : 
    IMessagePackFormatter<JSON_ChapterParam>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public JSON_ChapterParamFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "iname",
          0
        },
        {
          "name",
          1
        },
        {
          "expr",
          2
        },
        {
          "world",
          3
        },
        {
          "start",
          4
        },
        {
          "end",
          5
        },
        {
          "parent",
          6
        },
        {
          "hide",
          7
        },
        {
          "chap",
          8
        },
        {
          "banr",
          9
        },
        {
          "item",
          10
        },
        {
          "keyitem1",
          11
        },
        {
          "keynum1",
          12
        },
        {
          "keyitem2",
          13
        },
        {
          "keynum2",
          14
        },
        {
          "keyitem3",
          15
        },
        {
          "keynum3",
          16
        },
        {
          "keytime",
          17
        },
        {
          "hurl",
          18
        },
        {
          "limit",
          19
        }
      };
      this.____stringByteKeys = new byte[20][]
      {
        MessagePackBinary.GetEncodedStringBytes("iname"),
        MessagePackBinary.GetEncodedStringBytes("name"),
        MessagePackBinary.GetEncodedStringBytes("expr"),
        MessagePackBinary.GetEncodedStringBytes("world"),
        MessagePackBinary.GetEncodedStringBytes("start"),
        MessagePackBinary.GetEncodedStringBytes("end"),
        MessagePackBinary.GetEncodedStringBytes("parent"),
        MessagePackBinary.GetEncodedStringBytes("hide"),
        MessagePackBinary.GetEncodedStringBytes("chap"),
        MessagePackBinary.GetEncodedStringBytes("banr"),
        MessagePackBinary.GetEncodedStringBytes("item"),
        MessagePackBinary.GetEncodedStringBytes("keyitem1"),
        MessagePackBinary.GetEncodedStringBytes("keynum1"),
        MessagePackBinary.GetEncodedStringBytes("keyitem2"),
        MessagePackBinary.GetEncodedStringBytes("keynum2"),
        MessagePackBinary.GetEncodedStringBytes("keyitem3"),
        MessagePackBinary.GetEncodedStringBytes("keynum3"),
        MessagePackBinary.GetEncodedStringBytes("keytime"),
        MessagePackBinary.GetEncodedStringBytes("hurl"),
        MessagePackBinary.GetEncodedStringBytes("limit")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      JSON_ChapterParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, 20);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.name, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.expr, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.world, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += MessagePackBinary.WriteInt64(ref bytes, offset, value.start);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += MessagePackBinary.WriteInt64(ref bytes, offset, value.end);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.parent, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.hide);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.chap, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.banr, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.item, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.keyitem1, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.keynum1);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.keyitem2, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.keynum2);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.keyitem3, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[16]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.keynum3);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[17]);
      offset += MessagePackBinary.WriteInt64(ref bytes, offset, value.keytime);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[18]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.hurl, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[19]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.limit);
      return offset - num;
    }

    public JSON_ChapterParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (JSON_ChapterParam) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      string str1 = (string) null;
      string str2 = (string) null;
      string str3 = (string) null;
      string str4 = (string) null;
      long num3 = 0;
      long num4 = 0;
      string str5 = (string) null;
      int num5 = 0;
      string str6 = (string) null;
      string str7 = (string) null;
      string str8 = (string) null;
      string str9 = (string) null;
      int num6 = 0;
      string str10 = (string) null;
      int num7 = 0;
      string str11 = (string) null;
      int num8 = 0;
      long num9 = 0;
      string str12 = (string) null;
      int num10 = 0;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num11;
        if (!this.____keyMapping.TryGetValueSafe(key, out num11))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num11)
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
              str4 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              num3 = MessagePackBinary.ReadInt64(bytes, offset, out readSize);
              break;
            case 5:
              num4 = MessagePackBinary.ReadInt64(bytes, offset, out readSize);
              break;
            case 6:
              str5 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 7:
              num5 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 8:
              str6 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 9:
              str7 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 10:
              str8 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 11:
              str9 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 12:
              num6 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 13:
              str10 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 14:
              num7 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 15:
              str11 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 16:
              num8 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 17:
              num9 = MessagePackBinary.ReadInt64(bytes, offset, out readSize);
              break;
            case 18:
              str12 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 19:
              num10 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new JSON_ChapterParam()
      {
        iname = str1,
        name = str2,
        expr = str3,
        world = str4,
        start = num3,
        end = num4,
        parent = str5,
        hide = num5,
        chap = str6,
        banr = str7,
        item = str8,
        keyitem1 = str9,
        keynum1 = num6,
        keyitem2 = str10,
        keynum2 = num7,
        keyitem3 = str11,
        keynum3 = num8,
        keytime = num9,
        hurl = str12,
        limit = num10
      };
    }
  }
}
