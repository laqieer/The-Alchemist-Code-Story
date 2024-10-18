// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.Json_PlayerDataFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class Json_PlayerDataFormatter : 
    IMessagePackFormatter<Json_PlayerData>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public Json_PlayerDataFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "name",
          0
        },
        {
          "exp",
          1
        },
        {
          "lv",
          2
        },
        {
          "gold",
          3
        },
        {
          "abilpt",
          4
        },
        {
          "coin",
          5
        },
        {
          "arenacoin",
          6
        },
        {
          "multicoin",
          7
        },
        {
          "enseicoin",
          8
        },
        {
          "kakeracoin",
          9
        },
        {
          "cnt_multi",
          10
        },
        {
          "cnt_stmrecover",
          11
        },
        {
          "cnt_buygold",
          12
        },
        {
          "cuid",
          13
        },
        {
          "fuid",
          14
        },
        {
          "logincont",
          15
        },
        {
          "mail_unread",
          16
        },
        {
          "mail_f_unread",
          17
        },
        {
          "btlid",
          18
        },
        {
          "btltype",
          19
        },
        {
          "tuid",
          20
        },
        {
          "stamina",
          21
        },
        {
          "cave",
          22
        },
        {
          "abilup",
          23
        },
        {
          "arena",
          24
        },
        {
          "tour",
          25
        },
        {
          "vip",
          26
        },
        {
          "premium",
          27
        },
        {
          "unitbox_max",
          28
        },
        {
          "itembox_max",
          29
        },
        {
          "gachag",
          30
        },
        {
          "gachac",
          31
        },
        {
          "gachap",
          32
        },
        {
          "friends",
          33
        },
        {
          "newgame_at",
          34
        },
        {
          "selected_award",
          35
        },
        {
          "multi",
          36
        },
        {
          "g_shop",
          37
        }
      };
      this.____stringByteKeys = new byte[38][]
      {
        MessagePackBinary.GetEncodedStringBytes("name"),
        MessagePackBinary.GetEncodedStringBytes("exp"),
        MessagePackBinary.GetEncodedStringBytes("lv"),
        MessagePackBinary.GetEncodedStringBytes("gold"),
        MessagePackBinary.GetEncodedStringBytes("abilpt"),
        MessagePackBinary.GetEncodedStringBytes("coin"),
        MessagePackBinary.GetEncodedStringBytes("arenacoin"),
        MessagePackBinary.GetEncodedStringBytes("multicoin"),
        MessagePackBinary.GetEncodedStringBytes("enseicoin"),
        MessagePackBinary.GetEncodedStringBytes("kakeracoin"),
        MessagePackBinary.GetEncodedStringBytes("cnt_multi"),
        MessagePackBinary.GetEncodedStringBytes("cnt_stmrecover"),
        MessagePackBinary.GetEncodedStringBytes("cnt_buygold"),
        MessagePackBinary.GetEncodedStringBytes("cuid"),
        MessagePackBinary.GetEncodedStringBytes("fuid"),
        MessagePackBinary.GetEncodedStringBytes("logincont"),
        MessagePackBinary.GetEncodedStringBytes("mail_unread"),
        MessagePackBinary.GetEncodedStringBytes("mail_f_unread"),
        MessagePackBinary.GetEncodedStringBytes("btlid"),
        MessagePackBinary.GetEncodedStringBytes("btltype"),
        MessagePackBinary.GetEncodedStringBytes("tuid"),
        MessagePackBinary.GetEncodedStringBytes("stamina"),
        MessagePackBinary.GetEncodedStringBytes("cave"),
        MessagePackBinary.GetEncodedStringBytes("abilup"),
        MessagePackBinary.GetEncodedStringBytes("arena"),
        MessagePackBinary.GetEncodedStringBytes("tour"),
        MessagePackBinary.GetEncodedStringBytes("vip"),
        MessagePackBinary.GetEncodedStringBytes("premium"),
        MessagePackBinary.GetEncodedStringBytes("unitbox_max"),
        MessagePackBinary.GetEncodedStringBytes("itembox_max"),
        MessagePackBinary.GetEncodedStringBytes("gachag"),
        MessagePackBinary.GetEncodedStringBytes("gachac"),
        MessagePackBinary.GetEncodedStringBytes("gachap"),
        MessagePackBinary.GetEncodedStringBytes("friends"),
        MessagePackBinary.GetEncodedStringBytes("newgame_at"),
        MessagePackBinary.GetEncodedStringBytes("selected_award"),
        MessagePackBinary.GetEncodedStringBytes("multi"),
        MessagePackBinary.GetEncodedStringBytes("g_shop")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      Json_PlayerData value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, 38);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.name, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.exp);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.lv);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.gold);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.abilpt);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += formatterResolver.GetFormatterWithVerify<Json_Coin>().Serialize(ref bytes, offset, value.coin, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.arenacoin);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.multicoin);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.enseicoin);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.kakeracoin);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.cnt_multi);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.cnt_stmrecover);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.cnt_buygold);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.cuid, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.fuid, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.logincont);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[16]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mail_unread);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[17]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.mail_f_unread);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[18]);
      offset += MessagePackBinary.WriteInt64(ref bytes, offset, value.btlid);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[19]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.btltype, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[20]);
      offset += formatterResolver.GetFormatterWithVerify<Json_Hikkoshi>().Serialize(ref bytes, offset, value.tuid, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[21]);
      offset += formatterResolver.GetFormatterWithVerify<Json_Stamina>().Serialize(ref bytes, offset, value.stamina, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[22]);
      offset += formatterResolver.GetFormatterWithVerify<Json_Cave>().Serialize(ref bytes, offset, value.cave, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[23]);
      offset += formatterResolver.GetFormatterWithVerify<Json_AbilityUp>().Serialize(ref bytes, offset, value.abilup, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[24]);
      offset += formatterResolver.GetFormatterWithVerify<Json_Arena>().Serialize(ref bytes, offset, value.arena, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[25]);
      offset += formatterResolver.GetFormatterWithVerify<Json_Tour>().Serialize(ref bytes, offset, value.tour, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[26]);
      offset += formatterResolver.GetFormatterWithVerify<Json_Vip>().Serialize(ref bytes, offset, value.vip, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[27]);
      offset += formatterResolver.GetFormatterWithVerify<Json_Premium>().Serialize(ref bytes, offset, value.premium, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[28]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.unitbox_max);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[29]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.itembox_max);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[30]);
      offset += formatterResolver.GetFormatterWithVerify<Json_FreeGacha>().Serialize(ref bytes, offset, value.gachag, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[31]);
      offset += formatterResolver.GetFormatterWithVerify<Json_FreeGacha>().Serialize(ref bytes, offset, value.gachac, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[32]);
      offset += formatterResolver.GetFormatterWithVerify<Json_PaidGacha>().Serialize(ref bytes, offset, value.gachap, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[33]);
      offset += formatterResolver.GetFormatterWithVerify<Json_Friends>().Serialize(ref bytes, offset, value.friends, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[34]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.newgame_at);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[35]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.selected_award, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[36]);
      offset += formatterResolver.GetFormatterWithVerify<Json_MultiOption>().Serialize(ref bytes, offset, value.multi, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[37]);
      offset += formatterResolver.GetFormatterWithVerify<Json_GuerrillaShopPeriod>().Serialize(ref bytes, offset, value.g_shop, formatterResolver);
      return offset - num;
    }

    public Json_PlayerData Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (Json_PlayerData) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      string str1 = (string) null;
      int num3 = 0;
      int num4 = 0;
      int num5 = 0;
      int num6 = 0;
      Json_Coin jsonCoin = (Json_Coin) null;
      int num7 = 0;
      int num8 = 0;
      int num9 = 0;
      int num10 = 0;
      int num11 = 0;
      int num12 = 0;
      int num13 = 0;
      string str2 = (string) null;
      string str3 = (string) null;
      int num14 = 0;
      int num15 = 0;
      int num16 = 0;
      long num17 = 0;
      string str4 = (string) null;
      Json_Hikkoshi jsonHikkoshi = (Json_Hikkoshi) null;
      Json_Stamina jsonStamina = (Json_Stamina) null;
      Json_Cave jsonCave = (Json_Cave) null;
      Json_AbilityUp jsonAbilityUp = (Json_AbilityUp) null;
      Json_Arena jsonArena = (Json_Arena) null;
      Json_Tour jsonTour = (Json_Tour) null;
      Json_Vip jsonVip = (Json_Vip) null;
      Json_Premium jsonPremium = (Json_Premium) null;
      int num18 = 0;
      int num19 = 0;
      Json_FreeGacha jsonFreeGacha1 = (Json_FreeGacha) null;
      Json_FreeGacha jsonFreeGacha2 = (Json_FreeGacha) null;
      Json_PaidGacha jsonPaidGacha = (Json_PaidGacha) null;
      Json_Friends jsonFriends = (Json_Friends) null;
      int num20 = 0;
      string str5 = (string) null;
      Json_MultiOption jsonMultiOption = (Json_MultiOption) null;
      Json_GuerrillaShopPeriod guerrillaShopPeriod = (Json_GuerrillaShopPeriod) null;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num21;
        if (!this.____keyMapping.TryGetValueSafe(key, out num21))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num21)
          {
            case 0:
              str1 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 1:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 2:
              num4 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 3:
              num5 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 4:
              num6 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 5:
              jsonCoin = formatterResolver.GetFormatterWithVerify<Json_Coin>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 6:
              num7 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 7:
              num8 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 8:
              num9 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 9:
              num10 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 10:
              num11 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 11:
              num12 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 12:
              num13 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 13:
              str2 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 14:
              str3 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 15:
              num14 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 16:
              num15 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 17:
              num16 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 18:
              num17 = MessagePackBinary.ReadInt64(bytes, offset, out readSize);
              break;
            case 19:
              str4 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 20:
              jsonHikkoshi = formatterResolver.GetFormatterWithVerify<Json_Hikkoshi>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 21:
              jsonStamina = formatterResolver.GetFormatterWithVerify<Json_Stamina>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 22:
              jsonCave = formatterResolver.GetFormatterWithVerify<Json_Cave>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 23:
              jsonAbilityUp = formatterResolver.GetFormatterWithVerify<Json_AbilityUp>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 24:
              jsonArena = formatterResolver.GetFormatterWithVerify<Json_Arena>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 25:
              jsonTour = formatterResolver.GetFormatterWithVerify<Json_Tour>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 26:
              jsonVip = formatterResolver.GetFormatterWithVerify<Json_Vip>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 27:
              jsonPremium = formatterResolver.GetFormatterWithVerify<Json_Premium>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 28:
              num18 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 29:
              num19 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 30:
              jsonFreeGacha1 = formatterResolver.GetFormatterWithVerify<Json_FreeGacha>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 31:
              jsonFreeGacha2 = formatterResolver.GetFormatterWithVerify<Json_FreeGacha>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 32:
              jsonPaidGacha = formatterResolver.GetFormatterWithVerify<Json_PaidGacha>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 33:
              jsonFriends = formatterResolver.GetFormatterWithVerify<Json_Friends>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 34:
              num20 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 35:
              str5 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 36:
              jsonMultiOption = formatterResolver.GetFormatterWithVerify<Json_MultiOption>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 37:
              guerrillaShopPeriod = formatterResolver.GetFormatterWithVerify<Json_GuerrillaShopPeriod>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new Json_PlayerData()
      {
        name = str1,
        exp = num3,
        lv = num4,
        gold = num5,
        abilpt = num6,
        coin = jsonCoin,
        arenacoin = num7,
        multicoin = num8,
        enseicoin = num9,
        kakeracoin = num10,
        cnt_multi = num11,
        cnt_stmrecover = num12,
        cnt_buygold = num13,
        cuid = str2,
        fuid = str3,
        logincont = num14,
        mail_unread = num15,
        mail_f_unread = num16,
        btlid = num17,
        btltype = str4,
        tuid = jsonHikkoshi,
        stamina = jsonStamina,
        cave = jsonCave,
        abilup = jsonAbilityUp,
        arena = jsonArena,
        tour = jsonTour,
        vip = jsonVip,
        premium = jsonPremium,
        unitbox_max = num18,
        itembox_max = num19,
        gachag = jsonFreeGacha1,
        gachac = jsonFreeGacha2,
        gachap = jsonPaidGacha,
        friends = jsonFriends,
        newgame_at = num20,
        selected_award = str5,
        multi = jsonMultiOption,
        g_shop = guerrillaShopPeriod
      };
    }
  }
}
