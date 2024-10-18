// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.JukeBoxParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;
using System.Collections.Generic;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class JukeBoxParamFormatter : 
    IMessagePackFormatter<JukeBoxParam>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public JukeBoxParamFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "Iname",
          0
        },
        {
          "Sheet",
          1
        },
        {
          "Cue",
          2
        },
        {
          "SectionId",
          3
        },
        {
          "Title",
          4
        },
        {
          "TitleEn",
          5
        },
        {
          "Lyricist",
          6
        },
        {
          "Composer",
          7
        },
        {
          "Situation",
          8
        },
        {
          "DefaultUnlock",
          9
        },
        {
          "ExternalLink",
          10
        },
        {
          "Rate",
          11
        },
        {
          "UnlockType",
          12
        },
        {
          "CondList",
          13
        },
        {
          "CondQuest",
          14
        }
      };
      this.____stringByteKeys = new byte[15][]
      {
        MessagePackBinary.GetEncodedStringBytes("Iname"),
        MessagePackBinary.GetEncodedStringBytes("Sheet"),
        MessagePackBinary.GetEncodedStringBytes("Cue"),
        MessagePackBinary.GetEncodedStringBytes("SectionId"),
        MessagePackBinary.GetEncodedStringBytes("Title"),
        MessagePackBinary.GetEncodedStringBytes("TitleEn"),
        MessagePackBinary.GetEncodedStringBytes("Lyricist"),
        MessagePackBinary.GetEncodedStringBytes("Composer"),
        MessagePackBinary.GetEncodedStringBytes("Situation"),
        MessagePackBinary.GetEncodedStringBytes("DefaultUnlock"),
        MessagePackBinary.GetEncodedStringBytes("ExternalLink"),
        MessagePackBinary.GetEncodedStringBytes("Rate"),
        MessagePackBinary.GetEncodedStringBytes("UnlockType"),
        MessagePackBinary.GetEncodedStringBytes("CondList"),
        MessagePackBinary.GetEncodedStringBytes("CondQuest")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      JukeBoxParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 15);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.Iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.Sheet, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.Cue, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.SectionId, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.Title, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.TitleEn, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.Lyricist, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.Composer, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.Situation, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.DefaultUnlock);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.ExternalLink);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Rate);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += formatterResolver.GetFormatterWithVerify<JukeBoxParam.eUnlockType>().Serialize(ref bytes, offset, value.UnlockType, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += formatterResolver.GetFormatterWithVerify<List<string>>().Serialize(ref bytes, offset, value.CondList, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.CondQuest, formatterResolver);
      return offset - num;
    }

    public JukeBoxParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (JukeBoxParam) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      string str1 = (string) null;
      string str2 = (string) null;
      string str3 = (string) null;
      string str4 = (string) null;
      string str5 = (string) null;
      string str6 = (string) null;
      string str7 = (string) null;
      string str8 = (string) null;
      string str9 = (string) null;
      bool flag = false;
      int num3 = 0;
      int num4 = 0;
      JukeBoxParam.eUnlockType eUnlockType = JukeBoxParam.eUnlockType.QUEST;
      List<string> stringList = (List<string>) null;
      string str10 = (string) null;
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
              str5 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 5:
              str6 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 6:
              str7 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 7:
              str8 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 8:
              str9 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 9:
              flag = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 10:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 11:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 12:
              eUnlockType = formatterResolver.GetFormatterWithVerify<JukeBoxParam.eUnlockType>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 13:
              stringList = formatterResolver.GetFormatterWithVerify<List<string>>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 14:
              str10 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new JukeBoxParam();
    }
  }
}
