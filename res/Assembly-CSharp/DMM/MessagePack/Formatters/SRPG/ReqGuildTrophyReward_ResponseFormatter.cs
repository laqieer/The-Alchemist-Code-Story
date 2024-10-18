// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.ReqGuildTrophyReward_ResponseFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class ReqGuildTrophyReward_ResponseFormatter : 
    IMessagePackFormatter<ReqGuildTrophyReward.Response>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public ReqGuildTrophyReward_ResponseFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "guild_trophies",
          0
        },
        {
          "player",
          1
        },
        {
          "units",
          2
        },
        {
          "items",
          3
        },
        {
          "cards",
          4
        },
        {
          "artifacts",
          5
        }
      };
      this.____stringByteKeys = new byte[6][]
      {
        MessagePackBinary.GetEncodedStringBytes("guild_trophies"),
        MessagePackBinary.GetEncodedStringBytes("player"),
        MessagePackBinary.GetEncodedStringBytes("units"),
        MessagePackBinary.GetEncodedStringBytes("items"),
        MessagePackBinary.GetEncodedStringBytes("cards"),
        MessagePackBinary.GetEncodedStringBytes("artifacts")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      ReqGuildTrophyReward.Response value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 6);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_TrophyProgress[]>().Serialize(ref bytes, offset, value.guild_trophies, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<Json_TrophyPlayerData>().Serialize(ref bytes, offset, value.player, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<Json_Unit[]>().Serialize(ref bytes, offset, value.units, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<Json_Item[]>().Serialize(ref bytes, offset, value.items, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<Json_TrophyConceptCards>().Serialize(ref bytes, offset, value.cards, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<Json_Artifact[]>().Serialize(ref bytes, offset, value.artifacts, formatterResolver);
      return offset - num;
    }

    public ReqGuildTrophyReward.Response Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (ReqGuildTrophyReward.Response) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      JSON_TrophyProgress[] jsonTrophyProgressArray = (JSON_TrophyProgress[]) null;
      Json_TrophyPlayerData trophyPlayerData = (Json_TrophyPlayerData) null;
      Json_Unit[] jsonUnitArray = (Json_Unit[]) null;
      Json_Item[] jsonItemArray = (Json_Item[]) null;
      Json_TrophyConceptCards trophyConceptCards = (Json_TrophyConceptCards) null;
      Json_Artifact[] jsonArtifactArray = (Json_Artifact[]) null;
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
              jsonTrophyProgressArray = formatterResolver.GetFormatterWithVerify<JSON_TrophyProgress[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              trophyPlayerData = formatterResolver.GetFormatterWithVerify<Json_TrophyPlayerData>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              jsonUnitArray = formatterResolver.GetFormatterWithVerify<Json_Unit[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              jsonItemArray = formatterResolver.GetFormatterWithVerify<Json_Item[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              trophyConceptCards = formatterResolver.GetFormatterWithVerify<Json_TrophyConceptCards>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 5:
              jsonArtifactArray = formatterResolver.GetFormatterWithVerify<Json_Artifact[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new ReqGuildTrophyReward.Response()
      {
        guild_trophies = jsonTrophyProgressArray,
        player = trophyPlayerData,
        units = jsonUnitArray,
        items = jsonItemArray,
        cards = trophyConceptCards,
        artifacts = jsonArtifactArray
      };
    }
  }
}
