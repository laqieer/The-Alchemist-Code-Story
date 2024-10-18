// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.ReqAutoRepeatQuestEnd_ResponseFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class ReqAutoRepeatQuestEnd_ResponseFormatter : 
    IMessagePackFormatter<ReqAutoRepeatQuestEnd.Response>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public ReqAutoRepeatQuestEnd_ResponseFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "auto_repeat",
          0
        },
        {
          "player",
          1
        },
        {
          "items",
          2
        },
        {
          "units",
          3
        },
        {
          "cards",
          4
        },
        {
          "trophyprogs",
          5
        },
        {
          "bingoprogs",
          6
        },
        {
          "guild_trophies",
          7
        },
        {
          "quests",
          8
        },
        {
          "area",
          9
        },
        {
          "guildraid_bp_charge",
          10
        },
        {
          "rune_storage_used",
          11
        },
        {
          "story_ex_challenge",
          12
        },
        {
          "runes_detail",
          13
        }
      };
      this.____stringByteKeys = new byte[14][]
      {
        MessagePackBinary.GetEncodedStringBytes("auto_repeat"),
        MessagePackBinary.GetEncodedStringBytes("player"),
        MessagePackBinary.GetEncodedStringBytes("items"),
        MessagePackBinary.GetEncodedStringBytes("units"),
        MessagePackBinary.GetEncodedStringBytes("cards"),
        MessagePackBinary.GetEncodedStringBytes("trophyprogs"),
        MessagePackBinary.GetEncodedStringBytes("bingoprogs"),
        MessagePackBinary.GetEncodedStringBytes("guild_trophies"),
        MessagePackBinary.GetEncodedStringBytes("quests"),
        MessagePackBinary.GetEncodedStringBytes("area"),
        MessagePackBinary.GetEncodedStringBytes("guildraid_bp_charge"),
        MessagePackBinary.GetEncodedStringBytes("rune_storage_used"),
        MessagePackBinary.GetEncodedStringBytes("story_ex_challenge"),
        MessagePackBinary.GetEncodedStringBytes("runes_detail")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      ReqAutoRepeatQuestEnd.Response value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 14);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<Json_AutoRepeatQuestData>().Serialize(ref bytes, offset, value.auto_repeat, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<Json_PlayerData>().Serialize(ref bytes, offset, value.player, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<Json_Item[]>().Serialize(ref bytes, offset, value.items, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<Json_Unit[]>().Serialize(ref bytes, offset, value.units, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<Json_BtlRewardConceptCard[]>().Serialize(ref bytes, offset, value.cards, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_TrophyProgress[]>().Serialize(ref bytes, offset, value.trophyprogs, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_TrophyProgress[]>().Serialize(ref bytes, offset, value.bingoprogs, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_TrophyProgress[]>().Serialize(ref bytes, offset, value.guild_trophies, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_QuestProgress[]>().Serialize(ref bytes, offset, value.quests, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_ChapterCount>().Serialize(ref bytes, offset, value.area, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.guildraid_bp_charge);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rune_storage_used);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_StoryExChallengeCount>().Serialize(ref bytes, offset, value.story_ex_challenge, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += formatterResolver.GetFormatterWithVerify<Json_RuneData[]>().Serialize(ref bytes, offset, value.runes_detail, formatterResolver);
      return offset - num;
    }

    public ReqAutoRepeatQuestEnd.Response Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (ReqAutoRepeatQuestEnd.Response) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      Json_AutoRepeatQuestData autoRepeatQuestData = (Json_AutoRepeatQuestData) null;
      Json_PlayerData jsonPlayerData = (Json_PlayerData) null;
      Json_Item[] jsonItemArray = (Json_Item[]) null;
      Json_Unit[] jsonUnitArray = (Json_Unit[]) null;
      Json_BtlRewardConceptCard[] rewardConceptCardArray = (Json_BtlRewardConceptCard[]) null;
      JSON_TrophyProgress[] jsonTrophyProgressArray1 = (JSON_TrophyProgress[]) null;
      JSON_TrophyProgress[] jsonTrophyProgressArray2 = (JSON_TrophyProgress[]) null;
      JSON_TrophyProgress[] jsonTrophyProgressArray3 = (JSON_TrophyProgress[]) null;
      JSON_QuestProgress[] jsonQuestProgressArray = (JSON_QuestProgress[]) null;
      JSON_ChapterCount jsonChapterCount = (JSON_ChapterCount) null;
      int num3 = 0;
      int num4 = 0;
      JSON_StoryExChallengeCount exChallengeCount = (JSON_StoryExChallengeCount) null;
      Json_RuneData[] jsonRuneDataArray = (Json_RuneData[]) null;
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
              autoRepeatQuestData = formatterResolver.GetFormatterWithVerify<Json_AutoRepeatQuestData>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              jsonPlayerData = formatterResolver.GetFormatterWithVerify<Json_PlayerData>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              jsonItemArray = formatterResolver.GetFormatterWithVerify<Json_Item[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              jsonUnitArray = formatterResolver.GetFormatterWithVerify<Json_Unit[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              rewardConceptCardArray = formatterResolver.GetFormatterWithVerify<Json_BtlRewardConceptCard[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 5:
              jsonTrophyProgressArray1 = formatterResolver.GetFormatterWithVerify<JSON_TrophyProgress[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 6:
              jsonTrophyProgressArray2 = formatterResolver.GetFormatterWithVerify<JSON_TrophyProgress[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 7:
              jsonTrophyProgressArray3 = formatterResolver.GetFormatterWithVerify<JSON_TrophyProgress[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 8:
              jsonQuestProgressArray = formatterResolver.GetFormatterWithVerify<JSON_QuestProgress[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 9:
              jsonChapterCount = formatterResolver.GetFormatterWithVerify<JSON_ChapterCount>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 10:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 11:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 12:
              exChallengeCount = formatterResolver.GetFormatterWithVerify<JSON_StoryExChallengeCount>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 13:
              jsonRuneDataArray = formatterResolver.GetFormatterWithVerify<Json_RuneData[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new ReqAutoRepeatQuestEnd.Response()
      {
        auto_repeat = autoRepeatQuestData,
        player = jsonPlayerData,
        items = jsonItemArray,
        units = jsonUnitArray,
        cards = rewardConceptCardArray,
        trophyprogs = jsonTrophyProgressArray1,
        bingoprogs = jsonTrophyProgressArray2,
        guild_trophies = jsonTrophyProgressArray3,
        quests = jsonQuestProgressArray,
        area = jsonChapterCount,
        guildraid_bp_charge = num3,
        rune_storage_used = num4,
        story_ex_challenge = exChallengeCount,
        runes_detail = jsonRuneDataArray
      };
    }
  }
}
