// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.ReqGuildRoleBonus_ResponseFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class ReqGuildRoleBonus_ResponseFormatter : 
    IMessagePackFormatter<ReqGuildRoleBonus.Response>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public ReqGuildRoleBonus_ResponseFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "rewards",
          0
        },
        {
          "status",
          1
        },
        {
          "player",
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
          "runes",
          5
        },
        {
          "rune_storage_used",
          6
        }
      };
      this.____stringByteKeys = new byte[7][]
      {
        MessagePackBinary.GetEncodedStringBytes("rewards"),
        MessagePackBinary.GetEncodedStringBytes("status"),
        MessagePackBinary.GetEncodedStringBytes("player"),
        MessagePackBinary.GetEncodedStringBytes("items"),
        MessagePackBinary.GetEncodedStringBytes("cards"),
        MessagePackBinary.GetEncodedStringBytes("runes"),
        MessagePackBinary.GetEncodedStringBytes("rune_storage_used")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      ReqGuildRoleBonus.Response value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 7);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_GUildRoleBonusReward[]>().Serialize(ref bytes, offset, value.rewards, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.status);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<Json_PlayerData>().Serialize(ref bytes, offset, value.player, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<Json_Item[]>().Serialize(ref bytes, offset, value.items, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_ConceptCard[]>().Serialize(ref bytes, offset, value.cards, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<Json_RuneData[]>().Serialize(ref bytes, offset, value.runes, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rune_storage_used);
      return offset - num;
    }

    public ReqGuildRoleBonus.Response Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (ReqGuildRoleBonus.Response) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      JSON_GUildRoleBonusReward[] guildRoleBonusRewardArray = (JSON_GUildRoleBonusReward[]) null;
      int num3 = 0;
      Json_PlayerData jsonPlayerData = (Json_PlayerData) null;
      Json_Item[] jsonItemArray = (Json_Item[]) null;
      JSON_ConceptCard[] jsonConceptCardArray = (JSON_ConceptCard[]) null;
      Json_RuneData[] jsonRuneDataArray = (Json_RuneData[]) null;
      int num4 = 0;
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
              guildRoleBonusRewardArray = formatterResolver.GetFormatterWithVerify<JSON_GUildRoleBonusReward[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 2:
              jsonPlayerData = formatterResolver.GetFormatterWithVerify<Json_PlayerData>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              jsonItemArray = formatterResolver.GetFormatterWithVerify<Json_Item[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              jsonConceptCardArray = formatterResolver.GetFormatterWithVerify<JSON_ConceptCard[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 5:
              jsonRuneDataArray = formatterResolver.GetFormatterWithVerify<Json_RuneData[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 6:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new ReqGuildRoleBonus.Response()
      {
        rewards = guildRoleBonusRewardArray,
        status = num3,
        player = jsonPlayerData,
        items = jsonItemArray,
        cards = jsonConceptCardArray,
        runes = jsonRuneDataArray,
        rune_storage_used = num4
      };
    }
  }
}
