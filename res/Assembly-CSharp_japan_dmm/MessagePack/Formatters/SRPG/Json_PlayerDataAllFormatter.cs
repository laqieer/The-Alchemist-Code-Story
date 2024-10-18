// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.Json_PlayerDataAllFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class Json_PlayerDataAllFormatter : 
    IMessagePackFormatter<Json_PlayerDataAll>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public Json_PlayerDataAllFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "player",
          0
        },
        {
          "units",
          1
        },
        {
          "items",
          2
        },
        {
          "mails",
          3
        },
        {
          "parties",
          4
        },
        {
          "friends",
          5
        },
        {
          "artifacts",
          6
        },
        {
          "concept_cards",
          7
        },
        {
          "skins",
          8
        },
        {
          "notify",
          9
        },
        {
          "fuids",
          10
        },
        {
          "status",
          11
        },
        {
          "cuid",
          12
        },
        {
          "tut",
          13
        },
        {
          "first_contact",
          14
        },
        {
          "vs",
          15
        },
        {
          "tips",
          16
        },
        {
          "player_guild",
          17
        },
        {
          "fu_status",
          18
        },
        {
          "expire_items",
          19
        },
        {
          "trophyprogs",
          20
        },
        {
          "bingoprogs",
          21
        },
        {
          "guild_trophies",
          22
        },
        {
          "party_decks",
          23
        },
        {
          "bgms",
          24
        },
        {
          "rune_storage",
          25
        },
        {
          "rune_storage_used",
          26
        },
        {
          "story_ex_challenge",
          27
        },
        {
          "expansions",
          28
        }
      };
      this.____stringByteKeys = new byte[29][]
      {
        MessagePackBinary.GetEncodedStringBytes("player"),
        MessagePackBinary.GetEncodedStringBytes("units"),
        MessagePackBinary.GetEncodedStringBytes("items"),
        MessagePackBinary.GetEncodedStringBytes("mails"),
        MessagePackBinary.GetEncodedStringBytes("parties"),
        MessagePackBinary.GetEncodedStringBytes("friends"),
        MessagePackBinary.GetEncodedStringBytes("artifacts"),
        MessagePackBinary.GetEncodedStringBytes("concept_cards"),
        MessagePackBinary.GetEncodedStringBytes("skins"),
        MessagePackBinary.GetEncodedStringBytes("notify"),
        MessagePackBinary.GetEncodedStringBytes("fuids"),
        MessagePackBinary.GetEncodedStringBytes("status"),
        MessagePackBinary.GetEncodedStringBytes("cuid"),
        MessagePackBinary.GetEncodedStringBytes("tut"),
        MessagePackBinary.GetEncodedStringBytes("first_contact"),
        MessagePackBinary.GetEncodedStringBytes("vs"),
        MessagePackBinary.GetEncodedStringBytes("tips"),
        MessagePackBinary.GetEncodedStringBytes("player_guild"),
        MessagePackBinary.GetEncodedStringBytes("fu_status"),
        MessagePackBinary.GetEncodedStringBytes("expire_items"),
        MessagePackBinary.GetEncodedStringBytes("trophyprogs"),
        MessagePackBinary.GetEncodedStringBytes("bingoprogs"),
        MessagePackBinary.GetEncodedStringBytes("guild_trophies"),
        MessagePackBinary.GetEncodedStringBytes("party_decks"),
        MessagePackBinary.GetEncodedStringBytes("bgms"),
        MessagePackBinary.GetEncodedStringBytes("rune_storage"),
        MessagePackBinary.GetEncodedStringBytes("rune_storage_used"),
        MessagePackBinary.GetEncodedStringBytes("story_ex_challenge"),
        MessagePackBinary.GetEncodedStringBytes("expansions")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      Json_PlayerDataAll value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, 29);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<Json_PlayerData>().Serialize(ref bytes, offset, value.player, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<Json_Unit[]>().Serialize(ref bytes, offset, value.units, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<Json_Item[]>().Serialize(ref bytes, offset, value.items, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<Json_Mail[]>().Serialize(ref bytes, offset, value.mails, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<Json_Party[]>().Serialize(ref bytes, offset, value.parties, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<Json_Friend[]>().Serialize(ref bytes, offset, value.friends, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += formatterResolver.GetFormatterWithVerify<Json_Artifact[]>().Serialize(ref bytes, offset, value.artifacts, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_ConceptCard[]>().Serialize(ref bytes, offset, value.concept_cards, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += formatterResolver.GetFormatterWithVerify<Json_Skin[]>().Serialize(ref bytes, offset, value.skins, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += formatterResolver.GetFormatterWithVerify<Json_Notify>().Serialize(ref bytes, offset, value.notify, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += formatterResolver.GetFormatterWithVerify<Json_MultiFuids[]>().Serialize(ref bytes, offset, value.fuids, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.status);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.cuid, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += MessagePackBinary.WriteInt64(ref bytes, offset, value.tut);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.first_contact);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
      offset += formatterResolver.GetFormatterWithVerify<Json_Versus>().Serialize(ref bytes, offset, value.vs, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[16]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.tips, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[17]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_PlayerGuild>().Serialize(ref bytes, offset, value.player_guild, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[18]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.fu_status, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[19]);
      offset += formatterResolver.GetFormatterWithVerify<Json_ExpireItem[]>().Serialize(ref bytes, offset, value.expire_items, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[20]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_TrophyProgress[]>().Serialize(ref bytes, offset, value.trophyprogs, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[21]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_TrophyProgress[]>().Serialize(ref bytes, offset, value.bingoprogs, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[22]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_TrophyProgress[]>().Serialize(ref bytes, offset, value.guild_trophies, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[23]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_PartyOverWrite[]>().Serialize(ref bytes, offset, value.party_decks, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[24]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.bgms, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[25]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rune_storage);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[26]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rune_storage_used);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[27]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_StoryExChallengeCount>().Serialize(ref bytes, offset, value.story_ex_challenge, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[28]);
      offset += formatterResolver.GetFormatterWithVerify<Json_ExpansionPurchase[]>().Serialize(ref bytes, offset, value.expansions, formatterResolver);
      return offset - num;
    }

    public Json_PlayerDataAll Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (Json_PlayerDataAll) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      Json_PlayerData jsonPlayerData = (Json_PlayerData) null;
      Json_Unit[] jsonUnitArray = (Json_Unit[]) null;
      Json_Item[] jsonItemArray = (Json_Item[]) null;
      Json_Mail[] jsonMailArray = (Json_Mail[]) null;
      Json_Party[] jsonPartyArray = (Json_Party[]) null;
      Json_Friend[] jsonFriendArray = (Json_Friend[]) null;
      Json_Artifact[] jsonArtifactArray = (Json_Artifact[]) null;
      JSON_ConceptCard[] jsonConceptCardArray = (JSON_ConceptCard[]) null;
      Json_Skin[] jsonSkinArray = (Json_Skin[]) null;
      Json_Notify jsonNotify = (Json_Notify) null;
      Json_MultiFuids[] jsonMultiFuidsArray = (Json_MultiFuids[]) null;
      int num3 = 0;
      string str1 = (string) null;
      long num4 = 0;
      int num5 = 0;
      Json_Versus jsonVersus = (Json_Versus) null;
      string[] strArray1 = (string[]) null;
      JSON_PlayerGuild jsonPlayerGuild = (JSON_PlayerGuild) null;
      string str2 = (string) null;
      Json_ExpireItem[] jsonExpireItemArray = (Json_ExpireItem[]) null;
      JSON_TrophyProgress[] jsonTrophyProgressArray1 = (JSON_TrophyProgress[]) null;
      JSON_TrophyProgress[] jsonTrophyProgressArray2 = (JSON_TrophyProgress[]) null;
      JSON_TrophyProgress[] jsonTrophyProgressArray3 = (JSON_TrophyProgress[]) null;
      JSON_PartyOverWrite[] jsonPartyOverWriteArray = (JSON_PartyOverWrite[]) null;
      string[] strArray2 = (string[]) null;
      int num6 = 0;
      int num7 = 0;
      JSON_StoryExChallengeCount exChallengeCount = (JSON_StoryExChallengeCount) null;
      Json_ExpansionPurchase[] expansionPurchaseArray = (Json_ExpansionPurchase[]) null;
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
              jsonPlayerData = formatterResolver.GetFormatterWithVerify<Json_PlayerData>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              jsonUnitArray = formatterResolver.GetFormatterWithVerify<Json_Unit[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              jsonItemArray = formatterResolver.GetFormatterWithVerify<Json_Item[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              jsonMailArray = formatterResolver.GetFormatterWithVerify<Json_Mail[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              jsonPartyArray = formatterResolver.GetFormatterWithVerify<Json_Party[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 5:
              jsonFriendArray = formatterResolver.GetFormatterWithVerify<Json_Friend[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 6:
              jsonArtifactArray = formatterResolver.GetFormatterWithVerify<Json_Artifact[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 7:
              jsonConceptCardArray = formatterResolver.GetFormatterWithVerify<JSON_ConceptCard[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 8:
              jsonSkinArray = formatterResolver.GetFormatterWithVerify<Json_Skin[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 9:
              jsonNotify = formatterResolver.GetFormatterWithVerify<Json_Notify>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 10:
              jsonMultiFuidsArray = formatterResolver.GetFormatterWithVerify<Json_MultiFuids[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 11:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 12:
              str1 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 13:
              num4 = MessagePackBinary.ReadInt64(bytes, offset, out readSize);
              break;
            case 14:
              num5 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 15:
              jsonVersus = formatterResolver.GetFormatterWithVerify<Json_Versus>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 16:
              strArray1 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 17:
              jsonPlayerGuild = formatterResolver.GetFormatterWithVerify<JSON_PlayerGuild>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 18:
              str2 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 19:
              jsonExpireItemArray = formatterResolver.GetFormatterWithVerify<Json_ExpireItem[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 20:
              jsonTrophyProgressArray1 = formatterResolver.GetFormatterWithVerify<JSON_TrophyProgress[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 21:
              jsonTrophyProgressArray2 = formatterResolver.GetFormatterWithVerify<JSON_TrophyProgress[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 22:
              jsonTrophyProgressArray3 = formatterResolver.GetFormatterWithVerify<JSON_TrophyProgress[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 23:
              jsonPartyOverWriteArray = formatterResolver.GetFormatterWithVerify<JSON_PartyOverWrite[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 24:
              strArray2 = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 25:
              num6 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 26:
              num7 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 27:
              exChallengeCount = formatterResolver.GetFormatterWithVerify<JSON_StoryExChallengeCount>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 28:
              expansionPurchaseArray = formatterResolver.GetFormatterWithVerify<Json_ExpansionPurchase[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new Json_PlayerDataAll()
      {
        player = jsonPlayerData,
        units = jsonUnitArray,
        items = jsonItemArray,
        mails = jsonMailArray,
        parties = jsonPartyArray,
        friends = jsonFriendArray,
        artifacts = jsonArtifactArray,
        concept_cards = jsonConceptCardArray,
        skins = jsonSkinArray,
        notify = jsonNotify,
        fuids = jsonMultiFuidsArray,
        status = num3,
        cuid = str1,
        tut = num4,
        first_contact = num5,
        vs = jsonVersus,
        tips = strArray1,
        player_guild = jsonPlayerGuild,
        fu_status = str2,
        expire_items = jsonExpireItemArray,
        trophyprogs = jsonTrophyProgressArray1,
        bingoprogs = jsonTrophyProgressArray2,
        guild_trophies = jsonTrophyProgressArray3,
        party_decks = jsonPartyOverWriteArray,
        bgms = strArray2,
        rune_storage = num6,
        rune_storage_used = num7,
        story_ex_challenge = exChallengeCount,
        expansions = expansionPurchaseArray
      };
    }
  }
}
