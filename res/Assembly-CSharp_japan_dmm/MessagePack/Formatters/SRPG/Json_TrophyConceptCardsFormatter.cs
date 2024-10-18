// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.Json_TrophyConceptCardsFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class Json_TrophyConceptCardsFormatter : 
    IMessagePackFormatter<Json_TrophyConceptCards>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public Json_TrophyConceptCardsFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "mail",
          0
        },
        {
          "direct",
          1
        }
      };
      this.____stringByteKeys = new byte[2][]
      {
        MessagePackBinary.GetEncodedStringBytes("mail"),
        MessagePackBinary.GetEncodedStringBytes("direct")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      Json_TrophyConceptCards value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 2);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<Json_TrophyConceptCard[]>().Serialize(ref bytes, offset, value.mail, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<Json_TrophyConceptCard[]>().Serialize(ref bytes, offset, value.direct, formatterResolver);
      return offset - num;
    }

    public Json_TrophyConceptCards Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (Json_TrophyConceptCards) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      Json_TrophyConceptCard[] trophyConceptCardArray1 = (Json_TrophyConceptCard[]) null;
      Json_TrophyConceptCard[] trophyConceptCardArray2 = (Json_TrophyConceptCard[]) null;
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
              trophyConceptCardArray1 = formatterResolver.GetFormatterWithVerify<Json_TrophyConceptCard[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              trophyConceptCardArray2 = formatterResolver.GetFormatterWithVerify<Json_TrophyConceptCard[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new Json_TrophyConceptCards()
      {
        mail = trophyConceptCardArray1,
        direct = trophyConceptCardArray2
      };
    }
  }
}
