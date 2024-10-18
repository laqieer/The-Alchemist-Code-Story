// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.ReqDrawCardExec_ResponseFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class ReqDrawCardExec_ResponseFormatter : 
    IMessagePackFormatter<ReqDrawCardExec.Response>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public ReqDrawCardExec_ResponseFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "draw_info",
          0
        },
        {
          "drawcard_current_status",
          1
        },
        {
          "rewards",
          2
        },
        {
          "player",
          3
        },
        {
          "items",
          4
        },
        {
          "units",
          5
        },
        {
          "cards",
          6
        },
        {
          "artifacts",
          7
        },
        {
          "trophyprogs",
          8
        },
        {
          "bingoprogs",
          9
        }
      };
      this.____stringByteKeys = new byte[10][]
      {
        MessagePackBinary.GetEncodedStringBytes("draw_info"),
        MessagePackBinary.GetEncodedStringBytes("drawcard_current_status"),
        MessagePackBinary.GetEncodedStringBytes("rewards"),
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
      ReqDrawCardExec.Response value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 10);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<ReqDrawCard.CardInfo>().Serialize(ref bytes, offset, value.draw_info, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<ReqDrawCard.Response.Status>().Serialize(ref bytes, offset, value.drawcard_current_status, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<ReqDrawCard.CardInfo.Card[]>().Serialize(ref bytes, offset, value.rewards, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<Json_PlayerData>().Serialize(ref bytes, offset, value.player, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<Json_Item[]>().Serialize(ref bytes, offset, value.items, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<Json_Unit[]>().Serialize(ref bytes, offset, value.units, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_ConceptCard[]>().Serialize(ref bytes, offset, value.cards, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += formatterResolver.GetFormatterWithVerify<Json_Artifact[]>().Serialize(ref bytes, offset, value.artifacts, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_TrophyProgress[]>().Serialize(ref bytes, offset, value.trophyprogs, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_TrophyProgress[]>().Serialize(ref bytes, offset, value.bingoprogs, formatterResolver);
      return offset - num;
    }

    public ReqDrawCardExec.Response Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (ReqDrawCardExec.Response) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      ReqDrawCard.CardInfo cardInfo = (ReqDrawCard.CardInfo) null;
      ReqDrawCard.Response.Status status = (ReqDrawCard.Response.Status) null;
      ReqDrawCard.CardInfo.Card[] cardArray = (ReqDrawCard.CardInfo.Card[]) null;
      Json_PlayerData jsonPlayerData = (Json_PlayerData) null;
      Json_Item[] jsonItemArray = (Json_Item[]) null;
      Json_Unit[] jsonUnitArray = (Json_Unit[]) null;
      JSON_ConceptCard[] jsonConceptCardArray = (JSON_ConceptCard[]) null;
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
              cardInfo = formatterResolver.GetFormatterWithVerify<ReqDrawCard.CardInfo>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              status = formatterResolver.GetFormatterWithVerify<ReqDrawCard.Response.Status>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 2:
              cardArray = formatterResolver.GetFormatterWithVerify<ReqDrawCard.CardInfo.Card[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              jsonPlayerData = formatterResolver.GetFormatterWithVerify<Json_PlayerData>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              jsonItemArray = formatterResolver.GetFormatterWithVerify<Json_Item[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 5:
              jsonUnitArray = formatterResolver.GetFormatterWithVerify<Json_Unit[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 6:
              jsonConceptCardArray = formatterResolver.GetFormatterWithVerify<JSON_ConceptCard[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 7:
              jsonArtifactArray = formatterResolver.GetFormatterWithVerify<Json_Artifact[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 8:
              jsonTrophyProgressArray1 = formatterResolver.GetFormatterWithVerify<JSON_TrophyProgress[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 9:
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
      return new ReqDrawCardExec.Response()
      {
        draw_info = cardInfo,
        drawcard_current_status = status,
        rewards = cardArray,
        player = jsonPlayerData,
        items = jsonItemArray,
        units = jsonUnitArray,
        cards = jsonConceptCardArray,
        artifacts = jsonArtifactArray,
        trophyprogs = jsonTrophyProgressArray1,
        bingoprogs = jsonTrophyProgressArray2
      };
    }
  }
}
