// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.ReqGuildRaidMailRead_ResponseFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class ReqGuildRaidMailRead_ResponseFormatter : 
    IMessagePackFormatter<ReqGuildRaidMailRead.Response>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public ReqGuildRaidMailRead_ResponseFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "mails",
          0
        },
        {
          "processed",
          1
        },
        {
          "trophyprogs",
          2
        },
        {
          "bingoprogs",
          3
        },
        {
          "player",
          4
        },
        {
          "items",
          5
        },
        {
          "units",
          6
        },
        {
          "artifacts",
          7
        },
        {
          "cards",
          8
        },
        {
          "gift_mailids",
          9
        }
      };
      this.____stringByteKeys = new byte[10][]
      {
        MessagePackBinary.GetEncodedStringBytes("mails"),
        MessagePackBinary.GetEncodedStringBytes("processed"),
        MessagePackBinary.GetEncodedStringBytes("trophyprogs"),
        MessagePackBinary.GetEncodedStringBytes("bingoprogs"),
        MessagePackBinary.GetEncodedStringBytes("player"),
        MessagePackBinary.GetEncodedStringBytes("items"),
        MessagePackBinary.GetEncodedStringBytes("units"),
        MessagePackBinary.GetEncodedStringBytes("artifacts"),
        MessagePackBinary.GetEncodedStringBytes("cards"),
        MessagePackBinary.GetEncodedStringBytes("gift_mailids")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      ReqGuildRaidMailRead.Response value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 10);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GuildRaidMail>().Serialize(ref bytes, offset, value.mails, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GuildRaidMailListItem[]>().Serialize(ref bytes, offset, value.processed, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_TrophyProgress[]>().Serialize(ref bytes, offset, value.trophyprogs, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_TrophyProgress[]>().Serialize(ref bytes, offset, value.bingoprogs, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<Json_PlayerData>().Serialize(ref bytes, offset, value.player, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<Json_Item[]>().Serialize(ref bytes, offset, value.items, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += formatterResolver.GetFormatterWithVerify<Json_Unit[]>().Serialize(ref bytes, offset, value.units, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += formatterResolver.GetFormatterWithVerify<Json_Artifact[]>().Serialize(ref bytes, offset, value.artifacts, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_ConceptCard[]>().Serialize(ref bytes, offset, value.cards, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.gift_mailids, formatterResolver);
      return offset - num;
    }

    public ReqGuildRaidMailRead.Response Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (ReqGuildRaidMailRead.Response) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      JSON_GuildRaidMail jsonGuildRaidMail = (JSON_GuildRaidMail) null;
      JSON_GuildRaidMailListItem[] raidMailListItemArray = (JSON_GuildRaidMailListItem[]) null;
      JSON_TrophyProgress[] jsonTrophyProgressArray1 = (JSON_TrophyProgress[]) null;
      JSON_TrophyProgress[] jsonTrophyProgressArray2 = (JSON_TrophyProgress[]) null;
      Json_PlayerData jsonPlayerData = (Json_PlayerData) null;
      Json_Item[] jsonItemArray = (Json_Item[]) null;
      Json_Unit[] jsonUnitArray = (Json_Unit[]) null;
      Json_Artifact[] jsonArtifactArray = (Json_Artifact[]) null;
      JSON_ConceptCard[] jsonConceptCardArray = (JSON_ConceptCard[]) null;
      int[] numArray = (int[]) null;
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
              jsonGuildRaidMail = formatterResolver.GetFormatterWithVerify<JSON_GuildRaidMail>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              raidMailListItemArray = formatterResolver.GetFormatterWithVerify<JSON_GuildRaidMailListItem[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              jsonTrophyProgressArray1 = formatterResolver.GetFormatterWithVerify<JSON_TrophyProgress[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              jsonTrophyProgressArray2 = formatterResolver.GetFormatterWithVerify<JSON_TrophyProgress[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              jsonPlayerData = formatterResolver.GetFormatterWithVerify<Json_PlayerData>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 5:
              jsonItemArray = formatterResolver.GetFormatterWithVerify<Json_Item[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 6:
              jsonUnitArray = formatterResolver.GetFormatterWithVerify<Json_Unit[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 7:
              jsonArtifactArray = formatterResolver.GetFormatterWithVerify<Json_Artifact[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 8:
              jsonConceptCardArray = formatterResolver.GetFormatterWithVerify<JSON_ConceptCard[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 9:
              numArray = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new ReqGuildRaidMailRead.Response()
      {
        mails = jsonGuildRaidMail,
        processed = raidMailListItemArray,
        trophyprogs = jsonTrophyProgressArray1,
        bingoprogs = jsonTrophyProgressArray2,
        player = jsonPlayerData,
        items = jsonItemArray,
        units = jsonUnitArray,
        artifacts = jsonArtifactArray,
        cards = jsonConceptCardArray,
        gift_mailids = numArray
      };
    }
  }
}
