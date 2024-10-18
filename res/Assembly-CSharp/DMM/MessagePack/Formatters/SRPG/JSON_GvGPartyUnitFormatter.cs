// Decompiled with JetBrains decompiler
// Type: MessagePack.Formatters.SRPG.JSON_GvGPartyUnitFormatter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Internal;
using SRPG;
using System;

#nullable disable
namespace MessagePack.Formatters.SRPG
{
  public sealed class JSON_GvGPartyUnitFormatter : 
    IMessagePackFormatter<JSON_GvGPartyUnit>,
    IMessagePackFormatter
  {
    private readonly AutomataDictionary ____keyMapping;
    private readonly byte[][] ____stringByteKeys;

    public JSON_GvGPartyUnitFormatter()
    {
      this.____keyMapping = new AutomataDictionary()
      {
        {
          "hp",
          0
        },
        {
          "iid",
          1
        },
        {
          "iname",
          2
        },
        {
          "rare",
          3
        },
        {
          "plus",
          4
        },
        {
          "lv",
          5
        },
        {
          "exp",
          6
        },
        {
          "fav",
          7
        },
        {
          "abil",
          8
        },
        {
          "c_abil",
          9
        },
        {
          "jobs",
          10
        },
        {
          "select",
          11
        },
        {
          "quest_clear_unlocks",
          12
        },
        {
          "elem",
          13
        },
        {
          "concept_cards",
          14
        },
        {
          "doors",
          15
        },
        {
          "door_abils",
          16
        },
        {
          "rental",
          17
        },
        {
          "favpoint",
          18
        },
        {
          "rental_iname",
          19
        },
        {
          "runes",
          20
        }
      };
      this.____stringByteKeys = new byte[21][]
      {
        MessagePackBinary.GetEncodedStringBytes("hp"),
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
      JSON_GvGPartyUnit value,
      IFormatterResolver formatterResolver)
    {
      if (value == null)
        return MessagePackBinary.WriteNil(ref bytes, offset);
      int num = offset;
      offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, 21);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.hp);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
      offset += MessagePackBinary.WriteInt64(ref bytes, offset, value.iid);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rare);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.plus);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.lv);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.exp);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.fav);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
      offset += formatterResolver.GetFormatterWithVerify<Json_MasterAbility>().Serialize(ref bytes, offset, value.abil, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[9]);
      offset += formatterResolver.GetFormatterWithVerify<Json_CollaboAbility>().Serialize(ref bytes, offset, value.c_abil, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[10]);
      offset += formatterResolver.GetFormatterWithVerify<Json_Job[]>().Serialize(ref bytes, offset, value.jobs, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[11]);
      offset += formatterResolver.GetFormatterWithVerify<Json_UnitSelectable>().Serialize(ref bytes, offset, value.select, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[12]);
      offset += formatterResolver.GetFormatterWithVerify<string[]>().Serialize(ref bytes, offset, value.quest_clear_unlocks, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[13]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.elem);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[14]);
      offset += formatterResolver.GetFormatterWithVerify<JSON_ConceptCard[]>().Serialize(ref bytes, offset, value.concept_cards, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[15]);
      offset += formatterResolver.GetFormatterWithVerify<Json_Tobira[]>().Serialize(ref bytes, offset, value.doors, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[16]);
      offset += formatterResolver.GetFormatterWithVerify<Json_Ability[]>().Serialize(ref bytes, offset, value.door_abils, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[17]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.rental);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[18]);
      offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.favpoint);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[19]);
      offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.rental_iname, formatterResolver);
      offset += MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[20]);
      offset += formatterResolver.GetFormatterWithVerify<Json_RuneData[]>().Serialize(ref bytes, offset, value.runes, formatterResolver);
      return offset - num;
    }

    public JSON_GvGPartyUnit Deserialize(
      byte[] bytes,
      int offset,
      IFormatterResolver formatterResolver,
      out int readSize)
    {
      if (MessagePackBinary.IsNil(bytes, offset))
      {
        readSize = 1;
        return (JSON_GvGPartyUnit) null;
      }
      int num1 = offset;
      int num2 = MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
      offset += readSize;
      int num3 = 0;
      long num4 = 0;
      string str1 = (string) null;
      int num5 = 0;
      int num6 = 0;
      int num7 = 0;
      int num8 = 0;
      int num9 = 0;
      Json_MasterAbility jsonMasterAbility = (Json_MasterAbility) null;
      Json_CollaboAbility jsonCollaboAbility = (Json_CollaboAbility) null;
      Json_Job[] jsonJobArray = (Json_Job[]) null;
      Json_UnitSelectable jsonUnitSelectable = (Json_UnitSelectable) null;
      string[] strArray = (string[]) null;
      int num10 = 0;
      JSON_ConceptCard[] jsonConceptCardArray = (JSON_ConceptCard[]) null;
      Json_Tobira[] jsonTobiraArray = (Json_Tobira[]) null;
      Json_Ability[] jsonAbilityArray = (Json_Ability[]) null;
      int num11 = 0;
      int num12 = 0;
      string str2 = (string) null;
      Json_RuneData[] jsonRuneDataArray = (Json_RuneData[]) null;
      for (int index = 0; index < num2; ++index)
      {
        ArraySegment<byte> key = MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
        offset += readSize;
        int num13;
        if (!this.____keyMapping.TryGetValueSafe(key, out num13))
        {
          readSize = MessagePackBinary.ReadNextBlock(bytes, offset);
        }
        else
        {
          switch (num13)
          {
            case 0:
              num3 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 1:
              num4 = MessagePackBinary.ReadInt64(bytes, offset, out readSize);
              break;
            case 2:
              str1 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
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
              num9 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 8:
              jsonMasterAbility = formatterResolver.GetFormatterWithVerify<Json_MasterAbility>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 9:
              jsonCollaboAbility = formatterResolver.GetFormatterWithVerify<Json_CollaboAbility>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 10:
              jsonJobArray = formatterResolver.GetFormatterWithVerify<Json_Job[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 11:
              jsonUnitSelectable = formatterResolver.GetFormatterWithVerify<Json_UnitSelectable>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 12:
              strArray = formatterResolver.GetFormatterWithVerify<string[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 13:
              num10 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 14:
              jsonConceptCardArray = formatterResolver.GetFormatterWithVerify<JSON_ConceptCard[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 15:
              jsonTobiraArray = formatterResolver.GetFormatterWithVerify<Json_Tobira[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 16:
              jsonAbilityArray = formatterResolver.GetFormatterWithVerify<Json_Ability[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 17:
              num11 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 18:
              num12 = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
              break;
            case 19:
              str2 = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
              break;
            case 20:
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
      JSON_GvGPartyUnit jsonGvGpartyUnit = new JSON_GvGPartyUnit();
      jsonGvGpartyUnit.hp = num3;
      jsonGvGpartyUnit.iid = num4;
      jsonGvGpartyUnit.iname = str1;
      jsonGvGpartyUnit.rare = num5;
      jsonGvGpartyUnit.plus = num6;
      jsonGvGpartyUnit.lv = num7;
      jsonGvGpartyUnit.exp = num8;
      jsonGvGpartyUnit.fav = num9;
      jsonGvGpartyUnit.abil = jsonMasterAbility;
      jsonGvGpartyUnit.c_abil = jsonCollaboAbility;
      jsonGvGpartyUnit.jobs = jsonJobArray;
      jsonGvGpartyUnit.select = jsonUnitSelectable;
      jsonGvGpartyUnit.quest_clear_unlocks = strArray;
      jsonGvGpartyUnit.elem = num10;
      jsonGvGpartyUnit.concept_cards = jsonConceptCardArray;
      jsonGvGpartyUnit.doors = jsonTobiraArray;
      jsonGvGpartyUnit.door_abils = jsonAbilityArray;
      jsonGvGpartyUnit.rental = num11;
      jsonGvGpartyUnit.favpoint = num12;
      jsonGvGpartyUnit.rental_iname = str2;
      jsonGvGpartyUnit.runes = jsonRuneDataArray;
      return jsonGvGpartyUnit;
    }
  }
}
