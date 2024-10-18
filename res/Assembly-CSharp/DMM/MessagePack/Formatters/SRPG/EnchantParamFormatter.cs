// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.EnchantParamFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class EnchantParamFormatter : 
    IMessagePackFormatter<EnchantParam>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public EnchantParamFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "poison",
          0
        },
        {
          "paralyse",
          1
        },
        {
          "stun",
          2
        },
        {
          "sleep",
          3
        },
        {
          "charm",
          4
        },
        {
          "stone",
          5
        },
        {
          "blind",
          6
        },
        {
          "notskl",
          7
        },
        {
          "notmov",
          8
        },
        {
          "notatk",
          9
        },
        {
          "zombie",
          10
        },
        {
          "death",
          11
        },
        {
          "berserk",
          12
        },
        {
          "knockback",
          13
        },
        {
          "resist_buff",
          14
        },
        {
          "resist_debuff",
          15
        },
        {
          "stop",
          16
        },
        {
          "fast",
          17
        },
        {
          "slow",
          18
        },
        {
          "auto_heal",
          19
        },
        {
          "donsoku",
          20
        },
        {
          "rage",
          21
        },
        {
          "good_sleep",
          22
        },
        {
          "auto_jewel",
          23
        },
        {
          "notheal",
          24
        },
        {
          "single_attack",
          25
        },
        {
          "area_attack",
          26
        },
        {
          "dec_ct",
          27
        },
        {
          "inc_ct",
          28
        },
        {
          "esa_fire",
          29
        },
        {
          "esa_water",
          30
        },
        {
          "esa_wind",
          31
        },
        {
          "esa_thunder",
          32
        },
        {
          "esa_shine",
          33
        },
        {
          "esa_dark",
          34
        },
        {
          "max_damage_hp",
          35
        },
        {
          "max_damage_mp",
          36
        },
        {
          "side_attack",
          37
        },
        {
          "back_attack",
          38
        },
        {
          "obst_reaction",
          39
        },
        {
          "forced_targeting",
          40
        },
        {
          "values",
          41
        }
      };
      this.____stringByteKeys = new byte[42][]
      {
        MessagePackBinary.GetEncodedStringBytes("poison"),
        MessagePackBinary.GetEncodedStringBytes("paralyse"),
        MessagePackBinary.GetEncodedStringBytes("stun"),
        MessagePackBinary.GetEncodedStringBytes("sleep"),
        MessagePackBinary.GetEncodedStringBytes("charm"),
        MessagePackBinary.GetEncodedStringBytes("stone"),
        MessagePackBinary.GetEncodedStringBytes("blind"),
        MessagePackBinary.GetEncodedStringBytes("notskl"),
        MessagePackBinary.GetEncodedStringBytes("notmov"),
        MessagePackBinary.GetEncodedStringBytes("notatk"),
        MessagePackBinary.GetEncodedStringBytes("zombie"),
        MessagePackBinary.GetEncodedStringBytes("death"),
        MessagePackBinary.GetEncodedStringBytes("berserk"),
        MessagePackBinary.GetEncodedStringBytes("knockback"),
        MessagePackBinary.GetEncodedStringBytes("resist_buff"),
        MessagePackBinary.GetEncodedStringBytes("resist_debuff"),
        MessagePackBinary.GetEncodedStringBytes("stop"),
        MessagePackBinary.GetEncodedStringBytes("fast"),
        MessagePackBinary.GetEncodedStringBytes("slow"),
        MessagePackBinary.GetEncodedStringBytes("auto_heal"),
        MessagePackBinary.GetEncodedStringBytes("donsoku"),
        MessagePackBinary.GetEncodedStringBytes("rage"),
        MessagePackBinary.GetEncodedStringBytes("good_sleep"),
        MessagePackBinary.GetEncodedStringBytes("auto_jewel"),
        MessagePackBinary.GetEncodedStringBytes("notheal"),
        MessagePackBinary.GetEncodedStringBytes("single_attack"),
        MessagePackBinary.GetEncodedStringBytes("area_attack"),
        MessagePackBinary.GetEncodedStringBytes("dec_ct"),
        MessagePackBinary.GetEncodedStringBytes("inc_ct"),
        MessagePackBinary.GetEncodedStringBytes("esa_fire"),
        MessagePackBinary.GetEncodedStringBytes("esa_water"),
        MessagePackBinary.GetEncodedStringBytes("esa_wind"),
        MessagePackBinary.GetEncodedStringBytes("esa_thunder"),
        MessagePackBinary.GetEncodedStringBytes("esa_shine"),
        MessagePackBinary.GetEncodedStringBytes("esa_dark"),
        MessagePackBinary.GetEncodedStringBytes("max_damage_hp"),
        MessagePackBinary.GetEncodedStringBytes("max_damage_mp"),
        MessagePackBinary.GetEncodedStringBytes("side_attack"),
        MessagePackBinary.GetEncodedStringBytes("back_attack"),
        MessagePackBinary.GetEncodedStringBytes("obst_reaction"),
        MessagePackBinary.GetEncodedStringBytes("forced_targeting"),
        MessagePackBinary.GetEncodedStringBytes("values")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      EnchantParam value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, 42);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.poison);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.paralyse);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.stun);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.sleep);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.charm);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.stone);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.blind);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.notskl);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.notmov);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.notatk);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.zombie);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.death);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.berserk);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.knockback);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.resist_buff);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.resist_debuff);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[16]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.stop);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[17]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.fast);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[18]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.slow);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[19]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.auto_heal);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[20]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.donsoku);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[21]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.rage);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[22]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.good_sleep);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[23]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.auto_jewel);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[24]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.notheal);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[25]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.single_attack);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[26]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.area_attack);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[27]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.dec_ct);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[28]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.inc_ct);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[29]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.esa_fire);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[30]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.esa_water);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[31]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.esa_wind);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[32]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.esa_thunder);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[33]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.esa_shine);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[34]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.esa_dark);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[35]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.max_damage_hp);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[36]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.max_damage_mp);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[37]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.side_attack);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[38]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.back_attack);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[39]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.obst_reaction);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[40]);
      offset += MessagePackBinary.WriteInt16(ref bytes, offset, value.forced_targeting);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[41]);
      offset += formatterResolver.GetFormatterWithVerify<short[]>().Serialize(ref bytes, offset, value.values, formatterResolver);
      return offset - num;
    }

    public EnchantParam Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (EnchantParam) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      short num3 = 0;
      short num4 = 0;
      short num5 = 0;
      short num6 = 0;
      short num7 = 0;
      short num8 = 0;
      short num9 = 0;
      short num10 = 0;
      short num11 = 0;
      short num12 = 0;
      short num13 = 0;
      short num14 = 0;
      short num15 = 0;
      short num16 = 0;
      short num17 = 0;
      short num18 = 0;
      short num19 = 0;
      short num20 = 0;
      short num21 = 0;
      short num22 = 0;
      short num23 = 0;
      short num24 = 0;
      short num25 = 0;
      short num26 = 0;
      short num27 = 0;
      short num28 = 0;
      short num29 = 0;
      short num30 = 0;
      short num31 = 0;
      short num32 = 0;
      short num33 = 0;
      short num34 = 0;
      short num35 = 0;
      short num36 = 0;
      short num37 = 0;
      short num38 = 0;
      short num39 = 0;
      short num40 = 0;
      short num41 = 0;
      short num42 = 0;
      short num43 = 0;
      short[] numArray = (short[]) null;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num44;
        if (!this.____keyMapping.TryGetValueSafe(key, out num44))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num44)
          {
            case 0:
              num3 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 1:
              num4 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 2:
              num5 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 3:
              num6 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 4:
              num7 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 5:
              num8 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 6:
              num9 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 7:
              num10 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 8:
              num11 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 9:
              num12 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 10:
              num13 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 11:
              num14 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 12:
              num15 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 13:
              num16 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 14:
              num17 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 15:
              num18 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 16:
              num19 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 17:
              num20 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 18:
              num21 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 19:
              num22 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 20:
              num23 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 21:
              num24 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 22:
              num25 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 23:
              num26 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 24:
              num27 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 25:
              num28 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 26:
              num29 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 27:
              num30 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 28:
              num31 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 29:
              num32 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 30:
              num33 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 31:
              num34 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 32:
              num35 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 33:
              num36 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 34:
              num37 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 35:
              num38 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 36:
              num39 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 37:
              num40 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 38:
              num41 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 39:
              num42 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 40:
              num43 = MessagePackBinary.ReadInt16(bytes, offset, out readSize);
              break;
            case 41:
              numArray = formatterResolver.GetFormatterWithVerify<short[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new EnchantParam()
      {
        poison = num3,
        paralyse = num4,
        stun = num5,
        sleep = num6,
        charm = num7,
        stone = num8,
        blind = num9,
        notskl = num10,
        notmov = num11,
        notatk = num12,
        zombie = num13,
        death = num14,
        berserk = num15,
        knockback = num16,
        resist_buff = num17,
        resist_debuff = num18,
        stop = num19,
        fast = num20,
        slow = num21,
        auto_heal = num22,
        donsoku = num23,
        rage = num24,
        good_sleep = num25,
        auto_jewel = num26,
        notheal = num27,
        single_attack = num28,
        area_attack = num29,
        dec_ct = num30,
        inc_ct = num31,
        esa_fire = num32,
        esa_water = num33,
        esa_wind = num34,
        esa_thunder = num35,
        esa_shine = num36,
        esa_dark = num37,
        max_damage_hp = num38,
        max_damage_mp = num39,
        side_attack = num40,
        back_attack = num41,
        obst_reaction = num42,
        forced_targeting = num43,
        values = numArray
      };
    }
  }
}
