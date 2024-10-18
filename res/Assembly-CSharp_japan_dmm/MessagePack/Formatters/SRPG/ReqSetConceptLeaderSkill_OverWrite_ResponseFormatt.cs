// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.ReqSetConceptLeaderSkill_OverWrite_ResponseFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class ReqSetConceptLeaderSkill_OverWrite_ResponseFormatter : 
    IMessagePackFormatter<ReqSetConceptLeaderSkill_OverWrite.Response>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public ReqSetConceptLeaderSkill_OverWrite_ResponseFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "player",
          0
        },
        {
          "party_decks",
          1
        }
      };
      this.____stringByteKeys = new byte[2][]
      {
        MessagePackBinary.GetEncodedStringBytes("player"),
        MessagePackBinary.GetEncodedStringBytes("party_decks")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      ReqSetConceptLeaderSkill_OverWrite.Response value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 2);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<Json_PlayerData>().Serialize(ref bytes, offset, value.player, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_PartyOverWrite[]>().Serialize(ref bytes, offset, value.party_decks, formatterResolver);
      return offset - num;
    }

    public ReqSetConceptLeaderSkill_OverWrite.Response Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (ReqSetConceptLeaderSkill_OverWrite.Response) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      Json_PlayerData jsonPlayerData = (Json_PlayerData) null;
      JSON_PartyOverWrite[] jsonPartyOverWriteArray = (JSON_PartyOverWrite[]) null;
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
              jsonPlayerData = formatterResolver.GetFormatterWithVerify<Json_PlayerData>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              jsonPartyOverWriteArray = formatterResolver.GetFormatterWithVerify<JSON_PartyOverWrite[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new ReqSetConceptLeaderSkill_OverWrite.Response()
      {
        player = jsonPlayerData,
        party_decks = jsonPartyOverWriteArray
      };
    }
  }
}
