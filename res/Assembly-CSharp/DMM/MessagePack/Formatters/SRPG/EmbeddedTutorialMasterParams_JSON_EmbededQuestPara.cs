// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.EmbeddedTutorialMasterParams_JSON_EmbededQuestParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class EmbeddedTutorialMasterParams_JSON_EmbededQuestParamFormatter : 
    IMessagePackFormatter<EmbeddedTutorialMasterParams.JSON_EmbededQuestParam>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public EmbeddedTutorialMasterParams_JSON_EmbededQuestParamFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "worlds",
          0
        },
        {
          "areas",
          1
        },
        {
          "quests",
          2
        }
      };
      this.____stringByteKeys = new byte[3][]
      {
        MessagePackBinary.GetEncodedStringBytes("worlds"),
        MessagePackBinary.GetEncodedStringBytes("areas"),
        MessagePackBinary.GetEncodedStringBytes("quests")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      EmbeddedTutorialMasterParams.JSON_EmbededQuestParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 3);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_SectionParam[]>().Serialize(ref bytes, offset, value.worlds, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_ChapterParam[]>().Serialize(ref bytes, offset, value.areas, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_QuestParam[]>().Serialize(ref bytes, offset, value.quests, formatterResolver);
      return offset - num;
    }

    public EmbeddedTutorialMasterParams.JSON_EmbededQuestParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (EmbeddedTutorialMasterParams.JSON_EmbededQuestParam) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      JSON_SectionParam[] jsonSectionParamArray = (JSON_SectionParam[]) null;
      JSON_ChapterParam[] jsonChapterParamArray = (JSON_ChapterParam[]) null;
      JSON_QuestParam[] jsonQuestParamArray = (JSON_QuestParam[]) null;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num3;
        if (!this.____keyMapping.TryGetValueSafe(key, out num3))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num3)
          {
            case 0:
              jsonSectionParamArray = formatterResolver.GetFormatterWithVerify<JSON_SectionParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              jsonChapterParamArray = formatterResolver.GetFormatterWithVerify<JSON_ChapterParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              jsonQuestParamArray = formatterResolver.GetFormatterWithVerify<JSON_QuestParam[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new EmbeddedTutorialMasterParams.JSON_EmbededQuestParam()
      {
        worlds = jsonSectionParamArray,
        areas = jsonChapterParamArray,
        quests = jsonQuestParamArray
      };
    }
  }
}
