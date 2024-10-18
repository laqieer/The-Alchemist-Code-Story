// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.Json_TrophyPlayerDataAllFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class Json_TrophyPlayerDataAllFormatter : 
    IMessagePackFormatter<Json_TrophyPlayerDataAll>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public Json_TrophyPlayerDataAllFormatter()
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
          "concept_cards",
          3
        },
        {
          "star_mission",
          4
        }
      };
      this.____stringByteKeys = new byte[5][]
      {
        MessagePackBinary.GetEncodedStringBytes("player"),
        MessagePackBinary.GetEncodedStringBytes("units"),
        MessagePackBinary.GetEncodedStringBytes("items"),
        MessagePackBinary.GetEncodedStringBytes("concept_cards"),
        MessagePackBinary.GetEncodedStringBytes("star_mission")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      Json_TrophyPlayerDataAll value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 5);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<Json_TrophyPlayerData>().Serialize(ref bytes, offset, value.player, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<Json_Unit[]>().Serialize(ref bytes, offset, value.units, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<Json_Item[]>().Serialize(ref bytes, offset, value.items, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<Json_TrophyConceptCards>().Serialize(ref bytes, offset, value.concept_cards, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<ReqTrophyStarMission.StarMission>().Serialize(ref bytes, offset, value.star_mission, formatterResolver);
      return offset - num;
    }

    public Json_TrophyPlayerDataAll Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (Json_TrophyPlayerDataAll) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      Json_TrophyPlayerData trophyPlayerData = (Json_TrophyPlayerData) null;
      Json_Unit[] jsonUnitArray = (Json_Unit[]) null;
      Json_Item[] jsonItemArray = (Json_Item[]) null;
      Json_TrophyConceptCards trophyConceptCards = (Json_TrophyConceptCards) null;
      ReqTrophyStarMission.StarMission starMission = (ReqTrophyStarMission.StarMission) null;
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
              trophyPlayerData = formatterResolver.GetFormatterWithVerify<Json_TrophyPlayerData>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              jsonUnitArray = formatterResolver.GetFormatterWithVerify<Json_Unit[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              jsonItemArray = formatterResolver.GetFormatterWithVerify<Json_Item[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              trophyConceptCards = formatterResolver.GetFormatterWithVerify<Json_TrophyConceptCards>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              starMission = formatterResolver.GetFormatterWithVerify<ReqTrophyStarMission.StarMission>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new Json_TrophyPlayerDataAll()
      {
        player = trophyPlayerData,
        units = jsonUnitArray,
        items = jsonItemArray,
        concept_cards = trophyConceptCards,
        star_mission = starMission
      };
    }
  }
}
