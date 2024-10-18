// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.ReqTrophyStarMissionGetReward_ResponseFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class ReqTrophyStarMissionGetReward_ResponseFormatter : 
    IMessagePackFormatter<ReqTrophyStarMissionGetReward.Response>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public ReqTrophyStarMissionGetReward_ResponseFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "star_mission",
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
          "artifacts",
          5
        },
        {
          "trophyprogs",
          6
        },
        {
          "bingoprogs",
          7
        }
      };
      this.____stringByteKeys = new byte[8][]
      {
        MessagePackBinary.GetEncodedStringBytes("star_mission"),
        MessagePackBinary.GetEncodedStringBytes("player"),
        MessagePackBinary.GetEncodedStringBytes("items"),
        MessagePackBinary.GetEncodedStringBytes("units"),
        MessagePackBinary.GetEncodedStringBytes("cards"),
        MessagePackBinary.GetEncodedStringBytes("artifacts"),
        MessagePackBinary.GetEncodedStringBytes("trophyprogs"),
        MessagePackBinary.GetEncodedStringBytes("bingoprogs")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      ReqTrophyStarMissionGetReward.Response value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 8);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<ReqTrophyStarMission.StarMission>().Serialize(ref bytes, offset, value.star_mission, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<Json_PlayerData>().Serialize(ref bytes, offset, value.player, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<Json_Item[]>().Serialize(ref bytes, offset, value.items, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<Json_Unit[]>().Serialize(ref bytes, offset, value.units, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<ReqTrophyStarMissionGetReward.Response.JSON_StarMissionConceptCard[]>().Serialize(ref bytes, offset, value.cards, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<Json_Artifact[]>().Serialize(ref bytes, offset, value.artifacts, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_TrophyProgress[]>().Serialize(ref bytes, offset, value.trophyprogs, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_TrophyProgress[]>().Serialize(ref bytes, offset, value.bingoprogs, formatterResolver);
      return offset - num;
    }

    public ReqTrophyStarMissionGetReward.Response Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (ReqTrophyStarMissionGetReward.Response) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      ReqTrophyStarMission.StarMission starMission = (ReqTrophyStarMission.StarMission) null;
      Json_PlayerData jsonPlayerData = (Json_PlayerData) null;
      Json_Item[] jsonItemArray = (Json_Item[]) null;
      Json_Unit[] jsonUnitArray = (Json_Unit[]) null;
      ReqTrophyStarMissionGetReward.Response.JSON_StarMissionConceptCard[] missionConceptCardArray = (ReqTrophyStarMissionGetReward.Response.JSON_StarMissionConceptCard[]) null;
      Json_Artifact[] jsonArtifactArray = (Json_Artifact[]) null;
      JSON_TrophyProgress[] jsonTrophyProgressArray1 = (JSON_TrophyProgress[]) null;
      JSON_TrophyProgress[] jsonTrophyProgressArray2 = (JSON_TrophyProgress[]) null;
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
              starMission = formatterResolver.GetFormatterWithVerify<ReqTrophyStarMission.StarMission>().Deserialize(bytes, offset, formatterResolver, out readSize);
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
              missionConceptCardArray = formatterResolver.GetFormatterWithVerify<ReqTrophyStarMissionGetReward.Response.JSON_StarMissionConceptCard[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 5:
              jsonArtifactArray = formatterResolver.GetFormatterWithVerify<Json_Artifact[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 6:
              jsonTrophyProgressArray1 = formatterResolver.GetFormatterWithVerify<JSON_TrophyProgress[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 7:
              jsonTrophyProgressArray2 = formatterResolver.GetFormatterWithVerify<JSON_TrophyProgress[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new ReqTrophyStarMissionGetReward.Response()
      {
        star_mission = starMission,
        player = jsonPlayerData,
        items = jsonItemArray,
        units = jsonUnitArray,
        cards = missionConceptCardArray,
        artifacts = jsonArtifactArray,
        trophyprogs = jsonTrophyProgressArray1,
        bingoprogs = jsonTrophyProgressArray2
      };
    }
  }
}
