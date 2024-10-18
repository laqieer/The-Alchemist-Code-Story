// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.Json_UnitFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class Json_UnitFormatter : IMessagePackFormatter<Json_Unit>, IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public Json_UnitFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "iid",
          0
        },
        {
          "iname",
          1
        },
        {
          "rare",
          2
        },
        {
          "plus",
          3
        },
        {
          "lv",
          4
        },
        {
          "exp",
          5
        },
        {
          "fav",
          6
        },
        {
          "abil",
          7
        },
        {
          "c_abil",
          8
        },
        {
          "jobs",
          9
        },
        {
          "select",
          10
        },
        {
          "quest_clear_unlocks",
          11
        },
        {
          "elem",
          12
        },
        {
          "concept_cards",
          13
        },
        {
          "doors",
          14
        },
        {
          "door_abils",
          15
        },
        {
          "rental",
          16
        },
        {
          "favpoint",
          17
        },
        {
          "rental_iname",
          18
        },
        {
          "runes",
          19
        }
      };
      this.____stringByteKeys = new byte[20][]
      {
        MessagePackBinary.GetEncodedStringBytes("iid"),
        MessagePackBinary.GetEncodedStringBytes("iname"),
        MessagePackBinary.GetEncodedStringBytes("rare"),
        MessagePackBinary.GetEncodedStringBytes("plus"),
        MessagePackBinary.GetEncodedStringBytes("lv"),
        MessagePackBinary.GetEncodedStringBytes("exp"),
        MessagePackBinary.GetEncodedStringBytes("fav"),
        MessagePackBinary.GetEncodedStringBytes("abil"),
        MessagePackBinary.GetEncodedStringBytes("c_abil"),
        MessagePackBinary.GetEncodedStringBytes("jobs"),
        MessagePackBinary.GetEncodedStringBytes("select"),
        MessagePackBinary.GetEncodedStringBytes("quest_clear_unlocks"),
        MessagePackBinary.GetEncodedStringBytes("elem"),
        MessagePackBinary.GetEncodedStringBytes("concept_cards"),
        MessagePackBinary.GetEncodedStringBytes("doors"),
        MessagePackBinary.GetEncodedStringBytes("door_abils"),
        MessagePackBinary.GetEncodedStringBytes("rental"),
        MessagePackBinary.GetEncodedStringBytes("favpoint"),
        MessagePackBinary.GetEncodedStringBytes("rental_iname"),
        MessagePackBinary.GetEncodedStringBytes("runes")
      };
    }

    public int Serialize(
      ref byte[] bytes,
      int offset,
      Json_Unit value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, 20);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += MessagePackBinary.WriteInt64(ref bytes, offset, value.iid);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rare);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.plus);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.lv);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.exp);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.fav);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += formatterResolver.GetFormatterWithVerify<Json_MasterAbility>().Serialize(ref bytes, offset, value.abil, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += formatterResolver.GetFormatterWithVerify<Json_CollaboAbility>().Serialize(ref bytes, offset, value.c_abil, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += formatterResolver.GetFormatterWithVerify<Json_Job[]>().Serialize(ref bytes, offset, value.jobs, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += formatterResolver.GetFormatterWithVerify<Json_UnitSelectable>().Serialize(ref bytes, offset, value.select, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.quest_clear_unlocks, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.elem);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_ConceptCard[]>().Serialize(ref bytes, offset, value.concept_cards, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += formatterResolver.GetFormatterWithVerify<Json_Tobira[]>().Serialize(ref bytes, offset, value.doors, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
      offset += formatterResolver.GetFormatterWithVerify<Json_Ability[]>().Serialize(ref bytes, offset, value.door_abils, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[16]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rental);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[17]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.favpoint);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[18]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.rental_iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[19]);
      offset += formatterResolver.GetFormatterWithVerify<Json_RuneData[]>().Serialize(ref bytes, offset, value.runes, formatterResolver);
      return offset - num;
    }

    public Json_Unit Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (Json_Unit) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      long num3 = 0;
      string str1 = (string) null;
      int num4 = 0;
      int num5 = 0;
      int num6 = 0;
      int num7 = 0;
      int num8 = 0;
      Json_MasterAbility jsonMasterAbility = (Json_MasterAbility) null;
      Json_CollaboAbility jsonCollaboAbility = (Json_CollaboAbility) null;
      Json_Job[] jsonJobArray = (Json_Job[]) null;
      Json_UnitSelectable jsonUnitSelectable = (Json_UnitSelectable) null;
      string[] strArray = (string[]) null;
      int num9 = 0;
      JSON_ConceptCard[] jsonConceptCardArray = (JSON_ConceptCard[]) null;
      Json_Tobira[] jsonTobiraArray = (Json_Tobira[]) null;
      Json_Ability[] jsonAbilityArray = (Json_Ability[]) null;
      int num10 = 0;
      int num11 = 0;
      string str2 = (string) null;
      Json_RuneData[] jsonRuneDataArray = (Json_RuneData[]) null;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num12;
        if (!this.____keyMapping.TryGetValueSafe(key, out num12))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num12)
          {
            case 0:
              num3 = MessagePackBinary.ReadInt64(bytes, offset, out readSize);
              break;
            case 1:
              str1 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
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
              num7 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 6:
              num8 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 7:
              jsonMasterAbility = formatterResolver.GetFormatterWithVerify<Json_MasterAbility>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 8:
              jsonCollaboAbility = formatterResolver.GetFormatterWithVerify<Json_CollaboAbility>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 9:
              jsonJobArray = formatterResolver.GetFormatterWithVerify<Json_Job[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 10:
              jsonUnitSelectable = formatterResolver.GetFormatterWithVerify<Json_UnitSelectable>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 11:
              strArray = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 12:
              num9 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 13:
              jsonConceptCardArray = formatterResolver.GetFormatterWithVerify<JSON_ConceptCard[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 14:
              jsonTobiraArray = formatterResolver.GetFormatterWithVerify<Json_Tobira[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 15:
              jsonAbilityArray = formatterResolver.GetFormatterWithVerify<Json_Ability[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 16:
              num10 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 17:
              num11 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 18:
              str2 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 19:
              jsonRuneDataArray = formatterResolver.GetFormatterWithVerify<Json_RuneData[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            default:
              readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
              break;
          }
        }
        offset += readSize;
      }
      readSize = offset - num1;
      return new Json_Unit()
      {
        iid = num3,
        iname = str1,
        rare = num4,
        plus = num5,
        lv = num6,
        exp = num7,
        fav = num8,
        abil = jsonMasterAbility,
        c_abil = jsonCollaboAbility,
        jobs = jsonJobArray,
        select = jsonUnitSelectable,
        quest_clear_unlocks = strArray,
        elem = num9,
        concept_cards = jsonConceptCardArray,
        doors = jsonTobiraArray,
        door_abils = jsonAbilityArray,
        rental = num10,
        favpoint = num11,
        rental_iname = str2,
        runes = jsonRuneDataArray
      };
    }
  }
}
