// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.Json_LoginBonusTableFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class Json_LoginBonusTableFormatter : 
    IMessagePackFormatter<Json_LoginBonusTable>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public Json_LoginBonusTableFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "IsCanRecover",
          0
        },
        {
          "count",
          1
        },
        {
          "type",
          2
        },
        {
          "prefab",
          3
        },
        {
          "bonus_units",
          4
        },
        {
          "lastday",
          5
        },
        {
          "login_days",
          6
        },
        {
          "remain_recover",
          7
        },
        {
          "max_recover",
          8
        },
        {
          "current_month",
          9
        },
        {
          "bonuses",
          10
        },
        {
          "premium_bonuses",
          11
        }
      };
      this.____stringByteKeys = new byte[12][]
      {
        MessagePackBinary.GetEncodedStringBytes("IsCanRecover"),
        MessagePackBinary.GetEncodedStringBytes("count"),
        MessagePackBinary.GetEncodedStringBytes("type"),
        MessagePackBinary.GetEncodedStringBytes("prefab"),
        MessagePackBinary.GetEncodedStringBytes("bonus_units"),
        MessagePackBinary.GetEncodedStringBytes("lastday"),
        MessagePackBinary.GetEncodedStringBytes("login_days"),
        MessagePackBinary.GetEncodedStringBytes("remain_recover"),
        MessagePackBinary.GetEncodedStringBytes("max_recover"),
        MessagePackBinary.GetEncodedStringBytes("current_month"),
        MessagePackBinary.GetEncodedStringBytes("bonuses"),
        MessagePackBinary.GetEncodedStringBytes("premium_bonuses")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      Json_LoginBonusTable value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 12);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += MessagePackBinary.WriteBoolean(ref bytes, offset, value.IsCanRecover);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.count);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.type, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.prefab, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.bonus_units, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.lastday);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += formatterResolver.GetFormatterWithVerify<int[]>().Serialize(ref bytes, offset, value.login_days, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.remain_recover);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.max_recover);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.current_month);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += formatterResolver.GetFormatterWithVerify<Json_LoginBonus[]>().Serialize(ref bytes, offset, value.bonuses, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += formatterResolver.GetFormatterWithVerify<Json_PremiumLoginBonus[]>().Serialize(ref bytes, offset, value.premium_bonuses, formatterResolver);
      return offset - num;
    }

    public Json_LoginBonusTable Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (Json_LoginBonusTable) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      bool flag = false;
      int num3 = 0;
      string str1 = (string) null;
      string str2 = (string) null;
      string[] strArray = (string[]) null;
      int num4 = 0;
      int[] numArray = (int[]) null;
      int num5 = 0;
      int num6 = 0;
      int num7 = 0;
      Json_LoginBonus[] jsonLoginBonusArray = (Json_LoginBonus[]) null;
      Json_PremiumLoginBonus[] premiumLoginBonusArray = (Json_PremiumLoginBonus[]) null;
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
              flag = MessagePackBinary.ReadBoolean(bytes, offset, out readSize);
              break;
            case 1:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 2:
              str1 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 3:
              str2 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 4:
              strArray = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 5:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 6:
              numArray = formatterResolver.GetFormatterWithVerify<int[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 7:
              num5 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 8:
              num6 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 9:
              num7 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 10:
              jsonLoginBonusArray = formatterResolver.GetFormatterWithVerify<Json_LoginBonus[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 11:
              premiumLoginBonusArray = formatterResolver.GetFormatterWithVerify<Json_PremiumLoginBonus[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new Json_LoginBonusTable()
      {
        count = num3,
        type = str1,
        prefab = str2,
        bonus_units = strArray,
        lastday = num4,
        login_days = numArray,
        remain_recover = num5,
        max_recover = num6,
        current_month = num7,
        bonuses = jsonLoginBonusArray,
        premium_bonuses = premiumLoginBonusArray
      };
    }
  }
}
