// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.JSON_JukeBoxParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class JSON_JukeBoxParamFormatter : 
    IMessagePackFormatter<JSON_JukeBoxParam>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public JSON_JukeBoxParamFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "iname",
          0
        },
        {
          "sheet",
          1
        },
        {
          "cue",
          2
        },
        {
          "section",
          3
        },
        {
          "default_unlock",
          4
        },
        {
          "external_link",
          5
        },
        {
          "rate",
          6
        },
        {
          "range_unit",
          7
        },
        {
          "quests",
          8
        },
        {
          "cond_quest",
          9
        }
      };
      this.____stringByteKeys = new byte[10][]
      {
        MessagePackBinary.GetEncodedStringBytes("iname"),
        MessagePackBinary.GetEncodedStringBytes("sheet"),
        MessagePackBinary.GetEncodedStringBytes("cue"),
        MessagePackBinary.GetEncodedStringBytes("section"),
        MessagePackBinary.GetEncodedStringBytes("default_unlock"),
        MessagePackBinary.GetEncodedStringBytes("external_link"),
        MessagePackBinary.GetEncodedStringBytes("rate"),
        MessagePackBinary.GetEncodedStringBytes("range_unit"),
        MessagePackBinary.GetEncodedStringBytes("quests"),
        MessagePackBinary.GetEncodedStringBytes("cond_quest")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      JSON_JukeBoxParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 10);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.sheet, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.cue, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.section, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.default_unlock);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.external_link);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rate);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.range_unit);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.quests, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.cond_quest, formatterResolver);
      return offset - num;
    }

    public JSON_JukeBoxParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (JSON_JukeBoxParam) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      string str1 = (string) null;
      string str2 = (string) null;
      string str3 = (string) null;
      string str4 = (string) null;
      int num3 = 0;
      int num4 = 0;
      int num5 = 0;
      int num6 = 0;
      string[] strArray = (string[]) null;
      string str5 = (string) null;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num7;
        if (!this.____keyMapping.TryGetValueSafe(key, out num7))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num7)
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
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
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
              strArray = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 9:
              str5 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new JSON_JukeBoxParam()
      {
        iname = str1,
        sheet = str2,
        cue = str3,
        section = str4,
        default_unlock = num3,
        external_link = num4,
        rate = num5,
        range_unit = num6,
        quests = strArray,
        cond_quest = str5
      };
    }
  }
}
